using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductionLinesWEG.Models
{
    public class Utils
    {
        // gera uma key unica utilizando o sistema de encriptação
        public static string generateUniqueKey()
        {
            var rng = System.Security.Cryptography.RNGCryptoServiceProvider.Create();
            var bytes = new byte[32];

            rng.GetNonZeroBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}