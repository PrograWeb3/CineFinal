$(document).ready(function () {
    $("#numero_sala").on("change", function () {
        var numero_sala = $("#numero_sala").val();
        location.href = "/Administracion/Funciones/"+numero_sala;
    });
});