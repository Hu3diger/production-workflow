using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ProductionLinesWEG.Hub;

namespace ProductionLinesWEG.Models
{
    // classe usada para todos os tipos de esteiras
    public abstract class EsteiraAbstrata : ICloneable
    {

        protected Queue<Peca> _queueInputPecas;

        private Thread thread;

        public List<EsteiraAbstrata> EsteiraInput { get; private set; }


        public string Id { get; set; }
        public bool Ligado { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InLimit { get; set; }
        public List<int> listTime;
        public int Fail { get; protected set; }
        public int Success { get; protected set; }

        public bool IsClone { get; set; }

        public bool BlockedEsteira { get => CountInputPieces() >= InLimit && InLimit != -1; }

        /// <summary>
        /// Construtor abstrato que recebe um nome o limite de entrada na esteira
        /// </summary>
        /// <param name="name">Nome da esteira</param>
        /// <param name="limite"></param>
        public EsteiraAbstrata(string name, string description, int limite)
        {
            Name = name;
            Description = description;

            Ligado = false;
            IsClone = false;

            InLimit = limite;

            Fail = 0;
            Success = 0;

            _queueInputPecas = new Queue<Peca>();

            EsteiraInput = new List<EsteiraAbstrata>();
            
            listTime = new List<int>();
        }

        public int getProduced()
        {
            return Success + Fail;
        }
        /// <summary>
        /// Insere uma peça na lista e retorna se foi possivel ou não
        /// </summary>
        /// <param name="peca">Peca a ser inserida na esteira</param>
        /// <returns>
        /// TRUE para sim
        /// FALSE para não
        /// </returns>
        public Boolean InsertPiece(Peca peca)
        {
            if (!BlockedEsteira)
            {
                if (peca != null)
                {
                    _queueInputPecas.Enqueue(peca);
                }
                return true;
            }

            return false;
        }
        /// <summary>
        /// seta a esteira (e) como entrada (adicionando ela na lista)
        /// </summary>
        public void InsertInput(EsteiraAbstrata e)
        {
            int i;
            for (i = 0; i < EsteiraInput.Count; i++)
            {
                if (EsteiraInput[i].Id == e.Id)
                {
                    if (EsteiraInput[i] == e)
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception("Id equals but Object is not equal");
                    }
                }
            }

            if (i == EsteiraInput.Count)
            {
                EsteiraInput.Add(e);
            }
            else
            {
                EsteiraInput[i] = e;
            }
        }
        /// <summary>
        ///  remove a esteira (e) da lsita de inputs, caso exista
        /// </summary>
        /// <param name="e"></param>
        public void RemoveAllInput()
        {
            EsteiraInput.Clear();
        }
        /// <summary>
        /// metodo abstrato implementado pelas filhas
        /// Insere a esteira na lista de Outputs
        /// </summary>
        /// <param name="e"></param>
        public abstract void InsertOutput(EsteiraAbstrata e);
        /// <summary>
        /// metodo abstrato implementado pelas filhas
        /// Remove todas as esteiras de saida
        /// </summary>
        public abstract void RemoveAllOutput();
        /// <summary>
        /// remove a primeira peça da fila e a retorna
        /// </summary>
        public Peca RemovePiece()
        {
            Peca p = null;

            if (_queueInputPecas.Count > 0)
            {
                p = _queueInputPecas.Dequeue();
            }

            return p;
        }
        /// <summary>
        /// Retorna a primeira peça na fila da esteira sem remove-la da fila
        /// </summary>
        /// <returns>
        /// Primeira peça da fila
        /// </returns>
        public Peca GetInputPieceNoRemove()
        {
            if (CountInputPieces() != 0)
            {
                return _queueInputPecas.Peek();
            }

            return null;
        }
        /// <summary>
        /// Retorna o total de peça na fila da esteira.
        /// </summary>
        /// <returns>
        /// Total de peça na fila da esteira.
        /// </returns>
        public int CountInputPieces()
        {
            return _queueInputPecas.Count;
        }
        /// <summary>
        /// Liga a esteira e possibilita o trabalho dela
        /// </summary>
        public void TurnOn(Program program, string clientId, bool showTurnOnMessage)
        {
            CleanThread();

            Threads t = new Threads(this, program, clientId, showTurnOnMessage);

            thread = new Thread(t.threadEsteira);

            thread.Start();
        }
        /// <summary>
        /// encerra (caso esteja inicializada) e deleta a thread
        /// </summary>
        private void CleanThread()
        {
            if (thread != null)
            {
                thread.Abort();
            }

            thread = null;
        }
        /// <summary>
        /// Desliga a esteira e impossibilita o trabalho dela
        /// </summary>
        public void TurnOff(Program pgm)
        {
            if (Ligado)
            {
                Ligado = false;

                CleanThread();

                if (GetInputPieceNoRemove() != null)
                {
                    GetInputPieceNoRemove().ListAtributos.ForEach(x =>
                    {
                        if (x.Estado.Equals(Atributo.FAZENDO))
                        {
                            x.Estado = Atributo.INTERROMPIDO;
                            Fail++;
                        }
                    });
                }

                pgm.CheckSimulation();
            }
        }

        public List<Peca> getPiecesToList()
        {
            return _queueInputPecas.ToList();
        }

        public object Clone()
        {
            if (!IsClone)
            {
                _queueInputPecas = new Queue<Peca>();
                listTime = new List<int>();
            }
            EsteiraAbstrata e = (EsteiraAbstrata)this.MemberwiseClone();

            e.EsteiraInput = new List<EsteiraAbstrata>();
            e.RemoveAllOutput();

            e.IsClone = true;

            return ImplementedClone(e);
        }

        public abstract bool PassPiece();

        public abstract bool IsInCondition();

        protected abstract object ImplementedClone(EsteiraAbstrata e);

        public override string ToString()
        {
            return this.Name + " id " + this.Id;
        }
    }

    // classe usada para as esteiras que implementa apenas uma esteira de saida
    public abstract class SetableOutput : EsteiraAbstrata
    {
        public EsteiraAbstrata EsteiraOutput { get; private set; }

        public SetableOutput(string name, string description, int limite) : base(name, description, limite)
        {
        }

        public override void InsertOutput(EsteiraAbstrata e)
        {
            if (EsteiraOutput == null)
            {
                EsteiraOutput = e;
            }
            else
            {
                throw new Exception("Essa esteira ja contem uma saída");
            }
        }

        public override void RemoveAllOutput()
        {
            if (EsteiraOutput != null)
            {
                EsteiraOutput.EsteiraInput.Remove(this);
            }
            EsteiraOutput = null;
        }

        public override bool PassPiece()
        {
            if (EsteiraOutput != null && !EsteiraOutput.BlockedEsteira)
            {
                ImplementedFailSuccess();

                EsteiraOutput.InsertPiece(this.RemovePiece());

                return true;
            }

            return false;
        }

        public abstract void ImplementedFailSuccess();
    }
    // classe usada para as esteiras que possuem processos internos
    public class EsteiraModel : SetableOutput
    {
        private static int _countId = 0;

        private Processo _processMaster;
        private ProcessManager _processManager;

        public string NameProcessMaster { get => _processMaster.Name; }

        public EsteiraModel(string id, string name, string description, int limite) : base(name, description, limite)
        {
            if (id.Equals(""))
            {
                Id = "em" + (_countId++);
            }
            else
            {
                Id = id;
            }
        }
        /// <summary>
        /// insere um processo como processo master da esteira (o processo que a esteira controlará)
        /// </summary>
        /// <param name="p"></param>
        public void insertMasterProcess(Processo p)
        {
            _processMaster = (Processo)p.Clone();
            _processManager = new ProcessManager(_processMaster);
        }
        /// <summary>
        /// verifica se a esteira esta em condição de operação
        /// </summary>
        public override bool IsInCondition()
        {
            return _processManager != null && _processMaster != null;
        }
        /// <summary>
        /// verifica se possu um proximo processo para a operação
        /// </summary>
        public bool HasNextProcess()
        {
            return _processManager.hasNext();
        }
        /// <summary>
        /// reseta o processo para que possa ser usado novamente
        /// </summary>
        public void FinalizeProcess()
        {
            _processManager.Reset();
        }
        /// <summary>
        /// retorna o proximo processo a ser executado na operação da esteira
        /// </summary>
        /// <returns>null caso nao tenha mais</returns>
        public Processo NextProcess()
        {
            return _processManager.Next();
        }
        /// <summary>
        /// Emplementação do Clone para cada Classe
        /// </summary>
        /// <returns></returns>
        protected override object ImplementedClone(EsteiraAbstrata e)
        {
            ((EsteiraModel)e)._processMaster = (Processo)this._processMaster.Clone("c");
            ((EsteiraModel)e)._processManager = new ProcessManager(((EsteiraModel)e)._processMaster);

            return e;
        }

        public override void ImplementedFailSuccess()
        {

            Peca peca = this.GetInputPieceNoRemove();

            if (peca != null)
            {
                bool t = true;
                int time = 0;
                FinalizeProcess();

                while (HasNextProcess())
                {
                    Processo process = NextProcess();

                    Atributo at = peca.ListAtributos.Find(x => x.IdP.Equals(process.Id));
                    if (at != null)
                    {
                        if (!at.Estado.Equals(Atributo.FEITO) && !at.Estado.Equals(Atributo.ESPERANDO))
                        {
                            t = false;
                        }
                        time += at.Time;
                    }

                }

                FinalizeProcess();

                if (t)
                {
                    Success++;
                }
                else
                {
                    Fail++;
                }

                listTime.Add(time);
            }
        }
    }

    // classe usada para criar uma esteira que armazena as peças ate um determinado limite
    public class EsteiraArmazenamento : SetableOutput
    {
        private static int _countId = 0;

        public EsteiraArmazenamento(string id, string name, string description, int limite) : base(name, description, limite)
        {
            if (id.Equals(""))
            {
                Id = "ea" + (_countId++);
            }
            else
            {
                Id = id;
            }
        }
        /// <summary>
        /// remove todas as peças da lista
        /// </summary>
        public void DropAllPiece()
        {
            _queueInputPecas.Clear();
        }
        /// <summary>
        /// lista todas as peças
        /// </summary>
        public List<Peca> List()
        {
            return _queueInputPecas.ToList();
        }
        /// <summary>
        /// verifica se a esteira esta em condição de operação
        /// </summary>
        public override bool IsInCondition()
        {
            return true;
        }
        /// <summary>
        /// Emplementação do Clone para cada Classe
        /// </summary>
        /// <returns></returns>
        protected override object ImplementedClone(EsteiraAbstrata e)
        {
            return e;
        }

        public override void ImplementedFailSuccess()
        {
            Peca peca = this.GetInputPieceNoRemove();

            if (peca != null)
            {
                Atributo at = peca.getLastAtributo();
                if (at != null)
                {
                    if (at.Estado.Equals(Atributo.FEITO) || at.Estado.Equals(Atributo.ESPERANDO))
                    {
                        Success++;
                    }
                    else
                    {
                        Fail++;
                    }

                    listTime.Add(at.Time);
                }
            }
        }
    }

    // classe usada para etiquetar as peças (atribuir um id a elas)
    public class EsteiraEtiquetadora : SetableOutput
    {
        private static int _countId = 0;

        private static List<EsteiraEtiquetadora> listE = new List<EsteiraEtiquetadora>();
        public Logins Login { get; private set; }
        public int InitialValue { get; set; }
        public int MaxTag { get; set; }
        private int currentTag;

        public EsteiraEtiquetadora(Logins login, string id, string name, string description, int limite, int initialValue) : base(name, description, limite)
        {
            if (id.Equals(""))
            {
                Id = "ee" + (_countId++);
            }
            else
            {
                Id = id;
            }

            InitialValue = initialValue;
            currentTag = initialValue;
            Login = login;
            listE.Add(this);
            MaxTag = -1;
            SetRangeTag();
        }

        private void SetRangeTag()
        {
            if (RangeIsPossible(this.Login, InitialValue))
            {
                listE.FindAll(y => y.Login.Equals(this.Login)).ForEach(x =>
                {
                    listE.FindAll(z => z.Login.Equals(this.Login)).ForEach(t =>
                    {
                        if (x.InitialValue < t.InitialValue && x.currentTag < t.InitialValue && (x.MaxTag == -1 || t.InitialValue < x.MaxTag)) x.MaxTag = t.InitialValue;
                    });
                });
            }
            else
            {
                throw new Exception("Range não possivel, outra esteira cobre esse Range");
            }
        }

        public static bool RangeIsPossible(Logins login, int initialValue)
        {
            foreach (var x in listE)
            {
                if (x.Login.Equals(login) && initialValue > x.InitialValue && initialValue < x.currentTag) return false;
            }
            return true;
        }
        /// <summary>
        /// insere uma tag na peça seguindo uma ordem de valores
        /// </summary>
        public void InsertTag()
        {
            if (GetInputPieceNoRemove().Tag == -1)
            {
                if (currentTag < MaxTag || MaxTag == -1)
                {
                    GetInputPieceNoRemove().Tag = currentTag++;
                }
                else
                {
                    throw new Exception("Range máximo atingido, sequência em '" + listE.Find(x => x.InitialValue == MaxTag).Name + "'");
                }
            }
        }
        /// <summary>
        /// esta função deve ser chamada na "deletação" da esteira para que não haja conflitos futuros
        /// </summary>
        public void Destroy()
        {
            listE.Remove(this);
        }
        /// <summary>
        /// verifica se a esteira esta em condição de operação
        /// </summary>
        public override bool IsInCondition()
        {
            return RangeIsPossible(Login, currentTag);
        }
        /// <summary>
        /// Emplementação do Clone para cada Classe
        /// </summary>
        /// <returns></returns>
        protected override object ImplementedClone(EsteiraAbstrata e)
        {
            return e;
        }

        public override void ImplementedFailSuccess()
        {

            Peca peca = this.GetInputPieceNoRemove();

            if (peca != null)
            {
                Atributo at = peca.getLastAtributo();
                if (at != null)
                {
                    if (at.Estado.Equals(Atributo.FEITO) || at.Estado.Equals(Atributo.ESPERANDO))
                    {
                        Success++;
                    }
                    else
                    {
                        Fail++;
                    }

                    listTime.Add(at.Time);
                }
            }
        }
    }

    // classe usada para desviar os processos conforme as condições passadas
    public abstract class EsteiraDesvio : EsteiraAbstrata
    {
        private static int _countId = 0;
        public List<EsteiraAbstrata> EsteiraOutput { get; private set; }
        public int tipoDesvio;

        // esteira ainda não implementada
        public EsteiraDesvio(string id, string name, string description, int limite) : base(name, description, limite)
        {
            if (id.Equals(""))
            {
                Id = "ed" + (_countId++);
            }
            else
            {
                Id = id;
            }

            EsteiraOutput = new List<EsteiraAbstrata>();
        }

        public override void InsertOutput(EsteiraAbstrata e)
        {
            if (EsteiraOutput != null)
            {
                EsteiraOutput.Add(e);
            }
            else
            {
                throw new Exception("EsteiraOutput List null");
            }
        }

        public override void RemoveAllOutput()
        {
            EsteiraOutput = new List<EsteiraAbstrata>();
        }

        /// <summary>
        /// Emplementação do Clone para cada Classe
        /// </summary>
        /// <returns></returns>
        protected override object ImplementedClone(EsteiraAbstrata e)
        {
            return e;
        }
    }

    public class EsteiraBalanceadora : EsteiraDesvio
    {
        public EsteiraBalanceadora(string id, string name, string description, int limite) : base(id, name, description, limite)
        {
            tipoDesvio = 1;
        }

        public override bool PassPiece()
        {
            EsteiraAbstrata e = null;
            EsteiraOutput.ForEach(x =>
            {
                if (e == null || (x.CountInputPieces() < e.CountInputPieces() && !x.BlockedEsteira))
                {
                    e = x;
                }
            });

            if (e != null && !e.BlockedEsteira)
            {
                Peca peca = this.RemovePiece();

                if (peca != null)
                {
                    Atributo at = peca.getLastAtributo();
                    if (at != null)
                    {
                        if (at.Estado.Equals(Atributo.FEITO) || at.Estado.Equals(Atributo.ESPERANDO))
                        {
                            Success++;
                        }
                        else
                        {
                            Fail++;
                        }

                        listTime.Add(at.Time);
                    }
                }

                return e.InsertPiece(peca);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// verifica se a esteira esta em condição de operação
        /// </summary>
        public override bool IsInCondition()
        {
            return EsteiraOutput.Count >= 1;
        }
    }

    public class EsteiraSeletora : EsteiraDesvio
    {
        public EsteiraSeletora(string id, string name, string description, int limite) : base(id, name, description, limite)
        {
            tipoDesvio = 2;
        }

        public override bool PassPiece()
        {
            Peca pc = GetInputPieceNoRemove();

            if (pc.ListAtributos.Count > 1)
            {
                Atributo at = pc.ListAtributos[pc.ListAtributos.Count - 2];

                if (at.Estado.Equals(Atributo.INTERROMPIDO) && !this.EsteiraOutput[2].BlockedEsteira)
                {
                    Fail++;
                    listTime.Add(pc.ListAtributos[pc.ListAtributos.Count - 1].Time);
                    return this.EsteiraOutput[2].InsertPiece(this.RemovePiece());
                }
                else if (at.Estado.Equals(Atributo.DEFEITO) && !this.EsteiraOutput[1].BlockedEsteira)
                {
                    Fail++;
                    listTime.Add(pc.ListAtributos[pc.ListAtributos.Count - 1].Time);
                    return this.EsteiraOutput[1].InsertPiece(this.RemovePiece());
                }
            }

            if (!this.EsteiraOutput[0].BlockedEsteira)
            {
                Success++;
                listTime.Add(pc.ListAtributos[pc.ListAtributos.Count - 1].Time);
                return this.EsteiraOutput[0].InsertPiece(this.RemovePiece());
            }

            return false;
        }
        /// <summary>
        /// verifica se a esteira esta em condição de operação
        /// </summary>
        public override bool IsInCondition()
        {
            return EsteiraOutput.Count == 3;
        }
    }
}

