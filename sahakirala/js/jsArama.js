$(document).ready(function () {
            $.ajax({
                url: 'http://localhost:4285/SSApi/GetFacilityType',
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    $(data).each(function (key, item) {
                        $('#FacilityType').append('<option>' + item.FT_NAME + '</option>');
                    });
                }
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
                        console.log("HATA : ", req, status, err);
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
                        console.log("HATA : ", req, status, err);
                    }
                });
            }
            
            $('#SearchString').empty();

            $('#SearchString').tooltip({ 'trigger': 'hover', 'title': 'Saha ismini giriniz', "placement": "bottom" });
            $('#ddlAreas').tooltip({ 'trigger': 'hover', 'title': 'Bölge seçiniz', "placement": "bottom" });
            $('#FacilityType').tooltip({ 'trigger': 'hover', 'title': 'Tesis türünü seçiniz', "placement": "bottom" });
            $('#ddlCities').tooltip({ 'trigger': 'hover', 'title': 'Şehri seçiniz', "placement": "bottom" });
            $('#Price1').tooltip({ 'trigger': 'hover', 'title': 'Düşük fiyatı giriniz', "placement": "bottom" });
            $('#Price2').tooltip({ 'trigger': 'hover', 'title': 'Büyük fiyatı giriniz', "placement": "bottom" });
            $('#ddlDistricts').tooltip({ 'trigger': 'hover', 'title': 'İlçe seçiniz', "placement": "bottom" });

            $('#SearchString').val('');
            $('select option:first-child').attr("selected", "selected");
            $("#Price1").inputmask('Regex', { regex: "[0-9]*" });
            $("#Price2").inputmask('Regex', { regex: "[0-9]*" });

        });