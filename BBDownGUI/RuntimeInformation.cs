using System.Runtime.InteropServices;

namespace BBDownGUI
{
    internal class RuntimeInformation
    {
        public static PlatformID Platform = Environment.OSVersion.Platform;
        public static Architecture Architecture = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture;
    }
}
