$(function () {
    //Começar a construir o layout correcto
    var dataDiv = document.getElementById("userData");
    var ul = dataDiv.getElementsByTagName("ul");
    var lists = ul[0].getElementsByTagName("li");
    var divHead = document.createElement("div");
    for (var i = 0; i < lists.length; ++i) {
        var div = document.createElement("div");
        divHead.appendChild(div);
        div.setAttribute('class', "left");
        div.innerHTML = lists[i].innerHTML;
        dataDiv.appendChild(div);
        //Pedido ajax para obter os cartões
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open('GET', lists[i].getElementsByTagName("a")[0].href.replace("GetCards", "GetCardsByAjaxRequest"), false);
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                div.innerHTML += xmlhttp.responseText;
                console.log(div);
            }
        };
        xmlhttp.send();
    }
    $('.toRemove').remove();

    var css = document.createElement("style");
    css.type = "text/css";
    css.innerHTML = "#lists li{ \ " +
                    "height: 10px; width:100px; padding:10px; border:5px solid gray; \
                    margin:2px; border-color: steelblue; background-color: steelblue; \
                    color: transparent; border-radius: 10px; \
                    -moz-border-radius: 10px; -webkit-border-radius: 10px; text-emphasis-color: black; } \
                    #lists li a { color: black; text-decoration: none; } \
                     .connectedSortable { min-height: 30px; } "
    ;
    document.body.appendChild(css);
    
})();