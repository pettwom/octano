<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfCertificadoCalidadAlertas.aspx.cs" Inherits="AnhHydro.Sitio.VolumenesCalidad.GestionAlertas.wfCertificadoCalidadAlertas" %>
<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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

        /* ─── Botón Aceptar ─── */
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

        /* ─── Título de sección ─── */
        .section-title {
            font-weight: 600;
            letter-spacing: 0.3px;
            margin-bottom: 4px;
        }
    </style>
    <script type="text/javascript">


        function habilitarEdicion(s, e1) {
            if ($(".lnkEditarCal").length > 0) {
                $(".lnkEditarCal").off("click");
                $(".lnkEditarCal").on("click", function (e) {
                    e.preventDefault();
                    self.parent.presentacion.mostrarFancyboxAncho(this, 'Detalle del certificado de calidad registrado', 670, 600);
                });
                $(".lnkAgrDoc").on("click", function (e) {
                    e.preventDefault();
                    self.parent.presentacion.mostrarFancyboxAncho(this, 'Agregar respaldo del Certificado de Calidad', 930, 400);
                });
                $(".lnkModDoc").on("click", function (e) {
                    e.preventDefault();
                    self.parent.presentacion.mostrarFancyboxAncho(this, 'Gestion respaldo del Certificado de Calidad', 930, 400);
                });
            }
        }


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title" style="color: #2D7E3E">Pruebas de Calidad con Observaciones</h2>
<hr class="Hydro_Linea_Verde"/>
<div class="filter-card">
<table width="100%">
    <tr>
        <td style="width: 25%">Búsqueda por Fecha de Operación</td>
        <td style="width: 25%">
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
        <td style="width: 25%"><asp:Button ID="btnAceptar" runat="server" Text="Aceptar" ClientIDMode="Static" 
                onclick="btnAceptar_Click" BackColor="#2D7E3E" ForeColor="White" /></td>
    </tr>
</table>
</div>
    <div class="export-toolbar">
    <table>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <asp:LinkButton ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel" CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-excel"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Excel</span></asp:LinkButton>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:LinkButton ID="btnPdf" runat="server" title="Exportar datos de la grilla a Pdf" CssClass="Hydro_Boton_Blanco" OnClick="btnPdf_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-pdf"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a PDF</span></asp:LinkButton>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:LinkButton ID="btnWord" runat="server" title="Exportar datos de la grilla a Word" CssClass="Hydro_Boton_Blanco" OnClick="btnWord_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-word"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Word</span></asp:LinkButton>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
    </div>
    <div class="grid-responsive-wrap">
    <dx:ASPxGridView ID="grdCertificadoCalidad" runat="server" CssClass="Hydro_Table" Width="100%" AutoGenerateColumns="False" Font-Size="Small">      
        <SettingsSearchPanel Visible="True" />
        <Columns>
            <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="11" Width="0%" Visible="False">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn Caption="Certificado" VisibleIndex="12">
                <Settings AllowAutoFilter="False" />
                <DataItemTemplate>
                    <a href="../Reportes/WfReporteCalidad.aspx?idCalPrincipal=<%# DataBinder.Eval(Container.DataItem, "ID_REGISTROCAL_PRINCIPAL") %>&cite=<%# DataBinder.Eval(Container.DataItem, "CITE_GENERADO") %>&act=0&fec=<%# DataBinder.Eval(Container.DataItem, "FECHA_OPERACION").ToString().Replace("/","_")%> " class="lnkEditarCal">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/UI/img/iconos/Detalles.png" />
                    </a>
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataColumn>
            <%--<dx:GridViewDataColumn Caption="Fecha Op." VisibleIndex="1" >
                <Settings AllowAutoFilter="True" />
                <DataItemTemplate>                    
                    <%# Convert.ToDateTime(Eval("FECHA_OPERACION")).ToString("dd/MM/yyyy")%>
                </DataItemTemplate>
            </dx:GridViewDataColumn>--%>
            
            <dx:GridViewDataDateColumn Caption="Fecha Op." VisibleIndex="0" FieldName="FECHA_OPERACION" SortIndex="0" SortOrder="Descending">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
<TimeSectionProperties>
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
</TimeSectionProperties>

<ClearButton Visibility="Auto"></ClearButton>
                </PropertiesDateEdit>
                <Settings AutoFilterCondition="BeginsWith"></Settings>
            </dx:GridViewDataDateColumn>

            <%--<dx:GridViewDataColumn Caption="Cite" VisibleIndex="1" Width="150px">
                <Settings AllowAutoFilter="True" />
                <DataItemTemplate>
                    ANH-DRUIN-CAL <%# Eval("ID_REGISTROCAL_PRINCIPAL").ToString().PadLeft(4,'0')%>/2013
                </DataItemTemplate>
            </dx:GridViewDataColumn>--%>

            <dx:GridViewDataTextColumn Caption="Cite" FieldName="CITE_GENERADO" VisibleIndex="4">
                <Settings HeaderFilterMode="CheckedList"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Producto" FieldName="NOMBRE_PRODUCTO" VisibleIndex="5">
                <Settings HeaderFilterMode="CheckedList"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Lote" FieldName="VALOR_LOTE" VisibleIndex="6" >
                <Settings HeaderFilterMode="CheckedList"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="PTC" FieldName="PUNTO_CUSTODIO" VisibleIndex="7" Name="ptc">
                <Settings HeaderFilterMode="CheckedList"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Volúmen" FieldName="VOLUMEN_OP_DEBE" VisibleIndex="8">
                <Settings HeaderFilterMode="CheckedList"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Unidad Medida" FieldName="CODIGO_UNIDAD_MEDIDA" VisibleIndex="9" >
                <Settings HeaderFilterMode="CheckedList"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn VisibleIndex="13" Caption="Respaldo" Name="resp">
                 <DataItemTemplate>
                     <a runat="server" ID="lnkRegistro" href='<%# "wfRegistroPDF.aspx?idCalPrincipal="+Eval("ID_REGISTROCAL_PRINCIPAL")+"&Cite="+Eval("CITE_GENERADO") %>' class="lnkAgrDoc" title="Respaldo del Certificado de Calidad">
                         <asp:Image ID="Image1" runat="server" ImageUrl="~/UI/img/iconos/adj.png" AlternateText="Agregar documento" />
                     </a>
                     <a runat="server" ID="lnkModificar" href='<%# "wfModificar.aspx?Cite="+Eval("CITE_GENERADO") %>' class="lnkModDoc" title="Respaldo del Certificado de Calidad">
                         <asp:Image ID="Image2" runat="server" ImageUrl="~/UI/img/iconos/verDoc.png" AlternateText="Opciones del documento" />
                     </a>
                     
                    
                     <%--<a runat="server" ID="lnkVer" href='<%# "wfVeDocumento.aspx?Cite="+Eval("CITE_GENERADO")+"&idTipoRespaldo=1" %>' class="lnkVentana" title="Respaldo del Certificado de Calidad">Ver documento<br/></a>--%>
                 </DataItemTemplate>
             </dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn Caption="Entidad" FieldName="ENTIDAD" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Actividad" FieldName="TIPO_ACTIVIDAD" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tipo Alerta" FieldName="TIPO_ALERTA" VisibleIndex="10">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Resolución Adm." FieldName="RESOLUCION" VisibleIndex="3" Name="resadm">
            </dx:GridViewDataTextColumn>
        </Columns>
        <ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />
        <SettingsPager PageSize="20"><PageSizeItemSettings Visible="true" ShowAllItem="true" /></SettingsPager>
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterRowMenu="True" />
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
    </div>
    <dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdCertificadoCalidad">
    </dx:ASPxGridViewExporter>
</asp:Content>
