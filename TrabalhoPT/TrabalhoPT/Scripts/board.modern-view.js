$(function () {
    //Começar a construir o layout correcto
    var numCards = 0;
    var dataDiv = document.getElementById("userData");
    var ul = dataDiv.getElementsByTagName("ul");
    var lists = ul[0].getElementsByTagName("li");
    var liElems;
    for (var i = 0; i < lists.length; ++i) {
        var div = document.createElement("div");
        div.setAttribute('class', "left");
        div.innerHTML = lists[i].innerHTML;
        dataDiv.appendChild(div);
        //Pedido ajax para obter os cartões
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open('GET', lists[i].getElementsByTagName("a")[0].href.replace("GetCards", "GetCardsByAjaxRequest"), false);
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                div.innerHTML += xmlhttp.responseText;
                
                liElems = div.getElementsByTagName('li');
                var hrefList = div.childNodes[1].getAttribute('href');
                var firstIdx = hrefList.indexOf('href');
                var splitted = hrefList.substring(firstIdx).split('/', 5);
                if (liElems.length === 0) {
                    div.innerHTML += '<a href="/Lists/CreateCard/' + splitted[splitted.length - 1] + '">Adicionar Cartao</a>';
                }
                console.log(div.getElementsByTagName('ul'));
                div.getElementsByTagName('ul')[0].setAttribute('id', splitted[splitted.length - 1]);
            }
        };
        xmlhttp.send();
    }
    
    $('.toRemove').remove();
    var button = document.createElement('input');
    button.setAttribute('type', 'submit');
    button.setAttribute('value', 'save');
    button.setAttribute('onclick', 'return saveTable()');
    dataDiv.appendChild(button);
    var css = document.createElement("style");
    css.type = "text/css";
    css.innerHTML = ".connectedSortable li{ \ " +
                    "height: 10px; width:100px; padding:10px; border:5px solid gray; \
                    margin:2px; border-color: steelblue; background-color: steelblue; \
                    color: transparent; border-radius: 10px; \
                    -moz-border-radius: 10px; -webkit-border-radius: 10px; text-emphasis-color: black; } \
                    .connectedSortable li a { color: black; text-decoration: none; } \
                     .connectedSortable { min-height: 30px; } ";
    document.body.appendChild(css);
})();

function saveTable() {
    var dataDiv = document.getElementById("userData");
    var ul = dataDiv.getElementsByTagName("ul");
    for (var i = 0; i < ul.length; ++i) {
        var idList = ul[i].getAttribute('id');
        var li = ul[i].getElementsByTagName('li');
        for (var c = 0; c < li.length; ++c)
        {
            var idCard = li[c].getAttribute('id');
            var xmlhttp = new XMLHttpRequest();
            console.log('/Lists/SetCard/' + idList + '/' + idCard + '/' + c);
            xmlhttp.open('POST', '/Lists/SetCard/' + idList + '/' + idCard + '/' + c);
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    console.log('changed');
                }
            };
            xmlhttp.send();
        }
    }
}