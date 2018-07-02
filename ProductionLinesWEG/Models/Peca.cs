using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLineServerWEG.Models
{
    class Peca
    {
        private long _tag;

        public long Tag { get => _tag; set => _tag = value; }
        public List<Atributo> ListAtributos { get; private set; }

        /// <summary>
        /// Construtor da classa Peca, que inicializa a lista interna de processos e inicia com a tag -1
        /// </summary>
        public Peca()
        {
            _tag = -1;
            ListAtributos = new List<Atributo>();
        }

        public void setAtributo(string name, string value, string estado)
        {
            Atributo at = ListAtributos.Find(x => x.Name.Equals(name));

            if (at == null)
            {
                at = new Atributo(name);
                at.Value = value;
                at.Estado = estado;
                ListAtributos.Add(at);
            }
            else
            {
                at.Estado = estado;
                at.Value = value;
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
}
