window.onload = function () {
    start();

}



async function start() {
    try {
        $("#espere").removeAttr("hidden");
        const ctrl = new Controladores();
        const res = await GetJson(ctrl.administrador + 'datosCuenta');
        if (res != "") {
            const datos = JSON.parse(res);
            $("#idAdministrador").val(datos.idAdministrador);
            $("#cedula").val(datos.cedula);
            $("#nombre").val(datos.nombre);
            $("#apellido").val(datos.apellido);
            $("#telefono").val(datos.telefono);
            $("#mail").val(datos.mail);
            $("#usuario").val(datos.usuario);
            $("#contra").val(datos.contra);
            $("#confir").val(datos.contra);
            $("#activo").val(datos.activo);
            $("#fecha_registro").val(datos.fecha_registro);
        }

        await validarTodo();
        $("#espere").hide();
        $("#contenedor").removeAttr("hidden");

    } catch (e) {
        console.log(e);
        $("#espere").hide();
        $("#contenededor").removeAttr("hidden");
    }

}

async function guardar(btn) {
    const ctrl = new Controladores();
    try {
        const veri = await validarTodo();

        toastr.remove();
        if (veri == false) {
            toastr.warning("Revise los campos requeridos", msgOp_right());
            return;
        }


        if ($("#contra").val().trim() != $("#confir").val().trim()){
            toastr.warning("Las contraseñas no coinciden", msgOp_right());
            return;
         }

        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");
        $("#btnCancelar").hide();
        const frm = document.getElementById("frm");
        const form = new FormData(frm);
        const url = ctrl.administrador + 'guardar/';
        const res = await PostResString(url, form);

        if (res != "") {
            await GetString(ctrl.administrador + "salir");
            await toastr.success("Guardado con éxito, deberá volver a iniciar sesión", msgOp_right());

            await setTimeout(function () {
                location.reload();
            }, 1500);

            
        } else {
            toastr.error(res, msgOp_right());
        }
    } catch (e) {
        $(btn).text("GUARDAR");
        $(btn).removeAttr("disabled");
        $("#btnCancelar").show();
        toastr.error(e);
    }


}