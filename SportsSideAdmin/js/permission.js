$(document).ready(function () {
    bootbox.setLocale("tr");

    $(".btnGivePerm").click(function () {
        var id = $(this).attr("id");
        bootbox.confirm({
            title: "Uyarı",
            message: "Yetki vermek istediğinize emin misiniz?",
            buttons: {
                confirm: {
                    size: "small",
                    label: "Evet",
                    className: "btn-success"
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
                        url: "/Permission/GivePermission",
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
                            console.log("HATA : ", req, status, err);
                        }
                    });
                }
            }
        })
    });

    $(".btnTakePerm").click(function () {
        var id = $(this).attr("id");
        bootbox.confirm({
            title: "Uyarı",
            message: "Yetki almak istediğinize emin misiniz?",
            buttons: {
                confirm: {
                    size: "small",
                    label: "Evet",
                    className: "btn-success"
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
                        url: "/Permission/TakePermission",
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
                            console.log("HATA : ", req, status, err);
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
});