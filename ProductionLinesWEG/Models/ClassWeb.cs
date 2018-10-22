using System;

namespace ProductionLinesWEG.Models
{
    // classe para criar as mensagens e suas informações
    public class Dashboard
    {
        public string Date { get; set; }
        public string Message { get; set; }
        public int Nivel { get; set; }

        public Dashboard(string date, string message, int nivel)
        {
            Date = date;
            Message = message;
            Nivel = nivel;
        }
    }

    public class DashboardClient
    {
        public Dashboard dashboard { get; set; }
        public string ClientId { get; set; }
        public bool ToAllClients { get; set; }

        public DashboardClient(Dashboard dashboard, bool toAllClients)
        {
            this.dashboard = dashboard;
            ClientId = "";
            ToAllClients = toAllClients;
        }

        public DashboardClient(Dashboard dashboard, string clientId)
        {
            this.dashboard = dashboard;
            ClientId = clientId;
            ToAllClients = false;
        }
    }
}