using System.Resources;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Megaplan.API.Portable")]
[assembly: AssemblyDescription("Megaplan.API.Portable")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ihtfw")]
[assembly: AssemblyProduct("Megaplan.API.Portable")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version   
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(Constants.Version)]
[assembly: AssemblyFileVersion(Constants.Version)]

internal static class Constants
{
    public const string Version = "1.2.12";
}