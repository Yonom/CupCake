using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CupCake.Client.Settings;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for Profiles.xaml
    /// </summary>
    public partial class EditListWindow
    {
        private readonly IList _collection;
        private readonly EditListType _type;

        public EditListWindow(EditListType type)
        {
            _type = type;

            InitializeComponent();
            HasSelection = false;

            switch (type)
            {
                case EditListType.Profile:
                    _collection = SettingsManager.Settings.Profiles;
                    Title = "Manage Profiles";
                    break;
                case EditListType.Account:
                    _collection = SettingsManager.Settings.Accounts;
                    Title = "Manage Accounts";
                    break;
                case EditListType.Database:
                    _collection = SettingsManager.Settings.Databases;
                    Title = "Manage Databases";
                    AddButton.ContextMenu = Resources["AdvancedContextMenu"] as ContextMenu;
                    break;
            }

            ItemsListBox.SelectionChanged += ItemsListBox_SelectionChanged;
            Closing += EditListWindow_Closing;

            RefreshList();
        }

        private bool HasSelection
        {
            set
            {
                EditButton.IsEnabled = value;
                RemoveButton.IsEnabled = value;
            }
        }

        private void EditListWindow_Closing(object sender, CancelEventArgs e)
        {
            SettingsManager.Save();
        }

        private void ItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HasSelection = ItemsListBox.SelectedItem != null;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            IConfig p = null;
            switch (_type)
            {
                case EditListType.Profile:
                    p = Profile.NewEmpty();
                    break;
                case EditListType.Account:
                    p = Account.NewEmpty();
                    break;
                case EditListType.Database:
                    p = Database.NewEmpty();
                    break;
            }

            if (EditItem(p, true) == true)
            {
                AddItem(p);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var uiP = (TextBlock) ItemsListBox.SelectedItem;
            var p = (IConfig) uiP.Tag;
            if (EditItem(p, false) == true)
            {
                RefreshList();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var uiP = (TextBlock) ItemsListBox.SelectedItem;
            var p = (IConfig) uiP.Tag;
            RemoveItem(p);
        }

        private void AddItem(IConfig p)
        {
            _collection.Add(p);
            RefreshList();
        }

        private void RemoveItem(IConfig p)
        {
            _collection.Remove(p);
            RefreshList();
        }

        private void RefreshList()
        {
            ItemsListBox.Items.Clear();

            var textBlockStyle = FindResource("TextBlockStyle") as Style;
            var menuItem = FindResource("StandardMenuItem") as Style;
            foreach (IConfig p in ((IEnumerable<IConfig>) _collection).OrderBy(k => k.Id))
            {
                var textBlock = new TextBlock
                {
                    Style = textBlockStyle,
                    Text = p.Name.GetVisualName(),
                    Tag = p
                };

                if (_type == EditListType.Profile)
                {
                    var localProfile = (Profile) p;
                    var removeFromListMenuItem = new MenuItem
                    {
                        Header = "Open Folder In File Explorer",
                        Style = menuItem
                    };

                    removeFromListMenuItem.Click += (sender, args) =>
                    {
                        string dir = localProfile.Folder;
                        if (!dir.EndsWith("\\"))
                            dir += "\\";

                        Process.Start(dir);
                    };

                    var textBlockContextMenu = new ContextMenu();
                    textBlockContextMenu.Items.Add(removeFromListMenuItem);

                    textBlock.ContextMenu = textBlockContextMenu;
                }

                ItemsListBox.Items.Add(textBlock);
            }
        }

        private bool? EditItem(IConfig item, bool isNew, bool isAdvanced = false)
        {
            switch (_type)
            {
                case EditListType.Profile:
                    var profile = (Profile) item;
                    var editProfile = new EditProfileWindow(profile, isNew) {Owner = this};
                    return editProfile.ShowDialog();

                case EditListType.Account:
                    var account = (Account) item;
                    var editAccount = new EditAccountWindow(account, isNew) {Owner = this};
                    return editAccount.ShowDialog();

                case EditListType.Database:

                    var database = (Database) item;

                    Window editDatabase;
                    if (!isAdvanced && isNew)
                        editDatabase = new EditLocalDatabaseWindow(database) {Owner = this};
                    else
                        editDatabase = new EditDatabaseWindow(database, isNew) {Owner = this};

                    return editDatabase.ShowDialog();

                default:
                    return null;
            }
        }

        private void AdvancedMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_type == EditListType.Database)
            {
                Database p = Database.NewEmpty();

                if (EditItem(p, true, true) == true)
                {
                    AddItem(p);
                }
            }
        }
    }
}