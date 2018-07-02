// todos os methodos do servidor > cliente, dentro dessa função
function ServerReciveMethods() {

    connector.client.cookie = function (name, key) {
        $.cookie(name, key);
    };

    connector.client.acceptLoginUser = function (key) {
        $.cookie('AuthId', key);

        window.location.href = '/Dashboard';
    };

    connector.client.showToast = function (message) {
        M.toast({ html: message })
    };

};