
$(document).ready(function () {
    $.ajax({
        url: 'http://localhost:4285/SSApi/GetAnnoun',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                $("#h" + i + "").text(data[i].A_TITLE);
                $("#p" + i + "").text(data[i].A_CONTENT);
            }
        },
        error: function (data) {
        }
    })
})

//$(document).ready(function () {

//    $("#sifre").click(function () {
//        $('#sifre').attr("disabled", true);
//        $("#sifre").text("Mail Gönderiliyor");
//        var email = 'email=' + $("#email").val();
//        $.ajax({
//            url: 'http://localhost:30471/Account/SentMail',
//            type: 'post',
//            data: email,
//            dataType: 'json',
//            success: function (data) {

//                $('#sifre').attr("disabled", false);
//                $("#sifre").text("Tekrar Gönder");
//                switch (data) {
//                    case '0':
//                        $('#myModal').modal('show')
//                        break;
//                    case '1':
//                        $('#myModal2').modal('show')
//                        break;
//                    case '2':
//                        // $('#myModal').modal('show')
//                        break;
//                    case '3':
//                        // $('#myModal').modal('show')
//                        break;
//                    case '4':
//                        // $('#myModal').modal('show')
//                        break;
//                    default:
//                        //$('#myModal').modal('show')
//                }
//            },
//            error: function () {


//            },
//        })

//    })
//})

//aaaaaaaaaaaaaaaa

function ePostaKont(eposta) {
    var duzenli = new RegExp(/^[a-z]{1}[\d\w\.-]+@[\d\w-]{3,}\.[\w]{2,3}(\.\w{2})?$/);

    return duzenli.test(eposta);
}

$(document).ready(function () {
    $("#sifre").click(function () {

        var email2 = $("#email").val();
        if (email2 == "") {
            $('#myModal2').modal('show');
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
                                url: 'http://localhost:30471/Account/SentMail',
                                type: 'post',
                                data: "email=" + $("#email").val(),
                                dataType: 'json',
                                success: function (data) {
                                    if (data == "3")
                                    {
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
                                    else{
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
                $('#myModal3').modal('show');
            }
        }
    });

});



//aaaaaaaaaaaaaaaa



$(document).ready(function () {


    $("#gonder").click(function () {
        var a = $("#email").prop("class"); if (a.toString() == "form-control input-validation-error") {
            $("#gonder").text("Maili Düzelt Gönder");
        }
        else {
            $('#gonder').attr("disabled", true);
            $("#gonder").text("Mail Gönderiliyor");
            var isim = $("#isim").val();
            var soyad = $("#soyad").val();
            var email = $("#email").val();
            var tel = $("#tel").val();
            var mesaj = $("#mesaj").val();

            $.ajax({
                url: 'http://localhost:30471/Home/iletisimPost',
                type: 'post',
                data: { isim, soyad, email, tel, mesaj},
                dataType: 'json',
                success: function (data) {
                    $('#gonder').attr("disabled", false);
                    $("#gonder").text("Tekrar Gönder");
                    if (data == "1") {
                        $('#myModal2').modal('show')
                    }
                    else {
                        $('#myModal').modal('show')
                    }

                },
                error: function (data) {

                }
            })


        }


    })
})



