var farba = '#ff9f9f';
var bila = 'rgb(255, 255, 255)';

var DefValue = {};


(function ($) {

    $(document).ready(function () {

        $('body').scrollspy({ target: "#myScrollspy", offset: 20 });

        $(".Comment").closest('tr').hide();

        $("input").each(function () {

            var TypInput = $(this).attr('type');

            if (TypInput == 'checkbox') {
                DefValue[$(this).attr('id')] = $(this).prop('checked');
            }
            else {
                DefValue[$(this).attr('id')] = $(this).val().toString();
            }
        });


        $("input").bind("change paste keyup", function () {

            var IDInput = $(this).attr('id');
            var TypInput = $(this).attr('type');

            if (TypInput == 'checkbox') {
                if ($(this).prop('checked') != DefValue[IDInput]) {
                    $(this).css("background", farba);
                }
                else {
                    $(this).css("background", bila);
                }
            }
            else {
                if ($(this).val() != DefValue[IDInput]) {
                    $(this).css("background", farba);
                }
                else {
                    $(this).css("background", bila);
                }
            }
        });


        $(".ClickComment").click(function () {


            $(this).closest('tr').next().toggle(1000);

        });

    });

})(jQuery);

