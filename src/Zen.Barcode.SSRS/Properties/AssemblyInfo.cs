using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Zen Barcode SSRS CRI")]
[assembly: AssemblyDescription("Barcode Rendering Framework Custom Report Item for SSRS")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("a44a2807-82c2-4108-9d12-4c802c1bcd4f")]

// Need to allow partially trusted callers to get SSRS to call our builder
//	from the header/footer...
[assembly: AllowPartiallyTrustedCallers]
