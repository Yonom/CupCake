using System;
using System.IO;

namespace CupCake.Client.Settings
{
    public static class SettingsManager
    {
        public const int DebugId = -1;
        public const int DefaultId = -2;

        public const string UnnamedString = "<Unnamed>";
        public const string DefaultString = "<Default>";
        private static readonly string _settingsPath;

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

            DebugPath = ProfilesPath + "\\Debug";

            DatabasesPath = CupCakePath + "\\Databases";
            if (!Directory.Exists(DatabasesPath))
                Directory.CreateDirectory(DatabasesPath);

            DefaultDatabasePath = DatabasesPath + "\\Default.db";

            PluginsPath = CupCakePath + "\\Plugins";
            if (!Directory.Exists(PluginsPath))
                Directory.CreateDirectory(PluginsPath);

            try
            {
                _settingsPath = CupCakePath + "\\Settings.xml";
                Settings = !File.Exists(_settingsPath)
                    ? new Settings(true)
                    : XmlSerialize.Deserialize<Settings>(_settingsPath);
            }
            catch (Exception)
            {
                Settings = new Settings(true);
                MessageBoxHelper.Show(null, "Error", "Failed to load settings.");
            }
        }

        public static string CupCakePath { get; private set; }
        public static string ProfilesPath { get; private set; }
        public static string DefaultProfilePath { get; private set; }
        public static string DatabasesPath { get; private set; }
        public static string DefaultDatabasePath { get; private set; }
        public static string PluginsPath { get; private set; }

        public static Settings Settings { get; private set; }

        public static string DebugPath { get; private set; }

        public static void Save()
        {
            XmlSerialize.Serialize(Settings, _settingsPath);
        }
    }
}