// todos os methodos do servidor > cliente, dentro dessa função
function ServerReciveMethods() {

    connector.client.acceptLoginUser = function () {
        window.location.href = '@Url.Action("Index", "Dashboard")';
    };

    connector.client.showToast = function (message) {
        M.toast({ html: message })
    };

};