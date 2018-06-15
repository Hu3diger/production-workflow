$(document).ready(function () {
  $('.sidenav').sidenav();

  setInterval(inserirT, 2000);
});

var json = [];

function inserirT() {
  $("#xuxu").html('');
  let x = Math.floor(Math.random() * 100);
  let y = x < 10
  let teste = {
    horario: getHorario(),
    mensagem: 'Teste dhasdhasuiduhasuhdasuhdashas',
    critico: y
  };
  json.splice(0, 0, teste);

  if (json.length > 10) {
    if (!json[5].critico) {
      json.splice(5, 1);
    }
  }

  $(json).each(function () {
    let btn = '';
    if(this.critico){
      btn = '<a href="#">Details</a>';
    }
    $("#xuxu").append("<tr><td>" + this.horario + "</td><td>" + this.mensagem + "</td><td>" + btn + "</td></tr>");
  });

}
function getHorario() {
  let today = new Date();
  let dd = today.getDate();
  let mm = today.getMonth() + 1; //January is 0!
  let yyyy = today.getFullYear();
  let hh = today.getHours();
  let mn = today.getMinutes();
  let ss = today.getSeconds();
  if (dd < 10) {
    dd = '0' + dd
  }
  if (mm < 10) {
    mm = '0' + mm
  }
  if (hh < 10) {
    hh = '0' + hh
  }
  if (mn < 10) {
    mn = '0' + mn
  }
  if (ss < 10) {
    ss = '0' + ss
  }
  today = mm + '/' + dd + '/' + yyyy + ' - ' + hh + ':' + mn + ':' + ss;
  return today;
}