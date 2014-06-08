using CupCake.Command;
using CupCake.Core.Log;
using CupCake.Messages.Receive;
using CupCake.Players;

namespace CupCake.Server.IO
{
    public class IOMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.Events.Bind<InputEvent>(this.OnInput);
            this.Events.Bind<SayPlayerEvent>(this.OnSay);
            this.Events.Bind<WriteReceiveEvent>(this.OnWrite);
            this.Events.Bind<InfoReceiveEvent>(this.OnInfo);
            this.Events.Bind<UpgradeReceiveEvent>(this.OnUpgrade);
        }

        private void OnInput(object sender, InputEvent e)
        {
            this.CommandService.InvokeFromConsole(new ParsedCommand(e.Input));
        }

        private void OnUpgrade(object sender, UpgradeReceiveEvent e)
        {
            this.Logger.LogPlatform.Log("Game", LogPriority.Message, "The game has been updated.");
        }

        private void OnInfo(object sender, InfoReceiveEvent e)
        {
            this.Logger.LogPlatform.Log(e.Title, LogPriority.Message, e.Text);
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