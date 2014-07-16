using CupCake.Core.Log;
using CupCake.Messages.Receive;
using CupCake.Players;

namespace CupCake.Server.Muffins
{
    public class LogMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            Events.Bind<SayPlayerEvent>(OnSay);
            Events.Bind<WriteReceiveEvent>(OnWrite);
            Events.Bind<InfoReceiveEvent>(OnInfo);
            Events.Bind<UpgradeReceiveEvent>(OnUpgrade);
        }

        private void OnUpgrade(object sender, UpgradeReceiveEvent e)
        {
            Logger.Log(LogPriority.Message, "The game has been updated.");
        }

        private void OnInfo(object sender, InfoReceiveEvent e)
        {
            Logger.Log(LogPriority.Message, e.Title);
            Logger.Log(LogPriority.Message, e.Text);
        }

        private void OnWrite(object sender, WriteReceiveEvent e)
        {
            Logger.LogPlatform.Log(e.Title, LogPriority.Message, e.Text);
        }

        private void OnSay(object sender, SayPlayerEvent e)
        {
            Logger.LogPlatform.Log(e.Player.Username, LogPriority.Message, e.Player.Say);
        }
    }
}