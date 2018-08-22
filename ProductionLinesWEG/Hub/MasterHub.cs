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
        public string AuthId { get; set; }
        public HashSet<string> SessionGroup { get; set; }

        public SessionAuth(string authId, HashSet<string> sessionGroup)
        {
            AuthId = authId;
            SessionGroup = sessionGroup;
        }
    }

    // iisexpress-proxy 53139 to 3000
    // classe principal para a communicação com o servidor
    public class MasterHub : Microsoft.AspNet.SignalR.Hub
    {
        // lista de programas, "sessões"
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

        // lista de logins para controle de acesso
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



        // verifica se o usuario existe e aceita ou não usuario
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

                // invoca uma função do cliente para aceita-lo
                Clients.Caller.acceptLoginUser(l.AuthId);

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

        // função para o usuario chamar para exibir uma mensagem no console (Debug) do sistema
        public void ShowDebug(string msg)
        {
            Debug.WriteLine(msg);
        }
























        // cria um processo e o adiciona a lista fazendo varias verificações
        public void CreateProcess(string name, string description, int runTime, double variationRuntime, string nameFather, int position)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            // procura pelo programa que corresponde ao usuario (AuthId)
            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

            if (pgm != null)
            {
                if (pgm.listProcessos.Find(x => x.Name.Equals(name)) == null)
                {
                    // cria o processo e insere suas primeiras informações
                    Processo p = new Processo(new BaseProcesso(name, description, runTime));
                    p.BaseProcesso.VariationRuntime = variationRuntime;

                    pgm.CriaProcesso(p);

                    if (!nameFather.Equals(""))
                    {
                        Processo pcss = pgm.listProcessos.Find(x => x.Name.Equals(nameFather));
                        if (pcss != null)
                        {
                            if (pcss.FindInternalProcess(name) == null)
                            {
                                pcss.AddInternalProcess(position - 1, p);
                            }
                            else
                            {
                                Clients.Caller.showToast("Error: In Create: '" + name + "' Internal Process Found");
                            }
                        }
                        else
                        {
                            Clients.Caller.showToast("Pai '" + nameFather + "' não encontrado");
                        }
                    }

                    Clients.Caller.showToast("Processo '" + name + "' Criado");
                    CallListProcess();
                }
                else
                {
                    Clients.Caller.showToast("Processo '" + name + "' já existente");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
            }

        }

        // edita um processo
        public void ChangingProcess(string oldName, string newName, string description, int runTime, double variationRuntime, string nameFather, int position)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            // procura pelo programa que corresponde ao usuario (AuthId)
            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

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

                        if (!nameFather.Equals(""))
                        {
                            Processo pcssF = pgm.listProcessos.Find(x => x.Name.Equals(nameFather));

                            if (pcssF != null)
                            {
                                pcss.removerFather();
                                pcssF.AddInternalProcess(position - 1, pcss);
                            }
                            else
                            {
                                Clients.Caller.showToast("Pai '" + nameFather + "' não encontrado");
                            }
                        }
                        else
                        {
                            pcss.removerFather();
                        }

                        Clients.Caller.showToast("Processo '" + newName + "' Alterado");
                        CallListProcess();
                    }
                    else
                    {
                        Clients.Caller.showToast("Error: Find Process: '" + oldName + "'");
                    }
                }
                else
                {
                    Clients.Caller.showToast("Processo '" + newName + "' já existente");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
            }

        }

        // deleta um processo
        public void DeleteProcess(string name)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            // procura pelo programa que corresponde ao usuario (AuthId)
            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

            if (pgm != null)
            {

                Processo p = pgm.listProcessos.Find(x => x.Name.Equals(name));

                if (p != null)
                {
                    // pega todos os filhos e os remove (com o proprio processo junto)
                    p.GetInternalOrderProcess().ForEach(x =>
                    {
                        x.removerFather();
                        pgm.listProcessos.Remove(x);
                    });

                    Clients.Caller.showToast("Processo '" + name + "' Deletado");
                    CallListProcess();
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

        // lista os processos disponiveis para determinado processo (name)
        public List<string> ListFatherProcess(string name)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            // procura pelo programa que corresponde ao usuario (AuthId)
            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

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

        // retorna todos os processos em forma de "Cascade" para o usuario
        public void CallListProcess()
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {
                var connections = GetAllConnectionIdsByAuthId(AuthId).ToList();

                // procura pelo programa que corresponde ao usuario (AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

                if (pgm != null)
                {
                    Clients.Clients(connections).listProcessos(pgm.getProcessoToClient());
                }
                else
                {
                    Clients.Caller.showToast("Error: pgm CallListProcess");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: no Login");
            }
        }
























        // cria uma esteira e a insere no programa
        public void CreateEsteira(string name, string desc, int inlimit, int type, string additional)
        {

            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            var connections = GetAllConnectionIdsByConnectionId(Context.ConnectionId);

            // procura pelo programa que corresponde ao usuario (AuthId)
            Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

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
                                Clients.Caller.showToast("Processo '" + additional + "' não encontrado");
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

                                e = new EsteiraEtiquetadora("", name, desc, inlimit, initialValue);
                            }
                            catch (System.Exception)
                            {
                                Clients.Caller.showToast("Error: create EsteiraEtiquetadora, Try");
                            }

                            break;

                        // esteira de desvio
                        case 4:
                            Clients.Caller.showToast("Não é possivel criar esteiras desviadoras ainda");
                            break;

                        // caso seja bulado o sistema de tipo, cai aqui
                        default:
                            Clients.Caller.showToast("Error: Default type");
                            break;
                    }

                    // cria a esteira apenas se ela foi instanciada
                    if (e != null)
                    {
                        pgm.CriarEsteira(e);
                        Clients.Caller.showToast("Esteira '" + name + "' Criada");
                        CallListEsteira();
                    }
                }
                else
                {
                    Clients.Caller.showToast("Esteira '" + name + "' já existente");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: AuthId: '" + AuthId + "'");
            }
        }

        // retorna todas as esteiras separadas em tipos para o usuario
        public void CallListEsteira()
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {
                var connections = GetAllConnectionIdsByAuthId(AuthId).ToList();

                // procura pelo programa que corresponde ao usuario (AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

                if (pgm != null)
                {
                    Clients.Clients(connections).listEsteiras(pgm.getEsteirasToClient());
                }
                else
                {
                    Clients.Caller.showToast("Error: pgm CallListEsteira");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: no Login");
            }
        }









        // retorna os ids iniciais dos cloness
        public SendClassTable getValuesPgm()
        {
            SendClassTable sendClassTable = new SendClassTable();

            sendClassTable.Array = new int[6];

            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {

                // procura pelo programa que corresponde ao usuario (AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

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
                                    aux.Esteira = null;

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
                else
                {
                    Clients.Caller.showToast("Error: pgm getIdsClone");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: no Login");
            }

            return sendClassTable;

        }









        // mapeamento das esteiras
        public void saveTableProduction(dynamic recivedServ)
        {

            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {

                // procura pelo programa que corresponde ao usuario (AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

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

                                    // procura a esteira com o id do clone na lista geral de esteiras
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
                                            throw new Exception("Esteira Base = null, idM = " + idM);
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

                        Clients.Caller.showToast("Project Saved");

                    }
                    catch (Exception e)
                    {
                        Clients.Caller.showToast(e.Message);
                    }
                }
                else
                {
                    Clients.Caller.showToast("Error: pgm mapEsteiras");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: no Login");
            }
        }

        public List<string> getInformationEsteira(string id)
        {
            string AuthId = GetAuthIdByConnectionId(Context.ConnectionId);

            if (!AuthId.Equals(""))
            {

                // procura pelo programa que corresponde ao usuario (AuthId)
                Program pgm = listProgram.Find(x => x.AuthId.Equals(AuthId));

                if (pgm != null)
                {
                    List<string> list = new List<string>();

                    EsteiraAbstrata e = pgm.listEsteiras.Find(x => x.Id.Equals(id));

                    if (e != null)
                    {
                        e.EsteiraInput.ForEach(x => list.Add(x.Name));
                    }
                    else
                    {
                        Clients.Caller.showToast("Salve o programa para que as alterações tenham efeito");
                    }

                    return list;
                }
                else
                {
                    Clients.Caller.showToast("Error: pgm getInformationEsteira");
                }
            }
            else
            {
                Clients.Caller.showToast("Error: no Login");
            }

            return null;
        }
    }

    public class SendClassTable
    {
        public int[] Array { get; set; }
        public MapCell[,] ArrayMapCells { get; set; }
    }
}
