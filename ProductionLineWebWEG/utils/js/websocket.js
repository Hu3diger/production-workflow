var ws;
ws = new WebSocket("ws://localhost:8085/client");

ws.onopen = function() {
  alert("Conectado com sucesso!");
};

ws.onmessage = function(evt) {
  var t = [];
  t = evt.data.split("/../");
  inserirT(t[0], t[1]);
};

ws.onclose = function() {
  alert("Conexão fechada.");
};

function sendMessage(message) {
  if (message && ws) {
    ws.send(message);
  }
}

// Funções para o servidor

function cadProcess(){
  let msg = "/cadBaseProcess/" + $("#name_Process").val() + "/../" + $("#description_tx").val() + "/../" + $("#runTime_Process").val();
  sendMessage(msg);
  M.toast({html: 'Processo criado com sucesso!'});
}