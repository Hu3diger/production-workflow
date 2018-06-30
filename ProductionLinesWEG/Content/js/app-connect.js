var connector = connector || {};

$(function () {
    connector = $.connection.masterHub;

    ServerReciveMethods();

    $.connection.hub.start().done(function () { console.log("Conected"); });
});