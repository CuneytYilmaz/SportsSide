$(document).ready(function () {
    $("#ddlAreas").change(function () {
        var index = $(this).val();
        $("#ddlCities").empty();
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "http://localhost:4285/SSApi/GetCitiesByAreas",
            data: {areaId: index},
            success: function (data) {
                $(data).each(function (key, item) {
                    $("#ddlCities").append(
                        $("<option></option>").val(item.CITY_ID).html(item.CITY_NAME)
                    );
                });
            },
            error: function (req, status, err) {
                console.log("HATA : ", req, status, err);
            }
        });
        //var length = $("#ddlCities").children('option').length;
        //if (length == 0) {
        //    $("#ddlCities").append(("<option>Bölge seçiniz</option>"));
        //}
    });
});