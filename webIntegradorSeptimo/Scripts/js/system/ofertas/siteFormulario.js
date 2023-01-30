window.onload = function () {
    menus();
    start();
}

async function start() {
    var id = new URL(window.location.href).searchParams.get("id");
    $("#espere").removeAttr("hidden");
    await cargarProductos();
    if (id != null) {
        await cargarDatos(id);

    }
    $("#espere").hide();
    $("#contenedor").removeAttr("hidden");
}


async function cargarProductos() {
    try {
        const ctrl = new Controladores();
        const res = await GetString(ctrl.ofertas + "comboProducto");
        if (res != "") {
            $("#idProducto").html(res);
        }

    } catch (e) {
        console.log(e);
    }
}


async function cargarDatos(id) {
    try {
        $("#titulo").html("<i class='fa fa-angle-right p-2' ></i> Editar Oferta");
        const ctrl = new Controladores();
        const res = await PostIdResJson(ctrl.ofertas + "unDato", id);
        if (res != "") {
            const datos = JSON.parse(res);

            $("#idOferta").val(datos.idOferta);
            $("#idProducto").val(datos.idProducto);
            $("#descripcion").val(datos.descripcion);
            $("#porcentaje").val(datos.porcentaje);
            $("#fecha_inicio").val(convertirFecha(datos.fecha_inicio));
            $("#fecha_fin").val(convertirFecha(datos.fecha_fin));
            $("#activo").val(datos.activo.toString());
        }

    } catch (e) {
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
        const veriFechas = await verificarFechas($("#fecha_inicio"), $("#fecha_fin"));
        if (veriFechas == false) {

            toastr.warning("La fecha de inicio no puede ser mayor o igual a la final", msgOp_right());
            return;
        }

        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");
        $("#btnCancelar").hide();
        const frm = document.getElementById("frm");
        const form = new FormData(frm);
        const url = ctrl.ofertas + 'guardar/';
        const res = await PostResString(url, form);

        if (res == "ok") {
            await toastr.success("Guardado con éxito", msgOp_right());
            await setTimeout(function () {
                location.href = "../../ofertas";
            }, 500);
        } else if (res == "repe") {
            await toastr.info("El producto ya posee una oferta vigente!", msgOp_right());
            $(btn).text("GUARDAR");
            $(btn).removeAttr("disabled");
            $("#btnCancelar").show();
        } else {
            $(btn).text("GUARDAR");
            $(btn).removeAttr("disabled");
            $("#btnCancelar").show();
            toastr.error(res, msgOp_right());
        }

    } catch (e) {
        $(btn).text("GUARDAR");
        $(btn).removeAttr("disabled");
        $("#btnCancelar").show();
        toastr.error(e);
    }
}

async function verificarFechas(f1,f2) {
    try {
        var fecha_inicial = Date.parse($(f1).val());
        var fecha_fin = Date.parse($(f2).val());
        if (fecha_inicial >= fecha_fin) {
            $(f1).addClass("is-invalid");
            $(f2).addClass("is-invalid");
            $("#fec_may_in").remove();
            $(f1).parent().append('<span class="text-danger font-weight-bold pl-2" id="fec_may_in">La fecha de inicio no puede ser mayor o igual a la fecha fin</span>');
            return false;
        } else {
            $(f1).removeClass("is-invalid");
            $(f2).removeClass("is-invalid");
            $("#fec_may_in").remove();
        }
    } catch (e) {

    }
}

