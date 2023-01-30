window.onload = function () {
    start();
    menus();
}

async function start() {

    try {
        $("#espere").removeAttr("hidden");
        await cargarComboEstado();
        $("#espere").hide();
        $("#contenedor").removeAttr("hidden");
    } catch (e) {
        $("#espere").hide();
        $("#contenedor").removeAttr("hidden");
        toastr.error(e, msgOp_center());
    }

}

async function cargarComboEstado() {
    try {
        const ctrl = new Controladores();
        let res = await GetString(ctrl.reportes + 'comboEstados');
        if (res != "") {
                $("#cmbEstado").html(res);
            }
    } catch (e) {
        console.log(e);
    }
}


async function cargarTabla() {
    toastr.remove();
    if ($("#desde").val() == "" || $("#hasta") == "" || $("#cmbEstado").val() == "" || $("#cmbEstado").val() == undefined || $("#cmbEstado").val() == null) {
        toastr.info("Debe seleccionar un tipo, una fecha de desde y una hasta", msgOp_right());
        return;
    }
    let desde = $("#desde").val();
    let hasta = $("#hasta").val()
    let idEstado = $("#cmbEstado").val();

    if (Date.parse(desde) > Date.parse(hasta)) {
        toastr.info("La fecha desde no puede ser mayor a la fecha hasta", msgOp_right());
        return;
    }
    try {
        const ctrl = new Controladores();
        const res = await $.get(ctrl.reportes + '/reporte', {idEstado,desde,hasta});
        if (res == "") {
            $("#divImprimir").html("");
            $("#detalleReporte").text("");
            $("#cuerpoTabla").html("<tr><td colspan='3' class='text-center'>No existen resultados para esa búsqueda</td></tr>");
        } else {
            await $("#cuerpoTabla").html(res);
            $("#divImprimir").html("<button type='button' class='btn btn-round btn-sm texto-verde' style='background:#485150 !important;' onclick='window.print()'><i class='fa fa-print'></i> IMPRIMIR</button>");
            let texto = "<h5>Reporte de pedidos "+$("#cmbEstado option:selected").text()+"s</h5> </br> <strong>Desde:</strong> "+desde+" </br> <strong>Hasta:</strong> "+hasta;
            $("#detalleReporte").html(texto);
        }

    } catch (e) {
        toastr.error(e, msgOp_center());
    }

}

function redirect(id) {
    const ctrl = new Controladores();
    location.href = ctrl.categorias + "Categoria/?id=" + id;
}

async function preguntar(id) {
    toastr.remove();
    toastr.warning("<div class='text-center'><strong>¿Esta seguro que desea eliminar?</strong>  </br> Los cambios no se pueden deshacer</br>  <button type='button' class='btn btn-defult btn-sm mt-2' onclick='eliminar("+id+")'>Sí, Eliminar</button></div>", toastr.options={
        "closeButton" : true,
        "positionClass": "toast-top-right",
        "timeOut": 0,
        "onclick": null
    });

}

async function eliminar(id) {
    toastr.remove();
    try {
        const ctrl = new Controladores();
        const res = await PostIdResString(ctrl.categorias + "eliminar", id);
        if (res == "ok") {
            await toastr.success("Eliminado con éxito", msgOp_right());
            var table = $('#tabla').DataTable();
            table.clear().draw();
            table.destroy();
            await cargarTabla();
        } else {
            await toastr.error("Error al eliminar: "+res, msgOp_right());
        }

    } catch (e) {
        toastr.error(e);
    }



}