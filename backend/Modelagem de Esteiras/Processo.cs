using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Classe Base onde conterá o nome em comum entre todos os processos, podendo ser alterado ao longo do tempo
    /// </summary>
    class BaseProcesso
    {
        public string Name { get; set; }

        public BaseProcesso(string name)
        {
            Name = name;
        }
    }

    abstract class Processo : ICloneable
    {
        private BaseProcesso _baseProcesso;
        private int _errors;
        private bool _inProcess;
        private List<Processo> _processos;
        private int _order;
        private int _cascade;

        public string Name { get => _baseProcesso.Name; }
        public int Errors { get => _errors; }
        public bool InProcess { get => _inProcess; set => _inProcess = value; }
        public int Order { get => _order; }
        public int Cascade { get => _cascade; }
        /// <summary>
        /// Construtor da classe Processo onde BaseProcesso é o nome em comum entre todos os processos criados
        /// </summary>
        /// <param name="baseProcesso">A base em comum entre todos os processos que recebem ele</param>
        public Processo(BaseProcesso baseProcesso)
        {
            _baseProcesso = baseProcesso;
            _errors = 0;
            _inProcess = false;
            _processos = new List<Processo>();
            _order = 0;
        }
        /// <summary>
        /// Incrementa em 1 os erros que conteve na "execução" do processo.
        /// </summary>
        public void IncrementError()
        {
            _errors++;
        }
        /// <summary>
        /// Retorna TRUE caso o processo atual seja um processo final (não contenha "Sub-Processos")
        /// </summary>
        /// <returns>
        /// TRUE or FALSE
        /// </returns>
        public Boolean IsFinalProcess()
        {
            return _processos.Count == 0;
        }
        /// <summary>
        /// Procura pelo processo esspecificado dentro do processso a ser executado.
        /// </summary>
        /// <param name="nameProcess">Nome do processo a ser pesquisado (Name)</param>
        public void AddInternalProcess(int index, Processo process)
        {
            process._cascade = this._cascade + 1;



            if (index != -1)
            {
                if ((index > _processos.Count && _processos.Count != 0) || index < 0)
                {
                    throw new Exception("Wrong Position");
                }

                _processos.Insert(index, process);
            }
            else
            {
                _processos.Add(process);
            }
        }
        /// <summary>
        /// Retorna o Processo especificado buscando dentro do processso a ser executado.
        /// Null caso não encontre
        /// </summary>
        /// <param name="nameProcess">Nome do processo a ser pesquisado (Name)</param>
        /// <returns>
        /// Processo
        /// Null caso não encontre
        /// </returns>
        public Processo FindInternalProcess(string nameProcess)
        {
            if (this.Name.Equals(nameProcess))
            {
                return this;
            }
            Processo p = _processos.Find(x => x.Name.Equals(nameProcess));

            if (p == null)
            {
                for (int i = 0; i < _processos.Count; i++)
                {
                    p = _processos[i].FindInternalProcess(nameProcess);

                    if (p != null)
                    {
                        break;
                    }
                }
            }
            return p;
        }
        /// <summary>
        /// Retorna uma lista de Processos em order do "Filho" (que esta sendo procurado) até p "Pai" (O objeto que esta executando está função).
        /// </summary>
        /// <param name="nameProcess">Nome do processo a ser pesquisado (Name)</param>
        /// <returns>
        /// List Processo
        /// Null caso não encontre
        /// </returns>
        public List<Processo> GetFathersProcess(string nameProcess)
        {
            List<Processo> list = new List<Processo>();

            if (this.Name.Equals(nameProcess))
            {
                list.Add(this);
            }
            else
            {
                for (int i = 0; i < _processos.Count; i++)
                {
                    _processos[i].GetFathersProcess(nameProcess).ForEach(x => list.Add(x));

                    if (list.Count != 0)
                    {
                        list.Add(this);
                        break;
                    }
                }
            }
            return list;

        }
        /// <summary>
        /// Retorna uma lista de Processos em ordem de "execução" do processo.
        /// </summary>
        /// <returns>
        /// List Processo
        /// Null caso não encontre
        /// </returns>
        public List<Processo> GetInternalOrderProcess()
        {
            List<Processo> p = new List<Processo>();

            for (int i = 0; i < _processos.Count; i++)
            {
                _processos[i].GetInternalOrderProcess();
                p.Add(_processos[i]);
            }

            return p;
        }
        /// <summary>
        /// Ordena os itens dentro da lista alterando a propriedade "index" e colocando-os em sequencia.
        /// </summary>
        /// <param name="index">valor inicial da ordenação</param>
        /// <returns>
        /// int (valor do ultimo item da lista)
        /// </returns>
        public int Reorder(int index)
        {

            _processos.ForEach(x =>
            {
                index = x.Reorder(index);
                x._order = index++;
            });

            return index;
        }
        /// <summary>
        /// Limpa a lista de processos internos.
        /// </summary>
        public void ClearList()
        {
            _processos = new List<Processo>();
        }
        /// <summary>
        /// Exibe todos os item na ordem de "execução" de Processos
        /// </summary>
        public void TestProcess()
        {
            for (int i = 0; i < _processos.Count; i++)
            {
                Processo p = _processos[i];

                p.TestProcess();

                Console.WriteLine(p.Name + " || order: " + p._order + " || index: " + i);
            }
        }
        /// <summary>
        /// Clona o objeto, incluindo os objetos na lista, mantendo os Nomes em comum na BaseProcesso dos Processos.
        /// </summary>
        /// <returns>
        /// object (cast to Processo...)
        /// </returns>
        public object Clone()
        {
            Processo p = (Processo)this.MemberwiseClone();

            p._processos = CloneList();

            return p;
        }
        /// <summary>
        /// Clona a lista de processos internos a retorna mantendo os Nomes em comum na BaseProcesso dos Processos.
        /// </summary>
        /// <returns>
        /// List Processo Clonados
        /// </returns>
        public List<Processo> CloneList()
        {
            List<Processo> p = new List<Processo>();

            for (int i = 0; i < _processos.Count; i++)
            {
                _processos[i]._processos = _processos[i].CloneList();
                p.Add((Processo)_processos[i].Clone());
            }

            return p;
        }
    }

    class ProcessoPeca : Processo
    {
        private bool _done;
        private bool _critical;

        public bool Done { get => _done; set => _done = value; }
        public bool Critical { get => _critical; }

        public ProcessoPeca(BaseProcesso baseProcess) : base(baseProcess)
        {
            _critical = false;
            _done = false;
        }
    }

    class ProcessoEsteira : Processo
    {
        private int _pass;

        public int Pass { get => _pass; }

        public ProcessoEsteira(BaseProcesso baseProcess) : base(baseProcess)
        {
            _pass = 0;
        }

        public void IncrementPass()
        {
            _pass++;
        }
    }


}
