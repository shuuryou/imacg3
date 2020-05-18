using System;
using System.IO;
using System.Xml.Serialization;

/// <summary>
/// Mono is shit and doesn't support Application Settings properly.
/// They're written, they're apparently read, but the values never
/// make it to the Settings object and you just get the defaults.
/// 
/// Thanks to the Mono Team for ruining what was otherwise a pleasant
/// evening by making me write this class!
/// </summary>
namespace crtcpl
{
    [Serializable()]
    public sealed class SettingsImpl
    {
        public string SerialPort = null;
        public int SerialRate = -1;
        public bool AdvancedControls = false;
    }

    public static class Settings
    {
        public static SettingsImpl Default;

        static Settings()
        {
            Load();
        }

        public static void Load()
        {
            Logging.WriteBannerToLog("Load");

            string file = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
                "crtcpl", "config.xml");

            Logging.WriteLineToLog("Settings file is at: {0}", file);

            if (!File.Exists(file))
            {
                Logging.WriteLineToLog("Settings file does not exist.");
                Reset();
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(SettingsImpl));

            try
            {
                Logging.WriteLineToLog("Deserialize settings XML now...");

                using (FileStream fs = File.OpenRead(file))
                {
                    Default = (SettingsImpl)serializer.Deserialize(fs);
                    fs.Close();
                }
            }
            catch (InvalidOperationException e)
            {
                Logging.WriteLineToLog("Unable to load settings: {0}", e);
                Reset();
            }
            catch (IOException e)
            {
                Logging.WriteLineToLog("Unable to load settings: {0}", e);
                Reset();
            }
            catch (UnauthorizedAccessException e)
            {
                Logging.WriteLineToLog("Unable to load settings: {0}", e);
                Reset();
            }

            Logging.WriteLineToLog("Loaded settings OK!");
        }

        public static void Save()
        {
            Logging.WriteBannerToLog("Save");

            string file = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
                "crtcpl", "config.xml");

            Logging.WriteLineToLog("Settings file is at: {0}", file);

            string dir = Path.GetDirectoryName(file);

            if (!Directory.Exists(dir))
            {
                Logging.WriteLineToLog("Create settings directory.");
                Directory.CreateDirectory(dir);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(SettingsImpl));

            Logging.WriteLineToLog("Serialize settings to XML now...");

            try
            {
                using (FileStream fs = File.OpenWrite(file))
                {
                    serializer.Serialize(fs, Default);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (IOException e)
            {
                Logging.WriteLineToLog("Unable to save settings: {0}", e);
                Reset();
            }
            catch (UnauthorizedAccessException e)
            {
                Logging.WriteLineToLog("Unable to save settings: {0}", e);
                Reset();
            }

            Logging.WriteLineToLog("Saved settings OK!");
        }

        public static void Reset()
        {
            Logging.WriteBannerToLog("Reset");

            Default = new SettingsImpl();

            Logging.WriteLineToLog("Settings reset to default.");
        }
    }
}
