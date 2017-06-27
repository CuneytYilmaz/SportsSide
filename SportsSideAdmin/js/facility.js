$(document).ready(function () {
    $("table tr td button").click(function () {
        var id = $(this).attr("id");
        var value = $(this).attr("value"); 
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "http://localhost:4285/SSApi/GetPicturesFromFacility",
            data: { facilityId: id },
            success: function (data) {
                $("#frame").fadeIn();
                $("#overlay").fadeIn();
                $("#displayPager").hide();
                $("#sliderList li").remove();
                $("#sliderContainer div").remove();
                $(data).each(function (key, item) {
                    $('<div class="item"><img id="main" src="' + item.FP_PICTURE + '"><div class="carousel-caption"><p>' + value + '</p></div>   </div>').appendTo('.carousel-inner');
                    $('<li data-target="#carousel-example-generic" data-slide-to="' + key + '"></li>').appendTo('.carousel-indicators')

                    //if (key == 0) {
                    //    $("#slider").addClass("active");
                    //}
                    //$("#slider").append("<div class='item'><img src="+ item.FP_PICTURE +" alt='...'><div class='carousel-caption'></div></div>");
                    //$("#ddlCities").append(
                    //    $("<option></option>").val(item.CITY_ID).html(item.CITY_NAME)
                    //);
                });
                $('.item').first().addClass('active');
                $('.carousel-indicators > li').first().addClass('active');
                $('.carousel-control').show();
                if ($('.item').length == 1) {
                    $('.carousel-control').hide();
                }
                $('#carousel-example-generic').carousel();
            },
            error: function (req, status, err) {
            }
        });
    });

    $("#overlay").click(function () {
        $(this).fadeOut();
        $("#frame").fadeOut();
        $("#displayPager").show();
    });

    $("#ddlAreas").change(function () {
        var index = $(this).val();
        $("#ddlCities").empty();
        $("#ddlDistricts").empty();
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "http://localhost:4285/SSApi/GetCitiesByAreas",
            data: { areaId: index },
            success: function (data) {
                $(data).each(function (key, item) {
                    $("#ddlCities").append(
                        $("<option></option>").val(item.CITY_ID).html(item.CITY_NAME)
                    );
                    if (key == 0) {
                        getDistricts(item.CITY_ID);
                    }
                });
            },
            error: function (req, status, err) {
            }
        });
    });

    $("#ddlCities").change(function () {
        var index = $(this).val();
        getDistricts(index);
    });

    function getDistricts(index) {
        $("#ddlDistricts").empty();
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "http://localhost:4285/SSApi/GetDistrictsByCities",
            data: { cityId: index },
            success: function (data) {
                $(data).each(function (key, item) {
                    $("#ddlDistricts").append(
                        $("<option></option>").val(item.DISTRICT_ID).html(item.DISTRICT_NAME)
                    );
                });
            },
            error: function (req, status, err) {
            }
        });
    }
});