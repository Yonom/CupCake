using System;
using System.Linq;
using System.Collections.Generic;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;
using CupCake.Messages;

namespace CupCake.Keys
{
    public sealed class KeyService : CupCakeService
    {
        public bool RedKey { get; private set; }
        public bool GreenKey { get; private set; }
        public bool BlueKey { get; private set; }
        public bool CyanKey { get; private set; }
        public bool MagentaKey { get; private set; }
        public bool YellowKey { get; private set; }
        public bool TimeDoor { get; private set; }

        protected override void Enable()
        {
            this.Events.Bind<HideKeyReceiveEvent>(this.OnHideKey, EventPriority.High);
            this.Events.Bind<ShowKeyReceiveEvent>(this.OnShowKey, EventPriority.High);
        }

        private void OnHideKey(object sender, HideKeyReceiveEvent e)
        {
            this.ChangeKey(e.Keys.Select<KeyTrigger, Key>(t => t.Key), false);
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

                    case Key.Cyan:
                        this.CyanKey = enabled;
                        break;

                    case Key.Magenta:
                        this.MagentaKey = enabled;
                        break;

                    case Key.Yellow:
                        this.YellowKey = enabled;
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

                case Key.Cyan:
                    this.Events.Raise(new PressCyanKeySendEvent());
                    break;

                case Key.Magenta:
                    this.Events.Raise(new PressMagentaKeySendEvent());
                    break;
                
                case Key.Yellow:
                    this.Events.Raise(new PressYellowKeySendEvent());
                    break;

                default:
                    throw new NotSupportedException("The given Key could not be sent.");
            }
        }
    }
}