function requestLogin(user, pass) {

    connector.server.requestLogin(user, pass);

}

function logOut() {

    $.removeCookie('AuthId');

    connector.server.logOut().done(function () {
        window.location.href = '/Login';
    });

}

function changingProcess(name, desc, runtime) {

    connector.server.changingProcess(name, desc, runtime);

}