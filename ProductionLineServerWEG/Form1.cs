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
            Terminal.AppendText(x.Order + " | ");
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
            listEsteiras.Find(x => x.Name.Equals(listBox4.GetItemText(listBox4.SelectedItem))).insertMasterProcess(listProcessos.Find(x => x.Name.Equals(listBox3.GetItemText(listBox3.SelectedItem))));

            Terminal.AppendText("Processo inserido na esteira\n");

            attAllListBox();
        }

        private void BtnPreLoadProcess_Click(object sender, EventArgs e)
        {
            listProcessos.Add(new Processo(new BaseProcesso("a", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("b", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("c", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("d", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("e", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("f", "Processo qualquer", 1000)));
            listProcessos.Add(new Processo(new BaseProcesso("g", "Processo qualquer", 1000)));

            listProcessos[0].AddInternalProcess(-1, listProcessos[1]);
            listProcessos[0].AddInternalProcess(-1, listProcessos[3]);
            listProcessos[0].AddInternalProcess(-1, listProcessos[6]);

            listProcessos[1].AddInternalProcess(-1, listProcessos[2]);

            listProcessos[3].AddInternalProcess(-1, listProcessos[4]);
            listProcessos[3].AddInternalProcess(-1, listProcessos[5]);

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
    }
}
