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
        public Player Player { get; private set; }
        public Group OldPermission { get; private set; }
        public Group NewPermission { get; set; }

        internal ChangedPermissionEvent(Player player, Group oldPermission, Group newPermission)
        {
            this.Player = player;
            this.OldPermission = oldPermission;
            this.NewPermission = newPermission;
        }
    }
}
