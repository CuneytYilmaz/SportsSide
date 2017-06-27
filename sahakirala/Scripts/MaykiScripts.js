$(document).ready(function () {
    $.ajax({
        url: '/Home/DuyuruSlider',
        type: 'post',
        dataType: 'json',
        success: function (data) {
            var i;
            for (i = 0; i <= (data.length - 1) ; i++) {
                $("#h" + i + "").append(data[i].DuyuruBaslik),
                $("#h" + i + "").css("color", "white")
                $("#p" + i + "").append(data[i].DuyuruIcerik),
                $("#p" + i + "").css("color", "white")
            }
        },
        error: function (data) {
            alert("Başarısız");
        }
    })
})