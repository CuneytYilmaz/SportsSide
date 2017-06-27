$(document).ready(function () {
    $('#ddlAreas').change(function () {
        var index = $(this).val();
        $('#ddlCities').empty();
        $('#ddlDistricts').empty();
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
    $('#ddlCities').change(function () {
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

    $('#memberTabs a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });

    $(".btnDeleteSubscribe").click(function () {
        var id = $(this).attr("id");
        bootbox.confirm({
            title: "Uyarı",
            message: "Abonelikten çıkmak istediğinize emin misiniz?",
            buttons: {
                confirm: {
                    size: "small",
                    label: "Evet",
                    className: "btn-danger"
                },
                cancel: {
                    label: "Hayır",
                    className: "btn-default"
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        type: "GET",
                        url: "/Member/RejectSubscriber",
                        data: { id: id },
                        success: function (data) {
                            if (data) {
                                $("#mdResult").html("<div class='alert alert-success'>İşlem başarılı.</div>")
                                $('#myModal').modal('show')
                            }
                            else {
                                $("#mdResult").html("<div class='alert alert-danger'><strong>Hay aksi!</strong> Bir hata oluştu.</div>")
                                $('#myModal').modal('show')
                            }
                        },
                        error: function (req, status, err) {

                        }
                    });
                }
            }
        })
    });

    $(".btnDeleteReservation").click(function () {
        var id = $(this).attr("id");
        bootbox.confirm({
            title: "Uyarı",
            message: "Rezervasyonu iptal etmek istediğinize emin misiniz?",
            buttons: {
                confirm: {
                    size: "small",
                    label: "Evet",
                    className: "btn-danger"
                },
                cancel: {
                    label: "Hayır",
                    className: "btn-default"
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        type: "GET",
                        url: "/Member/RejectReservation",
                        data: { id: id },
                        success: function (data) {
                            if (data) {
                                $("#mdResult").html("<div class='alert alert-success'>İşlem başarılı.</div>")
                                $('#myModal').modal('show')
                            }
                            else {
                                $("#mdResult").html("<div class='alert alert-danger'><strong>Hay aksi!</strong> Bir hata oluştu.</div>")
                                $('#myModal').modal('show')
                            }
                        },
                        error: function (req, status, err) {

                        }
                    });
                }
            }
        })
    });

    $(".resultOK").click(function () {
        var url = $(this).attr("id");
        window.location.href = url;
    });

    $(".araBtn").click(function () {
        if ($("#UserPassword1").val() != $("#UserPassword2").val()) {
            $('#errorModal').modal('show');
            return false;
        }
        else {
                swal({
                    title: "İşlem Tamam",
                    text: "Değişiklikler başarıyla kaydedildi!",
                    type: "success",
                    showCancelButton: false,
                    showConfirmButton: false,
                    closeOnConfirm: false,
                    confirmButtonText: "Tamam",
                    confirmButtonColor: "#449d44",
                    timer: 2000
                }, function () {
                    var url = $('.araBtn').attr("id");
                    window.location.href = url;
                });
        }
    });



    $("#errorOK").click(function () {
        $('#errorModal').modal('hide');
    });

    $('#UserPassword1').tooltip({ 'trigger': 'hover', 'title': 'Şifrenizi giriniz', "placement": "bottom" });
    $('#UserPassword2').tooltip({ 'trigger': 'hover', 'title': 'Şifrenizi tekrar giriniz', "placement": "bottom" });
    $('#UserName').tooltip({ 'trigger': 'hover', 'title': 'Adınızı giriniz', "placement": "bottom" });
    $('#UserSurname').tooltip({ 'trigger': 'hover', 'title': 'Soyadınızı giriniz', "placement": "bottom" });
    $('#UserMail').tooltip({ 'trigger': 'hover', 'title': 'E-Mailinizi giriniz', "placement": "bottom" });
    $('#UserNumber').tooltip({ 'trigger': 'hover', 'title': 'Telefon numaranızı giriniz', "placement": "bottom" });

    $("#UserNumber").inputmask('Regex', { regex: "[0-9]*" });

    var selectedTab = $("#selectedTab").val();
    //$('#memberTabs a[href="#profile"]').tab('show') // Select tab by name
    //$('#memberTabs a:first').tab('show') // Select first tab
    //$('#memberTabs a:last').tab('show') // Select last tab
    $('#memberTabs li:eq(' + selectedTab + ') a').tab('show') // Select third tab (0-indexed)


});