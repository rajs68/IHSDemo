using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Net;
using System.ComponentModel;

namespace XL.SCM.Common
{
    /// <summary>
    /// 常用的扩展（未分类）
    /// </summary>
    public static class XExtentions
    {
        #region DAteTime

        public static string XToDateStr(this DateTime dt, bool withTime = true)
        {
            return dt.ToString("yyyy-MM-dd" + (withTime ? " HH:mm" : null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="nullText"></param>
        /// <returns></returns>
        public static string XToDateStr(this DateTime? dt, bool withTime = true, string defaultText = null)
        {
            return dt.HasValue ? dt.Value.ToString("yyyy-MM-dd" + (withTime ? " HH:mm" : null)) : defaultText;
        }

        public static string XToDateStr2(this DateTime? dt, string format = "yyyy-MM-dd HH:mm")
        {
            return dt.HasValue ? dt.Value.ToString(format) : "";
        }

        public static string XToDateStr2(this DateTime dt, string format = "yyyy-MM-dd HH:mm")
        {
            return dt.ToString(format);
        }

        public static string ToShortDisplay(this DateTime dt)
        {
            return dt == DateTime.MinValue ? "" : dt.ToString("yyyy-MM-dd");
        }

        public static string ToDisplay(this DateTime dt)
        {
            return dt == DateTime.MinValue ? "" : dt.ToString("yyyy-MM-dd HH:mm");
        }
        
        #endregion

        public static string ToRoe(this decimal roe)
        {
            return string.Format("{0:0.######}", roe);
        }

        public static string ToRoe(this decimal? roe)
        {
            return roe.HasValue ? string.Format("{0:0.######}", roe) : "";
        }

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }

        /// <summary>
        /// 把哈希表里的数据赋值给 T对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static T XLoadByHt<T>(this Hashtable ht)
        {
            var item = Activator.CreateInstance<T>();

            var type = item.GetType();
            foreach (var p in type.GetProperties())
            {
                if (!p.CanWrite) continue;
                if (p.PropertyType.IsValueType || p.PropertyType == typeof(string)
                    || (p.PropertyType.IsArray && p.PropertyType.HasElementType && p.PropertyType.GetElementType().IsValueType))
                {
                    p.SetValue(item, ht[p.Name], null);
                }
            }

            return item;
        }

        /// <summary>
        /// 获取对象的属性值，返回结果为Hashtable
        /// </summary>
        /// <param name="item">要处理的数据，不允许为NULL</param>
        /// <param name="containArray">是否包含 数组类型的属性，主要是Byte[]（对应数据库中的Image）类型</param>
        /// <returns></returns>
        public static Hashtable XGetPropertyValues(this object item, bool containArray = false)
        {
            if (null == item) throw new ArgumentNullException("item");

            var ht = new Hashtable();
            var type = item.GetType();

            if (type.IsValueType || type == typeof(string))
            {
                // 单值类型
                ht.Add(type.Name, item);
            }
            else if (containArray && type.IsArray)
            {
                if (type.HasElementType && type.GetElementType().IsValueType)
                {
                    ht.Add(type.Name, item);
                }
                else
                {
                    throw new ArgumentException("Not supported type: " + type.FullName);
                }
            }
            else
            {
                var ps = type.GetProperties();
                foreach (var p in ps)
                {
                    if (!p.CanRead) continue;
                    if (p.PropertyType.IsValueType || p.PropertyType == typeof(string)
                        || (containArray && p.PropertyType.IsArray && p.PropertyType.HasElementType && p.PropertyType.GetElementType().IsValueType))
                    {
                        var v = p.GetValue(item, null);
                        ht.Add(p.Name, v);
                    }
                }
            }

            return ht;
        }
    }
}
