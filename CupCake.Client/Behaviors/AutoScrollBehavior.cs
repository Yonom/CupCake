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

            _scrollViewer = AssociatedObject;
            _scrollViewer.ScrollChanged += _scrollViewer_ScrollChanged;
        }

        private void _scrollViewer_ScrollChanged(object sender, EventArgs e)
        {
            if ((int) _scrollViewer.VerticalOffset == (int) _scrollViewer.ScrollableHeight)
                _scrollViewer.ScrollToEnd();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (_scrollViewer != null)
                _scrollViewer.ScrollChanged -= _scrollViewer_ScrollChanged;
        }
    }
}