using Microsoft.AspNet.SignalR;
using ProductionLinesWEG.Hub;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProductionLinesWEG.Models
{
    public class Program
    {
        public Logins Login { get; private set; }
        public string AuthId
        {
            get
            {
                if (Login != null)
                {
                    return Login.AuthId;
                }
                else
                {
                    return "";
                }
            }
        }
        public bool InSimulation { get; set; }
        public int InDashboard { get; set; }
        public int NivelDash { get; set; }
        private int clearTick;

        // lista apenas de leitura que armazena os objetos do programa
        // como esteiras, processos e mensagens do dashboard
        public readonly List<Processo> listProcessos = new List<Processo>();
        public readonly List<EsteiraAbstrata> listEsteiras = new List<EsteiraAbstrata>();
        public readonly IList listDashboard = ArrayList.Synchronized(new List<Dashboard>());
        public readonly IList listTickDashboard = ArrayList.Synchronized(new List<Dashboard>());

        public int IdCloneEm { get; set; }
        public int IdCloneEa { get; set; }
        public int IdCloneEe { get; set; }
        public int IdCloneEd { get; set; }

        public int MinX { get; set; }
        public int MinY { get; set; }

        public MapCell[,] ArrayMapCells { get; set; }

        public Program(Logins login)
        {
            Login = login;

            IdCloneEm = 0;
            IdCloneEa = 0;
            IdCloneEe = 0;
            IdCloneEd = 0;

            MinX = 1;
            MinY = 1;

            InSimulation = false;
            InDashboard = 0;
            NivelDash = 1;
        }
        // adiciona uma mensagem  a lista de dashboard e o quão critico é a mensagem
        public void toDashboard(string message, int nivel)
        {
            if (nivel == 3)
            {
                listDashboard.Insert(0, new Dashboard(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), message, nivel));
            }

            if (InDashboard > 0)
            {
                listTickDashboard.Add(new Dashboard(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), message, nivel));
            }
        }
        // adiciona um processo na lista e exibe uma mensagem ao dashboard
        public void CriaProcesso(Processo p)
        {
            listProcessos.Add(p);
        }
        // insere o processo 2 dentro do processo 1
        public void InsertProcesso(string processo1, string processo2)
        {
            listProcessos.Find(x => x.Name.Equals(processo1)).AddInternalProcess(-1, listProcessos.Find(x => x.Name.Equals(processo2)));
        }

        // insere a esteira na lista de esteira do programa
        public void CriarEsteira(EsteiraAbstrata e)
        {
            listEsteiras.Add(e);
        }

        // insere o processo dentro da esteira model a ser procurada
        public void InserirPinE(string processo, string esteira)
        {
            EsteiraModel em = (EsteiraModel)listEsteiras.Find(x => x.Name.Equals(esteira));

            em.insertMasterProcess(listProcessos.Find(x => x.Name.Equals(processo)));
        }

        // pré carrega alguns processos e esteiras no programa
        public void PreLoadProgram()
        {

            listProcessos.Clear();

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
            listProcessos.Add(new Processo(new BaseProcesso("teste de erro", "Processo para teste de erro", 500)));

            listProcessos[0].AddInternalProcess(-1, listProcessos[1]);
            listProcessos[0].AddInternalProcess(-1, listProcessos[3]);
            listProcessos[0].AddInternalProcess(-1, listProcessos[6]);

            listProcessos[1].AddInternalProcess(-1, listProcessos[2]);

            listProcessos[3].AddInternalProcess(-1, listProcessos[4]);
            listProcessos[3].AddInternalProcess(-1, listProcessos[5]);

            listProcessos[6].AddInternalProcess(-1, listProcessos[7]);

            listProcessos[7].AddInternalProcess(-1, listProcessos[8]);
            listProcessos[7].AddInternalProcess(-1, listProcessos[9]);

            listProcessos[10].BaseProcesso.ErrorProbability = 50;

            toDashboard("Sistema pré-carregado com processos\n", 1);





            // pré-load das esteiras

            listEsteiras.Clear();

            listEsteiras.Add(new EsteiraModel("", "Esteira com Processo a", "Executa o processo a", 5));
            listEsteiras.Add(new EsteiraModel("", "Esteira com Processo b", "Executa o processo b", 2));
            listEsteiras.Add(new EsteiraModel("", "Esteira com Processo c", "Executa o processo c", 3));
            listEsteiras.Add(new EsteiraModel("", "Esteira com Processo d", "Executa o processo d", 1));
            listEsteiras.Add(new EsteiraModel("", "Esteira com Processo teste", "Executa o processo teste de erro", 5));

            ((EsteiraModel)listEsteiras[0]).insertMasterProcess(listProcessos[0]);

            ((EsteiraModel)listEsteiras[1]).insertMasterProcess(listProcessos[1]);

            ((EsteiraModel)listEsteiras[2]).insertMasterProcess(listProcessos[2]);

            ((EsteiraModel)listEsteiras[3]).insertMasterProcess(listProcessos[3]);

            ((EsteiraModel)listEsteiras[4]).insertMasterProcess(listProcessos[10]);

            listEsteiras.Add(new EsteiraEtiquetadora(this.Login, "", "Esteira Etiquetadora", "Atribui uam tag com inicio 10000...", 1, 10000));

            listEsteiras.Add(new EsteiraArmazenamento("", "Amazem 1 100pc", "Armazena X itens na esteira", 100));
            listEsteiras.Add(new EsteiraArmazenamento("", "Amazem 2 10pc", "Armazena X itens na esteira", 10));
            listEsteiras.Add(new EsteiraArmazenamento("", "Amazem 3 50pc", "Armazena X itens na esteira", 50));
            listEsteiras.Add(new EsteiraArmazenamento("", "Amazem 4 Xpc", "Armazena infinitos itens na esteira", -1));
            
            listEsteiras.Add(new EsteiraBalanceadora("", "Esteira Balanceadora", "Balanceia as peças nas esteiras de saída", 5));
            listEsteiras.Add(new EsteiraSeletora("", "Esteira Seletora", "Divide entre 3 saidas de acordo com os atributos do último processo", 5));

            toDashboard("Sistema pré-carregado com esteiras\n", 1);
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

        // converte a lsita de processos para uma forma com que depois
        // possa ser convertida em json sem entrar em loop recurssivo
        public List<Processo> getProcessoToClient()
        {
            Processo p = new Processo(new BaseProcesso("x", "x", 0), true);

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

            listEsteiras.FindAll(x => x is EsteiraModel && !x.IsClone).ForEach(x => list.listModel.Add((EsteiraModel)x));
            listEsteiras.FindAll(x => x is EsteiraArmazenamento && !x.IsClone).ForEach(x => list.listArmazenamento.Add((EsteiraArmazenamento)x));
            listEsteiras.FindAll(x => x is EsteiraEtiquetadora && !x.IsClone).ForEach(x => list.listEtiquetadora.Add((EsteiraEtiquetadora)x));
            listEsteiras.FindAll(x => x is EsteiraDesvio a && !x.IsClone).ForEach(x => list.listDesvio.Add((EsteiraDesvio)x));

            return list;
        }







        // metodo que o servidor chama para mapear e atribuir esteiras de Output e Input
        public void mapeamentoEsteiras(MapCell[,] mapCells)
        {
            List<EsteiraAbstrata> listEsteiraAux = new List<EsteiraAbstrata>();

            listEsteiras.ForEach(x =>
            {
                x.RemoveAllInput();
                x.RemoveAllOutput();

                if (x.IsClone) listEsteiraAux.Add(x);
            });

            for (int i = 0; i < mapCells.GetLength(0); i++)
            {

                for (int j = 0; j < mapCells.GetLength(1); j++)
                {
                    // como retorna um "Object", é obrigado a dar um Cast para trabalhar com o objeto
                    MapCell mapCell = ((MapCell)mapCells.GetValue(i, j));
                    if (mapCell != null && (mapCell.hasClass("esteiraP") || mapCell.hasClass("conectorP")))
                    {
                        if (mapCell.Esteira == null && mapCell.hasClass("esteiraP"))
                        {
                            throw new Exception("Erro no servidor (mapeamentoTeste) mapCell não possui uma Esteira mas contem a class 'esteiraP'.");
                        }
                        mapEsteira(mapCell, null, true);

                        // remove o objeto da lista para que apenas os objetos que nao estao sendo usados sejam apagados do sistema
                        listEsteiraAux.Remove(mapCell.Esteira);
                    }
                }

            }

            // remove o objeto de sobra da lista principal
            listEsteiraAux.ForEach(x => listEsteiras.Remove(x));

            this.ArrayMapCells = mapCells;
        }

        // metodo usado com recursividade para encontrar o caminho atravez dos conectores ate o destino (Esteiras de Output)
        private List<MapCell> mapEsteira(MapCell mapCell, MapCell previousMapCell, bool first)
        {
            List<MapCell> listMapCell = new List<MapCell>();

            if (mapCell.hasClass("esteiraP"))
            {
                // verifica se a esteira contem algum conector inadequaldo ao seu redor
                if (mapCell.Up != null && !mapCell.Up.hasClass("esteiraP") && mapCell.Up.hasClass("reciveCup"))
                {
                    throw new Exception("Erro de compilação, esteiras não possuem saída superior (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                }

                if (mapCell.Down != null && !mapCell.Down.hasClass("esteiraP") && mapCell.Down.hasClass("reciveCdown"))
                {
                    throw new Exception("Erro de compilação, esteiras não possuem saída inferior (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                }

                // verifica se é a primeira esteira da recursividade
                if (first)
                {
                    // como é a primeira esteira, verifica se há algo na frente
                    // e inicia o processo recursivo
                    if (mapCell.Front != null)
                    {
                        mapEsteira(mapCell.Front, mapCell, false).ForEach(x => listMapCell.Add(x));
                    }

                    List<MapCell> listAux = new List<MapCell>();

                    // usa uma lista auxiliar para remover as repetições de objetos gerados pelo mapeamento
                    listMapCell.ForEach(x =>
                    {
                        if (!listAux.Contains(x)) listAux.Add(x);
                    });

                    listMapCell = listAux;

                    // adiciona o output na esteira atual
                    // e o input na esteira da lista
                    try
                    {
                        listMapCell.ForEach(x =>
                        {
                            x.Esteira.InsertInput(mapCell.Esteira);
                            mapCell.Esteira.InsertOutput(x.Esteira);
                        });
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message + " (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                    }
                }
                else
                {
                    // adiciona a celula a lista para que no termino da recursividade possa ser ligada a outra esteira
                    listMapCell.Add(mapCell);
                }
            }
            else if (mapCell.hasClass("conectorP"))
            {
                // verifica se o conector tem saida superior
                if (mapCell.hasClass("Cup") || mapCell.hasClass("reciveCdown"))
                {
                    if (mapCell.Up != null)
                    {
                        // verifica se a celula atual é igual a anterior para que não haja loop na recursividade
                        if (mapCell.Up != previousMapCell)
                        {
                            // verifica se a celula superior pode receber ou "dar" para a celula atual e abre a recursividade
                            // senão dispara um throw para exibir um erro ao usuario
                            if (mapCell.Up.hasClass("Cdown") || mapCell.Up.hasClass("reciveCup") || (mapCell.hasClass("Cup") && mapCell.Up.hasClass("esteiraP")))
                            {
                                mapEsteira(mapCell.Up, mapCell, false).ForEach(x => listMapCell.Add(x));
                            }
                            else
                            {
                                throw new Exception("Erro de compilação, saída superior inadequada (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Erro de compilação, saída superior não definida (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                    }
                }

                // verifica se o conector tem saida frontal
                if (mapCell.hasClass("Cfront"))
                {
                    if (mapCell.Front != null)
                    {
                        // verifica se a celula atual é igual a anterior para que não haja loop na recursividade
                        if (mapCell.Front != previousMapCell)
                        {
                            // verifica se a celula frontal pode receber ou "dar" para a celula atual e abre a recursividade
                            // senão dispara um throw para exibir um erro ao usuario
                            if (mapCell.Front.hasClass("reciveCfront") || mapCell.Front.hasClass("esteiraP"))
                            {
                                mapEsteira(mapCell.Front, mapCell, false).ForEach(x => listMapCell.Add(x));
                            }
                            else
                            {
                                throw new Exception("Erro de compilação, saída frontal inadequada (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Erro de compilação, saída frontal não definida (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                    }
                }

                // verifica se o conector tem saida inferior
                if (mapCell.hasClass("Cdown") || mapCell.hasClass("reciveCup"))
                {
                    if (mapCell.Down != null)
                    {
                        // verifica se a celula atual é igual a anterior para que não haja loop na recursividade
                        if (mapCell.Down != previousMapCell)
                        {
                            // verifica se a celula inferior pode receber ou "dar" para a celula atual e abre a recursividade
                            // senão dispara um throw para exibir um erro ao usuario
                            if (mapCell.Down.hasClass("Cup") || mapCell.Down.hasClass("reciveCdown") || (mapCell.hasClass("Cdown") && mapCell.Down.hasClass("esteiraP")))
                            {
                                mapEsteira(mapCell.Down, mapCell, false).ForEach(x => listMapCell.Add(x));
                            }
                            else
                            {
                                throw new Exception("Erro de compilação, saída inferior inadequada (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Erro de compilação, saída inferior não definida (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                    }
                }

                // verifica se o conector tem entrada traseira
                if (mapCell.hasClass("reciveCfront"))
                {
                    if (mapCell.Back != null)
                    {
                        // verifica se a celula atual é igual a anterior para que não haja loop na recursividade
                        if (mapCell.Back != previousMapCell)
                        {
                            // verifica se a celula inferior pode receber ou "dar" para a celula atual e abre a recursividade
                            // senão dispara um throw para exibir um erro ao usuario
                            if (!mapCell.Back.hasClass("Cfront") && !mapCell.Back.hasClass("esteiraP"))
                            {
                                throw new Exception("Erro de compilação, entrada inadequada (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Erro de compilação, entrada não definida (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
                    }
                }
            }
            else
            {
                throw new Exception("Erro de compilação, Objeto não identificado (linha: " + mapCell.Row + ", Coluna: " + mapCell.Column + ")");
            }


            return listMapCell;
        }

        public void alterFatherProcess(string nameP, int pos, Processo f)
        {

            listProcessos.ForEach(x =>
            {
                if (x.Name.Equals(nameP))
                {
                    x.removerFather();
                    f.AddInternalProcess(pos - 1, x);
                }
            });

        }

        public void removeProcess(Processo p)
        {
            listProcessos.FindAll(x => x.Name.Equals(p.Name)).ForEach(y =>
            {
                // pega todos os filhos e os remove (com o proprio processo junto)
                y.GetInternalOrderProcess().ForEach(z =>
                {
                    if (ArrayMapCells != null)
                    {
                        for (int i = 0; i < ArrayMapCells.GetLength(0); i++)
                        {
                            for (int j = 0; j < ArrayMapCells.GetLength(1); j++)
                            {
                                MapCell aux = (MapCell)ArrayMapCells.GetValue(i, j);

                                if (aux != null && aux.Esteira != null && aux.Esteira is EsteiraModel && ((EsteiraModel)aux.Esteira).NameProcessMaster.Equals(z.Name))
                                {
                                    aux.Esteira = null;
                                    ArrayMapCells[i, j] = null;
                                }
                            }

                        }
                    }

                    listEsteiras.FindAll(h => h is EsteiraModel).FindAll(j => ((EsteiraModel)j).NameProcessMaster.Equals(z.Name)).ForEach(e => listEsteiras.Remove(e));
                    z.removerFather();
                    listProcessos.Remove(z);
                });
            });

        }

        public void removeEsteira(EsteiraAbstrata e)
        {
            if (ArrayMapCells != null)
            {
                for (int i = 0; i < ArrayMapCells.GetLength(0); i++)
                {
                    for (int j = 0; j < ArrayMapCells.GetLength(1); j++)
                    {
                        MapCell aux = (MapCell)ArrayMapCells.GetValue(i, j);

                        if (aux != null && aux.Esteira != null && aux.Esteira.Name.Equals(e.Name))
                        {
                            aux.Esteira = null;
                            ArrayMapCells[i, j] = null;
                        }
                    }

                }
            }

            listEsteiras.Remove(e);
        }

        public void clearListTickDashboard()
        {
            if (++clearTick >= InDashboard)
            {
                clearTick = 0;
                listTickDashboard.Clear();
            }
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










    public class MapCell : ICloneable
    {
        public string Id { get; set; }
        public string[] Classes { get; set; }
        public string Html_Children { get; set; }
        public dynamic DataObj { get; set; }

        public MapCell Up { get; set; }
        public MapCell Front { get; set; }
        public MapCell Back { get; set; }
        public MapCell Down { get; set; }

        public EsteiraAbstrata Esteira { get; set; }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public MapCell(string id, string[] classes, string html_Children, dynamic dataObj, int row, int column)
        {
            Id = id;
            Classes = classes;
            Html_Children = html_Children;
            Row = row;
            Column = column;
            DataObj = dataObj;
        }

        public bool hasClass(string className)
        {
            for (int i = 0; i < Classes.Count(); i++)
            {
                if (Classes[i].Equals(className))
                {
                    return true;
                }
            }

            return false;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
