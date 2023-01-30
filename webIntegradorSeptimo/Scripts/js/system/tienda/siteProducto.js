var pagaEnvio = false;
var idOferta = "0";
var idProducto = "0";
var srcImagenReferencia = "";
var descuentoPorcentaje = "0";
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
    try {
        var id = new URL(window.location.href).searchParams.get("id");
        if (id == undefined || id == "" || id == null) {
            window.location.href = "../../Home";
        }
        $("#contenedorMaestro").attr("hidden", true);
        $("#espereMaestro").attr("hidden", false);
        await cargarDatos(id);
        $("#espereMaestro").attr("hidden", true);
        $("#contenedorMaestro").attr("hidden", false);
    } catch (e) {
        console.log(e);
    }



}

async function cargarDatos(id) {

    try {
        let ctrl = new Controladores();
        let res = await PostIdResJson(ctrl.productos + "unDato", id); 
        if (res == "") {
            window.location.href = "../../Home";
        } else {
            let datos = JSON.parse(res);
            idProducto = datos.idProducto;
            $("#categoria").text(datos.categoria.nombre)
            $("#nombre").text(datos.nombre);
            $("#descripcion").text(datos.descripcion);
            $("#btnMas").html("<button class='btn btn-outline-primary js-btn-minus' id='btnMasMas' onclick='maxValueInput(\""+datos.stock+"\")' type='button'  >&plus;</button>");
            $("#btnMenos").html("<button class='btn btn-outline-primary js-btn-minus' id='btnMenosMenos' onclick='minValueInput()' type='button'  >&minus;</button>");
            $("#stock").text(datos.stock);
            $("#cantidad").attr("max",datos.stock)
            $("#valorIva").text(datos.iva);
            let imagenes = await PostIdResString(ctrl.productos + "sliderImagenesProducto", id);
            $("#carouselExampleIndicators").html(imagenes);
            srcImagenReferencia = $("#imagenPrincipalReferencia").attr("src");
            let oferta = await PostIdResJson(ctrl.productos + "ofertaProducto", id);
            if (oferta != "") {
                let datosOferta = JSON.parse(oferta);
                idOferta = datosOferta.idOferta;
                descuentoPorcentaje = datosOferta.porcentaje;
                var descuento = parseInt(datosOferta.porcentaje) / 100;
                var final = (1 - descuento)*parseFloat(datos.pvp);
                $("#precio").html("<del>$" + datos.pvp +"</del> <strong class='text-primary h4'>$<label id='precioFinal'>"+Math.round(final*100)/100+"</label></strong>");
            } else {
                $("#precio").html("<strong class='text-primary h4'>$<label id='precioFinal'>"+datos.pvp+"</label></strong>");
            }
           pagaEnvio = datos.paga_envio;
            calcularTodo();
        }


    } catch (e) {
        console.log(e);
        window.location.href = "../../Home";
    }

}

function calcularEnvio() {
    var cantidad = parseInt($("#cantidad").val());
    var precio = parseFloat($("#precioFinal").text());
    var subtotal = cantidad * precio;
    var descuento = subtotal * 0.075;
    $("#costoEnvio").text(Math.round(descuento * 100) / 100);

}

function calcularIva() {
    var cantidad = parseInt($("#cantidad").val());
    var precio = parseFloat($("#precioFinal").text());
    var iva = parseInt($("#valorIva").text());
    var subtotal = cantidad * precio;
    var descuento = subtotal * (iva/100);
    $("#costoIva").text(Math.round(descuento * 100) / 100);

}

function maxValueInput(max) {
    if (parseInt($("#cantidad").val()) == max) {
        return false;
    } else {
        var n1 = parseInt($("#cantidad").val())+1;   
        $("#cantidad").val(n1.toString());
        calcularTodo();
    }
}



function minValueInput() {
    if (parseInt($("#cantidad").val()) == 1) {
        return false;
    } else {
        $("#cantidad").val(parseInt($("#cantidad").val() - 1));
        calcularTodo();
    }

}

function calcularTodo() {
    if (pagaEnvio) {
        calcularEnvio();
    }
    calcularIva();
    var iva = parseFloat($("#costoIva").text());
    var envio = parseFloat($("#costoEnvio").text());
    var cantidad = parseInt($("#cantidad").val());
    var precio = parseFloat($("#precioFinal").text());
    var subtotal = cantidad * precio;
    $("#valorSubtotal").text(Math.round(subtotal * 100) / 100);
    var total = iva + envio + subtotal;
    $("#valorTotal").text(Math.round(total * 100) / 100);
}

async function agregarCarrito() {
    let cantidad = parseInt($("#cantidad").val());
    let precio = parseFloat($("#precioFinal").text());
    var iva = parseInt($("#valorIva").text());
    let item = {
        "idProducto": idProducto,
        "nombre": $("#nombre").text(),
        "descuento":descuentoPorcentaje,
        "precio": precio,
        "iva": iva,
        "pagaEnvio":pagaEnvio.toString(),
        "stock": $("#stock").text(),
        "costoEnvio": $("#costoEnvio").text(),
        "costoIva": $("#costoIva").text(),
        "valorTotal": $("#valorTotal").text(),
        "subtotal": $("#valorSubtotal").text(),
        "cantidad": cantidad,
        "idOferta": idOferta,
        "idPedido": 0,
        "srcImagen":srcImagenReferencia
    }
    const res = await masterActualizarCarrito(item);
    if (res) {
        toastr.success("Producto agregado al carrito exitosamente", msgOp_right());
        setTimeout(function () {
            window.location.href = "../../Home";
        }, 500);

    }


}


