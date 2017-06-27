$(document).ready(function () {

    if ($("#isLoggedin").val() != "True") {
        window.location.href = $("#urlLogin").val();
    }

    $("#main-menu li").click(function () {
        $("#main-menu li").removeClass("active-menu");
        $(this).addClass("active-menu");
    });

    $('ul.nav li.dropdown').hover(function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(200);
        $(this).css("background-color", "#5bd999");
    }, function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(200);
        $(this).css("background-color", "");
    });

    $("#sideNav").click(function () {
        if ($(this).hasClass('closed')) {
            $('.navbar-side').animate({ left: '0px' });
            $(this).removeClass('closed');
            $('#page-wrapper').animate({ 'margin-left': '260px' });

        }
        else {
            $(this).addClass('closed');
            $('.navbar-side').animate({ left: '-260px' });
            $('#page-wrapper').animate({ 'margin-left': '0px' });
        }
    });
});