window.onload = function () {
    start();
}

function paginar(id, elm) {
    $("div[class*='tabCategoria']").each(function () {
        this.hidden=true;
    });
    $("li[class*='paginadores']").removeClass("active");
    $(elm).addClass("active");
    $(id).removeAttr("hidden");
    window.scrollTo(0, 0);
}

async function start() {
    var id = new URL(window.location.href).searchParams.get("id");
    if (id == undefined || id == "" || id == null) {
        window.location.href = "../../Home";
    }
    $("#contenedorMaestro").attr("hidden", true);
    $("#espereMaestro").attr("hidden",false);
    await cargarCategorias(id);
    $("#espereMaestro").attr("hidden", true);
    $("#contenedorMaestro").attr("hidden", false);


}

async function cargarCategorias(id) {

    try {
        let ctrl = new Controladores();
        let res = await PostIdResString(ctrl.productos + "listarProductosCategorias/", id); 
        if (res == "") {
            window.location.href = "../../Home";
        } else {
            $("#contenedorCategoria").html(res);
        }


    } catch (e) {
        console.log(e);
        window.location.href = "../../Home";
    }

}