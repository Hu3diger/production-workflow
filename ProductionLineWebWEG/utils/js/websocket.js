var ws;
ws = new WebSocket("ws://10.3.74.190:8085/client");

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

function sendMessage() {
  if (message && ws) {
    var content = "[" + username + "] " + message;
    ws.send(content);
  }
}
