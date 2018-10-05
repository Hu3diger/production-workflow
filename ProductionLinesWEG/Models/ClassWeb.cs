using System;

namespace ProductionLinesWEG.Models
{
    // classe para criar as mensagens e suas informações
    public class Dashboard
    {
        public string Date { get; set; }
        public string Message { get; set; }
        public int Nivel{ get; set; }

        public Dashboard(string date, string message, int nivel)
        {
            Date = date;
            Message = message;
            Nivel = nivel;
        }
    }
}