using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace IHSDemo
{
    public static class StringUtil
    {
        /// <summary>
        /// 消息模板中替换参数前缀: {#
        /// </summary>
        public const string MSG_PRRAM_PREFIX = "{#";
        /// <summary>
        /// 消息模板中替换参数后缀: #}
        /// </summary>
        public const string MSG_PRRAM_SUFFIX = "#}";

        /// <summary>
        /// 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// </summary>
        public const string ReadyChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }
        /// </summary>
        public readonly static char[] ReadyNumbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        /// <summary>
        /// { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' }
        /// </summary>
        public readonly static char[] ReadyHexChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        /// <summary>
        /// { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'I', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }
        /// </summary>
        public readonly static char[] ReadyAlphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'I', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        /// <summary>
        /// { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' };
        /// </summary>
        public readonly static char[] ReadyNonAlphabet = new char[] { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' }; // [`] 和 [~] 共用一个键

        static object lock_order_no = new object();

        /// <summary>
        /// 生成唯一单号
        /// </summary>
        /// <returns></returns>
        public static string GenOrderNo()
        {
            lock (lock_order_no)
            {
                System.Threading.Thread.Sleep(1);
                return DateTime.Now.ToString("yyMMddHHmmssfff");
            }
        }

        /// <summary>
        /// 生成16进制字符的随机字符串
        /// </summary>
        /// <param name="length">要生成的字符串长度[1 - 0x80]</param>
        /// <returns></returns>
        public static string GenRandomHexString(int length)
        {
            if ((length < 1) || (length > 0x80))
            {
                throw new ArgumentException("要生成的随机字符串长度必须在[1-0x80]范围内");
            }
            byte[] data = new byte[length];
            char[] chArray = new char[length];
            using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(data); }
            var residue = ReadyHexChars.Length;
            for (int i = 0; i < length; i++)
            {
                int num1 = data[i] % residue;
                chArray[i] = ReadyHexChars[num1];
            }
            var str = new string(chArray);
            return str;
        }

        /// <summary>
        /// 生成随机字符串，长度为[1 - 0x80]
        /// </summary>
        /// <param name="length">长度 [1-0x80]</param>
        /// <param name="alphaLen">可能包含字母的数量[0 - length]</param>
        /// <param name="nonAlphaLen">可能包含的特殊字符数量[0 - length]</param>
        /// <returns></returns>
        public static string GenRandomString(int length, int numberOfAlpha = 0, int numberOfNonAlpha = 0)
        {
            if ((length < 1) || (length > 0x80))
            {
                throw new ArgumentException("要生成的随机字符串长度必须在[1-0x80]范围内");
            }
            if ((numberOfAlpha > length) || (numberOfAlpha < 0))
            {
                throw new ArgumentException("required_alphanumeric_characters_incorrect");
            }
            if ((numberOfNonAlpha > length) || (numberOfNonAlpha < 0))
            {
                throw new ArgumentException("required_non_alphanumeric_characters_incorrect");
            }
            byte[] data = new byte[length + numberOfAlpha + numberOfNonAlpha];
            char[] chArray = new char[length];
            using (var rng = new RNGCryptoServiceProvider()) { rng.GetBytes(data); }
            for (int i = 0; i < length; i++)
            {
                int num1 = data[i] % 10;
                chArray[i] = ReadyNumbers[num1];
            }
            if (numberOfAlpha > 0)
            {
                for (int j = 0; j < numberOfAlpha; j++)
                {
                    int num2 = length + j;
                    var num3 = data[num2] % ReadyAlphabet.Length;
                    var num4 = data[num2] % length;
                    chArray[num4] = ReadyAlphabet[num3];
                }
            }
            if (numberOfNonAlpha > 0)
            {
                for (int k = 0; k < numberOfNonAlpha; k++)
                {
                    int num5 = length + numberOfAlpha + k;
                    var num6 = data[num5] % ReadyNonAlphabet.Length;
                    var num7 = data[num5] % length;
                    chArray[num7] = ReadyNonAlphabet[num6];
                }
            }
            var str = new string(chArray);
            return str;
        }

        /// <summary>
        /// 生成唯一的字符串
        /// </summary>
        /// <returns>唯一的字符串</returns>
        public static string GenUniqueString(Guid? seed = null)
        {
            string readyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string rtn = "";
            var guid = (seed.HasValue ? seed.Value : Guid.NewGuid()).ToByteArray();
            for (int i = 0; i < 8; i++)
            {
                rtn += readyStr[BitConverter.ToUInt16(guid, i * 2) % 35];
            }
            //return new Regex(@"(^\d)", RegexOptions.IgnoreCase).IsMatch(rtn) ? GenUniqueString() : rtn;
            return rtn;
        }

        /// <summary>
        /// 将邮件中的 {#parameter#} 替换，并返回替换结果 (注意：模板中不要有单独的 {# 或 #})
        /// </summary>
        /// <param name="template">模板内容</param>
        /// <param name="paramList">参数列表，Key中不需要加前、后缀{##} [如：{#name#}, Key=name, value=....]</param>
        /// <returns></returns>
        public static string BuildMailText(string template, Hashtable paramList)
        {
            var array = template.Split(new string[] { MSG_PRRAM_PREFIX, MSG_PRRAM_SUFFIX }, StringSplitOptions.None);
            var sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sb.Append(array[i]);
                }
                else
                {
                    string tmp = array[i];
                    if (paramList.ContainsKey(tmp))
                    {
                        sb.Append(paramList[tmp]);
                    }
                    else
                    {
                        sb.Append(MSG_PRRAM_PREFIX + tmp + MSG_PRRAM_SUFFIX); // 没找到替换内容
                    }
                }
            }

            return sb.ToString();
        }

        public static TValue As<TValue>(this string value)
        {
            return value.As<TValue>(default(TValue));
        }

        public static TValue As<TValue>(this string value, TValue defaultValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value)) return defaultValue;

                TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    return (TValue)converter.ConvertFrom(value);
                }
                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(typeof(TValue)))
                {
                    return (TValue)converter.ConvertTo(value, typeof(TValue));
                }
            }
            catch (Exception)
            {
            }
            return defaultValue;
        }

        public static Guid AsGuid(this string value)
        {
            Guid guid; if (Guid.TryParse(value, out guid)) return guid;
            return Guid.Empty;
        }

        public static Guid? AsNullableGuid(this string value)
        {
            Guid guid; if (Guid.TryParse(value, out guid)) return guid; return null;
        }

        public static string AsTrim(this string value)
        {
            return string.Concat(value).Trim();
        }

        public static string AsLower(this string value, bool withTrim = true)
        {
            return withTrim ? (value + "").Trim().ToLower() : (value + "").ToLower();
        }

        public static string AsUpper(this string value, bool withTrim = true)
        {
            return withTrim ? (value + "").Trim().ToUpper() : (value + "").ToUpper();
        }

        public static bool AsBool(this string value, bool defaultValue = false)
        {
            return value.As<bool>(defaultValue);
        }

        public static bool AsBoolEx(this string value, string equalTo)
        {
            return string.Equals(value, equalTo, StringComparison.OrdinalIgnoreCase);
        }

        public static DateTime? AsDateTime(this string value)
        {
            DateTime dt;
            if (DateTime.TryParse(value, out dt)) return dt;
            return null;
        }

        public static DateTime AsDateTime(this string value, DateTime defaultValue)
        {
            DateTime dt;
            if (DateTime.TryParse(value, out dt)) return dt;
            return defaultValue;
        }

        public static decimal AsDecimal(this string value)
        {
            return value.AsDecimal(0);
        }

        public static decimal AsDecimal(this string value, decimal defaultValue)
        {
            decimal m = 0; decimal.TryParse(value, out m); return m;
        }

        public static float AsFloat(this string value)
        {
            return value.AsFloat(0);
        }

        public static float AsFloat(this string value, float defaultValue)
        {
            float m = 0; float.TryParse(value, out m); return m;
        }

        public static int AsInt(this string value)
        {
            return value.AsInt(0);
        }

        public static int AsInt(this string value, int defaultValue)
        {
            int m = 0; int.TryParse(value, out m); return m;
        }

        public static bool Is<TValue>(this string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
            return (((converter != null) && converter.CanConvertFrom(typeof(string))) && converter.IsValid(value));
        }

        public static bool IsBool(this string value)
        {
            return value.Is<bool>();
        }

        public static bool IsDateTime(this string value)
        {
            return value.Is<DateTime>();
        }

        public static bool IsDecimal(this string value)
        {
            return value.Is<decimal>();
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsFloat(this string value)
        {
            return value.Is<float>();
        }

        public static bool IsInt(this string value)
        {
            return value.Is<int>();
        }
    }
}
