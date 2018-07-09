using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLinesWEG.Models
{
    /// <summary>
    /// Classe Base onde conterá o nome em comum entre todos os processos, podendo ser alterado ao longo do tempo
    /// </summary>
    public class BaseProcesso
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Runtime { get; set; }
        public double VariationRuntime { get; set; }
        public double ErrorProbability { get; set; }

        public BaseProcesso(string name, string description, int runtime)
        {
            Name = name;
            Description = description;
            Runtime = runtime;
        }
    }

    public class Processo : ICloneable
    {
        public BaseProcesso BaseProcesso;


        public string Name { get => BaseProcesso.Name; }
        public string Description { get => BaseProcesso.Description; }
        public int Runtime { get => BaseProcesso.Runtime; }
        public double VariationRuntime { get => BaseProcesso.VariationRuntime; }
        public double ErrorProbability { get => BaseProcesso.ErrorProbability; }

        public int Errors { get; private set; }
        public bool InProcess { get; set; }
        public int Cascade { get; private set; }
        public List<Processo> ListProcessos { get; private set; }
        public Processo Father { get; private set; }

        public int Position { get; private set; }

        /// <summary>
        /// Construtor da classe Processo onde BaseProcesso é o nome em comum entre todos os processos criados
        /// </summary>
        /// <param name="baseProcesso">A base em comum entre todos os processos que recebem ele</param>
        public Processo(BaseProcesso baseProcesso)
        {
            BaseProcesso = baseProcesso;
            Errors = 0;
            InProcess = false;
            ListProcessos = new List<Processo>();
        }
        /// <summary>
        /// Incrementa em 1 os erros que conteve na "execução" do processo.
        /// </summary>
        public void IncrementError()
        {
            Errors++;
        }
        /// <summary>
        /// Retorna TRUE caso o processo atual seja um processo final (não contenha "Sub-Processos")
        /// </summary>
        /// <returns>
        /// TRUE or FALSE
        /// </returns>
        public Boolean IsFinalProcess()
        {
            return ListProcessos.Count == 0;
        }
        /// <summary>
        /// remove o pai do processo
        /// </summary>
        public void alterFather()
        {
            if (Father != null)
            {
                Father = new Processo(new BaseProcesso(Father.Name, "", 0));
            }
        }
        /// <summary>
        /// Adiciona o processo esspecificado dentro do processso a ser executado.
        /// </summary>
        /// <param name="nameProcess">Nome do processo a ser pesquisado (Name)</param>
        public void AddInternalProcess(int index, Processo process)
        {
            process.Cascade = this.Cascade + 1;

            process.Father = this;

            if (index < 0)
            {
                ListProcessos.Insert(0, process);
            }
            else if (index < ListProcessos.Count)
            {
                ListProcessos.Insert(index, process);
            }
            else
            {
                ListProcessos.Add(process);
            }

            ReorderAttributes();
        }

        public void removerFather()
        {
            if (Father != null)
            {
                Father.ListProcessos.Remove(this);
                Father.ReorderAttributes();

                Father = null;
            }
        }

        public void ReorderAttributes()
        {
            ReorderCascade();

            OrderPosition();
        }

        private void ReorderCascade()
        {
            if (Father != null)
            {
                this.Cascade = Father.Cascade + 1;
            }
            else
            {
                this.Cascade = 0;
            }

            ListProcessos.ForEach(x => x.ReorderCascade());
        }

        private void OrderPosition()
        {
            for (int i = 0; i < ListProcessos.Count; i++)
            {
                ListProcessos[i].Position = i + 1;
                ListProcessos[i].OrderPosition();
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
            Processo p = ListProcessos.Find(x => x.Name.Equals(nameProcess));

            if (p == null)
            {
                for (int i = 0; i < ListProcessos.Count; i++)
                {
                    p = ListProcessos[i].FindInternalProcess(nameProcess);

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

            if (Father != null)
            {
                Father.GetFathersProcess().ForEach(x => list.Add(x));
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
            for (int i = 0; i < ListProcessos.Count; i++)
            {
                ListProcessos[i].GetInternalOrderProcess().ForEach(x => p.Add(x));
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
            ListProcessos = new List<Processo>();
        }
        /// <summary>
        /// Exibe todos os item na ordem de "execução" de Processos
        /// </summary>
        public void TestProcess()
        {
            for (int i = 0; i < ListProcessos.Count; i++)
            {
                Processo p = ListProcessos[i];

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

            p.ListProcessos = CloneList();

            p.Father = null;

            p.ListProcessos.ForEach(x =>
            {
                x.Father = p;
            });

            p.ReorderAttributes();

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

            ListProcessos.ForEach(x =>
            {
                p.Add((Processo)x.Clone());
            });

            return p;
        }

        public void insertList(List<Processo> p)
        {
            ListProcessos = p;
        }
    }

    public class ProcessManager
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
}
