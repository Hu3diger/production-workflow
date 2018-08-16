using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductionLinesWEG.Models
{
    public class Testes
    {
        // pré carrega um programa com um usuario e alguns itens ja criados
        public static Program loadProgramTeste()
        {
            Program pgm = new Program("n1ePewjNIpySkXfpU+Ylf4nsQfhZgNKxDQ8vOptDVsg=");

            pgm.PreLoadProgram();

            return pgm;
        }
    }
}