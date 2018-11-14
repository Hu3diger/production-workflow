//requisita a atualização do dashboard continuamente
function testConnection() {

    connector.server.testConnection();

}

//requisita o login ao servidor, passando senha e user para validação
function requestLogin(user, pass) {

    connector.server.requestLogin(user, pass);

}

//requisita a atualização do dashboard continuamente
function getFirstAttDashboard() {

    connector.server.getFirstAttDashboard().done(function (rs) {
        if (rs == "Fail") {

        }
    });

}

//função para deslogar do servidor, remove o cookie e, redireciona para a tela de login
function logOut() {

    $.removeCookie('AuthId');

    connector.server.logOut().done(function () {
        window.location.href = '/Login';
    });
}

//função para a criação de processos, recebendo os dados do processo e enviando para o servidor
function createProcess(name, desc, runtime, variationRuntime, probability, nameFather, position) {

    connector.server.createProcess(name, desc, runtime, variationRuntime, probability, nameFather, position);

}

//função para alterar o processo, recebendo os dados novos e velhos, e os enviando para o servidor
function changingProcess(oldname, newname, desc, runtime, variationRuntime, probability, nameFather, position) {

    connector.server.changingProcess(oldname, newname, desc, runtime, variationRuntime, probability, nameFather, position);

}

//função para a remoção de processos, recebendo os dados do processo e enviando para o servidor
function deleteProcess(name) {

    connector.server.deleteProcess(name);

}

//função para a criação de esteiras, recebendo os dados da esteira e enviando para o servidor
function createEsteira(name, desc, inlimit, type, additional) {

    connector.server.createEsteira(name, desc, inlimit, type, additional);

}

//função para a alteração de esteiras, recebendo os dados da esteira e enviando para o servidor
function changingEsteira(oldName, newName, desc, inlimit, type, additional) {

    connector.server.changingEsteira(oldName, newName, desc, inlimit, type, additional);

}

//função para a remoção de processos, recebendo os dados do processo e enviando para o servidor
function deleteEsteira(name) {

    connector.server.deleteEsteira(name);

}

//função para requisitar a listagem das esteiras
function callListEsteira() {

    connector.server.callListEsteira();

}

//função para requisitar a listagem dos processos
function callListProcess() {

    connector.server.callListProcess();

}

//função que exibe uma mensagem no console (debug) do servidor
function showDebug(msg) {

    connector.server.showDebug(msg);

}

function turnOnEsteira(id) {

    connector.server.turnOnEsteira(id);

}

function turnOffEsteira(id) {

    connector.server.turnOffEsteira(id);

}

function chumbarPeca(id, qtd) {

    connector.server.chumbarPeca(id, qtd);

}

function clearDashboard() {

    $("#xuxu").html("");

    connector.server.clearDashboard();

}

function alterDash(nivel) {
    connector.server.alterDash(nivel);
}

function getTickDashboard() {
    connector.server.getTickDashboard();
}

function getTickEsteira() {
    connector.server.getTickEsteira($("#item").data().id);
}

function getFirstConnection() {
    connector.server.firstConnection();
}

function getNavColor() {
    connector.server.getNavColor();
}

function turnOnAllFront(obj) {

    connector.server.turnOnAllFront($(obj).attr("id"));

}

function turnOffAllFront(obj) {

    connector.server.turnOffAllFront($(obj).attr("id"));

}