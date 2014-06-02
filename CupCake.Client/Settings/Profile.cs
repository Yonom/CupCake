namespace CupCake.Client.Settings
{
    public class Profile : IConfig
    {
        public string Folder { get; set; }
        public int Database { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public static Profile NewEmpty()
        {
            return new Profile {Id = ++SettingsManager.Settings.LastProfileId};
        }
    }
}