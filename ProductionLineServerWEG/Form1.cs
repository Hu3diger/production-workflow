using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionLineServerWEG
{
    public partial class Form1 : Form
    {

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());

            new Form1().Show();
        }

        static List<Processo> listProcessos = new List<Processo>();
        static List<EsteiraAbstrata> listEsteiras = new List<EsteiraAbstrata>();

        public Form1()
        {
            InitializeComponent();
        }

        public void ExternalTerminal(string msg)
        {
            Terminal.Invoke(new MethodInvoker(delegate
            {
                Terminal.AppendText(msg);
            }));
        }

        private void BtnCriaProcesso_Click(object sender, EventArgs e)
        {
            listProcessos.Add(new Processo(new BaseProcesso(nomeP.Text, DescP.Text, int.Parse(RuntimeP.Text))));
            Terminal.AppendText("Processo add\n");

            attAllListBox();
        }

        private void BtnInsertProcesso_Click(object sender, EventArgs e)
        {
            listProcessos.Find(x => x.Name.Equals(listBox2.GetItemText(listBox2.SelectedItem))).AddInternalProcess(-1, listProcessos.Find(x => x.Name.Equals(listBox1.GetItemText(listBox1.SelectedItem))));

            Terminal.AppendText("Processo adicionado\n");

            attAllListBox();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = listBox1.GetItemText(listBox1.SelectedItem);

            attListBox2();
        }

        private void BtnListProcesso_Click(object sender, EventArgs e)
        {
            listProcessos.Where(x => x.getFather() == null).ToList().ForEach(y => y.GetInternalOrderProcess().ForEach(x => escreverList(x)));

            attAllListBox();
        }

        private void escreverList(Processo x)
        {
            for (int i = 0; i < x.Cascade; i++)
            {
                Terminal.AppendText("   ");
                if (i == x.Cascade - 1)
                {
                    Terminal.AppendText("ட");
                }
            }
            Terminal.AppendText(x.Cascade + " | " + x.Name + "\n");
        }

        private void attAllListBox()
        {
            attListBox1();
            attListBox2();
            attListBox3();
            attListBox4();
            attListBox5();
        }

        private void attListBox1()
        {
            listBox1.DataSource = listProcessos.Where(x => x.getFather() == null).Select(x => x.Name).ToList();
        }

        private void attListBox2()
        {

            listBox2.DataSource = listProcessos.Where(x => x.GetFathersProcess().Find(y => y.Name.Equals(listBox1.GetItemText(listBox1.SelectedItem))) == null).Select(x => x.Name).ToList();
        }

        private void attListBox3()
        {
            listBox3.DataSource = listProcessos.Select(x => x.Name).ToList();
        }

        private void attListBox4()
        {
            listBox4.DataSource = listEsteiras.Select(x => x.Name).ToList();
        }

        private void attListBox5()
        {
            listBox5.DataSource = listEsteiras.Select(x => x.Name).ToList();
        }

        private void Terminal_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            Terminal.SelectionStart = Terminal.Text.Length;
            // scroll it automatically
            Terminal.ScrollToCaret();
        }

        private void BtnCriarEsteira_Click(object sender, EventArgs e)
        {
            listEsteiras.Add(new EsteiraModel(NomeE.Text, int.Parse(InLimitE.Text)));

            Terminal.AppendText("Esteira add\n");

            attAllListBox();
        }

        private void BtnInserirPinE_Click(object sender, EventArgs e)
        {
            EsteiraModel em = (EsteiraModel)listEsteiras.Find(x => x.Name.Equals(listBox4.GetItemText(listBox4.SelectedItem)));

            em.insertMasterProcess(listProcessos.Find(x => x.Name.Equals(listBox3.GetItemText(listBox3.SelectedItem))));

            Terminal.AppendText("Processo inserido na esteira\n");

            attAllListBox();
        }

        private void BtnPreLoadProcess_Click(object sender, EventArgs e)
        {
            for (int i = listProcessos.Count - 1; i >= 0; i--)
            {
                listProcessos.RemoveAt(i);
            }

            listProcessos.Add(new Processo(new BaseProcesso("a", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("b", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("c", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("d", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("e", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("f", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("g", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("h", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("i", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("j", "Processo qualquer", 1000)));

            listProcessos[0].AddInternalProcess(-1, listProcessos[1]);
            listProcessos[0].AddInternalProcess(-1, listProcessos[3]);
            listProcessos[0].AddInternalProcess(-1, listProcessos[6]);

            listProcessos[1].AddInternalProcess(-1, listProcessos[2]);

            listProcessos[3].AddInternalProcess(-1, listProcessos[4]);
            listProcessos[3].AddInternalProcess(-1, listProcessos[5]);

            listProcessos[6].AddInternalProcess(-1, listProcessos[7]);

            listProcessos[7].AddInternalProcess(-1, listProcessos[8]);
            listProcessos[7].AddInternalProcess(-1, listProcessos[9]);

            attAllListBox();

            Terminal.AppendText("Sistema pré-carregado com processos\n");
        }

        private void BtnInsertPiece_Click(object sender, EventArgs e)
        {
            Peca pc = new Peca();

            if (listEsteiras.Find(x => x.Name.Equals(listBox5.GetItemText(listBox5.SelectedItem))).InsertPiece(pc))
            {
                Terminal.AppendText("Peça inserida na esteira selecionada\n");
            }
            else
            {
                Terminal.AppendText("Esteira lotada, não é possivel inserir mais peças\n");
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {

        }

        private void BtnStop_Click(object sender, EventArgs e)
        {

        }

        private void BtnLigarE_Click(object sender, EventArgs e)
        {
            listEsteiras.Find(x => x.Name.Equals(listBox5.GetItemText(listBox5.SelectedItem))).TurnOn(this);
        }

        private void BtnDesligarE_Click(object sender, EventArgs e)
        {
            listEsteiras.Find(x => x.Name.Equals(listBox5.GetItemText(listBox5.SelectedItem))).TurnOff();
        }

        private void BtnTesteProcessManager_Click(object sender, EventArgs e)
        {
            ProcessManager pm = new ProcessManager(listProcessos[0]);

            Processo p = null;

            Terminal.AppendText("\nIniciando Teste do ProcessManager\n");

            while (true)
            {
                p = pm.Next();

                if (p != null)
                {
                    Terminal.AppendText("retorno: " + p.Name + " / " + p.InProcess + "\n");
                    listProcessos.Where(x => x.getFather() == null).ToList().ForEach(y => y.GetInternalOrderProcess().ForEach(x => escreverListWithInP(x)));
                }
                else
                {
                    break;
                }
            }

            Terminal.AppendText("Lista do processos depois da conclusão\n");

            listProcessos.Where(x => x.getFather() == null).ToList().ForEach(y => y.GetInternalOrderProcess().ForEach(x => escreverListWithInP(x)));

            Terminal.AppendText("\nFim do Teste do ProcessManager\n");
        }

        private void escreverListWithInP(Processo x)
        {
            for (int i = 0; i < x.Cascade; i++)
            {
                Terminal.AppendText("   ");
                if (i == x.Cascade - 1)
                {
                    Terminal.AppendText("ட");
                }
            }
            Terminal.AppendText(x.Cascade + " | " + x.Name + " / " + x.InProcess + "\n");
        }

        private void BtnInsertEinE_Click(object sender, EventArgs e)
        {

        }

        private void BtnPreLoadEsteiras_Click(object sender, EventArgs e)
        {
            for (int i = listEsteiras.Count - 1; i >= 0; i--)
            {
                listEsteiras.RemoveAt(i);
            }

            for (int i = 0; i < 10; i++)
            {
                listEsteiras.Add(new EsteiraModel("Teste " + i, 5));
            }

            SetableOutput sa = (SetableOutput)listEsteiras[0];

            sa.insertAfter(listEsteiras[1]);

            attAllListBox();

            Terminal.AppendText("Sistema pré-carregado com esteiras e limite de 5\n");
        }

        private void Terminal2_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            Terminal2.SelectionStart = Terminal2.Text.Length;
            // scroll it automatically
            Terminal2.ScrollToCaret();
        }

        private void BtnListEsterias_Click(object sender, EventArgs e)
        {
            List<EsteiraAbstrata> l = listEsteiras.FindAll(x => x.EsteiraInput.Count == 0);

            for (int i = 0; i < l.Count; i++)
            {
                //  ManagePrint.listEsteiraRecursivo(l[i]);
            }
        }
    }

    class ManagePrint
    {
        static int comprimento = 0;
        static int inside = 0;

        static EsteiraAbstrata[,] mtxE;
        static int wE = 0;
        static int hE = 1;

        public static void listEsteiraRecursivo(EsteiraAbstrata e)
        {
            mtxReset();
            runNext(e);

            wE = inside + comprimento;

            mtxE = new EsteiraAbstrata[hE, wE];
        }

        private static void runNext(EsteiraAbstrata e)
        {
            runBack(e, (comprimento * -1));
            comprimento++;
            if (e.EsteiraOutput != null)
            {
                runNext(e.EsteiraOutput);
            }
        }

        private static void runBack(EsteiraAbstrata e, int ccd)
        {
            if (e.EsteiraInput.Count != 0)
            {
                if (e.EsteiraInput.Count > hE)
                {
                    hE = e.EsteiraInput.Count;
                }

                for (int i = 0; i < e.EsteiraInput.Count; i++)
                {
                    runBack(e.EsteiraInput[i], ccd + 1);
                }
            }

            if (ccd > inside)
            {
                inside = ccd;
            }
        }

        static string[,] mtx;
        static int mtxHeigth = 0;
        static int mtxWidth = 0;

        private static void mtxReset()
        {
            mtxHeigth = 0;
            mtxWidth = 0;
            hE = 1;
            comprimento = 0;
            mtx = null;
        }

        private static void mtxWriteln(string msg)
        {

        }
    }
}
