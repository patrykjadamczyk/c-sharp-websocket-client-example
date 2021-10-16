using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebsocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);
            var url = new Uri("ws://127.0.0.1:8080");

            using (var client = new Websocket.Client.WebsocketClient(url))
            {
                client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                client.ReconnectionHappened.Subscribe(info => Console.WriteLine($"Reconnection happened, type: {info.Type}"));
                client.MessageReceived.Subscribe(msg => Console.WriteLine($"Message received: {msg}"));
                client.DisconnectionHappened.Subscribe(info => Console.WriteLine($"Disconnect Happened: {info.ToString()}"));

                client.StartOrFail();

                Task.Run(() => client.Send("start"));
                while (true)
                {
                    Console.WriteLine("Write commands to send to websocket: ");
                    var message = Console.ReadLine();
                    client.Send(message);
                }
                // exitEvent.WaitOne();
            }
        }
    }
}