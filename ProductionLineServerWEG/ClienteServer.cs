using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;

namespace ProductionLineServerWEG
{
    class ClienteServer : WebSocketBehavior
    {

        private void run()
        {
            while (true)
            {
                Sessions.Broadcast("Testando essa poha toda/../true");
                Console.WriteLine("enviando...");
                Thread.Sleep(1000);
            }
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine(e.Data);
            Console.WriteLine(ID+"");
            new Thread(run).Start();
            Sessions.Broadcast(e.Data);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            Console.WriteLine(ID + " conected.");
            new Thread(run).Start();
        }
    }
}
