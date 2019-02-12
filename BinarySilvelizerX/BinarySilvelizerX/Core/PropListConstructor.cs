using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Extensions;

namespace BinarySilvelizerX.Core
{
    internal static class PropListConstructor
    {
        internal static PropertyInfo[] GeneratePropertyInfoList(Type objectType)
        {
            //Stage 1, getting attribute data
            var attributes = objectType.GetAttributesArray<SerializationModeAttribute>();
            var accessDefault = SerializerDefaults.DefaultPropAccessMode;
            var accessorDefault = SerializerDefaults.DefaultAccessorMode;
            var accessFilter = accessDefault;
            var accessorFilter = accessorDefault;
            string startPropStr = null;
            string endPropStr = null;
            if (attributes != null)
                foreach (var attribute in attributes)
                {
                    if (attribute.AccessMode != accessDefault)
                        accessFilter = attribute.AccessMode;
                    if (attribute.AccessorMode != accessorDefault)
                        accessorFilter = attribute.AccessorMode;
                    switch (attribute.OffsetMode)
                    {
                        case SerializationOffsetMode.Unrestricted:
                            break;

                        case SerializationOffsetMode.StartingFrom:
                            startPropStr = attribute.PropertyName;
                            break;

                        case SerializationOffsetMode.EndingOn:
                            endPropStr = attribute.PropertyName;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

            //Stage 2, getting properties based on received property access data
            var bindingFlags = GetBindingFlags(accessFilter);
            var props = objectType.GetProperties(bindingFlags);
            if (props.Length == 0) return props; //Already empty.. So we don't wanna go deeper
            props = GetPropertiesByModes(props, accessFilter, accessorFilter);
            if (props.Length == 0 || startPropStr == null && endPropStr == null) return props; //-V3130

            //Stage 3, processing startProp and endProp data
            return GetPropertiesByStartEnd(props, startPropStr, endPropStr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static PropertyInfo[] GetPropertiesByStartEnd(PropertyInfo[] source, string startProp, string endProp)
        {
            var startIndex = startProp != null ? GetPropertyIndex(source, startProp) : 0;
            var endIndex = endProp != null ? GetPropertyIndex(source, endProp) : source.Length - 1;

            var count = endIndex - startIndex + 1;
            if (count < 1)
                throw new Exception("Count of properties marked for (de-)serialization < 1");
            return new ArraySegment<PropertyInfo>(source, startIndex, count).ToArray(); //TODO check performance
            //var dest = new PropertyInfo[count];
            //Array.Copy(source, startIndex, dest, 0, count);
            //return dest;
        }

        private static PropertyInfo[] GetPropertiesByModes(IEnumerable<PropertyInfo> source,
            SerializationAccessMode access,
            SerializationAccessorMode accessor)
        {
            var res = source.Where(t => accessor == SerializationAccessorMode.OnlyReadable
                ? !t.CanWrite
                : (accessor == SerializationAccessorMode.OnlyWritable
                      ? !t.CanRead
                      : accessor != SerializationAccessorMode.OnlyBoth || t.CanRead & t.CanWrite)
                  && !Attribute.IsDefined(t, typeof(BFIgnoredAttribute)));
            return access == SerializationAccessMode.OnlyByteFields
                ? res.Where(t => Attribute.IsDefined(t, typeof(ByteFieldAttribute))).ToArray()
                : res.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetPropertyIndex(PropertyInfo[] props, string name)
        {
            var startProp = props.First(t => t.Name == name);
            if (startProp != null)
                return Array.IndexOf(props, startProp);
            throw new Exception("Could not find property by supplied name");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BindingFlags GetBindingFlags(SerializationAccessMode accessFilter)
        {
            var curFlag = BindingFlags.Instance;
            if (accessFilter == SerializationAccessMode.OnlyByteFields)
                curFlag |= BindingFlags.Public | BindingFlags.NonPublic;
            else
            {
                if ((accessFilter & SerializationAccessMode.AllPublic) != 0)
                    curFlag |= BindingFlags.Public;
                if ((accessFilter & SerializationAccessMode.AllPrivate) != 0)
                    curFlag |= BindingFlags.NonPublic;
            }
            return curFlag;
        }

        //private static IEnumerable<SerializationAccessMode> GetSeparatedFlags(SerializationAccessMode flag)
        //{
        //    var result = (ulong)flag;
        //    var values = Enum.GetValues(flag.GetType()).Cast<SerializationAccessMode>().Where(t => flag.HasFlag(t)).OrderBy(a => a).ToArray();
        //    var index = values.Length - 1;
        //    var resOut = new List<SerializationAccessMode>();
        //    while (index >= 0)
        //    {
        //        var curVal = (ulong)values[index];

        //        if (index == 0 && values[index] == 0)
        //            break;

        //        if ((result & curVal) == curVal)
        //        {
        //            result -= curVal;
        //            resOut.Add(values[index]);
        //        }

        //        index--;
        //    }
        //    return resOut;
        //}
    }
}