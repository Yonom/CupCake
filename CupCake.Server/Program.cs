using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using CupCake.Protocol;
using NDesk.Options;

namespace CupCake.Server
{
    internal class Program
    {
        private static int _port;
        private static string _pin;
        private static string _email;
        private static string _password;
        private static string _world;
        private static bool _standalone;
        private static readonly List<string> _dirs = new List<string>();

        private static bool _started;
        private static readonly CupCakeClientEx _clientEx = new CupCakeClientEx();

        private static string _title;
        private static readonly List<string> _outputs = new List<string>();

        private static event Action<string> Output;

        private static void OnOutput(string obj)
        {
            Action<string> handler = Output;
            if (handler != null) handler(obj);
        }

        private static event Action<string> Title;

        private static void OnTitle(string obj)
        {
            Action<string> handler = Title;
            if (handler != null) handler(obj);
        }

        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Title += Program_Title;
            Output += Program_Output;

            _clientEx.Output += OnOutput;
            _clientEx.Title += OnTitle;

            var p = new OptionSet
            {
                {
                    "standalone",
                    v => { _standalone = v != null; }
                },
                {
                    "port=",
                    (int v) => { _port = v; }
                },
                {
                    "pin=",
                    v => { _pin = v; }
                },
                {
                    "e|email=",
                    v => { _email = v; }
                },
                {
                    "p|password=",
                    v => { _password = v; }
                },
                {
                    "w|world=",
                    v => { _world = v; }
                },
                {
                    "d|dir=",
                    _dirs.Add
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
                return;
            }

            StartServer();

            Thread.Sleep(Timeout.Infinite);
        }

        private static void Program_Output(string obj)
        {
            _outputs.Add(obj);
        }

        private static void Program_Title(string obj)
        {
            _title = obj;
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
            var listener = new ServerListener(IPAddress.Any, _port, OnConnection);

            // Connect to server if not standalone
            if (!_standalone)
            {
                try
                {
                    listener.Connect(new IPEndPoint(IPAddress.Loopback, ServerListener.ServerPort), h =>
                    {
                        h.ReceiveClose += () => Environment.Exit(0);

                        OnConnection(h);
                    });
                }
                catch (SocketException)
                {
                    Console.WriteLine("Trouble connecting to server. Make sure CupCake Client is running.");
                    Environment.Exit(1);
                }
            }
        }

        private static void OnConnection(ClientHandle h)
        {
            bool authenticated = String.IsNullOrEmpty(_pin);

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

            h.ReceiveAuthentication += authentication =>
            {
                if (_pin == authentication.Pin || authenticated)
                {
                    authenticated = true;
                    h.DoSendTitle(_title);

                    foreach (string output in _outputs.Skip(_outputs.Count - 200))
                    {
                        h.DoSendOutput(output);
                    }
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

                _email = data.Email;
                _password = data.Password;
                _world = data.World;
                if (data.Directories != null)
                    _dirs.AddRange(data.Directories);
                Start();
            };
        }

        private static void Start()
        {
            if (_started) return;

            _started = true;
            _clientEx.Start(_email, _password, _world, _dirs.ToArray());
        }
    }
}