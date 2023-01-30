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
        const res = await GetString(ctrl.reposiciones + 'listar');
        if (res == "") {
            $("#cuerpoTabla").html("<tr><td colspan='4' class='text-center'>No existen registros</td></tr>");
        } else {
            await $("#cuerpoTabla").html(res);
            await $("#tabla").dataTable();
        }

    } catch (e) {
        toastr.error(e, msgOp_center());
    }

}
