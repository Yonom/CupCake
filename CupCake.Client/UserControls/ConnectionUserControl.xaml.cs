using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CupCake.Client.Settings;
using CupCake.Client.Windows;
using CupCake.Protocol;

namespace CupCake.Client.UserControls
{
    /// <summary>
    ///     Interaction logic for ConnectionUserControl.xaml
    /// </summary>
    public partial class ConnectionUserControl
    {
        private readonly ClientHandle _handle;
        private bool _cancelClose;
        private string _titleText = SettingsManager.UnnamedString;
        private string _statusText;
        private bool _isDisonnected;

        public bool IsDebug { get; set; }

        public string StatusString {
            get { return (this.IsDisonnected ? "Disconnected" : "Connected") + (this._statusText != null ? " | " +  this._statusText : String.Empty); }
        }

        private bool IsDisonnected
        {
            get { return this._isDisonnected; }
            set
            {
                this._isDisonnected = value;
                this.OnStatus(this.StatusString);
            }
        }

        private string StatusText
        {
            set
            {
                this._statusText = value;
                this.OnStatus(this.StatusString);
            }
        }

        public string TitleText
        {
            get { return this._titleText; }
            set
            {
                this._titleText = value;
                this.OnTitle(this._titleText);
            }
        }

        public event Action<string> Title;

        protected virtual void OnTitle(string title)
        {
            Action<string> handler = this.Title;
            if (handler != null) handler(title);
        }

        public event Action<string> Status;

        protected virtual void OnStatus(string status)
        {
            Action<string> handler = this.Status;
            if (handler != null) handler(status);
        }

        public ConnectionUserControl(ClientHandle handle)
        {
            this.InitializeComponent();

            this.Loaded += this.ConnectionUserControl_Loaded;

            this._handle = handle;
            this._handle.ReceiveOutput += this.ClientOutput;
            this._handle.ReceiveClose += this._handle_ConnectionClose;
            this._handle.ReceiveTitle += this._handle_ReceiveTitle;
            this._handle.ReceiveStatus += this._handle_ReceiveStatus;
            this._handle.ReceiveWrongAuth += this._handle_ReceiveWrongAuth;
        }

        private void KeepOpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClosingGrid.Visibility = Visibility.Collapsed;
            this._cancelClose = true;
        }

        public void Close()
        {
            this._handle.DoSendClose();
            this._cancelClose = true;
        }

        public void RemoveTab()
        {
            var parent = (TabItem)this.Parent;
            var parentParent = (TabControl)parent.Parent;
            parentParent.Items.Remove(parent);
        }

        public void Clear()
        {
            this.OutputTextBox.Text = "--- Log Cleared ---";
        }

        private void _handle_ReceiveWrongAuth()
        {
            this.AppendText("ERROR: Wrong authentication data provided.");
        }

        private void _handle_ReceiveTitle(Title title)
        {
            Dispatch.Invoke(() => this.TitleText = title.Text);
        }

        private void _handle_ReceiveStatus(Status status)
        {
            Dispatch.Invoke(() => this.StatusText = status.Text);
        }

        private void _handle_ConnectionClose()
        {
            Dispatch.Invoke(() =>
            {
                this.IsDisonnected = true;
                this.TitleText += " (Disconnected)";
                this.AppendText("--- Connection terminated ---");

                if (this.IsDebug)
                {
                    this.ClosingGrid.Visibility = Visibility.Visible;

                    var timer = new DispatcherTimer(DispatcherPriority.Normal, this.Dispatcher)
                    {
                        Interval = new TimeSpan(0, 0, 30)
                    };
                    timer.Tick += (sender, args) =>
                    {
                        if (!this._cancelClose)
                            this.RemoveTab();

                        timer.Stop();
                    };
                    timer.Start();
                }
            });
        }

        private void ConnectionUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void ClientOutput(Output output)
        {
            this.AppendText(output.Text);
        }

        private void AppendText(string text)
        {
            Dispatch.Invoke(() => this.OutputTextBox.AppendText(Environment.NewLine + text));
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.OutputTextBox.ScrollToEnd();
                this._handle.DoSendInput(this.InputTextBox.Text);
                this.InputTextBox.Clear();
            }
        }

        private void OutputTextBox_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.OutputTextBox.SelectedText.Length == 0)
            {
                this.InputTextBox.Focus();
            }
        }
    }
}