    //função para criação dos processos
    function criaProcesso() {
        $("#info").html('');

        //traz um json com todos os processos pais do sistema possíveis
        connector.server.listFatherProcess('').done(function (json) {
            var html = '\
        <div class="row">\
        <form class="col s12">\
            <div class="row">\
            <div class="input-field col s12 m8 offset-m2">\
                <input value="" id="nameP" type="text" class="validate">\
                <label for="nameP">Nome</label>\
            </div>\
            <div class="input-field col s12 m8 offset-m2">\
                <input value="" id="descP" type="text" class="validate">\
                <label for="descP">Descrição do processo</label>\
            </div>\
        </div>\
        <div class="row">\
            <div class="input-field col s12 m6 offset-m2">\
            <input value="0" id="runtimeP" type="number" class="validate active">\
            <label class="active" for="runtimeP">Tempo de execução (ms)</label>\
            </div>\
            <div class="input-field col s12 m2">\
                <input value="0" id="variationP" type="number" class="validate active">\
                <label class="active" for="variationP">Variação (%)</label>\
            </div>\
        </div>\
            <div class="row">\
            <div class="input-field col s12 m6 offset-m2">\
                <select id="processSelectFather">\
                <option value = "0" selected>Sem Processo Pai</option>\
            ';
          //percorre o json para exibir, dentro do select, todos os possíveis processos pais
            for (var i = 1; i < json.length + 1; i++) {
                html += '<option value="' + i + '">' + json[i - 1] + '</option>';
            }

            html += '</select>\
                <label>Processo pai</label>\
                </div>\
                <div class="input-field col s12 m2">\
                    <input value="0" id="positionP" type="number" class="validate active">\
                    <label class="active" for="positionP">Posição</label>\
                </div>\
            </div>\
            <div class="row">\
            <div class="col s12 m8 offset-m2">\
            <a class="waves-effect waves-light btn left red darken-4" onclick="limpaInfo()"><i class="material-icons left">cancel</i>cancelar</a>\
            <a class="waves-effect waves-light btn right green darken-4" onclick="enviaProcess()"><i class="material-icons left">sd_card</i>criar</a>\
            </div>\
        </div>\
        </form>\
    </div>';
            $("#info").html(html);
            $('select').formSelect();
        });
    }

    //função para exibir/alterar as informações dos processos
    function exibeInfo(id) {
        //atribui na variável data, as informações vindas do item selecionado
        let data = $("#" + id).data();
        let name = "'" + data.Name + "'";

        //verifica se data não é undefined
        if (void 0 !== data) {
            connector.server.listFatherProcess(data.Name).done(function (json) {
                var html = '<div class="row">\
        <form class="col s12">\
        <div class="row">\
            <div class="input-field col s12 m8 offset-m2">\
            <input value="' + data.Name + '" id="nameP" type="text" class="validate">\
            <label class="active" for="nameP">Nome</label>\
            </div>\
            <div class="input-field col s12 m8 offset-m2">\
            <input value="' + data.Description + '" id="descP" type="text" class="validate">\
            <label class="active" for="descP">Descrição do processo</label>\
            </div>\
        </div>\
        <div class="row">\
            <div class="input-field col s12 m6 offset-m2">\
            <input value="' + data.Runtime + '" id="runtimeP" type="number" class="validate active">\
            <label class="active" for="runtimeP">Tempo de execução (ms)</label>\
            </div>\
            <div class="input-field col s12 m2">\
                <input value="' + data.VariationRuntime + '" id="variationP" type="number" class="validate active">\
                <label class="active" for="variationP">Variação (%)</label>\
            </div>\
        </div>\
            <div class="row">\
            <div class="input-field col s12 m6 offset-m2">\
                <select id="processSelectFather">\
                <option value = "0" ' + (data.Father == null ? 'selected' : '') + '>Sem Processo Pai</option>\
            ';
                //percorre o json, preenchendo o select com os possíveis processos pais.
                for (var i = 1; i < json.length + 1; i++) {
                    html += '<option value="' + i + (data.Father != null ? (data.Father.Name == json[i - 1] ? '" selected>' : '">') : '">') + json[i - 1] + '</option>';
                }

                html += '</select>\
                <label>Processo pai</label>\
                </div>\
                <div class="input-field col s12 m2">\
                    <input value="' + data.Position + '" id="positionP" type="number" class="validate active">\
                    <label class="active" for="positionP">Posição</label>\
                </div>\
            </div>\
            <div class="row">\
                <div class="col s12 m8 offset-m2">\
                <a class="waves-effect waves-light btn left red darken-4 modal-trigger" href="#modal1"><i class="material-icons left">delete_forever</i>deletar</a>\
                <a class="waves-effect waves-light btn right green darken-4" id="saveProcess" onclick="alteraProcess('+ name + ')"><i class="material-icons left">sd_card</i>salvar</a>\
            </div>\
        </div>\
        </form>\
    </div>\
    <div id="modal1" class="modal">\
        <div class="modal-content">\
            <h5>Atenção!</h5>\
            <p>Deseja mesmo deletar este processo?</p>\
            <p>Obs: Irá também deletar todos os processos filhos do processo selecionado.   </p>\
        </div>\
        <div class="modal-footer">\
            <a href="#!" class="modal-close waves-effect www waves-light btn-flat">Não</a>\
            <a href="#!" class="modal-close waves-effect waves-light btn green darken-4" onclick="deleteProcess('+ name +')">Sim</a>\
        </div>\
    </div>\
    ';
                $("#info").html(html);
                $('select').formSelect();
                $('.modal').modal();
            });
        }
    }

    //função para limpar a id #info
    function limpaInfo() {
        $("#info").html("");
    }

    //função para criação do processo
    function enviaProcess() {
        //verifica se há campos vazios
        if (
            $("#nameP").val() === "" ||
            $("#descP").val() === "" ||
            $("#runtimeP").val() === "" ||
            $("#variationP").val() === "" ||
            $("#positionP").val() === ""
        ) {
            M.toast({ html: 'Existem campos em branco' })
        } else {
            let value = '';
            if ($('#processSelectFather :selected').val() != '0') {
                value = $('#processSelectFather :selected').text();
            }
            //chama a função para criar o processo
            createProcess(
                $("#nameP").val(),
                $("#descP").val(),
                $("#runtimeP").val(),
                $("#variationP").val(),
                value,
                $("#positionP").val(),
            );
        }
    }

    //função para alteração de processos
    function alteraProcess(oldname) {
        //verifica se há campos vazios
        if (
            $("#nameP").val() === "" ||
            $("#descP").val() === "" ||
            $("#runtimeP").val() === "" ||
            $("#variationP").val() === "" ||
            $("#positionP").val() === ""
        ) {
            M.toast({ html: 'Existem campos em branco' })
        } else {
            let value = '';
            if ($('#processSelectFather :selected').val() != '0') {
                value = $('#processSelectFather :selected').text();
            }
            //chama a função para a alteração do processo
            changingProcess(
                oldname,
                $("#nameP").val(),
                $("#descP").val(),
                $("#runtimeP").val(),
                $("#variationP").val(),
                value,
                $("#positionP").val(),
            );
        }
    }

    //função para geração da lista de processos
    function generateList(data, $e) {
        // cria um item interno 
        function createInner(obj, $target, id) {

            //adiciona a tag li dentro do target
            var li = $("<li>").appendTo($target);

            //cria uma variável boolean, e atribui o valor correspondente a condição
            var tf = obj.ListProcessos != undefined && obj.ListProcessos.length > 0;

            //adiciona dentro do li, a uma div com o respectivo processo.
            li.append(
                '<div id="' + id + '" class="collapsible-header' + (tf ? " padl" : "") + '" tabindex="0" onclick="exibeInfo(this.id)">\
            <div class="col s12 m12' + (tf ? " padl" : "") + '">\
                ' + (tf ? '<i class="material-icons colorIconPrincipal mw">chevron_right</i>' : "") + '\
                <i class="fas fa-cogs colorIconPrincipal"></i>' + obj.Name + "\
            </div>\
        </div>"
            );

            //atribui ao id, seus respectivos valores
            $("#" + id).data("Name", obj.Name);
            $("#" + id).data("Description", obj.Description);
            $("#" + id).data("Runtime", obj.Runtime);
            $("#" + id).data("Father", obj.Father);
            $("#" + id).data("VariationRuntime", obj.VariationRuntime);
            $("#" + id).data("Position", obj.Position);

            //verifica se o processo tem filhos para iniciar o processo de criação da árvore de processos
            if (tf) {

                //cria outra div dentro da tag li, criada anteriormente.
                var div = $('<div class="collapsible-body padd">').appendTo(li);

                //cria uma ul dentro da tag div criada acima,
                var innerList = $('<ul class= "collapsible" >').appendTo(div);

                //percorre a lista de processos, criando a árvore de sub-processos
                for (var i = 0; i < obj.ListProcessos.length; i++) {
                    var child = obj.ListProcessos[i];
                    //chama a função atual novamente, criando a mesma estrutura para os processos filhos
                    //criando a árvore de processos, dentro do processo pai
                    createInner(child, innerList, id + "-" + i);
                }
            }
        }

        //percorre a lista de processos "pais" e os cria.
        for (var i = 0; i < data.length; i++) {
            createInner(data[i], $e, "process" + i);
        }

        $(".collapsible").collapsible();
    }
