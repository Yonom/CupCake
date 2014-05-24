using System;
using CupCake.Core.Services;
using CupCake.EE.Blocks;
using CupCake.EE.Events.Send;
using CupCake.EE.User;
using CupCake.Room.Services;

namespace CupCake.Actions.Services
{
    public class ActionService : CupCakeService
    {
        private RoomService _room;

        protected override void Enable()
        {
            this.ServiceLoader.EnableComplete += this.ServiceLoader_EnableComplete;
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            this._room = this.ServiceLoader.Get<RoomService>();
        }

        public void ChangeFace(Smiley newSmiley)
        {
            this.Events.Raise(new ChangeFaceSendEvent(newSmiley));
        }

        public void MoveToLocation(int x, int y)
        {
            this.Events.Raise(new MoveSendEvent(x, y, 0, 0, 0, 0, 0, 0, this._room.GravityMultiplier));
        }

        public void GetCrown()
        {
            this.Events.Raise(new GetCrownSendEvent());
        }

        public void GetCoin(int count, int x, int y)
        {
            this.Events.Raise(new CoinSendEvent(count, x, y));
        }

        public void TouchPlayer(int userId, Potion reason)
        {
            this.Events.Raise(new TouchUserSendEvent(userId, reason));
        }

        public void TouchCake(int x, int y)
        {
            this.Events.Raise(new TouchCakeSendEvent(x, y));
        }

        public void TouchDiamond(int x, int y)
        {
            this.Events.Raise(new TouchDiamondSendEvent(x, y));
        }

        public void Checkpoint(int x, int y)
        {
            this.Events.Raise(new CheckpointSendEvent(x, y));
        }

        public void CompleteLevel()
        {
            this.Events.Raise(new CompleteLevelSendEvent());
        }

        public void Die()
        {
            this.Events.Raise(new DeathSendEvent());
        }

        public void AutoSay(AutoText text)
        {
            this.Events.Raise(new AutoSaySendEvent(text));
        }
    }
}