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
        private bool _passwordChanged;

        public EditAccountWindow(Account account, bool isNew)
        {
            _account = account;

            InitializeComponent();

            Title = isNew
                ? "New Account"
                : "Edit Account";

            NameTextBox.Text = account.Name;
            TypeComboBox.SelectedIndex = (int) account.Type;
            EmailTextBox.Text = account.Email;
            PasswordBox.Password = isNew
                ? String.Empty
                : new string('*', 12);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _account.Name = NameTextBox.Text;
            _account.Type = (AccountType) TypeComboBox.SelectedIndex;
            _account.Email = EmailTextBox.Text;
            if (_passwordChanged)
            {
                _account.Password = PasswordBox.SecurePassword.EncryptString();
            }
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void PreviewPasswordBox_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            _passwordChanged = true;
        }
    }
}