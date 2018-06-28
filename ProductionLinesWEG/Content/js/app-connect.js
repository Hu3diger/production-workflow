var connector = connector || {};

$(function () {
    connector = $.connection;
    $.connection.hub.start();
});