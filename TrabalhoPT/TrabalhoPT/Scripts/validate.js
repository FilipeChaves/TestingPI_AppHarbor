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
    console.log('submit');
    if (form == null) return true;
    var inputElems = form.getElementsByTagName('input');
    for (var i = 0; i < inputElems.length; ++i) {
        if (inputElems[i].getAttribute('data-val-required') != null) {
            if (inputElems[i].value === undefined || inputElems[i].value === "") {
                alert("Por favor preencha os campos obrigatorios");
                return false;
            }
        }
    }
    if (form.getElementsByClassName('field-validation-error').length > 0)
        return false;
    return true;
}

function validateRegisterForm() {
    var f = document.forms[1];
    if (f.getElementsByClassName('field-validation-error').length > 0 || areAllReqFieldsFilled(f))
        return false;
    return (userValidation(f["Username"]) && emailValidation(f["Email"]) && passValidation(f["Password"]) && passValidation(f["PasswordConfirmation"]));
}

function sendRequest(input, linkToReq, msg) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            if (xmlhttp.responseText === 'True') {
                setSpanError(input, msg);
                return;
            }
            setSpanValid(input);
        }
    };
    xmlhttp.open('GET', linkToReq);
    return xmlhttp.send();
}

function hasMoreThanXChars(number, input, str, msg, b) {
    if (str.length <= number) {
        setSpanError(input, msg);
        return false;
    }
    if(b)
        setSpanValid(input);
    return true;
}

function userValidation(user) {
    var userString = user.value;
    if (hasMoreThanXChars(2, user, userString, 'O Username precisa de ter mais do que 2 caracteres', false) === false)
        return false;
    return sendRequest(user, '/Account/UsernameExists/' + userString, 'Esse nome de utilizador já existe');
}

function boardValidation(board) {
    var boardString = board.value;
    if (hasMoreThanXChars(2, board, boardString, 'O nome do quadro precisa de ter mais do que 2 caracteres', false) === false)
        return false;
    return sendRequest(board, '/Boards/BoardExists/' + boardString, 'Esse nome já existe num quadro seu');
}

function listValidation(list) {
    console.log('lista');
    var listString = list.value;
    if (hasMoreThanXChars(2, list, listString, 'O nome da lista precisa de ter mais do que 2 caracteres', false) === false)
        return false;
    var currentUrl = document.URL;
    var linkToReq = '/Boards/ExistingLists' + currentUrl.substring(currentUrl.lastIndexOf('/'), currentUrl.length);
    console.log(linkToReq);
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var enumLists = JSON.parse(xmlhttp.responseText);
            console.log(enumLists);
            for (var i = 0; i < enumLists.length; ++i) {
                if (enumLists[i].Name == listString) {
                    setSpanError(list, 'Esse nome já existe numa lista deste quadro');
                    return;
                }
            }
            setSpanValid(list);
        }
    };
    xmlhttp.open('GET', linkToReq);
    var b = xmlhttp.send();
    setTimeout(function() { }, 100);
    return b;
    //return sendRequest(list, url, 'Esse nome já existe numa lista deste quadro');
}

function emailValidation(email) {
    var emailString = email.value;
    var str = 'Introduza um email valido';
    if (hasMoreThanXChars(6, email, emailString, str, false) === false)
        return false;
    if (emailString.indexOf("@", 0) == -1 || emailString.indexOf(".", 0) == -1) {
        setSpanError(email, str);
        return false;
    }
    setSpanValid(email);
    return true;
}

function passValidation(password) {
    var passString = password.value;
    if (hasMoreThanXChars(6, password, passString, 'A password tem de ter mais de 6 caracteres', true) === false)
        return false;
    return true;
}
