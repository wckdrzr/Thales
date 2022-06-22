﻿using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Wckdrzr.AutomaticVersionUpdate.Config;

namespace Wckdrzr.AutomaticVersionUpdate.IO
{
	public class FileServices
	{
		public PropertyGroup Config;
		private readonly Serializer _xmlSerializer;
		private const string ApplicationName = "Wckdrzr.Thales";
		private const string FileExtention = ".xml";
        private const string ConfigPath = "./";

        public FileServices()
		{
			_xmlSerializer = new Serializer();	

			PropertyGroup propertyGroup = _xmlSerializer.Deserialize<PropertyGroup>(File.ReadAllText(GetPropertyFileLocation()));

			if (propertyGroup.Label == ApplicationName)
			{
				Config = propertyGroup;
			}

			if (Config == null)
				Config = CreateDefaultConfig();
		}

		private void CreatePropertyFile(string filename)
        {
            Config = CreateDefaultConfig();

            XmlDocument tempDoc = new XmlDocument();
            tempDoc.LoadXml(_xmlSerializer.Serialize<PropertyGroup>(Config));

			var settings = new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = true
            };

            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using var stream = new StreamWriter(filename);
            using var writer = XmlWriter.Create(stream, settings);
            tempDoc.Save(writer);
        }

		private string GetPropertyFileLocation()
        {
			string filename = ConfigPath + $"{ApplicationName}{FileExtention}";

            if (!File.Exists(filename))
			{
				CreatePropertyFile(filename);
				return ApplicationName + FileExtention;
			}
			
			return $"{ApplicationName}{FileExtention}";
		}

        private PropertyGroup CreateDefaultConfig()
        {
			PropertyGroup defaultConfig = new PropertyGroup{ Label = ApplicationName, MajorVersion = 0, MinorVersion = 0, RevisionVersion = 1, BuildNumber=0, IsDefaultConfig = true};
			return defaultConfig;
        }

        public bool UpdateAppConfig()
        {
			string newPropertyString = _xmlSerializer.Serialize<PropertyGroup>(Config);

			try
			{
				string fileContents = File.ReadAllText(GetPropertyFileLocation());
				XmlDocument projFileXml = new XmlDocument();
				projFileXml.LoadXml(fileContents);

				XmlDocument tempDoc = new XmlDocument();
				tempDoc.LoadXml(newPropertyString);

				XmlNode oldNode;
				XmlNode parent = null;

				oldNode = projFileXml.SelectSingleNode($"/PropertyGroup[@Label='{ApplicationName}']");
				if (oldNode != null)
				{
					parent = oldNode.ParentNode;
					parent.RemoveChild(oldNode);
				}

				XmlNode newNode = tempDoc.DocumentElement;
				XmlNode importedNode = projFileXml.ImportNode(newNode, true);
				parent.AppendChild(importedNode);

				var settings = new XmlWriterSettings
				{
					Indent = true,
					OmitXmlDeclaration = true
				};

				var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

				using var stream = new StreamWriter(GetPropertyFileLocation());
				using var writer = XmlWriter.Create(stream, settings);
				projFileXml.Save(writer);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error Encountered updating the dotnet Project File: {ex.Message}");
				return false;
			}
			return true;
		}

		public bool WriteAssemblyInfoFile()
		{
			string filename = $"{ConfigPath}{Config.VersionInformationFilename}.cs";

			try
            {
				if (File.Exists(filename))
					File.Delete(filename);
			}
			catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
	
			string[] fileContents =
            {
                "using System.Reflection;",
                "using System.Runtime.InteropServices;",
                " ",
                $"[assembly: AssemblyVersion(\"{Config.VersionNumber()}\")]"
            };

            try
            {
				File.WriteAllLines($"{ConfigPath}{filename}.cs", fileContents);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Encountered: {ex.Message}");
                return false;
            }

            return true;
		}

		public bool WriteVersionRoute()
        {
			string[] fileContents =
			{
			"using Microsoft.AspNetCore.Mvc;",
			" ",
			"namespace WCKDRZR.AutoGenerated.Controllers",
			"{",
			"	[ApiController]",
			"	public class VersionController : ControllerBase",
			"	{",
			"		[HttpGet]",
			"		[Route(\"[controller]\")]",
			"		public string Get()",
			"       {",
			"			return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();",
			"		}",
			"	}",
			"}"
			};

			try
			{
				File.WriteAllLines($"{ConfigPath}Controllers/VersionController.cs", fileContents);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error Encountered: {ex.Message}");
				return false;
			}

			return true;
		}
	}
}

