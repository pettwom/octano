<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfRptCertificadoCalidadBajas.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad.wfRptCertificadoCalidadBajas" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function habilitarEdicion(s, e1) {
            if ($(".lnkEditarCal").length > 0) {
                $(".lnkEditarCal").off("click");
                $(".lnkEditarCal").on("click", function (e) {
                    e.preventDefault();
                    self.parent.presentacion.mostrarFancyboxAncho(this, 'Detalle del certificado de calidad registrado', 670, 600);
                });
            }
        }
    </script>

    <script type="text/javascript">
        function grid_CustomButtonClick(s, e) {
            if (e.buttonID == 'Eliminar') {
                e.processOnServer = false;
                $.msgBox({
                    title: "Confirmación",
                    content: "Desea ver detalle del certificado de calidad antes de eliminarlo?.",
                    type: "confirm",
                    buttons: [{ value: "Aceptar" }, { value: "Cancelar" }],
                    success: function (result) {
                        if (result == "Aceptar") {
                            grdCertificadoCalidad.PerformCallback(e.visibleIndex.toString());
                        }
                    }
                });
            }
            else {
                e.processOnServer = true;
            }
        }
    </script>
    <script type="text/javascript">
        function MsgEliminar(cite) {
            $.msgBox({
                title: "Confirmación",
                content: "" +
                    "Se eliminó correctamente el certificado de calidad " + cite + ".",
                type: "confirm",
                buttons: [{ value: "Aceptar" }],
                success: function (result) {

                }
            });
        }
    </script>
     <script type="text/javascript">
         function MsgModificar(cite) {
             $.msgBox({
                 title: "Confirmación",
                 content: "" +
                     "Su certificado de calidad " + cite + " fue modificado correctamente.",
                 type: "confirm",
                 buttons: [{ value: "Aceptar" }],
                 success: function (result) {

                 }
             });
         }
    </script>
    <script type="text/javascript">
        function MsgModificarError() {
            $.msgBox({
                title: "Error",
                content: "" +
                    "Su certificado de calidad NO fue modificado correctamente, intente de nuevo por favor.",
                type: "confirm",
                buttons: [{ value: "Aceptar" }],
                success: function (result) {

                }
            });
        }
    </script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            var id = $(this).attr("id");
            $(".lnkVentana").fancybox({
                'titleShow': false,
                'autoScale': true,
                'transitionIn': 'none',
                'transitionOut': 'none',
                autoSize: false,
                width: 930,
                height: 400,
                'type': 'iframe'
            });
        });
    </script>

    <style type="text/css">
        .floatL {
            float: left;
            margin: 3px;
        }
        .botones {
        }

        /* ─── Títulos ─── */
        .section-title {
            font-weight: 600;
            letter-spacing: 0.3px;
            margin-bottom: 4px;
        }

        /* ─── Card de filtros ─── */
        .filter-card {
            background: #f8f9fa;
            border: 1px solid #e9ecef;
            border-radius: 8px;
            padding: 16px 20px;
            margin-bottom: 20px;
        }
        .filter-card td {
            padding: 4px 10px;
            vertical-align: middle;
        }

        /* ─── Wrapper responsive para grilla ─── */
        .grid-responsive-wrap {
            overflow-x: auto;
            width: 100%;
            -webkit-overflow-scrolling: touch;
            border: 1px solid #e9ecef;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 1px 4px rgba(0,0,0,0.06);
        }

        /* ─── Botón Buscar ─── */
        #btnAceptar {
            transition: background 0.2s ease, box-shadow 0.2s ease, transform 0.1s ease;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-weight: 500;
            font-size: 14px;
            padding: 7px 24px !important;
            box-shadow: 0 1px 3px rgba(0,0,0,0.12);
        }
        #btnAceptar:hover {
            background-color: #236832 !important;
            box-shadow: 0 3px 8px rgba(45,126,62,0.35);
        }
        #btnAceptar:active {
            transform: scale(0.97);
        }

        /* ─── Toolbar de exportación ─── */
        .export-toolbar {
            display: flex;
            gap: 8px;
            flex-wrap: wrap;
            align-items: center;
            margin: 14px 0;
        }
        .export-toolbar .Hydro_Boton_Blanco {
            border-radius: 5px !important;
            padding: 7px 16px !important;
            border: 1px solid #2D7E3E !important;
            transition: all 0.2s ease !important;
            font-weight: 500 !important;
            font-size: 13px !important;
            line-height: 1.4 !important;
            text-decoration: none !important;
        }
        .export-toolbar .Hydro_Boton_Blanco:hover {
            background-color: #236832 !important;
            border-color: #236832 !important;
            box-shadow: 0 2px 6px rgba(45,126,62,0.25);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title" style="color: #2D7E3E">Certificados dados de baja</h2>
<hr class="Hydro_Linea_Verde"/>

<div class="filter-card">
<table width="100%">
    <tr>
        <td style="width: 25%">Búsqueda por Fecha de Operación</td>
        <td style="width: 25%">
           <center>
             <dx:ASPxDateEdit ID="txtFechaInicial" runat="server" 
                                Theme="DevEx" Width="150px" 
                DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" 
                EditFormat="Custom" ClientIDMode="Static" ClientInstanceName="txtFechaInicial" Caption="Fecha Inicial:">
                <CalendarProperties ShowWeekNumbers="False">
                </CalendarProperties>
                <TimeSectionProperties>
                    <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                </TimeSectionProperties>
            </dx:ASPxDateEdit>
            </center>
        </td>
        <td style="width: 25%">
            
            <dx:ASPxDateEdit ID="txtFechaFinal" runat="server" 
                                Theme="DevEx" Width="150px" 
                DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" 
                EditFormat="Custom" ClientIDMode="Static" ClientInstanceName="txtFechaFinal" Caption="Fecha Final:">
                <CalendarProperties ShowWeekNumbers="False">
                </CalendarProperties>
                <TimeSectionProperties>
                    <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                </TimeSectionProperties>
            </dx:ASPxDateEdit>
            
        </td>
        <td style="width: 25%"><asp:Button ID="btnAceptar" runat="server" Text="Buscar" ClientIDMode="Static" 
                onclick="btnAceptar_Click" BackColor="#2D7E3E" ForeColor="White" /></td>
    </tr>
</table>
</div>

    <div class="export-toolbar">
        <asp:LinkButton ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel" CssClass="Hydro_Boton_Blanco" onclick="btnExcel_Click" BackColor="#2D7E3E" BorderWidth="1px" ><span class="icon ui-icon-excel"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Excel</span></asp:LinkButton>            
        <asp:LinkButton ID="btnPdf" runat="server" title="Exportar datos de la grilla a Pdf" CssClass="Hydro_Boton_Blanco" onclick="btnPdf_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-pdf"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a PDF</span></asp:LinkButton>
        <asp:LinkButton ID="btnWord" runat="server" title="Exportar datos de la grilla a Word" CssClass="Hydro_Boton_Blanco" onclick="btnWord_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-word"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Word</span></asp:LinkButton>
    </div>
    
<div class="grid-responsive-wrap">
<dx:ASPxGridView ID="grdCertificadoCalidad" 
        ClientInstanceName="grdCertificadoCalidad" runat="server" 
        CssClass="Hydro_Content_Table" AutoGenerateColumns="False"
        OnCustomButtonCallback="grdCertificadoCalidad_CustomButtonCallback" 
        oncustomcallback="grdCertificadoCalidad_CustomCallback" Width="100%">
        
        <ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />
        
        <ClientSideEvents CustomButtonClick="grid_CustomButtonClick" />
        
        <Columns>
            <dx:GridViewDataColumn Caption="#" VisibleIndex="0"   Visible="False" >
                <DataItemTemplate>
                    <dx:ASPxLabel ID="lblRowID" runat="server" Text="ASPxLabel" Visible="False"></dx:ASPxLabel>

                    <asp:Panel ID="pnlDetalles" runat="server" CssClass="floatL" HorizontalAlign="Center">
                        <a href="../Reportes/WfReporteCalidad.aspx?idCalPrincipal=<%# DataBinder.Eval(Container.DataItem, "ID_REGISTROCAL_PRINCIPAL") %>&cite=<%# DataBinder.Eval(Container.DataItem, "CITE_GENERADO") %>&fec=<%# DataBinder.Eval(Container.DataItem, "FECHA_REGISTRO").ToString().Replace("/","_")%> " class="lnkEditarCal">
                        <asp:Image ID="imgDetalles" runat="server" ImageUrl="~/UI/img/iconos/Detalles.png" AlternateText="Detalles" ImageAlign="AbsMiddle" /></a>
                    </asp:Panel>

                </DataItemTemplate>
                <CellStyle CssClass="botones">
                </CellStyle>
            </dx:GridViewDataColumn>

            

            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Image" Name="ComandosEdiEli" Visible="False" ShowClearFilterButton="True">
                <CustomButtons>
                    <%--<dx:GridViewCommandColumnCustomButton ID="Detalles">
                        <Image ToolTip="Ver Certificado" Url="~/UI/img/iconos/Detalles.png" >
                            <SpriteProperties CssClass="unodos" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>--%>

                    <%--<dx:GridViewCommandColumnCustomButton ID="Editar">
                        <Image ToolTip="Editar Certificado" Url="~/UI/img/iconos/editar.png" />
                    </dx:GridViewCommandColumnCustomButton>--%>

                    <%--<dx:GridViewCommandColumnCustomButton ID="Eliminar">
                        <Image ToolTip="Eliminar Certificado" Url="~/UI/img/iconos/eliminar.png" />
                    </dx:GridViewCommandColumnCustomButton>--%>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            
            <%--<dx:GridViewDataTextColumn Caption="Estado" FieldName="ESTADO" Width="18%"
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>--%>

            <dx:GridViewCommandColumn VisibleIndex="2" ButtonType="Image"  
                Name="ComandosEdiEli" Visible="False">                
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="Fecha Op." FieldName="FECHA_OPERACION" 
                VisibleIndex="3" SortIndex="0" SortOrder="Descending">
                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy" DisplayFormatInEditMode="True">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Cite" FieldName="CITE_GENERADO" 
                VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nombre" FieldName="NOMBRE_PRODUCTO" 
                VisibleIndex="7"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tanque" FieldName="PUNTO_CUSTODIO" 
                VisibleIndex="8" >
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Resolucion" FieldName="OBSERVACIONES" 
                VisibleIndex="9" >
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Lote" FieldName="VALOR_LOTE" 
                VisibleIndex="10" ></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Volúmen" FieldName="VOLUMEN_OP_DEBE" 
                VisibleIndex="13" >
                <Settings AutoFilterCondition="BeginsWith"></Settings>
                <PropertiesTextEdit MaxLength="5" ></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Unidad" 
                FieldName="CODIGO_UNIDAD_MEDIDA" VisibleIndex="15"></dx:GridViewDataTextColumn>
            
            <dx:GridViewDataDateColumn Caption="Fecha Registro" FieldName="FECHA_REGISTRO" 
                VisibleIndex="17">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm">
<TimeSectionProperties>
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
</TimeSectionProperties>

<ClearButton Visibility="Auto"></ClearButton>
                </PropertiesDateEdit>
                <Settings AutoFilterCondition="BeginsWith"></Settings>
            </dx:GridViewDataDateColumn>
            
            <dx:GridViewDataColumn VisibleIndex="21" Width="125px" Caption="Opciones" Name="Opciones">
                <DataItemTemplate>
                    <a runat="server" id="lnkVer" href='<%# "../GestionAlertas/wfVeDocumento.aspx?Cite="+Eval("CITE_GENERADO")+"&idTipoRespaldo=1" %>' class="lnkVentana" title="Respaldo del Certificado de Calidad">Ver documento<br/></a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>

            <dx:GridViewDataTextColumn Caption="Usuario" FieldName="USUARIO_REGISTRO" Name="USUARIO_REGISTRO" Visible="False" VisibleIndex="18">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Entidad" FieldName="ENTIDAD_DENOMINACION" Name="ENTIDAD_DENOMINACION" VisibleIndex="19">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Actividad" FieldName="TIPO_ACTIVIDAD" Name="TIPO_ACTIVIDAD" VisibleIndex="20">
            </dx:GridViewDataTextColumn>

        </Columns>

        <SettingsPager PageSize="20"><PageSizeItemSettings Visible="true" ShowAllItem="true" /></SettingsPager>
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowFilterRowMenu="True" />
        <SettingsPopup> <HeaderFilter MinHeight="300" MinWidth="300"></HeaderFilter> </SettingsPopup>
        <Paddings Padding="5px" />
        <Styles>
            <GroupPanel BackColor="White"  ForeColor="#000000"></GroupPanel>
            <Header CssClass="Hydro_Td_Titulo" BackColor="#2D7E3E" ForeColor="White"></Header>
            <FilterRow BackColor="#2D7E3E" ForeColor="White"></FilterRow>
            <Row CssClass="Hydro_Tr_Contenido"></Row>
            <Table CssClass="Hydro_Content_Table" BackColor="White"></Table>
            <AlternatingRow BackColor="#EEF3E4">
            </AlternatingRow>
            <PagerBottomPanel BackColor="White"  ForeColor="#000000"></PagerBottomPanel>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdCertificadoCalidad">
    </dx:ASPxGridViewExporter>
    </div>
</asp:Content>

