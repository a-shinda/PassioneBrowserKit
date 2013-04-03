////////////////////////////////////////////////////////////////////////////
// jquery.animateUtil.js V1.0.0
// (c) 2012 by A.Shinada
// MIT License
//
// Please retain this copyright header in all versions of the software
////////////////////////////////////////////////////////////////////////////
(function(jQuery) {

    // jQuery.fn.animateUtil
    jQuery.fn.animateUtil = function(options) {

        // config
        var config = jQuery.extend({
            action: "FlipRight"
            , angle: 360
            , speed: 0.5
            , tranTimFunc: "ease"
            , funcStart: function() { }
            , funcEnd: function() { }
        }, options);
        // config

        if (config.action == "FlipLeft") {

            // do funcStart
            (config.funcStart)();

            // Change Css
            Flip($(this), "rotateY", "-");

        } else if (config.action == "FlipRight") {

            // do funcStart
            (config.funcStart)();

            // Change Css
            Flip($(this), "rotateY", "");

        } else if (config.action == "FlipTop") {

            // do funcStart
            (config.funcStart)();

            // Change Css
            Flip($(this), "rotateX", "");

        } else if (config.action == "FlipBottom") {

            // do funcStart
            (config.funcStart)();

            // Change Css
            Flip($(this), "rotateX", "-");

        } else if (config.action == "Shutter") {

            // do funcStart
            (config.funcStart)();

            // Change Css
            $(this).stop().css({
                "-moz-transition": "-moz-transform " + config.speed + "s " + config.tranTimFunc,
                "-moz-transform": "scaleY(0.0)"
            }).one("transitionend webkitTransitionEnd oTransitionEnd", function() {

                // Bind Event as Onece
                // Clear Css
                $(this).stop().css({
                    "-moz-transition": "-moz-transform " + config.speed + "s " + config.tranTimFunc,
                    "-moz-transform": "scaleY(1.0)"
                })
            });

        }

        // Flip
        function Flip(ui, property, minus) {

            $(ui).stop().css({
                "-moz-transition-property": "transform",
                "-moz-transition": "-moz-transform " + config.speed + "s " + config.tranTimFunc,
                "-moz-transform": property + "(" + minus + config.angle + "deg)",
                "-webkit-transition-property": "transform",
                "-webkit-transition": "-webkit-transform " + config.speed + "s " + config.tranTimFunc,
                "-webkit-transform": property + "(" + minus + config.angle + "deg)",
                "-o-transition-property": "transform",
                "-o-transition": "-o-transform " + config.speed + "s " + config.tranTimFunc,
                "-o-transform": property + "(" + minus + config.angle + "deg)"
            }).one("transitionend webkitTransitionEnd oTransitionEnd", function() {

                // Bind Event as Onece
                // Clear Css
                $(ui).stop().css({
                    "-moz-transition": "",
                    "-moz-transform": property + "(" + "0" + "deg)",
                    "-webkit-transition": "",
                    "-webkit-transform": property + "(" + "0" + "deg)",
                    "-o-transition": "",
                    "-o-transform": property + "(" + "0" + "deg)"
                });

                (config.funcEnd)();

            });

        }
        // Flip

    }

    // jQuery.fn.animateUtil

})(jQuery);