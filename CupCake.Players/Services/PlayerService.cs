using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Services;
using CupCake.EE.Events.Receive;
using CupCake.Players.Join;

namespace CupCake.Players.Services
{
    public class PlayerService : CupCakeService<JoinArgs>
    {
        protected override void Enable()
        {
            this.Events.Bind<AddReceiveEvent>(this.OnAdd);
        }

        private void OnAdd(object sender, AddReceiveEvent e)
        {
            this.EnablePart<Player>(new AddJoinArgs(e));
        }
    }
}
