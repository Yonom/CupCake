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
            get { return new ReadOnlyCollection<StatusItem>(_statuses); }
        }

        protected override void Enable()
        {
            SyntaxProvider = new BasicStatusSyntaxProvider();
        }

        public void Add(StatusItem item)
        {
            item.Changed += item_Changed;

            lock (_statuses)
            {
                _statuses.Add(item);
            }

            UpdateStatus();
        }

        public bool Remove(StatusItem item)
        {
            item.Changed -= item_Changed;

            bool result;
            lock (_statuses)
            {
                result = _statuses.Remove(item);
            }

            UpdateStatus();
            return result;
        }

        public void UpdateStatus()
        {
            StatusItem[] statusItems;
            lock (_statuses)
            {
                statusItems = _statuses.ToArray();
            }

            Events.Raise(new ChangeStatusEvent(SyntaxProvider.Parse(statusItems)));
        }

        private void item_Changed(object sender, EventArgs e)
        {
            UpdateStatus();
        }
    }
}