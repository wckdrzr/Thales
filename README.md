# Thales - Auto Build Verion Update library

## Dependencies

* [.NET Core SDK](https://www.microsoft.com/net/download/macos)

## Install

Install from NuGet: https://www.nuget.org/packages/WckdRzr.Thales/

## How to use

1. Add a NuGet script to your project.

2. Run a Build. 

3. The project's folder will now contain a settings file which contains parameters which include version and build numbers

4. Addtional params can be set:

5. Adding one of the below properties allows you change some default values or add functionallity
   
   | Feature                                 | Property Name                | Property Type |
   | --------------------------------------- |:----------------------------:|:-------------:|
   | Environment Variable Prefix to look for | <EnvironmentVariable_Prefix> | String        |
   | Add Version Controller                  | <AddVersionController>       | Boolean       |
   | Override the Version File Name          | <VersionInformationFilename> | String        |

## License

MIT © [Wckd Rzr](https://github.com/wckdrzr)
