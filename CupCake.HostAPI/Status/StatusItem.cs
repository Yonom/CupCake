using System;

namespace CupCake.HostAPI.Status
{
    public class StatusItem
    {
        private string _name;
        private string _value;

        public StatusItem(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnChanged();
                }
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                if (_name != value)
                {
                    _value = value;
                    OnChanged();
                }
            }
        }

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}