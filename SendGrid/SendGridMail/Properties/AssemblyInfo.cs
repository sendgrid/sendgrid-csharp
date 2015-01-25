using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("SendGridMail")]
[assembly: AssemblyDescription("A client library for interfacing with the SendGridMessage API")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("SendGridMessage")]
[assembly: AssemblyProduct("SendGridMail")]
[assembly: AssemblyCopyright("Copyright ©  2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("193fa200-8430-4206-aacd-2d2bb2dfa6cf")]

#if (BUILD)
[assembly: InternalsVisibleTo("Tests")]
#elif (DEBUG)
[assembly: InternalsVisibleTo("Tests")]
#else
[assembly: InternalsVisibleTo("Tests," + "" +
                              "PublicKey=00240000048000009400000006020000002400005253413100040000010001004126bffd5a4461" +
                              "e915193b2695401cee8d67bb14b252a34e5230e6468582f108aafbe31d39f2059240461d622e86" +
                              "2a294169d5f2659efe0d68b30d7ceee310356c70b54ece3c8c69bbd9db86e07c34ff4fd5d7528b" +
                              "3ddf078d272025cb7a588030c78020f5eb91872b38dc2832f561fe184715bb8edb6f0b3b644de5" +
                              "2bc588ae")]
#endif
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

[assembly: AssemblyVersion("5.1.0")]
[assembly: AssemblyFileVersion("5.1.0")]