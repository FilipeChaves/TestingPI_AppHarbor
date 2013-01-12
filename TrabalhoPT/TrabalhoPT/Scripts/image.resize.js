var max_size = 200;
console.log("estou no resize");
$("img").onload(function (i) {
    if ($(this).height() > $(this).width()) {
        var h = max_size;
        var w = Math.ceil($(this).width() / $(this).height() * max_size);
    } else {
        var w = max_size;
        var h = Math.ceil($(this).height() / $(this).width() * max_size);
    }
    $(this).css({ height: h, width: w });
});

/*
 * FONT:
 *   - http://adeelejaz.com/blog/resize-images-on-fly-using-jquery/
 *
 */