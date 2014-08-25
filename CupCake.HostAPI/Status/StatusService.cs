using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CupCake.Core;

namespace CupCake.HostAPI.Status
{
    public sealed class StatusService : CupCakeService
    {
        private readonly List<StatusItem> _statuses = new List<StatusItem>();

        public IStatusSyntaxProvider SyntaxProvider { get; set; }

        public ReadOnlyCollection<StatusItem> Statuses
        {
            get { return new ReadOnlyCollection<StatusItem>(this._statuses); }
        }

        protected override void Enable()
        {
            this.SyntaxProvider = new CupCakeStatusSyntaxProvider();
        }

        public void Add(StatusItem item)
        {
            item.Changed += this.item_Changed;

            lock (this._statuses)
            {
                this._statuses.Add(item);
            }

            this.UpdateStatus();
        }

        public bool Remove(StatusItem item)
        {
            item.Changed -= this.item_Changed;

            bool result;
            lock (this._statuses)
            {
                result = this._statuses.Remove(item);
            }

            this.UpdateStatus();
            return result;
        }

        public void UpdateStatus()
        {
            StatusItem[] statusItems;
            lock (this._statuses)
            {
                statusItems = this._statuses.ToArray();
            }

            this.Events.Raise(new ChangeStatusEvent(this.SyntaxProvider.Parse(statusItems)));
        }

        private void item_Changed(object sender, EventArgs e)
        {
            this.UpdateStatus();
        }
    }
}