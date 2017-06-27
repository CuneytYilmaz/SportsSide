$(document).ready(function () {
    $('.message a').click(function () {
        $('form').animate({ height: "toggle", opacity: "toggle" }, "slow");
    });
});

function ePostaKont(eposta) {
    var duzenli = new RegExp(/^[a-z]{1}[\d\w\.-]+@[\d\w-]{3,}\.[\w]{2,3}(\.\w{2})?$/);

    return duzenli.test(eposta);
}

$(document).ready(function () {

    $("#sifre").click(function () {
        var email2 = $("#email").val();
        if (email2 == "") {
            setTimeout(function () {
                swal({

                    title: "Hata!",
                    text: "E-Posta Adresi Boş Olamaz!",
                    type: "error",
                    showCancelButton: false,
                    closeOnConfirm: false,
                    showLoaderOnConfirm: true,
                    confirmButtonText: "Tekrar Dene",
                    confirmButtonColor: "#449d44"
                }, function () {
                    var url = $(".aaab").attr("id");

                    window.location.href = url;

                });
            }, 0);
        }
        else {
            if (ePostaKont($("#email").val()) == true) {
                swal({
                    title: "Emin misiniz?",
                    text: "Mail göndermek istediğinize emin misiniz?",
                    type: "warning",
                    showCancelButton: true,
                    closeOnConfirm: false,
                    closeOnCancel: false,
                    showLoaderOnConfirm: true,
                    confirmButtonText: "Evet!",
                    confirmButtonColor: "#449d44",
                    cancelButtonText: "Hayır"
                },
                function (isConfirm) {
                    if (isConfirm == false) {
                        swal("İptal edildi", "Mail göndermekten vazgeçildi!", "error");
                    }
                    else {
                        var email = $("#email").val();
                        $.ajax(
                            {
                                url: 'http://localhost:10988/Account/SentMail',
                                type: 'post',
                                data: "email=" + $("#email").val(),
                                dataType: 'json',
                                success: function (data) {
                                    if (data == "3") {
                                        setTimeout(function () {
                                            swal({

                                                title: "Hata!",
                                                text: "E-Posta Adresi Sisteme Kayıtlı Değil!",
                                                type: "error",
                                                showCancelButton: false,
                                                closeOnConfirm: false,
                                                showLoaderOnConfirm: true,
                                                confirmButtonText: "Tekrar Dene",
                                                confirmButtonColor: "#449d44"
                                            }, function () {
                                                var url = $(".aaab").attr("id");

                                                window.location.href = url;

                                            });
                                        }, 0);
                                    }
                                    else {
                                        if (data) {
                                            setTimeout(function () {
                                             
                                                swal({

                                                    title: "İşlem tamam!",
                                                    text: "mail göndermen başarıyla tamamlandı!",
                                                    type: "success",
                                                    showCancelButton: false,
                                                    closeOnConfirm: false,
                                                    showLoaderOnConfirm: true,
                                                    confirmButtonText: "Tamam",
                                                    confirmButtonColor: "#449d44"
                                                }, function () {
                                                    var url = $(".aaa").attr("id");
                                                    $('#myModal').modal('show')
                                                    window.location.href = url;

                                                });
                                            }, 0);
                                        }
                                        else {
                                            swal("Oops", "Mail gönderilirken bir sorun oluştu!", "error");
                                        }
                                    }


                                }
                            })
                        .done(function (data) {
                            swal("İşlem tamam!", "Mail gönderilirken tamamlandı!", "success");

                        })
                        .error(function (data) {
                            swal("Oops", "Mail gönderilirken bir sorun oluştu!", "error");
                        });

                    }
                });
            }
            else {
                setTimeout(function () {
                    swal({

                        title: "Hata!",
                        text: "Geçerli E-Posta Adresi Giriniz!",
                        type: "error",
                        showCancelButton: false,
                        closeOnConfirm: false,
                        showLoaderOnConfirm: true,
                        confirmButtonText: "Tekrar Dene",
                        confirmButtonColor: "#449d44"
                    }, function () {
                        var url = $(".aaab").attr("id");

                        window.location.href = url;

                    });
                }, 0);
            }
        }
    });

});
