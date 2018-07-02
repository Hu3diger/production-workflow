using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProductionLinesWEG.Models;

namespace ProductionLinesWEG.Hub
{
    public class MasterHub : Microsoft.AspNet.SignalR.Hub
    {
        public static readonly ConcurrentDictionary<string, HashSet<string>> sessions = new ConcurrentDictionary<string, HashSet<string>>();

        public static IEnumerable<string> GetAllConnectionIds(string connectionId)
        {
            foreach (var session in sessions)
            {
                if (session.Value.Contains(connectionId) == true)
                {
                    return session.Value;
                }
            }

            return Enumerable.Empty<string>();
        }

        public override Task OnConnected()
        {
            this.EnsureGroups();

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            this.EnsureGroups();

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            this.DisconnectGroups();

            return base.OnDisconnected(stopCalled);
        }

        private void EnsureGroups()
        {
            var connectionIds = null as HashSet<string>;
            var sessionId = this.Context.QueryString["SessionId"];
            var connectionId = this.Context.ConnectionId;

            Debug.WriteLine("cookie: " + sessionId);

            if (sessionId == null || sessionId.Equals(""))
            {
                string key = Utils.generateUniqueKey();

                sessionId = key;

                Clients.Caller.cookie("SessionId", key);
            }

            if (sessions.TryGetValue(sessionId, out connectionIds) == false)
            {
                connectionIds = sessions[sessionId] = new HashSet<string>();
            }

            connectionIds.Add(connectionId);
        }

        private void DisconnectGroups()
        {
            var connectionIds = null as HashSet<string>;
            var sessionId = this.Context.QueryString["SessionId"];
            var connectionId = this.Context.ConnectionId;

            if (sessions.TryGetValue(sessionId, out connectionIds) == false)
            {
                foreach (var session in sessions)
                {
                    if (session.Value.Contains(connectionId) == true)
                    {
                        sessionId = session.Key;
                        sessions.TryGetValue(sessionId, out connectionIds);
                    }
                }
            }

            connectionIds.Remove(connectionId);

            if (connectionIds.Count == 0)
            {
                sessions.TryRemove(sessionId, out connectionIds);
            }

            sessions.TryGetValue(sessionId, out connectionIds);
        }

























        public void ChangingProcess(string name, string description, int runTime)
        {
            Clients.Caller.showToast("Recived");
        }

        public void requestLogin(string name, string password)
        {

            if (name.Equals("kappa") && password.Equals("kappasenha"))
            {
                Clients.Caller.acceptLoginUser(Utils.generateUniqueKey());
            }
            else
            {
                Clients.Caller.showToast("Login Refused");
            }
        }

        public void logOut()
        {

        }
        
    }
}
