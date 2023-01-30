window.onload = function () {
    start();
    menus();
}

async function start() {

    try {
        $("#espere").removeAttr("hidden");
        await cargarTabla();
        $("#espere").hide();
        $("#contenedor").removeAttr("hidden");
    } catch (e) {
        $("#espere").hide();
        $("#contenedor").removeAttr("hidden");
        toastr.error(e, msgOp_center());
    }

}

async function cargarTabla() {
    toastr.remove();
    try {
        const ctrl = new Controladores();
        const res = await GetString(ctrl.entregados + 'listaPedidos');
        if (res == "") {
            $("#cuerpoTabla").html("<tr><td colspan='6' class='text-center'>No existen registros</td></tr>");
        } else {
            await $("#cuerpoTabla").html(res);
            await $("#tabla").dataTable(
                {
                    "columnDefs": [
                        { "orderable": false, "targets": 5 }
                    ]
                }
            );
        }

    } catch (e) {
        toastr.error(e, msgOp_center());
    }

}

function verPedido(id) {
    const ctrl = new Controladores();
    location.href = ctrl.entregados + "Pedido/?id=" + id;
}

