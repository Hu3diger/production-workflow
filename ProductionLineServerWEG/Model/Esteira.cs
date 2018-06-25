using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProductionLineServerWEG
{

    abstract class EsteiraAbstrata
    {
        private static int _countId = 1;

        private int _inputUse;

        protected Queue<Peca> _queueInputPecas;
        protected Queue<Peca> _queueOutputPecas;

        private Thread thread;


        private List<EsteiraAbstrata> _esteiraOutput;


        public int Id { get; private set; }
        public bool Ligado { get; private set; }
        public string Name { get; set; }
        public int InLimit { get; private set; }
        public int Fail { get; private set; }
        public int Success { get; private set; }
        public int Produced { get; private set; }

        protected bool BlockedEsteira { get => _inputUse >= InLimit && InLimit != -1; }

        internal List<EsteiraAbstrata> EsteiraOutput { get => _esteiraOutput; }
        /// <summary>
        /// Construtor abstrato que recebe um nome o limite de entrada na esteira
        /// </summary>
        /// <param name="name">Nome da esteira</param>
        /// <param name="limite"></param>
        public EsteiraAbstrata(string name, int limite)
        {
            Id = _countId++;

            Name = name;

            Ligado = false;

            InLimit = limite;
            _inputUse = 0;

            Produced = 0;
            Fail = 0;
            Success = 0;

            _queueInputPecas = new Queue<Peca>();
            _queueOutputPecas = new Queue<Peca>();

            _esteiraOutput = new List<EsteiraAbstrata>();
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
                _queueInputPecas.Enqueue(peca);
                _inputUse++;
                return true;
            }

            return false;
        }

        public Peca RemovePiece()
        {
            Peca p = _queueInputPecas.Dequeue();

            if (p != null)
            {
                _inputUse--;
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
        public void TurnOn(Form1 f)
        {

            cleanThread();

            Threads t = new Threads(this, f);

            thread = new Thread(t.threadEsteira);

            thread.Start();

            Ligado = true;
        }

        private void cleanThread()
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

            cleanThread();

            GetInputPieceNoRemove().ListAtributos.ForEach(x => {
                if (x.Estado.Equals(Atributo.FAZENDO))
                {
                    x.Estado = Atributo.INTERROMPIDO;
                }
            });
        }

        public abstract bool executeNextProcesses();
    }

    class EsteiraModel : EsteiraAbstrata
    {

        private Processo _processMaster;
        private ProcessManager _processManager;

        public EsteiraModel(string name, int limite) : base(name, limite)
        {
        }

        public void insertMasterProcess(Processo p)
        {
            _processMaster = (Processo)p.Clone();
            _processManager = new ProcessManager(_processMaster);
        }

        public bool IsInCondition()
        {
            return _processManager != null && _processMaster != null;
        }

        public bool hasNextProcess()
        {
            return _processManager.hasNext();
        }

        public void resetProcess()
        {
            _processManager.Reset();
        }

        public Processo nextProcess()
        {
            return _processManager.Next();
        }

        public void initializeProcess()
        {
            _processManager.Reset();
        }

        public void finalizeProcess()
        {
            _processManager.finalize();
            _processManager.Reset();
        }

        public void setAttributes()
        {
            Peca pc = GetInputPieceNoRemove();

            if (pc != null)
            {
                
            }
        }

        public override bool executeNextProcesses()
        {
            return false;
        }

        public new void TurnOff()
        {

        }
    }

    class EsteiraArmazenamento : EsteiraAbstrata
    {
        public EsteiraArmazenamento(string name, int limite) : base(name, limite)
        {
        }

        public void DropAllPiece()
        {
            _queueInputPecas.Clear();
        }

        public List<Peca> List()
        {
            return _queueInputPecas.ToList();
        }

        public override bool executeNextProcesses()
        {
            return false;
        }
    }

    class EsteiraEtiquetadora : EsteiraAbstrata
    {
        private static long _tags = 100001;

        public EsteiraEtiquetadora(string name, int limite) : base(name, limite)
        {
        }

        void InsertTag()
        {
            if (GetInputPieceNoRemove().Tag == -1)
            {
                GetInputPieceNoRemove().Tag = _tags++;
            }
        }

        void PieceTagged(EsteiraAbstrata esteira)
        {
            //   OutputPieceSuccess(esteira);
        }

        public override bool executeNextProcesses()
        {
            return false;
        }
    }

    class EsteiraDesvio : EsteiraAbstrata
    {
        public EsteiraDesvio(string name, int limite) : base(name, limite)
        {
        }

        void PiecePass(EsteiraAbstrata esteira)
        {
            //    OutputPieceSuccess(esteira);
        }

        void PieceError(EsteiraAbstrata esteira)
        {
            //    OutputPieceFail(esteira);
        }

        public override bool executeNextProcesses()
        {
            return false;
        }
    }
}
