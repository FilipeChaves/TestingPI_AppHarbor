/*$(document).ready(function () {
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
    
});*/

function setSpanElement(element, msg, oldClass, newClass) {
    var name = element.getAttribute('name');
    var validationValidFields = document.forms[1].getElementsByClassName(oldClass);

    for (var i = 0; i < validationValidFields.length; ++i) {
        if (validationValidFields[i].getAttribute('data-valmsg-for') === name) {
            validationValidFields[i].innerHTML = msg;
            validationValidFields[i].setAttribute('class', newClass);
            return;
        }
    }
}

function setSpanError(element, msg) {
    setSpanElement(element, msg, 'field-validation-valid', 'field-validation-error');
}

function setSpanValid(element) {
    setSpanElement(element, '', 'field-validation-error', 'field-validation-valid');
}

function areAllReqFieldsFilled(form) {
    if (form != null) return true;
    var inputElems = form.getElementsByClassName('input');
    for (var i = 0; i < inputElems.length; ++i) {
        if (inputElems[i].getAttribute('data-valmsg-required') != null) {
            if (inputElems[i].value === undefined || inputElems[i].value === "")
                return false;
        }
    }
    return true;
}

/*$(":button").click(function () {
    areAllReqFieldsFilled(document.forms[1]);
});*/

function validateRegisterForm() {
    var f = document.forms[1];
    if (f.getElementsByClassName('field-validation-error').length > 0 || areAllReqFieldsFilled(f))
        return false;
    if (f["Username"] === undefined || f["Email"] === undefined || f["Password"] === undefined)
        return false;
    return (userValidation(f["Username"]) && passValidation(f["Password"]) && emailValidation(f["Email"]));
}

function userValidation(user) {
    var userString = user.value;
    if (userString.length < 3) {
        setSpanError(user, 'O Username precisa de ter mais do que 2 caracteres');
        return false;
    }
    
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            if (xmlhttp.responseText === 'True') {
                setSpanError(user, 'Esse nome de utilizador já existe');
                return;
            }
            setSpanValid(user);
        }
    };
    xmlhttp.open('GET', '/Account/UsernameExists/' + userString);
    xmlhttp.send();
}

function emailValidation(email) {
    var emailString = email.value;
    if (emailString.indexOf("@", 0) == -1 || emailString.indexOf(".", 0) == -1) {
        setSpanError(email, 'Introduza um email válido');
        return false;
    }
    setSpanValid(email);
}

function passValidation(password) {
    var passString = password.value;
    if (passString.length < 6) {
        setSpanError(password, 'A password tem de ter mais de 6 caracteres');
        return false;
    }
    setSpanValid(password);
}
