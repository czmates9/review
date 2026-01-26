var farba = '#ff9f9f';
var bila = 'rgb(255, 255, 255)';

var DefValue = {};


(function ($) {

    $(document).ready(function () {

        $('body').scrollspy({ target: "#myScrollspy", offset: 20 });

        $(".Comment").closest('tr').hide();

        $("input").each(function () {


            var TypInput = $(this).attr('type');

            //console.log(TypInput);

            if (TypInput == 'checkbox') {
                DefValue[$(this).attr('id')] = $(this).prop('checked');
            }
            else {
                DefValue[$(this).attr('id')] = $(this).val().toString();
            }

            //console.log(DefValue);

        });


        $("input").bind("change paste keyup", function () {

            var IDInput = $(this).attr('id');
            var TypInput = $(this).attr('type');
            //console.log($(this));
            //console.log(IDInput);
            //console.log(TypInput);

            if (TypInput == 'checkbox') {
                if ($(this).prop('checked') != DefValue[IDInput]) {
                    $(this).css("background", farba );
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


            /*************FUNGUJE*********/
            //var ID = '.' + $(this).attr('id');
            //$(ID).toggle(1500);


            /*************TESTY***********/
            //console.log(" 1 Start");
            //console.log($(this).text());

            //var ID = '.' + $(this).attr('id');
            //console.log(" 2 " + $(this).attr('id'));
            //console.log(" 3 " + ID);

            //var disp = $(ID).css('display');
            //console.log(" 4 " + disp);           

            //$(ID).slideToggle(1500);
            //$(ID).toggle(1500);

            //if (disp == "none") {
            //    //$(ID).css("display", "block");

            //    $(ID).css({
            //        "opacity": "0",
            //        "display": "inline-",
            //    }).show().animate({ opacity: 1 }, 1000)


            //    //console.log(" 5 OK Block");
            //}
            //else
            //{
            //    $(ID).css("display", "none");

            //    //$(ID).css({
            //    //    "display": "none",
            //    //}).hide().animate({ display: "block" },3000)


            //    //console.log(" 6 OK None");
            //}

        });

    });

})(jQuery);


//function FindChange(DefaultHodnota, Vystup) {
//    var TextOut;
//    var IDInput = $(this).attr('id');
//    var TypInput = $(this).attr('type');

//    if (TypInput == 'checkbox') {
//        if ($(this).prop('checked') != DefaultHodnota[IDInput]) {
//            $(this).val().css({
//                background: farba
//            });
//        }
//        else {
//            $(this).val().css({
//                background: bila
//            });
//        }

//        TextOut = IDInput + " : " + $(this).prop('checked')
//        Vystup[IDInput] = $(this).prop('checked');
//    }
//    else {
//        if ($(this).val() != DefaultHodnota[IDInput]) {
//            $(this).val().css({
//                background: farba
//            });
//        }
//        else {
//            $(this).val().css({
//                background: bila
//            });
//        }

//        TextOut = IDInput + " : " + $(this).val();
//        Vystup[IDInput] = $(this).val().toString();

//        if ($(this).val() == "") {
//            delete Vystup[IDInput];
//        }

//    }
//}
