$(function () {
    $(".connectedSortable").sortable({
        items: "li:not(a)"
    });
    $(".connectedSortable").sortable({
        connectWith: ".connectedSortable"
    });
    
    $(".connectedSortable").disableSelection();
});