using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProductionLinesWEG.Models
{
    // classe usada para todos os tipos de esteiras
    public abstract class EsteiraAbstrata : ICloneable
    {

        protected Queue<Peca> _queueInputPecas;

        private Thread thread;

        public List<EsteiraAbstrata> EsteiraInput { get; private set; }


        public string Id { get; set; }
        public bool Ligado { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InLimit { get; private set; }
        public int Fail { get; private set; }
        public int Success { get; private set; }
        public int Produced { get; private set; }

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

            Produced = 0;
            Fail = 0;
            Success = 0;

            _queueInputPecas = new Queue<Peca>();

            EsteiraInput = new List<EsteiraAbstrata>();
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
            Peca p = _queueInputPecas.Dequeue();

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
        public void TurnOn(Program program)
        {
            CleanThread();

            Threads t = new Threads(this, program);

            thread = new Thread(t.threadEsteira);

            thread.Start();

            Ligado = true;
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
        public void TurnOff()
        {
            Ligado = false;

            CleanThread();

            if (GetInputPieceNoRemove() != null)
            {
                GetInputPieceNoRemove().ListAtributos.ForEach(x =>
                {
                    if (x.Estado.Equals(Atributo.FAZENDO) || x.Estado.Equals(Atributo.ESPERANDO))
                    {
                        x.Estado = Atributo.INTERROMPIDO;
                    }
                });
            }
        }

        public List<Peca> getToList()
        {
            return _queueInputPecas.ToList();
        }

        public object Clone()
        {
            TurnOff();
            EsteiraAbstrata e = (EsteiraAbstrata)this.MemberwiseClone();

            e.EsteiraInput = new List<EsteiraAbstrata>();
            e.RemoveAllOutput();

            e.IsClone = true;

            return ImplementedClone(e);
        }

        protected abstract object ImplementedClone(EsteiraAbstrata e);
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

        public Peca PassPiece()
        {
            Peca peca = null;
            if (!EsteiraOutput.BlockedEsteira)
            {
                peca = this.RemovePiece();
                EsteiraOutput.InsertPiece(peca);
            }

            return peca;
        }
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
        public bool IsInCondition()
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
        public void ResetProcess()
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
        /// finaliza o processo para que possa ser usado novamente
        /// </summary>
        public void FinalizeProcess()
        {
            _processManager.finalize();
            _processManager.Reset();
        }
        /// <summary>
        /// Emplementação do Clone para cada Classe
        /// </summary>
        /// <returns></returns>
        protected override object ImplementedClone(EsteiraAbstrata e)
        {
            ((EsteiraModel)e).insertMasterProcess(this._processMaster);

            return e;
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
        /// Emplementação do Clone para cada Classe
        /// </summary>
        /// <returns></returns>
        protected override object ImplementedClone(EsteiraAbstrata e)
        {
            return e;
        }
    }

    // classe usada para etiquetar as peças (atribuir um id a elas)
    public class EsteiraEtiquetadora : SetableOutput
    {
        private static int _countId = 0;

        private static List<EsteiraEtiquetadora> listE = new List<EsteiraEtiquetadora>();
        public int InitialValue { get; set; }
        private int currentTag;

        public EsteiraEtiquetadora(string id, string name, string description, int limite, int initialValue) : base(name, description, limite)
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
            listE.Add(this);
        }
        /// <summary>
        /// insere uma tag na peça seguindo uma ordem de valores
        /// </summary>
        public void InsertTag()
        {
            OrderValues();

            if (GetInputPieceNoRemove().Tag == -1)
            {
                GetInputPieceNoRemove().Tag = currentTag++;
            }
        }
        /// <summary>
        /// processo recursivo que verifica se o valor das tags esta "disponivel"
        /// </summary>
        private void OrderValues()
        {
            foreach (var x in listE)
            {
                if (currentTag >= InitialValue && currentTag <= x.currentTag)
                {
                    currentTag = x.currentTag;
                    OrderValues();
                    break;
                };
            }
        }
        /// <summary>
        /// esta função deve ser chamada na "deletação" da esteira para que não haja conflitos futuros
        /// </summary>
        public void Destroi()
        {
            listE.Remove(this);
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

    // classe usada para desviar os processos conforme as condições passadas
    public class EsteiraDesvio : EsteiraAbstrata
    {
        private static int _countId = 0;
        public static List<EsteiraAbstrata> EsteiraOutput { get; private set; }

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
            if (EsteiraOutput == null)
            {
                EsteiraOutput.Add(e);
            }
            else
            {
                throw new Exception("EsteiraOutput null (Esteira Desvio)");
            }
        }

        public override void RemoveAllOutput()
        {
            EsteiraOutput.Clear();
        }


        void PiecePass(EsteiraAbstrata esteira)
        {
            //    OutputPieceSuccess(esteira);
        }

        void PieceError(EsteiraAbstrata esteira)
        {
            //    OutputPieceFail(esteira);
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
}

