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
          <option value="1">Esteira Model</option>\
          <option value="2">Esteira de armazenamento</option>\
          <option value="3">Esteira etiquetadora</option>\
          <option value="4">Esteira de desvio</option>\
      </select>\
      <label>Tipo de esteira</label>\
    </div>\
    </div>\
      <div class="row" id="more">\</div>\
    <div class="row">\
      <div class="col s12 m8 offset-m2">\
        <a class="waves-effect waves-light btn left red darken-4" onclick="limpaAbout()"><i class="material-icons left">cancel</i>cancelar</a>\
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
            connector.server.listFatherProcess('').done(function (json) {
                var html = '<div class="input-field col s12 m8 offset-m2">\
            <select id="esteiraSelectProcess">\
            <option value="0" disabled selected>Sem Processo Pai</option>';

                //percorre o json colocando os itens do servidor dentro do select (exibe todos os processos)
                for (var i = 1; i < json.length + 1; i++) {
                    html += '<option value="' + i + '">' + json[i - 1] + '</option>';
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
          <option value="0" selected>'+ data.Type + '</option>\
      </select>\
      <label>Tipo de esteira</label>\
    </div>\
    </div>\
      <div class="row" id="more">\
      </div>\
    <div class="row">\
      <div class="col s12 m8 offset-m2">\
        <a class="waves-effect waves-light btn left red darken-4" onclick="limpaAbout()"><i class="material-icons left">cancel</i>cancelar</a>\
        <a class="waves-effect waves-light btn right green darken-4" onclick=""><i class="material-icons left">sd_card</i>salvar</a>\
      </div>\
    </div>\
    </form>\
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

    }
}

//função para salvar a esteira (criar ela no servidor)
function saveEsteira() {

    //variavel com o tipo da esteira, tipo vindo do item selecionado no collapsible
    let type = $('#selectTE :selected').val();
    let value = '';
    var typeBool = true;

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

//função para limpar a tag #aboutEsteira do html.
function limpaAbout() {
    $("#aboutEsteira").html("");
}

//função para gerar a lista das esteiras
function generateListEsteira(data, $e, type, initialId) {

    //função para gerar a lista interna das esteiras
    function InnerList(obj, $target, id) {
        var li = $("<li>").appendTo($target);
        //acrescenta dentro da ul->li as div's contendo as esteiras
        li.append(
            '<div id="' + id + '" class="collapsible-header" tabindex="0" onclick="alteraEsteira(this.id)">\
            <div class="col s12 m12">\
                <i class="fas fa-cogs colorIconPrincipal"></i>' + obj.Name + "\
            </div>\
        </div>"
        );

        //salva informações dentro da id do item
        $("#" + id).data("Name", obj.Name);
        $("#" + id).data("Description", obj.Description);
        $("#" + id).data("InLimit", obj.InLimit);
        $("#" + id).data("TypeN", type);

        //verifica o tipo do item, e acrescenta valores específicos para cada tipo
        if (type == 1) {
            $("#" + id).data("Addtional", obj.NameProcessMaster);
            $("#" + id).data("Type", "Esteira Modelo");
        } else if (type == 2) {
            $("#" + id).data("Type", "Esteira de armazenamento");
        } else if (type == 3) {
            $("#" + id).data("Addtional", obj.InitialValue);
            $("#" + id).data("Type", "Esteira etiquetadora");
        } else if (type == 4) {
            $("#" + id).data("Addtional", "");
            $("#" + id).data("Type", "Esteira de desvio");
        }
    }

    //for para a criação de várias esteiras no collapsible
    for (var i = 0; i < data.length; i++) {
        InnerList(data[i], $e, "esteira" + type + "-" + i);
    }

    $(".collapsible").collapsible();
}