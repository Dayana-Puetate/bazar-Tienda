async function salir() {
    try {
            const con = new Controladores();

            const res = await GetString(con.administrador + "salir");
            console.log(res);
        if (res == "ok") {
            window.top.location.reload();
        }
    } catch (e) {
        console.log(e);
    }

}


