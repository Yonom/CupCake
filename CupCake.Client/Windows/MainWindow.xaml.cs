using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += this.MainWindow_Loaded;
            var view = CollectionViewSource.GetDefaultView(this.ConnectionsTabControl.Items);
            view.CollectionChanged += this.view_CollectionChanged;
        }

        void view_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var userControl = (ConnectionUserControl)((TabItem)e.OldItems[0]).Content;
                userControl.Close();
            }

            if (this.ConnectionsTabControl.Items.Count > 0)
                this.ConnectionsTabControl.Visibility = Visibility.Visible;
            else
                this.ConnectionsTabControl.Visibility = Visibility.Collapsed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._listener = new ServerListener(IPAddress.Loopback, ServerListener.ServerPort, handle => Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var tabItem = new TabItem
                {
                    Header = "<Unnamed>",
                    Content = new ConnectionUserControl(handle)
                };

                if (new NewConnectionWindow(handle) {Owner = this}.ShowDialog() != true)
                {
                    handle.DoSendClose();
                }

                this.ConnectionsTabControl.Items.Add(tabItem);
                tabItem.IsSelected = true;
            })));
        }

        private void ButtonNew_OnClick(object sender, RoutedEventArgs e)
        {
            new Process { StartInfo = { UseShellExecute = false, CreateNoWindow = true, FileName = "CupCake.Server.exe" } }.Start();
        }

        private void ButtonAttach_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var attach = new AttachWindow { Owner = this };
                if (attach.ShowDialog() == true)
                {
                    IPEndPoint endPoint = IPHelper.Parse(attach.AddressTextBox.Text, 4577);
                    this._listener.Connect(endPoint,
                        handle => Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            var tabItem = new TabItem
                            {
                                Header = "<Unnamed>",
                                Content = new ConnectionUserControl(handle)
                            };

                            handle.DoSendAuthentication(attach.PinPasswordBox.Password);

                            this.ConnectionsTabControl.Items.Add(tabItem);
                            tabItem.IsSelected = true;
                        })));
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
            catch (SocketException ex)
            {
                MessageBoxHelper.Show(this, "Error", "Error while connecting: " + ex.Message);
            }
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
