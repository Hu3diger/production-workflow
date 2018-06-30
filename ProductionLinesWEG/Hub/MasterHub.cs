using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProductionLinesWEG.Hub
{
    public class MasterHub : Microsoft.AspNet.SignalR.Hub
    {
        public void ChangingProcess(string name, string description, int runTime)
        {
            Debug.WriteLine("{0}, {1}, {2}: {3}", name, description, runTime, Context.ConnectionId);

            Clients.Caller.showToast("Recived");
        }

        public void requestLogin(string name, string password)
        {

            if (name.Equals("kappa") && password.Equals("kappasenha"))
            {
                Clients.Caller.acceptLoginUser();
            }
            else
            {
                Clients.Caller.showToast("Login Refused");
            }
        }

        public override Task OnConnected()
        {
            // Add your own code here.
            // For example: in a chat application, record the association between
            // the current connection ID and user name, and mark the user as online.
            // After the code in this method completes, the client is informed that
            // the connection is established; for example, in a JavaScript client,
            // the start().done callback is executed.

            Debug.WriteLine("kappa conectado {0}", Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Add your own code here.
            // For example: in a chat application, mark the user as offline, 
            // delete the association between the current connection id and user name.

            Debug.WriteLine("kappa desconectado");
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            // Add your own code here.
            // For example: in a chat application, you might have marked the
            // user as offline after a period of inactivity; in that case 
            // mark the user as online again.

            Debug.WriteLine("kappa reconectado");
            return base.OnReconnected();
        }
    }
}