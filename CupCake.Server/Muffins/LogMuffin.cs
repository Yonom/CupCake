using CupCake.Core.Log;
using CupCake.Messages.Receive;
using CupCake.Players;

namespace CupCake.Server.Muffins
{
    public class LogMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.Events.Bind<SayPlayerEvent>(this.OnSay);
            this.Events.Bind<WriteReceiveEvent>(this.OnWrite);
            this.Events.Bind<InfoReceiveEvent>(this.OnInfo);
            this.Events.Bind<UpgradeReceiveEvent>(this.OnUpgrade);
        }

        private void OnUpgrade(object sender, UpgradeReceiveEvent e)
        {
            this.Logger.Log(LogPriority.Message, "The game has been updated.");
        }

        private void OnInfo(object sender, InfoReceiveEvent e)
        {
            this.Logger.Log(LogPriority.Message, e.Title);
            this.Logger.Log(LogPriority.Message, e.Text);
        }

        private void OnWrite(object sender, WriteReceiveEvent e)
        {
            this.Logger.LogPlatform.Log(e.Title, LogPriority.Message, e.Text);
        }

        private void OnSay(object sender, SayPlayerEvent e)
        {
            this.Logger.LogPlatform.Log(e.Player.Username, LogPriority.Message, e.Player.Say);
        }
    }
}