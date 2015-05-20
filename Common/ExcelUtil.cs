using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace IHSDemo
{
    /// <summary>
    /// Excel 数据导出、导入
    /// </summary>
    public class ExcelUtil
    {
        /// <summary>
        /// 1.EXCEL 第一行为列名。
        /// 2.EXCEL 第一行不能为空，如果为空将自动 转换为（F1.F2.F3等）
        /// 3.EXCEL 第一行内容不能为纯数字.如果为纯数字将自动 转换为（F1.F2.F3等）.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string filePath)
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
            using (OleDbConnection oleConn = new OleDbConnection(strConn))
            {
                try
                {
                    oleConn.Open();

                    DataSet oleDsExcle = new DataSet();

                    System.Data.DataTable dt = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            String sql = "SELECT * FROM  [" + row["TABLE_NAME"].ToString() + "]";
                            using (OleDbDataAdapter oleDaExcel = new OleDbDataAdapter(sql, oleConn))
                            {
                                DataSet tempDs = new DataSet();
                                oleDaExcel.Fill(tempDs, row["TABLE_NAME"].ToString());
                                oleDsExcle.Tables.Add(tempDs.Tables[0].Copy());
                            }
                        }
                        catch { }
                    }
                    return oleDsExcle;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static DataSet ExcelToDataSet(string filePath, string tableName)
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
            using (OleDbConnection oleConn = new OleDbConnection(strConn))
            {
                try
                {
                    oleConn.Open();
                    DataSet oleDsExcle = new DataSet();
                    System.Data.DataTable dt = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    String sql = "SELECT * FROM  [" + tableName + "$]";
                    using (OleDbDataAdapter oleDaExcel = new OleDbDataAdapter(sql, oleConn))
                    {
                        DataSet tempDs = new DataSet();
                        oleDaExcel.Fill(tempDs, tableName);
                        oleDsExcle.Tables.Add(tempDs.Tables[0].Copy());
                    }
                    return oleDsExcle;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static Stream ExportInvActionToExcelStream(DataTable mainData, DataTable questionData, DataTable userData, bool printHeaders = true, string cellBegin = "A1")
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                // 创建 Sheet
                ExcelWorksheet ws1 = pck.Workbook.Worksheets.Add("基本信息");
                // 加载 DataTable，允许有标题头
                if (string.IsNullOrEmpty(cellBegin))
                {
                    cellBegin = "A1";
                }

                if (printHeaders)
                {
                    for (int i = 0; i < mainData.Columns.Count; i++)
                    {
                        ExcelRange rgl = ws1.Cells[1, i + 1];
                        rgl.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rgl.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rgl.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rgl.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rgl.Style.Font.Color.SetColor(Color.FromArgb(79, 110, 154));
                        rgl.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(232, 242, 255));
                    }
                }

                ws1.Cells[cellBegin].LoadFromDataTable(mainData, printHeaders);

                // 创建 Sheet
                ExcelWorksheet ws2 = pck.Workbook.Worksheets.Add("调查问卷");
                // 加载 DataTable，允许有标题头
                if (string.IsNullOrEmpty(cellBegin))
                {
                    cellBegin = "A1";
                }

                if (printHeaders)
                {
                    for (int i = 0; i < questionData.Columns.Count; i++)
                    {
                        ExcelRange rgl = ws2.Cells[1, i + 1];
                        rgl.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rgl.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rgl.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rgl.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rgl.Style.Font.Color.SetColor(Color.FromArgb(79, 110, 154));
                        rgl.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(232, 242, 255));
                    }
                }

                ws2.Cells[cellBegin].LoadFromDataTable(questionData, printHeaders);

                // 创建 Sheet
                ExcelWorksheet ws3 = pck.Workbook.Worksheets.Add("调查对象");
                // 加载 DataTable，允许有标题头
                if (string.IsNullOrEmpty(cellBegin))
                {
                    cellBegin = "A1";
                }

                if (printHeaders)
                {
                    for (int i = 0; i < userData.Columns.Count; i++)
                    {
                        ExcelRange rgl = ws3.Cells[1, i + 1];
                        rgl.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rgl.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rgl.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rgl.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rgl.Style.Font.Color.SetColor(Color.FromArgb(79, 110, 154));
                        rgl.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(232, 242, 255));
                    }
                }

                ws3.Cells[cellBegin].LoadFromDataTable(userData, printHeaders);

                MemoryStream stream = new MemoryStream();
                pck.SaveAs(stream);
                return stream;
            }
        }

        public static DataSet ExcelToDataSet(Stream excelStream, int headRow, params string[] tables)
        {
            var ds = new DataSet();

            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Load(excelStream);
                foreach (var sheet in excel.Workbook.Worksheets)
                {
                    if(null != tables && tables.Length >0)
                    {
                        if (!tables.Contains(sheet.Name)) continue;
                    }
                    var dt = new DataTable(sheet.Name);
                    var cc1 = sheet.Dimension.End.Column;
                    if (cc1 > 31) cc1 = 31;

                    for (int i = 1; i <= cc1; i++)
                    {
                        var cn1 = sheet.Cells[headRow, i].Value + "";
                        dt.Columns.Add(new DataColumn(cn1));
                    }

                    var rr1 = sheet.Dimension.End.Row;
                    for (int i = headRow + 1; i <= rr1; i++)
                    {
                        var dr = dt.NewRow();
                        for (int j = 0; j < cc1; j++)
                        {
                            var cell = sheet.Cells[i, j + 1];
                            dr[j] = ParseCell(cell);
                        }
                        dt.Rows.Add(dr);
                    }

                    ds.Tables.Add(dt);
                    using (sheet) { }
                }

            }
            return ds;
        }

        static object ParseCell(ExcelRange cell)
        {
            return cell.Text;
        }

        public static DataTable ExcelToDataTable(Stream excelStream, IEnumerable<string> columns, int skipRow = 1)
        {
            DataTable table = new DataTable();

            using (ExcelPackage excel = new ExcelPackage(excelStream))
            {
                ExcelWorksheet sheet = excel.Workbook.Worksheets[1];

                int colCount = sheet.Dimension.End.Column >= columns.Count()
                                   ? columns.Count()
                                   : sheet.Dimension.End.Column;
                int rowCount = sheet.Dimension.End.Row;

                foreach (string column in columns)
                {
                    table.Columns.Add(new DataColumn(column));
                }

                for (int i = skipRow + 1; i <= rowCount; i++)
                {
                    DataRow row = table.NewRow();
                    for (int j = 1; j <= colCount; j++)
                    {
                        row[j - 1] = sheet.Cells[i, j].Value;
                    }
                    table.Rows.Add(row);
                }
            }

            return table;
        }

        /// <summary>
        /// 导出datatable至excel(memory)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="printHeaders"></param>
        /// <param name="cellBegin"></param>
        /// <returns></returns>
        public static Stream ExportDataTableToExcelStream(DataTable data, bool printHeaders = true, string cellBegin = "A1")
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                // 创建 Sheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                // 加载 DataTable，允许有标题头
                if (string.IsNullOrEmpty(cellBegin))
                {
                    cellBegin = "A1";
                }

                if (printHeaders)
                {
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        ExcelRange rgl = ws.Cells[1, i + 1];
                        rgl.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rgl.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rgl.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rgl.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rgl.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rgl.Style.Font.Color.SetColor(Color.FromArgb(79, 110, 154));
                        rgl.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(232, 242, 255));
                    }
                }

                ws.Cells[cellBegin].LoadFromDataTable(data, printHeaders);
                MemoryStream stream = new MemoryStream();
                pck.SaveAs(stream);
                return stream;
            }
        }
    }
}