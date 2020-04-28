using System;
using System.Runtime.InteropServices;

namespace crtcpl
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetActiveWindow(IntPtr hWnd);
    }
}
