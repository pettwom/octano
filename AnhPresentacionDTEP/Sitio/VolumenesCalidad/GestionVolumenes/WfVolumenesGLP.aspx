<%@ Page Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="WfVolumenesGLP.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionVolumenes.WfVolumenesGLP" %>

<%@ Register Assembly="DevExpress.PivotGrid.v15.1.Core, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.PivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Charts.v15.1.Core, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Charts"  TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register TagPrefix="dxcharts" Namespace="DevExpress.XtraCharts" Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../../UI/css/SlickGrid/slick.grid.css" />
    <link rel="stylesheet" type="text/css" href="../../../UI/css/SlickGrid/slick.grid.excel.css" />
    <link rel="stylesheet" type="text/css" href="../../../UI/css/jquery-ui-1.10.3.custom.min.css" />
    <link rel="stylesheet" type="text/css" href="../../../UI/css/msgBoxLight.css" />
    <script type="text/javascript" src="../../../UI/js/jquery.msgBox.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/lib/jquery.event.drag-2.2.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/slick.core.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/plugins/slick.cellrangedecorator.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/plugins/slick.cellrangeselector.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/plugins/slick.cellexternalcopymanager.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/plugins/slick.cellselectionmodel.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/slick.editors.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/slick.formatters.js"></script>
    <script type="text/javascript" src="../../../UI/js/SlickGrid/slick.grid.js"></script>
    <script type="text/javascript" src="../../../UI/js/VolumenesCalidad/GestionVolumenes/volumenesGLP.js"></script>
    <!-- Acción sobre el DropDownList Entidad Operadora -->
    <script type="text/javascript">
        //$(document).ready(function () { $("#ddlEntidadOperadora").change(function () { if ($(this).val() == "0") { $("#cmbPlantas option").remove(); var e = $("#cmbPlantas"); e.append(new Option("-- SELECCIONAR UNA PLANTA --", 0)); return } var t = "{'decIdEntidad': " + $(this).val() + "}"; $.ajax({ type: "POST", url: "WfVolumenesGLP.aspx/LLenarPlantasWeb", data: t, contentType: "application/json; charset=utf-8", dataType: "json", cache: false, success: function (e) { var t = e.d; if (t.length > 0) { $("#cmbPlantas option").remove(); var n = $("#cmbPlantas"); n.append(new Option("-- SELECCIONAR UNA PLANTA --", 0)); for (var r in t) { n.append(new Option(t[r].NOMBRE, t[r].ID_ENTIDAD)) } } }, error: function (e) { alert("ERROR " + e.status + " " + e.statusText) } }) }) })

        $(document).ready(function () {
            //pcOpciones.
            //            $("#pcOpciones_AT0").hide();
            //            $("#opcionesUno").hide();
            //            $("#grdGasAlimento").hide();


            if ($("#ddlEntidadOperadora").length > 0) {
                $("#grdGasAlimento").hide();
            } else {
                $("#grdGasAlimento").show();
            }
        });
    </script>
    <style type="text/css">
        .oculto
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="opcionesReporte">
        <table class="tblEditar" width="100%">
            <tr>
                <td class="txtRight Hydro_Titulo_Verde_Menor" style="text-align: left">
                    <div id="divEntidad" runat="server">
                        Entidad Operadora:
                        <asp:DropDownList ID="ddlEntidadOperadora" runat="server" AutoPostBack="True" ClientIDMode="Static"
                            Visible="False">
                            <asp:ListItem Value="0">--SELECIONAR ENTIDAD--</asp:ListItem>
                            <asp:ListItem Value="36247">YPFB Chaco S.A.</asp:ListItem>
                            <asp:ListItem Value="36276">YPFB Andina</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td class="txtRight Hydro_Titulo_Verde_Menor" style="text-align: left">
                    Planta:
                    <asp:DropDownList runat="server" ID="cmbPlantas" ClientIDMode="Static" EnableViewState="False">
                        <asp:ListItem Value="0">-- SELECCIONAR UNA PLANTA --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField runat="server" ID="hdnIdPlanta" ClientIDMode="Static" EnableViewState="False" />
                </td>
                <td style="width: 150px;">
                </td>
                <td class="txtCenter Hydro_Titulo_Verde_Menor">
                    Producto: GLP - Gas Licuado de Petr&oacute;leo
                </td>
            </tr>
        </table>
    </div>
    <div>
    <dx:ASPxPageControl ID="pcOpciones" ClientIDMode="Static" runat="server" Theme="Default"
        ClientInstanceName="pcOpciones" ActiveTabIndex="3" Width="100%">
        <ClientSideEvents ActiveTabChanged="function(s, e) { 
            if (e.tab.name=='tabAlimento') crearGridAlimento();
            else if (e.tab.name=='tabProduccion') crearGridProduccion();
            else if (e.tab.name=='tabResidual') crearGridResidual(); }" />
        <TabPages>
            <dx:TabPage Name="tabAlimento" Text="Gas de Alimento" ToolTip="Registrar Gas de alimento">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <div class="divPanelExcel">
                            <div class="opcionesReporte" id="opcionesUno">
                                <table>
                                    <tr>
                                        <td class="txtRight" style="width: 275px">
                                            CORRIENTE/CAMPO:
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlCorriente" ClientIDMode="Static" />
                                            <a href="#" class="Hydro_Boton_Actualizar_Excel" id="btnAlimentoCampo" runat="server"
                                                clientidmode="Static"><span class="icon ui-icon-refresh" title="Actualizar la lista de las corrientes y/o campos">
                                                </span></a>
                                        </td>
                                        <td rowspan="4" class="txtCenter" style="width: 300px; padding-left: 30px">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a href="#" class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" id="btnAlimentoDatos">
                                                        <span class="icon ui-icon-disk"></span>
                                                        <span class="label">Grabar</span>
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <a class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" id="btnAlimentoDeshacer">
                                                            <span class="icon ui-icon-trash"></span>
                                                            <span class="label">Limpiar grilla</span>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <span class="Hydro_Titulo_Verde_Menor">Fecha del &uacute;ltimo reporte:</span>
                                            <asp:TextBox runat="server" ID="txtFechaUltminoReporte" Width="75" Style="font-weight: bold !important"
                                                ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                            <a href="#" class="Hydro_Boton_Actualizar_Excel" id="btnAlimentoFecha" runat="server"
                                                clientidmode="Static"><span class="icon ui-icon-refresh" title="Fecha del último reporte">
                                                </span></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            VOLUMEN (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <%--<asp:DropDownList runat="server" ID="ddlUnidadMedVolumen" ClientIDMode="Static" 
                                                umed="MMPCD" Width="130px" />--%>
                                            <asp:DropDownList runat="server" ID="ddlUnidadMedVolumen" ClientIDMode="Static" Width="130px">
                                                <asp:ListItem>MMPCD</asp:ListItem>
                                                <asp:ListItem>m3</asp:ListItem>
                                                <asp:ListItem>pie3</asp:ListItem>
                                                <asp:ListItem>BBL</asp:ListItem>
                                                <asp:ListItem>L</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            CONTENIDO DE GLP (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlUnidadMedGLP" ClientIDMode="Static" umed="BBL/MMPCD"
                                                Width="130px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            PODER CALOR&Iacute;FICO (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlUnidadMedPoderCalor" ClientIDMode="Static"
                                                umed="BTU/PC" Width="130px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 100%;">
                                <div id="grdGasAlimento" style="height: auto;">
                                </div>
                            </div>
                            <br clear="all" />
                            <em>(*) </em>
                            <em style="color: rgb(0, 0, 0); font-family: Verdana; font-size: 11px; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">Los valores que reporte deben ser números, caso contrario se reportarán con valor de 0 (cero). Por favor verifique antes de Grabar.</em></div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tabProduccion" Text="Producción" ToolTip="Registrar producción de la planta">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                        <div class="divPanelExcel">
                            <div class="opcionesReporte">
                                <table>
                                    <tr>
                                        <td class="txtRight">
                                            Producci&oacute;n GLP (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlProdGlpUnidadMedidad" ClientIDMode="Static"
                                                              Width="70px" >
                                                <asp:ListItem>TMD</asp:ListItem>
                                                <asp:ListItem>MCD</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="txtRight">
                                            Entrega GLP/DUCTO (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlEntregaGlpDuctoUnidadMedidad" ClientIDMode="Static"
                                                umed="TMD" Width="70px" >
                                                <asp:ListItem>TMD</asp:ListItem>
                                                <asp:ListItem>MCD</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="4" class="txtCenter" style="width: 300px; padding-left: 30px">
                                            <center>
                                                <table>
                                                <tr>
                                                    <td>
                                                        <a class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" id="btnProduccionGrabar"><span class="icon ui-icon-disk"></span><span class="label">Grabar</span></a> 
                                                    </td>
                                                    <td>
                                                        <a class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" id="btnProduccionLimpiar"><span class="icon ui-icon-trash"></span><span class="label">Limpiar grilla</span></a>
                                                    </td>
                                                </tr>
                                                </table>
                                            </center>
                                            <br />
                                            <br />
                                            <span class="Hydro_Titulo_Verde_Menor">Fecha del &uacute;ltimo reporte:</span>
                                            <asp:TextBox runat="server" ID="txtProduccionFechaUltminoReporte" Width="75" Style="font-weight: bold !important"
                                                ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                            <a href="#" class="Hydro_Boton_Actualizar_Excel" id="btnProduccionFecha" runat="server"
                                                clientidmode="Static"><span class="icon ui-icon-refresh" title="Fecha del último reporte">
                                                </span></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            Producción PROPANO (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlProdPropanoUnidadMedidad" ClientIDMode="Static"
                                                umed="TMD" Width="70px" >
                                                <asp:ListItem>TMD</asp:ListItem>
                                                <asp:ListItem>MCD</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="txtRight">
                                            Saldo GLP (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <%--<asp:DropDownList runat="server" ID="ddlSaldoGlpUnidadMedidad" ClientIDMode="Static"
                                                umed="TM" Width="70px" />--%>
                                            <asp:DropDownList ID="ddlSaldoGlpUnidadMedidad" runat="server" ClientIDMode="Static">
                                                <asp:ListItem>Tm</asp:ListItem>
                                                <asp:ListItem>Kg</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            Entrega/Consumo PROPANO (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlEntregaPropanoUnidadMedidad" ClientIDMode="Static"
                                                umed="TMD" Width="70px" >
                                                <asp:ListItem>TMD</asp:ListItem>
                                                <asp:ListItem>MCD</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="txtRight">
                                            Saldo PROPANO (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <%--<asp:DropDownList runat="server" ID="ddlSaldoPropanoUnidadMedidad" ClientIDMode="Static"
                                                umed="TM" Width="70px" />--%>
                                            <asp:DropDownList ID="ddlSaldoPropanoUnidadMedidad" runat="server" ClientIDMode="Static">
                                                <asp:ListItem>Tm</asp:ListItem>
                                                <asp:ListItem>Kg</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            Entrega GLP/CISTERNA (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlEntregaGlpCisternaUnidadMedidad" ClientIDMode="Static"
                                                umed="TMD" Width="70px" >
                                                <asp:ListItem>TMD</asp:ListItem>
                                                <asp:ListItem>MCD</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="txtRight">
                                            Temperatura (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlTemperatura" ClientIDMode="Static" umed="°F"
                                                Width="70px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            Rendimiento Produccion GLP (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlRendProdGlp" ClientIDMode="Static"
                                                umed="% vol" Width="70px" >
                                                <asp:ListItem>% de Volumen</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="txtRight">
                                            Gas Combustible (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlGasCombustible" ClientIDMode="Static" umed="MMPCD"
                                                Width="70px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            Quema Gas (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                             <asp:DropDownList runat="server" ID="ddlQuemaGas" ClientIDMode="Static"
                                                 Width="70px" >
                                                <asp:ListItem>MMPCD</asp:ListItem>
                                                <asp:ListItem>MMMCD</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="txtRight">
                                            Gasolina Natural (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlGasolinaNatural" ClientIDMode="Static" umed="BPD"
                                                Width="70px" />
                                        </td>   
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 100%;">
                                <div id="grdProduccion" style="height: auto;">
                                </div>
                            </div>
                            <br clear="all" />
                            <em>(*) La justificaci&oacute;n solo es requerida cuando el valor registrado de la produccion
                                de GLP es menor al valor registrado de la Nominación de la Producción y Demanda
                                del mes</em>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tabResidual" Text="Gas Residual" ToolTip="Registrar Gas residual">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                        <div class="divPanelExcel">
                            <div class="opcionesReporte">
                                <table>
                                    <tr>
                                        <td class="txtRight" style="width: 295px">
                                            Volumen (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <%--<asp:DropDownList runat="server" ID="ddlResidualVolUnidadMedida" ClientIDMode="Static"
                                                umed="MMPCD" Width="100px" />--%>
                                            <asp:DropDownList ID="ddlResidualVolUnidadMedida" runat="server" ClientIDMode="Static"
                                                Width="100px">
                                                <asp:ListItem>MMPCD</asp:ListItem>
                                                <asp:ListItem>m3</asp:ListItem>
                                                <asp:ListItem>pie3</asp:ListItem>
                                                <asp:ListItem>BBL</asp:ListItem>
                                                <asp:ListItem>L</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3" class="txtCenter" style="width: 300px; padding-left: 30px">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" id="btnResidualGrabar">
                                                            <span class="icon ui-icon-disk"></span>
                                                            <span class="label">Grabar</span>
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <a class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" id="btnResidualLimpiar">
                                                            <span class="icon ui-icon-trash"></span>
                                                            <span class="label">Limpiar grilla</span>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                                                                        
                                            <br />
                                            <br />
                                            <span class="Hydro_Titulo_Verde_Menor">Fecha del &uacute;ltimo reporte:</span>
                                            <asp:TextBox runat="server" ID="txtResidualFechaUltminoReporte" Width="75" Style="font-weight: bold !important"
                                                ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                            <a href="#" class="Hydro_Boton_Actualizar_Excel" id="btnResidualFecha" runat="server"
                                                clientidmode="Static"><span class="icon ui-icon-refresh" title="Fecha del último reporte">
                                                </span></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            Punto de rocio (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlResidualPuntoRocio" ClientIDMode="Static"
                                                umed="°F" Width="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtRight">
                                            Poder calor&iacute;fico (Unidad de Medida):
                                        </td>
                                        <td class="txtLeft">
                                            <asp:DropDownList runat="server" ID="ddlResidualPoderCalorifico" ClientIDMode="Static"
                                                umed="BTU/PC" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 100%;">
                                <div id="grdResidual" style="height: auto;">
                                </div>
                            </div>
                            <br clear="all" />
                            <em>(*) </em>
                            <em style="color: rgb(0, 0, 0); font-family: Verdana; font-size: 11px; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">Los valores que reporte deben ser números, caso contrario se reportarán con valor de 0 (cero). Por favor verifique antes de Grabar.</em></div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tabReporte" Text="Reportes" ToolTip="Reportes de todos los datos registrados por planta">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                        <div class="divPanelExcel">
                            <div class="opcionesReporte">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>Desde</strong>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtFechaIni" ClientInstanceName="txtFechaIni" runat="server"
                                                Theme="Default" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                ClientIDMode="Static">
                                                <CalendarProperties ClearButtonText="Limpiar" ShowClearButton="False" ShowWeekNumbers="False"
                                                    TodayButtonText="Hoy">
                                                    <PrevYearImage ToolTip="Anterior año">
                                                    </PrevYearImage>
                                                    <PrevMonthImage ToolTip="Anterior mes">
                                                    </PrevMonthImage>
                                                    <NextMonthImage ToolTip="Siguiente mes">
                                                    </NextMonthImage>
                                                    <NextYearImage ToolTip="Siguiente año">
                                                    </NextYearImage>
                                                </CalendarProperties>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaFin.GetCalendar();
                                                      cal.minDate = s.GetDate();
                                                      cal.Update();
                                                      }"></ClientSideEvents>
                                                <ValidationSettings CausesValidation="True" Display="Dynamic" EnableCustomValidation="True"
                                                    ErrorText="Fecha de inicio requerida" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                    <RequiredField ErrorText="Fecha de inicio requerida" IsRequired="True" />
                                                    <RequiredField IsRequired="True" ErrorText="Fecha de inicio requerida"></RequiredField>
                                                </ValidationSettings>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaFin.GetCalendar();
                                                      cal.minDate = s.GetDate();
                                                      cal.Update();
                                                      }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <strong>hasta</strong>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtFechaFin" ClientInstanceName="txtFechaFin" runat="server"
                                                Theme="Default" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                ClientIDMode="Static">
                                                <CalendarProperties ClearButtonText="Limpiar" ShowClearButton="False" ShowWeekNumbers="False"
                                                    TodayButtonText="Hoy">
                                                    <PrevYearImage ToolTip="Anterior año">
                                                    </PrevYearImage>
                                                    <PrevMonthImage ToolTip="Anterior mes">
                                                    </PrevMonthImage>
                                                    <NextMonthImage ToolTip="Siguiente mes">
                                                    </NextMonthImage>
                                                    <NextYearImage ToolTip="Siguiente año">
                                                    </NextYearImage>
                                                </CalendarProperties>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaIni.GetCalendar();
                                                      cal.maxDate = s.GetDate();
                                                      cal.Update();
                                                 }"></ClientSideEvents>
                                                <ValidationSettings CausesValidation="True" Display="Dynamic" EnableCustomValidation="True"
                                                    ErrorText="Fecha de finalización requerida" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                    <RequiredField ErrorText="Fecha de finalización requerida" IsRequired="True" />
                                                    <RequiredField IsRequired="True" ErrorText="Fecha de finalizaci&#243;n requerida">
                                                    </RequiredField>
                                                </ValidationSettings>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaIni.GetCalendar();
                                                      cal.maxDate = s.GetDate();
                                                      cal.Update();
                                                 }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <a id="lnkGenerarReporte" runat="server" class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel"
                                                href="javascript:void(0)" clientidmode="Static"><span class="icon ui-icon-gear">
                                                </span><span class="label">Generar reporte</span></a>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkExcel" ClientIDMode="Static" runat="server" CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel"
                                                OnClick="lnkExcel_Click" OnClientClick="return verificaPlanta(this);" disabled="disabled"><span class="icon ui-icon-excel"></span><span class="label">Exportar a Excel</span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkPdf" ClientIDMode="Static" runat="server" CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel"
                                                OnClick="lnkPdf_Click" OnClientClick="return verificaPlanta(this);" disabled="disabled"><span class="icon ui-icon-pdf"></span><span class="label">Exportar a PDF</span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkWord" ClientIDMode="Static" runat="server" CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel"
                                                OnClick="lnkWord_Click" OnClientClick="return verificaPlanta(this);" disabled="disabled"><span class="icon ui-icon-word"></span><span class="label">Exportar a Word</span></asp:LinkButton>
                                        </td>
                                    </tr>

                                </table>
                                <table>
                                    <tr>
                                        <td class="txtRight">
                                            VOLUMEN (Unidad de Medida):
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlUnidadMedidaDestinoReportesVolumen" ClientIDMode="Static"
                                                Width="120px">
                                                <asp:ListItem>MMPCD</asp:ListItem>
                                                <asp:ListItem>m3</asp:ListItem>
                                                <asp:ListItem>pie3</asp:ListItem>
                                                <asp:ListItem>BBL</asp:ListItem>
                                                <asp:ListItem>L</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="txtRight">
                                            PESO (Unidad de Medida):
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlUnidadMedidaDestinoReportesPeso" runat="server" ClientIDMode="Static">
                                                <asp:ListItem>Tm</asp:ListItem>
                                                <asp:ListItem>Kg</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Reporte</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoReporte" runat="server" ClientIDMode="Static">
                                                <asp:ListItem>Todos</asp:ListItem>
                                                <asp:ListItem>Corriestes de Produccion</asp:ListItem>
                                                <asp:ListItem>Produccion</asp:ListItem>
                                                <asp:ListItem>Gas Residual</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <dx:ASPxPivotGrid ID="pgReporte" ClientInstanceName="pgReporte" runat="server" ClientIDMode="Static"
                                OnCustomCallback="pgReporte_CustomCallback" Theme="Default">
                                <ClientSideEvents EndCallback="function(s,e){$('#lnkExcel,#lnkPdf,#lnkWord').removeAttr('disabled');}">
                                </ClientSideEvents>
                                <Fields>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="0" FieldName="ANIO" ID="camporptANIO"
                                        Caption="Año" />
                                    <dx:PivotGridField Area="RowArea" AreaIndex="1" FieldName="MES" ID="campoMES" Caption="Mes"
                                        TotalsVisibility="CustomTotals">
                                        <CustomTotals>
                                            <dx:PivotGridCustomTotal />
                                            <dx:PivotGridCustomTotal SummaryType="Average" />
                                            <dx:PivotGridCustomTotal SummaryType="Max" />
                                            <dx:PivotGridCustomTotal SummaryType="Min" />
                                            <dx:PivotGridCustomTotal SummaryType="Count" />
                                        </CustomTotals>
                                    </dx:PivotGridField>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="2" FieldName="DIA" ID="campoDIA" Caption="Día" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="0" FieldName="TIPO" ID="campoTIPO"
                                        Caption="Tipo mov." UnboundType="Decimal" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="1" FieldName="GRUPO" ID="campoGRUPO"
                                        Caption="Grupo" UnboundType="Decimal" SortOrder="Descending" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="2" FieldName="ORDEN" ID="campoORDEN"
                                        Caption="#" UnboundType="Decimal" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="3" FieldName="PARAMETRO" ID="campoPARAMETRO"
                                        Caption="Parametros reportados" SortOrder="Descending" />
                                    <dx:PivotGridField Area="DataArea" AreaIndex="0" FieldName="VALOR" ID="campoVALOR"
                                        Caption="Valores reportados" CellFormat-FormatString="N" CellFormat-FormatType="Numeric"
                                        SummaryType="Sum" />
                                </Fields>
                                <OptionsView ShowHorizontalScrollBar="True" ShowColumnTotals="False" ShowColumnGrandTotals="False"
                                    ShowRowTotals="True" />
                                <OptionsFilter NativeCheckBoxes="False" />
                                <OptionsPager RowsPerPage="35" />
                                <OptionsView ShowHorizontalScrollBar="True" ShowColumnTotals="False" ShowColumnGrandTotals="False">
                                </OptionsView>
                                <OptionsPager RowsPerPage="35">
                                </OptionsPager>
                                <OptionsFilter NativeCheckBoxes="False"></OptionsFilter>
                            </dx:ASPxPivotGrid>
                            <dx:ASPxPivotGridExporter ID="pgeReporte" runat="server" ASPxPivotGridID="pgReporte"
                                Visible="False">
                            </dx:ASPxPivotGridExporter>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tabReporte" Text="Seguimiento Producción" ToolTip="Reporte del Seguimiento a la producción por planta">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                        <div class="divPanelExcel">
                            <div class="opcionesReporte">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>Desde</strong>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtFechaIniSeg" ClientInstanceName="txtFechaIniSeg" runat="server"
                                                Theme="Default" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                ClientIDMode="Static">
                                                <CalendarProperties ClearButtonText="Limpiar" ShowClearButton="False" ShowWeekNumbers="False"
                                                    TodayButtonText="Hoy">
                                                    <PrevYearImage ToolTip="Anterior año">
                                                    </PrevYearImage>
                                                    <PrevMonthImage ToolTip="Anterior mes">
                                                    </PrevMonthImage>
                                                    <NextMonthImage ToolTip="Siguiente mes">
                                                    </NextMonthImage>
                                                    <NextYearImage ToolTip="Siguiente año">
                                                    </NextYearImage>
                                                </CalendarProperties>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaFinSeg.GetCalendar();
                                                      cal.minDate = s.GetDate();
                                                      cal.Update();
                                                      }"></ClientSideEvents>
                                                <ValidationSettings CausesValidation="True" Display="Dynamic" EnableCustomValidation="True"
                                                    ErrorText="Fecha de inicio requerida" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                    <RequiredField ErrorText="Fecha de inicio requerida" IsRequired="True" />
                                                    <RequiredField IsRequired="True" ErrorText="Fecha de inicio requerida"></RequiredField>
                                                </ValidationSettings>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaFinSeg.GetCalendar();
                                                      cal.minDate = s.GetDate();
                                                      cal.Update();
                                                      }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <strong>hasta</strong>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtFechaFinSeg" ClientInstanceName="txtFechaFinSeg" runat="server"
                                                Theme="Default" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                ClientIDMode="Static">
                                                <CalendarProperties ClearButtonText="Limpiar" ShowClearButton="False" ShowWeekNumbers="False"
                                                    TodayButtonText="Hoy">
                                                    <PrevYearImage ToolTip="Anterior año">
                                                    </PrevYearImage>
                                                    <PrevMonthImage ToolTip="Anterior mes">
                                                    </PrevMonthImage>
                                                    <NextMonthImage ToolTip="Siguiente mes">
                                                    </NextMonthImage>
                                                    <NextYearImage ToolTip="Siguiente año">
                                                    </NextYearImage>
                                                </CalendarProperties>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaIniSeg.GetCalendar();
                                                      cal.maxDate = s.GetDate();
                                                      cal.Update();
                                                 }"></ClientSideEvents>
                                                <ValidationSettings CausesValidation="True" Display="Dynamic" EnableCustomValidation="True"
                                                    ErrorText="Fecha de finalización requerida" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                    <RequiredField ErrorText="Fecha de finalización requerida" IsRequired="True" />
                                                    <RequiredField IsRequired="True" ErrorText="Fecha de finalizaci&#243;n requerida">
                                                    </RequiredField>
                                                </ValidationSettings>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaIniSeg.GetCalendar();
                                                      cal.maxDate = s.GetDate();
                                                      cal.Update();
                                                 }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <a id="lnkGenerarReporteSeg" runat="server" class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel"
                                                href="javascript:void(0)" clientidmode="Static"><span class="icon ui-icon-gear">
                                                </span><span class="label">Generar reporte</span></a>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkExcelSeg" ClientIDMode="Static" disabled="disabled" runat="server"
                                                CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" OnClick="lnkExcelSeg_Click"
                                                OnClientClick="return verificaPlanta(this);"><span class="icon ui-icon-excel"></span><span class="label">Exportar a Excel</span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkPdfSeg" ClientIDMode="Static" disabled="disabled" runat="server"
                                                CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" OnClick="lnkPdfSeg_Click"
                                                OnClientClick="return verificaPlanta(this);"><span class="icon ui-icon-pdf"></span><span class="label">Exportar a PDF</span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkWordseg" ClientIDMode="Static" disabled="disabled" runat="server"
                                                CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" OnClick="lnkWordSeg_Click"
                                                OnClientClick="return verificaPlanta(this);"><span class="icon ui-icon-word"></span><span class="label">Exportar a Word</span></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <dx:ASPxPivotGrid ID="pgRptSeguimiento" ClientInstanceName="pgRptSeguimiento" runat="server"
                                ClientIDMode="Static" OnCustomCallback="pgRptSeguimiento_CustomCallback" OnCustomChartDataSourceData="pgRptSeguimiento_CustomChartDataSourceData"
                                Theme="Default">
                                <ClientSideEvents EndCallback="function(s, e) {$('#ddlTipoGrafico').change();$('#lnkExcelSeg,#lnkPdfSeg,#lnkWordseg').removeAttr('disabled');}" />
                                <Fields>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="0" FieldName="ANIO" ID="campoANIO" Caption="Año" />
                                    <dx:PivotGridField Area="RowArea" AreaIndex="1" FieldName="MES" ID="rptMes" Caption="Mes"
                                        TotalsVisibility="CustomTotals">
                                        <CustomTotals>
                                            <dx:PivotGridCustomTotal />
                                            <dx:PivotGridCustomTotal SummaryType="Average" />
                                        </CustomTotals>
                                    </dx:PivotGridField>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="2" FieldName="DIA" ID="rptDIA" Caption="Día" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="0" FieldName="TIPO" ID="rptTIPO"
                                        Caption="Tipo mov." UnboundType="Decimal" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="1" FieldName="PLANTA" ID="rptPLANTA"
                                        Caption="Planta" UnboundType="Decimal" SortOrder="Descending" Options-ShowTotals="False" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="2" FieldName="CAMPO" ID="rptCAMPO"
                                        Caption="Campo" UnboundType="Decimal" Options-ShowTotals="False" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="3" FieldName="UNIDAD_MEDIDA" ID="rptUNIDAD_MEDIDA"
                                        Caption="Unidad de Medida" SortOrder="Descending" />
                                    <dx:PivotGridField Area="DataArea" AreaIndex="0" FieldName="VALOR" ID="rptVALOR"
                                        Caption="Valores reportados" CellFormat-FormatString="N" CellFormat-FormatType="Numeric"
                                        SummaryType="Sum" />
                                </Fields>
                                <OptionsView ShowHorizontalScrollBar="True" ShowColumnTotals="False" ShowColumnGrandTotals="False"
                                    ShowRowTotals="True" />
                                <OptionsFilter NativeCheckBoxes="False" />
                                <OptionsPager RowsPerPage="32" />
                                <ClientSideEvents EndCallback="function(s, e) {$(&#39;#ddlTipoGrafico&#39;).change();$(&#39;#lnkExcelSeg,#lnkPdfSeg,#lnkWordseg&#39;).removeAttr(&#39;disabled&#39;);}">
                                </ClientSideEvents>
                                <OptionsView ShowHorizontalScrollBar="True" ShowColumnTotals="False" ShowColumnGrandTotals="False">
                                </OptionsView>
                                <OptionsPager RowsPerPage="32">
                                </OptionsPager>
                                <OptionsFilter NativeCheckBoxes="False"></OptionsFilter>
                            </dx:ASPxPivotGrid>
                            <dx:ASPxPivotGrid ID="pgChartRptSeguimiento" runat="server" Visible="false">
                                <Fields>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="0" FieldName="ANIO" ID="PivotGridField1"
                                        Caption="Año" />
                                    <dx:PivotGridField Area="RowArea" AreaIndex="1" FieldName="MES" ID="PivotGridField2"
                                        Caption="Mes" TotalsVisibility="CustomTotals">
                                        <CustomTotals>
                                            <dx:PivotGridCustomTotal />
                                            <dx:PivotGridCustomTotal SummaryType="Average" />
                                        </CustomTotals>
                                    </dx:PivotGridField>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="2" FieldName="DIA" ID="PivotGridField3"
                                        Caption="Día" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="0" FieldName="TIPO" ID="PivotGridField4"
                                        Caption="Tipo mov." UnboundType="Decimal" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="1" FieldName="PLANTA" ID="PivotGridField5"
                                        Caption="Planta" UnboundType="Decimal" SortOrder="Descending" Options-ShowTotals="False" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="2" FieldName="CAMPO" ID="PivotGridField6"
                                        Caption="Campo" UnboundType="Decimal" Options-ShowTotals="False" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="3" FieldName="UNIDAD_MEDIDA" ID="rptChartUNIDAD_MEDIDA"
                                        Caption="Unidad de Medida" SortOrder="Descending" />
                                    <dx:PivotGridField Area="DataArea" AreaIndex="0" FieldName="VALOR" ID="PivotGridField8"
                                        Caption="Valores reportados" CellFormat-FormatString="N" CellFormat-FormatType="Numeric"
                                        SummaryType="Sum" />
                                </Fields>
                                <OptionsView ShowHorizontalScrollBar="True" ShowColumnTotals="False" ShowColumnGrandTotals="False"
                                    ShowRowTotals="True" />
                                <OptionsFilter NativeCheckBoxes="False" />
                                <OptionsPager RowsPerPage="0" />
                                <OptionsView ShowHorizontalScrollBar="True" ShowColumnTotals="False" ShowColumnGrandTotals="False">
                                </OptionsView>
                                <OptionsPager RowsPerPage="0">
                                </OptionsPager>
                                <OptionsFilter NativeCheckBoxes="False"></OptionsFilter>
                            </dx:ASPxPivotGrid>
                            <dx:ASPxPivotGridExporter ID="pgeRptSeguimiento" runat="server" ASPxPivotGridID="pgRptSeguimiento"
                                Visible="False">
                            </dx:ASPxPivotGridExporter>
                            <div class="opcionesReporte">
                                <table>
                                    <tr>
                                        <td class="Hydro_Titulo_Verde_Menor">
                                            Opciones del gr&aacute;fico:
                                        </td>
                                        <td style="width: 50px;">
                                        </td>
                                        <td>
                                            <strong>Tipo de gr&aacute;fico</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlTipoGrafico" ClientIDMode="Static" AutoPostBack="False" />
                                        </td>
                                        <td style="width: 50px;">
                                        </td>
                                        <td>
                                            <strong>Unidad de medida</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlUMedGraficoSeg">
                                                <asp:ListItem Value="TM">TM</asp:ListItem>
                                                <asp:ListItem Value="M3">M3</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <dxchartsui:WebChartControl ID="chartRptSeguimiento" runat="server" ClientInstanceName="chartRptSeguimiento"
                                DataSourceID="pgChartRptSeguimiento" Width="1050px" Height="600px" SeriesDataMember="Series"
                                EnableViewState="False" SaveStateOnCallbacks="False" OnCustomCallback="chartRptSeguimiento_CustomCallback">
                                <borderoptions visible="False"></borderoptions>
                                <diagramserializable>
                                    <dxcharts:XYDiagram>
                                        <axisx interlaced="True" visibleinpanesserializable="-1">
                                            <Label Staggered="True"></Label>
                                            <Range SideMarginsEnabled="True"></Range>   
                                        </axisx>
                                        <axisy visibleinpanesserializable="-1">
                                            <Range SideMarginsEnabled="True"></Range>
                                        </axisy>
                                    </dxcharts:XYDiagram>
                                </diagramserializable>
                                <fillstyle>
                                    <OptionsSerializable>
                                        <dxcharts:SolidFillOptions></dxcharts:SolidFillOptions>
                                    </OptionsSerializable>
                                </fillstyle>
                                <legend alignmenthorizontal="Center" alignmentvertical="BottomOutside" direction="LeftToRight">
                                </legend>
                                <seriestemplate argumentdatamember="Arguments" valuedatamembersserializable="Values">
                                    <ViewSerializable>
                                        <dxcharts:SideBySideBarSeriesView></dxcharts:SideBySideBarSeriesView>
                                    </ViewSerializable>
                                    <LabelSerializable>
                                        <dxcharts:SideBySideBarSeriesLabel LineVisible="True">
                                        <FillStyle><OptionsSerializable>
                                        <dxcharts:SolidFillOptions></dxcharts:SolidFillOptions>
                                        </OptionsSerializable>
                                        </FillStyle>
                                        <PointOptionsSerializable>
                                        <dxcharts:PointOptions>
                                        <ArgumentNumericOptions Format="General"></ArgumentNumericOptions>
                                        <ValueNumericOptions Format="General"></ValueNumericOptions>
                                            </dxcharts:PointOptions>
                                        </PointOptionsSerializable>
                                        </dxcharts:SideBySideBarSeriesLabel>
                                    </LabelSerializable>
                                    <LegendPointOptionsSerializable>
                                        <dxcharts:PointOptions></dxcharts:PointOptions>
                                    </LegendPointOptionsSerializable>
                                </seriestemplate>
                                <titles>
                                <dxcharts:ChartTitle Text="SEGUIMIENTO A LA PRODUCCION ENTREGAS Y SALDOS DE GLP"></dxcharts:ChartTitle>
                                </titles>
                                <tooltipoptions><ToolTipPositionSerializable>
                                    <dxcharts:ToolTipMousePosition></dxcharts:ToolTipMousePosition>
                                </ToolTipPositionSerializable>
                                </tooltipoptions>
                                <crosshairoptions argumentlinecolor="222, 57, 205" valuelinecolor="222, 57, 205">
                                    <CommonLabelPositionSerializable>
                                        <dxcharts:CrosshairMousePosition></dxcharts:CrosshairMousePosition>
                                    </CommonLabelPositionSerializable>
                                </crosshairoptions>
                            </dxchartsui:WebChartControl>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tabReporteProde" Text="Seguimiento al Prode" ToolTip="Reporte de seguimiento al PRODE para varias plantas">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                        <div class="divPanelExcel">
                            <div class="opcionesReporte">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>Desde</strong>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtFechaIniProde" ClientInstanceName="txtFechaIniProde" runat="server"
                                                Theme="Default" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                ClientIDMode="Static">
                                                <CalendarProperties ClearButtonText="Limpiar" ShowClearButton="False" ShowWeekNumbers="False"
                                                    TodayButtonText="Hoy">
                                                    <PrevYearImage ToolTip="Anterior año">
                                                    </PrevYearImage>
                                                    <PrevMonthImage ToolTip="Anterior mes">
                                                    </PrevMonthImage>
                                                    <NextMonthImage ToolTip="Siguiente mes">
                                                    </NextMonthImage>
                                                    <NextYearImage ToolTip="Siguiente año">
                                                    </NextYearImage>
                                                </CalendarProperties>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaFinProde.GetCalendar();
                                                      cal.minDate = s.GetDate();
                                                      cal.Update();
                                                      }"></ClientSideEvents>
                                                <ValidationSettings CausesValidation="True" Display="Dynamic" EnableCustomValidation="True"
                                                    ErrorText="Fecha de inicio requerida" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                    <RequiredField ErrorText="Fecha de inicio requerida" IsRequired="True" />
                                                    <RequiredField IsRequired="True" ErrorText="Fecha de inicio requerida"></RequiredField>
                                                </ValidationSettings>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaFinProde.GetCalendar();
                                                      cal.minDate = s.GetDate();
                                                      cal.Update();
                                                      }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <strong>hasta</strong>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtFechaFinProde" ClientInstanceName="txtFechaFinProde" runat="server"
                                                Theme="Default" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                ClientIDMode="Static">
                                                <CalendarProperties ClearButtonText="Limpiar" ShowClearButton="False" ShowWeekNumbers="False"
                                                    TodayButtonText="Hoy">
                                                    <PrevYearImage ToolTip="Anterior año">
                                                    </PrevYearImage>
                                                    <PrevMonthImage ToolTip="Anterior mes">
                                                    </PrevMonthImage>
                                                    <NextMonthImage ToolTip="Siguiente mes">
                                                    </NextMonthImage>
                                                    <NextYearImage ToolTip="Siguiente año">
                                                    </NextYearImage>
                                                </CalendarProperties>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaIniProde.GetCalendar();
                                                      cal.maxDate = s.GetDate();
                                                      cal.Update();
                                                 }"></ClientSideEvents>
                                                <ValidationSettings CausesValidation="True" Display="Dynamic" EnableCustomValidation="True"
                                                    ErrorText="Fecha de finalización requerida" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                    <RequiredField ErrorText="Fecha de finalización requerida" IsRequired="True" />
                                                    <RequiredField IsRequired="True" ErrorText="Fecha de finalizaci&#243;n requerida">
                                                    </RequiredField>
                                                </ValidationSettings>
                                                <ClientSideEvents DateChanged="function(s) {
                                                      var cal = txtFechaIniProde.GetCalendar();
                                                      cal.maxDate = s.GetDate();
                                                      cal.Update();
                                                 }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <a id="lnkGenerarReporteProde" runat="server" class="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel"
                                                href="javascript:void(0)" clientidmode="Static"><span class="icon ui-icon-gear">
                                                </span><span class="label">Generar reporte</span></a>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkExcelProde" ClientIDMode="Static" disabled="disabled" runat="server"
                                                CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" OnClick="lnkExcelProde_Click"
                                                OnClientClick="return verificaPlantaProde(this);"><span class="icon ui-icon-excel"></span><span class="label">Exportar a Excel</span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkPdfProde" ClientIDMode="Static" disabled="disabled" runat="server"
                                                CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" OnClick="lnkPdfProde_Click"
                                                OnClientClick="return verificaPlantaProde(this);"><span class="icon ui-icon-pdf"></span><span class="label">Exportar a PDF</span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkWordProde" ClientIDMode="Static" disabled="disabled" runat="server"
                                                CssClass="Hydro_Boton_Blanco Hydro_Boton_Grilla_Excel" OnClick="lnkWordProde_Click"
                                                OnClientClick="return verificaPlantaProde(this);"><span class="icon ui-icon-word"></span><span class="label">Exportar a Word</span></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <strong>Unidad de medida<br />
                                                para el reporte</strong>
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList runat="server" ID="ddlUnidadMedidaProde" ClientIDMode="Static" />--%>
                                            <%--<asp:DropDownList runat="server" ID="ddlUnidadMedidaProde" ClientIDMode="Static" 
                                                Width="130px" >
                                                <asp:ListItem>MMPCD</asp:ListItem>
                                                <asp:ListItem>m3</asp:ListItem>
                                                <asp:ListItem>pie3</asp:ListItem>
                                                <asp:ListItem>BBL</asp:ListItem>
                                                <asp:ListItem>L</asp:ListItem>
                                                <asp:ListItem>TMD</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:DropDownList runat="server" ID="ddlUnidadMedidaProde" ClientIDMode="Static"
                                                Width="130px">
                                                <asp:ListItem Value="90" Selected="True">TMD</asp:ListItem>
                                                <asp:ListItem Value="89">FACTOR 1.5</asp:ListItem>
                                                <asp:ListItem Value="23">DOBLE</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <strong>Plantas que se<br />
                                                incluirán en el reporte</strong>
                                        </td>
                                        <td>
                                            <dx:ASPxCheckBoxList ID="cblPlantas" ClientInstanceName="cblPlantas" runat="server"
                                                Width="99%" RepeatColumns="4" RepeatLayout="Table">
                                            </dx:ASPxCheckBoxList>
                                            <%--<asp:CheckBoxList ID="cblPlantas" runat="server"></asp:CheckBoxList>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <dx:ASPxPivotGrid ID="pgRptProde" ClientInstanceName="pgRptProde" runat="server"
                                ClientIDMode="Static" OnCustomCallback="pgRptProde_CustomCallback" OnCustomChartDataSourceData="pgRptProde_CustomChartDataSourceData"
                                Theme="Default">
                                <ClientSideEvents EndCallback="function(s, e) {$('#ddlTipoGraficoProde').change();$('#lnkExcelProde,#lnkPdfProde,#lnkWordProde').removeAttr('disabled');}" />
                                <Fields>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="0" FieldName="ANIO" ID="campoprodeANIO"
                                        Caption="Año" />
                                    <dx:PivotGridField Area="RowArea" AreaIndex="1" FieldName="MES" ID="campoprodeMES"
                                        Caption="Mes" TotalsVisibility="CustomTotals">
                                        <CustomTotals>
                                            <dx:PivotGridCustomTotal />
                                            <dx:PivotGridCustomTotal SummaryType="Average" />
                                        </CustomTotals>
                                    </dx:PivotGridField>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="2" FieldName="DIA" ID="campoprodeDIA"
                                        Caption="Día" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="0" FieldName="TIPO" ID="campoprodeTIPO"
                                        Caption="Tipo mov." UnboundType="Decimal" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="1" FieldName="PLANTA" ID="campoprodePLANTA"
                                        Caption="Planta" UnboundType="Decimal" SortOrder="Ascending" Options-ShowTotals="False" />
                                    <dx:PivotGridField Area="DataArea" AreaIndex="0" FieldName="VALOR" ID="campoprodeVALOR"
                                        Caption="Valores reportados" CellFormat-FormatString="N" CellFormat-FormatType="Numeric"
                                        SummaryType="Sum" />
                                </Fields>
                                <OptionsView ShowHorizontalScrollBar="False" ShowColumnTotals="False" ShowColumnGrandTotals="False"
                                    ShowRowTotals="True" />
                                <OptionsFilter NativeCheckBoxes="False" />
                                <OptionsPager RowsPerPage="32" />
                                <ClientSideEvents EndCallback="function(s, e) {$(&#39;#ddlTipoGraficoProde&#39;).change();$(&#39;#lnkExcelProde,#lnkPdfProde,#lnkWordProde&#39;).removeAttr(&#39;disabled&#39;);}">
                                </ClientSideEvents>
                                <OptionsView ShowColumnTotals="False" ShowColumnGrandTotals="False"></OptionsView>
                                <OptionsPager RowsPerPage="32">
                                </OptionsPager>
                                <OptionsFilter NativeCheckBoxes="False"></OptionsFilter>
                            </dx:ASPxPivotGrid>
                            <dx:ASPxPivotGrid ID="pgChartRptProde" ClientInstanceName="pgRptProde1" runat="server"
                                ClientIDMode="Static" Visible="False">
                                <Fields>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="0" FieldName="ANIO" ID="PivotGridField7"
                                        Caption="Año" />
                                    <dx:PivotGridField Area="RowArea" AreaIndex="1" FieldName="MES" ID="PivotGridField9"
                                        Caption="Mes" TotalsVisibility="CustomTotals">
                                    </dx:PivotGridField>
                                    <dx:PivotGridField Area="RowArea" AreaIndex="2" FieldName="DIA" ID="PivotGridField10"
                                        Caption="Día" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="0" FieldName="TIPO" ID="PivotGridField11"
                                        Caption="Tipo mov." UnboundType="Decimal" />
                                    <dx:PivotGridField Area="ColumnArea" AreaIndex="1" FieldName="PLANTA" ID="PivotGridField12"
                                        Caption="Planta" UnboundType="Decimal" SortOrder="Ascending" Options-ShowTotals="False" />
                                    <dx:PivotGridField Area="DataArea" AreaIndex="0" FieldName="VALOR" ID="PivotGridField13"
                                        Caption="Valores reportados" CellFormat-FormatString="N" CellFormat-FormatType="Numeric"
                                        SummaryType="Sum" />
                                </Fields>
                            </dx:ASPxPivotGrid>
                            <dx:ASPxPivotGridExporter ID="pgeRptProde" runat="server" ASPxPivotGridID="pgRptProde"
                                Visible="False">
                            </dx:ASPxPivotGridExporter>
                            <div class="opcionesReporte">
                                <table>
                                    <tr>
                                        <td class="Hydro_Titulo_Verde_Menor">
                                            Opciones del gr&aacute;fico:
                                        </td>
                                        <td style="width: 50px;">
                                        </td>
                                        <td>
                                            <strong>Tipo de gr&aacute;fico</strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlTipoGraficoProde" ClientIDMode="Static" AutoPostBack="False" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <dxchartsui:WebChartControl ID="chartRptProde" runat="server" ClientInstanceName="chartRptProde"
                                DataSourceID="pgChartRptProde" Width="1050px" Height="600px" SeriesDataMember="Series"
                                EnableViewState="False" SaveStateOnCallbacks="False" OnCustomCallback="chartRptProde_CustomCallback">
                                <borderoptions visible="False"></borderoptions>
                                <diagramserializable>
                                    <dxcharts:XYDiagram>
                                        <axisx interlaced="True" visibleinpanesserializable="-1">
                                            <Label Staggered="True"></Label>
                                            <Range SideMarginsEnabled="True"></Range>   
                                        </axisx>
                                        <axisy visibleinpanesserializable="-1">
                                            <Range SideMarginsEnabled="True"></Range>
                                        </axisy>
                                    </dxcharts:XYDiagram>
                                </diagramserializable>
                                <fillstyle>
                                    <OptionsSerializable>
                                        <dxcharts:SolidFillOptions></dxcharts:SolidFillOptions>
                                    </OptionsSerializable>
                                </fillstyle>
                                <legend alignmenthorizontal="Center" alignmentvertical="BottomOutside" direction="LeftToRight">
                                </legend>
                                <seriestemplate argumentdatamember="Arguments" valuedatamembersserializable="Values">
                                    <ViewSerializable>
                                        <dxcharts:SideBySideBarSeriesView></dxcharts:SideBySideBarSeriesView>
                                    </ViewSerializable>
                                    <LabelSerializable>
                                        <dxcharts:SideBySideBarSeriesLabel LineVisible="True">
                                        <FillStyle><OptionsSerializable>
                                        <dxcharts:SolidFillOptions></dxcharts:SolidFillOptions>
                                        </OptionsSerializable>
                                        </FillStyle>
                                        <PointOptionsSerializable>
                                        <dxcharts:PointOptions>
                                        <ArgumentNumericOptions Format="General"></ArgumentNumericOptions>
                                        <ValueNumericOptions Format="General"></ValueNumericOptions>
                                            </dxcharts:PointOptions>
                                        </PointOptionsSerializable>
                                        </dxcharts:SideBySideBarSeriesLabel>
                                    </LabelSerializable>
                                    <LegendPointOptionsSerializable>
                                        <dxcharts:PointOptions></dxcharts:PointOptions>
                                    </LegendPointOptionsSerializable>
                                </seriestemplate>
                                <titles>
                                <dxcharts:ChartTitle Text="SEGUIMIENTO A LA PRODUCCION ENTREGAS Y SALDOS DE GLP"></dxcharts:ChartTitle>
                                </titles>
                                <tooltipoptions><ToolTipPositionSerializable>
                                    <dxcharts:ToolTipFreePosition></dxcharts:ToolTipFreePosition>
                                </ToolTipPositionSerializable>
                                </tooltipoptions>
                                <crosshairoptions argumentlinecolor="222, 57, 205" valuelinecolor="222, 57, 205">
                                    <CommonLabelPositionSerializable>
                                        <dxcharts:CrosshairMousePosition></dxcharts:CrosshairMousePosition>
                                    </CommonLabelPositionSerializable>
                                </crosshairoptions>
                            </dxchartsui:WebChartControl>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ClientSideEvents ActiveTabChanged="function(s, e) { 
            if (e.tab.name==&#39;tabAlimento&#39;) crearGridAlimento();
            else if (e.tab.name==&#39;tabProduccion&#39;) crearGridProduccion();
            else if (e.tab.name==&#39;tabResidual&#39;) crearGridResidual(); }"></ClientSideEvents>
    </dx:ASPxPageControl>
    </div>
    <div id="Div1" class="ocultar">
    </div>
</asp:Content>
