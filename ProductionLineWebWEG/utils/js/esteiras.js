function showModal() {
    $("#modal1").append('\
      <div class="modal-content">\
        <h5>Cadastrar nova esteira</h5>\
        <div class="row"></div>\
        <div class="row">\
        <div class="input-field col s12 m10 offset-m1">\
          <input id="name" type="text" class="validate">\
          <label for="name">Nome</label>\
        </div>\
        </div>\
        <div class="row">\
        <div class="input-field col s12 m10 offset-m1">\
          <input id="item1" type="text" class="validate">\
          <label for="item1">Item</label>\
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

    $('body').keyup(function(e) {
      if(e.keyCode  == 27) dimissModal("modal1");
    });

    $(document).mouseup(function(e) {
      let container = $("#modal1");
      if(!container.is(e.target) && container.has(e.target).length === 0) dimissModal("modal1");
    })
  }

function dimissModal(Tipo) {
    $('.'+Tipo+'').modal("close");
    $("#"+Tipo+"").html("");
}