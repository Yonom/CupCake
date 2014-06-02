using CupCake.Protocol;

namespace CupCake.Client.Settings
{
    public class Database : IConfig
    {
        public static string SQLiteFormat = "Data Source={0};Version=3;";

        public string ConnectionString { get; set; }
        public DatabaseType Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public static Database NewEmpty()
        {
            return new Database {Id = ++SettingsManager.Settings.LastDatabaseId};
        }
    }
}