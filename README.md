# Thales - Auto Build Verion Update library


## Dependencies

* [.NET Core SDK](https://www.microsoft.com/net/download/macos)


## Install

Install from NuGet: https://www.nuget.org/packages/WckdRzr.Thales/


## How to use



1. Add a NuGet script to your project.

2. Run a Build. 

3. The project's csproj file will now contain parameters which include version and build numbers

4. Addtional params can be set:
<EnvironmentVariable_Prefix>string value</EnvironmentVariable_Prefix> This will tell Thales the name of the env var to use to determine if a Major or Minor change is required
<VersionInformationFilename>string value</VersionInformationFilename> This allows you to override the default filename that contain the build and version information
<AddVersionController>boolean value</AddVersionController> This allow you to automatically generate a /version endpoint if your project contains controllers.


## License

MIT © [Wckd Rzr](https://github.com/wckdrzr)
