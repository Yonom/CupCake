using System;
using System.IO;

namespace CupCake.Client.Settings
{
    public static class SettingsManager
    {
        public static string CupCakePath { get; private set; }
        public static string ProfilesPath { get; private set; }
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

            _settingsPath = CupCakePath + "\\Settings.xml";
            Settings = !File.Exists(_settingsPath)
                ? new Settings()
                : XmlSerialize.Deserialize<Settings>(_settingsPath);
        }

        public static void Save()
        {
            XmlSerialize.Serialize(Settings, _settingsPath);
        }
    }
}
