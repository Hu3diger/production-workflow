// todos os methodos do servidor > cliente, dentro dessa função
function ServerReciveMethods() {

    connector.client.acceptLoginUser = function () {
        M.toast({ html: "Recived" })
        //window.location.href = '@Url.Action("Index", "Dashboard")';
    };

    connector.client.showToast = function (message) {
        M.toast({ html: message })
    };

};