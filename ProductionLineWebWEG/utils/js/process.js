function showModal() {
    $("#modal1").append('\
    <div class="modal-content">\
    <h5>Cadastrar nova esteira</h5>\
    <div class="row">\
    <div class="col s12">\
      <div class="input-field t">\
        <input id="name_Process" type="text" class="validate">\
        <label for="name_Process">Nome</label>\
      </div>\
    </div>\
    </div>\
    <div class="row">\
    <div class="col s12">\
      <div class="input-field t">\
        <textarea id="description_tx" class="materialize-textarea"></textarea>\
        <label for="description_tx">Descrição do processo</label>\
      </div>\
    </div>\
    </div>\
    <div class="row">\
    <div class="col s12">\
      <div class="input-field t">\
        <input id="runTime_Process" type="number" class="validate">\
        <label for="runTime_Process">Tempo de execução (ms)</label>\
      </div>\
    </div>\
    </div>\
    <div class="modal-footer">\
        <a href="#!" class="modal-close waves-effect waves-green btn-flat" onclick="dimissModal(\'modal1\');">Cancelar</a>\
        <a href="#!" class="waves-effect waves-green btn-flat" onclick="dimissModal(\'modal1\');">Salvar</a>\
    </div>\
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

function dimissModal(Tipo) {
    $('.' + Tipo + '').modal("close");
    $("#" + Tipo + "").html("");
}