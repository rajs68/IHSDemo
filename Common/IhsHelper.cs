using IHSEnergy.Enerdeq.ExportBuilder;
using IHSEnergy.Enerdeq.QueryBuilder;
using IHSEnergy.Enerdeq.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHSDemo
{
    class IhsHelper
    {
        public static IhsHelper Instance = null;

        static IhsHelper()
        {
            Instance = new IhsHelper();
        }

        private IhsHelper() { }

        WebServicesSession m_session = null;
        public WebServicesSession Session
        {
            get
            {
                return m_session ?? (m_session = WebServicesSession.Create(Config.IhsUser, Config.IhsPwd, Config.AppName));
            }
        }

        public string[] GetEntities(string state_code, string county_code)
        {
            var qb = new QueryBuilder(Session);
            var q = BuildQueryXml(state_code, county_code);
            
            var keys = qb.GetKeys(q);
            if (null == keys) keys = new string[0];


            return keys;
        }

        public DataSet LoadDataToDataSet(string[] ids)
        {
            DataSet ds = null;
            var data = GetExcelData(ids);

            using(var ms = new System.IO.MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                var t1 = Settings.SheetProductionHeader;
                var t2 = Settings.SheetMonthlyProduction;
                ds = ExcelUtil.ExcelToDataSet(ms, 1, t1, t2);
            }

            data = null;
            return ds;
        }

        byte[] GetExcelData(params string[] ids)
        {
            if (null == ids || ids.Length == 0) return null;

            var eb = new ExportBuilder(this.Session);
            var domain = Settings.Domain;
            var dateType = Settings.DataType;
            var template = Settings.Template;

            var jobId = eb.Build(domain, dateType, template, ids, "ihs_isa_01", Overwrite.True);

            if (null != jobId && !string.IsNullOrEmpty(jobId))
            {
                System.Threading.Thread.Sleep(500);
                while (!eb.IsComplete(jobId))
                {
                    System.Threading.Thread.Sleep(1000);
                }

                var data = eb.Retrieve(jobId, true);
                data = ZipToExcel(data);
                return data;
            }

            return null;
        }

        static byte[] ZipToExcel(byte[] data)
        {
            using(var ms = new System.IO.MemoryStream(data))
            {
                var zip = new ICSharpCode.SharpZipLib.Zip.FastZip();
                var path = System.IO.Path.Combine(Config.AppPath , "zip");
                if(!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                var ow = ICSharpCode.SharpZipLib.Zip.FastZip.Overwrite.Always;
                zip.ExtractZip(ms, path, ow, null, null, null, false, false);
   
                var filePath = System.IO.Directory.GetFiles(path)[0];
                var buffer = System.IO.File.ReadAllBytes(filePath);
                return buffer;
            }
        }

        static void SaveFile(byte[] data)
        {
            ZipToExcel(data);
            //System.IO.File.WriteAllBytes("c:\\zzz\\cc.zip", data);
        }

        static string BuildQueryXml(string state_code, string county_code)
        {
            var xml = "<criterias>"
                       + "<criteria type='group' groupId=\"\" ignored='false'>"
                       + "  <domain>US</domain>"
                         + "<datatype>Production Allocated</datatype>"
                         + "<attribute_group>Location</attribute_group>"
                      + "   <attribute>State/County</attribute>"
                        + " <filter logic='include'>"
                          + " <value id='0'>"
                            + "<group_actual>"
                              + " <operator logic=\"and\">"
                                + " <condition logic=\"equals\">"
                                  + " <attribute>state_code</attribute>"
                                   + "<value_list>"
                                    + " <value>" + state_code + "</value>"
                                  + " </value_list>"
                                 + "</condition>"
                                 + "<condition logic=\"equals\">"
                                  + " <attribute>county_code</attribute>"
                                  + " <value_list>"
                                    + " <value>" + county_code + "</value>"
                                  + " </value_list>"
                                 + "</condition>"
                              + " </operator>"
                             + "</group_actual>"
                             + "<group_display>name = TX LYNN</group_display>"
                          + " </value>"
                        + " </filter>"
                      + " </criteria>"
                      + "</criterias>";
            return xml;
        }

        public class Settings
        {
            public const string Domain = "US";

            public const string DataType = "Production Allocated";

            public const string Template = "Excel Production Workbook (Excel 2007, 2010)";

            public const string SheetProductionHeader = "Production Header";

            public const string SheetMonthlyProduction = "Monthly Production";

            /*
                [0]: "298 Production (comma delimited)"
              [1]: "298 Summary Production (comma delimited)"
              [2]: "298 Production (fixed field)"
              [3]: "298 Summary Production (fixed field)"
              [4]: "DMP2 Production"
              [5]: "DMP2 Summary Production"
              [6]: "EnerdeqML Production"
              [7]: "Excel Production Workbook (Excel 2002, 2003, 2007)"
              [8]: "Excel Production Workbook (Excel 2007, 2010)"
              [9]: "PowerTools Production Export"
              [10]: "PowerTools Summary Production Export"
              [11]: "Production Header"
              [12]: "Production ID List"
              [13]: "297 Well (fixed field)"
              [14]: "297 Well (comma delimited)"
              [15]: "EnerdeqML Well"
              [16]: "Excel Well Workbook (Excel 2002, 2003, 2007)"
              [17]: "Excel Well Workbook (Excel 2007, 2010)"
              [18]: "Well Header"
              [19]: "Well ID List"
           */
        }
    }
}
