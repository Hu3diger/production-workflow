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

    connector.client.listProcessos = function (jsonRecived) {
        $("#listProcesso").html("");
        limpaInfo();

        generateList(jsonRecived, $("#listProcesso"));
    }

    connector.client.listEsteiras = function (jsonRecived) {
        limpaAbout();

        console.log(jsonRecived);

        $("#esteirasModel").html("");
        $("#esteirasArma").html("");
        $("#esteirasEtiq").html("");
        $("#esteirasDesvio").html("");

        generateListEsteira(jsonRecived.listModel, $("#esteirasModel"), 1);
        generateListEsteira(jsonRecived.listArmazenamento, $("#esteirasArma"), 2);
        generateListEsteira(jsonRecived.listEtiquetadora, $("#esteirasEtiq"), 3);
        generateListEsteira(jsonRecived.listDesvio, $("#esteirasDesvio"), 4);
    }

};