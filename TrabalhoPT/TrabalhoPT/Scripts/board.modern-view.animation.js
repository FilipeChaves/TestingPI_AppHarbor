$(function () {
	$("div .connectedSortable").sortable({
		connectWith: ".connectedSortable"
	}).disableSelection();
});