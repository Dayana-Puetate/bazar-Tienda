async function ingresar(btn) {
    const ctrl = new Controladores();
    try {

        toastr.remove();
        const val = await validarTodo();
        if (val == false) {
            toastr.warning("Revise los campos requeridos", msgOp_right());
            return;
        }
        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");
        const frm = document.getElementById("frm");
        const form = new FormData(frm);
        const url = ctrl.administrador + 'login/';
        const res = await PostResJson(url, form);
        if (res != "") {
            var datos = JSON.parse(res);
            await toastr.success("Bienvenido " + datos.nombre, msgOp_right());
            await setTimeout(function () {
                $(btn).text("INGRESAR");
                $(btn).removeAttr("disabled");
                location.reload();
            }, 1000);

        } else {
            $(btn).text("INGRESAR");
            $(btn).removeAttr("disabled");
            toastr.error("Credenciales incorrectas", msgOp_right());
        }
    } catch (e) {
        $(btn).text("INGRESAR");
        $(btn).removeAttr("disabled");
        toastr.error(e);
    }

}




