function menus() {

    try {
        if (window.innerWidth < 769) {
            $("ul[class*='sidebar-menu']").css("display", "none");
        }
        if (window.location.href.search('Bienvenida') != -1) {
            $("a[href*='Bienvenida']").addClass("active");
            return false;
        }
        var location = window.location.href.split('/')[3];
        $("a[href*='" + location + "']").each(function () {
            $($(this).parent().get(0)).addClass("active");
            $($(this).parent().parent().get(0)).css("display", "block");
            $($(this).parent().parent().parent().children().get(0)).addClass("active");
        });
        } catch (e) {
        console.log(e);
    }


}