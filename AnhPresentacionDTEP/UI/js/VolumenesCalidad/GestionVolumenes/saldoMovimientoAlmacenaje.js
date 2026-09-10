var tituloMsgBox = "Octano";
var gridAlimento, gridProduccion, gridResidual;
var dataAlimento = [], dataProduccion = [], dataResidual = [];

var options = {
    editable: true,
    enableAddRow: false,
    enableCellNavigation: true,
    asyncEditorLoading: false,
    autoEdit: false,
    autoHeight: true
};

var pluginOptions = {
    /*clipboardCommandHandler: function (editCommand) { undoRedoBuffer.queueAndExecuteCommand.call(undoRedoBuffer, editCommand); },*/
    includeHeaderWhenCopying: false
};

var columns = new Array();

columns.push({ editor: Slick.Editors.Float, resizable: true, width: 110, id: "DETALLE", name: "DETALLE", field: "DETALLE", toolTip: "DETALLE DE TIPO DE OPERACIÓN" });
columns.push({ editor: Slick.Editors.Float, resizable: true, width: 90, id: "TERMINAL", name: "TERMINAL", field: "TERMINAL", toolTip: "TERMINAL DE PLANTA DE DISTRIBUCIÓN" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "AVG", name: "AVG", field: "AVG", toolTip: "GASOLINA DE AVIACIÓN" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "CRUDO", name: "CRUDO", field: "CRUDO", toolTip: "CRUDO" });
columns.push({ editor: Slick.Editors.Float, resizable: true, width:  75, id: "CRUDO_RECON", name: "CRUDO_RECON", field: "CRUDO RECONSTITUIDO", toolTip: "CRUDO RECONSTITUIDO" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "DO", name: "DO", field: "DIESEL OIL", toolTip: "DIESEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "FO", name: "FO", field: "FUEL OIL", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "GE", name: "GE", field: "GASOLINA ESPECIAL", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "GE_IMP", name: "GE_IMP", field: "GASOLINA ESPECIAL IMPORTADA", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "GLP", name: "GLP", field: "GAS LICUADO DE PETROLEO", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "GLS", name: "GLS", field: "GLS", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "GP", name: "GP", field: "GASOLINA PREMIUM", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "IDO", name: "IDO", field: "IDO", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "IGE", name: "IGE", field: "IGE", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "JF", name: "JF", field: "JET FUEL", toolTip: "FUEL OIL" });
columns.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "KE", name: "KE", field: "KEROSSENE", toolTip: "FUEL OIL" });
columns.push({ formatter: Slick.Formatters.PercentCompleteBar, resizable: false, width: 35, id: "estado", name: "", field: "estado" });



var columnasProduccion = new Array();

columnasProduccion.push({ editor: Slick.Editors.Date, resizable: false, width: 75, id: "fecha", name: "Fecha", field: "fecha", toolTip: "DD / MM / YYYY" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "produccionGLP", name: "Pro.GLP", field: "produccionGLP", toolTip: "Producción GLP(TM/DIA)" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "produccionPropano", name: "Pro.Prop", field: "produccionPropano", toolTip: "Producción PROPANO (TM/DIA)" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "entregaConsumoPropano", name: "E/C Prop", field: "entregaConsumoPropano", toolTip: "Entrega/Consumo PROPANO (TM/DIA)" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "entregaGlpCisterna", name: "E.GLP Cist", field: "entregaGlpCisterna", toolTip: "Entrega GLP/CISTERNA (TM/DIA)" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 75, id: "entregaGlpDucto", name: "E.GLP Duc", field: "entregaGlpDucto", toolTip: "Entrega GLP/DUCTO (TM/DIA)" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "saldoGLP", name: "S.GLP", field: "saldoGLP", toolTip: "Saldo GLP (TM)" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "saldoPropano", name: "S.PROP", field: "saldoPropano", toolTip: "Saldo PROPANO (TM)" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "gravedadEspecifica", name: "Grav.Esp", field: "gravedadEspecifica", toolTip: "Gravedad Especifica" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "TVR", name: "TVR", field: "TVR", toolTip: "TVR" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "temperatura", name: "Temp", field: "temperatura", toolTip: "Temperatura (ºF)" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "C2", name: "C2", field: "C2" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "C3", name: "C3", field: "C3" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "iC4", name: "i-C4", field: "iC4" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "nC4", name: "n-C4", field: "nC4" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "iC5", name: "i-C5", field: "iC5" });
columnasProduccion.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "nC5", name: "n-C5", field: "nC5" });
columnasProduccion.push({ editor: Slick.Editors.LongText, resizable: false, width: 40, id: "observaciones", name: "Obs", field: "observaciones", toolTip: "Observaciones" });
columnasProduccion.push({ editor: Slick.Editors.LongText, resizable: false, width: 40, id: "justificacion", name: "* Jus", field: "justificacion", toolTip: "Justificación (solo si se esta modificando el reporte de un dia pasado)" });
columnasProduccion.push({ formatter: Slick.Formatters.PercentCompleteBar, resizable: false, width: 38, id: "estado", name: "", field: "estado" });



var columnasResidual = new Array();

columnasResidual.push({ editor: Slick.Editors.Date, resizable: false, width: 75, id: "fecha", name: "Fecha", field: "fecha", toolTip: "DD / MM / YYYY" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "gravedadEspecifica", name: "Grav.Esp", field: "gravedadEspecifica", toolTip: "Gravedad Especifica" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 60, id: "volumen", name: "Volumen", field: "volumen", toolTip: "Volumen (MMPSCD)" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 48, id: "H2O", name: "C.H2O", field: "H2O", toolTip: "Cont. de H2O" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 67, id: "puntoRocio", name: "Punto Ro", field: "puntoRocio", toolTip: "Punto de Rocio (Dew Point Hc) ºF" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 63, id: "poderCalorifico", name: "Poder Ca", field: "poderCalorifico", toolTip: "Poder Calorífico (BTU/PC) @ 68ºF" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "N2", name: "N2", field: "N2" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "CO2", name: "CO2", field: "CO2" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "C1", name: "C1", field: "C1" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "C2", name: "C2", field: "C2" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "C3", name: "C3", field: "C3" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "iC4", name: "i-C4", field: "iC4" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "nC4", name: "n-C4", field: "nC4" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "iC5", name: "i-C5", field: "iC5" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "nC5", name: "n-C5", field: "nC5" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "nC6", name: "n-C6", field: "nC6" });
columnasResidual.push({ editor: Slick.Editors.Float, resizable: false, width: 55, id: "C7", name: "C7", field: "C7" });
columnasResidual.push({ editor: Slick.Editors.LongText, resizable: false, width: 40, id: "observaciones", name: "Obs", field: "observaciones", toolTip: "Observaciones" });
columnasResidual.push({ editor: Slick.Editors.LongText, resizable: false, width: 40, id: "justificacion", name: "* Jus", field: "justificacion", toolTip: "Justificación (solo si se esta modificando el reporte de un dia pasado)" });
columnasResidual.push({ formatter: Slick.Formatters.PercentCompleteBar, resizable: false, width: 38, id: "estado", name: "", field: "estado" });


var mostrarMensajeGrabarGrilla = function (funcionGrabar) {
    $.msgBox({
        title: tituloMsgBox,
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
};

var crearGridAlimento = function () {
    iniciarGrilla("alimento", gridAlimento, dataAlimento, columns, '#grdGasAlimento');
    $("#btnAlimentoDatos").off("click");
    $("#btnAlimentoDatos").on("click", function(e) {
        e.preventDefault();
        mostrarMensajeGrabarGrilla(function() {
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
                                beforeSend: function() { },
                                success: function(msg) {
                                    dataAlimento = gridAlimento.getData();
                                    $.each(msg.d, function(index, obj) {
                                        if (obj.RESULTADO >= 0) {
                                            actualizarFilaEstado(gridAlimento, dataAlimento, i, 100, "alimento", "estado");
                                            mostrarMensajeVentana(i, count, "info", "Se terminaron de registrar los datos.");
                                        } else {
                                            actualizarFilaEstado(gridAlimento, dataAlimento, i, 20, "alimento", "estado");
                                            mostrarMensajeVentana(0, 0, "alert", obj.MENSAJE);
                                        }
                                    });
                                },
                                error: function(xhr, ajaxOptions, thrownError) {
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
    });
    $("#btnAlimentoDeshacer").off("click");
    $("#btnAlimentoDeshacer").on("click", function (e) {
        e.preventDefault();
        mostrarMensajeBorrarGrilla("alimento");
    });
};
var crearGridProduccion = function () {
    iniciarGrilla("produccion", gridProduccion, dataProduccion, columnasProduccion, '#grdProduccion');
    $("#btnProduccionGrabar").off("click");
    $("#btnProduccionGrabar").on("click", function (e) {
        e.preventDefault();
        mostrarMensajeGrabarGrilla(function() {
            if ($("#hdnIdPlanta").val() != "0" && $("#hdnIdPlanta").val() != "") {
                dataProduccion = gridProduccion.getData();
                var count = gridProduccion.getDataLength();
                actualizarEstadoGrilla(gridProduccion, dataProduccion, count, 50, "estado");
                for (var i = 0; i < count; i++) {
                    if (DiferenciaFechas(dataProduccion[i]["fecha"], $("#txtProduccionFechaUltminoReporte").val()) > 0) {
                        $.ajax({
                                type: "POST",
                                url: 'WfVolumenesGLP.aspx/RegistrarVolumenesProduccionGLP',
                                data: JSON.stringify({
                                    planta: $("#hdnIdPlanta").val(),
                                    umProdGlp: $("#ddlProdGlpUnidadMedidad :selected").val(),
                                    umProdPropano: $("#ddlProdPropanoUnidadMedidad :selected").val(),
                                    umEntregaPropano: $("#ddlEntregaPropanoUnidadMedidad :selected").val(),
                                    umEntregaGlpCisterna: $("#ddlEntregaGlpCisternaUnidadMedidad :selected").val(),
                                    umEntregaGlpDucto: $("#ddlEntregaGlpDuctoUnidadMedidad :selected").val(),
                                    umSaldoGlp: $("#ddlSaldoGlpUnidadMedidad :selected").val(),
                                    umSaldoPropano: $("#ddlSaldoPropanoUnidadMedidad :selected").val(),
                                    umTemperatura: $("#ddlTemperatura :selected").val(),
                                    fecha: dataProduccion[i]["fecha"],
                                    produccionGLP: serializarValor(dataProduccion[i]["produccionGLP"]),
                                    produccionPropano: serializarValor(dataProduccion[i]["produccionPropano"]),
                                    entregaConsumoPropano: serializarValor(dataProduccion[i]["entregaConsumoPropano"]),
                                    entregaGlpCisterna: serializarValor(dataProduccion[i]["entregaGlpCisterna"]),
                                    entregaGlpDucto: serializarValor(dataProduccion[i]["entregaGlpDucto"]),
                                    saldoGLP: serializarValor(dataProduccion[i]["saldoGLP"]),
                                    saldoPropano: serializarValor(dataProduccion[i]["saldoPropano"]),
                                    gravedadEspecifica: serializarValor(dataProduccion[i]["gravedadEspecifica"]),
                                    tvr: serializarValor(dataProduccion[i]["TVR"]),
                                    temperatura: serializarValor(dataProduccion[i]["temperatura"]),
                                    c2: serializarValor(dataProduccion[i]["C2"]),
                                    c3: serializarValor(dataProduccion[i]["C3"]),
                                    iC4: serializarValor(dataProduccion[i]["iC4"]),
                                    nC4: serializarValor(dataProduccion[i]["nC4"]),
                                    iC5: serializarValor(dataProduccion[i]["iC5"]),
                                    nC5: serializarValor(dataProduccion[i]["nC5"]),
                                    obs: dataProduccion[i]["observaciones"] || "",
                                    just: dataProduccion[i]["justificacion"] || ""
                                }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                cache: false,
                                async: false,
                                beforeSend: function() { },
                                success: function(msg) {
                                    dataProduccion = gridProduccion.getData();
                                    $.each(msg.d, function(index, obj) {
                                        if (obj.RESULTADO >= 0) {
                                            actualizarFilaEstado(gridProduccion, dataProduccion, i, 100, "produccion", "estado");
                                            mostrarMensajeVentana(i, count, "info", "Se terminaron de registrar los datos.");
                                        } else {
                                            actualizarFilaEstado(gridProduccion, dataProduccion, i, 20, "produccion", "estado");
                                            mostrarMensajeVentana(0, 0, "alert", obj.MENSAJE);
                                        }
                                    });
                                },
                                error: function(xhr, ajaxOptions, thrownError) {
                                    dataProduccion = gridProduccion.getData();
                                    actualizarFilaEstado(gridProduccion, dataProduccion, i, 20, "produccion", "estado");
                                    mostrarMensajeVentana(i, count, "error");
                                }
                            });
                    } else {
                        mostrarMensaje("alert", "No se guardaran los datos del día " + dataProduccion[i]["fecha"] + ", porque estos ya se encuentran registrados en el sistema");
                    }
                }
            } else {
                mostrarMensaje("alert", "Primero debe seleccionar una planta");
            }
        });
    });
    $("#btnProduccionLimpiar").off("click");
    $("#btnProduccionLimpiar").on("click", function (e) {
        e.preventDefault();
        mostrarMensajeBorrarGrilla("produccion");
    });
    $("#btnProduccionFecha").click();
};
var crearGridResidual = function () {
    iniciarGrilla("residual", gridResidual, dataResidual, columnasResidual, '#grdResidual');
    $("#btnResidualGrabar").off("click");
    $("#btnResidualGrabar").on("click", function (e) {
        e.preventDefault();
        mostrarMensajeGrabarGrilla(function() {
            if ($("#hdnIdPlanta").val() != "0" && $("#hdnIdPlanta").val() != "") {
                dataResidual = gridResidual.getData();
                var count = gridResidual.getDataLength();
                actualizarEstadoGrilla(gridResidual, dataResidual, count, 50, "estado");
                for (var i = 0; i < count; i++) {
                    if (DiferenciaFechas(dataResidual[i]["fecha"], $("#txtResidualFechaUltminoReporte").val()) > 0) {
                        $.ajax({
                                type: "POST",
                                url: 'WfVolumenesGLP.aspx/RegistrarVolumenesResidualGLP',
                                data: JSON.stringify({
                                    planta: $("#hdnIdPlanta").val(),
                                    umReslVolumen: $("#ddlResidualVolUnidadMedida :selected").val(),
                                    umResPuntoRocio: $("#ddlResidualPuntoRocio :selected").val(),
                                    umResPoderCalorifico: $("#ddlResidualPoderCalorifico :selected").val(),
                                    fecha: dataResidual[i]["fecha"],
                                    gravedadEspecifica: serializarValor(dataResidual[i]["gravedadEspecifica"]),
                                    volumen: serializarValor(dataResidual[i]["volumen"]),
                                    h2O: serializarValor(dataResidual[i]["H2O"]),
                                    puntoRocio: serializarValor(dataResidual[i]["puntoRocio"]),
                                    poderCalorifico: serializarValor(dataResidual[i]["poderCalorifico"]),
                                    n2: serializarValor(dataResidual[i]["N2"]),
                                    co2: serializarValor(dataResidual[i]["CO2"]),
                                    c1: serializarValor(dataResidual[i]["C1"]),
                                    c2: serializarValor(dataResidual[i]["C2"]),
                                    c3: serializarValor(dataResidual[i]["C3"]),
                                    iC4: serializarValor(dataResidual[i]["iC4"]),
                                    nC4: serializarValor(dataResidual[i]["nC4"]),
                                    iC5: serializarValor(dataResidual[i]["iC5"]),
                                    nC5: serializarValor(dataResidual[i]["nC5"]),
                                    nC6: serializarValor(dataResidual[i]["nC6"]),
                                    c7: serializarValor(dataResidual[i]["C7"]),
                                    obs: dataResidual[i]["observaciones"] || "",
                                    just: dataResidual[i]["justificacion"] || ""
                                }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                cache: false,
                                async: false,
                                beforeSend: function() { },
                                success: function(msg) {
                                    dataResidual = gridResidual.getData();
                                    $.each(msg.d, function(index, obj) {
                                        if (obj.RESULTADO >= 0) {
                                            actualizarFilaEstado(gridResidual, dataResidual, i, 100, "residual", "estado");
                                            mostrarMensajeVentana(i, count, "info", "Se terminaron de registrar los datos.");
                                        } else {
                                            actualizarFilaEstado(gridResidual, dataResidual, i, 20, "residual", "estado");
                                            mostrarMensajeVentana(0, 0, "alert", obj.MENSAJE);
                                        }
                                    });
                                },
                                error: function(xhr, ajaxOptions, thrownError) {
                                    dataResidual = gridResidual.getData();
                                    actualizarFilaEstado(gridResidual, dataResidual, i, 20, "residual", "estado");
                                    mostrarMensajeVentana(i, count, "error");
                                }
                            });
                    } else {
                        mostrarMensaje("alert", "No se guardaran los datos del día " + dataResidual[i]["fecha"] + ", porque estos ya se encuentran registrados en el sistema");
                    }
                }
            } else {
                mostrarMensaje("alert", "Primero debe seleccionar una planta");
            }
        });
    });
    $("#btnResidualLimpiar").off("click");
    $("#btnResidualLimpiar").on("click", function (e) {
        e.preventDefault();
        mostrarMensajeBorrarGrilla("residual");
    });
    $("#btnResidualFecha").click();
};

/*Tipo mensaje es equivalente a los tipos de mensaje del JQuery.msgBox, por defecto se mostrara como error*/
var mostrarMensajeVentana = function (i, count, tipoMensaje, contenido) {
    if (i == count - 1 || (i == 0 && count == 0)) {
        if (tipoMensaje == undefined) tipoMensaje = "error";
        if (contenido == undefined) contenido = "Algunos datos no pudieron registrarse en el sistema";
        $.msgBox({
            title: tituloMsgBox,
            content: contenido,
            type: tipoMensaje,
            buttons: [{ value: "Aceptar"}],
            opacity: 0.1
        });
    }
};
var mostrarMensaje = function (tipoMensaje, contenido) {
    if (tipoMensaje == undefined) tipoMensaje = "error";
    if (contenido == undefined) contenido = "Error al generar el mensaje.";
    $.msgBox({
        title: tituloMsgBox,
        content: contenido,
        type: tipoMensaje,
        buttons: [{ value: "Aceptar"}],
        opacity: 0.1
    });
};
var dGrid = "";
var mostrarMensajeBorrarGrilla = function (grid) {
    $.msgBox({
        title: tituloMsgBox,
        content: "Quiere borrar todos los datos de la grilla?",
        type: "confirm",
        buttons: [{ value: "Aceptar" }, { value: "Cancelar"}],
        success: function (result) {
            if (result == "Aceptar") {
                switch (grid) {
                    case "alimento": dataAlimento = []; dataAlimento[0] = {};
                        gridAlimento.setData(dataAlimento);
                        gridAlimento.render(); break;
                    case "produccion": dataProduccion = []; dataProduccion[0] = {};
                        gridProduccion.setData(dataProduccion);
                        gridProduccion.render(); break;
                    case "residual": dataResidual = []; dataResidual[0] = {};
                        gridResidual.setData(dataResidual);
                        gridResidual.render(); break;
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
        case "alimento": gridAlimento = vGrid; gridAlimento.setData(vData); gridAlimento.render(); break;
        case "produccion": gridProduccion = vGrid; gridProduccion.setData(vData); gridProduccion.render(); break;
        case "residual": gridResidual = vGrid; gridResidual.setData(vData); gridResidual.render(); break;
    }
};
var iniciarGrilla = function (nGrid, vGrid, vData, vColumnas, vIdDiv) {
    if (vGrid == undefined || vGrid.getDataLength() < 1) {
        vData[0] = { };
        vGrid = new Slick.Grid(vIdDiv, vData, vColumnas, options);
        vGrid.setSelectionModel(new Slick.CellSelectionModel());
        vGrid.registerPlugin(new Slick.CellExternalCopyManager(pluginOptions));
        vGrid.setActiveCell(0, 0);
    }
    switch (nGrid) {
        case "alimento": gridAlimento = vGrid; break;
        case "produccion": gridProduccion = vGrid; break;
        case "residual": gridResidual = vGrid; break;
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

var serializarValor = function (valor) {
    var erComma = /,/g;
    if (erComma.test(valor)) {
        valor = valor.replace('.', '').replace(',', '.'); ;
    }
    return parseFloat(valor) || 0;
};
function fecha(cadena) {
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
}
function verificaPlanta() {
    if ($.trim($("#hdnIdPlanta").val()) != "")
        return true;
    else {
        mostrarMensaje("alert", "Seleccione una PLANTA.");
        return false;
    }
}
function verificaPlantaProde() {
    if (cblPlantas.GetSelectedValues().length > 0)
        return true;
    else {
        mostrarMensaje("alert", "Seleccione una PLANTA.");
        return false;
    }
}
var DiferenciaFechas = function(cadenaFecha1, cadenaFecha2) {
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
};
$(function () {
    var ancho = $("#cantCuerpo").width() - 40;
    $("#grdGasAlimento").width(ancho);
    $("#grdProduccion").width(ancho);
    $("#grdResidual").width(ancho);
    crearGridAlimento();
    $('#cmbPlantas').on("change", function() {
        $("#hdnIdPlanta").val($('#cmbPlantas :selected').val());
        buscarCampos();
    });
    $('#cmbPlantas').focus();
    /*$("#txtPlantas").focus();
    $("#txtPlantas").autocomplete({
    minLength: 2,
    autoFocus: true,
    source: function (request, response) {
    $.ajax({
    url: "WfVolumenesGLP.aspx/BuscarEntidad",
    data: "{ entidad: '" + request.term + "' }",
    dataType: "json",
    type: "POST",
    cache: false,
    contentType: "application/json; charset=utf-8",
    dataFilter: function (data) { return data; },
    beforeSend: function () { $("#hdnIdPlanta").val(""); },
    success: function (data) {
    response($.map(data.d, function (item) {
    return {
    label: item.nombre.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + $.ui.autocomplete.escapeRegex(request.term) + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<strong>$1</strong>"),
    value: item.nombre,
    idEntidad: item.idEntidad
    };
    }));
    },
    error: function (xhr, textStatus, errorThrown) {
    /*alert(textStatus);*
    }
    });
    },
    select: function (event, ui) {
    $("#hdnIdPlanta").val(ui.item.idEntidad);
    $("#txtPlantas").val(ui.item.value);
    buscarCampos();
    return false;
    }
    }).data("ui-autocomplete")._renderItem = function (ul, item) {
    return $("<li>")
    .append("<a>" + item.label + "</a>")
    .appendTo(ul);
    };*/
    $("#ddlCorriente").on("change", function () {
        buscarFechaUltimoReporte();
    });
    $("#btnAlimentoCampo").on("click", function (e) {
        e.preventDefault();
        buscarCampos();
    });
    $("#btnAlimentoFecha").on("click", function (e) {
        e.preventDefault();
        buscarFechaUltimoReporte();
    });
    $("#btnProduccionFecha").on("click", function (e) {
        e.preventDefault();
        buscarFechaUltimoReporteProduccion();
    });
    $("#btnResidualFecha").on("click", function (e) {
        e.preventDefault();
        buscarFechaUltimoReporteResidual();
    });
    $("#lnkGenerarReporte").on("click", function (e) {
        if ($.trim($("#hdnIdPlanta").val()) != "") {
            e.preventDefault();
            pgReporte.PerformCallback($("#hdnIdPlanta").val() + "|" + txtFechaIni.GetText() + "|" + txtFechaFin.GetText());
        } else {
            mostrarMensaje("alert", "Seleccione una PLANTA.");
        }
    });
    $("#lnkGenerarReporteSeg").on("click", function (e) {
        e.preventDefault();
        if ($.trim($("#hdnIdPlanta").val()) != "") {
            pgRptSeguimiento.PerformCallback($("#hdnIdPlanta").val() + "|" + txtFechaIniSeg.GetText() + "|" + txtFechaFinSeg.GetText());
        } else {
            mostrarMensaje("alert", "Seleccione una PLANTA.");
        }
    });
    $("#ddlTipoGrafico").on("change", function () {
        chartRptSeguimiento.PerformCallback('chartChanged|' + $("#ddlTipoGrafico :selected").val());
    });
    $("#lnkGenerarReporteProde").on("click", function (e) {
        e.preventDefault();
        var idPlantas = "", sep = "";
        for (var i = 0; i < cblPlantas.GetSelectedValues().length; i++) {
            idPlantas += sep + cblPlantas.GetSelectedValues()[i];
            sep = ";";
        }
        if (idPlantas != "") {
            pgRptProde.PerformCallback(idPlantas + "|" + txtFechaIniProde.GetText() + "|" + txtFechaFinProde.GetText() + "|" + $("#ddlUnidadMedidaProde :selected").val());
        } else {
            mostrarMensaje("alert", "Seleccione una PLANTA.");
        }
    });
    $("#ddlTipoGraficoProde").on("change", function () {
        chartRptProde.PerformCallback('chartChanged|' + $("#ddlTipoGraficoProde :selected").val());
    });
    var buscarCampos = function () {
        $.ajax({
            url: "WfVolumenesGLP.aspx/BuscarCampos",
            data: "{ idEntidad: " + $("#hdnIdPlanta").val() + " }",
            dataType: "json",
            type: "POST",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            beforeSend: function () {
                $("#ddlCorriente").html("<option>Buscando campos...</option>");
                $('#btnAlimentoCampo').find('span').removeClass('ui-icon-refresh').removeClass('icon').addClass('ui-icon-loading');
            },
            success: function (data) {
                var opciones = "";
                if (data.d.length > 1)
                    opciones = "<option value='0'>-- Seleccione un campo--</option>";
                $.each(data.d, function (index, obj) {
                    opciones += "<option value='" + obj.idCampo + "'>" + obj.nombre + "</option>";
                });
                $("#ddlCorriente").html(opciones);
                $('#btnAlimentoCampo').find('span').addClass('ui-icon-refresh').addClass('icon').removeClass('ui-icon-loading');
                buscarFechaUltimoReporte();
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#btnAlimentoCampo').find('span').addClass('ui-icon-refresh').addClass('icon').removeClass('ui-icon-loading');
                $("#ddlCorriente").html("<option>-- No se puede conectar con el servicio --</option>");
            }
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
    var buscarFechaUltimoReporteProduccion = function () {
        if ($("#hdnIdPlanta").val() != "0" && $("#hdnIdPlanta").val() != "") {
            $.ajax({
                url: "WfVolumenesGLP.aspx/FechaUltimoReporteProduccion",
                data: "{ idEntidad: " + $("#hdnIdPlanta").val() + " }",
                dataType: "json",
                type: "POST",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtProduccionFechaUltminoReporte").val("");
                    $('#btnProduccionFecha').find('span').removeClass('ui-icon-refresh').removeClass('icon').addClass('ui-icon-loading');
                },
                success: function (data) {
                    $.each(data.d, function (index, obj) {
                        $("#txtProduccionFechaUltminoReporte").val(obj.fecha);
                    });
                    $('#btnProduccionFecha').find('span').addClass('ui-icon-refresh').addClass('icon').removeClass('ui-icon-loading');
                },
                error: function (xhr, textStatus, errorThrown) {
                    $("#txtProduccionFechaUltminoReporte").val("Error");
                    $('#btnProduccionFecha').find('span').addClass('ui-icon-refresh').addClass('icon').removeClass('ui-icon-loading');
                }
            });
        }
    };
    var buscarFechaUltimoReporteResidual = function () {
        if ($("#hdnIdPlanta").val() != "0" && $("#hdnIdPlanta").val() != "") {
            $.ajax({
                url: "WfVolumenesGLP.aspx/FechaUltimoReporteResidual",
                data: "{ idEntidad: " + $("#hdnIdPlanta").val() + " }",
                dataType: "json",
                type: "POST",
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtResidualFechaUltminoReporte").val("");
                    $('#btnResidualFecha').find('span').removeClass('ui-icon-refresh').removeClass('icon').addClass('ui-icon-loading');
                },
                success: function (data) {
                    $.each(data.d, function (index, obj) {
                        $("#txtResidualFechaUltminoReporte").val(obj.fecha);
                    });
                    $('#btnResidualFecha').find('span').addClass('ui-icon-refresh').addClass('icon').removeClass('ui-icon-loading');
                },
                error: function (xhr, textStatus, errorThrown) {
                    $("#txtResidualFechaUltminoReporte").val("Error");
                    $('#btnResidualFecha').find('span').addClass('ui-icon-refresh').addClass('icon').removeClass('ui-icon-loading');
                }
            });
        }
    };
});