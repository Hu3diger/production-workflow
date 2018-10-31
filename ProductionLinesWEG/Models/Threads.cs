using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ProductionLinesWEG.Hub;
using ProductionLinesWEG.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web.Script.Serialization;

namespace ProductionLinesWEG.Models
{
    class Threads
    {
        private readonly IHubConnectionContext<dynamic> Clients = GlobalHost.ConnectionManager.GetHubContext<MasterHub>().Clients;
        private EsteiraAbstrata e;

        private Program pgm;
        private string ClientId;

        public Threads(EsteiraAbstrata e, Program p, string clientId)
        {
            this.e = e;
            pgm = p;
            ClientId = clientId;
        }

        private void initSimulation()
        {
            e.Ligado = true;

            if (!pgm.InSimulation)
            {
                pgm.toDashboard("Simulação iniciada", 2, true);
            }

            pgm.toDashboard("Esteira '" + e.Name + "' ligada", 1, ClientId);

            pgm.InSimulation = true;
        }

        /// <summary>
        /// Thread pricipal de controle da esteira
        /// </summary>
        public void threadEsteira()
        {// verifica se a esteira esta em condição de operação (masterProcess existe) antes de iniciar
            if (e.IsInCondition())
            {

                if (e is SetableOutput es)
                {
                    if (es.EsteiraOutput == null)
                    {
                        pgm.toDashboard("Esteira: (" + e.Name + ") Abortando inicio: sem Output", 2, ClientId);
                        e.TurnOff(pgm);
                    }
                    else if (es is EsteiraModel em)
                    {
                        initSimulation();
                        while (true)
                        {
                            // pega a primeira peça da fila
                            Peca pc = em.GetInputPieceNoRemove();

                            if (pc != null)
                            {
                                int run = 0;
                                // reseta o processo para iniciar o ciclo todo novamente
                                em.FinalizeProcess();

                                // verifica se existe processo adiante
                                while (em.HasNextProcess())
                                {
                                    Processo ps = em.NextProcess();
                                    Atributo atributo = pc.ListAtributos.Find(c => c.IdP.Equals(ps.Id));
                                    if (atributo == null  || (atributo != null && !atributo.Estado.Equals(Atributo.INTERROMPIDO)))
                                    {
                                        Atributo at = new Atributo(ps.Id, ps.Name);

                                        pc.addAtributo(at);

                                        pgm.toDashboard("Esteira: (" + em.Name + ") (" + ps.Id + ") Executando", 4, false);

                                        run = ps.RuntimeWithVariation;

                                        // seta atributo
                                        at.Estado = Atributo.FAZENDO;

                                        // simula o tempo de um proceso real 
                                        Thread.Sleep(run);
                                        at.Time = run;

                                        // seta atributo
                                        if (ps.InSuccess)
                                        {
                                            at.Estado = Atributo.FEITO;
                                        }
                                        else
                                        {
                                            at.Estado = Atributo.DEFEITO;
                                        }

                                        pgm.toDashboard("Esteira: (" + em.Name + ") '" + ps.Name + "' Finalizado", 4, false);
                                    }
                                    else
                                    {
                                        pgm.toDashboard("Esteira: (" + em.Name + ") Processo '" + ps.Name + "' já feito (interrompido)", 4, false);
                                    }
                                }

                                pgm.toDashboard("Esteira: (" + em.Name + ") Bateria de processos finalizados", 4, false);

                                while (!em.PassPiece())
                                {
                                    Thread.Sleep(250);
                                }
                            }
                            else
                            {
                                // fica esperando a peca (250ms para não sobrecarregar o servidor)
                                Thread.Sleep(250);
                            }
                        }
                    }
                    else if (es is EsteiraEtiquetadora ee)
                    {
                        initSimulation();
                        while (true)
                        {
                            // pega a primeira peça da fila
                            Peca pc = ee.GetInputPieceNoRemove();

                            if (pc != null)
                            {
                                Atributo at = new Atributo(ee.Id, ee.Name);

                                try
                                {
                                    Stopwatch stopWatch = new Stopwatch();

                                    pgm.toDashboard("Esteira: (" + ee.Name + ") Inserindo TAG", 4, false);

                                    pc.addAtributo(at);

                                    // seta atributo
                                    at.Estado = Atributo.FAZENDO;

                                    stopWatch.Start();

                                    Thread.Sleep(100);
                                    ee.InsertTag();

                                    stopWatch.Stop();

                                    TimeSpan ts = stopWatch.Elapsed;
                                    at.Time = ts.Milliseconds;

                                    at.Estado = Atributo.FEITO;
                                    at.Value = "Tag inserida";

                                    pgm.toDashboard("(" + ee.Name + ") Peça entiquetada com: '" + pc.Tag + "'", 4, false);

                                    while (!ee.PassPiece())
                                    {
                                        Thread.Sleep(250);
                                    }
                                }
                                catch (Exception e)
                                {
                                    pgm.toDashboard("(" + ee.Name + " desligando) " + e.Message, 2, ClientId);
                                    at.Estado = Atributo.DEFEITO;
                                    at.Value = "Sem etiqueta";
                                    
                                    ee.TurnOff(pgm);
                                }

                            }
                            else
                            {
                                // fica esperando a peca (250ms para não sobrecarregar o servidor)
                                Thread.Sleep(250);
                            }
                        }
                    }
                    else if (es is EsteiraArmazenamento ea)
                    {
                        initSimulation();
                        while (true)
                        {
                            // pega a primeira peça da fila
                            Peca pc = ea.GetInputPieceNoRemove();

                            if (pc != null)
                            {

                                Atributo at = new Atributo(ea.Id, ea.Name);

                                pc.addAtributo(at);

                                Thread.Sleep(100);

                                at.Time = 100;
                                at.Estado = Atributo.FEITO;
                                at.Value = "Peça encaminhada";

                                pgm.toDashboard("(" + ea.Name + ") Peça '" + pc.Tag + "' encaminhada", 4, false);

                                while (!ea.PassPiece())
                                {
                                    Thread.Sleep(250);
                                }
                            }
                            else
                            {
                                // fica esperando a peca (250ms para não sobrecarregar o servidor)
                                Thread.Sleep(250);
                            }
                        }
                    }
                }
                else if (e is EsteiraDesvio ed)
                {
                    if (ed.EsteiraOutput != null && ed.EsteiraOutput.Count > 0)
                    {
                        initSimulation();
                        while (true)
                        {
                            // pega a primeira peça da fila
                            Peca pc = ed.GetInputPieceNoRemove();

                            if (pc != null)
                            {

                                Atributo at = new Atributo(ed.Id, ed.Name);

                                pc.addAtributo(at);

                                Thread.Sleep(100);

                                at.Time = 100;
                                at.Estado = Atributo.FEITO;
                                at.Value = "Peça encaminhada";

                                pgm.toDashboard("(" + ed.Name + ") Peça '" + pc.Tag + "' encaminhada", 4, false);

                                while (!ed.PassPiece())
                                {
                                    Thread.Sleep(250);
                                }
                            }
                            else
                            {
                                // fica esperando a peca (250ms para não sobrecarregar o servidor)
                                Thread.Sleep(250);
                            }
                        }
                    }
                    else
                    {
                        pgm.toDashboard("Esteira: (" + ed.Name + ") Abortando inicio: sem Output", 3, ClientId);
                    }
                }
                else
                {
                    pgm.toDashboard("Thread init Error", 3, ClientId);
                }
            }
            else
            {
                pgm.toDashboard("Esteira: (" + e.Name + ") Abortando inicio: sem condição de operação, verificar integridade da planta", 3, ClientId);
            }
            pgm.CheckSimulation();
        }
    }
}
