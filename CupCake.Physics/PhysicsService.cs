using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CupCake.Core;
using CupCake.Messages.Receive;
using CupCake.Physics.EEPhysics;
using CupCake.Players;

namespace CupCake.Physics
{
    public sealed class PhysicsService : CupCakeService<Player>
    {
        private readonly PhysicsWorld _physicsWorld = new PhysicsWorld();

        protected override void Enable()
        {
            this.Events.Bind<JoinPlayerEvent>(this.OnJoin);
            this.Events.Bind<ReceiveEvent>(this.OnMessage);
        }

        private void OnJoin(object sender, JoinPlayerEvent e)
        {
            e.Player.SetPhysicsPlayer(this._physicsWorld.Players[e.Player.UserId]);
        }

        private void OnMessage(object sender, ReceiveEvent e)
        {
            this._physicsWorld.HandleMessage(e.PlayerIOMessage);
        }
    }
}
