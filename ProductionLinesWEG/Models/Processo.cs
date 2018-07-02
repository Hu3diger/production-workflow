using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLineServerWEG.Models
{
    /// <summary>
    /// Classe Base onde conterá o nome em comum entre todos os processos, podendo ser alterado ao longo do tempo
    /// </summary>
    class BaseProcesso
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Runtime { get; set; }

        public BaseProcesso(string name, string description, int runtime)
        {
            Name = name;
            Description = description;
            Runtime = runtime;
        }
    }

    class Processo : ICloneable
    {
        private BaseProcesso _baseProcesso;
        private int _errors;
        private bool _inProcess;

        public Processo _father;
        private List<Processo> _processos;
        
        private int _cascade;
        private Double _errorProbability;

        public string Name { get => _baseProcesso.Name; }
        public string Description { get => _baseProcesso.Description; }
        public int Runtime { get => _baseProcesso.Runtime; }

        public int Errors { get => _errors; }
        public bool InProcess { get => _inProcess; set => _inProcess = value; }
        public int Cascade { get => _cascade; }
        public double ErrorProbability { get => _errorProbability; set => _errorProbability = value; }

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
        /// returna quem é o pai do processo (nulo ne não tiver)
        /// </summary>
        /// <param name="p">Processo a ser declarado api</param>
        public Processo getFather()
        {
            return _father;
        }
        /// <summary>
        /// Adiciona o processo esspecificado dentro do processso a ser executado.
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

                process._father = this;

                _processos.Insert(index, process);
            }
            else
            {
                process._father = this;

                _processos.Add(process);
            }

            ReorderCascade();
        }
        public void ReorderCascade()
        {
            if (_father != null)
            {
                this._cascade = _father._cascade + 1;
            }
            else
            {
                this._cascade = 0;
            }

            _processos.ForEach(x => x.ReorderCascade());
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
        /// Retorna uma lista de Processos em order do "Filho" (O objeto que esta executando está função) até o "Pai".
        /// </summary>
        /// <param name="nameProcess">Nome do processo a ser pesquisado (Name)</param>
        /// <returns>
        /// List Processo
        /// Null caso não encontre
        /// </returns>
        public List<Processo> GetFathersProcess()
        {
            List<Processo> list = new List<Processo>();

            list.Add(this);

            if (_father != null)
            {
                _father.GetFathersProcess().ForEach(x => list.Add(x));
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

            p.Add(this);
            for (int i = 0; i < _processos.Count; i++)
            {
                _processos[i].GetInternalOrderProcess().ForEach(x => p.Add(x));
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

                Console.WriteLine(p.Name + " || index: " + i);
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

            p._father = null;

            p._processos.ForEach(x =>
            {
                x._father = p;
            });

            p.ReorderCascade();

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

            _processos.ForEach(x =>
            {
                p.Add((Processo)x.Clone());
            });

            return p;
        }
    }

    class ProcessManager
    {
        private List<Processo> _listOrdem;
        private int _ordem;

        public ProcessManager(Processo process)
        {
            _listOrdem = process.GetInternalOrderProcess();
            Reset();
        }

        public void Reset()
        {
            _ordem = 0;
            while (hasNext())
            {
                Next();
            }
            finalize();
        }

        public bool hasNext()
        {
            return _ordem < _listOrdem.Count;
        }

        public void finalize()
        {
            if (_ordem != 0)
            {
                int j;
                for (j = _ordem - 1; _listOrdem[j].Cascade != 0; j--)
                {
                    _listOrdem[j].InProcess = false;
                }

                _listOrdem[j].InProcess = false;
            }
            _ordem = 0;
        }

        public Processo Next()
        {
            if (_ordem == 0)
            {
                _listOrdem[_ordem].InProcess = true;

                _ordem++;

                if (_listOrdem[_ordem - 1].IsFinalProcess())
                {
                    return _listOrdem[_ordem - 1];
                }
            }

            for (; _ordem < _listOrdem.Count; _ordem++)
            {

                if (_listOrdem[_ordem].Cascade == _listOrdem[_ordem - 1].Cascade)
                {
                    _listOrdem[_ordem - 1].InProcess = false;
                }
                else if (_listOrdem[_ordem].Cascade < _listOrdem[_ordem - 1].Cascade)
                {
                    int i;
                    for (i = _ordem - 1; _listOrdem[i].Cascade != _listOrdem[_ordem].Cascade; i--)
                    {
                        _listOrdem[i].InProcess = false;
                    }

                    _listOrdem[i].InProcess = false;
                }

                _listOrdem[_ordem].InProcess = true;

                if (_listOrdem[_ordem].IsFinalProcess())
                {
                    _ordem++;
                    return _listOrdem[_ordem - 1];
                }
            }

            return null;
        }
    }

    class ProcessControl
    {

    }


}
