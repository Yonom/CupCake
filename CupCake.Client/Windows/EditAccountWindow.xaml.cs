using System;
using System.Windows;
using System.Windows.Input;
using CupCake.Client.Settings;
using CupCake.Protocol;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for EditAccountWindow.xaml
    /// </summary>
    public partial class EditAccountWindow
    {
        private readonly Account _account;

        public EditAccountWindow(Account account, bool isNew)
        {
            this._account = account;

            this.InitializeComponent();

            this.Title = isNew
                ? "New Account"
                : "Edit Account";

            this.NameTextBox.Text = account.Name;
            this.EmailTextBox.Text = account.Email;
            this.PasswordBox.Password = isNew
                ? String.Empty
                : new string('*', 12);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this._account.Name = this.NameTextBox.Text;
            this._account.Email = this.EmailTextBox.Text;
            if (this.PasswordBox.Password != new string('*', 12))
            {
                this._account.Password = this.PasswordBox.SecurePassword.EncryptString();
            }
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}