using System;
using System.Linq;
using System.Text.RegularExpressions;
using PlayerIOClient;
using Rabbit;
using Rabbit.Auth;

namespace CupCake.Server
{
    internal class CupCakeAuth
    {
        private const int EEVersion = 181;

        public Connection Connect(string email, string password, string worldId)
        {
            var client = this.Login(email, password);
            return Connect(client, worldId);
        }

        public Client Login(string email, string password)
        {
            var authenticationType = RabbitAuth.GetAuthType(email, password);
            switch (authenticationType)
            {
                case AuthenticationType.Facebook:
                    return Facebook.Authenticate(password);

                case AuthenticationType.Kongregate:
                    return Kongregate.Authenticate(email, password);

                case AuthenticationType.ArmorGames:
                    return ArmorGames.Authenticate(email, password);

                case AuthenticationType.MouseBreaker:
                    return MouseBreaker.Authenticate(email, password);

                case AuthenticationType.UserName:
                    return UserName.Authenticate(email, password);

                default:
                    return PlayerIO.QuickConnect.SimpleConnect(RabbitAuth.GameId, email, password);
            }
        }

        public Connection Connect(Client client, string worldId)
        {
            var roomPrefix = worldId.StartsWith("BW", StringComparison.OrdinalIgnoreCase)
                ? "Beta"
                : "Everybodyedits";

            return this.ConnectInternal(client, worldId, roomPrefix);
        }

        private Connection ConnectInternal(Client client, string worldId, string roomPrefix, int roomVersion = EEVersion)
        {
            try
            {
                var roomType = roomPrefix + roomVersion;
                return client.Multiplayer.CreateJoinRoom(worldId, roomType, true, null, null);
            }
            catch (PlayerIOError ex)
            {
                if (ex.ErrorCode == ErrorCode.UnknownRoomType)
                {
                    var version = this.GetVersion(roomPrefix, ex.Message);
                    return this.ConnectInternal(client, worldId, roomPrefix, version);
                }
                else
                {
                    throw;
                }
            }
        }

        private int GetVersion(string roomPrefix, string message)
        {
            try
            {
                var results = new Regex(roomPrefix + @"([0-9]+)[,\]]").Matches(message);
                return results.Cast<Match>().Max(m => Int32.Parse(m.Groups[1].Value));
            }
            catch (Exception ex)
            {
                throw new FormatException("Unable to parse the version number received from EE.", ex);
            }
        }
    }
}
