window.onload = function () {
    start();
    menus();
}

async function start() {
    var id = new URL(window.location.href).searchParams.get("id");
    $("#espere").removeAttr("hidden");
    if (id != null) {
        await cargarDatos(id);
    }
    $("#espere").hide();
    $("#contenedor").removeAttr("hidden");
}

async function cargarDatos(id) {
    try {
        $("#titulo").html("<i class='fa fa-angle-right p-2' ></i> Editar Cobertura");
        const ctrl = new Controladores();
        const res = await PostIdResJson(ctrl.coberturas + "unDato", id);
        if (res != "") {
            const datos = JSON.parse(res);

            $("#idCobertura").val(datos.idCobertura);
            $("#ciudad").val(datos.ciudad);
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
        const url = ctrl.coberturas + 'guardar/';
        const res = await PostResString(url, form);

        if (res == "ok") {
            await toastr.success("Guardado con éxito", msgOp_right());
            await setTimeout(function () {
                location.href = "../../Coberturas";
            }, 500);
        } else if (res == "repe") {
            await toastr.info("La ciudad ya existe!", msgOp_right());
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