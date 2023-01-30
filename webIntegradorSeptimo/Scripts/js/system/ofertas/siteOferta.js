
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
        const res = await GetString(ctrl.ofertas + 'listar');
        if (res == "") {
            $("#cuerpoTabla").html("<tr><td colspan='7' class='text-center'>No existen registros</td></tr>");
        } else {
            await $("#cuerpoTabla").html(res);
            await $("#tabla").dataTable(
                {
                    "columnDefs": [
                        { "orderable": false, "targets": 6 }
                    ]
                }
            );
        }

    } catch (e) {
        toastr.error(e, msgOp_center());
    }

}

function redirect(id) {
    const ctrl = new Controladores();
    location.href = ctrl.ofertas + "Oferta/?id=" + id;
}

async function preguntar(id) {
    toastr.remove();
    toastr.warning("<div class='text-center'><strong>¿Esta seguro que desea eliminar?</strong>  </br> Los cambios no se pueden deshacer</br>  <button type='button' class='btn btn-defult btn-sm mt-2' onclick='eliminar(" + id + ")'>Sí, Eliminar</button></div>", toastr.options = {
        "closeButton": true,
        "positionClass": "toast-top-right",
        "timeOut": 0,
        "onclick": null
    });

}

async function eliminar(id) {
    toastr.remove();
    try {
        const ctrl = new Controladores();
        const res = await PostIdResString(ctrl.ofertas + "eliminar", id);
        if (res == "ok") {
            await toastr.success("Eliminado con éxito", msgOp_right());
            var table = $('#tabla').DataTable();
            table.clear().draw();
            table.destroy();
            await cargarTabla();
        } else {
            await toastr.error("Error al eliminar: " + res, msgOp_right());
        }

    } catch (e) {
        toastr.error(e);
    }



}