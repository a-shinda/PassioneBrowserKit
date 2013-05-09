
$(document).ready(function() {

    // Append ScrollBar
    $("#main").niceScroll({ cursorcolor: "rgba(1, 0, 0, 0.6)" });

    $('#divLeft ul li').click(function(e) {

        var index = $("#divLeft ul li").index(this);
        var deg = 90 * index;

        $("#divRight").css({
            "-moz-transform": "rotateX(" + deg + "deg)",
            "-webkit-transform": "rotateX(" + deg + "deg)"
        });

    });

});
