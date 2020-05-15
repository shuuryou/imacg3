using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace crtcpl
{
    internal static class Logging
    {
        private static StreamWriter s_LogWriter;

        public static string LogFile { get; private set; }

        public static void OpenLog()
        {
            if (s_LogWriter == null)
            {
                LogFile = Path.Combine(Path.GetTempPath(), "crtcpl.log");
                s_LogWriter = new StreamWriter(LogFile, false, Encoding.UTF8);
            }
        }

        public static void CloseLog()
        {
            if (s_LogWriter == null)
            {
                return;
            }

            WriteLineToLog("Closing log file.");
            s_LogWriter.Close();

            s_LogWriter.Dispose();
            s_LogWriter = null;
        }

        public static void WriteLineToLog(string value)
        {
            if (s_LogWriter != null)
            {
                s_LogWriter.WriteLine("{0}\t{1}", DateTime.Now, value);
                s_LogWriter.Flush();
            }
        }

        public static void WriteBufferToLog(byte[] buffer, int offset, int amount)
        {
            if (s_LogWriter != null)
            {
                s_LogWriter.Write("{0}\t{{ ", DateTime.Now);
                for (int i = offset; i < amount; i++)
                {
                    s_LogWriter.Write("0x{0:X2}", buffer[i]);

                    if (i != amount - 1)
                    {
                        s_LogWriter.Write(", ");
                    }
                }
                s_LogWriter.WriteLine(" }");
                s_LogWriter.Flush();
            }
        }

        public static void WriteBannerToLog(string name)
        {
            WriteLineToLog("------------- {0} -------------", name);
        }

        public static void WriteLineToLog(string format, params object[] args)
        {
            WriteLineToLog(string.Format(CultureInfo.InvariantCulture, format, args));
        }
    }
}

