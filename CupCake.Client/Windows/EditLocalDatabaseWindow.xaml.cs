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
            InitializeComponent();

            Title = "New Local Database";

            _database = database;
            NameTextBox.Text = database.Name;
            FolderTextBox.Text = SettingsManager.DatabasesPath + "\\";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _database.Type = DatabaseType.SQLite;
            _database.Name = NameTextBox.Text;
            _database.ConnectionString = String.Format(Database.SQLiteFormat, FolderTextBox.Text);

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void NameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string filePath = FolderTextBox.Text;
            string fileName = NameTextBox.Text + ".db";

            if (filePath.EndsWith("\\"))
                FolderTextBox.Text += fileName;
            else if (!String.IsNullOrEmpty(_lastName) && filePath.EndsWith(_lastName))
                FolderTextBox.Text = filePath.Substring(0, filePath.Length - _lastName.Length) + fileName;


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
                string folder = dlg.FileName;

                FolderTextBox.Text = folder;
            }
        }
    }
}