using System;
using System.Linq;
using System.Threading;
using CupCake.Core.Host;
using CupCake.Core.Platforms;

namespace CupCake.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new CupCakeClient();
            client.Start();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}