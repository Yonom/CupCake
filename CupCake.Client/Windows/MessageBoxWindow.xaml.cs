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
            InitializeComponent();

            Title = title;
            BodyTextBlock.Text = body;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}