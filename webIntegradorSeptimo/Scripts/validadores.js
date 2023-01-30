$("input[class*='validarVacio']").keyup(function () {
    var id = $(this).attr('id');
    validarVacio(id);
});

$("input[class*='validarCedula']").keyup(function () {
    var id = $(this).attr('id');
    validarCedula(id);
});


$("input[class*='validarCorreo']").keyup(function () {
    var id = $(this).attr('id');
    validarCorreo(id);
});


$("input[class*='validarIntCero']").keyup(function () {
    var id = $(this).attr('id');
    validarIntegerCero(id);
});

$("input[class*='validarIva']").keyup(function () {
    var id = $(this).attr('id');
    validarIva(id);
});

$("input[class*='validarInt']").keyup(function () {
    var id = $(this).attr('id');
    validarInteger(id);
});

$("input[class*='validarIntPorcentaje']").keyup(function () {
    var id = $(this).attr('id');
    validarIntegerPorcentaje(id);
});


$("input[class*='validarDecimal']").keyup(function () {

    var id = $(this).attr('id');
    validarDecimal(id);
});


$("textarea[class*='validarVacio']").keyup(function () {
    var id = $(this).attr('id');
    validarVacio(id);
});

$("select[class*='validarVacio']").change(function () {
    var id = $(this).attr('id');
    validarVacio(id);
});


async function validarVacio(id) {
    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "Val";
        if ($(obj).val().trim() == "" || $(obj).val() == null || $(obj).val() == undefined) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else {
            $(msg).hide();
            $(obj).removeClass("is-invalid");
            res = true;

        }

        return res;
    } catch (e) {
        $(obj).addClass("is-invalid");
        $(msg).removeAttr("hidden");
        $(msg).show();
        return false;
    }

}








async function validarVacioTodos(id) {
    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "Val";
        if ($(obj).val().trim() == "" || $(obj).val() == null || $(obj).val()==undefined) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else {
            $(msg).hide();
            res = true;

        }

        return res;
    } catch (e) {
        $(obj).addClass("is-invalid");
        $(msg).removeAttr("hidden");
        $(msg).show();
        return false;
    }

}



async function validarCedula(id) {

    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "CedVal";
        var msgv = "#" + id.toString() + "Val";

        if ($(obj).val().trim() == "") {
            $(msg).hide();
            validarVacio(id);
            return true;
        }
        const veri = await VerificaIdentificacion($(obj).val());
        if (veri == false) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else {
            $(msg).hide();
            $(msgv).hide()
            $(obj).removeClass("is-invalid");
            res = true;
        }
        return res;
    } catch (e) {
        console.log(e);
    }

}


async function validarCorreo(id) {
    
    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "CorVal";
        var msgv = "#" + id.toString() + "Val";

        if ($(obj).val().trim() == "") {
            $(msg).hide();
            validarVacio(id);
            return true;
        }
        const veri = await verificarCorreo($(obj).val());
        if (veri == false) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else {
            $(msg).hide();
            $(msgv).hide()
            $(obj).removeClass("is-invalid");
            res = true;
        }
        return res;
    } catch (e) {
        console.log(e);
    }


}



async function validarDecimal(id) {
    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "Val";

        if ($(obj).val().trim() == "") {
            validarVacio(id);
            return true;
        }

        if (parseFloat($(obj).val())<=0) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if ($(obj).val().split(".").length>2) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if (($(obj).val().split(",").length > 2)) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        }else {
            $(msg).hide();
            $(obj).removeClass("is-invalid");
            res = true;
        }
        return res;
    } catch (e) {
        console.log(e);
    }


}



async function validarIntegerCero(id) {
    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "Val";

        if ($(obj).val().trim() == "") {
            validarVacio(id);
            return true;
        }

        if (parseInt($(obj).val()) < 0) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if ($(obj).val().split(".").length > 1) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if (($(obj).val().split(",").length > 1)) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else {
            $(msg).hide();
            $(obj).removeClass("is-invalid");
            res = true;
        }
        return res;
    } catch (e) {
        console.log(e);
    }


}


async function validarIva(id) {
    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "Val";

        if ($(obj).val().trim() == "") {
            validarVacio(id);
            return true;
        }

        if (parseInt($(obj).val()) < 0 || parseInt($(obj).val())>20) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if ($(obj).val().split(".").length > 1) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if (($(obj).val().split(",").length > 1)) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else {
            $(msg).hide();
            $(obj).removeClass("is-invalid");
            res = true;
        }
        return res;
    } catch (e) {
        console.log(e);
    }


}

async function validarInteger(id) {
    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "Val";

        if ($(obj).val().trim() == "") {
            validarVacio(id);
            return true;
        }

        if (parseFloat($(obj).val()) <= 0) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if ($(obj).val().split(".").length > 1) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if (($(obj).val().split(",").length > 1)) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else {
            $(msg).hide();
            $(obj).removeClass("is-invalid");
            res = true;
        }
        return res;
    } catch (e) {
        console.log(e);
    }


}


async function validarIntegerPorcentaje(id) {
    let res;
    try {
        var obj = "#" + id.toString();
        var msg = "#" + id.toString() + "Val";

        if ($(obj).val().trim() == "") {
            validarVacio(id);
            return true;
        }

        if (parseFloat($(obj).val()) <= 0 || parseFloat($(obj).val()) >90) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if ($(obj).val().split(".").length > 1) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else if (($(obj).val().split(",").length > 1)) {
            $(obj).addClass("is-invalid");
            $(msg).removeAttr("hidden");
            $(msg).show();
            res = false;
        } else {
            $(msg).hide();
            $(obj).removeClass("is-invalid");
            res = true;
        }
        return res;
    } catch (e) {
        console.log(e);
    }


}






async function validarTodo() {
    let res;
    try {

        await $("input[class*='validarCorreo']").each(async function (i) {
            var el = $("input[class*='validarCorreo']").get(i);
            const x = await validarCorreo($(el).attr("id"));
            if (x == false) {
                res = false;
            }
        });


        await $("input[class*='validarCedula']").each(async function (i) {
            setTimeout(function () {console.log("") }, 500);
            var el = $("input[class*='validarCedula']").get(i);
            const x = await validarCedula($(el).attr("id"));
            if (x == false) {
                res = false;
            }
        });



        await $("input[class*='validarVacio']").each(async function (i) {
            var el = $("input[class*='validarVacio']").get(i);
            const x = await validarVacioTodos($(el).attr("id"));
            if (x == false) {
                res = false;
            }
        });


        await $("textarea[class*='validarVacio']").each(async function (i) {
            var el = $("textarea[class*='validarVacio']").get(i);
            const x = await validarVacioTodos($(el).attr("id"));
            if (x == false) {
                res = false;
            }
        });

        await $("select[class*='validarVacio']").each(async function (i) {
            var el = $("select[class*='validarVacio']").get(i);
            const x = await validarVacioTodos($(el).attr("id"));
            if (x == false) {
                res = false;
            }
        });

        await $("input[class*='validarIntCero']").each(async function (i) {
            var el = $("input[class*='validarIntCero']").get(i);
            const x = await validarIntegerCero($(el).attr("id"));
            if (x == false) {
                res = false;
            }
        });

        await $("input[class*='validarIva']").each(async function (i) {
            var el = $("input[class*='validarIva']").get(i);
            const x = await validarIva($(el).attr("id"));
            if (x == false) {
                res = false;
            }
        });

        await $("input[class*='validarInt']").each(async function (i) {
            setTimeout(function () { console.log("") }, 500);
        var el = $("input[class*='validarInt']").get(i);
        const x = await validarInteger($(el).attr("id"));
        if (x == false) {
            res = false;
        }
    });


        await $("input[class*='validarIntPorcentaje']").each(async function (i) {
            setTimeout(function () { console.log("") }, 500);
            var el = $("input[class*='validarIntPorcentaje']").get(i);
            const x = await validarIntegerPorcentaje($(el).attr("id"));
            if (x == false) {
                res = false;
            }
        });



    await $("input[class*='validarDecimal']").each(async function (i) {
        setTimeout(function () { console.log("")}, 500);
        var el = $("input[class*='validarDecimal']").get(i);
        const x = await validarDecimal($(el).attr("id"));
        if (x == false) {
            res = false;
        }
    });




    } catch (e) {
        console.log("parametros:" + e.toString());
    }
    return res;
}










async function todoVacio() {
    let res;
    try {

        await $("input[class*='validarVacio']").each(async function (i) {
            var el = $("input[class*='validarVacio']").get(i);

            const x = await validarVacio($(el).attr("id"));
            if (x == false) {
                res = false;
            }

        });


    } catch (e) {
        console.log(e);
    }
    return res;
}

async function verificarCorreo(valor) {
    let res;
    const emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    try {
        res = await emailRegex.test(valor);
        return res;
    } catch (e) {
        console.log(e);
    }


}


async function VerificaIdentificacion(identificacion)
{
    //console.log(identificacion);

    let estado = false;
    try {
        var valced = "";
        var provincia = 0;
        if (identificacion.toString().length >= 10) {

            valced = identificacion.toString().trim();
            provincia = parseInt((valced[0].toString() + valced[1].toString()));
            if (provincia > 0 && provincia < 25) {
                if (parseInt(valced[2].toString()) < 6) {
                    estado = await VerificaCedula(valced);
                }
                else if (parseInt(valced[2].toString()) == 6) {
                    estado = await VerificaSectorPublico(valced);
                }
                else if (parseInt(valced[2].toString()) == 9) {
                    estado = await VerificaPersonaJuridica(valced);
                }
            }
        }
        return estado;
    } catch (e) {
        console.log("VerificarIdentificación: "+e);
    }

}





async function VerificaCedula(validarCedula)
{

    if (validarCedula.toString().trim().includes('2222222222')) {
        return false;
    }

    if (validarCedula.toString().trim().includes('1800000000')) {
        return false;
    }

 

    let res = false;
    try {
        var aux = 0;
        var par = 0;
        var impar = 0;
        var verifi = 0;
        for (var i = 0; i < 9; i += 2) {
            aux = 2 * parseInt(validarCedula[i].toString());
            if (aux > 9)
                aux -= 9;
            par += aux;
        }
        for (var i = 1; i < 9; i += 2) {
            impar += parseInt(validarCedula[i].toString());
        }

        aux = par + impar;
        if (aux % 10 != 0) {
            verifi = 10 - (aux % 10);
        }
        else
            verifi = 0;
        if (verifi == parseInt(validarCedula[9].toString())) {
            res=true;
        } else {
            res=false;
        }

        return res;


    } catch (e) {
        console.log('Verificar cedula: '+e);
    }    


}

async function VerificaPersonaJuridica(validarCedula)
{
    if (validarCedula.toString().trim() == '2222222222') {
        return false;
    }

    let res;
    try {
        var aux = 0;
        var prod = 0;
        var veri = 0;
        veri = parseInt(validarCedula[10].toString()) + parseInt(validarCedula[11].toString()) + parseInt(validarCedula[12].toString());
        if (veri > 0) {
            var coeficiente = [4, 3, 2, 7, 6, 5, 4, 3, 2];
            for (var i = 0; i < 9; i++) {
                prod = parseInt(validarCedula[i].toString()) * coeficiente[i];
                aux += prod;
            }
            if (aux % 11 == 0) {
                veri = 0;
            }
            else if (aux % 11 == 1) {
                res=false;
            }
            else {
                aux = aux % 11;
                veri = 11 - aux;
            }

            if (veri == parseInt(validarCedula[9].toString())) {
                res:true;
            }
            else {
                res:false;
            }
        }
        else {
            res:false;
        }

        return res;
    } catch (e) {
        Console.lgo("VerificaPersonaJuridica: " + e);
    }
    
}


async function VerificaSectorPublico(validarCedula)
{
    let res;
    try {

        var aux = 0;
        var prod = 0;
        var veri = 0;
        veri = parseInt(validarCedula[9].toString()) + parseInt(validarCedula[10].toString()) + parseInt(validarCedula[11].toString()) + parseInt(validarCedula[12].toString());
        if (veri > 0) {
            var coeficiente = [3, 2, 7, 6, 5, 4, 3, 2];

            for (var i = 0; i < 8; i++) {
                prod = parseInt(validarCedula[i].toString()) * coeficiente[i];
                aux += prod;
            }

            if (aux % 11 == 0) {
                veri = 0;
            }
            else if (aux % 11 == 1) {
                res=false;
            }
            else {
                aux = aux % 11;
                veri = 11 - aux;
            }

            if (veri == parseInt(validarCedula[8].toString())) {
                res=true;
            }
            else {
                res=false;
            }
        }
        else {
            res=false;
        }

        return res;

    } catch (e) {
        console.log("VerificaSectorPublico: " + e);
    }
    
}

function convertirFecha(fecha) {

    try {
        var res = fecha.replace(/\/Date\((-?\d+)\)\//, '$1');
        var f1 = new Date(parseInt(res));
        return f1.toISOString().split("T")[0];

    } catch (e) {
        console.log(e);
    }
}








