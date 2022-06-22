using System;
using Wckdrzr.AutomaticVersionUpdate.Config;
using Wckdrzr.AutomaticVersionUpdate.IO;

namespace Wckdrzr.AutomaticVersionUpdate
{
	public class Executor
	{
		private readonly FileServices _fs;
		private readonly PropertyGroup _loadedConfig;
		private readonly bool _setMajor;
		private readonly bool _setMinor;
        private readonly bool _setRevision;

        public Executor()
		{
			_fs = new FileServices();
			_loadedConfig = _fs.Config;

			_setMajor = Convert.ToBoolean(Environment.GetEnvironmentVariable($"{_loadedConfig.EnvironmentVariablePrefix}_Major"));
			_setMinor = Convert.ToBoolean(Environment.GetEnvironmentVariable($"{_loadedConfig.EnvironmentVariablePrefix}_Minor"));
            _setRevision = Convert.ToBoolean(Environment.GetEnvironmentVariable($"{_loadedConfig.EnvironmentVariablePrefix}_Revision"));
        }

		public void Execute()
        {
			//Calculate Build Numbers
			CalculateBuildVersion();

            //Write VersionInfo	
            _fs.WriteAssemblyInfoFile();

            //Write Controller if requested
            if (_loadedConfig.AddVersionController)
                _fs.WriteVersionRoute();

            //Save Config Data back to CSPROJ
            _fs.UpdateAppConfig();
		}

        private void CalculateBuildVersion()
        {
			//SeedTime
            DateTime initialDate = new DateTime(2019, 11, 19);

            //Calculate the number of days since SeedTime
            var buildVersion = (int)(DateTime.UtcNow - initialDate).TotalDays;

            //Add the number of minutes since midnight last night
            double secondsSinceMidnight = (DateTime.Now - DateTime.Today).TotalSeconds / 10;

            buildVersion += (int)secondsSinceMidnight;

			_loadedConfig.BuildNumber = buildVersion;

            if (_setRevision)
            {
                _loadedConfig.RevisionVersion++;
            }

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

