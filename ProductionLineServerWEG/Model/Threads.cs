using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProductionLineServerWEG
{
    class Threads
    {
        private EsteiraAbstrata e;
        private Form1 f;

        public Threads(EsteiraAbstrata e, Form1 f)
        {
            this.e = e;
            this.f = f;
        }

        public void mainThread()
        {
            if (verificacao())
            {



            }
        }

        public bool verificacao()
        {
            return true;
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
                        if (em.hasNextProcess())
                        {
                            Peca pc = em.GetInputPieceNoRemove();

                            if (pc != null)
                            {
                                Processo p = em.nextProcess();

                                f.ExternalTerminal(p.Name + " Executando \n");

                                pc.insertAtributo(p.Name, "Kappa value", Atributo.FAZENDO);

                                Thread.Sleep(p.Runtime);

                                pc.insertAtributo(p.Name, "Kappa value", Atributo.FEITO);

                                f.ExternalTerminal(p.Name + " Finalizado \n");
                            }
                            else
                            {
                                f.ExternalTerminal("Esperando Peça\n");
                                Thread.Sleep(250);
                            }
                        }
                        else
                        {
                            em.resetProcess();
                            Peca p = em.RemovePiece();

                            f.ExternalTerminal("Exibindo atributos da peca recém 'feita': \n");

                            p.ListAtributos.ForEach(x => f.ExternalTerminal("Processo: " + x.Name + " / Estado: " + x.Estado + "\n"));

                            f.ExternalTerminal("Droped First / End\n");
                        }
                    }
                    else
                    {
                        f.ExternalTerminal("Insert a master process in Esteira\n");
                        break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    if (!e.executeNextProcesses())
                    {
                        Thread.Sleep(100);

                    }
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
