using System;
using System.Runtime.InteropServices;

namespace crtcpl
{
#if !MONO
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
#endif
}
