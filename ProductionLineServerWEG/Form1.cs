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
        }

        private void attListBox1()
        {
            listBox1.DataSource = listProcessos.Where(x => x.getFather() == null).Select(x => x.Name).ToList();
        }

        private void attListBox2()
        {

            listBox2.DataSource = listProcessos.Where(x => x.GetFathersProcess().Find(y => y.Name.Equals(listBox1.GetItemText(listBox1.SelectedItem))) == null).Select(x => x.Name).ToList();
        }

        private void Terminal_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            Terminal.SelectionStart = Terminal.Text.Length;
            // scroll it automatically
            Terminal.ScrollToCaret();
        }
    }
}
