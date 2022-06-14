using System;
using System.Net.Http.Headers;
using System.Text.Json;
using Wckdrzr.AutomaticBuildNumber.IO;

namespace Wckdrzr.AutomaticBuildNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            string configFile = args.Length > 0 ? args[0] : null;
            
            configFile = "../../../auto_build_number_config.json"; //test file
            Console.WriteLine("Runnning...");

            if (!string.IsNullOrWhiteSpace(configFile))
            {
                Executor e = new Executor(configFile);
                e.Execute();
                Console.WriteLine("Done");


            }
        }
    }
}

