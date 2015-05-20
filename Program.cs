using IHSEnergy.Enerdeq.ExportBuilder;
using IHSEnergy.Enerdeq.QueryBuilder;
using IHSEnergy.Enerdeq.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHSDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var demo = new IhsHelper();
            //demo.Start("42", "305"); // 42=Texas, 305=Lynn
            Console.WriteLine("state_code=" + Config.IhsStateCode + ", county_code=" + Config.IhsCountyCode);
            Console.WriteLine("Press Enter Key to start...");
            Console.ReadLine();

            var dh = new DataHelper();
            dh.Start();

            Console.WriteLine();
            Console.WriteLine("---- success, press Enter key to exit ----");
            Console.ReadLine();
        }
    }
}
