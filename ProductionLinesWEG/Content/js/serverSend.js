function requestLogin(user, pass) {

    connector.server.requestLogin(user, pass);

}

function logOut() {

    $.removeCookie('AuthId');

    connector.server.logOut().done(function () {
        window.location.href = '/Login';
    });

}

function createProcess(name, desc, runtime) {

    connector.server.createProcess(name, desc, runtime);

}

function changingProcess(oldname, newname, desc, runtime) {

    connector.server.changingProcess(oldname, newname, desc, runtime);

}

function deleteProcess(name) {

    connector.server.deleteProcess(name);

}

function callListProcess() {

    connector.server.callListProcess();

}