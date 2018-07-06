using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductionLinesWEG.Models
{
    public class Testes
    {
        public static Program loadProgramTeste()
        {
            Program pgm = new Program("n1ePewjNIpySkXfpU+Ylf4nsQfhZgNKxDQ8vOptDVsg=");

            pgm.PreLoadProcess();

            return pgm;
        }
    }
}