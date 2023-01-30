
window.onload = function () {
   start();
   menus();
}



async function start() {

    var id = new URL(window.location.href).searchParams.get("id");
    $("#espere").removeAttr("hidden");
    await cargarCategorias();

    if (id != null) {
        await cargarDatos(id);
        await cargarImagenes(id);
    }
    $("#espere").hide();
    $("#contenedor").removeAttr("hidden");
}


async function cargarCategorias() {

    try {
        const ctrl = new Controladores();
        $("#idCategoria").html(await GetString(ctrl.productos + "comboCategorias"));

    } catch (e) {
        console.log(e);
    }

}

async function cargarImagenes(id) {
    try {

        const ctrl = new Controladores();
        const res = await PostIdResJson(ctrl.productos + "imagenes", id);
        if (res != "") {
            const datos = JSON.parse(res);
            datos.forEach(async function (item) {
                await jclc_add('../../Imagenes/'+item.referencia,item.size);
            });
        }


    } catch (e) {
        console.log(e);
    }

}


async function cargarDatos(id) {

    try {
        $("#titulo").html("<i class='fa fa-angle-right p-2' ></i> Editar Producto");
        const ctrl = new Controladores();
        const res = await PostIdResJson(ctrl.productos + "unDato", id);
        if (res != "") {
            const datos = JSON.parse(res);
            $("#idProducto").val(datos.idProducto);
            $("#idCategoria").val(datos.idCategoria);
            $("#nombre").val(datos.nombre);
            $("#descripcion").val(datos.descripcion);
            $("#pvp").val(datos.pvp);
            $("#stock").val(datos.stock);
            $("#iva").val(datos.iva);
            $("#paga_envio").val(datos.paga_envio.toString());
        }

    } catch (e) {
        console.log(e);
    }
}


async function guardar(btn) {
const ctrl = new Controladores();

try {
    toastr.clear();
    const veriImagenes = await jclc_min_files(1);

    if (veriImagenes == false) {
        toastr.warning("Se requiere al menos una imagen", msgOp_right());
        return;
    }
    const veri = await validarTodo();
    if (veri == false) {
        toastr.warning("Revise los campos requeridos", msgOp_right());
        return;
    }

    $(btn).text("ESPERE. . . ");
    $(btn).attr("disabled");
    $("#btnCancelar").hide();
    const frm = document.getElementById("frm");
    const form = new FormData(frm);

    jclc_array.forEach(function (item) {
        form.append("files", item.file);
    });

    const res = await PostResString(ctrl.productos + "guardar", form);


    if (res == "ok") {
        jclc_array.clear;
        await toastr.success("Guardado con éxito", msgOp_right());
        await setTimeout(function () {
            location.href = "../../Productos";
        }, 500);
    } else if (res == "repe") {
        await toastr.info("El producto ya existe!", msgOp_right());
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

