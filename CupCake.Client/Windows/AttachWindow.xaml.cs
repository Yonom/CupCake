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
            this.InitializeComponent();

            this.AddressTextBox.Text = SettingsManager.Settings.LastAttachAddress;
            this.PinPasswordBox.Password = SettingsManager.Settings.LastAttachPin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsManager.Settings.LastAttachAddress = this.AddressTextBox.Text;
            SettingsManager.Settings.LastAttachPin = this.PinPasswordBox.Password;
            SettingsManager.Save();

            this.DialogResult = true;
        }
    }
}