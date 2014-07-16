using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CupCake.Client.Settings;
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
        private bool _isDisonnected;
        private string _statusText;
        private string _titleText = SettingsManager.UnnamedString;

        public ConnectionUserControl(ClientHandle handle)
        {
            InitializeComponent();

            Loaded += ConnectionUserControl_Loaded;

            _handle = handle;
            _handle.ReceiveOutput += ClientOutput;
            _handle.ReceiveClose += _handle_ConnectionClose;
            _handle.ReceiveTitle += _handle_ReceiveTitle;
            _handle.ReceiveStatus += _handle_ReceiveStatus;
            _handle.ReceiveWrongAuth += _handle_ReceiveWrongAuth;
        }

        public bool IsDebug { get; set; }

        public string StatusString
        {
            get
            {
                return (IsDisonnected ? "Disconnected" : "Connected") +
                       (_statusText != null ? " | " + _statusText : String.Empty);
            }
        }

        private bool IsDisonnected
        {
            get { return _isDisonnected; }
            set
            {
                _isDisonnected = value;
                OnStatus(StatusString);
            }
        }

        private string StatusText
        {
            set
            {
                _statusText = value;
                OnStatus(StatusString);
            }
        }

        public string TitleText
        {
            get { return _titleText; }
            set
            {
                _titleText = value;
                OnTitle(_titleText);
            }
        }

        public event Action<string> Title;

        protected virtual void OnTitle(string title)
        {
            Action<string> handler = Title;
            if (handler != null) handler(title);
        }

        public event Action<string> Status;

        protected virtual void OnStatus(string status)
        {
            Action<string> handler = Status;
            if (handler != null) handler(status);
        }

        private void KeepOpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClosingGrid.Visibility = Visibility.Collapsed;
            _cancelClose = true;
        }

        public void Close()
        {
            _handle.DoSendClose();
            _cancelClose = true;
        }

        public void RemoveTab()
        {
            var parent = (TabItem) Parent;
            var parentParent = (TabControl) parent.Parent;
            parentParent.Items.Remove(parent);
        }

        public void Clear()
        {
            OutputTextBox.Text = "--- Log Cleared ---";
        }

        private void _handle_ReceiveWrongAuth()
        {
            AppendText("ERROR: Wrong authentication data provided.");
        }

        private void _handle_ReceiveTitle(Title title)
        {
            Dispatch.Invoke(() => TitleText = title.Text);
        }

        private void _handle_ReceiveStatus(Status status)
        {
            Dispatch.Invoke(() => StatusText = status.Text);
        }

        private void _handle_ConnectionClose()
        {
            Dispatch.Invoke(() =>
            {
                IsDisonnected = true;
                TitleText += " (Disconnected)";
                AppendText("--- Connection terminated ---");

                if (IsDebug)
                {
                    ClosingGrid.Visibility = Visibility.Visible;

                    var timer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher)
                    {
                        Interval = new TimeSpan(0, 0, 30)
                    };
                    timer.Tick += (sender, args) =>
                    {
                        if (!_cancelClose)
                            RemoveTab();

                        timer.Stop();
                    };
                    timer.Start();
                }
            });
        }

        private void ConnectionUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void ClientOutput(Output output)
        {
            AppendText(output.Text);
        }

        private void AppendText(string text)
        {
            Dispatch.Invoke(() => OutputTextBox.AppendText(Environment.NewLine + text));
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OutputTextBox.ScrollToEnd();
                _handle.DoSendInput(InputTextBox.Text);
                InputTextBox.Clear();
            }
        }

        private void OutputTextBox_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (OutputTextBox.SelectedText.Length == 0)
            {
                InputTextBox.Focus();
            }
        }
    }
}