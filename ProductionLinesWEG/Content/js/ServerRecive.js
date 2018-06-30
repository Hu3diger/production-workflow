// todos os methodos do servidor > cliente, dentro dessa função
function ServerReciveMethods() {

    connector.client.acceptLoginUser = function (key) {
        $.cookie('loginAuthKey', key);

        window.location.href = '/Dashboard';
    };

    connector.client.showToast = function (message) {
        M.toast({ html: message })
    };

};