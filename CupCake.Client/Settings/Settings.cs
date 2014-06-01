using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CupCake.Protocol;

namespace CupCake.Client.Settings
{
    public class Settings
    {
        public Settings() : this(false)
        {
        }

        public Settings(bool isNew)
        {
            Accounts = new List<Account>();
            Profiles = new List<Profile>();
            RecentWorlds = new List<RecentWorld>();
            Databases = new List<Database>();

            if (isNew)
            {
                this.Profiles.Add(new Profile
                {
                    Name = "<Default>",
                    Folder = SettingsManager.ProfilesPath
                });

                this.Databases.Add(new Database
                {
                    Name = "<Default>",
                    Type = DatabaseType.SQLite,
                    ConnectionString = String.Format(Database.SQLiteFormat, SettingsManager.DefaultDatabasePath)
                });
            }
        }

        public List<Account> Accounts { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<RecentWorld> RecentWorlds { get; set; }
        public List<Database> Databases { get; set; }

        public int LastProfileId { get; set; }
        public int LastAccountId { get; set; }
        public int LastRecentWorldId { get; set; }
        public int LastDatabaseId { get; set; }

        public string LastAttachAddress { get; set; }
        public string LastAttachPin { get; set; }
    }
}
