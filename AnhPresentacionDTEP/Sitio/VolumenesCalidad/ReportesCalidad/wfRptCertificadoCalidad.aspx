<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfRptCertificadoCalidad.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad.wfRptCertificadoCalidad" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



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
            } else
                if (e.buttonID == 'EliminaDirecto') {
                    e.processOnServer = confirm('¿ESTA SEGURO DE ELIMINAR EL CERTIFICADO DE CALIDAD?');;
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

    <%--<script type="text/javascript">
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
    </script>--%>

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

        /* ─── Botón Buscar ─── */
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

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" width="100%">
    <h2 class="section-title" style="color: #2D7E3E">Reporte Administrador</h2>
    <hr class="Hydro_Linea_Verde" />
    <div class="filter-card">
    <table style="width: 100%">
        <tr>
            <td style="width: 25%">
                <div style="text-align: left">Búsqueda por Fecha de Operación</div>
            </td>
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
                    EditFormat="Custom" ClientIDMode="Static" ClientInstanceName="txtFechaInicial" Caption="Fecha Final:">
                    <CalendarProperties ShowWeekNumbers="False">
                    </CalendarProperties>
                    <TimeSectionProperties>
                        <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                    </TimeSectionProperties>
                </dx:ASPxDateEdit>

            </td>
            <td>
                <asp:Button ID="btnAceptar" runat="server" Text="Buscar" ClientIDMode="Static"
                    OnClick="btnAceptar_Click" BackColor="#2D7E3E" ForeColor="White" />

            </td>
            <td>
                
            </td>
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
        OnCustomCallback="grdCertificadoCalidad_CustomCallback" Width="100%" OnCustomButtonInitialize="grdCertificadoCalidad_CustomButtonInitialize">
        <%--OnHtmlRowCreated="grdCertificadoCalidad_HtmlRowCreated"
        OnHtmlDataCellPrepared="grdCertificadoCalidad_OnHtmlDataCellPrepared"--%>

        

        <ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />

        

        <ClientSideEvents CustomButtonClick="grid_CustomButtonClick" />

        

        <SettingsPager PageSize="20">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" />
        </SettingsPager>
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterRowMenu="True" HorizontalScrollBarMode="Visible" />
        <SettingsPopup>
            <HeaderFilter MinHeight="300" MinWidth="300"></HeaderFilter>
        </SettingsPopup>

        <Columns>
            <dx:GridViewDataColumn Caption="Certificado" VisibleIndex="23" Name="Certificado">
                <DataItemTemplate>
                    <dx:ASPxLabel ID="lblRowID" runat="server" Text="ASPxLabel" Visible="False"></dx:ASPxLabel>

                    <asp:Panel ID="pnlDetalles" runat="server" CssClass="floatL" HorizontalAlign="Center">
                        <a href="../Reportes/WfReporteCalidad.aspx?idCalPrincipal=<%# DataBinder.Eval(Container.DataItem, "ID_REGISTROCAL_PRINCIPAL") %>&cite=<%# DataBinder.Eval(Container.DataItem, "CITE_GENERADO") %>&fec=<%# DataBinder.Eval(Container.DataItem, "FECHA_REGISTRO").ToString().Replace("/","_")%> " class="lnkEditarCal">
                          <asp:Image ID="imgDetalles" runat="server" ImageUrl="~/UI/img/iconos/Detalles.png" AlternateText="Detalles" ImageAlign="AbsMiddle" /></a>
                    </asp:Panel>

                </DataItemTemplate>
                <CellStyle CssClass="botones" HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataColumn>

            <dx:GridViewCommandColumn VisibleIndex="24" ButtonType="Image" Name="ComandosEdiEli" ShowClearFilterButton="True" Caption="Opciones">
                <CustomButtons>
                    <%--<dx:GridViewCommandColumnCustomButton ID="Detalles">
                        <Image ToolTip="Ver Certificado" Url="~/UI/img/iconos/Detalles.png" >
                            <SpriteProperties CssClass="unodos" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>--%>

                    <dx:GridViewCommandColumnCustomButton ID="Editar">
                        <Image ToolTip="Editar Certificado" Url="~/UI/img/iconos/editar.png" />
                    </dx:GridViewCommandColumnCustomButton>

                    <dx:GridViewCommandColumnCustomButton ID="Eliminar">
                        <Image ToolTip="Eliminar Certificado" Url="~/UI/img/iconos/eliminar.png" />
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="EliminaDirecto" Visibility="Invisible">
                        <Image ToolTip="Eliminar Certificado vacío" Url="~/UI/img/iconos/eliminar.png" />
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewCommandColumn>

            <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Image"
                Name="ComandosDetalle" Visible="False" Caption="1">
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="Cite" FieldName="CITE_GENERADO"
                VisibleIndex="2" Name="CITE_GENERADO">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nombre" FieldName="NOMBRE_PRODUCTO"
                VisibleIndex="3" Name="NOMBRE_PRODUCTO">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Lote/Certificado" FieldName="VALOR_LOTE"
                VisibleIndex="5" Name="VALOR_LOTE">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="PTC" FieldName="PUNTO_CUSTODIO"
                VisibleIndex="7" Name="PUNTO_CUSTODIO">
                <Settings HeaderFilterMode="CheckedList"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Resolución" FieldName="OBSERVACIONES"
                VisibleIndex="9" Name="OBSERVACIONES">
                <Settings HeaderFilterMode="CheckedList"></Settings>
            </dx:GridViewDataTextColumn>
             <dx:GridViewDataDateColumn Caption="Fecha R.A." FieldName="FECHA_RA" VisibleIndex="10" Name="FECHA_RA">
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
            <dx:GridViewDataTextColumn Caption="Volúmen" FieldName="VOLUMEN_OP_DEBE"
                VisibleIndex="12" Name="VOLUMEN_OP_DEBE">
                <Settings AutoFilterCondition="BeginsWith"></Settings>
                <PropertiesTextEdit MaxLength="5"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Unidad"
                FieldName="CODIGO_UNIDAD_MEDIDA" VisibleIndex="13" Name="CODIGO_UNIDAD_MEDIDA">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Fecha Registro" FieldName="FECHA_REGISTRO"
                VisibleIndex="15" Name="FECHA_REGISTRO">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm tt">
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

            <dx:GridViewDataTextColumn Caption="Usuario" FieldName="USUARIO_REGISTRO"
                VisibleIndex="16" Name="USUARIO_REGISTRO">
                <CellStyle Font-Size="10px">       </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Entidad" FieldName="ENTIDAD_DENOMINACION"
                VisibleIndex="17" Name="ENTIDAD_DENOMINACION">
            </dx:GridViewDataTextColumn>
              <dx:GridViewDataTextColumn Caption="Entidad Destino" FieldName="ENTIDAD_DEST"
                VisibleIndex="18" Name="ENTIDAD_DEST">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn VisibleIndex="23" Caption="Respaldo" Name="Ver">
                <DataItemTemplate>
                    <a runat="server" id="lnkVer" href='<%# "../GestionAlertas/wfVeDocumento.aspx?Cite="+Eval("CITE_GENERADO") %>' class="lnkVentana" title="Respaldo del Certificado de Calidad">
                        <asp:Image ID="imgDetalles" runat="server" ImageUrl="~/UI/img/iconos/verDoc.png" AlternateText="Ver Documento" ImageAlign="AbsMiddle" />
                    </a>

                </DataItemTemplate>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewDataColumn>

            <dx:GridViewDataDateColumn Caption="Fecha Op." FieldName="FECHA_OPERACION" VisibleIndex="1" Name="FECHA_OPERACION" SortIndex="0" SortOrder="Descending">
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

            <dx:GridViewDataTextColumn Caption="Actividad" FieldName="TIPO_ACTIVIDAD" Name="TIPO_ACTIVIDAD" VisibleIndex="22">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="PTC Orig. Bases/Mod. Transporte" FieldName="NOMBRE_PRODUCTO_COM" Name="NOMBRE_PRODUCTO_COM" VisibleIndex="8">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Descripcion" FieldName="OBS_DOCUMENTO" Name="OBS_DOCUMENTO" VisibleIndex="11">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Ruta de Internacion/N°" FieldName="RUTA_INTERNACION" Name="RUTA_INTERNACION" VisibleIndex="20">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Empresa Proveedora" FieldName="EMPRESA_PROVEEDORA" Name="EMPRESA_PROVEEDORA" VisibleIndex="19">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="TK Org. Externo" FieldName="TK_ORIG_EXTERNO" Name="TK_ORIG_EXTERNO" VisibleIndex="20">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nro Lote/Verificación" FieldName="NRO_LOTE_VERIF" Name="NRO_LOTE_VERIF" VisibleIndex="21">
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
