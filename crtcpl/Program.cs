using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace crtcpl
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.Equals("/log", StringComparison.OrdinalIgnoreCase))
                {
                    Logging.OpenLog();
                }

                if (arg.Equals("/reset", StringComparison.OrdinalIgnoreCase))
                {
                    Logging.WriteLineToLog("Resetting settings.");

                    Settings.Default.Reset();
                    Settings.Default.Save();

                    MessageBox.Show(StringRes.StringRes.SettingsReset,
                        StringRes.StringRes.SettingsResetTitle, MessageBoxButtons.OK);
                }
            }

            Logging.WriteBannerToLog("Main");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Mutex m = new Mutex(true, "crtcpl", out bool result);

            if (!result)
            {
                Logging.WriteLineToLog("Another instance is already running.");

                try
                {
#if !MONO
                    // Best effort switch to active instance
                    Process[] procs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Application.ExecutablePath));
                    foreach (Process p in procs)
                    {
                        if (p.Id == Process.GetCurrentProcess().Id)
                        {
                            continue;
                        }

                        if (p.MainWindowHandle == IntPtr.Zero)
                        {
                            continue;
                        }

                        const int SW_SHOW = 5;

                        NativeMethods.SetForegroundWindow(p.MainWindowHandle);
                        NativeMethods.ShowWindow(p.MainWindowHandle, SW_SHOW);

                        Logging.WriteLineToLog("Switched to other instance.");

                        break;
                    }
#else
                    Logging.WriteLineToLog("Show error message.");

                    // Sorry, don't know what to do.
                    MessageBox.Show(StringRes.StringRes.AlreadyRunning, StringRes.StringRes.AlreadyRunningTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                }
                catch (Exception e)
                {
                    Logging.WriteLineToLog("Exception while switching to other instance: {0}", e);
#if DEBUG
                    throw;
#endif
                }

                Logging.WriteLineToLog("Exiting.");

                return 1;
            }

            try
            {
                if (Settings.Default.NeedsUpgrade)
                {
                    Logging.WriteLineToLog("Upgrading settings.");

                    Settings.Default.Upgrade();
                    Settings.Default.NeedsUpgrade = false;
                    Settings.Default.Save();
                }

                if (!string.IsNullOrWhiteSpace(Settings.Default.SerialPort))
                {
                    Logging.WriteLineToLog("Try to connect to serial port {0} at rate {1} from settings.",
                        Settings.Default.SerialPort, Settings.Default.SerialRate);

                    try
                    {
                        UCCom.Open(Settings.Default.SerialPort, Settings.Default.SerialRate);
                    }
                    catch (UCComException e)
                    {
                        Logging.WriteLineToLog("Could not open serial port, so set to null. Error: {0}", e);

                        Settings.Default.SerialPort = null;
                        Settings.Default.SerialRate = -1;
                    }
                }

                Logging.WriteLineToLog("Main window opening now.");

                using (AppletForm a = new AppletForm())
                {
                    Application.Run(a);
                }
            }
            catch (Exception e)
            {
                Logging.WriteLineToLog("Crashed with exception in Main: {0}", e);
            }
            finally
            {
                Logging.WriteLineToLog("Final cleanup running.");

                m.ReleaseMutex();
                m.Dispose();

                Logging.WriteLineToLog("Released and disposed mutex.");

                Logging.WriteLineToLog("Closing serial port.");

                try
                {
                    if (UCCom.IsOpen)
                    {
                        UCCom.Close();
                    }
                }
                catch (Exception e)
                {
                    Logging.WriteLineToLog("Error closing serial port: {0}", e);
                }
            }

            Logging.WriteLineToLog("Exiting now...");

            Application.Exit();

            return 0;
        }
    }
}
