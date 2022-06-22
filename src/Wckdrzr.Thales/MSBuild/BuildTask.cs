using System;
using Microsoft.Build.Utilities;
namespace Wckdrzr.AutomaticVersionUpdate.MSBuild
{
    public class OnBuild : Task
    {
        public override bool Execute()
        {
            Console.Write("Thales Automatic Version Number Package running...");
            try
            {
                Executor runner = new Executor();
                runner.Execute();
            }
            catch (Exception)
            {
                Console.WriteLine("There was an error running Thales");
                return false;
            }
            
            Console.WriteLine("Version Number update complete");
            return true;
        }
    }
}

