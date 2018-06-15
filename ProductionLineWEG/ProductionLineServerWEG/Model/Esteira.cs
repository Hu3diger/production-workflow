using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLineServerWEG
{

    abstract class EsteiraAbstrata
    {
        private static int _countId = 1;
        
        private int _inputUse;

        protected Queue<Peca> _queueInputPecas;
        protected Queue<Peca> _queueOutputPecas;

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
            }

            return !BlockedEsteira;
        }

        //public void EnterInNextProcess()
        //{
        //    _controlProcess.OrderBy(x => x.Order).ToList().ForEach(x => 
        //    {
        //        if (x.Order != 0)
        //        {
        //
        //        }
        //        else
        //        {
        //            if (x.Process.CurrentPiece != null)
        //            {
        //                setInProcess(x, _queueInputPecas.Dequeue());
        //            }
        //        }
        //    });
        //}
        //
        //protected Boolean OutputPieceSuccess(EsteiraAbstrata esteira)
        //{
        //    if (!esteira.BlockedEsteira)
        //    {
        //        esteira.InsertPiece(_queuePecas.Dequeue());
        //        _produced++;
        //        _success++;
        //        _inputUse--;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //
        //protected Boolean OutputPieceFail(EsteiraAbstrata esteira)
        //{
        //    if (!esteira.BlockedEsteira)
        //    {
        //        esteira.InsertPiece(_queuePecas.Dequeue());
        //        _produced++;
        //        _fail++;
        //        _inputUse--;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //
        //protected Boolean OutputPieceDefault(EsteiraAbstrata esteira, Boolean success)
        //{
        //    if (!esteira.BlockedEsteira)
        //    {
        //        esteira.InsertPiece(_queuePecas.Dequeue());
        //        _produced++;
        //        _fail++;
        //        _inputUse--;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Retorna a primeira peça na fila da esteira sem remove-la da fila
        /// </summary>
        /// <returns>
        /// Primeira peça da fila
        /// </returns>
        public Peca GetInputPieceNoRemove()
        {
            return _queueInputPecas.Peek();
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
        public void TurnOn()
        {
            Ligado = true;
        }
        /// <summary>
        /// Desliga a esteira e impossibilita o trabalho dela
        /// </summary>
        public void TurnOff()
        {
            Ligado = false;
        }
    }

    class EsteiraModel : EsteiraAbstrata
    {
        public EsteiraModel(string name, int limite) : base(name, limite)
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
    }

    class EsteiraVerificacao : EsteiraAbstrata
    {
        public EsteiraVerificacao(string name, int limite) : base(name, limite)
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
    }
}
