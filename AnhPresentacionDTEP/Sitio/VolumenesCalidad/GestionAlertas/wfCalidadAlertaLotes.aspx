<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfCalidadAlertaLotes.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAlertas.wfCalidadAlertaLotes" %>
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
    <%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfCalidadAlertaLotes.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAlertas.wfCalidadAlertaLotes" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
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

    <style type="text/css">
        .floatL {
            float: left;
            margin: 3px;
        }
        .botones {
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="section-title" style="color: #2D7E3E; font-size: 20px;">Reporte de Certificados Observados por Duplicidad en Datos</h1>
<hr class="Hydro_Linea_Verde"/>
<div class="filter-card">
<table width="100%">
    <tr>
        <td style="width: 25%">Búsqueda por Fecha de Operación</td>
        <td style="width: 25%">
            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" 
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
            <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" 
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
        <td style="width: 25%"><asp:Button ID="Button1" runat="server" Text="Aceptar" ClientIDMode="Static" 
                onclick="btnAceptar_Click" BackColor="#2D7E3E" ForeColor="White" /></td>
    </tr>
</table>
</div>
    <div class="export-toolbar">
    <table>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" title="Exportar datos de la grilla a Excel" CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-excel"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Excel</span></asp:LinkButton>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:LinkButton ID="LinkButton2" runat="server" title="Exportar datos de la grilla a Pdf" CssClass="Hydro_Boton_Blanco" OnClick="btnPdf_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-pdf"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a PDF</span></asp:LinkButton>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:LinkButton ID="LinkButton3" runat="server" title="Exportar datos de la grilla a Word" CssClass="Hydro_Boton_Blanco" OnClick="btnWord_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-word"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Word</span></asp:LinkButton>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
    </div>

<div class="grid-responsive-wrap">
<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grdCertificadoCalidad" runat="server" CssClass="Hydro_Content_Table" AutoGenerateColumns="False" Width="100%">
        
        <ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />
        
        
        <Columns>
            <dx:GridViewDataColumn Caption="Certificado" VisibleIndex="23" >
                <DataItemTemplate>
                    <dx:ASPxLabel ID="lblRowID" runat="server" Text="ASPxLabel" Visible="False"></dx:ASPxLabel>

                    <asp:Panel ID="pnlDetalles" runat="server" CssClass="floatL" HorizontalAlign="Center">
                        <a href="../Reportes/WfReporteCalidad.aspx?idCalPrincipal=<%# DataBinder.Eval(Container.DataItem, "ID_REGISTROCAL_PRINCIPAL") %>&cite=<%# DataBinder.Eval(Container.DataItem, "CITE") %>&fec=<%# DataBinder.Eval(Container.DataItem, "FECHA_REGISTRO").ToString().Replace("/","_")%> " class="lnkEditarCal">
                        <asp:Image ID="imgDetalles" runat="server" ImageUrl="~/UI/img/iconos/Detalles.png" AlternateText="Detalles" ImageAlign="AbsMiddle" /></a>
                    </asp:Panel>

                </DataItemTemplate>
                <CellStyle CssClass="botones">
                </CellStyle>
            </dx:GridViewDataColumn>
            
            <dx:GridViewDataTextColumn Caption="Fecha Op." FieldName="FECHA_OPERACION" Width="10%" 
                VisibleIndex="4">
                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy" DisplayFormatInEditMode="True">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Cite" FieldName="CITE" Width="18%"
                VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nombre producto" FieldName="NOMBRE_PRODUCTO" Width="18%"
                VisibleIndex="8"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nombre comercial" FieldName="NOMBRE_COMERCIAL" Width="18%"
                VisibleIndex="9"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Lote" FieldName="LOTE" 
                VisibleIndex="10" Width="8%"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tanque" FieldName="PTC" 
                VisibleIndex="12" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Resolucion" FieldName="RESOLUCION" 
                VisibleIndex="13" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Volúmen" FieldName="VOLUMEN" 
                VisibleIndex="14" Width="8%">
                <Settings AutoFilterCondition="BeginsWith"></Settings>
                <PropertiesTextEdit MaxLength="5" ></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Unidad" Width="5%"
                FieldName="UNIDAD_MEDIDA" VisibleIndex="16"></dx:GridViewDataTextColumn>
            
            <dx:GridViewDataDateColumn Caption="Fecha Registro" FieldName="FECHA_REGISTRO" 
                VisibleIndex="18" Width="10%">
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
            
            <dx:GridViewDataTextColumn Caption="Usuario" FieldName="USUARIO" 
                VisibleIndex="19" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn Caption="Entidad" FieldName="ENTIDAD" 
                VisibleIndex="20" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn Caption="Cite Relacionado" FieldName="ALERTA" 
                VisibleIndex="22" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tipo Alerta" FieldName="TIPO" VisibleIndex="21">
            </dx:GridViewDataTextColumn>
        </Columns>

        <SettingsPager PageSize="20"><PageSizeItemSettings Visible="true" ShowAllItem="true" /></SettingsPager>
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterRowMenu="True"/>
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
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdCertificadoCalidad">
    </dx:ASPxGridViewExporter>
</asp:Content>


    <style type="text/css">
        .floatL {
            float: left;
            margin: 3px;
        }
        .botones {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Reporte de Certificados Observados por Duplicidad en Datos</h1>
<hr class="Hydro_Linea_Verde"/>
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

<dx:ASPxGridView ID="grdCertificadoCalidad" ClientInstanceName="grdCertificadoCalidad" runat="server" CssClass="Hydro_Content_Table" AutoGenerateColumns="False" Width="100%">
        
        <ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />
        
        
        <Columns>
            <dx:GridViewDataColumn Caption="Certificado" VisibleIndex="23" >
                <DataItemTemplate>
                    <dx:ASPxLabel ID="lblRowID" runat="server" Text="ASPxLabel" Visible="False"></dx:ASPxLabel>

                    <asp:Panel ID="pnlDetalles" runat="server" CssClass="floatL" HorizontalAlign="Center">
                        <a href="../Reportes/WfReporteCalidad.aspx?idCalPrincipal=<%# DataBinder.Eval(Container.DataItem, "ID_REGISTROCAL_PRINCIPAL") %>&cite=<%# DataBinder.Eval(Container.DataItem, "CITE") %>&fec=<%# DataBinder.Eval(Container.DataItem, "FECHA_REGISTRO").ToString().Replace("/","_")%> " class="lnkEditarCal">
                        <asp:Image ID="imgDetalles" runat="server" ImageUrl="~/UI/img/iconos/Detalles.png" AlternateText="Detalles" ImageAlign="AbsMiddle" /></a>
                    </asp:Panel>

                </DataItemTemplate>
                <CellStyle CssClass="botones">
                </CellStyle>
            </dx:GridViewDataColumn>
            
            <dx:GridViewDataTextColumn Caption="Fecha Op." FieldName="FECHA_OPERACION" Width="10%" 
                VisibleIndex="4">
                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy" DisplayFormatInEditMode="True">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Cite" FieldName="CITE" Width="18%"
                VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nombre producto" FieldName="NOMBRE_PRODUCTO" Width="18%"
                VisibleIndex="8"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nombre comercial" FieldName="NOMBRE_COMERCIAL" Width="18%"
                VisibleIndex="9"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Lote" FieldName="LOTE" 
                VisibleIndex="10" Width="8%"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tanque" FieldName="PTC" 
                VisibleIndex="12" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Resolucion" FieldName="RESOLUCION" 
                VisibleIndex="13" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Volúmen" FieldName="VOLUMEN" 
                VisibleIndex="14" Width="8%">
                <Settings AutoFilterCondition="BeginsWith"></Settings>
                <PropertiesTextEdit MaxLength="5" ></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Unidad" Width="5%"
                FieldName="UNIDAD_MEDIDA" VisibleIndex="16"></dx:GridViewDataTextColumn>
            
            <dx:GridViewDataDateColumn Caption="Fecha Registro" FieldName="FECHA_REGISTRO" 
                VisibleIndex="18" Width="10%">
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
            
            <dx:GridViewDataTextColumn Caption="Usuario" FieldName="USUARIO" 
                VisibleIndex="19" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn Caption="Entidad" FieldName="ENTIDAD" 
                VisibleIndex="20" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn Caption="Cite Relacionado" FieldName="ALERTA" 
                VisibleIndex="22" Width="8%">
                <Settings HeaderFilterMode="CheckedList" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tipo Alerta" FieldName="TIPO" VisibleIndex="21">
            </dx:GridViewDataTextColumn>
        </Columns>

        <SettingsPager PageSize="20"><PageSizeItemSettings Visible="true" ShowAllItem="true" /></SettingsPager>
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterRowMenu="True"/>
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
</asp:Content>

