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
    var t = true;
    //função para atualizar o dashboard
    connector.client.reciveDashboard = function (jMessage) {
        try {
            var jsonDashboard = [];

            for (var i = 0; i < jMessage.length; i++) {
                var v = jMessage[i];


                let line = {
                    horario: v.Date,
                    mensagem: v.Message,
                    critico: v.Critico
                };

                jsonDashboard.push(line);
            }

            inserirT(jsonDashboard);
        } catch (e) {

        }
    };

    //lista os processos na tela de processos
    connector.client.listProcessos = function (jsonRecived) {
        $("#listProcesso").html("");
        limpaInfo();

        generateList(jsonRecived, $("#listProcesso"));
    }

    //lista as esteiras na tela de esteiras
    connector.client.listEsteiras = function (jsonRecived) {
        try {
            limpaAbout();
        } catch (e) {
            // null
        }

        try {
            $("#esteirasModel").html("");
            $("#esteirasArma").html("");
            $("#esteirasEtiq").html("");
            $("#esteirasDesvio").html("");

            generateListEsteira(jsonRecived.listModel, $("#esteirasModel"), 1);
            generateListEsteira(jsonRecived.listArmazenamento, $("#esteirasArma"), 2);
            generateListEsteira(jsonRecived.listEtiquetadora, $("#esteirasEtiq"), 3);
            generateListEsteira(jsonRecived.listDesvio, $("#esteirasDesvio"), 4);
        } catch (e) {
            // null
        }

        try {
            $("#esteirasModelProd").html("");
            $("#esteirasArmaProd").html("");
            $("#esteirasEtiqProd").html("");
            $("#esteirasDesvioProd").html("");

            // Pagina Produção
            generateListEsteiraProd(jsonRecived.listModel, $("#esteirasModelProd"), 1);
            generateListEsteiraProd(jsonRecived.listArmazenamento, $("#esteirasArmaProd"), 2);
            generateListEsteiraProd(jsonRecived.listEtiquetadora, $("#esteirasEtiqProd"), 3);
            generateListEsteiraProd(jsonRecived.listDesvio, $("#esteirasDesvioProd"), 4);
        } catch (e) {
            // null
        }

        try {
            $("#tmodelo tbody").html("");
            $("#tarmazenamento tbody").html("");
            $("#tetiquetadora tbody").html("");
            $("#tdesvio tbody").html("");

            // Pagina Planta
            generateListProducao(jsonRecived.listModel, $("#tmodelo tbody"), 1, "blue darken-4");
            generateListProducao(jsonRecived.listArmazenamento, $("#tarmazenamento tbody"), 2, "orange darken-4");
            generateListProducao(jsonRecived.listEtiquetadora, $("#tetiquetadora tbody"), 3, "lime darken-2");
            generateListProducao(jsonRecived.listDesvio, $("#tdesvio tbody"), 4, "teal darken-2");

            setDropDragItens();
        } catch (e) {
            // null
        }
    }

};