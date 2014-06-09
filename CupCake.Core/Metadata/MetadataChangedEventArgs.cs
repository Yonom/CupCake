using System;

namespace CupCake.Core.Metadata
{
    public class MetadataChangedEventArgs : EventArgs
    {
        public string Key { get; private set; }
        public object OldValue { get; private set; }
        public object NewValue { get; private set; }

        public MetadataChangedEventArgs(string key, object oldValue, object newValue)
        {
            this.Key = key;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}