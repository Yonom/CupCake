using System;
using CupCake.Core;
using CupCake.Messages.Send;
using CupCake.Messages.User;
using CupCake.Room;

namespace CupCake.Actions
{
    public sealed class ActionService : CupCakeService
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
            this.Events.Raise(new MoveSendEvent(x, y, 0, 0, 0, 0, 0, 0, this._room.GravityMultiplier, false));
        }

        public void GetCrown()
        {
            this.Events.Raise(new GetCrownSendEvent());
        }

        public void GetCoin(int coins, int blueCoins, int x, int y)
        {
            this.Events.Raise(new CoinSendEvent(coins, blueCoins, x, y));
        }

        [Obsolete("Use the other constructor with blueCoins parameter", true)]
        public void GetCoin(int count, int x, int y)
        {
            this.Events.Raise(new CoinSendEvent(count, 0, x, y));
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

        public void WootUp()
        {
            this.Events.Raise(new WootUpSendEvent());
        }

        public void GodMode(bool enabled)
        {
            if (this._room.AccessRight < AccessRight.Edit)
                throw new InvalidOperationException("You need edit rights to enter god mode.");

            this.Events.Raise(new GodModeSendEvent(enabled));
        }

        public void GuardianMode(bool enabled)
        {
            this.Events.Raise(new GuardianModeSendEvent(enabled));
        }

        public void ModMode()
        {
            this.Events.Raise(new ModModeSendEvent());
        }

        public void Purple(bool show)
        {
            this.Events.Raise(new ShowPurpleSendEvent(show));
        }
    }
}