using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CupCake.Core;
using CupCake.Players;

namespace CupCake.Physics
{
    public sealed class PhysicsService : CupCakeService<Player>
    {
        protected override void Enable()
        {
            this.Events.Bind<JoinPlayerEvent>(this.OnJoin);
            
            var timer = this.GetTimer(PhysicsPlayer.PhysicsMsPerTick);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        private void OnJoin(object sender, JoinPlayerEvent e)
        {
            e.Player.SetPhysicsPlayer(this.EnablePart<PhysicsPlayer>(e.Player));
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Events.Raise(new TickPhysicsEvent());
        }
    }
}
