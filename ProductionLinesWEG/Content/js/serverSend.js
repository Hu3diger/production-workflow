function requestLogin(user, pass) {

    connector.server.requestLogin(user, pass);

}

function logOut() {

    $.removeCookie('AuthId');

    connector.server.logOut().done(function () {
        window.location.href = '/Login';
    });

}

function createProcess(name, desc, runtime, variationRuntime, nameFather, position) {

    connector.server.createProcess(name, desc, runtime, variationRuntime, nameFather, position);

}

function changingProcess(oldname, newname, desc, runtime, variationRuntime, nameFather, position) {

    connector.server.changingProcess(oldname, newname, desc, runtime, variationRuntime, nameFather, position);

}

function deleteProcess(name) {

    connector.server.deleteProcess(name);

}

function callListProcess() {

    connector.server.callListProcess();
    connector.server.callListProcess();
    connector.server.callListProcess();
}

function showDebug(msg) {

    connector.server.showDebug(msg);

}