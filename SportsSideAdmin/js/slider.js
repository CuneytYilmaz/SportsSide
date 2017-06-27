$(document).ready(function () {

    function setImg(src) {
        $("#main").attr("src", src);
    }

    $("table tr td button").click(function () {
        var src = $(this).attr("id");
        var value = $(this).attr("value");
        setImg(src);
        $("#frame").fadeIn();
        $("#overlay").fadeIn();
    });

    $('body').on('click', 'img', function () {
        var src = $(this).attr("src");
        setImg(src);
        $("#frame").fadeIn();
        $("#overlay").fadeIn();
    })

    $("#overlay").click(function () {
        $(this).fadeOut();
        $("#frame").fadeOut();
    });

    $("#btnSubmit").click(function () {
        var isValidate = true;
        if ($("#sliderName").val() == null || $("#sliderName").val() == "") {
            $("#validateSliderName").text("Lütfen slider aciklamasi giriniz.")
            isValidate = false;
        }
        else {
            $("#validateSliderName").text("")
        }
        if ($("#btnFile").val() == null || $("#btnFile").val() == "") {
            $("#validateSliderPicture").text("Lütfen slider fotografi giriniz.")
            isValidate = false;
        }
        else {
            $("#validateSliderPicture").text("")
        }

        if (isValidate == false) {
            return false;
        }
    });
});