//requisita o login ao servidor, passando senha e user para validação
function requestLogin(user, pass) {

    connector.server.requestLogin(user, pass);

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

//função para a criação de esteiras, recebendo os dados da esteira e enviando para o servidor
function createEsteira(name, desc, inlimit, type, additional) {

    connector.server.createEsteira(name, desc, inlimit, type, additional);

}

//função para a remoção de processos, recebendo os dados do processo e enviando para o servidor
function deleteProcess(name) {

    connector.server.deleteProcess(name);

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

function saveTableProduction(array) {

    connector.server.saveTableProduction(array);

}