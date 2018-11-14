//faz a requisição para o servidor, passando a senha e user informado nos inputs.
function login(){
    $("#loading").show();
    requestLogin($("#user").val(), $("#password").val());
}

function initLogin(){
    $("#btnLogin").removeAttr("disabled");
    $("#loading").hide();
}