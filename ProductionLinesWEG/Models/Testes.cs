using ProductionLinesWEG.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductionLinesWEG.Models
{
    public class Testes
    {
        // pré carrega um programa com um usuario e alguns itens ja criados
        public static Program loadProgramTeste(Logins login)
        {
            Program pgm = new Program(login);

            pgm.PreLoadProgram();

            return pgm;
        }
    }
}