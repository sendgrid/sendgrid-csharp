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
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("193fa200-8430-4206-aacd-2d2bb2dfa6cf")]

#if (BUILD)
[assembly: InternalsVisibleTo("Tests," + "" +
                              "PublicKey=0024000004800000940000000602000000240000525341310004000001000100812ec26a66c8e0" +
                              "8c790704ac4b46bcc9da9f4bca4da0ec7c06ce6dcd73baeb2c5525f36a237b253e80e16febb4c0" +
                              "52f50734d5e1cf3bf478d9c88f0f69df53b47306419182983bc35c33c3bafb5e90b9bd7aa7b9a9" +
                              "da09abe3667d50db891012e077e4b9aefe9799a58222fa67127c230219755d7670073c7463d90c" +
                              "f9e79dba")]
#elif (DEBUG)
[assembly: InternalsVisibleTo("Tests," + "" +
                              "PublicKey=0024000004800000940000000602000000240000525341310004000001000100812ec26a66c8e0" +
                              "8c790704ac4b46bcc9da9f4bca4da0ec7c06ce6dcd73baeb2c5525f36a237b253e80e16febb4c0" +
                              "52f50734d5e1cf3bf478d9c88f0f69df53b47306419182983bc35c33c3bafb5e90b9bd7aa7b9a9" +
                              "da09abe3667d50db891012e077e4b9aefe9799a58222fa67127c230219755d7670073c7463d90c" +
                              "f9e79dba")]
#else
[assembly: InternalsVisibleTo("Tests," + "" +
                              "PublicKey=0024000004800000940000000602000000240000525341310004000001000100812ec26a66c8e0" +
                              "8c790704ac4b46bcc9da9f4bca4da0ec7c06ce6dcd73baeb2c5525f36a237b253e80e16febb4c0" +
                              "52f50734d5e1cf3bf478d9c88f0f69df53b47306419182983bc35c33c3bafb5e90b9bd7aa7b9a9" +
                              "da09abe3667d50db891012e077e4b9aefe9799a58222fa67127c230219755d7670073c7463d90c" +
                              "f9e79dba")]
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

[assembly: AssemblyVersion("6.3.2")]
[assembly: AssemblyFileVersion("6.3.2")]