/*efectos de transicion de pantalla*/
var presentacion = (function () {
    /* variables y funciones privadas */
    var tOcultarElemento = 15000,
        fancyboxWidth = 920,
        fancyboxHeight = 540,
        tituloMensajeBox = "Octano",
    /*tablas especificacion*/
        mostrarElemento = function (elementoMostrar, elementoOcultar, ejecutarfuncion) {
            if (typeof ejecutarfuncion == "function")
                ejecutarfuncion();
            $(elementoOcultar).slideUp('fast', function () {
                $(elementoMostrar).slideDown('slow');
            });
        },
    /*ocultar mensajes*/
        ocultarElemento = function (elemento, tiempo) {
            $(elemento).delay(tiempo != undefined ? tiempo : tOcultarElemento).slideUp('slow');
        },
    /*mostrar efecto transferencia entre objetos*/
        mostrarTransferencia = function (elementoOrigen, idElementoDestino) {
            $(elementoOrigen).effect("transfer", { to: idElementoDestino, className: "ui-effects-transfer" }, 500);
        },
    /*tablas especificacion*/
        mostrarMensaje = function (mensaje, elemento, tipo) {
            var clase = (tipo == 1 /*error*/
                ? "Hydro_Div_Aviso_Rojo"
                : tipo == 2 /*aviso*/
                    ? "Hydro_Div_Aviso_Azul"
                    : tipo == 3 /*OK*/
                        ? "Hydro_Div_Aviso_Verde"
                        : tipo == 4 /*alerta*/
                            ? "Hydro_Div_Aviso_Naranja"
                            : "Hydro_Div_Aviso_Azul" /*por defecto*/);
            $(elemento).attr("class", "");
            $(elemento).html(mensaje).addClass(clase).show();
            ocultarElemento(elemento);
        },
    /*Mostrar mensaje para grabar los datos, luego ejecutar una funcion*/
        mostrarMensajeGrabarGrilla = function (funcionGrabar) {
            $.msgBox({
                title: tituloMensajeBox,
                content: "Quiere guardar los datos de la grilla?",
                type: "confirm",
                buttons: [{ value: "Aceptar" }, { value: "Cancelar"}],
                success: function (result) {
                    if (result == "Aceptar") {
                        if (typeof funcionGrabar == "function")
                            funcionGrabar();
                    }
                }
            });
        },
    /*Tipo mensaje es equivalente a los tipos de mensaje del JQuery.msgBox, por defecto se mostrara como error*/
         mostrarMensajeVentana = function (i, count, tipoMensaje, contenido) {
             if (i == count - 1 || (i == 0 && count == 0)) {
                 if (tipoMensaje == undefined) tipoMensaje = "error";
                 if (contenido == undefined) contenido = "Algunos datos no pudieron registrarse en el sistema";
                 $.msgBox({
                     title: tituloMensajeBox,
                     content: contenido,
                     type: tipoMensaje,
                     buttons: [{ value: "Aceptar"}],
                     opacity: 0.1
                 });
             }
         },
    /*Tipo mensaje es equivalente a los tipos de mensaje del JQuery.msgBox, por defecto se mostrara como error*/
        mostrarMensajeSimple = function (tipoMensaje, contenido) {
            if (tipoMensaje == undefined) tipoMensaje = "error";
            if (contenido == undefined) contenido = "Error al generar el mensaje.";
            $.msgBox({
                title: tituloMensajeBox,
                content: contenido,
                type: tipoMensaje,
                buttons: [{ value: "Aceptar"}],
                opacity: 0.1
            });
        },
    /*Quitar los '.' (puntos) de los separadores de miles, reeemplazar la ',' por '.' para el punto decimal, porque en JavaScript el '.' es el punto decimal*/
        serializarValor = function (valor) {
            var erComma = /,/g;
            if (erComma.test(valor)) {
                valor = valor.replace('.', '').replace(',', '.'); ;
            }
            return parseFloat(valor) || 0;
        },
    /*Crear un objeto con los campos de una fecha*/
        fecha = function (cadena) {
            /*Separador para la introduccion de las fechas*/
            var separador = "/";
            if ($.trim(cadena) != "") {
                /*Separa por dia, mes y año*/
                if (cadena.indexOf(separador) != -1) {
                    var cadenaFecha = cadena.split(separador);
                    this.dia = Number(cadenaFecha[0]);
                    this.mes = Number(cadenaFecha[1]) - 1;
                    this.anio = Number(cadenaFecha[2]);
                } else {
                    this.dia = 0;
                    this.mes = 0;
                    this.anio = 0;
                }
            }
        },
    /*Obtener la diferencia en días de dos fechas*/
        diferenciaFechas = function (cadenaFecha1, cadenaFecha2) {
            var dias = 1;
            if ($.trim(cadenaFecha2) != '') {
                /*Obtiene dia, mes y año*/
                var fecha1 = new fecha(cadenaFecha1);
                var fecha2 = new fecha(cadenaFecha2);

                /*Obtiene objetos Date*/
                var miFecha1 = new Date(fecha1.anio, fecha1.mes, fecha1.dia);
                var miFecha2 = new Date(fecha2.anio, fecha2.mes, fecha2.dia);

                /*Resta fechas y redondea*/
                var diferencia = miFecha1.getTime() - miFecha2.getTime();
                dias = Math.floor(diferencia / (1000 * 60 * 60 * 24));
            }
            return dias;
        },
    /*copiar contenido de texto en devexpress*/
        copiarTexto = function (elementoOrigen, elementoDestino) {
            if (elementoDestino.GetText() == '')
                elementoDestino.SetText(elementoOrigen.GetText());
        },
    /*cerrar popup*/
        cerrarFancybox = function () {
            $.fancybox.close(true);
        },
    /*mostrar popup*/
        mostrarFancybox = function (elemento, titulo, width, height) {
            $.fancybox.open({
                title: (titulo != '' ? '<h3>' + titulo + '</h3>': ''),
                href: $(elemento).attr('href'),
                autoSize: true,
                closeClick: false,
                minWidth: (width != undefined ? width : fancyboxWidth),
                minHeight: (height != undefined ? height : fancyboxHeight),
                type: 'iframe',
                padding: 5,
                helpers: {
                    title: {
                        type: 'inside',
                        position: 'top'
                    }
                }
            });
        },
    /*mostrar popup con ancho*/
        mostrarFancyboxAncho = function (elemento, titulo, width, height) {
            $.fancybox.open({
                title: '<h3>' + titulo + '</h3>',
                href: $(elemento).attr('href'),
                autoSize: false,
                closeClick: false,
                width: (width != undefined ? width : fancyboxWidth),
                height: (height != undefined ? height : fancyboxHeight),
                type: 'iframe',
                padding: 5,
                helpers: {
                    title: {
                        type: 'inside',
                        position: 'top'
                    }
                }
            });
        },
    /*formatear numero*/
        formatearNumero = function (numero, decimales) {
            decimales = decimales == undefined ? 1 : decimales;
            return numero.toFixed(decimales).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        };
    /* API publica */
    return {
        mostrarElemento: mostrarElemento,
        ocultarElemento: ocultarElemento,
        mostrarFancybox: mostrarFancybox,
        cerrarFancybox: cerrarFancybox,
        copiarTexto: copiarTexto,
        mostrarTransferencia: mostrarTransferencia,
        mostrarMensaje: mostrarMensaje,
        mostrarMensajeGrabarGrilla: mostrarMensajeGrabarGrilla,
        mostrarMensajeVentana: mostrarMensajeVentana,
        mostrarMensajeSimple: mostrarMensajeSimple,
        diferenciaFechas: diferenciaFechas,
        serializarValor: serializarValor,
        mostrarFancyboxAncho: mostrarFancyboxAncho,
        formatearNumero: formatearNumero
    };
})();