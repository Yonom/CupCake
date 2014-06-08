using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Events;
using CupCake.Players;

namespace CupCake.Permissions
{
    public class ChangedPermissionEvent : Event
    {
        public Player Player { get; set; }
        public Group NewPermission { get; set; }

        public ChangedPermissionEvent(Player player, Group newPermission)
        {
            this.Player = player;
            this.NewPermission = newPermission;
        }
    }
}
