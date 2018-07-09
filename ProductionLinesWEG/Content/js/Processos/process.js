
function criaProcesso() {
    $("#info").html('\
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
          <input value="" id="runtimeP" type="number" class="validate">\
          <label for="runtimeP">Tempo de execução (ms)</label>\
        </div>\
        <div class="input-field col s12 m2">\
            <input value="" id="variation" type="number" class="validate">\
            <label for="runtimeP">Variação (%)</label>\
        </div>\
      </div>\
      <div class="row">\
        <div class="input-field col s12 m8 offset-m2">\
          <select>\
            <option value="" disabled selected>Selecione o processo pai</option>\
            <option value="1">Option 1</option>\
            <option value="2">Option 2</option>\
            <option value="3">Option 3</option>\
          </select>\
          <label>Processo pai</label>\
        </div>\
      </div>\
      <div class="row">\
        <div class="col s12 m8 offset-m2">\
          <a class="waves-effect waves-light btn left red darken-4" onclick="limpaInfo()"><i class="material-icons left">cancel</i>cancelar</a>\
          <a class="waves-effect waves-light btn right green darken-4" onclick="enviaProcess()"><i class="material-icons left">sd_card</i>criar</a>\
        </div>\
      </div>\
    </form>\
    </div>'
    );
    $('select').formSelect();
    // $("select").formSelect();

    //$("body").keyup(function(e) {
    //   if (e.keyCode == 27) dimissModalProcesso("modal1");
    // });

    // $(document).mouseup(function(e) {
    //   let container = $("#modal1");
    //   if (!container.is(e.target) && container.has(e.target).length === 0)
    //     dimissModalProcesso("modal1");
    //});
}

function exibeInfo(id) {
    // let name = $(this).children().text().replace(/\s/g, '');
    let data = $("#" + id).data();
    let name = "'" + data.Name + "'";
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
            <input value="' + data.VariationRuntime + '" id="variation" type="number" class="validate active">\
            <label class="active" for="variation">Variação (%)</label>\
        </div>\
      </div>\
        <div class="row">\
          <div class="input-field col s12 m6 offset-m2">\
            <select id="processSelectFather">\
            <option value = "0" ' + (data.Father == null ? 'selected' : '') + '>Sem Processo Pai</option>\
        ';

            for (var i = 1; i < json.length + 1; i++) {
                html += '<option value="' + i + (data.Father != null ? (data.Father.Name == json[i - 1] ? '" selected>' : '">') : '">') + json[i - 1] + '</option>';
            }

            html += '</select>\
            <label>Processo pai</label>\
            </div>\
            <div class="input-field col s12 m2">\
                 <input value="' + data.Position + '" id="position" type="number" class="validate active">\
                <label class="active" for="position">Posição</label>\
            </div>\
        </div>\
        <div class="row">\
            <div class="col s12 m8 offset-m2">\
            <a class="waves-effect waves-light btn left red darken-4" id="saveProcess"><i class="material-icons left">delete_forever</i>deletar</a>\
            <a class="waves-effect waves-light btn right green darken-4" id="saveProcess" onclick="alteraProcess('+ name + ')"><i class="material-icons left">sd_card</i>salvar</a>\
        </div>\
      </div>\
    </form>\
  </div>';
            $("#info").html(html);
            $('select').formSelect();
        });
    }
}

function limpaInfo() {
    $("#info").html("");
}

function enviaProcess() {
    if (
        $("#nameP").val() === "" ||
        $("#descP").val() === "" ||
        $("#runtimeP").val() === ""
    ) {
        M.toast({ html: 'Existem campos em branco' })
    } else {
        let value = '';
        if ($('#processSelectFather :selected').val() != '0') {
            value = $('#processSelectFather :selected').text();
        }
        createProcess(
            $("#nameP").val(),
            $("#descP").val(),
            $("#runtimeP").val(),
            value
        );
        $("#info").html("");
        callListProcess();
    }
}

function alteraProcess(oldname) {
    if (
        $("#nameP").val() === "" ||
        $("#descP").val() === "" ||
        $("#runtimeP").val() === ""
    ) {
        M.toast({ html: 'Existem campos em branco' })
    } else {
        let value = ''; 
        if($('#processSelectFather :selected').val() != '0'){
            value = $('#processSelectFather :selected').text();
        }
        changingProcess(
            oldname,
            $("#nameP").val(),
            $("#descP").val(),
            $("#runtimeP").val(),
            value
        );
        $("#info").html("");

        callListProcess();
    }
}

function generateList(data, $e) {
    // create an inner item
    function createInner(obj, $target, id) {
        var li = $("<li>").appendTo($target);

        var tf = obj.ListProcessos != undefined && obj.ListProcessos.length > 0;

        li.append(
            '<div id="' + id + '" class="collapsible-header' + (tf ? " padl" : "") + '" tabindex="0" onclick="exibeInfo(this.id)">\
          <div class="col s12 m12' + (tf ? " padl" : "") + '">\
            ' + (tf ? '<i class="material-icons t mw">chevron_right</i>' : "") + '\
            <i class="fas fa-cogs t"></i>' + obj.Name + "\
          </div>\
      </div>"
        );

        $("#" + id).data("Name", obj.Name);
        $("#" + id).data("Description", obj.Description);
        $("#" + id).data("Runtime", obj.Runtime);
        $("#" + id).data("Father", obj.Father);
        $("#" + id).data("VariationRuntime", obj.VariationRuntime);
        $("#" + id).data("Position", obj.Position);

        if (tf) {
            var div = $('<div class="collapsible-body padd">').appendTo(li);

            var innerList = $('<ul class= "collapsible" >').appendTo(div);

            for (var i = 0; i < obj.ListProcessos.length; i++) {
                var child = obj.ListProcessos[i];
                createInner(child, innerList, id + "-" + i);
            }
        }
    }

    for (var i = 0; i < data.length; i++) {
        createInner(data[i], $e, "process" + i);
    }

    console.log(data);

    $(".collapsible").collapsible();
}
