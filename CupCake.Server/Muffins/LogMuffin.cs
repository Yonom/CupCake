using CupCake.Core;
using CupCake.Core.Log;
using CupCake.Messages.Receive;
using CupCake.Players;

namespace CupCake.Server.Muffins
{
    public class LogMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
        }

        [EventListener]
        private void OnUpgrade(UpgradeReceiveEvent e)
        {
            this.Logger.Log(LogPriority.Message, "The game has been updated.");
        }

        [EventListener]
        private void OnInfo(InfoReceiveEvent e)
        {
            this.Logger.Log(LogPriority.Message, e.Title);
            this.Logger.Log(LogPriority.Message, e.Text);
        }

        [EventListener]
        private void OnWrite(WriteReceiveEvent e)
        {
            this.Logger.LogPlatform.Log(e.Title, LogPriority.Message, e.Text);
        }

        [EventListener]
        private void OnSay(SayPlayerEvent e)
        {
            this.Logger.LogPlatform.Log(e.Player.Username, LogPriority.Message, e.Player.Say);
        }
    }
}