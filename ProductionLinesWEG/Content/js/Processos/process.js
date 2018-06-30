function showModalProcesso() {
    $("#modal1").append('\
    <div class="modal-content">\
        <h5>Cadastrar nova esteira</h5>\
        <div class="row"></div>\
        <div class="row">\
          <div class="input-field col s12 m10 offset-m1">\
            <input id="nameModal" type="text" class="validate">\
            <label for="nameModal">Nome</label>\
          </div>\
        </div>\
        <div class="row">\
          <div class="input-field col s12 m10 offset-m1">\
            <textarea id="descModal" class="materialize-textarea"></textarea>\
            <label for="descModal">Descrição do processo</label>\
          </div>\
        </div>\
        <div class="row">\
          <div class="input-field col s12 m10 offset-m1">\
            <input id="runtimeModal" type="number" class="validate">\
            <label for="runtimeModal">Tempo de execução (ms)</label>\
          </div>\
        </div>\
        <div class="row">\
          <div class="col s12 m8 l8 offset-m2 offset-l2">\
            <div class="error-div left warning left-align">\
            </div>\
          </div>\
        </div>\
      </div>\
      <div class="modal-footer">\
        <a href="#!" class="modal-close waves-effect waves-green btn-flat" onclick="dimissModalProcesso(\'modal1\');">Cancelar</a>\
        <a href="#!" class="waves-effect waves-green btn-flat" onclick="dimissModalProcesso(\'modal1\');">Salvar</a>\
      </div>\
    ');
    $('select').formSelect();

    $('body').keyup(function (e) {
        if (e.keyCode == 27) dimissModalProcesso("modal1");
    });

    $(document).mouseup(function (e) {
        let container = $("#modal1");
        if (!container.is(e.target) && container.has(e.target).length === 0) dimissModalProcesso("modal1");
    })
}

function exibeInfo(){
  // let name = $(this).children().text().replace(/\s/g, '');
  let name = $(this).children().data("text");
  if (void 0 !== name) {
    console.log(name);
    $('#info').html('\
    <div class="row">\
    <form class="col s12">\
      <div class="row">\
        <div class="input-field col s12 m8 offset-m2">\
          <input disabled value="'+ name +'" id="nameInfo" type="text" class="validate">\
          <label class="active" for="nameInfo">Nome</label>\
        </div>\
        <div class="input-field col s12 m8 offset-m2">\
          <input disabled value="Teste 123" id="descInfo" type="text" class="validate">\
          <label class="active" for="descInfo">Descrição do processo</label>\
        </div>\
      </div>\
      <div class="row">\
        <div class="input-field col s12 m8 offset-m2">\
          <input disabled value="333" id="runtimeInfo" type="text" class="validate active">\
          <label class="active" for="runtimeInfo">Tempo de execução (ms)</label>\
        </div>\
      </div>\
      <div class="row">\
        <div class="col s12 m8 offset-m2">\
          <a>\
            <label>\
              <input class="indeterminate-checkbox" onchange="enable();" type="checkbox" />\
              <span>Editar</span>\
            </label>\
          </a>\
          <a class="waves-effect waves-light btn right blue darken-4" disabled id="saveProcess"><i class="material-icons left">sd_card</i>salvar</a>\
        </div>\
      </div>\
    </form>\
  </div>\
  ');
  }
}

function dimissModalProcesso(Tipo) {
    changingProcess($("#nameModal").val(), $("#descModal").val(), $("#runtimeModal").val());

    $('.' + Tipo + '').modal("close");
    $("#" + Tipo + "").html("");
}
