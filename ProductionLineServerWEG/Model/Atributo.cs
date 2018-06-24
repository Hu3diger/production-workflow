using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLineServerWEG
{
    class Atributo
    {
        public string Name { get; private set; }
        public string Value { get; set; }
        public string Estado { get; set; }

        public static readonly string ESPERANDO = "Esperando";
        public static readonly string FAZENDO = "Fazendo";
        public static readonly string FEITO = "Feito";

        public Atributo(string name)
        {
            Name = name;
            Estado = ESPERANDO;
        }
    }
}
