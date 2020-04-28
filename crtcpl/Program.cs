using System;
using System.Windows.Forms;

namespace crtcpl
{
    public static class Program
    {
        public const byte SUPPORTED_EEPROM_VERSION = 0x01;

        [STAThread]
        public static void Main()
        {
#if DEBUG
            System.Threading.Thread.CurrentThread.CurrentCulture =
                 System.Threading.Thread.CurrentThread.CurrentUICulture =
                 new System.Globalization.CultureInfo("de-de");
#endif
            if (Settings.Default.NeedsUpgrade)
            {
                Settings.Default.Upgrade();
                Settings.Default.NeedsUpgrade = false;
                Settings.Default.Save();
            }

            if (!string.IsNullOrWhiteSpace(Settings.Default.SerialPort))
            {
                bool bad = false;
                try
                {
                    UCCom.Open(Settings.Default.SerialPort);
                    byte[] ret = UCCom.SendCommand(1, 0, 0);

                    if (ret == null || ret.Length != 1 || ret[0] != SUPPORTED_EEPROM_VERSION)
                    {
                        bad = true;
                    }
                }
                catch (UCComException)
                {
                    bad = true;
                }

                if (bad)
                {
                    Settings.Default.SerialPort = null;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (AppletForm a = new AppletForm())
            {
                Application.Run(a);
            }
        }
    }
}