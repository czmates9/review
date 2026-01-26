$(document).ready(function () {

    $('body').scrollspy({ target: "#myScrollspy", offset: 150 });

    /*
    * SCROLL
    */

    var menu = $('.nav'),
        menuLinks = menu.find('a');

    menuLinks.on('click', function (event) {

        if (this.hash !== "") {

            event.preventDefault();
            var hash = this.hash;

            var ofsetik = $(hash).offset().top - $("#NavHead").height();
            //var ofsetik = $(hash).offset().top;

            //console.log("Hash:            " + hash);
            //console.log("HashOffsetTop:   " + $(hash).offset().top);
            //console.log("Hesight:         " + $("#NavHead").height());
            //console.log("Ofsetik:         " + ofsetik);

            $('html, body').animate({
                scrollTop: ofsetik
            }, 800, function () {
                    //window.location.hash = hash;
            });
        }

    });

    /*
    * BACT TO TOP
    */

    var backToTop = $('<a>', {
        href: '#section1',
        class: 'back-to-top',
        html: '<button type="button" class="btn btn-danger btn-lg" title="Vyjed nahoru...">Nahoru</button> '
    });

    backToTop
        .hide()
        .appendTo('body')
        .on('click', function () {
            $('html, body').animate({ scrollTop: 0 }, 800);
        });

    var win = $(window);
    win.on('scroll', function () {
        if (win.scrollTop() >= 500) {
            backToTop.fadeIn();
        }
        else {
            backToTop.hide();
        }
    });



});