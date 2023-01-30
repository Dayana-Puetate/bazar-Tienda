window.onload = function () {
    start();
    menus();
}

async function start() {

    $("#espere").removeAttr("hidden");
    await cargarDatos();
    $("#espere").hide();
    $("#contenedor").removeAttr("hidden");
}

async function cargarDatos() {
    try {

        const ctrl = new Controladores();
        const res = await GetJson(ctrl.configuraciones + "unDato");
        if (res != "") {
            $("#titulo").html("<i class='fa fa-angle-right p-2' ></i> Editar configuración de sistema");
            const datos = JSON.parse(res);

            $("#idConfiguracion").val(datos.idConfiguracion);
            $("#about").val(datos.about);
            $("#telefono").val(datos.telefono);
            $("#direccion").val(datos.direccion);
        }

    } catch (e) {
        console.log(e);
    }
}

async function guardar(btn) {
    const ctrl = new Controladores();
    try {
        const veri = await validarTodo();

        if (veri == false) {
            toastr.warning("Revise los campos requeridos", msgOp_right());
            return;
        }


        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");
        $("#btnCancelar").hide();
        const frm = document.getElementById("frm");
        const form = new FormData(frm);
        const url = ctrl.configuraciones + 'guardar/';
        const res = await PostResString(url, form);

        if (res == "ok") {
            await toastr.success("Guardado con éxito", msgOp_right());
            await setTimeout(function () {
                location.href = "../../Administrador";
            }, 500);
        } else {
            $(btn).text("GUARDAR");
            $(btn).removeAttr("disabled");
            $("#btnCancelar").show();
            toastr.error(res, msgOp_right());
        }

    } catch (e) {
        $(btn).text("GUARDAR");
        $(btn).removeAttr("disabled");
        $("#btnCancelar").show();
        toastr.error(e);
    }
}