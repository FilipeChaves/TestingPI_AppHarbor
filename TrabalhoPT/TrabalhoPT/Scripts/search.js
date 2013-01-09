function showResult(str) {

    if (str.length < 3) {
        document.getElementById("search").innerHTML = "";
        document.getElementById("search").style.border = "0px";
        return;
    }
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            console.log(xmlhttp.responseText);
            document.getElementById("search").innerHTML = xmlhttp.responseText;
            document.getElementById("search").style.border = "1px solid #A5ACB2";
        }
    };
    xmlhttp.open('GET', '/Home/Search/' + str);
    xmlhttp.send();
}