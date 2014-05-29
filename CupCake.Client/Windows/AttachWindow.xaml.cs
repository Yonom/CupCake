using System.Windows;

namespace CupCake.Client.Windows
{
    /// <summary>
    /// Interaction logic for AttachWindow.xaml
    /// </summary>
    public partial class AttachWindow
    {
        public AttachWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
