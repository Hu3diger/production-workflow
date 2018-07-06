using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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
        private static readonly List<Program> listProgram = new List<Program>() {
            new Program("OvZVPeUiR/Oty38YoQ5aWSbpAUkeneSW7wZQS2cn5YY="),
            new Program("SKB/minqyPAptFbdNxdRMUJRyequO3vV3JLXd1DlnNo="),
            new Program("R73l6UpSa0m2TKAwVfZWjlDwKbRUBF60Nz+lvo5zNsY="),
            new Program("DY0EW9LSaamcN2kElmjBMj8O7ueqnA0eR/bsVjI5p+4="),
            new Program("ZENQihH1ELif/yBmtQ7LpfBBgxja1IQ1rrX83UAsg1k="),
            new Program("hDIOcptrDXxpgF2uR5vac0rwCp+2oL0O0tDYbbPTJ3Q="),
            new Program("ZwRQjIzNwlWGlUtEw3ZLH4QNbgIuhfMRlvNnuIpFwk8="),
            new Program("OcuBennwdzY0eaPwdQVpjEi4PkixkDizuvf+ONhGhhE="),
            new Program("uaC/BujQFm3HsZv3cJy9oEQPMU1z6KUbTOfzx9dA8dw="),
            new Program("m+YxKSQTbs60IkCgpXeNTXA71nCyuD22s9CCk5AceRg="),
            new Program("9IiTO6J5/Qpfm3GxsI1BdI4LBJijxxUhPdXsvJ9Br68="),
            new Program("XsVZlNM1vdTBMnRwXRXX2og+DGwtnsFFBuUoNtUt0HQ="),
            new Program("rFjRfLB4yfWrXsDI/yfV+KqxaDXNcPbZ0/LftOBbte0="),
            new Program("qAsFeFMMKHR0BW+j/B+UJHyPldMUhb1NG4ckRfWkMQU="),
            new Program("HICxhJwT407lxTdFXC0ZuSDkjcUXXwUCvOyv2KEMBdA="),
            new Program("kum72wJmLuL7meRQ/FlcRnV0AjEElHQXZiWq7rNnb7A="),
            new Program("0va/K0rIllLzjg9TUE27PgjoIJp3OSaxMPxGGVjDMcE="),
            new Program("QtrUsnpimPQ6hJxLE0JHFx6aRrZscqmzECLgGH3Q+WU="),
            Testes.loadProgramTeste()
        };

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
        internal const string SessionId = "SessionId";

        public static HubConnectionContext toClientsByAuthId(string AuthId)
        {
            return GlobalHost.ConnectionManager.GetHubContext<MasterHub>().Clients.Clients(GetAllConnectionIdsByAuthId(AuthId).ToList());
        }

        public static IEnumerable<string> GetAllConnectionIdsByConnectionId(string connectionId)
        {
            foreach (var session in sessions)
            {
                if (session.Value.SessionGroup.Contains(connectionId) == true)
                {
                    return session.Value.SessionGroup;
                }
            }

            return Enumerable.Empty<string>();
        }

        public static string GetAuthIdByConnectionId(string connectionId)
        {
            foreach (var session in sessions)
            {
                if (session.Value.SessionGroup.Contains(connectionId) == true)
                {
                    return session.Value.AuthId;
                }
            }

            return null;
        }

        public static IEnumerable<string> GetAllConnectionIdsByAuthId(string authId)
        {
            HashSet<string> kappa = new HashSet<string>();

            foreach (var session in sessions)
            {
                if (session.Value.AuthId.Equals(authId) == true)
                {
                    foreach (var item in session.Value.SessionGroup)
                    {
                        kappa.Add(item);
                    }
                }
            }

            return kappa;
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
            var sessionId = this.Context.QueryString[SessionId];
            var AuthId = this.Context.QueryString["AuthId"];
            var connectionId = this.Context.ConnectionId;

            if (sessionId == null || sessionId.Equals(""))
            {
                string key = Utils.generateUniqueKey();

                sessionId = key;

                Clients.Caller.cookie("SessionId", key);
            }

            if (sessions.TryGetValue(sessionId, out connectionIds) == false)
            {
                connectionIds = sessions[sessionId] = new SessionAuth(AuthId, new HashSet<string>());
            }

            connectionIds.SessionGroup.Add(connectionId);
        }

        private void DisconnectGroups()
        {
            var connectionIds = null as SessionAuth;
            var sessionId = this.Context.QueryString[SessionId];
            var connectionId = this.Context.ConnectionId;

            if (sessions.TryGetValue(sessionId, out connectionIds) == false)
            {
                foreach (var session in sessions)
                {
                    if (session.Value.SessionGroup.Contains(connectionId) == true)
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

        public void requestLogin(string user, string password)
        {
            Logins l = _listLogins.Find(x => x.User.Equals(user) && x.Password.Equals(password));

            if (l != null)
            {

                foreach (var session in sessions)
                {
                    if (session.Value.SessionGroup.Contains(Context.ConnectionId))
                    {
                        session.Value.AuthId = l.AuthId;
                    }
                }

                Clients.Caller.acceptLoginUser(l.AuthId);

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

























        public void CreateProcess(string name, string description, int runTime)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

            if (pgm != null)
            {
                pgm.CriaProcesso(name, description, runTime);
                Clients.Caller.showToast("Processo '" + name + "' Criado");
            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
            }

        }

        public void ChangingProcess(string oldName, string newName, string description, int runTime)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

            if (pgm != null)
            {
                Processo pcss = pgm.listProcessos.Find(x => x.Name.Equals(oldName));
                if (pcss != null)
                {
                    pcss.BaseProcesso.Name = newName;
                    pcss.BaseProcesso.Description = description;
                    pcss.BaseProcesso.Runtime = runTime;

                    Clients.Caller.showToast("Processo '" + newName + "' Alerado");
                }
                else
                {
                    Clients.Caller.showToast("Error: Find Process: '" + oldName + "'");
                }

            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
            }

        }

        public void DeleteProcess(string name)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

            if (pgm != null)
            {
                if (pgm.listProcessos.Remove(pgm.listProcessos.Find(x => x.Name.Equals(name))))
                {
                    Clients.Caller.showToast("Processo '" + name + "' Deletado");
                }
                else
                {
                    Clients.Caller.showToast("Error: Find Process: '" + name + "'");
                }

            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
            }

        }

        public List<string> ListFatherProcess(string name)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

            Debug.WriteLine("entrou");

            if (pgm != null)
            {
                return pgm.listFatherProcess(name);
            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
            }

            return new List<string>();

        }

        public void InsertProcess(string processo1, string processo2)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

            if (pgm != null)
            {
                Processo p1 = pgm.listProcessos.Find(x => x.Name.Equals(processo1));
                Processo p2 = pgm.listProcessos.Find(x => x.Name.Equals(processo2));

                if (p1 != null && p2 != null)
                {
                    pgm.InsertProcesso(processo1, processo2);
                    Clients.Caller.showToast("Processo '" + processo1 + "' Adicionado dentro do Processo '" + processo2 + "'");
                }
                else
                {
                    Clients.Caller.showToast("Error: Find Process: '" + processo1 + "' or '" + processo2 + "'");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
            }

        }

        public void callListProcess()
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {
                var connections = GetAllConnectionIdsByAuthId(AuthId).ToList();

                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

                if (pgm != null)
                {
                    Clients.Clients(connections).listProcessos(pgm.getProcessoToClient());
                }
                else
                {
                    Clients.Caller.showToast("Error: CallListProcess");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: no Login");
            }
        }




    }
}
