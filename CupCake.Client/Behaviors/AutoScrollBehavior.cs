using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CupCake.Client.Behaviors
{
    public class AutoScrollBehavior : Behavior<ScrollViewer>
    {
        private ScrollViewer _scrollViewer;

        protected override void OnAttached()
        {
            base.OnAttached();

            this._scrollViewer = this.AssociatedObject;
            this._scrollViewer.ScrollChanged += this._scrollViewer_ScrollChanged;
        }

        private void _scrollViewer_ScrollChanged(object sender, EventArgs e)
        {
            if ((int)this._scrollViewer.VerticalOffset == (int)this._scrollViewer.ScrollableHeight)
                this._scrollViewer.ScrollToEnd();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this._scrollViewer != null)
                this._scrollViewer.ScrollChanged -= this._scrollViewer_ScrollChanged;
        }
    }
}
