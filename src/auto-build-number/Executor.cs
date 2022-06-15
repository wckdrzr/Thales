using System;
using System.IO;
using System.Text.Json;
using Wckdrzr.AutomaticBuildNumber.Config;
using Wckdrzr.AutomaticBuildNumber.IO;

namespace Wckdrzr.AutomaticBuildNumber
{
	public class Executor
	{
		private FileServices fs;
		public PropertyGroup _loadedConfig;
		private bool _setMajor;
		private bool _setMinor;

        public Executor()
		{
			fs = new FileServices();
			_loadedConfig = fs.config;

			_setMajor = Convert.ToBoolean(Environment.GetEnvironmentVariable($"{_loadedConfig.EnvironmentVariable_Prefix}_Major"));
			_setMinor = Convert.ToBoolean(Environment.GetEnvironmentVariable($"{_loadedConfig.EnvironmentVariable_Prefix}_Minor"));
		}

		public void Execute()
        {
			//Calculate Build Numbers
			CalculateBuildVersion();

            //Write VersionInfo	
            fs.WriteAssemblyInfoFile();

            //Write Controller if requested
            if (_loadedConfig.AddVersionController)
                fs.WriteVersionRoute();

            //Save Config Data bacl to CSPROJ
            fs.WriteAppConfig();
		}

        private void CalculateBuildVersion()
        {
			//SeedTime
            DateTime InitialDate = new DateTime(2019, 11, 19);

            int BuildVersion;

            //Calculate the number of days since SeedTime
            BuildVersion = (int)(DateTime.UtcNow - InitialDate).TotalDays;

            //Add the number of minutes since midnight lastnight
            double SecondsSinceMidnight = (DateTime.Now - DateTime.Today).TotalSeconds / 10;

            BuildVersion += (int)SecondsSinceMidnight;

			_loadedConfig.BuildNumber = BuildVersion;
			
			if (_setMinor)
            {
				_loadedConfig.MinorVersion++;
			}

			if (_setMajor)
			{
				_loadedConfig.MajorVersion++;
			}
        }

		public void GetVersion()
        {
			string version = _loadedConfig.VersionNumber();
			Console.WriteLine("Current Application Version is: " + version);
		}
	}
}

