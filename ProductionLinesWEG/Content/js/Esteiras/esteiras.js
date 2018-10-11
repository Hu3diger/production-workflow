//função para a exibição dos campos de criação das esteiras
function showCreate() {
    //inserindo todo o html abaixo dentro da tag aboutEsteira
    $("#aboutEsteira").html('\
    <div class="row">\
    <form class="col s12">\
    <div class="row">\
      <div class="input-field col s12 m8 offset-m2">\
          <input value="" id="nameP" type="text" class="validate">\
          <label for="nameP">Nome</label>\
        </div>\
        <div class="input-field col s12 m8 offset-m2">\
          <input value="" id="descP" type="text" class="validate">\
          <label for="descP">Descrição da esteira</label>\
        </div>\
        <div class="input-field col s12 m8 offset-m2">\
          <input value="" id="limiteP" type="number" class="validate">\
          <label id="lLimiteP" for="limiteP">Limite de entrada (-1 = ilimitado)</label>\
        </div>\
      </div>\
    </div>\
    <div class="row">\
    <div class="input-field col s12 m8 offset-m2">\
      <select id="selectTE">\
          <option value="0" disabled selected>Selecione o tipo da esteira</option>\
          <option value="1">Esteira de processamento</option>\
          <option value="2">Esteira de armazenamento</option>\
          <option value="3">Esteira etiquetadora</option>\
          <option value="4">Esteira de desvio</option>\
      </select>\
      <label>Tipo de esteira</label>\
    </div>\
    </div>\
      <div class="row" id="more">\
      </div>\
    <div class="row">\
      <div class="col s12 m8 offset-m2">\
        <a href="" onclick="limpaAbout()">cancelar</a>\
        <a class="waves-effect waves-light btn right green darken-4" onclick="saveEsteira()"><i class="material-icons left">sd_card</i>criar</a>\
      </div>\
    </div>\
    </form>\
    </div>\
    ');
    $('select').formSelect();

    $("#selectTE").change(function () {
        //pega o valor selecionado no select
        let value = $('#selectTE :selected').val();

        //verificação de qual item foi selecionado
        if (value == '1') {
            var html2 = '';
            var html1 = '\
                <div class="input-field col s12 m8 offset-m2" >\
                    <select id="esteiraSelectProcess">\
                        <option value="0" disabled selected>Sem Processo Pai</option>\
                    </select>\
                    <label>Processo master</label>\
                </div>\
            ';

            $("#more").html(html1);

            connector.server.listFatherProcess('').done(function (json) {

                //percorre o json colocando os itens do servidor dentro do select (exibe todos os processos)
                for (var i = 1; i < json.length + 1; i++) {
                    html2 += '<option value="' + i + '">' + json[i - 1] + '</option>';
                }

                $("#esteiraSelectProcess").html(html2);
                $('select').formSelect();
            });

            $('select').formSelect();

            $("#limiteP").attr("disabled", false);
        } else if (value == '2') {

            $("#more").html("");

        } else if (value == '3') {
            //acrescenta um input na tag #more, conforme a opção selecionada no select
            $("#more").html('\
            <div class="input-field col s12 m8 offset-m2">\
                <input value="" id="etiqueta" type="number" class="validate">\
                <label for="etiqueta">Valor inicial para geração das etiquetas</label>\
            </div>\
        ');
            $("#limiteP").attr("disabled", false);
        } else if (value == '4') {
            //acrescenta um input na tag #more, conforme a opção selecionada no select
            $("#more").html('\
            <div class="input-field col s12 m8 offset-m2">\
                <input value="" id="desvio" type="text" class="validate">\
                <label for="desvio">Mais coisa pro desvio</label>\
            </div>\
        ');
            $("#limiteP").attr("disabled", false);
        } else {
            $("#more").html("");
            $("#limiteP").attr("disabled", false);
        }
    });
}


//função para exibir os campos para edição
//recebe a id da esteira selecionada no collapsible
function alteraEsteira(id) {
    let data = $("#" + id).data();
    let name = "'" + data.Name + "'";
    if (void 0 !== data) {
        $("#aboutEsteira").html('\
    <div class="row">\
    <form class="col s12">\
    <div class="row">\
      <div class="input-field col s12 m8 offset-m2">\
          <input value="'+ data.Name + '" id="nameP" type="text" class="validate active">\
          <label class="active" for="nameP">Nome</label>\
        </div>\
        <div class="input-field col s12 m8 offset-m2">\
          <input value="'+ data.Description + '" id="descP" type="text" class="validate active">\
          <label class="active" for="descP">Descrição da esteira</label>\
        </div>\
        <div class="input-field col s12 m8 offset-m2">\
          <input value="'+ data.InLimit + '" id="limiteP" type="number" class="validate active">\
          <label class="active" id="lLimiteP" for="limiteP">Limite de entrada (-1 = ilimitado)</label>\
        </div>\
      </div>\
    </div>\
    <div class="row">\
    <div class="input-field col s12 m8 offset-m2">\
      <select id="selectTE" disabled>\
          <option value="'+ data.TypeN + '" selected>' + data.Type + '</option>\
      </select>\
      <label>Tipo de esteira</label>\
    </div>\
    </div>\
      <div class="row" id="more">\
      </div>\
    <div class="row">\
      <div class="col s12 m8 offset-m2">\
      <a href="" onclick="limpaAbout()">Cancelar</a>\
        <a class="waves-effect waves-light btn right green darken-4" onclick="changeEsteira(\''+ data.Name +'\')"><i class="material-icons left">sd_card</i>salvar</a>\
        <a class="waves-effect waves-light btn right red darken-4 modal-trigger" href="#modal1"><i class="material-icons left">delete_forever</i>deletar</a>\
      </div>\
    </div>\
    </form>\
    </div>\
    <div id="modal1" class="modal">\
        <div class="modal-content">\
            <h5>Atenção!</h5>\
            <p>Deseja mesmo deletar esta esteira?</p>\
            <p>Obs: Irá também deletar todos os registros da esteira selecionada.   </p>\
        </div>\
        <div class="modal-footer">\
            <a href="#!" class="modal-close waves-effect www waves-light btn-flat">Não</a>\
            <a href="#!" class="modal-close waves-effect waves-light btn green darken-4" onclick="deleteEsteira(\''+ data.Name +'\')">Sim</a>\
        </div>\
    </div>\
    ');
        
        //variável pega o tipo da esteira selecionada para edição
        let value = data.TypeN;

        if (value == '1') {
            connector.server.listFatherProcess('').done(function (json) {
                var html = '<div class="input-field col s12 m8 offset-m2">\
            <select id="esteiraSelectProcess">';

                for (var i = 1; i < json.length + 1; i++) {
                    html += '<option value="' + i + (data.Addtional == json[i - 1] ? '" selected>' : '">') + json[i - 1] + '</option>';
                }

                html += '</select>\
            <label>Processo master</label>\
            </div>';

                $("#more").html(html);
                $('select').formSelect();
            });
            $("#limiteP").attr("disabled", false);
        } else if (value == '2') {

            $("#more").html("");

        } else if (value == '3') {
            //acrescenta um input na tag #more, conforme a opção selecionada no select, contendo o valor da esteira
            $("#more").html('\
            <div class="input-field col s12 m8 offset-m2">\
                <input value="'+ data.Addtional + '" id="etiqueta" type="number" class="validate active">\
                <label class="active" for="etiqueta">Valor inicial para geração das etiquetas</label>\
            </div>\
        ');
            $("#limiteP").attr("disabled", false);
        } else if (value == '4') {
            //acrescenta um input na tag #more, conforme a opção selecionada no select, contendo o valor da esteira
            $("#more").html('\
            <div class="input-field col s12 m8 offset-m2">\
                <input value="'+ data.Addtional + '" id="desvio" type="text" class="validate active">\
                <label class="active" for="desvio">Mais coisa pro desvio</label>\
            </div>\
        ');
            $("#limiteP").attr("disabled", false);
        } else {
            $("#more").html("");
            $("#limiteP").attr("disabled", false);
        }

        $('select').formSelect();
        $('.modal').modal();
    }
}

//função para salvar a esteira (criar ela no servidor)
function saveEsteira() {

    //variavel com o tipo da esteira, tipo vindo do item selecionado no collapsible
    let type = $('#selectTE :selected').val();
    let value = '';
    var typeBool = true;
    console.log($('#selectTE :selected').val());

    //faz a verificação do tipo, e atribui para a tag value, o valor do campo extra de cada tipo de esteira.
    if (type == '1') {
        if ($('#esteiraSelectProcess :selected').val() == '0') {
            M.toast({ html: 'Processo inválido' });
            typeBool = false;
        } else {
            value = $('#esteiraSelectProcess :selected').text();
        }
    } else if (type == '2') {
        value = ' ';
    } else if (type == '3') {
        value = $('#etiqueta').val();
    } else if (type == '4') {
        value = $('#desvio').val();
    } else {
        M.toast({ html: 'Type inválido' });
        typeBool = false;
    }

    //verifica se o tipo existe
    if (typeBool) {
        //verifica se os campos estão em branco
        if (
            $("#nameP").val() === "" ||
            $("#descP").val() === "" ||
            $("#limiteP").val() === "" ||
            value === ""
        ) {
            M.toast({ html: 'Existem campos em branco' })
        } else {
            //chama o método para a criação da esteira
            createEsteira(
                $("#nameP").val(),
                $("#descP").val(),
                $("#limiteP").val(),
                type,
                value.toString()
            );
        }
    }
}

//função para salvar a esteira (criar ela no servidor)
function changeEsteira(oldName) {

    //variavel com o tipo da esteira, tipo vindo do item selecionado no collapsible
    let type = $('#selectTE :selected').val();
    let value = '';
    var typeBool = true;
    console.log($('#selectTE :selected').val());

    //faz a verificação do tipo, e atribui para a tag value, o valor do campo extra de cada tipo de esteira.
    if (type == '1') {
        if ($('#esteiraSelectProcess :selected').val() == '0') {
            M.toast({ html: 'Processo inválido' });
            typeBool = false;
        } else {
            value = $('#esteiraSelectProcess :selected').text();
        }
    } else if (type == '2') {
        value = ' ';
    } else if (type == '3') {
        value = $('#etiqueta').val();
    } else if (type == '4') {
        value = $('#desvio').val();
    } else {
        M.toast({ html: 'Type inválido' });
        typeBool = false;
    }

    //verifica se o tipo existe
    if (typeBool) {
        //verifica se os campos estão em branco
        if (
            $("#nameP").val() === "" ||
            $("#descP").val() === "" ||
            $("#limiteP").val() === "" ||
            value === ""
        ) {
            M.toast({ html: 'Existem campos em branco' })
        } else {
            //chama o método para a criação da esteira
            changingEsteira(
                oldName,
                $("#nameP").val(),
                $("#descP").val(),
                $("#limiteP").val(),
                type,
                value.toString()
            );
        }
    }
}

//função para limpar a tag #aboutEsteira do html.
function limpaAbout() {
    $("#aboutEsteira").html("");
}

//função para gerar a lista das esteiras
function generateListEsteira(data, $e, type) {

    //função para gerar a lista interna das esteiras
    function InnerList(obj, $target) {
        var li = $("<li>").appendTo($target);
        //acrescenta dentro da ul->li as div's contendo as esteiras
        li.append(
            '<div id="' + obj.Id + '" class="collapsible-header" tabindex="0" onclick="alteraEsteira(this.id)">\
                <div class="col s12 m12">\
                    <i class="fas fa-cogs colorIconPrincipal"></i>' + obj.Name + "\
                </div>\
            </div>"
        );

        var jObj = $("#" + obj.Id);

        //salva informações dentro da id do item
        jObj.data("Name", obj.Name);
        jObj.data("Description", obj.Description);
        jObj.data("InLimit", obj.InLimit);
        jObj.data("TypeN", type);

        //verifica o tipo do item, e acrescenta valores específicos para cada tipo
        if (type == 1) {
            jObj.data("Addtional", obj.NameProcessMaster);
            jObj.data("Type", "Esteira de Processamento");
        } else if (type == 2) {
            jObj.data("Type", "Esteira de armazenamento");
        } else if (type == 3) {
            jObj.data("Addtional", obj.InitialValue);
            jObj.data("Type", "Esteira etiquetadora");
        } else if (type == 4) {
            jObj.data("Addtional", "");
            jObj.data("Type", "Esteira de desvio");
        }
    }

    if (data.length > 0) {
        var ul = $("<ul class='collapsible'>").appendTo($e);
    }

    //for para a criação de várias esteiras no collapsible
    for (var i = 0; i < data.length; i++) {
        InnerList(data[i], ul);
    }

    $(".collapsible").collapsible();
}