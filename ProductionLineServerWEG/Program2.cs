using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLineServerWEG
{
    class Program2
    {
        static List<Processo> listProcessos = new List<Processo>();

        static void ListAnacleto()
        {
            while (true)
            {
                string recive = Console.ReadLine();

                string[] commands = recive.Split(' ');

                /*  create process nome_Processo
                *   list process
                *   insert process nome_Processo nomeOndeSeraInseridoProcesso posição(int)
                */
                switch (commands[0])
                {
                    case "create":

                        switch (commands[1].ToLower())
                        {
                            case "process":

                                BaseProcesso bP = new BaseProcesso(commands[2], "", 1000);

                                Processo p = new Processo(bP);

                                listProcessos.Add(p);

                                break;

                            default:
                                Testes.WriteLine("'" + commands[1] + "' não encontrado");
                                break;
                        }

                        break;

                    case "list":

                        switch (commands[1].ToLower())
                        {
                            case "process":

                                listProcessos.ForEach(x => Testes.WriteLine(x.Name));

                                break;

                            default:
                                Testes.WriteLine("'" + commands[1] + "' não encontrado");
                                break;
                        }

                        break;

                    case "insert":

                        switch (commands[1].ToLower())
                        {
                            case "process":

                                try
                                {

                                    Processo p1 = listProcessos.Find(x => x.Name.Equals(commands[2]));
                                    Processo p2 = listProcessos.Find(x => x.Name.Equals(commands[3]));

                                    p2.AddInternalProcess(int.Parse(commands[4]), p1);
                                }
                                catch (Exception)
                                {

                                    throw;
                                }

                                break;

                            default:
                                Testes.WriteLine("'" + commands[1] + "' não encontrado");
                                break;
                        }

                        break;

                    default:
                        Testes.WriteLine("'" + commands[0] + "' não encontrado");
                        break;
                }
            }
        }
    }

    class Testes
    {
        public static void TesteProcesso1()
        {
            Processo p1 = new Processo(new BaseProcesso("kappa1", "", 1000));
            Processo p11 = new Processo(new BaseProcesso("kappa11", "", 1000));
            Processo p111 = new Processo(new BaseProcesso("kappa111", "", 1000));
            Processo p112 = new Processo(new BaseProcesso("kappa112", "", 1000));
            Processo p113 = new Processo(new BaseProcesso("kappa113", "", 1000));
            Processo p1131 = new Processo(new BaseProcesso("kappa1131", "", 1000));
            Processo p12 = new Processo(new BaseProcesso("kappa12", "", 1000));
            Processo p121 = new Processo(new BaseProcesso("kappa121", "", 1000));
            Processo p1211 = new Processo(new BaseProcesso("kappa1211", "", 1000));
            Processo p122 = new Processo(new BaseProcesso("kappa122", "", 1000));
            Processo p1221 = new Processo(new BaseProcesso("kappa1221", "", 1000));
            Processo p123 = new Processo(new BaseProcesso("kappa123", "", 1000));
            Processo p13 = new Processo(new BaseProcesso("kappa13", "", 1000));
            Processo p14 = new Processo(new BaseProcesso("kappa14", "", 1000));
            Processo p141 = new Processo(new BaseProcesso("kappa141", "", 1000));
            Processo p142 = new Processo(new BaseProcesso("kappa142", "", 1000));
            Processo p1421 = new Processo(new BaseProcesso("kappa1421", "", 1000));
            Processo p143 = new Processo(new BaseProcesso("kappa143", "", 1000));
            Processo p15 = new Processo(new BaseProcesso("kappa15", "", 1000));
            Processo p151 = new Processo(new BaseProcesso("kappa151", "", 1000));


            p1.AddInternalProcess(-1, p11);
            p11.AddInternalProcess(-1, p111);
            p11.AddInternalProcess(-1, p112);
            p11.AddInternalProcess(-1, p113);
            p113.AddInternalProcess(-1, p1131);
            p1.AddInternalProcess(-1, p12);
            p12.AddInternalProcess(-1, p121);
            p121.AddInternalProcess(-1, p1211);
            p12.AddInternalProcess(-1, p122);
            p122.AddInternalProcess(-1, p1221);
            p12.AddInternalProcess(-1, p123);
            p1.AddInternalProcess(-1, p13);
            p1.AddInternalProcess(-1, p14);
            p14.AddInternalProcess(-1, p141);
            p14.AddInternalProcess(-1, p142);
            p142.AddInternalProcess(-1, p1421);
            p14.AddInternalProcess(-1, p143);
            p1.AddInternalProcess(-1, p15);
            p15.AddInternalProcess(-1, p151);

            p1.Reorder(0);
            p1.FindInternalProcess(null);

            while (true)
            {
                Write("Digite o nome do objeto a ser procurado: ");

                string t = Console.ReadLine();

                switch (t.ToLower())
                {
                    case "exit":
                        goto nha;

                    case "test":
                        p1.TestProcess();
                        break;

                    case "list":
                        p1.GetInternalOrderProcess().ForEach(x => WriteLine(x.Order + " | " + x.Cascade + " | " + x.Name));
                        break;

                    case "listthis":
                        Processo p2 = (Processo)p1.Clone();
                        p1.GetInternalOrderProcess().ForEach(x => WriteLine(x.Order + " | " + x.Name));
                        WriteLine("P2===============");
                        p2.GetInternalOrderProcess().ForEach(x => WriteLine(x.Order + " | " + x.Name));
                        break;

                    default:
                        Processo teste = p1.FindInternalProcess(t);

                        if (teste != null)
                        {
                            teste.GetFathersProcess().ForEach(x => WriteLine("in " + x.Name));
                            WriteLine(teste.Name);
                            WriteLine("Cascade: " + teste.Cascade);
                        }
                        else
                        {
                            WriteLine("\n Objeto \"" + t + "\" não encontrado...");
                        }
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
            nha:
            WriteLine("End...");
        }

        public static void TesteProcesso2()
        {
            WriteLine("Teste 2");

            BaseProcesso kappa = new BaseProcesso("kaaapa", "", 1000);

            Processo p1 = new Processo(kappa);
            Processo p2 = new Processo(kappa);

            p1.AddInternalProcess(0, p2);

            WriteLine(p1.Name);

            kappa.Name = "teste";

            p1.InProcess = true;
            WriteLine(p1.Name);
            WriteLine(p1.InProcess);

            Processo p3 = (Processo)p1.Clone();

            kappa.Name = "Chupisco";

            WriteLine(p3.Name);
            WriteLine(p1.Name);

            WriteLine(p3.InProcess);
            p3.InProcess = false;
            WriteLine(p3.InProcess);
            WriteLine(p1.InProcess);

            kappa.Name = "Teste4";

            WriteLine(p1.GetInternalOrderProcess()[0].Name);
            WriteLine(p3.GetInternalOrderProcess()[0].Name);

            p1.GetInternalOrderProcess()[0].InProcess = true;
            p3.GetInternalOrderProcess()[0].InProcess = false;

            WriteLine(p1.GetInternalOrderProcess()[0].InProcess);
            WriteLine(p3.GetInternalOrderProcess()[0].InProcess);

            p3.ClearList();

            WriteLine(p1.CloneList().Count);
            WriteLine(p3.CloneList().Count);



            Console.ReadKey();
        }

        static int line = 0;
        public static void WriteLine(object t)
        {
            Console.WriteLine(line++ + " | " + t.ToString());
        }

        public static void Write(object t)
        {
            Console.Write(line++ + " | " + t.ToString());
        }
    }
}
