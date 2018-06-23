function showModal() {
    $("#modal1").append('\
    <div class="modal-content">\
        <h5>Cadastrar nova esteira</h5>\
        <div class="row"></div>\
        <div class="row">\
          <div class="input-field col s12 m10 offset-m1">\
            <input id="name_Process" type="text" class="validate">\
            <label for="name_Process">Nome</label>\
          </div>\
        </div>\
        <div class="row">\
          <div class="input-field col s12 m10 offset-m1">\
            <textarea id="description_tx" class="materialize-textarea"></textarea>\
            <label for="description_tx">Descrição do processo</label>\
          </div>\
        </div>\
        <div class="row">\
          <div class="input-field col s12 m10 offset-m1">\
            <input id="runTime_Process" type="number" class="validate">\
            <label for="runTime_Process">Tempo de execução (ms)</label>\
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
        <a href="#!" class="modal-close waves-effect waves-green btn-flat" onclick="dimissModal(\'modal1\');">Cancelar</a>\
        <a href="#!" class="waves-effect waves-green btn-flat" onclick="dimissModal(\'modal1\');">Salvar</a>\
      </div>\
    ');
    $('select').formSelect();

    $('body').keyup(function (e) {
        if (e.keyCode == 27) dimissModal("modal1");
    });

    $(document).mouseup(function (e) {
        let container = $("#modal1");
        if (!container.is(e.target) && container.has(e.target).length === 0) dimissModal("modal1");
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
          <input disabled value="'+ name +'" id="name" type="text" class="validate">\
          <label class="active" for="name">Nome</label>\
        </div>\
        <div class="input-field col s12 m8 offset-m2">\
          <input disabled value="Teste 123" id="desc" type="text" class="validate">\
          <label class="active" for="desc">Descrição do processo</label>\
        </div>\
      </div>\
      <div class="row">\
        <div class="input-field col s12 m8 offset-m2">\
          <input disabled value="333" id="temp" type="text" class="validate active">\
          <label class="active" for="temp">Tempo de execução (ms)</label>\
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
          <a class="waves-effect waves-light btn right blue darken-4" disabled id="button"><i class="material-icons left">sd_card</i>salvar</a>\
        </div>\
      </div>\
    </form>\
  </div>\
  ');
  }
}

function dimissModal(Tipo) {
    $('.' + Tipo + '').modal("close");
    $("#" + Tipo + "").html("");
}
