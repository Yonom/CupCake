using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using CupCake.Client.Settings;
using CupCake.Client.UserControls;
using CupCake.Protocol;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private int _connectionCount;
        private ServerListener _listener;

        public MainWindow()
        {
            this.InitializeComponent();
            this.HasConnectionSelected = false;

            ICollectionView view = CollectionViewSource.GetDefaultView(this.ConnectionsTabControl.Items);
            view.CollectionChanged += this.view_CollectionChanged;

            this.ContentRendered += this.MainWindow_ContentRendered;

            Application.Current.Exit += this.App_Exit;
        }

        public RecentWorld IncomingSettings { get; set; }

        private bool HasConnectionSelected
        {
            set
            {
                this.ClearLogMenuItem.IsEnabled = value;
                this.CloseConnectionMenuItem.IsEnabled = value;

                // Hide the ConnectionsTabControl if empty
                this.ConnectionsTabControl.Visibility = value
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private int ConnectionCount
        {
            get { return this._connectionCount; }
            set
            {
                this._connectionCount = value;

                this.ActiveConnectionsRun.Text = Convert.ToString(value);
            }
        }

        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            this.RefreshRecent();
            this.StartServer();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            SettingsManager.Save();
        }

        private void view_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Close the connection of a Tab if it is closed
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                this.CloseConnection((TabItem)e.OldItems[0]);
            }

            this.HasConnectionSelected = this.ConnectionsTabControl.Items.Count > 0;
        }

        private void ButtonAttach_Click(object sender, RoutedEventArgs e)
        {
            this.ShowAttach();
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonNew_OnClick(object sender, RoutedEventArgs e)
        {
            this.NewConnection(false);
        }

        private void NewConnectionMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.NewConnection(false);
        }

        private void NewConnectionVisibleConsoleMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.NewConnection(true);
        }

        private void NewConnectionDebugEnabledMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.NewConnection(true, true);
        }

        private void AttachMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.ShowAttach();
        }

        private void CloseConnectionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            object tab = this.ConnectionsTabControl.SelectedItem;
            if (tab != null)
            {
                this.CloseConnection((TabItem)tab);
            }
        }

        private void ClearLogMenuItem_Click(object sender, RoutedEventArgs e)
        {
            object tab = this.ConnectionsTabControl.SelectedItem;
            if (tab != null)
            {
                this.ClearLog((TabItem)tab);
            }
        }

        private void ProfilesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowList(EditListType.Profile);
        }

        private void DatabasesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowList(EditListType.Database);
        }

        private void AccountsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowList(EditListType.Account);
        }

        private void GithubMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Yonom/CupCake");
        }

        private void AboutMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowAbout();
        }

        private void OpenProfilesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(SettingsManager.ProfilesPath);
        }

        private void OpenDatabasesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(SettingsManager.DatabasesPath);
        }

        private void RefreshRecent()
        {
            this.RecentStackPanel.Children.Clear();

            var recentButton = this.FindResource("RecentButton") as Style;
            var menuItem = this.FindResource("StandardMenuItem") as Style;
            foreach (RecentWorld recent in SettingsManager.Settings.RecentWorlds.OrderByDescending(v => v.Id))
            {
                RecentWorld localRecent = recent;

                // Rename
                var renameMenuItem = new MenuItem
                {
                    Header = "Rename",
                    Style = menuItem
                };

                renameMenuItem.Click += (sender, args) => this.ShowRename(localRecent);

                // Remove
                var removeFromListMenuItem = new MenuItem
                {
                    Header = "Remove From List",
                    Style = menuItem
                };

                removeFromListMenuItem.Click += (sender, args) => this.RemoveRecent(localRecent);

                var buttonContextMenu = new ContextMenu();
                buttonContextMenu.Items.Add(renameMenuItem);
                buttonContextMenu.Items.Add(removeFromListMenuItem);

                var button = new Button
                {
                    Style = recentButton,
                    Content = new TextBlock(new Run((recent.Name ?? recent.WorldId).GetVisualName())),
                    ContextMenu = buttonContextMenu
                };

                button.Click += (sender, args) =>
                {
                    this.SetIncoming(localRecent);
                    this.NewConnection(false);
                };

                this.RecentStackPanel.Children.Add(button);
            }
        }

        private void AddRecent(RecentWorld recent)
        {
            // If there is an unnamed connection with the same world id, remove it to avoid duplicates
            RecentWorld old = SettingsManager.Settings.RecentWorlds.FirstOrDefault(
                v => (String.IsNullOrWhiteSpace(v.Name) && v.WorldId == recent.WorldId));
            if (old != null)
            {
                SettingsManager.Settings.RecentWorlds.Remove(old);
            }

            SettingsManager.Settings.RecentWorlds.Add(recent);
        }

        private void RemoveRecent(RecentWorld recent)
        {
            SettingsManager.Settings.RecentWorlds.Remove(recent);
            SettingsManager.Save();
            this.RefreshRecent();
        }

        private void SetIncoming(RecentWorld recent)
        {
            this.IncomingSettings = recent;
        }

        private void StartServer()
        {
            try
            {
                // Start the server
                this._listener = new ServerListener(IPAddress.Loopback, ServerListener.ServerPort, this.HandleIncoming);
                this._listener.DebugRequest += this._listener_DebugRequest;
            }
            catch (SocketException ex)
            {
                MessageBoxHelper.Show(this, "Unable to open TCP Listener",
                    "Could not listen on port 4577. " + ex.Message);
                this.Close();
            }
        }

        private void _listener_DebugRequest(Stream obj)
        {
            var str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var msgBytes = Encoding.Unicode.GetBytes(str);
                obj.Write(msgBytes, 0, msgBytes.Length);
        }

        private void HandleIncoming(ClientHandle handle)
        {
            Dispatch.Invoke(() =>
            {
                this.ConnectionCount++;

                handle.ReceiveClose += () => Dispatch.Invoke(() => { this.ConnectionCount--; });

                handle.ReceiveRequestData += data => Dispatch.Invoke(() =>
                {
                    // Use requested settings
                    RecentWorld recent = this.IncomingSettings;
                    this.IncomingSettings = null;

                    bool isNew = false;
                    if (recent == null)
                    {
                        isNew = true;

                        recent = SettingsManager.Settings.RecentWorlds.Count == 0
                            ? new RecentWorld()
                            : SettingsManager.Settings.RecentWorlds.OrderByDescending(r => r.Id).First().Clone();
                    }

                    if (new NewConnectionWindow(handle, recent, data.IsDebug) {Owner = this}.ShowDialog() == true)
                    {
                        if (isNew)
                            this.AddRecent(recent);
                    }
                    else
                    {
                        handle.DoSendClose();
                    }

                    recent.UpdateId();
                    SettingsManager.Save();
                    this.RefreshRecent();
                });

                var tabItem = new TabItem
                {
                    Header = String.Empty.GetVisualName(),
                    Content = new ConnectionUserControl(handle)
                };

                this.ConnectionsTabControl.Items.Add(tabItem);
                tabItem.IsSelected = true;
            });
        }

        private void NewConnection(bool showConsole, bool isDebug = false)
        {
            var p = new Process
            {
                StartInfo =
                {
                    FileName = "CupCake.Server.exe"
                }
            };

            if (!showConsole)
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
            }

            if (isDebug)
            {
                p.StartInfo.Arguments = "--debug";
            }

            p.Start();
        }

        private void ShowAttach()
        {
            var attach = new AttachWindow {Owner = this};
            if (attach.ShowDialog() == true)
            {
                try
                {
                    IPEndPoint endPoint = IPHelper.Parse(attach.AddressTextBox.Text, 4577);

                    try
                    {
                        this._listener.Connect(endPoint, handle =>
                        {
                            this.HandleIncoming(handle);
                            handle.DoSendAuthentication(attach.PinPasswordBox.Password);
                        });
                    }
                    catch (SocketException ex)
                    {
                        MessageBoxHelper.Show(this, "Error", "Error while connecting: " + ex.Message);
                    }
                }
                catch (ArgumentException)
                {
                    MessageBoxHelper.Show(this, "Error", "Address can't be empty.");
                }
                catch (FormatException)
                {
                    MessageBoxHelper.Show(this, "Error", "Invalid Address.");
                }
            }
        }

        private void CloseConnection(TabItem tab)
        {
            var userControl = (ConnectionUserControl)tab.Content;
            userControl.Close();
        }

        private void ClearLog(TabItem tab)
        {
            var userControl = (ConnectionUserControl)tab.Content;
            userControl.Clear();
        }

        private void ShowAbout()
        {
            var about = new AboutWindow {Owner = this};
            about.ShowDialog();
        }

        private void ShowList(EditListType type)
        {
            var profiles = new EditListWindow(type) {Owner = this};
            profiles.ShowDialog();
        }

        private void ShowRename(RecentWorld recent)
        {
            var rename = new RenameWindow {Owner = this, NameTextBox = {Text = recent.Name}};
            if (rename.ShowDialog() == true)
            {
                recent.Name = rename.NameTextBox.Text;
            }

            SettingsManager.Save();
            this.RefreshRecent();
        }
    }
}