function showResult(str) {

    if (str.length < 3) {
        document.getElementById("searchResults").innerHTML = "";
        document.getElementById("searchResults").style.border = "0px";
        return;
    }
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open('GET', '/Home/Search/' + str);
    xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            console.log(xmlhttp.responseText);
            //document.getElementById("searchResults").style.border = "1px solid #A5ACB2";
            var results = xmlhttp.responseText.split("\n");
            for (var i = 0; i < results.length; ++i) {
                var result = results[i].toString().trim();
                if (result.length > 0) {
                    document.getElementById("searchResults").innerHTML = xmlhttp.responseText;
                }
            }
        }
    };
    xmlhttp.send();
}

var count = 0;

function removeValue(elem) {
    if(count++===0)
        elem.value = "";
}