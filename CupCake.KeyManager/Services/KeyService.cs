using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CupCake.Core.Events;
using CupCake.Core.Services;
using CupCake.EE.Blocks;
using CupCake.EE.Events.Receive;

namespace CupCake.KeyManager.Services
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
            foreach (var k in keys)
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
    }
}
