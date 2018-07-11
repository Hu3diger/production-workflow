function showModalEsteira() {
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
          <label id="lLimiteP" for="limiteP">Limite de entrada</label>\
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
        <a class="waves-effect waves-light btn right green darken-4" onclick="enviaProcess()"><i class="material-icons left">sd_card</i>criar</a>\
      </div>\
    </div>\
    </form>\
    </div>\
    ');
    $('select').formSelect();

    $("#selectTE").change(function () {
        let value = $('#selectTE :selected').val();

        if (value == '1') {
            connector.server.listFatherProcess('').done(function (json) {
                var html = '<div class="input-field col s12 m8 offset-m2">\
            <select id="processSelectFather">\
            <option value="0" disabled selected>Sem Processo Pai</option>';

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
            $("#more").html('\
        <div class="input-field col s12 m8 offset-m2">\
          <input value="" id="armazenamento" type="number" class="validate">\
          <label for="armazenamento">Limite de armazenamento (-1 = ilimitado)</label>\
        </div>\
        '); 
            $("#limiteP").val(null);
            $("#lLimiteP").removeAttr("class");

            $("#limiteP").removeAttr("class");
            $("#limiteP").attr("class", "validate");
            
            $("#limiteP").attr("disabled", true);
        } else if (value == '3') {
            $("#more").html('\
        <div class="input-field col s12 m8 offset-m2">\
          <input value="" id="etiqueta" type="number" class="validate">\
          <label for="etiqueta">Valor inicial para geração das etiquetas</label>\
        </div>\
        ');
            $("#limiteP").attr("disabled", false);
        } else if (value == '4') {
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

    //    $('body').keyup(function(e) {
    //      if(e.keyCode  == 27) dimissModalEsteira("modal1");
    //    });

    //    $(document).mouseup(function(e) {
    //      let container = $("#modal1");
    //      if(!container.is(e.target) && container.has(e.target).length === 0) dimissModalEsteira("modal1");
    //    })
}

function limpaAbout() {
    $("#aboutEsteira").html("");
}