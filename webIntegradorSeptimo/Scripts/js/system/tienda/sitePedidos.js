﻿window.onload = function () {

    start();
}

async function start() {
    try {
        $("#contenedorMaestro").attr("hidden", true);
        $("#espereMaestro").attr("hidden", false);
        await cargarPedidos();
        $("#espereMaestro").attr("hidden", true);
        $("#contenedorMaestro").attr("hidden", false);
    } catch (e) {
        console.log(e);
    }
}




async function cargarPedidos() {
    try {
        const ctrl = new Controladores();
        let res = await GetString(ctrl.home + 'listaPedidos');
        if (res == "") {
            $("#cuerpoTabla").html("<tr><td colspan='5' class='text-center'>Aún no ha realizado ningún pedido</td></tr>")
        } else {
            await $("#cuerpoTabla").html(res);
            await $("#tabla").dataTable(
                {
                    "order": [[0, "desc"]],
                    "columnDefs": [
                        { "orderable": false, "targets": 5 }
                    ]
                }
            );
        }
    } catch (e) {
        console.log(e);
    }
}

async function cargarDatos() {
    try {
        const ctrl = new Controladores();
        let res = await GetJson(ctrl.home + 'unDato');
        if (res != "") {
            let datos = JSON.parse(res);
            $("#idCliente").val(datos.idCliente);
            $("#idCobertura").val(datos.idCobertura);
            $("#nombre").val(datos.nombre);
            $("#apellido").val(datos.apellido);
            $("#celular").val(datos.celular);
            $("#direccion").val(datos.direccion);
            $("#correo").val(datos.correo);
            $("#contra").val(datos.contra);
        } else {
            toastr.error("No pudimos recuperar sus datos recargue la página y vuela a intentarlo");
        }
    } catch (e) {
        toastr.error("No pudimos recuperar sus datos recargue la página y vuela a intentarlo");
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

        if ($("#contra").val() != $("#confir").val()) {
            toastr.info("Las contraseñas no coinciden", msgOp_right());
            return;
        }

        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");

        const frm = document.getElementById("frm");
        const form = new FormData(frm);
        const url = ctrl.home + 'guardar/';
        const res = await PostResString(url, form);

        if (res == "ok") {
            const res2 = await GetString(ctrl.home + "salir/");
            if (res2 == "ok") {
                await toastr.success("Cuenta editada con éxito, vuelva a iniciar sesión", msgOp_right());
                await setTimeout(function () {
                    location.href = "../../Home";
                }, 500);
            }

        } else if (res == "repe") {
            await toastr.info("El correo ingresado ya se encuentra registrado!", msgOp_right());
            $(btn).text("GUARDAR CAMBIOS");
            $(btn).removeAttr("disabled");
        } else {
            $(btn).text("GUARDAR CAMBIOS");
            $(btn).removeAttr("disabled");
            toastr.error(res, msgOp_right());
        }

    } catch (e) {
        $(btn).text("GUARDAR CAMBIOS");
        $(btn).removeAttr("disabled");
        toastr.error(e);
    }
}

function verPedido(id) {
    window.location.href = "../../Tienda/Pedido/?id=" + id;
}




