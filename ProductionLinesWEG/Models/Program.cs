using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductionLinesWEG.Models
{
    public class Program
    {
        public string AuthId { get; set; }

        // lista apenas de leitura que armazena os objetos do programa
        // como esteiras, processos e mensagens do dashboard
        public readonly List<Processo> listProcessos = new List<Processo>();
        public readonly List<EsteiraAbstrata> listEsteiras = new List<EsteiraAbstrata>();
        public readonly List<Dashboard> listDashboard = new List<Dashboard>();

        public Program(string authId)
        {
            AuthId = authId;
        }
        // adiciona uma mensagem  a lista de dashboard e o quão critico é a mensagem
        public void toDashboard(string message, bool critico)
        {
            listDashboard.Insert(0, new Dashboard(new DateTime(), message, critico));
            verificarDashboard();
        }

        // adiciona uma mensagem  a lista de dashboard sem ser critica a mensagem
        public void toDashboard(string message)
        {
            listDashboard.Insert(0, new Dashboard(new DateTime(), message, false));
            verificarDashboard();
        }
        // verifica se a lista de mensagem e as deleta caso não seja criatica depois de uma determina posição
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
        // adiciona um processo na lista e exibe uma mensagem ao dashboard
        public void CriaProcesso(Processo p)
        {
            listProcessos.Add(p);
            toDashboard("Processo add\n");

            attAllListBox();
        }
        // insere o processo 2 dentro do processo 1
        public void InsertProcesso(string processo1, string processo2)
        {
            listProcessos.Find(x => x.Name.Equals(processo1)).AddInternalProcess(-1, listProcessos.Find(x => x.Name.Equals(processo2)));

            toDashboard("Processo adicionado\n");

            attAllListBox();
        }




        // metodos para "acompanhamento" para retirada de duvidas
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
        // fim dos metodos para "acompanhamento" para retirada de duvidas




        // insere a esteira na lista de esteira do programa
        public void CriarEsteira(EsteiraAbstrata e)
        {
            listEsteiras.Add(e);

            toDashboard("Esteira add\n");

            attAllListBox();
        }

        // insere o processo dentro da esteira model a ser procurada
        public void InserirPinE(string processo, string esteira)
        {
            EsteiraModel em = (EsteiraModel)listEsteiras.Find(x => x.Name.Equals(esteira));

            em.insertMasterProcess(listProcessos.Find(x => x.Name.Equals(processo)));

            toDashboard("Processo inserido na esteira\n");

            attAllListBox();
        }

        // pré carrega alguns processos no programa
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

        // insere uma peca na esteira para ser processada
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

        // liga a esteira para iniciar os processos
        public void LigarEsteira(string esteira)
        {
            listEsteiras.Find(x => x.Name.Equals(esteira)).TurnOn(this);
        }

        // desliga a esteira e para os processos
        public void DesligarEsteira(string esteira)
        {
            listEsteiras.Find(x => x.Name.Equals(esteira)).TurnOff();
        }

        // pré carrega o programa com algumas esteiras
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

        // converte a lsita de processos para uma forma com que depois
        // possa ser convertida em json sem entrar em loop recurssivo
        public List<Processo> getProcessoToClient()
        {
            Processo p = new Processo(new BaseProcesso("x", "x", 0));

            p.insertList(listProcessos.Where(x => x.Father == null).ToList());

            List<Processo> lp = new List<Processo>();

            lp = p.CloneList();

            ListWithOutFather(lp);

            return lp.Where(x => x.Father == null).ToList();
        }

        // lista todos os pais possiveis de determinado processo
        public List<string> listFatherProcess(string processName)
        {
            return listProcessos.Where(x => x.GetFathersProcess().Find(y => y.Name.Equals(processName)) == null).Select(x => x.Name).ToList();
        }

        // remove os pais da lista de processos para que não entre em recurssividade no processo de converção
        private void ListWithOutFather(List<Processo> p)
        {
            for (int i = 0; i < p.Count; i++)
            {
                p[i].alterFather();
                ListWithOutFather(p[i].ListProcessos);
            }
        }










        // adiciona as esteiras separadas por tipo para que o cliente possa implementar com facilidade
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

    // classe usada para armazenar as esteiras separadas por tipo definido
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
