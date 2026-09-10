var uploadCompleteFlag;
function FilesUploadComplete(s, e) {
    uploadCompleteFlag = true;
    PopupProgressingPanel.Hide();
    if (e.errorText != "")
        ShowMessage(e.errorText);
    else if (e.callbackData == "success")
        CargarNombresHojas();
}
function ShowMessage(message) {
    window.setTimeout("alert('" + message + "')", 0);
}
function FileUploadStart(s, e) {
    uploadCompleteFlag = false;
    window.setTimeout("ShowPopupProgressingPanel()", 500);
}
function ShowPopupProgressingPanel() {
    if (!uploadCompleteFlag) {
        PopupProgressingPanel.Show();
        pbProgressing.SetPosition(0);
        pnlProgressingInfo.SetContentHtml("");
    }
}
function UploadingProgressChanged(s, e) {
    pbProgressing.SetPosition(e.progress);
    var info = e.currentFileName + "&emsp;[" + GetKBytes(e.uploadedContentLength) + " / " + GetKBytes(e.totalContentLength) + "] KBytes";
    pnlProgressingInfo.SetContentHtml(info);
}
function GetKBytes(bytes) {
    return Math.floor(bytes / 1024);
}


var gridMovimiento;
var dataMovimiento = [];

var options = {
    editable: true,
    enableAddRow: false,
    enableCellNavigation: true,
    asyncEditorLoading: false,
    autoEdit: false,
    autoHeight: true
};

var pluginOptions = {
    includeHeaderWhenCopying: false
};

var columnas = new Array();

columnas.push({ resizable: false, width: 200, editor: Slick.Editors.Text, id: "gdv", name: "GDV", field: "GDV" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "ene", name: "ENE", field: "ENE" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "feb", name: "FEB", field: "FEB" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "mar", name: "MAR", field: "MAR" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "abr", name: "ABR", field: "ABR" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "may", name: "MAY", field: "MAY" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "jun", name: "JUN", field: "JUN" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "jul", name: "JUL", field: "JUL" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "ago", name: "AGO", field: "AGO" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "sep", name: "SEP", field: "SEP" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "oct", name: "OCT", field: "OCT" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "nov", name: "NOV", field: "NOV" });
columnas.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "dic", name: "DIC", field: "DIC" });
columnas.push({ resizable: false, width: 38, formatter: Slick.Formatters.PercentCompleteBar, id: "estado", name: "", field: "estado" });

var columnasMes = new Array();

columnasMes.push({ resizable: false, width: 200, editor: Slick.Editors.Text, id: "gdv", name: "GDV", field: "GDV" });
columnasMes.push({ resizable: false, width: 100, editor: Slick.Editors.Float, id: "vol", name: "VOLUMEN", field: "VOLUMEN" });
columnasMes.push({ resizable: false, width: 70, formatter: Slick.Formatters.PercentCompleteBar, id: "estado", name: "", field: "estado" });

var crearGridMovimiento = function (columnasGrilla) {
    iniciarGrilla("movimiento", gridMovimiento, dataMovimiento, columnasGrilla, '#grdMovimiento');
    $("#btnMovimientoGrabar").off("click");
    $("#btnMovimientoGrabar").on("click", function (e) {
        e.preventDefault();
        verificaFormula();
    });
    /*$("#btnAlimentoDatos").off("click");
    $("#btnAlimentoDatos").on("click", function (e) {
    e.preventDefault();
    mostrarMensajeGrabarGrilla(function () {
    if ($("#hdnIdPlanta").val() != "0" && $("#hdnIdPlanta").val() != ""
    && $("#ddlCorriente :selected").val() != "0" && $("#ddlCorriente :selected").val() != "") {
    dataAlimento = gridAlimento.getData();
    var count = gridAlimento.getDataLength();
    actualizarEstadoGrilla(gridAlimento, dataAlimento, count, 50, "estado");
    for (var i = 0; i < count; i++) {
    if (DiferenciaFechas(dataAlimento[i]["fecha"], $("#txtFechaUltminoReporte").val()) > 0) {
    $.ajax({
    type: "POST",
    url: 'WfVolumenesGLP.aspx/RegistrarVolumenesAlimentoGLP',
    data: JSON.stringify({
    planta: $("#hdnIdPlanta").val(),
    corriente: $("#ddlCorriente :selected").val(),
    umVolumen: $("#ddlUnidadMedVolumen :selected").val(),
    umGLP: $("#ddlUnidadMedGLP :selected").val(),
    umPoderCalor: $("#ddlUnidadMedPoderCalor :selected").val(),
    fecha: dataAlimento[i]["fecha"],
    gravEspec: serializarValor(dataAlimento[i]["gravedadEspecifica"]),
    volumen: serializarValor(dataAlimento[i]["volumen"]),
    contGLP: serializarValor(dataAlimento[i]["contenidoGLP"]),
    poderCalor: serializarValor(dataAlimento[i]["poderCalorifico"]),
    n2: serializarValor(dataAlimento[i]["N2"]),
    co2: serializarValor(dataAlimento[i]["CO2"]),
    c1: serializarValor(dataAlimento[i]["C1"]),
    c2: serializarValor(dataAlimento[i]["C2"]),
    c3: serializarValor(dataAlimento[i]["C3"]),
    iC4: serializarValor(dataAlimento[i]["iC4"]),
    nC4: serializarValor(dataAlimento[i]["nC4"]),
    iC5: serializarValor(dataAlimento[i]["iC5"]),
    nC5: serializarValor(dataAlimento[i]["nC5"]),
    nC6: serializarValor(dataAlimento[i]["nC6"]),
    c7: serializarValor(dataAlimento[i]["C7"]),
    obs: dataAlimento[i]["observaciones"] || "",
    just: dataAlimento[i]["justificacion"] || ""
    }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    cache: false,
    async: false,
    beforeSend: function () { },
    success: function (msg) {
    dataAlimento = gridAlimento.getData();
    $.each(msg.d, function (index, obj) {
    if (obj.RESULTADO >= 0) {
    actualizarFilaEstado(gridAlimento, dataAlimento, i, 100, "alimento", "estado");
    mostrarMensajeVentana(i, count, "info", "Se terminaron de registrar los datos.");
    } else {
    actualizarFilaEstado(gridAlimento, dataAlimento, i, 20, "alimento", "estado");
    mostrarMensajeVentana(0, 0, "alert", obj.MENSAJE);
    }
    });
    },
    error: function (xhr, ajaxOptions, thrownError) {
    dataAlimento = gridAlimento.getData();
    actualizarFilaEstado(gridAlimento, dataAlimento, i, 80, "alimento", "estado");
    mostrarMensajeVentana(i, count, "error");
    }
    });
    } else {
    mostrarMensaje("alert", "No se guardaran los datos del día " + dataAlimento[i]["fecha"] + ", porque estos ya se encuentran registrados en el sistema");
    }
    }
    } else {
    mostrarMensaje("alert", "Primero debe seleccionar una planta y una corriente/campo");
    }
    });
    });*/
    $("#btnMovimientoLimpiar").off("click");
    $("#btnMovimientoLimpiar").on("click", function (e) {
        e.preventDefault();
        mostrarMensajeBorrarGrilla("movimiento");
    });
};

var dGrid = "";
var mostrarMensajeBorrarGrilla = function (grid) {
    $.msgBox({
        title: presentacion.tituloMensajeBox,
        content: "Quiere borrar todos los datos de la grilla?",
        type: "confirm",
        buttons: [{ value: "Aceptar" }, { value: "Cancelar"}],
        success: function (result) {
            if (result == "Aceptar") {
                switch (grid) {
                    case "movimiento":
                        dataMovimiento = []; dataMovimiento[0] = {};
                        gridMovimiento.setData(dataMovimiento);
                        gridMovimiento.render();
                        break;
                }
            }
        }
    });
};

var actualizarFilaEstado = function (vGrid, vData, i, valor, nGrid, campo) {
    if (campo == undefined) campo = "estado";
    vData[i][campo] = valor;
    vGrid.invalidateRow(i);
    switch (nGrid) {
        case "movimiento": gridMovimiento = vGrid; gridMovimiento.setData(vData); gridMovimiento.render(); break;
    }
};
var iniciarGrilla = function (nGrid, vGrid, vData, vColumnas, vIdDiv) {
    /*if (vGrid == undefined || vGrid.getDataLength() < 1) {*/
    vData[0] = {};
    vGrid = new Slick.Grid(vIdDiv, vData, vColumnas, options);
    vGrid.setSelectionModel(new Slick.CellSelectionModel());
    vGrid.registerPlugin(new Slick.CellExternalCopyManager(pluginOptions));
    vGrid.setActiveCell(0, 0);
    /*}*/
    switch (nGrid) {
        case "movimiento": gridMovimiento = vGrid; break;
    }
};
var actualizarEstadoGrilla = function (vGrid, vData, count, valor, campo) {
    if (valor == undefined) valor = 50;
    if (campo == undefined) campo = "estado";
    for (var i = 0; i < count; i++) {
        vData[i][campo] = valor;
        vGrid.invalidateRow(i);
    }
    vGrid.render();
};

var verificaFormula = function () {
    dataMovimiento = gridMovimiento.getData();
    var count = gridMovimiento.getDataLength();

    var volumenMes;
    var valor = $('#ddlTipoReporte :selected').text();
    if (!valor.match(/mes/g)) {
        volumenMes = "ENE";
    } else {
        volumenMes = "VOLUMEN";
    }
    var items = new Array;
    for (var i = 0; i < count; i++) {
        items[i] = { id: dataMovimiento[i]["GDV"], vol: presentacion.serializarValor(dataMovimiento[i][volumenMes]) };
    }
    var jsonText = JSON.stringify(items);
    $.ajax({
        type: "POST",
        url: 'WfMovimientoProducto.aspx/VerificarFormula',
        data: "{ idProducto:" + $("#ddlProducto :selected").val() + ",gestion:" + teAnioReporte.GetText() + ",parametros: " + jsonText + " }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        async: false,
        beforeSend: function () { },
        success: function (msg) {
            dataMovimiento = gridMovimiento.getData();
            items = new Array;
            $.each(msg.d, function (index, obj) {
                if (obj.resultado == 0) {
                    var sep = "";
                    var formula = "";
                    $.each(obj.parametros, function (index2, param) {
                        formula += (sep != param.tOperacion ?
                                (sep != "" ? "</ul>" : "") + "<ul id='" + param.tOperacion + "' class='formula' " + (param.tOperacion == "0na" ? "style='background-color: #FFCCFF !important;" : "") + "'>" +
                                    "<li class='ui-state-default ui-state-disabled'>" + (param.tOperacion != "0na" ? param.tOperacion : "Datos que no estan en la f&oacute;rmula") + "</li>"
                                : "") +
                                    (param.id != "" ? "<li class='ui-state-highlight'>" +
                                        "<a class='Hydro_Boton_Actualizar_Excel' href='javascript:void(0)'><span class='icon ui-icon-" + (param.suma == 0 ? "minusthick" : "plusthick") + "'></span></a>" +
                                            param.id + "</li>" : "");
                        sep = param.tOperacion;
                    });
                    formula += formula != "" ? "</ul>" : "";

                    $('#nombreProducto').html($('#ddlProducto :selected').text());
                    $('#listas').html(formula);

                    $("ul.formula").sortable({
                        connectWith: "ul",
                        items: "li:not(.ui-state-disabled)"
                    });
                    $("ul.formula .icon").click(function () {
                        $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
                    });

                    $.fancybox.open({
                        href: '#divFormula',
                        autoSize: true,
                        closeClick: false,
                        minWidth: '90%',
                        minHeight: 400,
                        type: 'inline',
                        padding: 5,
                        helpers: {
                            title: {
                                type: 'inside',
                                position: 'top'
                            }
                        }
                    });
                } else {
                    presentacion.mostrarMensajeVentana(0, 0, "alert", obj.mensaje);
                }
            });
        }
    });
};
var registrarFormula = function () {
    dataMovimiento = gridMovimiento.getData();
    var count = gridMovimiento.getDataLength();

    var volumenMes;
    var valor = $('#ddlTipoReporte :selected').text();
    if (!valor.match(/mes/g)) {
        volumenMes = "ENE";
    } else {
        volumenMes = "VOLUMEN";
    }
    var items = new Array;
    for (var i = 0; i < count; i++) {
        items[i] = { id: dataMovimiento[i]["GDV"], vol: presentacion.serializarValor(dataMovimiento[i][volumenMes]) };
    }
    var jsonText = JSON.stringify(items);
    $.ajax({
        type: "POST",
        url: 'WfMovimientoProducto.aspx/RegistrarFormula',
        data: "{ idProducto:" + $("#ddlProducto :selected").val() + ",gestion:" + teAnioReporte.GetText() + ",parametros: " + jsonText + " }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        async: false,
        beforeSend: function () { },
        success: function (msg) {
            dataMovimiento = gridMovimiento.getData();
            items = new Array;
            $.each(msg.d, function (index, obj) {
                if (obj.resultado == 0) {
                    var sep = "";
                    var formula = "";
                    $.each(obj.parametros, function (index2, param) {
                        formula += (sep != param.tOperacion ?
                                (sep != "" ? "</ul>" : "") + "<ul id='" + param.tOperacion + "' class='formula'>" +
                                    (param.tOperacion != "0na" ? "<li class='ui-state-default ui-state-disabled'>" + param.tOperacion + "</li>" : "")
                                : "") +
                                    (param.id != "" ? "<li class='ui-state-highlight'>" +
                                        "<a class='Hydro_Boton_Actualizar_Excel' href='javascript:void(0)'><span class='icon ui-icon-" + (param.suma == 0 ? "minusthick" : "plusthick") + "'></span></a>" +
                                            param.id + "</li>" : "");
                        sep = param.tOperacion;
                    });
                    formula += formula != "" ? "</ul>" : "";

                    $('#nombreProducto').html($('#ddlProducto :selected').text());
                    $('#listas').html(formula);

                    $("ul.formula").sortable({
                        connectWith: "ul",
                        items: "li:not(.ui-state-disabled)"
                    });
                    $("ul.formula .icon").click(function () {
                        $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
                    });

                    $.fancybox.open({
                        href: '#divFormula',
                        autoSize: true,
                        closeClick: false,
                        minWidth: '90%',
                        minHeight: 400,
                        type: 'inline',
                        padding: 5,
                        helpers: {
                            title: {
                                type: 'inside',
                                position: 'top'
                            }
                        }
                    });
                } else {
                    presentacion.mostrarMensajeVentana(0, 0, "alert", obj.mensaje);
                }
            });
        }
    });
};
var CargarNombresHojas = function () {
    $.ajax({
        type: "POST",
        url: 'WfMovimientoProducto.aspx/ListarNombresHojas',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        async: false,
        beforeSend: function () { $("#ddlHojaExcel").html("Cargando ..."); },
        success: function (msg) {
            $("#ddlHojaExcel").html("");
            var hojas = "";
            $.each(msg.d, function (index, obj) {
                hojas += "<option value='" + obj.index + "'>" + obj.nombre + "</option>";
            });
            $("#ddlHojaExcel").html(hojas);
        }
    });
};
$(function () {
    $(document).tooltip();
    $('#ddlTipoReporte').on("change", function () {
        var regex = new RegExp(/mes/g);
        var valor = $('#ddlTipoReporte :selected').text();
        if (valor.match(regex)) {
            crearGridMovimiento(columnasMes);
            $('#trMes').show();
        } else {
            crearGridMovimiento(columnas);
            $('#trMes').hide();
        }
        dataMovimiento = []; dataMovimiento[0] = {};
        gridMovimiento.setData(dataMovimiento);
        gridMovimiento.render();
    });
    $('#ddlTipoReporte').change();
    $("#btnAlimentoFecha").on("click", function (e) {
        e.preventDefault();
        buscarFechaUltimoReporte();
    });
    $("#lnkGenerarReporte").on("click", function (e) {
        e.preventDefault();
        var idEntidades = "", sep = "";
        for (var i = 0; i < cblEntidades.GetSelectedValues().length; i++) {
            idEntidades += sep + cblEntidades.GetSelectedValues()[i];
            sep = ";";
        }
        if (idEntidades != "") {
            pgReporte.PerformCallback(idEntidades + "|" + txtFechaIni.GetText() + "|" + txtFechaFin.GetText() + "|" + $("#ddlUnidadMedida :selected").val());
        } else {
            presentacion.mostrarMensaje("alert", "Seleccione una PLANTA.");
        }
    });
    $("#btnGrabarArchivo").on("click", function () {
        var continuar = true, mensajeError = "";
        if (Number($("#ddlRefineria :selected").val()) < 1) {
            continuar = false;
            mensajeError = "Antes de continuar seleccione una Refiner&iacute;a";
        }
        if (continuar) {
            $.ajax({
                type: "POST",
                url: 'WfMovimientoProducto.aspx/GrabarArchivoCargado',
                data: "{ gestion:" + teAnioReporteArchivo.GetText() + ",mes: " + teMesReporteArchivo.GetText() + ", idEntidad : " + $("#ddlRefineria :selected").val() + ",idUnMedArchivo : " + $("#ddlUnidadMedArchivo :selected").val() + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $('#btnGrabarArchivo').find('span.icon').removeClass('ui-icon-disk').removeClass('icon').addClass('ui-icon-loading');
                },
                success: function (msg) {
                    $.each(msg.d, function (index, obj) {
                        presentacion.mostrarMensajeSimple((index == "OK" ? "info" : "error"), obj);
                    });
                    $('#btnGrabarArchivo').find('span.ui-icon-loading').addClass('ui-icon-disk').addClass('icon').removeClass('ui-icon-loading');
                },
                error: function (xhr, textStatus, errorThrown) {
                    presentacion.mostrarMensajeSimple("alert", "Error al registrar");
                    $('#btnGrabarArchivo').find('span.ui-icon-loading').addClass('ui-icon-disk').addClass('icon').removeClass('ui-icon-loading');
                }
            });
        } else {
            presentacion.mostrarMensajeSimple("alert", mensajeError);
        }
    });
    $("#btnCargarDatos").on("click", function () {
        var continuar = true, mensajeError = "";
        if ($('#ddlHojaExcel > option').length < 1) {
            continuar = false;
            mensajeError = "Seleccione un archivo con el movimiento de productos.";
        }
        else if (Number($("#ddlRefineria :selected").val()) < 1) {
            continuar = false;
            mensajeError = "Antes de continuar seleccione una Refiner&iacute;a";
        }
        if (continuar) {
            $.ajax({
                type: "POST",
                url: 'WfMovimientoProducto.aspx/LeerArchivoCargado',
                data: "{ index:" + $("#ddlHojaExcel :selected").val() + ",mes: " + teMesReporteArchivo.GetText() + ", idEntidad : " + $("#ddlRefineria :selected").val() + ",idUnMedArchivo : " + $("#ddlUnidadMedArchivo :selected").val() + ", idUnMedReporte : " + $("#ddlUnidadMedReporte :selected").val() + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#divRptMovimiento").html('<b>Procesando el archivo ...</b>');
                    $('#btnCargarDatos').find('span.icon').removeClass('ui-icon-gear').removeClass('icon').addClass('ui-icon-loading');
                },
                success: function (msg) {
                    var titulo = '';
                    var datos = '';
                    var cantidadColumna;
                    var tabla = new Array();
                    var tituloTabla = new Array();
                    var html = '', grupo = '';
                    var factor = 0;
                    $.each(msg.d, function (index, obj) {
                        var perdidas = 0, demasias = 0;
                        var perdidaDemasia = false;
                        var sinTipoOperacion = false;
                        cantidadColumna = 2;

                        titulo = '';
                        datos = '';

                        if (index != "factor") {
                            $.each(obj, function (index2, param) {
                                if (index2 != 'ninguno' && index2 != 'grupo') {
                                    var sum = 0;
                                    $.each(param, function (index3, listaValor) {
                                        var valor = 0, suma = 0;
                                        $.each(listaValor, function (index4, valorDato) {
                                            switch (index4) {
                                                case "valor": valor = valorDato; break;
                                                case "suma": suma = (valorDato == 1 ? 1 : -1); break;
                                            }
                                        });
                                        sum += (suma) * valor;
                                    });
                                    if (index2 == 'PERDIDAS') {
                                        perdidas = sum;
                                        perdidaDemasia = true;
                                    } else if (index2 == 'DEMASIAS') {
                                        demasias = sum;
                                        perdidaDemasia = true;
                                    } else {
                                        titulo += '<td>' + index2 + '</td>';
                                        datos += '<td>' + presentacion.formatearNumero(sum * factor) + '</td>';
                                    }
                                    cantidadColumna++;
                                } else if (index2 == 'grupo') {
                                    $.each(param, function (index3, listaValor) {
                                        grupo = index3;
                                    });
                                } else if (index2 == 'ninguno') {
                                    var sum1 = 0;
                                    $.each(param, function (index3, listaValor) {
                                        var valor = 0, suma = 0;
                                        $.each(listaValor, function (index4, valorDato) {
                                            switch (index4) {
                                                case "valor": valor = valorDato; break;
                                            }
                                        });
                                        sum1 += (suma) * valor;
                                    });
                                    sinTipoOperacion = sum > 0;
                                }
                            });

                            titulo = '<tr><td></td><td>PRODUCTO</td>' + titulo;
                            datos = '<tr><td><a href="javascript:void(0)" class="lnkModificar Hydro_Link_Boton_Verde" producto="' + index + '">Modificar C&aacute;lculo</a></td><td>' + index + (sinTipoOperacion ? '<em class="Hydro_Alerta_Rojo">* Existen procesos sin tipo de operaci&oacute;n</em>' : '') + '</td>' + datos;

                            if (perdidaDemasia) {
                                var balance = (demasias - perdidas) * factor;
                                demasias = balance > 0 ? balance : 0;
                                perdidas = balance < 0 ? balance * (-1) : 0;
                                titulo += '<td>PERDIDAS</td><td>DEMASIAS</td>';
                                datos += '<td>' + presentacion.formatearNumero(perdidas) + '</td><td>' + presentacion.formatearNumero(demasias) + '</td>';
                            }
                            titulo += '</tr>';
                            datos += '</tr>';

                            if (tabla[titulo] == undefined) tabla[titulo] = '';

                            tituloTabla[titulo] = '<tr><td colspan="' + cantidadColumna + '"><b>MOVIMIENTO ' + grupo + '</b></td></tr>';
                            tabla[titulo] += datos;
                        } else {
                            $.each(obj, function (index2, param) {
                                factor = Number(index2);
                            });
                        }
                    });
                    for (titulo in tabla) {
                        html += '<table>' + tituloTabla[titulo] + titulo + tabla[titulo] + '</table><br/><br/>';
                    }
                    $("#divRptMovimiento").html(html);
                    modificarFormula();
                    $('#btnCargarDatos').find('span.ui-icon-loading').addClass('ui-icon-gear').addClass('icon').removeClass('ui-icon-loading');
                },
                error: function (xhr, textStatus, errorThrown) {
                    $("#divRptMovimiento").html('<b>' + errorThrown.message + '</b>');
                    $('#btnCargarDatos').find('span.ui-icon-loading').addClass('ui-icon-gear').addClass('icon').removeClass('ui-icon-loading');
                }
            });
        } else {
            presentacion.mostrarMensajeSimple("alert", mensajeError);
        }
    });
    var modificarFormula = function () {
        $(".lnkModificar").off("click");
        $(".lnkModificar").on("click", function () {
            var productoSeleccionado = $(this).attr("producto");
            $.ajax({
                type: "POST",
                url: 'WfMovimientoProducto.aspx/ModificarFormula',
                data: '{ producto:"' + $(this).attr("producto") + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                async: false,
                beforeSend: function () { },
                success: function (msg) {
                    items = new Array;
                    var formula = "";
                    var sep = "";
                    $.each(msg.d, function (index, obj) {
                        if (index != 'grupo') {
                            formula += (sep != index ?
                                (sep != "" ? "</ul>" : "") + "<ul id='" + index + "' class='formula'>" +
                                    "<li class='ui-state-default ui-state-disabled'>" + (index == "ninguno" ? "Sin tipo de operación" : index) + "</li>"
                                : "");
                            $.each(obj, function (index2, listaValor) {
                                var valor = 0, suma = 0;
                                $.each(listaValor, function (index3, valorDato) {
                                    switch (index3) {
                                        case "valor": valor = valorDato; break;
                                        case "suma": suma = valorDato; break;
                                    }
                                });
                                if (index == "ninguno") { suma = 1; }
                                formula += (valor != 0 ? "<li class='ui-state-highlight'>" +
                                    "<a class='Hydro_Boton_Actualizar_Excel' href='javascript:void(0)' valor='" + (suma.toString()) + "'><span class='icon ui-icon-" + (suma == 0 ? "minusthick" : "plusthick") + "'></span></a>" +
                                        index2 + "</li>" : "");
                            });
                            sep = index;
                        }
                    });
                    formula += formula != "" ? "</ul>" : "";
                    $('#nombreProducto').html(productoSeleccionado);
                    $('#listas').html(formula);

                    $("ul.formula").sortable({
                        connectWith: "ul",
                        items: "li:not(.ui-state-disabled)"
                    });
                    $("ul.formula .icon").click(function () {
                        $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
                    });

                    grabarCambioFormulaArchivo();

                    $.fancybox.open({
                        href: '#divFormula',
                        autoSize: true,
                        closeClick: false,
                        minWidth: '90%',
                        minHeight: 400,
                        type: 'inline',
                        padding: 5,
                        helpers: {
                            title: {
                                type: 'inside',
                                position: 'top'
                            }
                        }
                    });
                }
            });
        });
    };

    var grabarCambioFormulaArchivo = function () {
        $("#btnGrabarArchivoCalculo").show();
        $("#btnGrabarArchivoCalculo").off("click");
        $("#btnGrabarArchivoCalculo").on("click", function (e) {
            e.preventDefault();
            if ($("#ninguno").find("li.ui-state-highlight").length < 1) {
                var formula = new Array();
                var i = 0;
                $(".formula").each(function () {
                    if ($(this).attr("id") != "ninguno") {
                        var tipoOperacion = $(this).attr("id");
                        var procesos = new Array();
                        var j = 0;
                        $(this).find("li.ui-state-highlight").each(function () {
                            var suma = ($(this).find('span.icon').hasClass("ui-icon-plusthick")) ? 1 : 0;
                            procesos[j++] = { proc: $(this).text(), suma: suma };
                        });
                        formula[i++] = { tOper: tipoOperacion, procesos: procesos };
                    }
                });
                var jsonText = JSON.stringify(formula);
                $.ajax({
                    url: "WfMovimientoProducto.aspx/RegistrarFormulaArchivo",
                    data: "{ idEntidad: " + $("#ddlRefineria :selected").val() + ",producto: \"" + $("#nombreProducto").text() + "\", formula: " + jsonText + " }",
                    dataType: "json",
                    type: "POST",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    beforeSend: function () {
                        $('#btnGrabarArchivoCalculo').find('span.icon').removeClass('ui-icon-disk').removeClass('icon').addClass('ui-icon-loading');
                    },
                    success: function (data) {
                        $.each(data.d, function (index, obj) {
                            if (obj.RESULTADO == 1) {
                                $("#btnGrabarArchivoCalculo").hide();
                                $("#btnCargarDatos").click();
                                setTimeout('presentacion.cerrarFancybox();', 4000);
                            } else {
                                presentacion.mostrarMensajeSimple("alert", "No se registraron los cambios. Error: " + obj.MENSAJE);
                            }
                        });
                        $('#btnGrabarArchivoCalculo').find('span.ui-icon-loading').addClass('ui-icon-disk').addClass('icon').removeClass('ui-icon-loading');
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        $('#btnGrabarArchivoCalculo').find('span.ui-icon-loading').addClass('ui-icon-disk').addClass('icon').removeClass('ui-icon-loading');
                    }
                });
            } else {
                presentacion.mostrarMensajeSimple("alert", "La columna \"Sin tipo de operaci&oacute;n\" debe quedar vac&iacute;a");
            }
        });
        $("#btnCancelarArchivoCalculo").off("click");
        $("#btnCancelarArchivoCalculo").on("click", function () {
            presentacion.cerrarFancybox();
        });
    };
    var buscarFechaUltimoReporte = function () {
        if ($("#ddlCorriente :selected").val() != "0" && $("#ddlCorriente :selected").val() != "") {
            $.ajax({
                url: "WfVolumenesGLP.aspx/FechaUltimoReporte",
                data: "{ idEntidad: " + $("#hdnIdPlanta").val() + ", idCampo: " + $("#ddlCorriente :selected").val() + " }",
                dataType: "json",
                type: "POST",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtFechaUltminoReporte").val("");
                    $('#btnAlimentoFecha').find('span').removeClass('ui-icon-refresh').removeClass('icon').addClass('ui-icon-loading');
                },
                success: function (data) {
                    $.each(data.d, function (index, obj) {
                        $("#txtFechaUltminoReporte").val(obj.fecha);
                    });
                    $('#btnAlimentoFecha').find('span').addClass('ui-icon-refresh').addClass('icon').removeClass('ui-icon-loading');
                },
                error: function (xhr, textStatus, errorThrown) {
                    $("#txtFechaUltminoReporte").val("Error");
                    $('#btnAlimentoFecha').find('span').addClass('ui-icon-refresh').addClass('icon').removeClass('ui-icon-loading');
                }
            });
        }
    };
});