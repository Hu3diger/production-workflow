using System;

namespace ProductionLinesWEG.Models
{
    public class Dashboard
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool Critico { get; set; }

        public Dashboard(DateTime date, string message, bool critico)
        {
            Date = date;
            Message = message;
            Critico = critico;
        }
    }
}