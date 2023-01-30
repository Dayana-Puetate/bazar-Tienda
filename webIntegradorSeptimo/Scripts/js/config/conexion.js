function PostResJson(url, form) {
    return new Promise(function (respuesta, error) {
        let xhr = new XMLHttpRequest();
        xhr.open('POST', url + '/');
        xhr.onload = function () {
            if (this.status >= 200 && this.status < 300) {
                respuesta(xhr.response);
            } else {
                error({
                    estado: this.status,
                    texto: xhr.statusText
                });
            }
        };
        xhr.onerror = function () {
            error({
                estado: this.status,
                texto: xhr.statusText
            });
        };
        xhr.send(form);
    });
}


function PostIdResJson(url, param) {
    return new Promise(function (respuesta, error) {
        var params = JSON.stringify({"id": param});
        let xhr = new XMLHttpRequest();

        xhr.open('POST', url + '/');
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xhr.onload = function () {
            if (this.status >= 200 && this.status < 300) {
                respuesta(xhr.response);
            } else {
                error({
                    estado: this.status,
                    texto: xhr.statusText
                });
            }
        };
        xhr.onerror = function () {
            error({
                estado: this.status,
                texto: xhr.statusText
            });
        };
        xhr.send(params);
    });
}


function PostIdResString(url, param) {
    return new Promise(function (respuesta, error) {
        var params = JSON.stringify({ "id": param });
        let xhr = new XMLHttpRequest();
        xhr.open('POST', url + '/');
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xhr.onload = function () {
            if (this.status >= 200 && this.status < 300) {
                respuesta(xhr.responseText);
            } else {
                error({
                    estado: this.status,
                    texto: xhr.statusText
                });
            }
        };
        xhr.onerror = function () {
            error({
                estado: this.status,
                texto: xhr.statusText
            });
        };
        xhr.send(params);
    });
}



function PostResString(url, form) {
    return new Promise(function (respuesta, error) {
        let xhr = new XMLHttpRequest();
        xhr.open('POST', url + '/');
        xhr.onload = function () {
            if (this.status >= 200 && this.status < 300) {
                respuesta(xhr.responseText);
            } else {
                error({
                    estado: this.status,
                    texto: xhr.statusText
                });
            }
        };
        xhr.onerror = function () {
            error({
                estado: this.status,
                texto: xhr.statusText
            });
        };
        xhr.send(form);
    });
}



function GetString(url) {
    return new Promise(function (respuesta, error) {
        let xhr = new XMLHttpRequest();
        xhr.open('GET', url + '/');
        xhr.onload = function () {
            if (this.status >= 200 && this.status < 300) {
                respuesta(xhr.responseText);
            } else {
                error({
                    estado: this.status,
                    texto: xhr.statusText
                });
            }
        };
        xhr.onerror = function () {
            error({
                estado: this.status,
                texto: xhr.statusText
            });
        };
        xhr.send();
    });
}


function GetJson(url) {
    return new Promise(function (respuesta, error) {
        let xhr = new XMLHttpRequest();
        xhr.open('GET', url + '/');
        xhr.onload = function () {
            if (this.status >= 200 && this.status < 300) {
                respuesta(xhr.response);
            } else {
                error({
                    estado: this.status,
                    texto: xhr.statusText
                });
            }
        };
        xhr.onerror = function () {
            error({
                estado: this.status,
                texto: xhr.statusText
            });
        };
        xhr.send();
    });
}
