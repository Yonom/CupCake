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
            ServiceLoader.EnableComplete += ServiceLoader_EnableComplete;
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            _room = ServiceLoader.Get<RoomService>();
        }

        public void ChangeFace(Smiley newSmiley)
        {
            Events.Raise(new ChangeFaceSendEvent(newSmiley));
        }

        public void MoveToLocation(int x, int y)
        {
            Events.Raise(new MoveSendEvent(x, y, 0, 0, 0, 0, 0, 0, _room.GravityMultiplier, false));
        }

        public void GetCrown()
        {
            Events.Raise(new GetCrownSendEvent());
        }

        public void GetCoin(int count, int x, int y)
        {
            Events.Raise(new CoinSendEvent(count, x, y));
        }

        public void TouchPlayer(int userId, Potion reason)
        {
            Events.Raise(new TouchUserSendEvent(userId, reason));
        }

        public void TouchCake(int x, int y)
        {
            Events.Raise(new TouchCakeSendEvent(x, y));
        }

        public void TouchDiamond(int x, int y)
        {
            Events.Raise(new TouchDiamondSendEvent(x, y));
        }

        public void Checkpoint(int x, int y)
        {
            Events.Raise(new CheckpointSendEvent(x, y));
        }

        public void CompleteLevel()
        {
            Events.Raise(new CompleteLevelSendEvent());
        }

        public void Die()
        {
            Events.Raise(new DeathSendEvent());
        }

        public void AutoSay(AutoText text)
        {
            Events.Raise(new AutoSaySendEvent(text));
        }

        public void WootUp()
        {
            Events.Raise(new WootUpSendEvent());
        }

        public void GodMode(bool enabled)
        {
            if (_room.AccessRight < AccessRight.Edit)
                throw new InvalidOperationException("You need edit rights to enter god mode.");

            Events.Raise(new GodModeSendEvent(enabled));
        }

        public void ModMode()
        {
            Events.Raise(new ModModeSendEvent());
        }

        public void Purple(bool show)
        {
            Events.Raise(new ShowPurpleSendEvent(show));
        }
    }
}