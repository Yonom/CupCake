using System;
using System.Collections.Generic;
using CupCake.Protocol;

namespace CupCake.Client.Settings
{
    public class Settings
    {
        public Settings()
            : this(false)
        {
        }

        public Settings(bool isNew)
        {
            this.Accounts = new List<Account>();
            this.Profiles = new List<Profile>();
            this.RecentWorlds = new List<RecentWorld>();
            this.Databases = new List<Database>();

            if (isNew)
            {
                this.Profiles.Add(new Profile
                {
                    Id = SettingsManager.DefaultId,
                    Name = SettingsManager.DefaultString,
                    Folder = SettingsManager.ProfilesPath,
                    Database = SettingsManager.DefaultId
                });

                this.Databases.Add(new Database
                {
                    Id = SettingsManager.DefaultId,
                    Name = SettingsManager.DefaultString,
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