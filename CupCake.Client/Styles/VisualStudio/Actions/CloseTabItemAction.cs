using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Selen.Wpf.SystemStyles.Actions
{
    public class CloseTabItemAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty TabControlProperty =
            DependencyProperty.Register("TabControl", typeof(TabControl), typeof(CloseTabItemAction),
                new PropertyMetadata(default(TabControl)));

        public static readonly DependencyProperty TabItemProperty =
            DependencyProperty.Register("TabItem", typeof(TabItem), typeof(CloseTabItemAction),
                new PropertyMetadata(default(TabItem)));

        public TabControl TabControl
        {
            get { return (TabControl)this.GetValue(TabControlProperty); }
            set { this.SetValue(TabControlProperty, value); }
        }

        public TabItem TabItem
        {
            get { return (TabItem)this.GetValue(TabItemProperty); }
            set { this.SetValue(TabItemProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            this.TabControl.Items.Remove(this.TabItem);
        }
    }
}