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
        $("#displayPager").hide();
    });

    $('body').on('click', 'img', function () {
        var src = $(this).attr("src");
        setImg(src);
        $("#frame").fadeIn();
        $("#overlay").fadeIn();
        $("#displayPager").fadeOut();
    })

    $("#overlay").click(function () {
        $(this).fadeOut();
        $("#frame").fadeOut();
        $("#displayPager").fadeIn();
    });

    $("#btnSubmit").click(function () {
        if ($("#ftName").val() == null || $("#ftName").val() == "") {
            $("#validateFtName").text("Lütfen tesis turu adi giriniz.")
            return false;
        }
        else {
            $("#validateFtName").text("")
        }
    });

    $("#isDelete").change(function () {
        if ($(this).is(":checked")) {
            $("#imgUpload").hide();
        }
        else {
            $("#imgUpload").show();
        }
    });
});