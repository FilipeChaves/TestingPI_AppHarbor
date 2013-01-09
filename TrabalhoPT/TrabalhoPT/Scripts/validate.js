$(document).ready(function () {
    console.log("1");
    
    //objecto principal
    var validator = new Object;
    
    //Função que executa o pedido ajax
    validator.ajaxExecuter = function (url, process) {
        console.log("2");
        var xhr = new XMLHttpRequest();
        xhr.open('GET', url);
        xhr.onreadystatechange = (function () {
            console.log("3");
            console.log('ready state changed to %s', xhr.readyState);
            if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
                process();
            }
        });
        xhr.send();
    };
    
    //Função que valida o nome
    validator.nameValidator = function(name) {
        if (name.length == 0)
            return false
    };
    
    $('#loginForm').getE
    
});