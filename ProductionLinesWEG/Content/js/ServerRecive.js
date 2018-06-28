// todos os methodos do servidor > cliente, dentro dessa função
function ServerReciveMethods() {

    connector.client.showToast = function (message) {
        M.toast({ html: message })
    };

};