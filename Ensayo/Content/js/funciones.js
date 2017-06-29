$(document).ready(function () {
    $("#numero_sala").on("change", function () {
        alert("Has cambiado el numero de sala");
        $.ajax(
            {
                url: "/Inicio",
                method: "GET",
                success:
            }
        );
    });
});