using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ControlProcess
    {
        public Processo Process { get; set; }
        public int Order { get; set; }
    }

    abstract class EsteiraAbstrata
    {
        private static int _countId = 1;

        private int _id;

        private string _name;

        private Boolean _ligado;

        private int _inLimit;
        private int _inputUse;

        private int _produced;
        private int _fail;
        private int _success;

        protected Queue<Peca> _queueInputPecas;
        protected Queue<Peca> _queueOutputPecas;

        private List<EsteiraAbstrata> _esteiraOutput;

        private List<ControlProcess> _controlProcess;


        public int Id { get => _id; }
        public bool Ligado { get => _ligado; set => _ligado = value; }
        public string Name { get => _name; set => _name = value; }
        public int InLimit { get => _inLimit; }
        public int Fail { get => _fail; }
        public int Success { get => _success; }

        protected bool BlockedEsteira { get => _inputUse >= InLimit && InLimit != -1; }

        internal List<EsteiraAbstrata> EsteiraOutput { get => _esteiraOutput; }

        public EsteiraAbstrata(string name, int limite)
        {
            _id = _countId++;

            _name = name;

            _ligado = false;

            _inLimit = limite;
            _inputUse = 0;

            _produced = 0;
            _fail = 0;
            _success = 0;

            _queueInputPecas = new Queue<Peca>();
            _queueOutputPecas = new Queue<Peca>();

            _esteiraOutput = new List<EsteiraAbstrata>();
            _controlProcess = new List<ControlProcess>();
        }

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

        //private void setInProcess(ControlProcess controlProcess, Peca peca)
        //{
        //    controlProcess.Process.CurrentPiece = peca;
        //    peca.SetInProcess(controlProcess.Process.Name, true);
        //}

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

        public Peca GetInputPieceNoRemove()
        {
            return _queueInputPecas.Peek();
        }

        public int CountInputPieces()
        {
            return _queueInputPecas.Count;
        }

        public void TurnOn()
        {
            _ligado = true;
        }

        public void TurnOff()
        {
            _ligado = false;
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
