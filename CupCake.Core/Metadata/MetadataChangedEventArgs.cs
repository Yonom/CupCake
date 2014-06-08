using System;

namespace CupCake.Core.Metadata
{
    public class MetadataChangedEventArgs : EventArgs
    {
        public string Key { get; private set; }
        public object NewValue { get; private set; }

        public MetadataChangedEventArgs(string key, object newValue)
        {
            this.Key = key;
            this.NewValue = newValue;
        }
    }
}