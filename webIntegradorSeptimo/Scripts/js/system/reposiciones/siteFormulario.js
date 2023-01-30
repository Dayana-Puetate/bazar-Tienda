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
        const res = await GetString(ctrl.reposiciones + "comboProducto");
        if (res != "") {
            $("#idProducto").html(res);
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


        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");
        $("#btnCancelar").hide();
        const frm = document.getElementById("frm");
        const form = new FormData(frm);
        const url = ctrl.reposiciones + 'guardar/';
        const res = await PostResString(url, form);

        if (res == "ok") {
            await toastr.success("Guardado con éxito", msgOp_right());
            await setTimeout(function () {
                location.href = "../../Reposiciones";
            }, 500);
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