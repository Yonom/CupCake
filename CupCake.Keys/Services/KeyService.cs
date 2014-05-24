using System;
using System.Collections.Generic;
using CupCake.Core.Events;
using CupCake.Core.Services;
using CupCake.Messages.Blocks;
using CupCake.Messages.Events.Receive;
using CupCake.Messages.Events.Send;

namespace CupCake.Keys.Services
{
    public class KeyService : CupCakeService
    {
        public bool RedKey { get; private set; }
        public bool GreenKey { get; private set; }
        public bool BlueKey { get; private set; }
        public bool TimeDoor { get; private set; }

        protected override void Enable()
        {
            this.Events.Bind<HideKeyReceiveEvent>(this.OnHideKey, EventPriority.High);
            this.Events.Bind<ShowKeyReceiveEvent>(this.OnShowKey, EventPriority.High);
        }

        private void OnHideKey(object sender, HideKeyReceiveEvent e)
        {
            this.ChangeKey(e.Keys, false);
        }

        private void OnShowKey(object sender, ShowKeyReceiveEvent e)
        {
            this.ChangeKey(e.Keys, true);
        }

        private void ChangeKey(IEnumerable<Key> keys, bool enabled)
        {
            foreach (Key k in keys)
            {
                switch (k)
                {
                    case Key.Blue:
                        this.BlueKey = enabled;
                        break;

                    case Key.Green:
                        this.GreenKey = enabled;
                        break;

                    case Key.Red:
                        this.RedKey = enabled;
                        break;

                    case Key.TimeDoor:
                        this.TimeDoor = enabled;
                        break;
                }
            }
        }

        public void PressKey(Key key)
        {
            switch (key)
            {
                case Key.Blue:
                    this.Events.Raise(new PressBlueKeySendEvent());
                    break;

                case Key.Green:
                    this.Events.Raise(new PressGreenKeySendEvent());
                    break;

                case Key.Red:
                    this.Events.Raise(new PressRedKeySendEvent());
                    break;

                default:
                    throw new NotSupportedException("The given Key could not be sent.");
            }
        }
    }
}