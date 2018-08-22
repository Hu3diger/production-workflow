//função para exibir os campos para edição
//recebe a id da esteira selecionada no collapsible
function showProd(id) {
    let data = $("#" + id).data();
    let name = "'" + data.Name + "'";
    if (void 0 !== data) {
        $("#listaProd").html('\
    <div class="row">\
        <form class="col s12 l10 offset-l1">\
            <div class="row">\
                <table>\
                    <tr>\
                        <th>Peça</th>\
                        <th>Ação</th>\
                    </tr>\
                    <tr>\
                        <td>Banana de aço</td>\
                        <td><a href="#!">Detalhes</a></td>\
                    </tr>\
                    <tr>\
                        <td>'+ data.InLimit +'</td>\
                        <td><a href="#!">Detalhes</a></td>\
                    </tr>\
                </table>\
            </div>\
    </div>\
    <div class="row">\
      <div class="col s12 l10 offset-l1">\
        <a class="waves-effect waves-light btn left red darken-4" onclick="limpaProd()"><i class="material-icons left">cancel</i>Cancelar</a>\
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

//função para gerar a lista das esteiras
function generateListEsteiraProd(data, $e, type) {

    //função para gerar a lista interna das esteiras
    function InnerList(obj, $target) {
        var li = $("<li>").appendTo($target);
        //acrescenta dentro da ul->li as div's contendo as esteiras
        li.append(
            '<div id="' + obj.Id + '" class="collapsible-header" tabindex="0" onclick="showProd(this.id)">\
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
            jObj.data("Type", "Esteira Modelo");
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

function limpaProd(){
    $('#listaProd').html('');
}