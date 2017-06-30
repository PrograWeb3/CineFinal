$(document).ready(function () {
    $("#numero_sala").on("change", function () {
        var numero_sala = $("#numero_sala").val();
        $.ajax(
            {                
                url: "/Administracion/FuncionesPorSala",
                type: "GET",
                data: { Id: numero_sala },
                dataType: "json",
                contentType: "application/json",
                success: function (result) {
                    alert(result);
                    $(".tabla").html(result);
                }
            });
    });
});