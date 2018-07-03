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
        private readonly List<Dashboard> listDashboard = new List<Dashboard>();
        private EsteiraAbstrata e;

        //private Form1 f;

        public Threads(EsteiraAbstrata e)
        {
            this.e = e;
        }

        public void threadEsteira()
        {
            if (e is EsteiraModel)
            {
                EsteiraModel em = (EsteiraModel)e;

                while (true)
                {
                    if (em.IsInCondition())
                    {
                        Peca pc = em.GetInputPieceNoRemove();

                        if (pc != null)
                        {
                            em.resetProcess();

                            while (em.hasNextProcess())
                            {
                                Processo ps = em.nextProcess();

                                toDashboard(ps.Name + " Executando");

                                pc.setAtributo(ps.Name, "Kappa value", Atributo.FAZENDO);

                                Thread.Sleep(ps.Runtime);

                                pc.setAtributo(ps.Name, "Kappa value", Atributo.FEITO);

                                toDashboard(ps.Name + " Finalizado");
                            }

                            em.finalizeProcess();

                            Peca p = em.RemovePiece();

                            toDashboard("Exibindo atributos da peca recém 'feita':");

                            p.ListAtributos.ForEach(x => toDashboard("Processo: " + x.Name + " / Estado: " + x.Estado));

                            toDashboard("Droped First / End\n");
                        }
                        else
                        {
                            toDashboard(em.Name + "Esperando Peça");
                            Thread.Sleep(250);
                        }
                    }
                    else
                    {
                        toDashboard("Insert a master process in Esteira");
                        break;
                    }
                }
            }
            else
            {
                //while (true)
                //{

                //}
            }
        }

        private void toDashboard(string message, bool critico)
        {
            listDashboard.Insert(0, new Dashboard(new DateTime(), message, critico));
            verificarDashboard();
        }

        private void toDashboard(string message)
        {
            listDashboard.Insert(0, new Dashboard(new DateTime(), message, false));
            verificarDashboard();
        }

        private void verificarDashboard()
        {
            if (listDashboard.Count > 10)
            {
                if (!listDashboard[5].Critico)
                {
                    listDashboard.RemoveAt(5);
                }
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
