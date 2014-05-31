using System;
using System.IO;
using System.Windows;

namespace CupCake.Client.Settings
{
    public static class SettingsManager
    {
        public static string CupCakePath { get; private set; }
        public static string ProfilesPath { get; private set; }
        public static string DefaultProfilePath { get; set; }
        private static readonly string _settingsPath;

        public static Settings Settings { get; private set; }

        static SettingsManager()
        {

            CupCakePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CupCake";
            if (!Directory.Exists(CupCakePath))
                Directory.CreateDirectory(CupCakePath);

            ProfilesPath = CupCakePath + "\\Profiles";
            if (!Directory.Exists(ProfilesPath))
                Directory.CreateDirectory(ProfilesPath);

            DefaultProfilePath = ProfilesPath + "\\Default";
            if (!Directory.Exists(DefaultProfilePath))
                Directory.CreateDirectory(DefaultProfilePath);

            try
            {
                _settingsPath = CupCakePath + "\\Settings.xml";
                Settings = !File.Exists(_settingsPath)
                    ? new Settings()
                    : XmlSerialize.Deserialize<Settings>(_settingsPath);
            }
            catch (Exception)
            {
                Settings = new Settings();
                MessageBoxHelper.Show(null, "Error", "Failed to load settings.");
            }
        }

        public static void Save()
        {
            XmlSerialize.Serialize(Settings, _settingsPath);
        }
    }
}
