var connector = connector || {};

$(function () {
    connector = $.connection.masterHub;

    $.connection.hub.qs = {
        SessionId: $.cookie('SessionId'),
        AuthId: $.cookie('AuthId'),
    };

    ServerReciveMethods();

    $.connection.hub.start().done(function () {
        console.log("Conected");
        callListProcess();
    });
});