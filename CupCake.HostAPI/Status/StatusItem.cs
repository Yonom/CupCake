using System;

namespace CupCake.HostAPI.Status
{
    public class StatusItem
    {
        private string _name;
        private string _value;

        public StatusItem(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name
        {
            get { return this._name; }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.OnChanged();
                }
            }
        }

        public string Value
        {
            get { return this._value; }
            set
            {
                if (this._name != value)
                {
                    this._value = value;
                    this.OnChanged();
                }
            }
        }

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = this.Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}