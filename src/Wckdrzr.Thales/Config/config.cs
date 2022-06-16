using System;
namespace Wckdrzr.AutomaticVersionUpdate.Config
{
	public class Config
	{
        public string Company { get; set; }
        public string Copyright { get; set; }
        public string ProductName { get; set; }
        public string SpecialBuild { get; set; }
        public string ProductConfiguration { get; set; }

        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int RevisionVersion { get; set; }
        public int BuildVersion { get; set; }

        public string OutputFileName { get; set; }
        public bool AddVersionController { get; set; }
        public string EnvironmentVariablePrefix { get; set; }

        public string VersionNumber()
        {
            return $"{MajorVersion}.{MinorVersion}.{RevisionVersion}.{BuildVersion}";
        }
    }
}

