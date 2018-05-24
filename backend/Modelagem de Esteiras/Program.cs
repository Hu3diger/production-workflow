using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Testes.TesteProcesso1();
            Testes.TesteProcesso2();
        }
    }

    class Testes
    {
        public static void TesteProcesso1()
        {
        Processo p1 = new ProcessoPeca(new BaseProcesso("kappa1"));
            Processo p11 = new ProcessoPeca(new BaseProcesso("kappa11"));
                Processo p111 = new ProcessoPeca(new BaseProcesso("kappa111"));
                Processo p112 = new ProcessoPeca(new BaseProcesso("kappa112"));
                Processo p113 = new ProcessoPeca(new BaseProcesso("kappa113"));
                    Processo p1131 = new ProcessoPeca(new BaseProcesso("kappa1131"));
            Processo p12 = new ProcessoPeca(new BaseProcesso("kappa12"));
                Processo p121 = new ProcessoPeca(new BaseProcesso("kappa121"));
                    Processo p1211 = new ProcessoPeca(new BaseProcesso("kappa1211"));
                Processo p122 = new ProcessoPeca(new BaseProcesso("kappa122"));
                    Processo p1221 = new ProcessoPeca(new BaseProcesso("kappa1221"));
                Processo p123 = new ProcessoPeca(new BaseProcesso("kappa123"));
            Processo p13 = new ProcessoPeca(new BaseProcesso("kappa13"));
            Processo p14 = new ProcessoPeca(new BaseProcesso("kappa14"));
                Processo p141 = new ProcessoPeca(new BaseProcesso("kappa141"));
                Processo p142 = new ProcessoPeca(new BaseProcesso("kappa142"));
                    Processo p1421 = new ProcessoPeca(new BaseProcesso("kappa1421"));
                Processo p143 = new ProcessoPeca(new BaseProcesso("kappa143"));
            Processo p15 = new ProcessoPeca(new BaseProcesso("kappa15"));
                Processo p151 = new ProcessoPeca(new BaseProcesso("kappa151"));


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
                        p1.GetInternalOrderProcess().ForEach(x => WriteLine(x.Order + " | " + x.Name));
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
                            p1.GetFathersProcess(teste.Name).ForEach(x => WriteLine("in " + x.Name));
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
            WriteLine("End");
        }

        public static void TesteProcesso2()
        {
            WriteLine("Teste 2");

            BaseProcesso kappa = new BaseProcesso("kaaapa");

            ProcessoPeca p1 = new ProcessoPeca(kappa);
            ProcessoPeca p2 = new ProcessoPeca(kappa);

            p1.AddInternalProcess(0, p2);

            WriteLine(p1.Name);

            kappa.Name = "teste";

            p1.InProcess = true;
            WriteLine(p1.Name);
            WriteLine(p1.InProcess);

            ProcessoPeca p3 = (ProcessoPeca) p1.Clone();

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
