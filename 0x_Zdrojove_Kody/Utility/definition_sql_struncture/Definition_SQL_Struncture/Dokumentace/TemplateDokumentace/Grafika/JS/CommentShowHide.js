(function ($) {

    $(document).ready(function () {

        $(".Comment").closest('tr').hide();

        $(".ClickComment").click(function () {
            $(this).closest('tr').next().toggle(1000);
        });

    });

})(jQuery);

