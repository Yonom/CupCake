namespace CupCake.Client.Settings
{
    public class RecentWorld : IConfig
    {
        public string WorldId { get; set; }
        public int Profile { get; set; }
        public int Account { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public void UpdateId()
        {
            Id = ++SettingsManager.Settings.LastRecentWorldId;
        }

        public RecentWorld Clone()
        {
            return new RecentWorld
            {
                WorldId = WorldId,
                Profile = Profile,
                Account = Account,
                Id = Id
            };
        }
    }
}