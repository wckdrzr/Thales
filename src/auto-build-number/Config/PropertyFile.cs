using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection.Metadata.Ecma335;

namespace Wckdrzr.AutomaticBuildNumber.Config

{
	[XmlRoot(ElementName = "PropertyGroup")]
	public class PropertyGroup
	{
		private const string ENV_PREFIX = "ABN";
		private const string VERSIONFILENAME = "VersionInfo";

		private string _envPrifix;
		private string _versionInformationFilename;

		[XmlAttribute(AttributeName = "Label")]
		public string Label { get; set; }
		[XmlElement(ElementName = "MajorVersion")]
		public int MajorVersion { get; set; }
		[XmlElement(ElementName = "MinorVersion")]
		public int MinorVersion { get; set; }
		[XmlElement(ElementName = "Revision")]
		public int RevisionVersion { get; set; }
		[XmlElement(ElementName = "BuildNumber")]
		public int BuildNumber { get; set; }

		[XmlElement(ElementName = "VersionInformationFilename")]
		public string VersionInformationFilename
		{
			get{ return _versionInformationFilename == null ? VERSIONFILENAME : _versionInformationFilename; }
			set{ _versionInformationFilename = value; }
		}
		[XmlElement(ElementName = "EnvironmentVariable_Prefix")]
		public string EnvironmentVariable_Prefix
		{
			get { return _envPrifix == null ? ENV_PREFIX : _envPrifix;	}
			set { _envPrifix = value; }
		}
		[XmlElement(ElementName = "AddVersionController")]
		public bool AddVersionController { get; set; }
		public bool IsDefaultConfig { get; set; }

		public bool ShouldSerializeEnvironmentVariable_Prefix()
		{
			return (!EnvironmentVariable_Prefix.Equals(ENV_PREFIX));
		}

		public bool ShouldSerializeVersionInformationFilename()
		{
			return (!VersionInformationFilename.Equals(VERSIONFILENAME));
		}

		public bool ShouldSerializeAddVersionController()
		{
			return (!AddVersionController);
		}

		public bool ShouldSerializeIsDefaultConfig()
		{
			return false;
		}

		public string VersionNumber()
        {
			return $"{MajorVersion}.{MinorVersion}.{RevisionVersion}.{BuildNumber}";
		}
	}

	[XmlRoot(ElementName = "Project")]
	public class Project
	{
        [XmlElement(ElementName = "PropertyGroup")]
        public List<PropertyGroup> PropertyGroup { get; set; }
	}
}
