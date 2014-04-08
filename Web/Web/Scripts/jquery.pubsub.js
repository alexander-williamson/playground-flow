(function ($) {

    var o = $({});

    $.subscribe = function () {
        console.log("subscribe: " + arguments[0]);
        o.on.apply(o, arguments);
    };

    $.unsubscribe = function () {
        o.off.apply(o, arguments);
    };

    $.publish = function () {
        console.log("publish: " + arguments[0]);
        o.trigger.apply(o, arguments);
    };

}(jQuery));