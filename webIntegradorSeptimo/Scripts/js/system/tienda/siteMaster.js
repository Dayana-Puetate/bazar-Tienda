    seleccionMenu();
    dropDownCategoria();
    pie();
    sitePlusMinus();
    siteMagnificPopup();
    searchShow();
    siteSliderRange();
    masterSesion();
    masterActualizarCarrito();

    

AOS.init({
    duration: 800,
    easing: 'slide',
    once: true
});




function siteMenuClone () {

    $('<div class="site-mobile-menu"></div>').prependTo('.site-wrap');

    $('<div class="site-mobile-menu-header"></div>').prependTo('.site-mobile-menu');
    $('<div class="site-mobile-menu-close "></div>').prependTo('.site-mobile-menu-header');
    $('<div class="site-mobile-menu-logo"></div>').prependTo('.site-mobile-menu-header');

    $('<div class="site-mobile-menu-body"></div>').appendTo('.site-mobile-menu');

    $('.js-logo-clone').clone().appendTo('.site-mobile-menu-logo');
    $('h3[class*="js-logo-clone"]').remove();

    $('<span class="ion-ios-close js-menu-toggle"></div>').prependTo('.site-mobile-menu-close');


    $('.js-clone-nav').each(function () {
        var $this = $(this);
        $this.clone().attr('class', 'site-nav-wrap').appendTo('.site-mobile-menu-body');
    });


    setTimeout(function () {

        var counter = 0;
        $('.site-mobile-menu .has-children').each(function () {
            var $this = $(this);

            $this.prepend('<span class="arrow-collapse collapsed">');

            $this.find('.arrow-collapse').attr({
                'data-toggle': 'collapse',
                'data-target': '#collapseItem' + counter,
            });

            $this.find('> ul').attr({
                'class': 'collapse',
                'id': 'collapseItem' + counter,
            });

            counter++;

        });

    }, 1000);

    $('body').on('click', '.arrow-collapse', function (e) {
        var $this = $(this);
        if ($this.closest('li').find('.collapse').hasClass('show')) {
            $this.removeClass('active');
        } else {
            $this.addClass('active');
        }
        e.preventDefault();

    });

    $(window).resize(function () {
        var $this = $(this),
            w = $this.width();

        if (w > 768) {
            if ($('body').hasClass('offcanvas-menu')) {
                $('body').removeClass('offcanvas-menu');
            }
        }
    })

    $('body').on('click', '.js-menu-toggle', function (e) {
        var $this = $(this);
        e.preventDefault();

        if ($('body').hasClass('offcanvas-menu')) {
            $('body').removeClass('offcanvas-menu');
            $this.removeClass('active');
        } else {
            $('body').addClass('offcanvas-menu');
            $this.addClass('active');
        }
    })

    $(document).mouseup(function (e) {
        var container = $(".site-mobile-menu");
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            if ($('body').hasClass('offcanvas-menu')) {
                $('body').removeClass('offcanvas-menu');
            }
        }
    });
};



function sitePlusMinus() {
    $('.js-btn-minus').on('click', function (e) {
        e.preventDefault();
        if ($(this).closest('.input-group').find('.form-control').val() != 0) {
            $(this).closest('.input-group').find('.form-control').val(parseInt($(this).closest('.input-group').find('.form-control').val()) - 1);
        } else {
            $(this).closest('.input-group').find('.form-control').val(parseInt(0));
        }
    });
    $('.js-btn-plus').on('click', function (e) {
        e.preventDefault();
        $(this).closest('.input-group').find('.form-control').val(parseInt($(this).closest('.input-group').find('.form-control').val()) + 1);
    });
};


function siteSliderRange() {
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function (event, ui) {
            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount").val("$" + $("#slider-range").slider("values", 0) +
        " - $" + $("#slider-range").slider("values", 1));
};


function siteMagnificPopup() {
    $('.image-popup').magnificPopup({
        type: 'image',
        closeOnContentClick: true,
        closeBtnInside: false,
        fixedContentPos: true,
        mainClass: 'mfp-no-margins mfp-with-zoom', // class to remove default margin from left and right side
        gallery: {
            enabled: true,
            navigateByImgClick: true,
            preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
        },
        image: {
            verticalFit: true
        },
        zoom: {
            enabled: true,
            duration: 300 // don't foget to change the duration also in CSS
        }
    });

    $('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
        disableOn: 700,
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,

        fixedContentPos: false
    });
};

function searchShow() {
    // alert();
    var searchWrap = $('.search-wrap');
    $('.js-search-open').on('click', function (e) {
        e.preventDefault();
        searchWrap.addClass('active');
        setTimeout(function () {
            searchWrap.find('.form-control').focus();
        }, 300);
    });
    $('.js-search-close').on('click', function (e) {
         e.preventDefault();
        searchWrap.removeClass('active');
    })
};

async function pie() {
    const ctrl = new Controladores();
    let resl = await GetJson(ctrl.configuraciones + 'unDato');
    if (resl != "") {
        let dtl = JSON.parse(resl);
        $("#pieAbout").text(dtl.about);
        $("#pieDireccion").text(dtl.direccion);
        $("#pieTelefono").text(dtl.telefono);
        $("#pieTelefono").attr("href", "tel://" + dtl.telefono)
    }
}

function seleccionMenu() {
    let dirMaster = window.location.href;
    if (dirMaster.includes("Tienda") == false) {
        $("li[class*='dirHome']").addClass('active');
    } else {
        $("a[id='dirProducto']").addClass('text-primary');
    }
}

async function dropDownCategoria() {
    const ctrl = new Controladores();
    let resc = await GetString(ctrl.productos + 'dropdownCategorias');
    if (resc != "") {
        $("#categoriasDropdown").html(resc);
        siteMenuClone();
    }
}

async function masterSesion() {
    const ctrl = new Controladores();
    let resc = await GetString(ctrl.home + 'verificarSesion');
    if (resc == "False") {
        $("#masterDropdownSesion").removeClass("d-inline-block");
        $("#masterDropdownSesion").attr("hidden", true);
        $("#bannerAcceder").attr("hidden", false);
        
    } else {
        $("#masterAccederSesion").removeClass("d-inline-block");
        $("#masterAccederSesion").attr("hidden", true);
    }
}

$("#txtBusquedaMaster").on("keyup", function (e) {
    if (e.keyCode === 13) {
        if ($("#txtBusquedaMaster").val().trim() == "") {
            $(".search-wrap").removeClass("active");
        } else {
            window.location.href = "../../Tienda/Busqueda/?search=" + $("#txtBusquedaMaster").val();

        }
    }
});

function masterActualizarCarrito(producto) {
    let anterior = window.localStorage.getItem("carrito");
    if (anterior==null) {
            window.localStorage.setItem("carrito", []);
            $("#masterItemsCarrito").text("0");
            return true;
    }
    let masterArreglo = new Array();
    let carritoVeri = window.localStorage.getItem("carrito");
    let carrito = new Array();
    if (carritoVeri != "") {
        let contenido = JSON.parse(window.localStorage.getItem("carrito"));
        carrito.push(...contenido);
    }
    masterArreglo.push(...carrito);
    $("#masterItemsCarrito").text(masterArreglo.length.toString());

    if (producto == null || producto==undefined) {
        return false;
    }



    let pr = JSON.parse(JSON.stringify(producto));
    let veri = false;
    if (masterArreglo.length > 0) {
        masterArreglo.forEach(function (item) {
            let jsonItem = JSON.parse(JSON.stringify(item));
            if (pr.idProducto == jsonItem.idProducto) {
                veri = true;
            } 
        });
    }
    if (veri == false) {
        masterArreglo.push(pr);
        window.localStorage.setItem("carrito", JSON.stringify(masterArreglo));
        $("#masterItemsCarrito").text(masterArreglo.length.toString());
        return true;
    } else {
        toastr.info("El producto ya se encuentra en el carrito de compras", msgOp_center());
        return false;
    }



}

function masterLimpiarCarrito() {
    window.localStorage.clear();
}

async function cerrarSesion() {
    try {
        let ctrl = new Controladores();
        let res = await GetString(ctrl.home + 'salir');
        if (res == "ok") {
            window.top.location.href = "../../Home";
        }

    } catch (e) {
        console.log(e);
    }
}