using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CupCake.Client.Settings;
using CupCake.Protocol;

namespace CupCake.Client.Windows
{
    /// <summary>
    /// Interaction logic for EditAccountWindow.xaml
    /// </summary>
    public partial class EditAccountWindow
    {
        private bool _passwordChanged;
        private readonly Account _account;

        public EditAccountWindow(Account account, bool isNew)
        {
            this._account = account;

            InitializeComponent();

            this.Title = isNew
                ? "New Account"
                : "Edit Account";

            this.Closing += EditAccountWindow_Closing;

            this.TypeComboBox.SelectedIndex = (int)account.Type;
            this.EmailTextBox.Text = account.Email;
            this.PasswordBox.Password = isNew
                ? String.Empty
                : new string('*', 12);
        }

        void EditAccountWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this._passwordChanged)
            {
                this._account.Password = this.PasswordBox.SecurePassword.EncryptString();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void PasswordBox_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            this._passwordChanged = true;
        }

        private void EmailTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this._account.Email = EmailTextBox.Text;
        }

        private void TypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._account.Type = (AccountType)this.TypeComboBox.SelectedIndex;
        }
    }
}
