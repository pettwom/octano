<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfRptCertificadoCalidad.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad.wfRptCertificadoCalidad" %>
<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function habilitarEdicion(s, e1) {
            if ($(".lnkEditarCal").length > 0) {
                $(".lnkEditarCal").off("click");
                $(".lnkEditarCal").on("click", function (e) {
                    e.preventDefault();
                    self.parent.presentacion.mostrarFancyboxAncho(this, 'Detalle del certificado de calidad registrado', 670, 600);
                });
                $(".lnkVentana").on("click", function (e) {
                    e.preventDefault();
                    self.parent.presentacion.mostrarFancyboxAncho(this, 'Respaldo del Certificado de Calidad', 930, 420);
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


    <style type="text/css">
        .floatL { float: left; margin: 3px; }
        .botones { }

        /* ─── Títulos ─── */
        .section-title {
            font-weight: 700;
            letter-spacing: 0.4px;
            margin-bottom: 6px;
            font-size: 20px;
        }

        /* ─── Card de filtros ─── */
        .filter-card {
            background: #f8f9fa;
            border: 1px solid #dee2e6;
            border-radius: 10px;
            padding: 18px 22px;
            margin-bottom: 22px;
            box-shadow: 0 1px 5px rgba(0,0,0,0.05);
        }
        .filter-card td {
            padding: 6px 12px;
            vertical-align: middle;
        }

        /* ─── Column toggle ─── */
        .column-toggle {
            display: flex;
            align-items: center;
            gap: 10px;
            padding: 10px 0 16px;
            flex-wrap: wrap;
        }
        .column-toggle input[type="checkbox"] {
            accent-color: #2D7E3E;
            width: 17px;
            height: 17px;
            cursor: pointer;
            margin: 0;
        }
        .column-toggle label {
            font-weight: 500;
            font-size: 13.5px;
            color: #333;
            cursor: pointer;
            user-select: none;
            margin: 0;
        }
        .column-toggle-hint {
            font-size: 12.5px;
            color: #888;
            font-style: italic;
        }

        /* ─── Wrapper responsive para grilla ─── */
        .grid-responsive-wrap {
            overflow-x: auto;
            width: 100%;
            -webkit-overflow-scrolling: touch;
            border: 1px solid #dee2e6;
            border-radius: 10px;
            background: #fff;
            box-shadow: 0 1px 5px rgba(0,0,0,0.05);
        }

        /* ─── Botón Aceptar ─── */
        #btnAceptar {
            transition: background 0.2s ease, box-shadow 0.2s ease, transform 0.1s ease;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-weight: 600;
            font-size: 14px;
            padding: 9px 28px !important;
            box-shadow: 0 1px 3px rgba(0,0,0,0.12);
            letter-spacing: 0.3px;
            min-width: 110px;
        }
        #btnAceptar:hover {
            background-color: #236832 !important;
            box-shadow: 0 4px 10px rgba(45,126,62,0.35);
            transform: translateY(-1px);
        }
        #btnAceptar:active {
            transform: translateY(0) scale(0.97);
        }

        /* ─── Toolbar de exportación ─── */
        .export-toolbar {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
            align-items: center;
            margin: 16px 0 10px;
        }
        .export-toolbar .Hydro_Boton_Blanco {
            border-radius: 6px !important;
            padding: 8px 18px !important;
            border: 1px solid #2D7E3E !important;
            transition: all 0.2s ease !important;
            font-weight: 500 !important;
            font-size: 13px !important;
            line-height: 1.4 !important;
            text-decoration: none !important;
            letter-spacing: 0.2px;
        }
        .export-toolbar .Hydro_Boton_Blanco:hover {
            background-color: #236832 !important;
            border-color: #236832 !important;
            box-shadow: 0 3px 8px rgba(45,126,62,0.25);
            transform: translateY(-1px);
        }
        .export-toolbar .Hydro_Boton_Blanco:active {
            transform: translateY(0);
        }

        /* ─── Grilla — hover sutil ─── */
        .grid-responsive-wrap .dxgvControl tr.dxgvDataRow:hover {
            background-color: #f0f6f0 !important;
        }
        .grid-responsive-wrap .Hydro_Td_Titulo {
            white-space: nowrap;
        }

        /* ─── Responsive ─── */
        @media (max-width: 767px) {
            .section-title {
                font-size: 17px;
            }

            .filter-card td {
                padding: 5px 6px;
            }
            .filter-card td center {
                text-align: left;
            }

            #btnAceptar {
                width: 100%;
                padding: 10px !important;
            }

            .export-toolbar {
                flex-direction: column;
                align-items: stretch;
            }
            .export-toolbar .Hydro_Boton_Blanco {
                text-align: center;
            }

            .column-toggle {
                flex-direction: column;
                align-items: flex-start;
                gap: 4px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title" style="color: #2D7E3E">Reporte Operador</h2>
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
                EditFormat="Custom" ClientIDMode="Static" ClientInstanceName="txtFechaInicial" Caption="FechaInicial:">
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
        <td style="width: 25%"><asp:Button ID="btnAceptar" runat="server" Text="Aceptar" ClientIDMode="Static" 
                onclick="btnAceptar_Click" BackColor="#2D7E3E" ForeColor="White" /></td>
    </tr>
</table>
</div>

    <div class="export-toolbar">
        <asp:LinkButton ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel" CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-excel"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Excel</span></asp:LinkButton>
        <asp:LinkButton ID="btnPdf" runat="server" title="Exportar datos de la grilla a Pdf" CssClass="Hydro_Boton_Blanco" OnClick="btnPdf_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-pdf"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a PDF</span></asp:LinkButton>
        <asp:LinkButton ID="btnWord" runat="server" title="Exportar datos de la grilla a Word" CssClass="Hydro_Boton_Blanco" OnClick="btnWord_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-word"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Word</span></asp:LinkButton>
    </div>

    <div class="column-toggle">
        <asp:CheckBox ID="chkMostrarColumnas" runat="server" Text="Mostrar columnas extendidas" AutoPostBack="true" />
        <span class="column-toggle-hint">(desmarcar oculta: PTC Orig., Empresa Proveedora, Fecha R.A., Ruta de Internación, Descripción, TK Org. Externo y Nro Lote/Verificación)</span>
    </div>

<div class="grid-responsive-wrap">
<dx:ASPxGridView ID="grdCertificadoCalidad" 
        ClientInstanceName="grdCertificadoCalidad" runat="server" 
        CssClass="Hydro_Content_Table" AutoGenerateColumns="False"
        OnCustomButtonCallback="grdCertificadoCalidad_CustomButtonCallback" 
        oncustomcallback="grdCertificadoCalidad_CustomCallback" Width="100%" OnCustomButtonInitialize="grdCertificadoCalidad_CustomButtonInitialize">
        
        <ClientSideEvents CustomButtonClick="grid_CustomButtonClick" />
        
    
        <ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />
        
        <SettingsPager PageSize="20"><PageSizeItemSettings Visible="true" ShowAllItem="true" /></SettingsPager>
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterRowMenu="True" HorizontalScrollBarMode="Visible" />
        <SettingsPopup> <HeaderFilter MinHeight="300" MinWidth="300"></HeaderFilter> </SettingsPopup>
        
        <Columns>
            
            <%--<dx:GridViewDataTextColumn Caption="Estado" FieldName="ESTADO" Width="18%"
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataColumn Caption="Respaldo" VisibleIndex="21" Name="Respaldo" ShowInCustomizationForm="True" >
                <DataItemTemplate>
                    <a runat="server" id="lnkVer" href='<%# "../GestionAlertas/wfVeDocumento.aspx?Cite="+Eval("CITE_GENERADO") %>' class="lnkVentana" title="Respaldo del Certificado de Calidad">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/UI/img/iconos/verDoc.png" AlternateText="Ver Documento" ImageAlign="AbsMiddle" />
                    </a> 

                </DataItemTemplate>
                <CellStyle CssClass="botones">                </CellStyle>
            </dx:GridViewDataColumn>

            <dx:GridViewDataColumn Caption="Certificado" VisibleIndex="22" Name="ComandosDetalle" ShowInCustomizationForm="True" >
                <DataItemTemplate>
                    <dx:ASPxLabel ID="lblRowID" runat="server" Text="ASPxLabel" Visible="False"></dx:ASPxLabel>

                    <asp:Panel ID="pnlDetalles" runat="server" CssClass="floatL" HorizontalAlign="Center">
                        <a href="../Reportes/WfReporteCalidad.aspx?idCalPrincipal=<%# DataBinder.Eval(Container.DataItem, "ID_REGISTROCAL_PRINCIPAL") %>&cite=<%# DataBinder.Eval(Container.DataItem, "CITE_GENERADO") %>&act=<%# DataBinder.Eval(Container.DataItem, "ID_TIPO_ACTIVIDAD") %>&fec=<%# DataBinder.Eval(Container.DataItem, "FECHA_REGISTRO").ToString().Replace("/","_")%> " class="lnkEditarCal">
                        <asp:Image ID="imgDetalles" runat="server" ImageUrl="~/UI/img/iconos/Detalles.png" AlternateText="Detalles" ImageAlign="AbsMiddle" /></a>
                    </asp:Panel>

                </DataItemTemplate>
                <CellStyle CssClass="botones">
                </CellStyle>
            </dx:GridViewDataColumn>



            

            <dx:GridViewCommandColumn VisibleIndex="23" ButtonType="Image" Name="ComandosEdiEli" Visible="true" ShowClearFilterButton="True" Caption="Opciones" ShowInCustomizationForm="True">
                <CustomButtons>
                    <%--<dx:GridViewCommandColumnCustomButton ID="Detalles">
                        <Image ToolTip="Ver Certificado" Url="~/UI/img/iconos/Detalles.png" >
                            <SpriteProperties CssClass="unodos" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>--%>

                    <dx:GridViewCommandColumnCustomButton ID="Editar">
                        <Image ToolTip="Editar Certificado" Url="~/UI/img/iconos/editar.png" />
                    </dx:GridViewCommandColumnCustomButton>

                    <%--<dx:GridViewCommandColumnCustomButton ID="Eliminar">
                        <Image ToolTip="Eliminar Certificado" Url="~/UI/img/iconos/eliminar.png" />
                    </dx:GridViewCommandColumnCustomButton>--%>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            
            <dx:GridViewCommandColumn VisibleIndex="24" ButtonType="Image" Name="ComandosDetalle" Visible="False" Width="0" ShowInCustomizationForm="True">                
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="Cite" FieldName="CITE_GENERADO" VisibleIndex="1" Name="CITE_GENERADO" ShowInCustomizationForm="True">
                <Settings AllowSort="True" AutoFilterCondition="Contains" ShowInFilterControl="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tabla Específica" FieldName="NOMBRE_PRODUCTO" VisibleIndex="2" Name="NOMBRE_PRODUCTO" ShowInCustomizationForm="True">
                <Settings AllowSort="True" AutoFilterCondition="Contains" ShowInFilterControl="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="PTC" FieldName="PUNTO_CUSTODIO" VisibleIndex="3" Name="PUNTO_CUSTODIO" ShowInCustomizationForm="True">
                <Settings AllowSort="True" AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" ShowInFilterControl="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Resolucion" FieldName="OBSERVACIONES" 
                VisibleIndex="5" Name="OBSERVACIONES" ShowInCustomizationForm="True">
                <Settings AllowSort="True" AutoFilterCondition="Contains" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Lote/Certificado" FieldName="VALOR_LOTE" 
                VisibleIndex="8"  Name="VALOR_LOTE" ShowInCustomizationForm="True">
                <Settings HeaderFilterMode="CheckedList" ShowInFilterControl="True" ></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Volúmen" FieldName="VOLUMEN_OP_DEBE" 
                VisibleIndex="9" Name="VOLUMEN_OP_DEBE" ShowInCustomizationForm="True">
                <PropertiesTextEdit MaxLength="5">
                </PropertiesTextEdit>
                <Settings AllowSort="True" AutoFilterCondition="BeginsWith" ShowInFilterControl="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Unidad" FieldName="CODIGO_UNIDAD_MEDIDA" 
                VisibleIndex="10" Name="CODIGO_UNIDAD_MEDIDA" ShowInCustomizationForm="True">
                <Settings AutoFilterCondition="BeginsWith" AllowSort="True" ShowInFilterControl="True"></Settings>
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataDateColumn Caption="Fecha Registro" FieldName="FECHA_REGISTRO"     VisibleIndex="11"  Name="FECHA_REGISTRO" ShowInCustomizationForm="True">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm ">
                    <TimeSectionProperties>
                    <TimeEditProperties>
                    <ClearButton Visibility="Auto"></ClearButton>
                    </TimeEditProperties>
                    </TimeSectionProperties>
                    <ClearButton Visibility="Auto"></ClearButton>
                </PropertiesDateEdit>
                <Settings AutoFilterCondition="BeginsWith"></Settings>
                <CellStyle Font-Size="9px">       </CellStyle>
            </dx:GridViewDataDateColumn>
            
            <dx:GridViewDataDateColumn Caption="Fecha Op." FieldName="FECHA_OPERACION" VisibleIndex="0" Name="FECHA_OPERACION" SortIndex="0" SortOrder="Descending" ShowInCustomizationForm="True">
                <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy">
                    <TimeSectionProperties>
                        <TimeEditProperties>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </TimeEditProperties>
                    </TimeSectionProperties>
                    <ClearButton Visibility="Auto">
                    </ClearButton>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            
            <dx:GridViewDataTextColumn Caption="Actividad" FieldName="TIPO_ACTIVIDAD" Name="TIPO_ACTIVIDAD" VisibleIndex="19" ShowInCustomizationForm="True">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Usuario" FieldName="USUARIO_REGISTRO" Name="USUARIO_REGISTRO" VisibleIndex="12" ShowInCustomizationForm="True">
                <CellStyle Font-Size="9px">       </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Entidad" FieldName="ENTIDAD_DENOMINACION" Name="ENTIDAD_DENOMINACION" VisibleIndex="13" ShowInCustomizationForm="True">
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" Name="ID_TIPO_ACTIVIDAD" Visible="False" VisibleIndex="20" ShowInCustomizationForm="True">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ver" Name="Ver" Visible="False" VisibleIndex="25" Width="0" ShowInCustomizationForm="True">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="PTC Orig. Bases/Mod. Transporte" FieldName="NOMBRE_PRODUCTO_COM" Name="NOMBRE_PRODUCTO_COM" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Descripcion" FieldName="OBS_DOCUMENTO" Name="OBS_DOCUMENTO" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Fecha R.A." FieldName="FECHA_RA" VisibleIndex="6" Name="FECHA_RA">
                <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy">
                    <TimeSectionProperties>
                        <TimeEditProperties>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </TimeEditProperties>
                    </TimeSectionProperties>
                    <ClearButton Visibility="Auto">
                    </ClearButton>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>  
            <dx:GridViewDataTextColumn Caption="Entidad Destino" FieldName="ENTIDAD_DEST" Name="ENTIDAD_DEST" VisibleIndex="14">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ruta de Internación/N°" FieldName="RUTA_INTERNACION" Name="RUTA_INTERNACION" VisibleIndex="16">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Empresa Proveedora" FieldName="EMPRESA_PROVEEDORA" Name="EMPRESA_PROVEEDORA" VisibleIndex="15">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="TK Org. Externo" FieldName="TK_ORIG_EXTERNO" Name="TK_ORIG_EXTERNO" VisibleIndex="17">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nro Lote/Verificación" FieldName="NRO_LOTE_VERIF" Name="NRO_LOTE_VERIF" VisibleIndex="18">
            </dx:GridViewDataTextColumn>
        </Columns>
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
        <Paddings Padding="5px" />
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdCertificadoCalidad">
    </dx:ASPxGridViewExporter>
    </div>
</asp:Content>