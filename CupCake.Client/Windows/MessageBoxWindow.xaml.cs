using System.Windows;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow
    {
        public MessageBoxWindow(string title, string body)
        {
            this.InitializeComponent();

            this.Title = title;
            this.BodyTextBlock.Text = body;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}