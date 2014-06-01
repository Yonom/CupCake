using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CupCake.Protocol;

namespace CupCake.Client.Settings
{
    public class Database : IConfig
    {
        public static string SQLiteFormat = "Data Source={0};Version=3;";

        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public DatabaseType Type { get; set; }

        public static Database NewEmpty()
        {
            return new Database { Id = ++SettingsManager.Settings.LastDatabaseId };
        }
    }
}
