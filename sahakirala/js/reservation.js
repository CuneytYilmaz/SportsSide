$(document).ready(function () {
    $('.item').first().addClass('active');
    $('.carousel-indicators > li').first().addClass('active');
    $('#carousel-example-generic').carousel();
    $('#partialSummary').hide();
    $('.form_date').datetimepicker({
        language: 'tr',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0
    });
    $("#btnSearch").click(function () {
        var facilityId = hdnFacility.value;
        $("#hdnDate").val($("#dateField").val());
        var myDate = $("#hdnDate").val();
        $.ajax({
            url: "/Reservation/Search",
            type: "GET",
            data: { id: facilityId, date: myDate }
        })
            .done(function (partialViewResult) {
                $("#partialArea").html(partialViewResult);
            });
    });

    $("#btnSubscribe").click(function () {
        $("#ddlDays").val($("#ddlDays option:first").val());
        $("#ddlHours").val($("#ddlHours option:first").val());
        $("#dvErrors").hide();
        $("#dvErrorTable").hide();
        $("#validateDay").text("");
        $("#validateHour").text("");

        var facilityId = hdnFacility.value;
        $.ajax({
            url: "/Reservation/RefreshSubscribe",
            type: "GET",
            data: { id: facilityId }
        })
        .done(function (partialViewResult) {
            $("#partialSubscribe").html(partialViewResult);
            $('#myModal').modal('show');
        });
    });

    if ($("#hdnNoPhoto").val() == "True") {
        $("#carousel-example-generic").hide();
    }
});