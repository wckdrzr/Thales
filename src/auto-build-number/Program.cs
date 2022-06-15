using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using Wckdrzr.AutomaticBuildNumber.Config;
using Wckdrzr.AutomaticBuildNumber.IO;

namespace Wckdrzr.AutomaticBuildNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Auto Build Number Pacage Runnning...");

            Executor e = new Executor();
            e.Execute();
            Console.WriteLine("Version Update Complete");

        }
    }
}

