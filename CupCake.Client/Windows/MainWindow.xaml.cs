using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Deployment.Application;
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

            // Disable Check for Updates if it is not supported
            this.CheckForUpdatesMenuItem.IsEnabled = ApplicationDeployment.IsNetworkDeployed;

            // Bind to the content rendered event
            ICollectionView view = CollectionViewSource.GetDefaultView(this.ConnectionsTabControl.Items);
            view.CollectionChanged += this.view_CollectionChanged;

            this.ContentRendered += this.MainWindow_ContentRendered;
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

        private void CheckForUpdatesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.CheckForUpdates();
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
                this._listener.PathRequest += this.ListenerPathRequest;
            }
            catch (SocketException ex)
            {
                MessageBoxHelper.Show(this, "Unable to open TCP Listener",
                    "Could not listen on port 4577. " + ex.Message);
                this.Close();
            }
        }

        private void ListenerPathRequest(Stream obj)
        {
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (str != null)
            {
                byte[] msgBytes = Encoding.Unicode.GetBytes(str);
                obj.Write(msgBytes, 0, msgBytes.Length);
            }
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

        private void CheckForUpdates()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                UpdateCheckInfo info;
                try
                {
                    info = ad.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBoxHelper.Show(this, "Error", "The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBoxHelper.Show(this, "Error", "Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBoxHelper.Show(this, "Error", "This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    var result =  MessageBoxHelper.Show(this, "Update Available", "An update is available. Press OK to update or close the window to cancel.");

                    if (result == true)
                    {
                        try
                        {
                            ad.Update();
                            MessageBoxHelper.Show(this, "Update succeeded", "The application has been upgraded, and will now restart."); 
                            System.Windows.Forms.Application.Restart();
                            Application.Current.Shutdown();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBoxHelper.Show(this, "Update failed", "Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                        }
                    }
                }
                else
                {
                    MessageBoxHelper.Show(this, "No updates found", "You are already running the lastest version of CupCake.");
                }
            }
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