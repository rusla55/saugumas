using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace server
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine(e.Data);
            Sessions.Broadcast(e.Data);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer server = new WebSocketServer("ws://127.0.0.1:7890");
            server.AddWebSocketService<Echo>("/Echo");
            server.Start();
            Console.WriteLine("Server started");
            Console.ReadKey();
            server.Stop();
        }
    }
}
