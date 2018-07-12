using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductionLinesWEG.Models
{
    public class Program
    {
        public string AuthId { get; set; }

        public readonly List<Processo> listProcessos = new List<Processo>();
        public readonly List<EsteiraAbstrata> listEsteiras = new List<EsteiraAbstrata>();
        public readonly List<Dashboard> listDashboard = new List<Dashboard>();

        public Program(string authId)
        {
            AuthId = authId;
        }

        public void toDashboard(string message, bool critico)
        {
            listDashboard.Insert(0, new Dashboard(new DateTime(), message, critico));
            verificarDashboard();
        }

        public void toDashboard(string message)
        {
            listDashboard.Insert(0, new Dashboard(new DateTime(), message, false));
            verificarDashboard();
        }

        private void verificarDashboard()
        {
            if (listDashboard.Count > 10)
            {
                if (!listDashboard[5].Critico)
                {
                    listDashboard.RemoveAt(5);
                }
            }
        }

        public void CriaProcesso(Processo p)
        {
            listProcessos.Add(p);
            toDashboard("Processo add\n");

            attAllListBox();
        }

        public void InsertProcesso(string processo1, string processo2)
        {
            listProcessos.Find(x => x.Name.Equals(processo1)).AddInternalProcess(-1, listProcessos.Find(x => x.Name.Equals(processo2)));

            toDashboard("Processo adicionado\n");

            attAllListBox();
        }

        public void listBox1_SelectedIndexChanged()
        {
            //string value = listBox1.GetItemText(listBox1.SelectedItem);

            attListBox2();
        }

        public void attAllListBox()
        {
            attListBox1();
            attListBox2();
            attListBox3();
            attListBox4();
            attListBox5();
        }

        public void attListBox1()
        {
            //listBox1.DataSource = listProcessos.Where(x => x.getFather() == null).Select(x => x.Name).ToList();
        }

        public void attListBox2()
        {

            //listBox2.DataSource = listProcessos.Where(x => x.GetFathersProcess().Find(y => y.Name.Equals(listBox1.GetItemText(listBox1.SelectedItem))) == null).Select(x => x.Name).ToList();
        }

        public void attListBox3()
        {
            //listBox3.DataSource = listProcessos.Select(x => x.Name).ToList();
        }

        public void attListBox4()
        {
            //listBox4.DataSource = listEsteiras.Select(x => x.Name).ToList();
        }

        public void attListBox5()
        {
            //listBox5.DataSource = listEsteiras.Select(x => x.Name).ToList();
        }

        public void CriarEsteira(EsteiraAbstrata e)
        {
            listEsteiras.Add(e);

            toDashboard("Esteira add\n");

            attAllListBox();
        }

        public void InserirPinE(string processo, string esteira)
        {
            EsteiraModel em = (EsteiraModel)listEsteiras.Find(x => x.Name.Equals(esteira));

            em.insertMasterProcess(listProcessos.Find(x => x.Name.Equals(processo)));

            toDashboard("Processo inserido na esteira\n");

            attAllListBox();
        }

        public void PreLoadProcess()
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

            toDashboard("Sistema pré-carregado com processos\n");
        }

        public void InsertPiece(string esteira)
        {
            Peca pc = new Peca();

            if (listEsteiras.Find(x => x.Name.Equals(esteira)).InsertPiece(pc))
            {
                toDashboard("Peça inserida na esteira selecionada\n");
            }
            else
            {
                toDashboard("Esteira lotada, não é possivel inserir mais peças\n");
            }
        }

        public void LigarEsteira(string esteira)
        {
            listEsteiras.Find(x => x.Name.Equals(esteira)).TurnOn(this);
        }

        public void DesligarEsteira(string esteira)
        {
            listEsteiras.Find(x => x.Name.Equals(esteira)).TurnOff();
        }

        public void PreLoadEsteiras()
        {
            for (int i = listEsteiras.Count - 1; i >= 0; i--)
            {
                listEsteiras.RemoveAt(i);
            }

            for (int i = 0; i < 10; i++)
            {
                listEsteiras.Add(new EsteiraModel("Teste " + i, "", 5));
            }

            SetableOutput sa = (SetableOutput)listEsteiras[0];

            sa.insertAfter(listEsteiras[1]);

            attAllListBox();

            toDashboard("Sistema pré-carregado com esteiras e limite de 5\n");
        }

        public List<Processo> getProcessoToClient()
        {
            Processo p = new Processo(new BaseProcesso("x", "x", 0));

            p.insertList(listProcessos.Where(x => x.Father == null).ToList());

            List<Processo> lp = new List<Processo>();

            lp = p.CloneList();

            ListWithOutFather(lp);

            return lp.Where(x => x.Father == null).ToList();
        }

        public List<string> listFatherProcess(string processName)
        {
            return listProcessos.Where(x => x.GetFathersProcess().Find(y => y.Name.Equals(processName)) == null).Select(x => x.Name).ToList();
        }

        private void ListWithOutFather(List<Processo> p)
        {
            for (int i = 0; i < p.Count; i++)
            {
                p[i].alterFather();
                ListWithOutFather(p[i].ListProcessos);
            }
        }











        public ListEsteiraClient getEsteirasToClient()
        {
            ListEsteiraClient list = new ListEsteiraClient();

            listEsteiras.FindAll(x => x is EsteiraModel).ForEach(x => list.listModel.Add((EsteiraModel)x));
            listEsteiras.FindAll(x => x is EsteiraArmazenamento).ForEach(x => list.listArmazenamento.Add((EsteiraArmazenamento)x));
            listEsteiras.FindAll(x => x is EsteiraEtiquetadora).ForEach(x => list.listEtiquetadora.Add((EsteiraEtiquetadora)x));
            listEsteiras.FindAll(x => x is EsteiraDesvio).ForEach(x => list.listDesvio.Add((EsteiraDesvio)x));

            return list;
        }
    }

    public class ListEsteiraClient
    {
        public List<EsteiraModel> listModel { get; private set; }
        public List<EsteiraArmazenamento> listArmazenamento { get; private set; }
        public List<EsteiraEtiquetadora> listEtiquetadora { get; private set; }
        public List<EsteiraDesvio> listDesvio { get; private set; }

        public ListEsteiraClient()
        {
            listModel = new List<EsteiraModel>();
            listArmazenamento = new List<EsteiraArmazenamento>();
            listEtiquetadora = new List<EsteiraEtiquetadora>();
            listDesvio = new List<EsteiraDesvio>();
        }
    }
}
