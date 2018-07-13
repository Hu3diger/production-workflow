//criação de um novo objeto connector
var connector = connector || {};

//função para estabelecer da conexão (utilizando SignalR)
$(function () {
    connector = $.connection.masterHub;

    $.connection.hub.qs = {
        SessionId: $.cookie('SessionId'),
        AuthId: $.cookie('AuthId'),
    };
    //registra os métodos que o servidor pode chamar neste cliente.
    ServerReciveMethods();

    //starta a conexão
    $.connection.hub.start().done(function () {
        console.log("Conected");
        callListProcess();
        callListEsteira();
    });
});