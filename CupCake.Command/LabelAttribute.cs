using System;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LabelAttribute : Attribute
    {
        private readonly string[] _labels;

        public LabelAttribute(params string[] labels)
        {
            this._labels = labels;
        }

        public string[] Labels
        {
            get { return this._labels; }
        }
    }
}