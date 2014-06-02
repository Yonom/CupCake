using PlayerIOClient;

namespace CupCake.Server
{
    internal static class RoomHelper
    {
        public static int GetVersion()
        {
            Client client = PlayerIO.QuickConnect.SimpleConnect(CupCakeClientEx.GameId, "guest", "guest");
            DatabaseObject dbO = client.BigDB.Load("config", "config");
            return dbO.GetInt("version");
        }

        public static string GetRoomType(string roomId, int version)
        {
            if (roomId.StartsWith("BW"))
                return "Beta" + version;

            return "Everybodyedits" + version;
        }
    }
}