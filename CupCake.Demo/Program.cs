using System.Threading;
using CupCake.Core.Host;
using PlayerIOClient;

namespace CupCake.Demo
{
    internal class Program
    {
        private const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";

        private static void Main(string[] args)
        {
            var playerioclient = PlayerIO.QuickConnect.SimpleConnect(GameId, "sepi1376@gmail.com", "1346279");
            var connection = playerioclient.Multiplayer.JoinRoom("PWWkBWyGyla0I", null);

            var client = new CupCakeClient(connection);
            client.Start();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}