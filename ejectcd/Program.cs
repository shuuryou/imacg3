using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace ejectcd
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            Mutex m = new Mutex(true, "ejectcd", out bool result);

            if (!result)
            {
                Console.Error.Write("Another instance is already running.");
                return 1;
            }

            try
            {
                DriveInfo[] drives;

                try
                {
                    drives = DriveInfo.GetDrives();
                }
                catch (IOException e)
                {
                    Console.Error.WriteLine(e.Message);
                    return 1;
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.Error.WriteLine(e.Message);
                    return 1;
                }

                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType != DriveType.CDRom)
                    {
                        continue;
                    }

                    Console.WriteLine("Eject CD-ROM drive {0}.", drive.Name);

                    IntPtr hDrive = new IntPtr(INVALID_HANDLE_VALUE);

                    try
                    {
                        // Open the device
                        hDrive = CreateFile(@"\\.\" + drive.Name[0] + ':', FileAccess.Read, FileShare.ReadWrite | FileShare.Delete,
                            IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

                        if ((int)hDrive == INVALID_HANDLE_VALUE) { throw new Win32Exception(); }

                        // Try and eject
                        int dummy = 0;
                        bool ejected = DeviceIoControl(hDrive, IOCTL_STORAGE_EJECT_MEDIA, IntPtr.Zero, 0,
                            IntPtr.Zero, 0, ref dummy, IntPtr.Zero);

                        if (!ejected) { throw new Win32Exception(); }

                    }
                    catch (Win32Exception e)
                    {
                        Console.Error.WriteLine(e.Message);
                        return 1;
                    }
                    finally
                    {
                        if (hDrive.ToInt32() != INVALID_HANDLE_VALUE)
                        {
                            CloseHandle(hDrive);
                        }
                    }
                }

                Console.WriteLine("Done.");
            }
            finally
            {
                m.ReleaseMutex();
                m.Dispose();
            }

            return 0;
        }

        private const long INVALID_HANDLE_VALUE = -1;
        private const int IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateFile(
           [MarshalAs(UnmanagedType.LPTStr)] string filename,
           [MarshalAs(UnmanagedType.U4)] FileAccess access,
           [MarshalAs(UnmanagedType.U4)] FileShare share,
           IntPtr securityAttributes,
           [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
           [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
           IntPtr templateFile);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DeviceIoControl(
                IntPtr hDevice,
                uint dwIoControlCode,
                IntPtr InBuffer,
                int nInBufferSize,
                IntPtr OutBuffer,
                int nOutBufferSize,
                ref int pBytesReturned,
                [In] IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);
    }
}