$(document).ready(function () {
    alert('aaaaaa');
    $.ajax({
        
        url: 'http://localhost:4285/SSApi/GetAnnounAdmin',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            
            $.each(data, function (i, item) {
                $('<tr>').html(
                    $('td').text(item.A_TITLE),
                    $('td').text(item.A_CONTENT)
                ).appendTo('#annTable');

            });
        },
        error: function (data) {
            alert("Başarısız");
        }
    })
})