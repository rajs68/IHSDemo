using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHSDemo
{
    static class Logger
    {
        public static void Write(string log)
        {
            Console.Write(log);
        }

        public static void WriteLine(string text, params object[] args)
        {
            string log = text;
            if(null != args && args.Length > 0)
            {
                log = string.Format(text, args);
            }
            Console.WriteLine("[" + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "] " + log);
        }
    }
}
