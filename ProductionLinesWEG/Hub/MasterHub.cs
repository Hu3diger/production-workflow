using System;
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
    // classe para armazenar os logins
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

    // classe para armazenar as sessões
    public class SessionAuth
    {
        public Logins Login { get; private set; }

        public HashSet<string> SessionGroup { get; set; }
        public string AuthId
        {
            get
            {
                if (Login != null)
                {
                    return Login.AuthId;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Login = MasterHub.listLogins.Find(x => x.AuthId.Equals(value));
            }
        }

        public SessionAuth(string authId, HashSet<string> sessionGroup)
        {
            Login = MasterHub.listLogins.Find(x => x.AuthId.Equals(authId));
            SessionGroup = sessionGroup;
        }
    }

    // iisexpress-proxy 53139 to 3000
    // classe principal para a communicação com o servidor
    public class MasterHub : Microsoft.AspNet.SignalR.Hub
    {
        private bool inDashboard = false;

        // lista de logins para controle de acesso
        public static readonly List<Logins> listLogins = new List<Logins> {
            new Logins("treina01",  "senha123", "OvZVPeUiR/Oty38YoQ5aWSbpAUkeneSW7wZQS2cn5YY="),
            new Logins("treina02",  "senha123", "SKB/minqyPAptFbdNxdRMUJRyequO3vV3JLXd1DlnNo="),
            new Logins("treina03",  "senha123", "R73l6UpSa0m2TKAwVfZWjlDwKbRUBF60Nz+lvo5zNsY="),
            new Logins("treina04",  "senha123", "DY0EW9LSaamcN2kElmjBMj8O7ueqnA0eR/bsVjI5p+4="),
            new Logins("treina05",  "senha123", "ZENQihH1ELif/yBmtQ7LpfBBgxja1IQ1rrX83UAsg1k="),
            new Logins("treina06",  "senha123", "hDIOcptrDXxpgF2uR5vac0rwCp+2oL0O0tDYbbPTJ3Q="),
            new Logins("treina07",  "senha123", "ZwRQjIzNwlWGlUtEw3ZLH4QNbgIuhfMRlvNnuIpFwk8="),
            new Logins("treina08",  "senha123", "OcuBennwdzY0eaPwdQVpjEi4PkixkDizuvf+ONhGhhE="),
            new Logins("treina09",  "senha123", "uaC/BujQFm3HsZv3cJy9oEQPMU1z6KUbTOfzx9dA8dw="),
            new Logins("treina10", "senha123", "m+YxKSQTbs60IkCgpXeNTXA71nCyuD22s9CCk5AceRg="),
            new Logins("treina11", "senha123", "9IiTO6J5/Qpfm3GxsI1BdI4LBJijxxUhPdXsvJ9Br68="),
            new Logins("treina12", "senha123", "XsVZlNM1vdTBMnRwXRXX2og+DGwtnsFFBuUoNtUt0HQ="),
            new Logins("treina13", "senha123", "rFjRfLB4yfWrXsDI/yfV+KqxaDXNcPbZ0/LftOBbte0="),
            new Logins("treina14", "senha123", "qAsFeFMMKHR0BW+j/B+UJHyPldMUhb1NG4ckRfWkMQU="),
            new Logins("treina15", "senha123", "HICxhJwT407lxTdFXC0ZuSDkjcUXXwUCvOyv2KEMBdA="),
            new Logins("treina16", "senha123", "kum72wJmLuL7meRQ/FlcRnV0AjEElHQXZiWq7rNnb7A="),
            new Logins("treina17", "senha123", "0va/K0rIllLzjg9TUE27PgjoIJp3OSaxMPxGGVjDMcE="),
            new Logins("treina18", "senha123", "QtrUsnpimPQ6hJxLE0JHFx6aRrZscqmzECLgGH3Q+WU="),
            new Logins("treina19", "senha123", "QtrUsnpimPQ6hJxLE0JHFx6aRrZscqmzECLgGH3Q+WU="),
            new Logins("kappa", "kappasenha", "n1ePewjNIpySkXfpU+Ylf4nsQfhZgNKxDQ8vOptDVsg=")
        };

        // lista de programas, "sessões"
        //public static readonly List<Program> listProgram = new List<Program>() {
        //    Testes.loadProgramTeste(listLogins[0]),
        //    Testes.loadProgramTeste(listLogins[1]),
        //    Testes.loadProgramTeste(listLogins[2]),
        //    Testes.loadProgramTeste(listLogins[3]),
        //    Testes.loadProgramTeste(listLogins[4]),
        //    Testes.loadProgramTeste(listLogins[5]),
        //    Testes.loadProgramTeste(listLogins[6]),
        //    Testes.loadProgramTeste(listLogins[7]),
        //    Testes.loadProgramTeste(listLogins[8]),
        //    Testes.loadProgramTeste(listLogins[9]),
        //    Testes.loadProgramTeste(listLogins[10]),
        //    Testes.loadProgramTeste(listLogins[11]),
        //    Testes.loadProgramTeste(listLogins[12]),
        //    Testes.loadProgramTeste(listLogins[13]),
        //    Testes.loadProgramTeste(listLogins[14]),
        //    Testes.loadProgramTeste(listLogins[15]),
        //    Testes.loadProgramTeste(listLogins[16]),
        //    Testes.loadProgramTeste(listLogins[17]),
        //    Testes.loadProgramTeste(listLogins[18]),
        //    Testes.loadProgramTeste(listLogins[19]),
        //};

        public static readonly List<Program> listProgram = new List<Program>() {
            new Program(listLogins[0]),
            new Program(listLogins[1]),
            new Program(listLogins[2]),
            new Program(listLogins[3]),
            new Program(listLogins[4]),
            new Program(listLogins[5]),
            new Program(listLogins[6]),
            new Program(listLogins[7]),
            new Program(listLogins[8]),
            new Program(listLogins[9]),
            new Program(listLogins[10]),
            new Program(listLogins[11]),
            new Program(listLogins[12]),
            new Program(listLogins[13]),
            new Program(listLogins[14]),
            new Program(listLogins[15]),
            new Program(listLogins[16]),
            new Program(listLogins[17]),
            new Program(listLogins[18]),
            Testes.loadProgramTeste(listLogins[19]),
        };

        // lista de sessões para controle de usuarios
        public static readonly ConcurrentDictionary<string, SessionAuth> sessions = new ConcurrentDictionary<string, SessionAuth>();
        internal const string SessionId = "SessionId";

        // retorna todos as conexões vinculadas as connectionId passado
        public static IEnumerable<string> GetAllConnectionIdsByConnectionId(string connectionId)
        {
            foreach (var session in sessions)
            {
                if (session.Value.SessionGroup.Contains(connectionId) == true)
                {
                    return session.Value.SessionGroup;
                }
            }

            // retorna vazio caso não encontre
            return Enumerable.Empty<string>();
        }

        // retorna o AuthId de acordo com o connectionId (caso esteja logado)
        public static string GetAuthIdByConnectionId(string connectionId)
        {
            foreach (var session in sessions)
            {
                if (session.Value.SessionGroup.Contains(connectionId) == true)
                {
                    return session.Value.AuthId;
                }
            }

            return "";
        }

        // retorna todas as conexões vinculadas os authId
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

        // sobrescreve o metodo onde é chamado quando um usuario se conecta com o servidor
        public override Task OnConnected()
        {
            this.EnsureGroups();

            return base.OnConnected();
        }

        // sobrescreve o metodo onde é chamado quando um usuario se reconecta com o servidor
        public override Task OnReconnected()
        {
            this.EnsureGroups();

            return base.OnReconnected();
        }

        // sobrescreve o metodo onde é chamado quando um usuario se desconecta do servidor
        public override Task OnDisconnected(bool stopCalled)
        {
            inDashboard = false;
            // remove o usuario (apenas uma conexão por vez) da lista de usuarios (sessions)

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

            if (connectionIds != null)
            {
                connectionIds.SessionGroup.Remove(connectionId);

                if (connectionIds.SessionGroup.Count == 0)
                {
                    sessions.TryRemove(sessionId, out connectionIds);
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        // função usada para agrupar os connectionsIds numa lista
        // para poder distribuir chamadas de acordo com alguns parametros
        // (como authId, sessionId ou connectionId)
        private void EnsureGroups()
        {
            var connectionIds = null as SessionAuth;
            var sessionId = this.Context.QueryString[SessionId];
            var AuthId = this.Context.QueryString["AuthId"];
            var connectionId = this.Context.ConnectionId;

            if (sessionId == null || sessionId.Equals(""))
            {
                // gera uma key unica para o usuario
                string key = Utils.generateUniqueKey();

                sessionId = key;

                // requisita o cookie do cliente atravez duma função do signalr implementada no cliente
                Clients.Caller.cookie("SessionId", key);
            }

            if (sessions.TryGetValue(sessionId, out connectionIds) == false)
            {
                connectionIds = sessions[sessionId] = new SessionAuth(AuthId, new HashSet<string>());
            }

            connectionIds.SessionGroup.Add(connectionId);
        }








        public void testConnection()
        {

            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {
                // procura pelo programa que corresponde ao usuario (AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

                if (pgm != null)
                {
                    Clients.Caller.showToast("Conectado com o servidor, Logado");
                }
                else
                {
                    Clients.Caller.showToast("Program does not match authentication, try again");
                }
            }
            else
            {
                Clients.Caller.showToast("Conectado com o servidor, necessário Login");
            }
        }










        // verifica se o usuario existe e aceita ou não usuario
        public void requestLogin(string user, string password)
        {
            Logins l = listLogins.Find(x => x.User.Equals(user) && x.Password.Equals(password));

            if (l != null)
            {

                foreach (var session in sessions)
                {
                    if (session.Value.SessionGroup.Contains(Context.ConnectionId))
                    {
                        session.Value.AuthId = l.AuthId;
                    }
                }

                // procura pelo programa que corresponde ao usuario (l.AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(l.AuthId));

                if (pgm != null)
                {
                    // invoca uma função do cliente para aceita-lo
                    RegisterMessageDashboard("Logado com sucesso", 1, true);
                    Clients.Caller.acceptLoginUser(l.AuthId);
                }
                else
                {
                    Clients.Caller.showToast("Error find pgm Login");
                }

            }
            else
            {
                // exibe um toast para um cliente (Caller) especifico
                Clients.Caller.showToast("Login Refused");
            }
        }

        // desloga o usuario do sistema, mas não o remove
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

        public Program CheckReturnPgm()
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {
                // procura pelo programa que corresponde ao usuario (AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

                if (pgm != null)
                {
                    if (pgm.InSimulation)
                    {
                        Clients.Caller.showToast("Program is in simulation, please stop the simulation");
                        return null;
                    }
                    else
                    {
                        return pgm;
                    }
                }
                else
                {
                    Clients.Caller.showToast("Program does not match authentication, try again");
                    return null;
                }
            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
                return null;
            }
        }

        public Program CheckPgmInSimulation()
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {
                // procura pelo programa que corresponde ao usuario (AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

                if (pgm != null)
                {
                    return pgm;
                }
                else
                {
                    Clients.Caller.showToast("Program does not match authentication, try again");
                    return null;
                }
            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
                return null;
            }
        }

        public void RegisterMessageDashboard(string message, int nivel, bool thisClient)
        {
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                pgm.toDashboard(message, nivel);

                if (thisClient)
                {
                    Clients.Caller.showToast(message);
                }
                else
                {
                    Clients.Clients(GetAllConnectionIdsByAuthId(pgm.AuthId).ToList()).showToast(message);
                }
            }
        }






















        // cria um processo e o adiciona a lista fazendo varias verificações
        public void CreateProcess(string name, string description, int runTime, double variationRuntime, double errorProbability, string nameFather, int position)
        {
            Program pgm = CheckReturnPgm();

            if (pgm != null)
            {
                if (pgm.listProcessos.Find(x => x.Name.Equals(name)) == null)
                {
                    // cria o processo e insere suas primeiras informações
                    Processo p = new Processo(new BaseProcesso(name, description, runTime));
                    p.BaseProcesso.VariationRuntime = variationRuntime;
                    p.BaseProcesso.ErrorProbability = errorProbability;

                    pgm.CriaProcesso(p);

                    if (!nameFather.Equals(""))
                    {
                        Processo pcss = pgm.listProcessos.Find(x => x.Name.Equals(nameFather));
                        if (pcss != null)
                        {
                            if (pcss.FindInternalProcess(name) == null)
                            {
                                pgm.alterFatherProcess(name, position, pcss);
                            }
                            else
                            {
                                RegisterMessageDashboard("Error: In Create: '" + name + "' Internal Process Found", 3, true);
                            }
                        }
                        else
                        {
                            RegisterMessageDashboard("Pai '" + nameFather + "' não encontrado", 2, true);
                        }
                    }

                    RegisterMessageDashboard("Processo '" + name + "' Criado", 1, true);
                    CallListProcess();
                }
                else
                {
                    RegisterMessageDashboard("Processo '" + name + "' já existente", 2, true);
                }
            }
        }

        // edita um processo
        public void ChangingProcess(string oldName, string newName, string description, int runTime, double variationRuntime, double errorProbability, string nameFather, int position)
        {
            Program pgm = CheckReturnPgm();

            if (pgm != null)
            {

                if (oldName.Equals(newName) || pgm.listProcessos.Find(x => x.Name.Equals(newName)) == null)
                {

                    Processo pcss = pgm.listProcessos.Find(x => x.Name.Equals(oldName));
                    if (pcss != null)
                    {
                        // faz suas alterações no processo
                        pcss.BaseProcesso.Name = newName;
                        pcss.BaseProcesso.Description = description;
                        pcss.BaseProcesso.Runtime = runTime;
                        pcss.BaseProcesso.VariationRuntime = variationRuntime;
                        pcss.BaseProcesso.ErrorProbability = errorProbability;

                        if (!nameFather.Equals(""))
                        {
                            Processo pcssF = pgm.listProcessos.Find(x => x.Name.Equals(nameFather));

                            if (pcssF != null)
                            {
                                pgm.alterFatherProcess(newName, position, pcssF);
                            }
                            else
                            {
                                RegisterMessageDashboard("Pai '" + nameFather + "' não encontrado", 2, true);
                            }
                        }
                        else
                        {
                            pcss.removerFather();
                        }

                        RegisterMessageDashboard("Processo '" + newName + "' Alterado", 1, true);
                        CallListProcess();
                    }
                    else
                    {
                        RegisterMessageDashboard("Error: Find Process: '" + oldName + "'", 3, true);
                    }
                }
                else
                {
                    RegisterMessageDashboard("Processo '" + newName + "' já existente", 2, true);
                }
            }
        }

        // deleta um processo
        public void DeleteProcess(string name)
        {
            Program pgm = CheckReturnPgm();

            if (pgm != null)
            {

                Processo p = pgm.listProcessos.Find(x => x.Name.Equals(name));

                if (p != null)
                {
                    pgm.removeProcess(p);

                    RegisterMessageDashboard("Processo '" + name + "' Deletado", 1, true);
                    CallListProcess();
                }
                else
                {
                    RegisterMessageDashboard("Error: Find Process: '" + name + "'", 3, true);
                }
            }
        }

        // lista os processos disponiveis para determinado processo (name)
        public List<string> ListFatherProcess(string name)
        {
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                return pgm.listFatherProcess(name);
            }
            else
            {
                return new List<string>();
            }

        }

        // retorna todos os processos em forma de "Cascade" para o usuario
        public void CallListProcess()
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);
            var connections = GetAllConnectionIdsByAuthId(AuthId).ToList();
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                Clients.Clients(connections).listProcessos(pgm.getProcessoToClient());
            }
        }























        
        // cria uma esteira e a insere no programa
        public void CreateEsteira(string name, string desc, int inlimit, int type, string additional)
        {
            Program pgm = CheckReturnPgm();

            if (pgm != null)
            {
                // procura pela esteira e veficica se exite
                if (pgm.listEsteiras.Find(x => x.Name.Equals(name)) == null)
                {
                    EsteiraAbstrata e = null;

                    // verifica o tipo de esteira e trabalha de acordo com o tipo
                    switch (type)
                    {
                        // esteira model
                        case 1:

                            Processo p = pgm.listProcessos.Find(x => x.Name.Equals(additional));

                            if (p != null)
                            {
                                e = new EsteiraModel("", name, desc, inlimit);
                                EsteiraModel em = (EsteiraModel)e;

                                em.insertMasterProcess(p);
                            }
                            else
                            {
                                RegisterMessageDashboard("Processo in Select '" + additional + "' não encontrado", 3, true);
                            }

                            break;

                        // esteira de armazenamento
                        case 2:
                            e = new EsteiraArmazenamento("", name, desc, inlimit);
                            break;

                        // esteira etiquetadora
                        case 3:

                            try
                            {
                                int initialValue = int.Parse(additional);

                                e = new EsteiraEtiquetadora(pgm.Login, "", name, desc, inlimit, initialValue);
                            }
                            catch (System.Exception)
                            {
                                RegisterMessageDashboard("Error: create EsteiraEtiquetadora, Try", 3, true);
                            }

                            break;

                        // esteira de desvio
                        case 4:
                            try
                            {
                                int typeD = int.Parse(additional);

                                if (typeD == 1)
                                {
                                    e = new EsteiraBalanceadora("", name, desc, inlimit);
                                }
                                else if (typeD == 2)
                                {
                                    e = new EsteiraSeletora("", name, desc, inlimit);
                                }
                                else
                                {
                                    RegisterMessageDashboard("Tipo de Desvio inválido: " + typeD, 3, true);
                                }
                            }
                            catch (System.Exception)
                            {
                                RegisterMessageDashboard("Error: create EsteiraEtiquetadora, Try", 3, true);
                            }

                            break;

                        // caso seja bulado o sistema de tipo, cai aqui
                        default:
                            RegisterMessageDashboard("Error: Default type in CreateEsteira", 3, true);
                            break;
                    }

                    // cria a esteira apenas se ela foi instanciada
                    if (e != null)
                    {
                        pgm.CriarEsteira(e);
                        RegisterMessageDashboard("Esteira '" + name + "' Criada", 1, true);
                        CallListEsteira();
                    }
                }
                else
                {
                    RegisterMessageDashboard("Esteira '" + name + "' já existente", 2, true);
                }
            }
        }


        // cria uma esteira e a insere no programa
        public void ChangingEsteira(string oldname, string newname, string desc, int inlimit, int type, string additional)
        {
            Program pgm = CheckReturnPgm();

            if (pgm != null)
            {
                // procura pela esteira e veficica se exite
                if (pgm.listEsteiras.Find(x => x.Name.Equals(newname)) == null)
                {
                    EsteiraAbstrata e = pgm.listEsteiras.Find(x => x.Name.Equals(oldname));

                    if (e != null)
                    {
                        e.Name = newname;
                        e.Description = desc;
                        e.InLimit = inlimit;

                        // verifica o tipo de esteira e trabalha de acordo com o tipo
                        switch (type)
                        {
                            // esteira model
                            case 1:

                                Processo p = pgm.listProcessos.Find(x => x.Name.Equals(additional));

                                if (p != null)
                                {
                                    EsteiraModel em = (EsteiraModel)e;

                                    if (!em.NameProcessMaster.Equals(additional))
                                    {
                                        em.insertMasterProcess(p);
                                    }
                                }
                                else
                                {
                                    RegisterMessageDashboard("Processo in Select '" + additional + "' não encontrado", 3, true);
                                }

                                break;

                            // esteira de armazenamento
                            case 2:
                                break;

                            // esteira etiquetadora
                            case 3:

                                try
                                {
                                    int initialValue = int.Parse(additional);

                                    if (EsteiraEtiquetadora.RangeIsPossible(pgm.Login, initialValue))
                                    {
                                        EsteiraEtiquetadora ee = (EsteiraEtiquetadora)e;
                                        ee.InitialValue = initialValue;
                                    }
                                    else
                                    {
                                        RegisterMessageDashboard("Range não permitido (já utilizado)", 3, true);
                                    }
                                }
                                catch (System.Exception)
                                {
                                    RegisterMessageDashboard("Error: create EsteiraEtiquetadora, Try", 3, true);
                                }

                                break;

                            // esteira de desvio
                            case 4:
                                break;

                            // caso seja bulado o sistema de tipo, cai aqui
                            default:
                                RegisterMessageDashboard("Error: Default type in CreateEsteira", 3, true);
                                break;
                        }

                        RegisterMessageDashboard("Esteira '" + newname + "' Alterada", 1, true);
                        CallListEsteira();
                    }
                    else
                    {
                        RegisterMessageDashboard("Error: Find Esteira: '" + oldname + "'", 3, true);
                    }
                }
                else
                {
                    RegisterMessageDashboard("Esteira '" + oldname + "' já existente", 2, true);
                }
            }
        }

        // cria uma esteira e a insere no programa
        public void DeleteEsteira(string name)
        {
            Program pgm = CheckReturnPgm();

            if (pgm != null)
            {

                EsteiraAbstrata e = pgm.listEsteiras.Find(x => x.Name.Equals(name));

                if (e != null)
                {
                    pgm.removeEsteira(e);

                    RegisterMessageDashboard("Esteira '" + name + "' Deletado", 1, true);
                    CallListEsteira();
                }
                else
                {
                    RegisterMessageDashboard("Error: Find Process: '" + name + "'", 3, true);
                }
            }
        }

        // retorna todas as esteiras separadas em tipos para o usuario
        public void CallListEsteira()
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);
            var connections = GetAllConnectionIdsByAuthId(AuthId).ToList();

            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                ListEsteiraClient l = pgm.getEsteirasToClient();

                Debug.WriteLine("========= Debug List E to Client Recursive =========");
                for (int i = 0; i < l.listArmazenamento.Count; i++)
                {
                    Debug.WriteLine(l.listArmazenamento[i] +" > "+ l.listArmazenamento[i].EsteiraOutput);
                }
                for (int i = 0; i < l.listDesvio.Count; i++)
                {
                    Debug.WriteLine(l.listDesvio[i].Id + " {");
                    for (int j = 0; j < l.listDesvio[i].EsteiraOutput.Count; j++)
                    {
                        Debug.WriteLine(l.listDesvio[i].EsteiraOutput[j]);
                    }
                    Debug.WriteLine("}");
                }
                for (int i = 0; i < l.listEtiquetadora.Count; i++)
                {
                    Debug.WriteLine(l.listEtiquetadora[i] + " > " + l.listArmazenamento[i].EsteiraOutput);
                }
                for (int i = 0; i < l.listModel.Count; i++)
                {
                    Debug.WriteLine(l.listModel[i] + " > " + l.listModel[i].EsteiraOutput);
                }

                Debug.WriteLine("======= Fim Debug List E to Client Recursive =======");

                Clients.Clients(connections).listEsteiras(pgm.getEsteirasToClient());
            }
        }









        // retorna os ids iniciais dos cloness
        public SendClassTable getValuesPgm()
        {
            SendClassTable sendClassTable = new SendClassTable();
            sendClassTable.Array = new int[6];

            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {

                sendClassTable.Array[0] = pgm.IdCloneEm;
                sendClassTable.Array[1] = pgm.IdCloneEa;
                sendClassTable.Array[2] = pgm.IdCloneEe;
                sendClassTable.Array[3] = pgm.IdCloneEd;

                sendClassTable.Array[4] = pgm.MinX;
                sendClassTable.Array[5] = pgm.MinY;





                MapCell[,] AuxMapCells = pgm.ArrayMapCells;





                if (AuxMapCells != null)
                {
                    for (int i = 0; i < AuxMapCells.GetLength(0); i++)
                    {
                        for (int j = 0; j < AuxMapCells.GetLength(1); j++)
                        {
                            MapCell aux = (MapCell)AuxMapCells.GetValue(i, j);

                            if (aux != null)
                            {
                                if (aux.Esteira != null)
                                {
                                    aux.Esteira = (EsteiraAbstrata)pgm.listEsteiras.Find(x => x.Id.Equals(aux.Esteira.Id)).Clone();
                                }

                                aux.Up = null;
                                aux.Front = null;
                                aux.Down = null;
                                aux.Back = null;
                            }
                        }

                    }
                }



                sendClassTable.ArrayMapCells = AuxMapCells;

            }

            return sendClassTable;

        }









        // mapeamento das esteiras
        public void saveTableProduction(dynamic recivedServ)
        {
            Program pgm = CheckReturnPgm();

            if (pgm != null)
            {

                try
                {
                    var array = recivedServ.array;

                    // cria uma matriz 2d
                    MapCell[,] matrizMapCell;

                    // inicializa a matriz com o tamanho da tabela recebida
                    matrizMapCell = new MapCell[array.Count, array[0].Count];

                    for (int i = 0; i < array.Count; i++)
                    {

                        for (int j = 0; j < array[i].Count; j++)
                        {
                            // objeto separado
                            var obj = array[i][j];

                            // classes do objeto (div)
                            string[] classes = new string[obj.classes.Count];

                            // conteudo dentro da div
                            string children = obj.children;

                            // o objeto deve conter classes,
                            // conteudo (no caso o nome do objeto que esta dentro da div),
                            // id (o id do clone)
                            // e idM (id da Esteira Base)
                            if (classes.Count() != 0 && children != null && obj.id != null)
                            {
                                // copia todas as classes para um array de string para realizar a conversão de dynamic > string
                                for (int k = 0; k < obj.classes.Count; k++)
                                {
                                    classes[k] = obj.classes[k];
                                }

                                // converte paar string
                                string id = obj.id;

                                // cria um objeto MapCell (array de classes, conteudo, linha, coluna)
                                matrizMapCell[i, j] = new MapCell(id, classes, children, obj.dataObj, i, j);

                                // procura a esteira com o id do clone na lis6ta geral de esteiras
                                matrizMapCell[i, j].Esteira = pgm.listEsteiras.Find(x => x.Id.Equals(id));

                                // caso nao ache, cria um novo com o clone da Base
                                if (matrizMapCell[i, j].Esteira == null && matrizMapCell[i, j].hasClass("esteiraP") && matrizMapCell[i, j].DataObj != null)
                                {
                                    string idM = matrizMapCell[i, j].DataObj.idM;

                                    EsteiraAbstrata e = pgm.listEsteiras.Find(x => x.Id.Equals(idM));

                                    if (e != null)
                                    {
                                        matrizMapCell[i, j].Esteira = (EsteiraAbstrata)e.Clone();
                                        matrizMapCell[i, j].Esteira.Id = id;

                                        pgm.listEsteiras.Add(matrizMapCell[i, j].Esteira);
                                    }
                                    else
                                    {
                                        throw new Exception("Esteira Base = null, idM = " + idM + " (linha: " + i + ", Coluna: " + j + ")");
                                    }

                                }
                            }
                        }
                    }

                    for (int i = 0; i < matrizMapCell.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrizMapCell.GetLength(1); j++)
                        {
                            // verifica se contem o objeto na matriz
                            if (matrizMapCell.GetValue(i, j) != null)
                            {

                                // verifica e faz as atribuições para organizar as "celulas" de forma dinamica
                                // assim facilitando na recursividade
                                if (i > 0)
                                {
                                    ((MapCell)matrizMapCell.GetValue(i, j)).Up = (MapCell)matrizMapCell.GetValue(i - 1, j);
                                }
                                if (j > 0)
                                {
                                    ((MapCell)matrizMapCell.GetValue(i, j)).Back = (MapCell)matrizMapCell.GetValue(i, j - 1);
                                }
                                if (j < matrizMapCell.GetLength(1) - 1)
                                {
                                    ((MapCell)matrizMapCell.GetValue(i, j)).Front = (MapCell)matrizMapCell.GetValue(i, j + 1);
                                }
                                if (i < matrizMapCell.GetLength(0) - 1)
                                {
                                    ((MapCell)matrizMapCell.GetValue(i, j)).Down = (MapCell)matrizMapCell.GetValue(i + 1, j);
                                }
                            }

                        }

                    }

                    // metodo que inicia faz "ajustes" e atribuições e inicia o mapeamento
                    pgm.mapeamentoEsteiras(matrizMapCell);

                    // atribui os ultimos ids registrados
                    pgm.IdCloneEm = recivedServ.countIdEm;
                    pgm.IdCloneEa = recivedServ.countIdEa;
                    pgm.IdCloneEe = recivedServ.countIdEe;
                    pgm.IdCloneEd = recivedServ.countIdEd;

                    pgm.MinX = recivedServ.minX;
                    pgm.MinY = recivedServ.minY;

                    RegisterMessageDashboard("Projeto Salvo", 1, true);

                }
                catch (Exception e)
                {
                    RegisterMessageDashboard(e.Message, 3, true);
                }
            }
        }

        public void turnOnEsteira(string id)
        {
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                List<string> list = new List<string>();

                EsteiraAbstrata e = pgm.listEsteiras.Find(x => x.Id.Equals(id));

                if (e != null)
                {
                    e.TurnOn(pgm);

                    if (!pgm.InSimulation)
                    {
                        RegisterMessageDashboard("Simulação iniciada, Alterações travadas", 2, false);
                    }

                    RegisterMessageDashboard("Esteira '" + e.Name + "' ligada", 1, false);

                    pgm.InSimulation = true;
                }
                else
                {
                    RegisterMessageDashboard("Salve o programa para que as alterações tenham efeito", 2, true);
                }

            }
        }

        public void turnOffEsteira(string id)
        {
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                List<string> list = new List<string>();

                EsteiraAbstrata e = pgm.listEsteiras.Find(x => x.Id.Equals(id));

                if (e != null)
                {
                    e.TurnOff();
                    RegisterMessageDashboard("Esteira '" + e.Name + "' desligada", 1, false);
                }
                else
                {
                    RegisterMessageDashboard("Salve o programa para que as alterações tenham efeito", 2, true);
                }

                bool controlePgm = false;

                foreach (var x in pgm.listEsteiras)
                {
                    if (x.Ligado)
                    {
                        controlePgm = true;
                        break;
                    }
                }

                pgm.InSimulation = controlePgm;

                if (!pgm.InSimulation)
                {
                    RegisterMessageDashboard("Simulação parada, Alterações destravadas", 2, false);
                }
            }
        }

        public void chumbarPeca(string id, int qtd)
        {
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                List<string> list = new List<string>();

                EsteiraAbstrata e = pgm.listEsteiras.Find(x => x.Id.Equals(id));

                if (e != null)
                {
                    int i = 0;
                    bool control = true;
                    while (control && i < qtd)
                    {
                        if (e.InsertPiece(new Peca()))
                        {
                            i++;
                        }
                        else
                        {
                            control = false;
                        }
                    }

                    if (i < qtd)
                    {
                        RegisterMessageDashboard("Inseridas " + i + " peças na esteira '" + e.Name + "' (limite atingido)", 2, true);
                    }
                    else
                    {
                        RegisterMessageDashboard("Inseridas " + i + " peças na esteira '" + e.Name + "'", 1, true);
                    }
                }
                else
                {
                    RegisterMessageDashboard("Salve o programa para que as alterações tenham efeito", 2, true);
                }

            }
        }

        public List<Peca> getPieces(string id)
        {
            List<Peca> list = null;

            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {

                EsteiraAbstrata e = pgm.listEsteiras.Find(x => x.Id.Equals(id));

                if (e != null)
                {
                    list = e.getToList();
                }
                else
                {
                    RegisterMessageDashboard("Salve o programa para que as alterações tenham efeito", 2, true);
                }

            }

            return list;
        }












        public async Task<string> getAttDashboard()
        {
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                inDashboard = true;
                pgm.InDashboard++;

                Clients.Caller.showToast("Initialized Dashboard");

                Clients.Caller.setNivelDash(pgm.NivelDash);

                Clients.Caller.reciveListDashboard(pgm.listDashboard);
                while (inDashboard)
                {
                    Clients.Caller.reciveTickDashboard(pgm.listTickDashboard);
                    await Task.Delay(250);
                }
                Clients.Caller.showToast("Finished Dashboard");
            }

            return "Finished Dashboard";
        }

        public void clearDashboard()
        {
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                pgm.listDashboard.Clear();
                pgm.listTickDashboard.Clear();
                Clients.Caller.showToast("Dashboard cleaned");
            }
        }

        public void alterDash(int nivel)
        {
            Program pgm = CheckPgmInSimulation();

            if (pgm != null)
            {
                pgm.NivelDash = nivel;
            }
        }
    }

    public class SendClassTable
    {
        public int[] Array { get; set; }
        public MapCell[,] ArrayMapCells { get; set; }
    }
}
