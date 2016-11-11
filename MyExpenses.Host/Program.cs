using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Host
{
    class Program
    {
        static string baseAddress = "http://localhost:9000/";

        static void Main(string[] args)
        {
            using (var host = WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("API running...");

                Console.ReadKey(true);
            }
        }
    }
}
