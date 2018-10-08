using System.ComponentModel;

// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace BinarySilvelizerX.Utils
{
    public static class TextUtils
    {
        // ReSharper disable InconsistentNaming
        /// <summary>
        /// CodePage enum, msdn: https://msdn.microsoft.com/en-us/library/system.text.encoding(v=vs.110).aspx#Remarks
        /// </summary>
        public enum CodePage
        {
            /// <summary>Default system encoding</summary>
            [Description("Default system encoding")]
            Default = 0,

            /// <summary>IBM EBCDIC (US-Canada)</summary>
            [Description("IBM EBCDIC (US-Canada)")]
            IBM037 = 37,

            /// <summary>OEM United States</summary>
            [Description("OEM United States")]
            IBM437 = 437,

            /// <summary>IBM EBCDIC (International)</summary>
            [Description("IBM EBCDIC (International)")]
            IBM500 = 500,

            /// <summary>Arabic (ASMO 708)</summary>
            [Description("Arabic (ASMO 708)")]
            ASMO708 = 708,

            /// <summary>Arabic (DOS)</summary>
            [Description("Arabic (DOS)")]
            DOS720 = 720,

            /// <summary>Greek (DOS)</summary>
            [Description("Greek (DOS)")]
            Ibm737 = 737,

            /// <summary>Baltic (DOS)</summary>
            [Description("Baltic (DOS)")]
            Ibm775 = 775,

            /// <summary>Western European (DOS)</summary>
            [Description("Western European (DOS)")]
            Ibm850 = 850,

            /// <summary>Central European (DOS)</summary>
            [Description("Central European (DOS)")]
            Ibm852 = 852,

            /// <summary>OEM Cyrillic</summary>
            [Description("OEM Cyrillic")]
            IBM855 = 855,

            /// <summary>Turkish (DOS)</summary>
            [Description("Turkish (DOS)")]
            Ibm857 = 857,

            /// <summary>OEM Multilingual Latin I</summary>
            [Description("OEM Multilingual Latin I")]
            IBM00858 = 858,

            /// <summary>Portuguese (DOS)</summary>
            [Description("Portuguese (DOS)")]
            IBM860 = 860,

            /// <summary>Icelandic (DOS)</summary>
            [Description("Icelandic (DOS)")]
            Ibm861 = 861,

            /// <summary>Hebrew (DOS)</summary>
            [Description("Hebrew (DOS)")]
            DOS862 = 862,

            /// <summary>French Canadian (DOS)</summary>
            [Description("French Canadian (DOS)")]
            IBM863 = 863,

            /// <summary>Arabic (864)</summary>
            [Description("Arabic (864)")]
            IBM864 = 864,

            /// <summary>Nordic (DOS)</summary>
            [Description("Nordic (DOS)")]
            IBM865 = 865,

            /// <summary>Cyrillic (DOS)</summary>
            [Description("Cyrillic (DOS)")]
            Cp866 = 866,

            /// <summary>Greek, Modern (DOS)</summary>
            [Description("Greek, Modern (DOS)")]
            Ibm869 = 869,

            /// <summary>IBM EBCDIC (Multilingual Latin-2)</summary>
            [Description("IBM EBCDIC (Multilingual Latin-2)")]
            IBM870 = 870,

            /// <summary>Thai (Windows)</summary>
            [Description("Thai (Windows)")]
            Windows874 = 874,

            /// <summary>IBM EBCDIC (Greek Modern)</summary>
            [Description("IBM EBCDIC (Greek Modern)")]
            Cp875 = 875,

            /// <summary>Japanese (Shift-JIS)</summary>
            [Description("Japanese (Shift-JIS)")]
            Shiftjis = 932,

            /// <summary>Chinese Simplified (GB2312)</summary>
            [Description("Chinese Simplified (GB2312)")]
            Gb2312 = 936,

            /// <summary>Korean</summary>
            [Description("Korean")]
            Ksc56011987 = 949,

            /// <summary>Chinese Traditional (Big5)</summary>
            [Description("Chinese Traditional (Big5)")]
            Big5 = 950,

            /// <summary>IBM EBCDIC (Turkish Latin-5)</summary>
            [Description("IBM EBCDIC (Turkish Latin-5)")]
            IBM1026 = 1026,

            /// <summary>IBM Latin-1</summary>
            [Description("IBM Latin-1")]
            IBM01047 = 1047,

            /// <summary>IBM EBCDIC (US-Canada-Euro)</summary>
            [Description("IBM EBCDIC (US-Canada-Euro)")]
            IBM01140 = 1140,

            /// <summary>IBM EBCDIC (Germany-Euro)</summary>
            [Description("IBM EBCDIC (Germany-Euro)")]
            IBM01141 = 1141,

            /// <summary>IBM EBCDIC (Denmark-Norway-Euro)</summary>
            [Description("IBM EBCDIC (Denmark-Norway-Euro)")]
            IBM01142 = 1142,

            /// <summary>IBM EBCDIC (Finland-Sweden-Euro)</summary>
            [Description("IBM EBCDIC (Finland-Sweden-Euro)")]
            IBM01143 = 1143,

            /// <summary>IBM EBCDIC (Italy-Euro)</summary>
            [Description("IBM EBCDIC (Italy-Euro)")]
            IBM01144 = 1144,

            /// <summary>IBM EBCDIC (Spain-Euro)</summary>
            [Description("IBM EBCDIC (Spain-Euro)")]
            IBM01145 = 1145,

            /// <summary>IBM EBCDIC (UK-Euro)</summary>
            [Description("IBM EBCDIC (UK-Euro)")]
            IBM01146 = 1146,

            /// <summary>IBM EBCDIC (France-Euro)</summary>
            [Description("IBM EBCDIC (France-Euro)")]
            IBM01147 = 1147,

            /// <summary>IBM EBCDIC (International-Euro)</summary>
            [Description("IBM EBCDIC (International-Euro)")]
            IBM01148 = 1148,

            /// <summary>IBM EBCDIC (Icelandic-Euro)</summary>
            [Description("IBM EBCDIC (Icelandic-Euro)")]
            IBM01149 = 1149,

            /// <summary>Unicode</summary>
            [Description("Unicode")]
            Utf16 = 1200,

            /// <summary>Unicode (Big endian)</summary>
            [Description("Unicode (Big endian)")]
            UnicodeFFFE = 1201,

            /// <summary>Central European (Windows)</summary>
            [Description("Central European (Windows)")]
            Windows1250 = 1250,

            /// <summary>Cyrillic (Windows)</summary>
            [Description("Cyrillic (Windows)")]
            Windows1251 = 1251,

            /// <summary>Western European (Windows)</summary>
            [Description("Western European (Windows)")]
            Windows1252 = 1252,

            /// <summary>Greek (Windows)</summary>
            [Description("Greek (Windows)")]
            Windows1253 = 1253,

            /// <summary>Turkish (Windows)</summary>
            [Description("Turkish (Windows)")]
            Windows1254 = 1254,

            /// <summary>Hebrew (Windows)</summary>
            [Description("Hebrew (Windows)")]
            Windows1255 = 1255,

            /// <summary>Arabic (Windows)</summary>
            [Description("Arabic (Windows)")]
            Windows1256 = 1256,

            /// <summary>Baltic (Windows)</summary>
            [Description("Baltic (Windows)")]
            Windows1257 = 1257,

            /// <summary>Vietnamese (Windows)</summary>
            [Description("Vietnamese (Windows)")]
            Windows1258 = 1258,

            /// <summary>Korean (Johab)</summary>
            [Description("Korean (Johab)")]
            Johab = 1361,

            /// <summary>Western European (Mac)</summary>
            [Description("Western European (Mac)")]
            Macintosh = 10000,

            /// <summary>Japanese (Mac)</summary>
            [Description("Japanese (Mac)")]
            Xmacjapanese = 10001,

            /// <summary>Chinese Traditional (Mac)</summary>
            [Description("Chinese Traditional (Mac)")]
            Xmacchinesetrad = 10002,

            /// <summary>Korean (Mac)</summary>
            [Description("Korean (Mac)")]
            Xmackorean = 10003,

            /// <summary>Arabic (Mac)</summary>
            [Description("Arabic (Mac)")]
            Xmacarabic = 10004,

            /// <summary>Hebrew (Mac)</summary>
            [Description("Hebrew (Mac)")]
            Xmachebrew = 10005,

            /// <summary>Greek (Mac)</summary>
            [Description("Greek (Mac)")]
            Xmacgreek = 10006,

            /// <summary>Cyrillic (Mac)</summary>
            [Description("Cyrillic (Mac)")]
            Xmaccyrillic = 10007,

            /// <summary>Chinese Simplified (Mac)</summary>
            [Description("Chinese Simplified (Mac)")]
            Xmacchinesesimp = 10008,

            /// <summary>Romanian (Mac)</summary>
            [Description("Romanian (Mac)")]
            Xmacromanian = 10010,

            /// <summary>Ukrainian (Mac)</summary>
            [Description("Ukrainian (Mac)")]
            Xmacukrainian = 10017,

            /// <summary>Thai (Mac)</summary>
            [Description("Thai (Mac)")]
            Xmacthai = 10021,

            /// <summary>Central European (Mac)</summary>
            [Description("Central European (Mac)")]
            Xmacce = 10029,

            /// <summary>Icelandic (Mac)</summary>
            [Description("Icelandic (Mac)")]
            Xmacicelandic = 10079,

            /// <summary>Turkish (Mac)</summary>
            [Description("Turkish (Mac)")]
            Xmacturkish = 10081,

            /// <summary>Croatian (Mac)</summary>
            [Description("Croatian (Mac)")]
            Xmaccroatian = 10082,

            /// <summary>Unicode (UTF-32)</summary>
            [Description("Unicode (UTF-32)")]
            Utf32 = 12000,

            /// <summary>Unicode (UTF-32 Big endian)</summary>
            [Description("Unicode (UTF-32 Big endian)")]
            Utf32BE = 12001,

            /// <summary>Chinese Traditional (CNS)</summary>
            [Description("Chinese Traditional (CNS)")]
            XChineseCNS = 20000,

            /// <summary>TCA Taiwan</summary>
            [Description("TCA Taiwan")]
            Xcp20001 = 20001,

            /// <summary>Chinese Traditional (Eten)</summary>
            [Description("Chinese Traditional (Eten)")]
            XChineseEten = 20002,

            /// <summary>IBM5550 Taiwan</summary>
            [Description("IBM5550 Taiwan")]
            Xcp20003 = 20003,

            /// <summary>TeleText Taiwan</summary>
            [Description("TeleText Taiwan")]
            Xcp20004 = 20004,

            /// <summary>Wang Taiwan</summary>
            [Description("Wang Taiwan")]
            Xcp20005 = 20005,

            /// <summary>Western European (IA5)</summary>
            [Description("Western European (IA5)")]
            XIA5 = 20105,

            /// <summary>German (IA5)</summary>
            [Description("German (IA5)")]
            XIA5German = 20106,

            /// <summary>Swedish (IA5)</summary>
            [Description("Swedish (IA5)")]
            XIA5Swedish = 20107,

            /// <summary>Norwegian (IA5)</summary>
            [Description("Norwegian (IA5)")]
            XIA5Norwegian = 20108,

            /// <summary>US-ASCII</summary>
            [Description("US-ASCII")]
            UsAscii = 20127,

            /// <summary>T.61</summary>
            [Description("T.61")]
            Xcp20261 = 20261,

            /// <summary>ISO-6937</summary>
            [Description("ISO-6937")]
            Xcp20269 = 20269,

            /// <summary>IBM EBCDIC (Germany)</summary>
            [Description("IBM EBCDIC (Germany)")]
            IBM273 = 20273,

            /// <summary>IBM EBCDIC (Denmark-Norway)</summary>
            [Description("IBM EBCDIC (Denmark-Norway)")]
            IBM277 = 20277,

            /// <summary>IBM EBCDIC (Finland-Sweden)</summary>
            [Description("IBM EBCDIC (Finland-Sweden)")]
            IBM278 = 20278,

            /// <summary>IBM EBCDIC (Italy)</summary>
            [Description("IBM EBCDIC (Italy)")]
            IBM280 = 20280,

            /// <summary>IBM EBCDIC (Spain)</summary>
            [Description("IBM EBCDIC (Spain)")]
            IBM284 = 20284,

            /// <summary>IBM EBCDIC (UK)</summary>
            [Description("IBM EBCDIC (UK)")]
            IBM285 = 20285,

            /// <summary>IBM EBCDIC (Japanese katakana)</summary>
            [Description("IBM EBCDIC (Japanese katakana)")]
            IBM290 = 20290,

            /// <summary>IBM EBCDIC (France)</summary>
            [Description("IBM EBCDIC (France)")]
            IBM297 = 20297,

            /// <summary>IBM EBCDIC (Arabic)</summary>
            [Description("IBM EBCDIC (Arabic)")]
            IBM420 = 20420,

            /// <summary>IBM EBCDIC (Greek)</summary>
            [Description("IBM EBCDIC (Greek)")]
            IBM423 = 20423,

            /// <summary>IBM EBCDIC (Hebrew)</summary>
            [Description("IBM EBCDIC (Hebrew)")]
            IBM424 = 20424,

            /// <summary>IBM EBCDIC (Korean Extended)</summary>
            [Description("IBM EBCDIC (Korean Extended)")]
            XEBCDICKoreanExtended = 20833,

            /// <summary>IBM EBCDIC (Thai)</summary>
            [Description("IBM EBCDIC (Thai)")]
            IBMThai = 20838,

            /// <summary>Cyrillic (KOI8-R)</summary>
            [Description("Cyrillic (KOI8-R)")]
            Koi8r = 20866,

            /// <summary>IBM EBCDIC (Icelandic)</summary>
            [Description("IBM EBCDIC (Icelandic)")]
            IBM871 = 20871,

            /// <summary>IBM EBCDIC (Cyrillic Russian)</summary>
            [Description("IBM EBCDIC (Cyrillic Russian)")]
            IBM880 = 20880,

            /// <summary>IBM EBCDIC (Turkish)</summary>
            [Description("IBM EBCDIC (Turkish)")]
            IBM905 = 20905,

            /// <summary>IBM Latin-1</summary>
            [Description("IBM Latin-1")]
            IBM00924 = 20924,

            /// <summary>Japanese (JIS 0208-1990 and 0212-1990)</summary>
            [Description("Japanese (JIS 0208-1990 and 0212-1990)")]
            EUCJP_1990 = 20932,

            /// <summary>Chinese Simplified (GB2312-80)</summary>
            [Description("Chinese Simplified (GB2312-80)")]
            Xcp20936 = 20936,

            /// <summary>Korean Wansung</summary>
            [Description("Korean Wansung")]
            Xcp20949 = 20949,

            /// <summary>IBM EBCDIC (Cyrillic Serbian-Bulgarian)</summary>
            [Description("IBM EBCDIC (Cyrillic Serbian-Bulgarian)")]
            Cp1025 = 21025,

            /// <summary>Cyrillic (KOI8-U)</summary>
            [Description("Cyrillic (KOI8-U)")]
            Koi8u = 21866,

            /// <summary>Western European (ISO)</summary>
            [Description("Western European (ISO)")]
            Iso88591 = 28591,

            /// <summary>Central European (ISO)</summary>
            [Description("Central European (ISO)")]
            Iso88592 = 28592,

            /// <summary>Latin 3 (ISO)</summary>
            [Description("Latin 3 (ISO)")]
            Iso88593 = 28593,

            /// <summary>Baltic (ISO)</summary>
            [Description("Baltic (ISO)")]
            Iso88594 = 28594,

            /// <summary>Cyrillic (ISO)</summary>
            [Description("Cyrillic (ISO)")]
            Iso88595 = 28595,

            /// <summary>Arabic (ISO)</summary>
            [Description("Arabic (ISO)")]
            Iso88596 = 28596,

            /// <summary>Greek (ISO)</summary>
            [Description("Greek (ISO)")]
            Iso88597 = 28597,

            /// <summary>Hebrew (ISO-Visual)</summary>
            [Description("Hebrew (ISO-Visual)")]
            Iso88598 = 28598,

            /// <summary>Turkish (ISO)</summary>
            [Description("Turkish (ISO)")]
            Iso88599 = 28599,

            /// <summary>Estonian (ISO)</summary>
            [Description("Estonian (ISO)")]
            Iso885913 = 28603,

            /// <summary>Latin 9 (ISO)</summary>
            [Description("Latin 9 (ISO)")]
            Iso885915 = 28605,

            /// <summary>Europa</summary>
            [Description("Europa")]
            XEuropa = 29001,

            /// <summary>Hebrew (ISO-Logical)</summary>
            [Description("Hebrew (ISO-Logical)")]
            Iso88598i = 38598,

            /// <summary>Japanese (JIS)</summary>
            [Description("Japanese (JIS)")]
            Iso2022jp = 50220,

            /// <summary>Japanese (JIS-Allow 1 byte Kana)</summary>
            [Description("Japanese (JIS-Allow 1 byte Kana)")]
            CsISO2022JP = 50221,

            /// <summary>Japanese (JIS-Allow 1 byte Kana - SO/SI)</summary>
            [Description("Japanese (JIS-Allow 1 byte Kana - SO/SI)")]
            Iso2022jpOneByte = 50222,

            /// <summary>Korean (ISO)</summary>
            [Description("Korean (ISO)")]
            Iso2022kr = 50225,

            /// <summary>Chinese Simplified (ISO-2022)</summary>
            [Description("Chinese Simplified (ISO-2022)")]
            Xcp50227 = 50227,

            /// <summary>Japanese (EUC)</summary>
            [Description("Japanese (EUC)")]
            Eucjp = 51932,

            /// <summary>Chinese Simplified (EUC)</summary>
            [Description("Chinese Simplified (EUC)")]
            EUCCN = 51936,

            /// <summary>Korean (EUC)</summary>
            [Description("Korean (EUC)")]
            Euckr = 51949,

            /// <summary>Chinese Simplified (HZ)</summary>
            [Description("Chinese Simplified (HZ)")]
            Hzgb2312 = 52936,

            /// <summary>Chinese Simplified (GB18030)</summary>
            [Description("Chinese Simplified (GB18030)")]
            GB18030 = 54936,

            /// <summary>ISCII Devanagari</summary>
            [Description("ISCII Devanagari")]
            Xisciide = 57002,

            /// <summary>ISCII Bengali</summary>
            [Description("ISCII Bengali")]
            Xisciibe = 57003,

            /// <summary>ISCII Tamil</summary>
            [Description("ISCII Tamil")]
            Xisciita = 57004,

            /// <summary>ISCII Telugu</summary>
            [Description("ISCII Telugu")]
            Xisciite = 57005,

            /// <summary>ISCII Assamese</summary>
            [Description("ISCII Assamese")]
            Xisciias = 57006,

            /// <summary>ISCII Oriya</summary>
            [Description("ISCII Oriya")]
            Xisciior = 57007,

            /// <summary>ISCII Kannada</summary>
            [Description("ISCII Kannada")]
            Xisciika = 57008,

            /// <summary>ISCII Malayalam</summary>
            [Description("ISCII Malayalam")]
            Xisciima = 57009,

            /// <summary>ISCII Gujarati</summary>
            [Description("ISCII Gujarati")]
            Xisciigu = 57010,

            /// <summary>ISCII Punjabi</summary>
            [Description("ISCII Punjabi")]
            Xisciipa = 57011,

            /// <summary>Unicode (UTF-7)</summary>
            [Description("Unicode (UTF-7)")]
            Utf7 = 65000,

            /// <summary>Unicode (UTF-8)</summary>
            [Description("Unicode (UTF-8)")]
            Utf8 = 65001
        }

        // ReSharper restore InconsistentNaming
    }
}