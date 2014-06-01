using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIOClient;

namespace CupCake.Server
{
    internal static class RoomHelper
    {
        public static int GetVersion()
        {
            var client = PlayerIO.QuickConnect.SimpleConnect(CupCakeClientEx.GameId, "guest", "guest");
            var dbO = client.BigDB.Load("config", "config");
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
