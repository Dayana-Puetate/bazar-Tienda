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
    var busqueda = new URL(window.location.href).searchParams.get("search");
    if (busqueda == undefined || busqueda == "" || busqueda == null) {
        window.location.href = "../../Home";
        return;
    }
    $("#contenedorMaestro").attr("hidden", true);
    $("#espereMaestro").attr("hidden",false);
    await buscarProductos(busqueda);
    $("#espereMaestro").attr("hidden", true);
    $("#contenedorMaestro").attr("hidden", false);


}

async function buscarProductos(id) {

    try {
        let ctrl = new Controladores();
        let res = await PostIdResString(ctrl.productos + "buscarProductos/", id); 
        if (res == "") {
            $("#contenedorBusqueda").html("<div class='w-100 text-center text-danger'><h3>No se encontraron resultados para </br><small class='text-muted font-italic'>''" + id + "''</small></h3><h1 class='pb-3'><i class='fa fa-exclamation-circle fa-3x'></i></h1><h4 class='text-muted'>Estamos actualizando nuestro catálogo de productos constantemente</h4></div>");
        } else {
            $("#contenedorBusqueda").html(res);
        }


    } catch (e) {

    }

}