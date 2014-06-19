using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Players;

namespace CupCake.Physics
{
    public static class PlayerExtensions
    {
        public static PhysicsPlayer GetPhysicsPlayer(this Player p)
        {
            PhysicsPlayer physicsP;
            p.Metadata.GetMetadata("PhysicsPlayer", out physicsP);
            return physicsP;
        }

        public static void SetPhysicsPlayer(this Player p, PhysicsPlayer physicsPlayer)
        {
            p.Metadata.SetMetadata("PhysicsPlayer", physicsPlayer);
        }
    }
}
