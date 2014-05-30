using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CupCake.Client.Settings;
using CupCake.Client.UserControls;
using CupCake.Protocol;

namespace CupCake.Client.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ServerListener _listener;
        private int _connectionCount;

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


        public MainWindow()
        {
            InitializeComponent();
            this.HasConnectionSelected = false;

            var view = CollectionViewSource.GetDefaultView(this.ConnectionsTabControl.Items);
            view.CollectionChanged += this.view_CollectionChanged;

            this.StartServer();

            Application.Current.Exit += this.App_Exit;
        }

        void App_Exit(object sender, ExitEventArgs e)
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

        private void AttachMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.ShowAttach();
        }

        private void CloseConnectionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var tab = this.ConnectionsTabControl.SelectedItem;
            if (tab != null)
            {
                this.CloseConnection((TabItem)tab);
            }
        }

        private void ClearLogMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var tab = this.ConnectionsTabControl.SelectedItem;
            if (tab != null)
            {
                this.ClearLog((TabItem)tab);
            }
        }

        private void ProfilesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowList(EditListType.Profile);
        }

        private void AccountsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowList(EditListType.Account);
        }

        private void AboutMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowAbout();
        }

        private void StartServer()
        {
            // Start the server
            this._listener = new ServerListener(IPAddress.Loopback, ServerListener.ServerPort,
                handle => this.HandleIncoming(handle, () =>
                {
                    if (new NewConnectionWindow(handle) {Owner = this}.ShowDialog() != true)
                    {
                        handle.DoSendClose();
                    }
                }));
        }

        private void HandleIncoming(ClientHandle handle, Action afferBind)
        {
            Dispatch.Invoke(() =>
            {
                this.ConnectionCount++;

                handle.ReceiveClose += () => Dispatch.Invoke(() =>
                {
                    this.ConnectionCount--;
                });

                var tabItem = new TabItem
                {
                    Header = "<Unnamed>",
                    Content = new ConnectionUserControl(handle)
                };

                afferBind();

                this.ConnectionsTabControl.Items.Add(tabItem);
                tabItem.IsSelected = true;
            });
        }

        private void NewConnection(bool showConsole)
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
                        this._listener.Connect(endPoint,
                            handle =>
                                this.HandleIncoming(handle,
                                    () => handle.DoSendAuthentication(attach.PinPasswordBox.Password)));
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
            var profiles = new EditListWindow(type) { Owner = this };
            profiles.ShowDialog();
        }
    }
}