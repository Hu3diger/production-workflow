//inserção dos itens na dashboard, vindo do servidor
// m = message  (string)
// c = critical (bool)
// h = horario  (time)
function inserirT(jsonDash) {
    $("#xuxu").html("");

    if (jsonDash.length > 5) {
        if (!jsonDash[5].critico) {
            jsonDash.splice(5, 1);
        }
    }

    $(jsonDash).each(function () {
        let btn = "";
        if (this.critico) {
            btn = '<a href="#">Details</a>';
        }
        $("#xuxu").append(
            "<tr><td>" +
            this.horario +
            "</td><td>" +
            this.mensagem +
            "</td><td>" +
            btn +
            "</td></tr>"
        );
    });
}

//pega o horário do sistema para utilizar na inserção na dashboard
function getHorario() {
    let today = new Date();
    let dd = today.getDate();
    let mm = today.getMonth() + 1;
    let yyyy = today.getFullYear();
    let hh = today.getHours();
    let mn = today.getMinutes();
    let ss = today.getSeconds();
    if (dd < 10) {
        dd = "0" + dd;
    }
    if (mm < 10) {
        mm = "0" + mm;
    }
    if (hh < 10) {
        hh = "0" + hh;
    }
    if (mn < 10) {
        mn = "0" + mn;
    }
    if (ss < 10) {
        ss = "0" + ss;
    }
    today = mm + "/" + dd + "/" + yyyy + " - " + hh + ":" + mn + ":" + ss;
    return today;
}