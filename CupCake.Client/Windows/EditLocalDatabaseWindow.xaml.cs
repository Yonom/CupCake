using System;
using System.Windows;
using System.Windows.Controls;
using CupCake.Client.Settings;
using CupCake.Protocol;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for EditDatabaseWindow.xaml
    /// </summary>
    public partial class EditLocalDatabaseWindow
    {
        private readonly Database _database;
        private string _lastName;

        public EditLocalDatabaseWindow(Database database)
        {
            this.InitializeComponent();

            this.Title = "New Local Database";

            this._database = database;
            this.NameTextBox.Text = database.Name;
            this.FolderTextBox.Text = SettingsManager.DatabasesPath + "\\";
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
            string filePath = this.FolderTextBox.Text;
            string fileName = this.NameTextBox.Text + ".db";

            if (filePath.EndsWith("\\"))
                this.FolderTextBox.Text += fileName;
            else if (!String.IsNullOrEmpty(this._lastName) && filePath.EndsWith(this._lastName))
                this.FolderTextBox.Text = filePath.Substring(0, filePath.Length - this._lastName.Length) + fileName;


            this._lastName = fileName;
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
                string folder = dlg.FileName;

                this.FolderTextBox.Text = folder;
            }
        }
    }
}