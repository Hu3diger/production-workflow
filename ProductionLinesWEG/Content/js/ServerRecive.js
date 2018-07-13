// todos os methodos do servidor > cliente, dentro dessa função
function ServerReciveMethods() {

    //registra um cookie no browser
    connector.client.cookie = function (name, key) {
        $.cookie(name, key);
    };

    //aceita o login do usuário, registra o cookie "AuthId" e o redireciona para a dashboard
    connector.client.acceptLoginUser = function (key) {
        $.cookie('AuthId', key);

        window.location.href = '/Dashboard';
    };

    //função para exibir toast, pode ser utilizada no servidor
    connector.client.showToast = function (message) {
        M.toast({ html: message })
    };

    //lista os processos na tela de processos
    connector.client.listProcessos = function (jsonRecived) {
        $("#listProcesso").html("");
        limpaInfo();

        generateList(jsonRecived, $("#listProcesso"));
    }

    //lista as esteiras na tela de esteiras
    connector.client.listEsteiras = function (jsonRecived) {
        limpaAbout();


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