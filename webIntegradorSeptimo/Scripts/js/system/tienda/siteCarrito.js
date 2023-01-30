var pagaenvio = false;
var idoferta = "0";
var idproducto = "0";
var srcimagenreferencia = "";
var descuentoporcentaje = "0";
window.onload = function () {
    start();
}

async function start() {
    try {
        $("#contenedorMaestro").attr("hidden", true);
        $("#espereMaestro").attr("hidden", false);
        let carVeri = window.localStorage.getItem("carrito");
        if (carVeri == "" || carVeri=="[]") {
            $("#contenedorCarritoCarrito").html("<div class='w-100 text-center text-primary'><h3>Aun no has agregado ningun producto a tu carrito </br><small class='text-muted font-italic'></small></h3><h1 class='pb-3'><i class='fa fa-cart-arrow-down fa-3x'></i></h1><h4 class='text-muted pb-5'>Agrega productos y vuele</h4><div class='text-center'> <a class='btn btn-outline-primary btn-md' href='../../Home' >Seguir Comprando</a ></div></div>");
            $("#espereMaestro").attr("hidden", true);
            $("#contenedorMaestro").attr("hidden", false);
            return false;
        }

        let arrayCarrito = new Array();
        arrayCarrito.push(...JSON.parse(carVeri));
        let html = "";
        arrayCarrito.forEach(function (item, i) {
            html += "<tr id='articuloCarrito"+i+"'>" +
                "<td class='product-thumbnail'>" +
                "<img src='"+item.srcImagen+"' class='rounded-circle img-fluid shadow' style='width:70px;height:70px' alt='Image' >" +
                "</td>" +
                "<td class='product-name' style='max-width:290px'>" +
                "<h2 class='h5 text-black'>"+item.nombre+"</h2>" +
                "</td>" +
                "<td>$"+item.precio+"</td>" +
                "<td>" +
                "<div class='input-group mb-3 ml-auto mr-auto' style='width: 140px;'>" +
                "<div class='input-group-prepend'>" +
                "<button class='btn btn-outline-primary js-btn-minus' type='button' onclick='minValueInput(\"" + i +"\")'>&minus;</button>" +
                "</div>" +
                "<input type='text' class='form-control text-center' id='cantidad"+i+"' value='"+item.cantidad+"'  " +
                "aria-label='Example text with button addon' aria-describedby='button-addon1' readonly>" +
                "<div class='input-group-append'>" +
                "<button class='btn btn-outline-primary js-btn-plus' type='button' onclick='maxValueInput(\"" + i + "\",\"" + item.stock +"\")'>&plus;</button>" +
                "</div>" +
                "</div>" +
                "</td>" +
                "<td>$<span id='articuloSubtotal"+i+"'>"+item.subtotal+"<span></td>" +
                "<td><button  class='btn btn-primary height-auto btn-sm' onclick='eliminarDeCarrito(\"" + i +"\")'><i class='fa fa-trash-o'></i></button></td>" +
                "</tr>";

        });  
        $("#cuerpoTablaCarrito").html(html);

        await realizarCalculos();


        $("#espereMaestro").attr("hidden", true);
        $("#contenedorMaestro").attr("hidden", false);
    } catch (e) {
        console.log(e);
/*        window.location.href = "../../Home";*/
        $("#espereMaestro").attr("hidden", true);
        $("#contenedorMaestro").attr("hidden", false);
    }


}


function maxValueInput(index,max) {
    let elemento = "#cantidad" + index;
    if (parseInt($(elemento).val()) == max) {
        return false;
    } else {
        var n1 = parseInt($(elemento).val())+1;   
        $(elemento).val(n1.toString());
    }

    let carVeri = window.localStorage.getItem("carrito");
    let arrayCarrito = new Array();
    arrayCarrito.push(...JSON.parse(carVeri));
    arrayCarrito.forEach(function (item, i) {
        if (i == index) {
            item.cantidad = parseInt($(elemento).val());
            let articuloSub = "#articuloSubtotal" + i;
            let resIva = 0;
            let resEnv = 0;
            let resSubtotal = 0;
            let resTotal = 0;
            resSubtotal = parseInt(item.cantidad) * parseFloat(item.precio);
            if (item.pagaEnvio == "true") {
                resEnv = resSubtotal * 0.075;
            }        
            resIva = resSubtotal * (item.iva / 100);
            resTotal = resIva + resEnv + resSubtotal;
            item.costoIva = Math.round(resIva * 100) / 100;
            item.subtotal = Math.round(resSubtotal * 100) / 100;
            item.costoEnvio = Math.round(resEnv * 100) / 100;
            item.valorTotal = Math.round(resTotal * 100) / 100;  
            $(articuloSub).text(item.subtotal);
        }
    });
    window.localStorage.setItem("carrito", JSON.stringify(arrayCarrito));
    realizarCalculos();

}



function minValueInput(index) {
    let elemento = "#cantidad" + index;
    if (parseInt($(elemento).val()) == 1) {
        return false;
    } else {
        $(elemento).val(parseInt($(elemento).val() - 1));
    }

    let carVeri = window.localStorage.getItem("carrito");
    let arrayCarrito = new Array();
    arrayCarrito.push(...JSON.parse(carVeri));
    arrayCarrito.forEach(function (item, i) {
        if (i == index) {
            item.cantidad = parseInt($(elemento).val());
            let articuloSub = "#articuloSubtotal" + i;
            let resIva = 0;
            let resEnv = 0;
            let resSubtotal = 0;
            let resTotal = 0;
            resSubtotal = parseInt(item.cantidad) * parseFloat(item.precio);
            if (item.pagaEnvio == "true") {
                resEnv = resSubtotal * 0.075;
            }
            resIva = resSubtotal * (item.iva / 100);
            resTotal = resIva + resEnv + resSubtotal;
            item.costoIva = Math.round(resIva * 100) / 100;
            item.subtotal = Math.round(resSubtotal * 100) / 100;
            item.costoEnvio = Math.round(resEnv * 100) / 100;
            item.valorTotal = Math.round(resTotal * 100) / 100;
            $(articuloSub).text(item.subtotal);
        }
    });
    window.localStorage.setItem("carrito", JSON.stringify(arrayCarrito));
    realizarCalculos();

}

function eliminarDeCarrito(index) {
    let elemento = "#articuloCarrito" + index;
    let carVeri = window.localStorage.getItem("carrito");
    let arrayCarrito = new Array();
    arrayCarrito.push(...JSON.parse(carVeri));
    arrayCarrito.splice(index, 1);
    window.localStorage.setItem("carrito", JSON.stringify(arrayCarrito));
    if (arrayCarrito.length == 0) {
        $("#contenedorCarritoCarrito").html("<div class='w-100 text-center text-primary'><h3>Aun no has agregado ningun producto a tu carrito </br><small class='text-muted font-italic'></small></h3><h1 class='pb-3'><i class='fa fa-cart-arrow-down fa-3x'></i></h1><h4 class='text-muted pb-5'>Agrega productos y vuele</h4><div class='text-center'> <a class='btn btn-outline-primary btn-md' href='../../Home' >Seguir Comprando</a ></div></div>");
    }
    masterActualizarCarrito();
    realizarCalculos();
    $(elemento).remove();

}


function realizarCalculos() {
    let carVeri = window.localStorage.getItem("carrito");
    let arrayCarrito = new Array();
    arrayCarrito.push(...JSON.parse(carVeri));
    let subtotal = 0;
    let total = 0;
    let iva = 0;
    let envio = 0;
    arrayCarrito.forEach(function (item, i) {
        subtotal = subtotal + parseFloat(item.subtotal);
        iva = iva + parseFloat(item.costoIva);
        envio = envio + parseFloat(item.costoEnvio);
        total = total + parseFloat(item.valorTotal);
    });
    $("#valorSubtotal").text(Math.round(subtotal * 100) / 100);
    $("#valorEnvio").text(Math.round(envio * 100) / 100);
    $("#valorIva").text(Math.round(iva * 100) / 100);
    $("#valorTotal").text(Math.round(total * 100) / 100);
}

async function realizarPedido(btn) {
    try {
        let ctrl = new Controladores();
        let verificaSesion = await GetString(ctrl.home + 'verificarSesion');

        if (verificaSesion != "True") {
            window.location.href = "../../Tienda/Registro";
            return false;
        }


        $(btn).text("ESPERE. . . ");
        $(btn).attr("disabled");

        let carVeri = window.localStorage.getItem("carrito");
        let arrayCarrito = new Array();
        arrayCarrito.push(...JSON.parse(carVeri));
        let form = new FormData();
        form.append("detalle", JSON.stringify(arrayCarrito));
        let guarda = await PostResString(ctrl.home + 'registrarPedido', form);
        if (guarda == "ok") {
            await masterLimpiarCarrito();
            await masterActualizarCarrito();
            await setTimeout(function () {
                location.href = "../../Tienda/Gracias";
            }, 300);
            $(btn).text("REALIZAR PEDIDO");
            $(btn).removeAttr("disabled");
        } else if(guarda==""){
            await toastr.error("Ha ocurrido un error al procesar su compra", msgOp_right());
            $(btn).text("REALIZAR PEDIDO");
            $(btn).removeAttr("disabled");
        }else{
            await toastr.info(guarda, msgOp_right());
            $(btn).text("REALIZAR PEDIDO");
            $(btn).removeAttr("disabled");
        }



    } catch (e) {
        console.log(e);
    }
}


