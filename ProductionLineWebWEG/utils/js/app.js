var ws;
    ws = new WebSocket("ws://localhost:8085/client");
    
    ws.onopen = function() {
        alert("Conectado com sucesso!");
    };

    ws.onmessage = function(evt) {
        //evt.data

        //oq fazer depois de conectado
    };

    ws.onclose = function() {
        alert("Conexão fechada.");
    };

function sendMessage() {
    var message = document.getElementById("inputMessage").value;
    if (message&&ws) {
        var content = "[" + username + "] " + message;
        ws.send(content);
    }
}