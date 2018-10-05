using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ProductionLinesWEG.Hub;
using ProductionLinesWEG.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;

namespace ProductionLinesWEG.Models
{
    class Threads
    {
        private readonly IHubConnectionContext<dynamic> Clients = GlobalHost.ConnectionManager.GetHubContext<MasterHub>().Clients;
        private EsteiraAbstrata e;

        private Program program;

        public Threads(EsteiraAbstrata e, Program p)
        {
            this.e = e;
            program = p;
        }
        /// <summary>
        /// Thread pricipal de controle da esteira
        /// </summary>
        public void threadEsteira()
        {
            if (e is EsteiraModel)
            {
                EsteiraModel em = (EsteiraModel)e;

                if (em.EsteiraOutput == null)
                {
                    program.toDashboard("Abortando inicio: '" + em.Name + "' sem Output", 2);
                    em.TurnOff();
                }
                else
                {
                    while (true)
                    {
                        // verifica se a esteira esta em condição de operação (masterProcess existe) antes de iniciar
                        if (em.IsInCondition())
                        {
                            // pega a primeira peça da fila
                            Peca pc = em.GetInputPieceNoRemove();

                            if (pc != null)
                            {
                                int run = 0;
                                // reseta o processo para iniciar o ciclo todo novamente
                                em.ResetProcess();

                                // verifica se existe processo adiante
                                while (em.HasNextProcess())
                                {
                                    Processo ps = em.NextProcess();
                                    Atributo at = new Atributo(ps.Id, ps.Name);

                                    pc.addAtributo(at);

                                    program.toDashboard(ps.Name + "(" + ps.Id + ") Executando em " + em.Name, 4);

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

                                    program.toDashboard(ps.Name + " Finalizado", 4);
                                }

                                // finalia o processo para ter um novo ciclo
                                em.FinalizeProcess();

                                program.toDashboard("Esteira: (" + em.Name + ")Bateria de processos finalizados", 4);

                                while (!em.PassPiece())
                                {
                                    program.toDashboard(em.Name + " aguardando envio para a proxima esteira", 4);
                                    Thread.Sleep(250);
                                }
                            }
                            else
                            {
                                // fica esperando a peca (250ms para não sobrecarregar o servidor)
                                Thread.Sleep(250);
                            }
                        }
                        else
                        {
                            program.toDashboard("Inserir um 'Master Process' na esteira (não é possivel iniciar)", 3);
                            break;
                        }
                    }
                }
            }
            else if (e is EsteiraEtiquetadora)
            {
                EsteiraEtiquetadora em = (EsteiraEtiquetadora)e;

                if (em.EsteiraOutput == null)
                {
                    program.toDashboard("Abortando inicio: '" + em.Name + "' sem Output", 2);
                    em.TurnOff();
                }
                else
                {
                    while (true)
                    {
                        // pega a primeira peça da fila
                        Peca pc = em.GetInputPieceNoRemove();

                        if (pc != null)
                        {
                            em.InsertTag();

                            program.toDashboard("(" + em.Name + ") Peça entiquetada com: '" + pc.Tag + "'", 4);

                            while (!em.PassPiece())
                            {
                                program.toDashboard(em.Name + " aguardando envio para a proxima esteira", 4);
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
            else if (e is EsteiraArmazenamento)
            {
                EsteiraArmazenamento em = (EsteiraArmazenamento)e;

                if (em.EsteiraOutput == null)
                {
                    program.toDashboard("Abortando inicio: '" + em.Name + "' sem Output", 2);
                    em.TurnOff();
                }
                else
                {
                    while (true)
                    {
                        // pega a primeira peça da fila
                        Peca pc = em.GetInputPieceNoRemove();

                        if (pc != null)
                        {
                            while (!em.PassPiece())
                            {
                                program.toDashboard(em.Name + " aguardando envio para a proxima esteira", 4);
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
            else if (e is EsteiraDesvio)
            {

            }
            else
            {
                program.toDashboard("Thread init Error", 3);
            }
        }
    }





















    //      private async void startButton_Click(object sender, RoutedEventArgs e)
    //      {
    //          // Instantiate the CancellationTokenSource.  
    //          cts = new CancellationTokenSource();
    //      
    //          resultsTextBox.Clear();
    //      
    //          try
    //          {
    //              await AccessTheWebAsync(cts.Token);
    //              // ***Small change in the display lines.  
    //              resultsTextBox.Text += "\r\nDownloads complete.";
    //          }
    //          catch (OperationCanceledException)
    //          {
    //              resultsTextBox.Text += "\r\nDownloads canceled.";
    //          }
    //          catch (Exception)
    //          {
    //              resultsTextBox.Text += "\r\nDownloads failed.";
    //          }
    //      
    //          // Set the CancellationTokenSource to null when the download is complete.  
    //          cts = null;
    //      }
    //      
    //      // Add an event handler for the Cancel button.  
    //      private void cancelButton_Click(object sender, RoutedEventArgs e)
    //      {
    //          if (cts != null)
    //          {
    //              cts.Cancel();
    //          }
    //      }
    //      
    //      // Provide a parameter for the CancellationToken.  
    //      // ***Change the return type to Task because the method has no return statement.  
    //      async Task AccessTheWebAsync(CancellationToken ct)
    //      {
    //          // Declare an HttpClient object.  
    //          HttpClient client = new HttpClient();
    //      
    //          // ***Call SetUpURLList to make a list of web addresses.  
    //          List<string> urlList = SetUpURLList();
    //      
    //          // ***Add a loop to process the list of web addresses.  
    //          foreach (var url in urlList)
    //          {
    //              // GetAsync returns a Task<HttpResponseMessage>.   
    //              // Argument ct carries the message if the Cancel button is chosen.   
    //              // ***Note that the Cancel button can cancel all remaining downloads.  
    //              HttpResponseMessage response = await client.GetAsync(url, ct);
    //      
    //              // Retrieve the website contents from the HttpResponseMessage.  
    //              byte[] urlContents = await response.Content.ReadAsByteArrayAsync();
    //      
    //              resultsTextBox.Text +=
    //                  String.Format("\r\nLength of the downloaded string: {0}.\r\n", urlContents.Length);
    //          }
    //      }
    //      
    //      // ***Add a method that creates a list of web addresses.  
    //      private List<string> SetUpURLList()
    //      {
    //          List<string> urls = new List<string>
    //              {
    //                  "http://msdn.microsoft.com",
    //                  "http://msdn.microsoft.com/library/hh290138.aspx",
    //                  "http://msdn.microsoft.com/library/hh290140.aspx",
    //                  "http://msdn.microsoft.com/library/dd470362.aspx",
    //                  "http://msdn.microsoft.com/library/aa578028.aspx",
    //                  "http://msdn.microsoft.com/library/ms404677.aspx",
    //                  "http://msdn.microsoft.com/library/ff730837.aspx"
    //              };
    //          return urls;
    //      }

}
