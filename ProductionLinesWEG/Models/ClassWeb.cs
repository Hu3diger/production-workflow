using System;

namespace ProductionLinesWEG.Models
{
    // classe para criar as mensagens e suas informações
    public class Dashboard
    {
        public string Date { get; set; }
        public string Message { get; set; }
        public bool Critico { get; set; }

        public Dashboard(string date, string message, bool critico)
        {
            Date = date;
            Message = message;
            Critico = critico;
        }
    }
}