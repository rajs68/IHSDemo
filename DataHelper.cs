using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHSDemo
{
    public class DataHelper
    {
        public void Start()
        {
            Logger.WriteLine("Start query entity keys....");
            var keys = IhsHelper.Instance.GetEntities(Config.IhsStateCode, Config.IhsCountyCode);
            Logger.WriteLine("Query success, total=" + keys.Length);

            int page = 0, index = 0, capacity = 10;

            while (index < keys.Length)
            {
                var c = keys.Length - index;
                if (c > capacity) c = capacity;

                var ids = new string[c];
                Array.Copy(keys, index, ids, 0, c);

                LoadData(ids, index, c);

                page++; index = page * capacity;
            }

            Logger.WriteLine("---- End ----");
        }

        void LoadData(string[] keys, int index, int count)
        {
            if (null == keys || keys.Length == 0) return;

            Logger.WriteLine();

            var s1 = Environment.NewLine + string.Join(Environment.NewLine, keys);
            Logger.WriteLine("Start load data " + "[" + (index + 1) + " + " + count + "]:" + s1);

            try
            {
                var data = IhsHelper.Instance.LoadDataToDataSet(keys);
                Logger.WriteLine("Load data complate");
                var dtPrdHeader = data.Tables[IhsHelper.Settings.SheetProductionHeader];
                var dtMonthlyPrd = data.Tables[IhsHelper.Settings.SheetMonthlyProduction];

                SavePrdHeader(dtPrdHeader);
                SaveMothlyPrd(dtMonthlyPrd);
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Error:" + ex.Message);
            }
        }

        void SaveMothlyPrd(DataTable table)
        {
            Logger.WriteLine("Start save Monthly Production...");
            var sql = @"INSERT INTO [dbo].[MonthlyProduction]
           ([Entity]
           ,[Source]
           ,[EntityType]
           ,[PrimaryProduct]
           ,[LeaseName]
           ,[WellNumber]
           ,[API]
           ,[RegulatoryAPI]
           ,[Year]
           ,[Month]
           ,[Liquid]
           ,[Gas]
           ,[Water]
           ,[RatioGasOil]
           ,[PercentWater]
           ,[Wells]
           ,[DaysOn])
     VALUES
           (@p0
           ,@p1
           ,@p2
           ,@p3
           ,@p4
           ,@p5
           ,@p6
           ,@p7
           ,@p8
           ,@p9
           ,@p10
           ,@p11
           ,@p12
           ,@p13
           ,@p14
           ,@p15
           ,@p16)";

            foreach (DataRow row in table.Rows)
            {
                var pc = 17;
                var pp = new SqlParameter[pc];
                for (int i = 0; i < pc; i++)
                {
                    pp[i] = new SqlParameter("@p" + i, row[i]);                    
                }

                SqlHelper.ExecuteNonQuery(Config.DbConnStr, CommandType.Text, sql, pp);
            }
            Logger.WriteLine("Save Monthly Production completed.");
        }

        void SavePrdHeader(DataTable table)
        {
            Logger.WriteLine("Start save Production Header..");
            var sql = @"INSERT INTO [dbo].[ProductionHeader]
           ([Entity]
           ,[Source]
           ,[EntityType]
           ,[PrimaryProduct]
           ,[ProvinceStateName]
           ,[DistrictName]
           ,[CountyName]
           ,[OSIndicator]
           ,[BasinName]
           ,[OperatorName]
           ,[FieldName]
           ,[ProdZoneName]
           ,[LeaseName]
           ,[LeaseNumber]
           ,[WellNumber]
           ,[Location]
           ,[GathererGas]
           ,[GathererLiquid]
           ,[StatusDate]
           ,[StatusCurrentName]
           ,[DateProductionStart]
           ,[DateProductionStop]
           ,[DateInjectionStart]
           ,[DateInjectionStop]
           ,[PoolName]
           ,[TemperatureGradient]
           ,[NFactor])
     VALUES
           (@p0,@p1
           ,@p2
           ,@p3
           ,@p4
           ,@p5
           ,@p6
           ,@p7
           ,@p8
           ,@p9
           ,@p10
           ,@p11
           ,@p12
           ,@p13
           ,@p14
           ,@p15
           ,@p16
           ,@p17
           ,@p18
           ,@p19
           ,@p20
           ,@p21
           ,@p22
           ,@p23
           ,@p24
           ,@p25
           ,@p26)";

            foreach (DataRow row in table.Rows)
            {
                var pc = 27;
                var pp = new SqlParameter[pc];
                for (int i = 0; i < pc; i++)
                {
                    pp[i] = new SqlParameter("@p" + i, row[i]);
                }

                SqlHelper.ExecuteNonQuery(Config.DbConnStr, CommandType.Text, sql, pp);
            }
            Logger.WriteLine("Save Production Header completed.");
        }
    }
}
