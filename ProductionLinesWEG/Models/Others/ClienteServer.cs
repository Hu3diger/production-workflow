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
            //while (true)
            //{
            //    Sessions.Broadcast("Testando essa poha toda/../true");
            //    Console.WriteLine("enviando...");
            //    Thread.Sleep(1000);
            //}
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            string msg = e.Data;

            Console.WriteLine("recived from {0}: {1}", ID, e.Data);

            if (msg.Contains("/cadBaseProcess/"))
            {
                string varp = msg.Substring(16);
                string[] vetor = varp.Split(new string[] { "/../" }, StringSplitOptions.None);

                BaseProcesso bP = new BaseProcesso(vetor[0], vetor[1], int.Parse(vetor[2]));
                Program.getProgram().ListProcesso.Add(new Processo(bP));

                Console.Clear();

                Program.getProgram().ListProcesso.ForEach(x => Console.WriteLine("Processo: {0}, {1}, {2}", x.Name, x.Description, x.Runtime));
            }
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            Console.WriteLine(ID + " conected.");
            new Thread(run).Start();
        }
    }
}
