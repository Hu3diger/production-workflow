using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
// Resumo:
//     Executa a ação especificada em cada elemento do System.Collections.Generic.List`1.
//
// Parâmetros:
//   action:
//     O delegado System.Action`1 a ser executado em cada elemento do System.Collections.Generic.List`1.
//
// Exceções:
//   T:System.ArgumentNullException:
//     action é null.
//
//   T:System.InvalidOperationException:
//     Um elemento na coleção foi modificado. Essa exceção é lançada, começando com
//     o .NET Framework 4.5.
namespace ConsoleApp1
{
    class Peca
    {
        private long _tag;
        private List<ProcessoPeca> _processos;

        public long Tag { get => _tag; set => _tag = value; }

        public Peca()
        {
            _processos = new List<ProcessoPeca>();
            _tag = 1;
        }
        
        public int GetTotalRepairs()
        {
            int errors = 0;
            for (int i = 0; i < _processos.Count; i++)
            {
                errors += _processos[i].Errors;
            }

            return errors;
        }

        public void AddProcess(ProcessoPeca process)
        {
            _processos.Add(process);
        }

        //public ProcessoPeca GetProcess(string nameProcess)
        //{
        //    return _processos.Find(x => x.Name.Equals(nameProcess));
        //}

        public Boolean GetInProcess(string nameProcess)
        {
            return FindProcessPiece(nameProcess).InProcess;
        }

        public void SetInProcess(string nameProcess, bool value)
        {
            FindProcessPiece(nameProcess).InProcess = value;
        }
        
        public Boolean GetDoneProcess(string nameProcess)
        {
            return FindProcessPiece(nameProcess).Done;
        }

        public void SetDoneProcess(string nameProcess, bool value)
        {
            FindProcessPiece(nameProcess).Done = value;
        }

        public int GetErrosProcess(string nameProcess)
        {
            return FindProcessPiece(nameProcess).Errors;
        }

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
