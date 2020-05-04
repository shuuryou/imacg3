using System;
using System.Runtime.InteropServices;

namespace crtcpl
{
#if !MONO
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetActiveWindow(IntPtr hWnd);
    }
#endif
}
