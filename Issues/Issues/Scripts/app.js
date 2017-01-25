
$(function () {
    $('body').niceScroll({
        cursorcolor: "#666",
        cursorborder: "0px solid #fff",

        cursorwidth: "8px",
        cursorborderradius: "10px",
        horizrailenabled: false,
        zindex: 1050
    });

    //$('select').addClass('chosen-rtl').chosen({ width: '100%', disable_search_threshold: 10, no_results_text: "No items in the select" });
    $('select').chosen({ width: '100%', disable_search_threshold: 10, no_results_text: "No items in the select" });

});