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
        let res = await PostIdResString(ctrl.entregados + 'unPedidoDetalle/', id);
        if (res == "") {
            $("#cuerpoTabla").html("<tr><td colspan='5' class='text-center'>Aún no ha realizado ningún pedido</td></tr>")
        } else {
            await $("#cuerpoTabla").html(res);
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

