using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ProductionLinesWEG.Models;

namespace ProductionLinesWEG.Hub
{
    public class Logins
    {
        public string User { get; set; }
        public string Password { get; set; }

        public string AuthId { get; set; }

        public Logins(string user, string password, string authId)
        {
            User = user;
            Password = password;
            AuthId = authId;
        }
    }

    public class SessionAuth
    {
        public string AuthId { get; set; }
        public HashSet<string> SessionGroup { get; set; }

        public SessionAuth(string authId, HashSet<string> sessionGroup)
        {
            AuthId = authId;
            SessionGroup = sessionGroup;
        }
    }

    public class MasterHub : Microsoft.AspNet.SignalR.Hub
    {
        private static readonly List<Program> listProgram = new List<Program>();
        
        private static readonly List<Logins> _listLogins = new List<Logins> {
            new Logins("teste1",  "senha123", "OvZVPeUiR/Oty38YoQ5aWSbpAUkeneSW7wZQS2cn5YY="),
            new Logins("teste2",  "senha123", "SKB/minqyPAptFbdNxdRMUJRyequO3vV3JLXd1DlnNo="),
            new Logins("teste3",  "senha123", "R73l6UpSa0m2TKAwVfZWjlDwKbRUBF60Nz+lvo5zNsY="),
            new Logins("teste4",  "senha123", "DY0EW9LSaamcN2kElmjBMj8O7ueqnA0eR/bsVjI5p+4="),
            new Logins("teste5",  "senha123", "ZENQihH1ELif/yBmtQ7LpfBBgxja1IQ1rrX83UAsg1k="),
            new Logins("teste6",  "senha123", "hDIOcptrDXxpgF2uR5vac0rwCp+2oL0O0tDYbbPTJ3Q="),
            new Logins("teste7",  "senha123", "ZwRQjIzNwlWGlUtEw3ZLH4QNbgIuhfMRlvNnuIpFwk8="),
            new Logins("teste8",  "senha123", "OcuBennwdzY0eaPwdQVpjEi4PkixkDizuvf+ONhGhhE="),
            new Logins("teste9",  "senha123", "uaC/BujQFm3HsZv3cJy9oEQPMU1z6KUbTOfzx9dA8dw="),
            new Logins("teste10", "senha123", "m+YxKSQTbs60IkCgpXeNTXA71nCyuD22s9CCk5AceRg="),
            new Logins("teste11", "senha123", "9IiTO6J5/Qpfm3GxsI1BdI4LBJijxxUhPdXsvJ9Br68="),
            new Logins("teste12", "senha123", "XsVZlNM1vdTBMnRwXRXX2og+DGwtnsFFBuUoNtUt0HQ="),
            new Logins("teste13", "senha123", "rFjRfLB4yfWrXsDI/yfV+KqxaDXNcPbZ0/LftOBbte0="),
            new Logins("teste14", "senha123", "qAsFeFMMKHR0BW+j/B+UJHyPldMUhb1NG4ckRfWkMQU="),
            new Logins("teste15", "senha123", "HICxhJwT407lxTdFXC0ZuSDkjcUXXwUCvOyv2KEMBdA="),
            new Logins("teste16", "senha123", "kum72wJmLuL7meRQ/FlcRnV0AjEElHQXZiWq7rNnb7A="),
            new Logins("teste17", "senha123", "0va/K0rIllLzjg9TUE27PgjoIJp3OSaxMPxGGVjDMcE="),
            new Logins("teste18", "senha123", "QtrUsnpimPQ6hJxLE0JHFx6aRrZscqmzECLgGH3Q+WU="),
            new Logins("kappa", "kappasenha", "n1ePewjNIpySkXfpU+Ylf4nsQfhZgNKxDQ8vOptDVsg=")
        };

        public static readonly ConcurrentDictionary<string, SessionAuth> sessions = new ConcurrentDictionary<string, SessionAuth>();

        public static HubConnectionContext toClientsByAuthId(string AuthId)
        {
            return GlobalHost.ConnectionManager.GetHubContext<MasterHub>().Clients.Clients(GetAllConnectionIdsByAuthId(AuthId).ToList());
        }

        public static IEnumerable<string> GetAllConnectionIdsByConnectionId(string connectionId)
        {
            foreach (var session in sessions)
            {
                if (!session.Value.SessionGroup.Contains(connectionId))
                {
                    return session.Value.SessionGroup;
                }
            }

            return Enumerable.Empty<string>();
        }

        public static IEnumerable<string> GetAllConnectionIdsBySessionId(string sessionId)
        {
            foreach (var session in sessions)
            {
                if (session.Key.Equals(sessionId))
                {
                    return session.Value.SessionGroup;
                }
            }

            return Enumerable.Empty<string>();
        }

        public static IEnumerable<string> GetAllConnectionIdsByAuthId(string authId)
        {
            foreach (var session in sessions)
            {
                if (session.Value.AuthId.Equals(authId))
                {
                    return session.Value.SessionGroup;
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
            var connectionIds = null as SessionAuth;
            var sessionId = this.Context.QueryString["SessionId"];
            var connectionId = this.Context.ConnectionId;

            if (sessionId == null || sessionId.Equals(""))
            {
                string key = Utils.generateUniqueKey();

                sessionId = key;

                Clients.Caller.cookie("SessionId", key);
            }

            if (!sessions.TryGetValue(sessionId, out connectionIds))
            {
                connectionIds = sessions[sessionId] = new SessionAuth("", new HashSet<string>());
            }

            connectionIds.SessionGroup.Add(connectionId);
        }

        private void DisconnectGroups()
        {
            var connectionIds = null as SessionAuth;
            var sessionId = this.Context.QueryString["SessionId"];
            var connectionId = this.Context.ConnectionId;

            if (!sessions.TryGetValue(sessionId, out connectionIds))
            {
                foreach (var session in sessions)
                {
                    if (!session.Value.SessionGroup.Contains(connectionId))
                    {
                        sessionId = session.Key;
                        sessions.TryGetValue(sessionId, out connectionIds);
                    }
                }
            }

            connectionIds.SessionGroup.Remove(connectionId);

            if (connectionIds.SessionGroup.Count == 0)
            {
                sessions.TryRemove(sessionId, out connectionIds);
            }
        }

























        public void ChangingProcess(string name, string description, int runTime)
        {
            Clients.Caller.showToast("Recived");
        }

        public void requestLogin(string user, string password)
        {
            Logins l = _listLogins.Find(x => x.User.Equals(user) && x.Password.Equals(password));

            if (l != null)
            {
                Clients.Caller.acceptLoginUser(l.AuthId);

                foreach (var session in sessions)
                {
                    if (session.Value.SessionGroup.Contains(Context.ConnectionId))
                    {
                        session.Value.AuthId = l.AuthId;
                    }
                }

            }
            else
            {
                Clients.Caller.showToast("Login Refused");
            }
        }

        public void logOut()
        {
            foreach (var session in sessions)
            {
                if (session.Value.SessionGroup.Contains(Context.ConnectionId))
                {
                    session.Value.AuthId = "";
                }
            }
        }

    }
}
