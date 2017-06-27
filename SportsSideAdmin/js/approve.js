$(document).ready(function () {
    bootbox.setLocale("tr");
    var selectedTab = $("#selectedTab").val();
    $('#myTabs li:eq(' + selectedTab + ') a').tab('show');
    $(".btnRejectRes").click(function () {
        var id = $(this).attr("id");
        bootbox.confirm({
            title: "Uyarı",
            message: "Reddetmek istediğinize emin misiniz?",
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
                        url: "/Approve/RejectReservation",
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

    $(".btnApproveRes").click(function () {
        var id = $(this).attr("id");
        bootbox.confirm({
            title: "Uyarı",
            message: "Onaylamak istediğinize emin misiniz?",
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
                        url: "/Approve/ApproveReservation",
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

    $(".btnApproveSub").click(function () {
        var id = $(this).attr("id");
        bootbox.confirm({
            title: "Uyarı",
            message: "Onaylamak istediğinize emin misiniz?",
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
                        url: "/Approve/ApproveSubscriber",
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

    $(".btnRejectSub").click(function () {
        var id = $(this).attr("id");
        bootbox.confirm({
            title: "Uyarı",
            message: "Reddetmek istediğinize emin misiniz?",
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
                        url: "/Approve/RejectSubscriber",
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

    $('#myTabs a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
        $(".dropdown-menu").css({ display: "none" });
    });

    $('ul.nav li.approveTabs').hover(function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(200);
    }, function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(200);
    });

    $(".resultOK").click(function () {
        var url = $(this).attr("id");
        window.location.href = url;
    });
});