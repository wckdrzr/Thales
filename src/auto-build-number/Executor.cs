using System;
using System.IO;
using System.Text.Json;
using Wckdrzr.AutomaticBuildNumber.Config;
using Wckdrzr.AutomaticBuildNumber.IO;

namespace Wckdrzr.AutomaticBuildNumber
{
	public class Executor
	{
		public config _loadedConfig;
        private string _configFilename;
		private bool _setMajor;
		private bool _setMinor;

        public Executor(string configFilename)
		{
			_configFilename = configFilename;
			_loadedConfig = LoadConfig(configFilename);

			_setMajor = Convert.ToBoolean(Environment.GetEnvironmentVariable($"{_loadedConfig.EnvironmentVariable_Prefix}_Major"));
			_setMinor = Convert.ToBoolean(Environment.GetEnvironmentVariable($"{_loadedConfig.EnvironmentVariable_Prefix}_Minor"));
		}

        private config LoadConfig(string configFilename)
        {
			try
			{
				return JsonSerializer.Deserialize<config>(File.ReadAllText(configFilename));
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine($"Specified Config file provided '{configFilename}' cannot be found!");
			}
			catch (FileLoadException)
			{
				Console.WriteLine($"Specified Config file provided '{configFilename}' cannot be read correctly, is it valid JSON?");
			}
			catch (Exception ex)
            {
				Console.WriteLine($"Unexpected error: {ex.Message}");
            }

			return null;
		}

		public void Execute()
        {
			FileWriter fw = new FileWriter(_configFilename);

			//Calculate Build Numbers
			CalculateBuildVersion();

			//Write VersionInfo	
			fw.WriteAssemblyInfoFile();

			//Write Controller if requested
			if (_loadedConfig.AddVersionController)
				fw.WriteVersionRoute();

			//Save Config File
			fw.WriteConfigUpdate();
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

			_loadedConfig.BuildVersion = BuildVersion;
			
			if (_setMinor)
            {
				_loadedConfig.MinorVersion++;
			}

			if (_setMajor)
			{
				_loadedConfig.MajorVersion++;
			}

			Console.WriteLine($"Build Number : {_loadedConfig.VersionNumber()}");
        }

		public void GetVersion()
        {
			string version = _loadedConfig.VersionNumber();
			Console.WriteLine("Current Application Version is: " + version);
		}
	}
}

