using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLinesWEG.Models
{
    // atributos que será inserido na peca quando estiver sendo executada
    public class Atributo
    {
        public string IdP { get; private set; }
        public string NameP { get; private set; }
        public string Value { get; set; }
        public string Estado { get; set; }
        public DateTime Data { get; set; }
        public int Time { get; set; }

        public static readonly string ESPERANDO = "Esperando";
        public static readonly string FAZENDO = "Fazendo";
        public static readonly string INTERROMPIDO = "Interrompido";
        public static readonly string DEFEITO = "Defeito";
        public static readonly string FEITO = "Feito";

        public Atributo(string idP, string nameP)
        {
            IdP = idP;
            NameP = nameP;
            Estado = ESPERANDO;
            Data = DateTime.Now;
            Value = "Default";
        }
    }
}
