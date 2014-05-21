using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Services;
using CupCake.EE.Events.Receive;

namespace CupCake.Players.Services
{
    public class PlayerService : CupCakeService<PlayerService>
    {
        protected override void Enable()
        {
            this.Events.Bind<AddReceiveEvent>(OnAdd);
        }

        private void OnAdd(object sender, AddReceiveEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
