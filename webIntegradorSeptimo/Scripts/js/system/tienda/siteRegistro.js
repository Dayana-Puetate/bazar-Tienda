window.onload = function () {
    start();
}

async function start() {
    $("#contenedorMaestro").attr("hidden", true);
    $("#espereMaestro").attr("hidden", false);
    await cargarCobertura();
    $("#espereMaestro").attr("hidden", true);
    $("#contenedorMaestro").attr("hidden", false);
}

async function cargarCobertura() {
    try {
        const ctrl = new Controladores();
        let res = await GetString(ctrl.home + "comboCobertura");
        if (res != "") {
            $("#idCobertura").html(res);
        }
    } catch (e) {
        console.log(e);
    }


}

async function crearCuenta(btn) {
    const ctrl = new Controladores();
    try {
        const veri = await validarTodo();

        if (veri == false) {
            toastr.warning("Revise los campos requeridos", msgOp_right());
            $("input[id*='Login']").each(function (i) {
                let el = $("input[id*='Login']").get(i);
                let nomEl = "#" + $(el).attr("id")+"Val";
                $(el).removeClass("is-invalid");
                $(nomEl).attr("hidden", true);
            });
            return;
        }

        if ($("#contra").val() != $("#confir").val()) {
            toastr.info("Las contraseñas no coinciden",msgOp_right());
            return;
        }

        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");

        const frm = document.getElementById("frmRegistro");
        const form = new FormData(frm);
        const url = ctrl.home + 'guardar/';
        const res = await PostResString(url, form);

        if (res == "ok") {
            await toastr.success("Cuenta creada con éxito", msgOp_right());
            await setTimeout(function () {
                location.href = "../../Home";
            }, 500);
        } else if (res == "repe") {
            await toastr.info("El correo ingresado ya se encuentra registrado!", msgOp_right());
            $(btn).text("CREAR CUENTA");
            $(btn).removeAttr("disabled");
        } else {
            $(btn).text("CREAR CUENTA");
            $(btn).removeAttr("disabled");
            toastr.error(res, msgOp_right());
        }

    } catch (e) {
        $(btn).text("CREAR CUENTA");
        $(btn).removeAttr("disabled");
        toastr.error(e);
    }
}


async function iniciarSesion(btn) {
    const ctrl = new Controladores();
    try {
        if ($("#correoLogin").val().trim() == "" || $("#contraLogin").val().trim() == "") {
            toastr.info("Ingrese su correo y su contraseña", msgOp_right());
            return;
        }
        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");

        const frm = document.getElementById("frmLogin");
        const form = new FormData(frm);
        const url = ctrl.home + 'login/';
        const res = await PostResString(url, form);
        if (res == "ok") {
            await toastr.success("Bienvenido", msgOp_right());
            await setTimeout(function () {
                location.href = "../../Home";
            }, 500);
        } else {
            $(btn).text("INICIAR SESIÓN");
            $(btn).removeAttr("disabled");
            toastr.error(res, msgOp_right());
        }

    } catch (e) {
        $(btn).text("INICIAR SESIÓN");
        $(btn).removeAttr("disabled");
        toastr.error(e);
    }
}
