using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using CupCake.Core.Storage;
using CupCake.DefaultCommands.Commands;
using CupCake.Protocol;
using CupCake.Server.StorageProviders;
using NDesk.Options;

namespace CupCake.Server
{
    internal static class Program
    {
        // This UserCommandsMuffin DefaultCommands to be included in CupCake.Client's output directory
#pragma warning disable 169
        private static CommandBase<object> _uselessVariable;
#pragma warning restore 169

        private static CupCakeServerSettings _settings = new CupCakeServerSettings();

        private static bool _started;
        private static readonly CupCakeClientHost _clientEx = new CupCakeClientHost();

        private static ServerListener _listener;
        private static string _title = "<Unnamed>";
        private static string _status;
        private static readonly List<string> _outputs = new List<string>();
        private static int _shutdownReason;
        private static readonly ManualResetEvent _shutdownEvent = new ManualResetEvent(false);

        private static event Action<string> Output;

        private static void OnOutput(string output)
        {
            Action<string> handler = Output;
            if (handler != null) handler(output);
        }

        private static event Action<string> Title;

        private static void OnTitle(string title)
        {
            Action<string> handler = Title;
            if (handler != null) handler(title);
        }

        private static event Action<string> Status;

        private static void OnStatus(string status)
        {
            Action<string> handler = Status;
            if (handler != null) handler(status);
        }

        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Title += Program_Title;
            Status += Program_Status;
            Output += Program_Output;

            _clientEx.Output += OnOutput;
            _clientEx.Title += OnTitle;
            _clientEx.Status += OnStatus;

            var p = new OptionSet
            {
                {
                    "localonly",
                    v => { _settings.LocalOnly = v != null; }
                },
                {
                    "settings=",
                    v => { _settings = XmlSerialize.Deserialize<CupCakeServerSettings>(v); }
                },
                {
                    "debug",
                    v => { _settings.Debug = v != null; }
                },
                {
                    "standalone",
                    v => { _settings.Standalone = v != null; }
                },
                {
                    "autoconnect",
                    v => { _settings.Autoconnect = v != null; }
                },
                {
                    "envpath=",
                    v => { Environment.CurrentDirectory = v; }
                },
                {
                    "port=",
                    (int v) => { _settings.Port = v; }
                },
                {
                    "pin=",
                    v => { _settings.Pin = v; }
                },
                {
                    "e|email=",
                    v => { _settings.Email = v; }
                },
                {
                    "p|pass|password=",
                    v => { _settings.Password = v; }
                },
                {
                    "w|world=",
                    v => { _settings.World = v; }
                },
                {
                    "d|dir=",
                    _settings.Dirs.Add
                },
                {
                    "db|dbtype=",
                    v => { _settings.DatabaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), v); }
                },
                {
                    "cs|connectionstring|connectionstr|connstr=",
                    v => { _settings.ConnectionString = v; }
                }
            };
            try
            {
                p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("CupCake.Server: ");
                Console.WriteLine(e.Message);
                return 0;
            }
            
            StartServer();

            new Thread(() =>
            {
                while (true)
                {
                    var input = Console.ReadLine();

                    if (_clientEx != null && input != null)
                        _clientEx.Input(input);
                }
            }) {IsBackground = true}.Start();
            _shutdownEvent.WaitOne();
            return _shutdownReason;
        }

        private static void Program_Output(string output)
        {
            _outputs.Add(output);

            Console.WriteLine(output);
        }

        private static void Program_Title(string title)
        {
            _title = title;
        }

        private static void Program_Status(string status)
        {
            _status = status;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                OnOutput(ex.ToString());
            }
        }

        private static void StartServer()
        {
            var ipAddress = _settings.LocalOnly
                ? IPAddress.Loopback
                : IPAddress.Any;
            _listener = new ServerListener(ipAddress, _settings.Port, OnConnection);
            OnOutput("^^^ Started server on " + _listener.EndPoint);

            if (!_settings.Standalone)
            {
                try
                {
                    _listener.Connect(new IPEndPoint(IPAddress.Loopback, ServerListener.ServerPort), h =>
                    {
                        OnConnection(h);

                        h.ReceiveClose += () =>
                        {
                            Shutdown(0);
                        };
                        
                        if (!_settings.Autoconnect)
                        {
                            h.DoSendRequestData(_settings.Debug);
                        }
                    });
                }
                catch (SocketException)
                {
                    Console.WriteLine("Trouble connecting to server. Make sure CupCake Client is running.");
                    Shutdown(1);
                }
            }

            if (_settings.Standalone || _settings.Autoconnect)
            {
                Start();
            }
        }

        private static void OnConnection(ClientHandle h)
        {
            bool authenticated = false;
            if (String.IsNullOrEmpty(_settings.Pin))
            {
                authenticated = true;
                SendBuffer(h);
            }

            Output += s =>
            {
                if (!authenticated) return;

                h.DoSendOutput(s);
            };

            Title += s =>
            {
                if (!authenticated) return;

                h.DoSendTitle(s);
            };

            Status += s =>
            {
                if (!authenticated) return;

                h.DoSendStatus(s);
            };

            h.ReceiveAuthentication += authentication =>
            {
                if (_settings.Pin == authentication.Pin || authenticated)
                {
                    authenticated = true;
                    SendBuffer(h);
                }
                else
                {
                    h.DoSendWrongAuth();
                    h.DoSendClose();
                }
            };

            h.ReceiveInput += input =>
            {
                if (!authenticated) return;

                _clientEx.Input(input.Text);
            };

            h.ReceiveSetData += data =>
            {
                if (!authenticated) return;

                if (data.Settings != null)
                    _settings = XmlSerialize.Deserialize<CupCakeServerSettings>(data.Settings);

                if (_settings.Email == null)
                    _settings.Email = data.Email;
                if (_settings.Password == null)
                    _settings.Password = data.Password;
                if (_settings.World == null)
                    _settings.World = data.World;
                if (_settings.ConnectionString == null)
                    _settings.DatabaseType = data.DatabaseType;
                if (_settings.ConnectionString == null)
                    _settings.ConnectionString = data.ConnectionString;

                if (data.Directories != null)
                    _settings.Dirs.InsertRange(0, data.Directories);

                Start();
            };
        }

        private static void SendBuffer(ClientHandle h)
        {
            h.DoSendTitle(_title);
            h.DoSendStatus(_status);

            foreach (string output in _outputs.Skip(_outputs.Count - 200))
            {
                h.DoSendOutput(output);
            }
        }


        private static void Start()
        {
            if (_started) return;
            _started = true;

            IStorageProvider storage;
            try
            {
                if (_settings.DatabaseType == DatabaseType.MySql)
                    storage = new MySqlStorageProvider(_settings.ConnectionString);
                else if (_settings.DatabaseType == DatabaseType.SQLite)
                    storage = new SQLiteStorageProvider(_settings.ConnectionString);
                else
                    storage = null;
            }
            catch (StorageException)
            {
                OnOutput("Error: Unable to connect to database.");
                throw;
            }

            _clientEx.Start(_settings.Email, _settings.Password, _settings.World, _settings.Dirs.ToArray(), storage);
        }

        internal static void Shutdown(int reason)
        {
            _clientEx.Dispose();
            _shutdownReason = reason;
            _shutdownEvent.Set();
        }

        internal static void Restart()
        {
            _listener.Dispose();

            var args = new List<string>
            {
                @"--env """ + Environment.CurrentDirectory + @"""",
                @"--autoconnect",
                @"--email " + _settings.Email,
                @"--pass " + _settings.Password,
                @"--world " + _settings.World,
                @"--port " + _settings.Port,
                @"--dbtype " + _settings.DatabaseType,
                @"--cs """ + _settings.ConnectionString + @""""
            };
            if (_settings.Debug)
                args.Add(@"--debug");
            if (_settings.Standalone)
                args.Add(@"--standalone");
            if (_settings.LocalOnly)
                args.Add(@"--localonly");
            if (!String.IsNullOrEmpty(_settings.Pin))
                args.Add(@"--pin " + _settings.Pin);

            args.AddRange(_settings.Dirs.Select(d => @"--dir """ + d + @""""));

            string fileName;
            if (IsRunningOnMono())
            {
                fileName = "mono";
                args.Insert(0, GetFileName());
            }
            else
            {
                fileName = GetFileName();
            }

            var p = new Process
            {
                StartInfo =
                {
                    FileName = fileName,
                    Arguments = String.Join(" ", args)
                }
            };
            if (!HasMainWindow())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
            }
            p.Start();
        }

        private static string GetFileName()
        {
            return Assembly.GetEntryAssembly().GetName().CodeBase;
        }

        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        private static bool HasMainWindow()
        {
            return (Process.GetCurrentProcess().MainWindowHandle != IntPtr.Zero);
        }
    }
}