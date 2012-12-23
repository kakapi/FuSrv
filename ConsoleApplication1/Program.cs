using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(System.Configuration.ConfigurationManager.AppSettings["key1"]);
            Console.Write(System.Configuration.ConfigurationSettings.AppSettings["key1"]);
            Console.Read();
        }
    }
}
