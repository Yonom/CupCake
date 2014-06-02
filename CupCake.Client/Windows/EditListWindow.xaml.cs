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
            this._type = type;

            this.InitializeComponent();
            this.HasSelection = false;

            switch (type)
            {
                case EditListType.Profile:
                    this._collection = SettingsManager.Settings.Profiles;
                    this.Title = "Manage Profiles";
                    break;
                case EditListType.Account:
                    this._collection = SettingsManager.Settings.Accounts;
                    this.Title = "Manage Accounts";
                    break;
                case EditListType.Database:
                    this._collection = SettingsManager.Settings.Databases;
                    this.Title = "Manage Databases";
                    this.AddButton.ContextMenu = this.Resources["AdvancedContextMenu"] as ContextMenu;
                    break;
            }

            this.ItemsListBox.SelectionChanged += this.ItemsListBox_SelectionChanged;
            this.Closing += this.EditListWindow_Closing;

            this.RefreshList();
        }

        private bool HasSelection
        {
            set
            {
                this.EditButton.IsEnabled = value;
                this.RemoveButton.IsEnabled = value;
            }
        }

        private void EditListWindow_Closing(object sender, CancelEventArgs e)
        {
            SettingsManager.Save();
        }

        private void ItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.HasSelection = this.ItemsListBox.SelectedItem != null;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            IConfig p = null;
            switch (this._type)
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

            if (this.EditItem(p, true) == true)
            {
                this.AddItem(p);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var uiP = (TextBlock)this.ItemsListBox.SelectedItem;
            var p = (IConfig)uiP.Tag;
            if (this.EditItem(p, false) == true)
            {
                this.RefreshList();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var uiP = (TextBlock)this.ItemsListBox.SelectedItem;
            var p = (IConfig)uiP.Tag;
            this.RemoveItem(p);
        }

        private void AddItem(IConfig p)
        {
            this._collection.Add(p);
            this.RefreshList();
        }

        private void RemoveItem(IConfig p)
        {
            this._collection.Remove(p);
            this.RefreshList();
        }

        private void RefreshList()
        {
            this.ItemsListBox.Items.Clear();

            var textBlockStyle = this.FindResource("TextBlockStyle") as Style;
            var menuItem = this.FindResource("StandardMenuItem") as Style;
            foreach (IConfig p in ((IEnumerable<IConfig>)this._collection).OrderBy(k => k.Id))
            {
                var textBlock = new TextBlock
                {
                    Style = textBlockStyle,
                    Text = p.Name.GetVisualName(),
                    Tag = p
                };

                if (this._type == EditListType.Profile)
                {
                    var localProfile = (Profile)p;
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

                this.ItemsListBox.Items.Add(textBlock);
            }
        }

        private bool? EditItem(IConfig item, bool isNew, bool isAdvanced = false)
        {
            switch (this._type)
            {
                case EditListType.Profile:
                    var profile = (Profile)item;
                    var editProfile = new EditProfileWindow(profile, isNew) {Owner = this};
                    return editProfile.ShowDialog();

                case EditListType.Account:
                    var account = (Account)item;
                    var editAccount = new EditAccountWindow(account, isNew) {Owner = this};
                    return editAccount.ShowDialog();

                case EditListType.Database:

                    var database = (Database)item;

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
            if (this._type == EditListType.Database)
            {
                Database p = Database.NewEmpty();

                if (this.EditItem(p, true, true) == true)
                {
                    this.AddItem(p);
                }
            }
        }
    }
}