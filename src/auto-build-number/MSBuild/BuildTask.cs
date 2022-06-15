using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
namespace Wckdrzr.AutomaticBuildNumber.MSBuild
{
    public class OnBuild : Task
    {
        public string ConfigFile { get; set; }

        public override bool Execute()
        {
            //Entrypoint
            Console.WriteLine("Automatic Version Number Package running...");
            Executor runner = new Executor();
            runner.Execute();
            Console.WriteLine("Version Number update complete");
            return true;
        }
    }
}

