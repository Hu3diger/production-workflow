var socket = new io.Socket("10.3.74.190", {
  port: 8085
});
socket.connect();

// Add a connect listener
socket.on("connect", function() {
  alert("Conectado com sucesso!");
});
// Add a connect listener
socket.on("message", function(data) {
  var t = [];
  t = evt.data.split("/../");
  inserirT(t[0], t[1]);
});
// Add a disconnect listener
socket.on("disconnect", function() {
  alert("Conexão fechada.");
});

// Sends a message to the server via sockets
function sendMessage() {
  if (message && ws) {
    var content = "[" + username + "] " + message;
    ws.send(content);
  }
}
