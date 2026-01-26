$(document).ready(function() {


$("#ImageCode").on(("mouseenter"), function() {
        $(this).animate({ opacity: 1 }, 800);
        //$(this).css('opacity', '1');
    });

    $("#ImageCode").on(("mouseleave"), function() {
        $(this).animate({opacity : 0}, 800);
        //$(this).css('opacity', '0');
    });

    $("#LabelV").on(("mouseenter"), function () {
        $(this).animate({ opacity: 1 }, 800);
        //$(this).css('opacity', '1');
    });

    $("#LabelV").on(("mouseleave"), function () {
        $(this).animate({ opacity: 0 }, 800);
        //$(this).css('opacity', '0');
    });

    $("#Label_Databaze").on(("mouseenter"), function () {
        $(this).animate({ opacity: 1 }, 800);
        //$(this).css('opacity', '1');
    });

    $("#Label_Databaze").on(("mouseleave"), function () {
        $(this).animate({ opacity: 0 }, 800);
        //$(this).css('opacity', '0');
    });

    $("#Label_Provider").on(("mouseenter"), function () {
        $(this).animate({ opacity: 1 }, 800);
        //$(this).css('opacity', '1');
    });

    $("#Label_Provider").on(("mouseleave"), function () {
        $(this).animate({ opacity: 0 }, 800);
        //$(this).css('opacity', '0');
    });

});







