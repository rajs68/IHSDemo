using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IHSDemo
{
    public static class Config
    {
        static Config()
        {
            IhsStateCode = GetAppSettings("ihs_state", IhsStateCode);
            IhsCountyCode = GetAppSettings("ihs_county", IhsCountyCode);
            IhsUser = GetAppSettings("ihs_user", IhsUser);
            IhsPwd = GetAppSettings("ihs_pwd", IhsPwd);
            AppName = GetAppSettings("ihs_app_name", AppName);

            DbConnStr = GetConnStr("DB_IHS");

            AppPath = Environment.CurrentDirectory;
        }

        public static readonly string AppPath = "";

        public static readonly string DbConnStr = null;

        public static readonly string IhsUser = "jbirmingham@3roc.com";

        public static readonly string IhsPwd = "J7gt5635";

        public static readonly string AppName = "IHS-ISA-01";

        public static readonly string IhsStateCode = "42";
        public static readonly string IhsCountyCode = "305";

        /// <summary>
        /// 获取Web.Config中的 AppSettings
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="defaultValue">找不到时返回的默认值</param>
        /// <returns></returns>
        public static string GetAppSettings(string key, string defaultValue = null)
        {
            var v = ConfigurationManager.AppSettings[key];
            return v ?? defaultValue;
        }

        /// <summary>
        /// 数据库链接串
        /// </summary>
        /// <returns></returns>
        private static string GetConnStr(string connName)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
                var sb = new System.Data.SqlClient.SqlConnectionStringBuilder(conn);
                sb.MultipleActiveResultSets = true;
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("获取数据库连接字符串出错。详细信息：" + ex.Message);
            }
        }
    }
}