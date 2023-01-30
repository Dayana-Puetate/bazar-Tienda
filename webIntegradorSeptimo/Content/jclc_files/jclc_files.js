var jclc_array = new Array();

$('input[type="file"]').change(function (event) {
    if (event.target.files[0] == undefined) {
        event.target.value = null;
        return;
    }

    if (!jclc_max_files(100)) {
        event.target.value = null;
        return
    }

    if (!jclc_exist_files(event.target.files[0].name)) {
        event.target.value = null;
        return
    }



    if (!/^image/.test(event.target.files[0].type)) {
        toastr.error("El archivo seleccionado no es una imagen",msgOp_right());
        event.target.value = null;
        return;
    }
    var cont = jclc_array.length + 1;
    jclc_array.push({
        id: 'jclc_img_ADDED' + cont + '_val',
        file: event.target.files[0]
    });
    event.target.value = null;
    jclc_add_event(jclc_array[jclc_array.length - 1], cont);
    jclc_view_advice();
});



async function jclc_add_event(evento,id) {
    var file = evento.file;
    var fileId = id;
    const img = await jclc_readFile(file);
    var removeLink = "<a title='Eliminar' class=\"btn-cerrar\"  onclick='jclc_quitar_preview("+fileId+")'></a>";
    var preview = "";
    preview = "<div class='card m-2 shadow card-img-preview' style='width:10em;height:12em;' id='jclc_img_preview" + fileId + "'>" +
        "<div class='card-body p-0 bg-light'>" +
        "<div class='w-100 text-right pr-2'>" +
        removeLink +
        "</div>" +
        "<div class='w-100 h-100 text-center'>" +
        "<img class='almedio' style='width:75px;height:75px' src=\"" + img + "\"> " +
        "</div>" +
        "</div>" +
        "<div class='card-footer w-100 text-white bg-dark text-center' style='overflow-y:auto;max-height:5em;'>" +
        "<strong style='font-size:0.9em'>" + escape(file.name) + " </strong> </br> <strong class='texto-verde' style='font-size:0.6em'>" + (((file.size) / 1024) / 1024).toFixed(2) + " MB  </stong>" +
        "</div>" +
        "</div>";

    $("div[class*='jclc_img']").append(preview);
}


function jclc_readFile(file) {
    var reader = new FileReader();
    var deferred = $.Deferred();

    reader.onload = function (event) {
        deferred.resolve(event.target.result);
    };

    reader.onerror = function () {
        deferred.reject(this);
    };

    if (/^image/.test(file.type)) {
        reader.readAsDataURL(file);
    } else {
        reader.readAsText(file);
    }

    return deferred.promise();
};



function jclc_quitar_preview(id) {
    $("#jclc_img_preview" + id).remove();
    jclc_array.splice(id - 1, 1);
    jclc_view_advice();
}


async function jclc_add(fileUrl, size) {
    var removeLink = "<a title='Eliminar' class=\"btn-cerrar\"  onclick='jclc_quitar_preview_cargado(\"" + fileUrl.split("/").pop().replaceAll(" ", "").replace(/[^\w\s]/gi, '') + "\",\"" + fileUrl.split("/").pop()+"\")'></a>";
    var preview = "";
    preview = "<div class='card m-2 shadow card-img-preview' style='width:10em;height:12em;' id='" + fileUrl.split("/").pop().replaceAll(" ", "").replace(/[^\w\s]/gi, '') + "'>" +
        "<div class='card-body p-0 bg-light'>" +
        "<div class='w-100 text-right pr-2'>" +
        removeLink +
        "</div>" +
        "<div class='w-100 h-100 text-center'>" +
        "<img class='almedio' style='width:75px;height:75px' src=\"" + fileUrl + "\"> " +
        "</div>" +
        "</div>" +
        "<div class='card-footer bg-dark text-center text-white' style='overflow-y:auto;max-height:5em;'>" +
        "<strong style='font-size:0.9em'>" + fileUrl.split('/').pop() + " </strong> </br> <strong class='texto-verde' style='font-size:0.6em'>" + (((parseFloat(size)) / 1024) / 1024).toFixed(2) + " MB  </stong>" +
        "</div>" +
        "</div>";
    $("div[class*='jclc_img']").append(preview);
    jclc_view_advice_oncharge();
}



function jclc_quitar_preview_cargado(id,url) {
    var rem = "#" + id;
    $(rem).remove();
    jclc_view_advice_oncharge();
    jclc_auxiliar_vector(url);
}

function jclc_auxiliar_vector(img) {
    try {
        var input = document.getElementById("vectorEliminarImagenes");
        input.value = input.value + img + "$7@(10";
    } catch (e) {
        console.log("No se a declarado el input auxiliar necesario");
    }
}






function jclc_view_advice() {
    if (jclc_array.length == 0) {
        $("div[class*=jclc_advice]").show();
    } else {
        $("div[class*=jclc_advice]").hide();
    }

    if ($("div[class*='card-img-preview']").length > 0) {
        $("div[class*=jclc_advice]").hide();
    } 
}

function jclc_view_advice_oncharge() {
    if ($("div[class*='card-img-preview']").length > 0) {
        $("div[class*=jclc_advice]").hide();
    } else {
        $("div[class*=jclc_advice]").show();
    }
}



function jclc_max_files(number) {
    try {
        if (jclc_array.length >= number) {
            toastr.info("El máximo de archivos permitidos es : " + number,msgOp_right());
            return false;
        } else {
            return true;
        }
    } catch (e) {
        console.log(e);
    }
}


function jclc_min_files(number) {
    try {
        if ($("div[class*='card-img-preview']").length < number) {
            return false;
        } else {
            return true;
        }
    } catch (e) {
        console.log(e);
    }
}



function jclc_exist_files(file) {
    try {
        let res = true;
        jclc_array.forEach(function (value) {
            if (value.file.name == file) {
                toastr.info("El archivo ya ha sido agregado",msgOp_right());
                res = false;
            }
        });
        return res;
    } catch (e) {
        console.log(e);
    }
}



//function PostResString(url, form) {
//    return new Promise(function (respuesta, error) {
//        let xhr = new XMLHttpRequest();
//        xhr.open('POST', url + '/');
//        xhr.onload = function () {
//            if (this.status >= 200 && this.status < 300) {
//                respuesta(xhr.responseText);
//            } else {
//                error({
//                    estado: this.status,
//                    texto: xhr.statusText
//                });
//            }
//        };
//        xhr.onerror = function () {
//            error({
//                estado: this.status,
//                texto: xhr.statusText
//            });
//        };
//        xhr.send(form);
//    });
//}

//async function subir() {
//    var frm = new FormData();
//    jclc_array.forEach(function (value) {
//        frm.append("files", value.file);
//    });

//    const res = await PostResString("../../Home/Guardar", frm);
//    alert(res);

//}