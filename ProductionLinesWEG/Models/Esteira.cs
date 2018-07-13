using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProductionLinesWEG.Models
{
    // classe usada para todos os tipos de esteiras
    public abstract class EsteiraAbstrata
    {
        private static int _countId = 1;

        private int _inputUse;

        protected Queue<Peca> _queueInputPecas;
        protected Queue<Peca> _queueOutputPecas;

        private Thread thread;

        public EsteiraAbstrata EsteiraOutput { get; private set; }
        public List<EsteiraAbstrata> EsteiraInput { get; private set; }


        public int Id { get; private set; }
        public bool Ligado { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InLimit { get; private set; }
        public int Fail { get; private set; }
        public int Success { get; private set; }
        public int Produced { get; private set; }

        protected bool BlockedEsteira { get => _inputUse >= InLimit && InLimit != -1; }

        /// <summary>
        /// Construtor abstrato que recebe um nome o limite de entrada na esteira
        /// </summary>
        /// <param name="name">Nome da esteira</param>
        /// <param name="limite"></param>
        public EsteiraAbstrata(string name, string description, int limite)
        {
            Id = _countId++;

            Name = name;
            Description = description;

            Ligado = false;

            InLimit = limite;
            _inputUse = 0;

            Produced = 0;
            Fail = 0;
            Success = 0;

            _queueInputPecas = new Queue<Peca>();
            _queueOutputPecas = new Queue<Peca>();

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
                _queueInputPecas.Enqueue(peca);
                _inputUse++;
                return true;
            }

            return false;
        }
        /// <summary>
        /// Insere a esteira passada (e) antes da esteira atual (Insere no input)
        /// </summary>
        public void insertBefore(EsteiraAbstrata e)
        {
            e.releaseAndConnect();

            EsteiraInput.ForEach(x => x.setOutput(e));
            e.EsteiraInput = EsteiraInput;

            this.EsteiraInput = new List<EsteiraAbstrata>();
            this.setInput(e);

            e.setOutput(this);
        }
        /// <summary>
        /// seta a esteira (e) como saida
        /// </summary>
        public void setOutput(EsteiraAbstrata e)
        {
            EsteiraOutput = e;
        }
        /// <summary>
        /// seta a esteira (e) como entrada (adicionando ela na lista)
        /// </summary>
        public void setInput(EsteiraAbstrata e)
        {
            int i;
            for (i = 0; i < EsteiraInput.Count; i++)
            {
                if (EsteiraInput[i].Id == e.Id) break;
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
        public void removeInput(EsteiraAbstrata e)
        {
            int i = EsteiraInput.FindIndex(x => x.Id == e.Id);

            if (i != -1)
            {
                EsteiraInput.RemoveAt(i);
            }
        }
        /// <summary>
        /// desprende a esteira atual das suas vizinhas e as conectas
        /// </summary>
        public void releaseAndConnect()
        {
            if (EsteiraOutput != null)
            {
                EsteiraOutput.EsteiraInput = EsteiraInput;
            }

            EsteiraInput.ForEach(x => x.setOutput(EsteiraOutput));
        }
        /// <summary>
        /// remove a primeira peça da fila e a retorna
        /// </summary>
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
        public void TurnOn(Program program)
        {

            cleanThread();

            Threads t = new Threads(this, program);

            thread = new Thread(t.threadEsteira);

            thread.Start();

            Ligado = true;
        }
        /// <summary>
        /// encerra (caso esteja inicializada) e deleta a thread
        /// </summary>
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

            if (GetInputPieceNoRemove() != null)
            {
                GetInputPieceNoRemove().ListAtributos.ForEach(x =>
                {
                    if (x.Estado.Equals(Atributo.FAZENDO))
                    {
                        x.Estado = Atributo.INTERROMPIDO;
                    }
                });
            }
        }
    }
    
    // classe usada para as esteiras que implementa apenas uma esteira de saida
    public abstract class SetableOutput : EsteiraAbstrata
    {
        public SetableOutput(string name, string description, int limite) : base(name, description, limite)
        {
        }
        /// <summary>
        /// insere a esteira (e) depois da esteira atua
        /// </summary>
        /// <param name="e"></param>
        public void insertAfter(EsteiraAbstrata e)
        {
            e.releaseAndConnect();
            if (this.EsteiraOutput != null)
            {
                this.EsteiraOutput.removeInput(this);
                this.EsteiraOutput.setInput(e);
            }
            e.setOutput(EsteiraOutput);
            e.setInput(this);
            this.setOutput(e);
        }
    }

    // classe usada para as esteiras que possuem processos internos
    public class EsteiraModel : SetableOutput
    {

        private Processo _processMaster;
        private ProcessManager _processManager;

        public string NameProcessMaster { get => _processMaster.Name; }

        public EsteiraModel(string name, string description, int limite) : base(name, description, limite)
        {
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
        public bool hasNextProcess()
        {
            return _processManager.hasNext();
        }
        /// <summary>
        /// reseta o processo para que possa ser usado novamente
        /// </summary>
        public void resetProcess()
        {
            _processManager.Reset();
        }
        /// <summary>
        /// retorna o proximo processo a ser executado na op~eração da esteira
        /// </summary>
        /// <returns>null caso nao tenha mais</returns>
        public Processo nextProcess()
        {
            return _processManager.Next();
        }
        /// <summary>
        /// finaliza o processo para que possa ser usado novamente
        /// </summary>
        public void finalizeProcess()
        {
            _processManager.finalize();
            _processManager.Reset();
        }
    }

    // classe usada para criar uma esteira que armazena as peças ate um determinado limite
    public class EsteiraArmazenamento : SetableOutput
    {
        public EsteiraArmazenamento(string name, string description, int limite) : base(name, description, limite)
        {
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
    }

    // classe usada para etiquetar as peças (atribuir um id a elas)
    public class EsteiraEtiquetadora : SetableOutput
    {
        private static List<EsteiraEtiquetadora> listE = new List<EsteiraEtiquetadora>();
        public int InitialValue { get; set; }
        private int currentTag;

        public EsteiraEtiquetadora(string name, string description, int limite, int initialValue) : base(name, description, limite)
        {
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
    }

    // classe usada para desviar os processos conforme as condições passadas
    public class EsteiraDesvio : EsteiraAbstrata
    {
        // esteira ainda não implementada
        public EsteiraDesvio(string name, string description, int limite) : base(name, description, limite)
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
