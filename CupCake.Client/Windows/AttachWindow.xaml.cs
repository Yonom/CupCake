using System.Windows;
using CupCake.Client.Settings;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for AttachWindow.xaml
    /// </summary>
    public partial class AttachWindow
    {
        public AttachWindow()
        {
            InitializeComponent();

            AddressTextBox.Text = SettingsManager.Settings.LastAttachAddress;
            PinPasswordBox.Password = SettingsManager.Settings.LastAttachPin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsManager.Settings.LastAttachAddress = AddressTextBox.Text;
            SettingsManager.Settings.LastAttachPin = PinPasswordBox.Password;
            SettingsManager.Save();

            DialogResult = true;
        }
    }
}