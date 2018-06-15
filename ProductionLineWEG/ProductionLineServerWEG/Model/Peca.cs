using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLineServerWEG
{
    class Peca
    {
        private long _tag;
        private List<ProcessoPeca> _processos;

        public long Tag { get => _tag; set => _tag = value; }

        /// <summary>
        /// Construtor da classa Peca, que inicializa a lista interna de processos e inicia com a tag -1
        /// </summary>
        public Peca()
        {
            _processos = new List<ProcessoPeca>();
            _tag = -1;
        }
        /// <summary>
        /// Retorna a soma de todos os erros ocorridos nos processos da peça
        /// </summary>
        /// <returns>
        /// int errors
        /// </returns>
        public int GetTotalRepairs()
        {
            int errors = 0;
            for (int i = 0; i < _processos.Count; i++)
            {
                errors += _processos[i].Errors;
            }

            return errors;
        }
        /// <summary>
        /// Adiciona um processo a lista de processos da peça
        /// </summary>
        /// <param name="process">Processos a ser adicionado na peça</param>
        public void AddProcess(ProcessoPeca process)
        {
            _processos.Add(process);
        }
        /// <summary>
        /// Retorna o primeiro processo encontrado na lista com, o nome especificado
        /// </summary>
        /// <param name="nameProcess">Nome do processo a ser encontrado</param>
        /// <returns>
        /// ProcessoPeca com o nome especificado
        /// Null caso nao encontre
        /// </returns>
        public ProcessoPeca FindProcessPiece(string nameProcess)
        {
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

            return (ProcessoPeca) p;
        }
        
    }

    //Boolean Printed { get; set; }
    //int PrintedError;

    //Boolean FirstComponents { get; set; }
    //Boolean SecondComponents { get; set; }

    //Boolean SmallWeld { get; set; }
    //int SmallWeldError;

    //Boolean ElectronicInspectionSmallWeld { get; set; }
    //int ElectronicInspectionSmallWeldError;

    //Boolean LargeComponents { get; set; }
    //Boolean BathWeld { get; set; }
    //int BathWeldError;

    //Boolean ManualInspectionBathWeld { get; set; }
    //int ManualInspectionBathWeldError;

    //Boolean GraphicsChip { get; set; }
    //Boolean FirstTest { get; set; }
    //int FirstTestError;

    //Boolean Carcass { get; set; }
    //int CascassError;

    //Boolean ManualInspection { get; set; }
    //int ManualInspectionError;

    //Boolean FinalTest { get; set; }
    //int FinalTestError;

    //Boolean Packing { get; set; }
    //Boolean FinalWeight { get; set; }
    //int FinalWeightError;
}
