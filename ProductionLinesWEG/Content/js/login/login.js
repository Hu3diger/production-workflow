//faz a requisição para o servidor, passando a senha e user informado nos inputs.
function login(){
    requestLogin($("#user").val(), $("#password").val());
}