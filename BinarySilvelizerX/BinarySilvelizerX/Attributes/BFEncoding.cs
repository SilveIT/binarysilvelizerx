using System;
using System.Text;
using BinarySilvelizerX.Utils;

namespace BinarySilvelizerX.Attributes
{
    public class BFEncodingAttribute : Attribute
    {
        internal Encoding Encoding { get; }

        public BFEncodingAttribute(TextUtils.CodePage codePage)
        {
            Encoding = Encoding.GetEncoding((int)codePage);
        }
    }
}