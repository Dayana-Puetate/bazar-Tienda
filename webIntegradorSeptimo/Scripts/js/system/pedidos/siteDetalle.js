window.onload = function () {
    start();
    menus();
}

async function start() {
    var id = new URL(window.location.href).searchParams.get("id");
    $("#espere").removeAttr("hidden");
    if (id != null) {
        await cargarDatosEnvio(id);
        await cargarDetallePedidos(id);
        await cargarDatosPedido(id);
        await cargarDatosTracking(id);
        await mostrarBotonCancelar(id);
        await mostrarBotonAccion(id);

    }
    $("#espere").hide();
    $("#contenedor").removeAttr("hidden");
}

async function cargarDatosEnvio(id) {
    try {
        const ctrl = new Controladores();
        let res = await PostIdResString(ctrl.pedidos + 'datosEnvio/', id);
        if (res != "") {
            await $("#cuerpoTablaEnvio").html(res);
        }
    } catch (e) {
        console.log(e);
    }
}



async function cargarDetallePedidos(id) {
    try {
        const ctrl = new Controladores();
        let res = await PostIdResString(ctrl.pedidos + 'unPedidoDetalle/', id);
        if (res == "") {
            $("#cuerpoTabla").html("<tr><td colspan='5' class='text-center'>Aún no ha realizado ningún pedido</td></tr>")
        } else {
            await $("#cuerpoTabla").html(res);
        }
    } catch (e) {
        console.log(e);
    }
}

async function mostrarBotonCancelar(id) {
    try {
        const ctrl = new Controladores();
        let res = await PostIdResString(ctrl.pedidos + 'puedeCancelarPedido/', id);
        if (res == "ok") {
            $("#btnCancelar").attr("hidden", false);
        }
    } catch (e) {
        console.log(e);
    }
}

async function mostrarBotonAccion(id) {
    try {
        const ctrl = new Controladores();
        let res = await PostIdResString(ctrl.pedidos + 'botonAccion/', id);
        if (res != "") {
            $("#contenedorBotones").append(res);
        }
    } catch (e) {
        console.log(e);
    }
}

async function cargarDatosPedido(id) {
    try {
        const ctrl = new Controladores();
        let res = await PostIdResJson(ctrl.home + 'unPedido', id);
        if (res != "") {
            let datos = JSON.parse(res);
            $("#idPedido").text(datos.idPedido);
            $("#valorSubtotal").text(datos.subtotal);
            $("#valorEnvio").text(datos.envio);
            $("#valorIva").text(datos.iva);
            $("#valorTotal").text(datos.total);

        } else {
            toastr.error("No pudimos recuperar sus datos recargue la página y vuela a intentarlo");
        }
    } catch (e) {
        toastr.error("No pudimos recuperar sus datos recargue la página y vuela a intentarlo");
        console.log(e);
    }
}

async function cargarDatosTracking(id) {
    try {
        const ctrl = new Controladores();
        let res = await PostIdResString(ctrl.home + 'trackingPedido', id);
        if (res != "") {
            $("#cuerpoTablaTracking").html(res);

        } else {
            toastr.error("No pudimos recuperar los datos de su tracking recargue la página y vuela a intentarlo");
        }
    } catch (e) {
        toastr.error("No pudimos recuperar los datos de su tracking recargue la página y vuela a intentarlo");
        console.log(e);
    }
}

async function preguntar() {
    toastr.remove();
    toastr.warning("<div class='text-center'><strong>¿Esta seguro que desea cancelar este pedido?</strong>  </br> Los cambios no se pueden deshacer</br>  <button type='button' class='btn btn-defult btn-sm mt-2' onclick='cancelar()'>Sí, Cancelar</button></div>", toastr.options = {
        "closeButton": true,
        "positionClass": "toast-top-right",
        "timeOut": 0,
        "onclick": null
    });
}
async function cancelar() {
    toastr.remove();
    let btn = document.getElementById("btnCancelar");
    const ctrl = new Controladores();
    try {
        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");
        let id = $("#idPedido").text();
        const url = ctrl.home + 'cancelarPedido/';
        const res = await PostIdResString(url, id);

        if (res == "ok") {
            await toastr.success("Pedido cancelado exitosamente", msgOp_right());
            await setTimeout(function () {
                location.href = "../../Pedidos";
            }, 500);
        } else {
            $(btn).text("CANCELAR PEDIDO");
            $(btn).removeAttr("disabled");
            toastr.error(res, msgOp_right());
        }

    } catch (e) {
        $(btn).text("CANCELAR PEDIDO");
        $(btn).removeAttr("disabled");
        toastr.error(e);
    }
}

async function guardarEstado(id, idEstado) {
    toastr.remove();
    let btn = document.getElementById("btnGuardarEstado");
    let nombreAnterior = $(btn).text();
    const ctrl = new Controladores();
    try {
        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");
        const url = ctrl.pedidos + "guardarEstado";
        const res = await $.get(url,{id,idEstado});
        if (res == "ok") {
            await toastr.success("Pedido procesado exitosamente", msgOp_right());
            await setTimeout(function () {
                location.href = "../../Pedidos";
            }, 500);
        } else {
            $(btn).text(nombreAnterior);
            $(btn).removeAttr("disabled");
            toastr.error(res, msgOp_right());
        }

    } catch (e) {
        $(btn).text(nombreAnterior);
        $(btn).removeAttr("disabled");
        toastr.error(e);
    }
}
