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
            Executor e = new Executor(ConfigFile);
            e.Execute();
            return true;
        }
    }
}

