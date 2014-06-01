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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CupCake.Client.Windows
{
    /// <summary>
    /// Interaction logic for EditDatabaseWindow.xaml
    /// </summary>
    public partial class EditLocalDatabaseWindow
    {
        private readonly Database _database;
        private string _lastName;

        public EditLocalDatabaseWindow(Database database)
        {
            InitializeComponent();

            this.Title = "New Local Database";

            this._database = database;
            this.NameTextBox.Text = database.Name;
            this.FolderTextBox.Text = SettingsManager.DatabasesPath + "\\"; ;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this._database.Type = DatabaseType.SQLite;
            this._database.Name = this.NameTextBox.Text;
            this._database.ConnectionString = String.Format(Database.SQLiteFormat, this.FolderTextBox.Text);

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void NameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var filePath = this.FolderTextBox.Text;
            var fileName = this.NameTextBox.Text + ".db";

            if (filePath.EndsWith("\\"))
                this.FolderTextBox.Text += fileName;
            else if (!String.IsNullOrEmpty(_lastName) && filePath.EndsWith(_lastName))
                this.FolderTextBox.Text = filePath.Substring(0, filePath.Length - _lastName.Length) + fileName;


            _lastName = fileName;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog
            {
                Title = "Choose database folder",
                IsFolderPicker = true,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = SettingsManager.DatabasesPath,
                EnsurePathExists = false,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;

                this.FolderTextBox.Text = folder;
            }
        }
    }
}
