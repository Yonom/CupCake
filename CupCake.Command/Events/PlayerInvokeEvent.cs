using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Events;
using CupCake.Permissions;
using CupCake.Players;
using CupCake.Players.Events;

namespace CupCake.Command.Events
{
    public class PlayerInvokeEvent : Event
    {
        public Player Player { get; private set; }
        public string Message { get; set; }
        public Group Group { get; set; }

        public PlayerInvokeEvent(Player player, string message)
        {
            this.Player = player;
            this.Message = message;
            this.Group = player.GetGroup();
        }

        public PlayerInvokeEvent(Player player, string message, Group @group)
        {
            this.Player = player;
            this.Message = message;
            this.Group = @group;
        }
    }
}
