using CupCake.Core.Log;
using CupCake.Players;

namespace CupCake.Server
{
    public class BasicLogMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.Events.Bind<SayPlayerEvent>(this.OnSay);
        }

        private void OnSay(object sender, SayPlayerEvent e)
        {
            this.Logger.LogPlatform.Log(e.Player.Username, LogPriority.Message, e.Player.Say);
        }
    }
}