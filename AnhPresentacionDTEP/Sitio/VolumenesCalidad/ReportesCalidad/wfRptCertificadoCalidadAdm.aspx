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

    //$(document).ready(function () {
    //    var id = $(this).attr("id");
    //    $(".lnkVentana").fancybox({
    //        'titleShow': false,
    //        'autoScale': true,
    //        'transitionIn': 'none',
    //        'transitionOut': 'none',
    //        autoSize: false,
    //        width: 930,
    //        height: 400,
    //        'type': 'iframe'
    //    });
    //});
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
    <h2 class="section-title" style="color: #2D7E3E">Reporte Supervisor</h2>
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
                EditFormat="Custom" ClientIDMode="Static" ClientInstanceName="txtFechaFinal" Caption=" FechaFinal:">
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
    <dx:ASPxGridView ID="grdCertificadoCalidad" runat="server" 
        CssClass="Hydro_Content_Table" AutoGenerateColumns="False" Width="100%">
        <SettingsSearchPanel Visible="True" />
        <Columns>
            <dx:GridViewDataColumn Caption="Certificado" VisibleIndex="21"  Name="ComandosDetalle">
                <Settings AllowAutoFilter="False" />
                <DataItemTemplate>
                    <a href="../Reportes/WfReporteCalidad.aspx?idCalPrincipal=<%# DataBinder.Eval(Container.DataItem, "ID_REGISTROCAL_PRINCIPAL") %>&cite=<%# DataBinder.Eval(Container.DataItem, "CITE_GENERADO") %>&act=<%# DataBinder.Eval(Container.DataItem, "ID_TIPO_ACTIVIDAD") %>&fec=<%# DataBinder.Eval(Container.DataItem, "FECHA_REGISTRO").ToString().Replace("/","_")%> " class="lnkEditarCal" title="Reporte">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/UI/img/iconos/Detalles.png" />
                    </a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            
            <dx:GridViewDataTextColumn Caption="Cite" FieldName="CITE_GENERADO" 
                VisibleIndex="1" Name="CITE_GENERADO"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tabla Especifica" FieldName="NOMBRE_PRODUCTO" 
                VisibleIndex="2" Name="NOMBRE_PRODUCTO"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Lote" FieldName="VALOR_LOTE" 
                VisibleIndex="3" Name="VALOR_LOTE"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="PTC" FieldName="PUNTO_CUSTODIO" 
                VisibleIndex="4" Name="PUNTO_CUSTODIO"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Resolucion" FieldName="OBSERVACIONES" 
                VisibleIndex="6" Name="OBSERVACIONES">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Volúmen" FieldName="VOLUMEN_OP_DEBE" 
                VisibleIndex="10" Name="VOLUMEN_OP_DEBE"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Unidad Medida" 
                FieldName="CODIGO_UNIDAD_MEDIDA" VisibleIndex="11" Name="CODIGO_UNIDAD_MEDIDA"></dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Fecha Reg." FieldName="FECHA_REGISTRO" 
                VisibleIndex="12" Name="FECHA_REGISTRO">
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
            <dx:GridViewDataTextColumn Caption="Usuario" FieldName="USUARIO_REGISTRO" 
                VisibleIndex="13" Name="USUARIO_REGISTRO"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Entidad" FieldName="ENTIDAD_DENOMINACION" 
                VisibleIndex="14" Name="ENTIDAD_DENOMINACION"></dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn VisibleIndex="23" Visible="False" Name="ComandosEdiEli"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn VisibleIndex="22" Caption="Respaldo" Name="Ver">
                <DataItemTemplate>
                    
                    <a runat="server" id="A1" href='<%# "../GestionAlertas/wfVeDocumento.aspx?Cite="+Eval("CITE_GENERADO") %>' class="lnkVentana" title="Respaldo del Certificado de Calidad">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/UI/img/iconos/verDoc.png" AlternateText="Ver Documento" ImageAlign="AbsMiddle" />
                    </a>

                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataDateColumn Caption="Fecha Op." FieldName="FECHA_OPERACION" VisibleIndex="0" Name="FECHA_OPERACION" SortIndex="0" SortOrder="Descending">
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
            <dx:GridViewDataTextColumn Caption="Actividad" FieldName="TIPO_ACTIVIDAD" Name="TIPO_ACTIVIDAD" VisibleIndex="20">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" Name="ID_TIPO_ACTIVIDAD" Visible="False" VisibleIndex="24">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="PTC Orig. Bases/Mod. Transporte" FieldName="NOMBRE_PRODUCTO_COM" Name="NOMBRE_PRODUCTO_COM" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Descripcion" FieldName="OBS_DOCUMENTO" Name="OBS_DOCUMENTO" VisibleIndex="9">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Fecha R.A" FieldName="FECHA_RA" VisibleIndex="8" Name="FECHA_RA">
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
            <dx:GridViewDataTextColumn Caption="Entidad Destino" FieldName="ENTIDAD_DEST" Name="ENTIDAD_DEST" VisibleIndex="15">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ruta de Internacion/N°" FieldName="RUTA_INTERNACION" Name="RUTA_INTERNACION" VisibleIndex="17">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Empresa Proveedora" FieldName="EMPRESA_PROVEEDORA" Name="EMPRESA_PROVEEDORA" VisibleIndex="16">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="TK Org. Externo" FieldName="TK_ORIG_EXTERNO" Name="TK_ORIG_EXTERNO" VisibleIndex="18">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nro Lote/Verificación" FieldName="NRO_LOTE_VERIF" Name="NRO_LOTE_VERIF" VisibleIndex="19">
            </dx:GridViewDataTextColumn>
        </Columns>
        <ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />
        <SettingsPager PageSize="20"><PageSizeItemSettings Visible="true" ShowAllItem="true" /></SettingsPager>
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowFilterRowMenu="True" ShowGroupPanel="True" HorizontalScrollBarMode="Visible" />
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

    
        <asp:GridView ID="dgDatosExcel" runat="server" Font-Size="8"  AutoGenerateColumns="false" style ="display:none">

                    <Columns>
                        <%--<asp:BoundField DataField="PCAL413" HeaderText="Gravedad específica a 15.6/15.6 °C ó Densidad a 20 °C"  />--%>
                        <asp:BoundField HeaderText="Fecha Op." DataField="FECHA_OPERACION"  /> 
                        <asp:BoundField HeaderText="Cite" DataField="CITE_GENERADO"  />
                        <asp:BoundField HeaderText="Nombre" DataField="NOMBRE_PRODUCTO"  /> 
                        <asp:BoundField HeaderText="Lote/Certificado" DataField="VALOR_LOTE"  />
                        <asp:BoundField HeaderText="Tanque" DataField="PUNTO_CUSTODIO"  />
                        <asp:BoundField HeaderText="Resolución" DataField="OBSERVACIONES"  />
                        <asp:BoundField HeaderText="Volúmen" DataField="VOLUMEN_OP_DEBE"  />
                        <asp:BoundField HeaderText="Unidad" DataField="CODIGO_UNIDAD_MEDIDA"  />
                        <asp:BoundField HeaderText="Fecha Registro" DataField="FECHA_REGISTRO"  />
                        <asp:BoundField HeaderText="Usuario" DataField="USUARIO_REGISTRO"  />
                        <asp:BoundField HeaderText="Entidad" DataField="ENTIDAD_DENOMINACION"  />
                        <asp:BoundField HeaderText="Actividad" DataField="TIPO_ACTIVIDAD"  />
                        <asp:BoundField DataField="PCAL1" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL2" HeaderText="Tensión de vapor a 100 °F (38 °C)" />
                        <asp:BoundField DataField="PCAL3" HeaderText="Residuo volátil , 95% vol" />
                        <asp:BoundField DataField="PCAL4" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL5" HeaderText="Pentano y más pesados" />
                        <asp:BoundField DataField="PCAL6" HeaderText="Residuo por evaporación 100 ML" />
                        <asp:BoundField DataField="PCAL7" HeaderText="Corrosión lámina de cobre" />
                        <asp:BoundField DataField="PCAL8" HeaderText="Azufre total (*)" />
                        <asp:BoundField DataField="PCAL9" HeaderText="Humedad" />
                        <asp:BoundField DataField="PCAL10" HeaderText="Poder calorífico superior" />
                        <asp:BoundField DataField="PCAL11" HeaderText="Relación V/L = 20 (760 mmHg)" />
                        <asp:BoundField DataField="PCAL12" HeaderText="Tensión de vapor Reid a 100°F (37.8 °C)" />
                        <asp:BoundField DataField="PCAL13" HeaderText="Contenido de Plomo (**)" />
                        <asp:BoundField DataField="PCAL14" HeaderText="Corrosión lámina de cobre (3h/50 °C)" />
                        <asp:BoundField DataField="PCAL15" HeaderText="Contenido de etano" />
                        <asp:BoundField DataField="PCAL16" HeaderText="Gomas existentes" />
                        <asp:BoundField DataField="PCAL17" HeaderText="Azufre total" />
                        <asp:BoundField DataField="PCAL18" HeaderText="Octanaje RON" />
                        <asp:BoundField DataField="PCAL19" HeaderText="Octanaje MON" />
                        <asp:BoundField DataField="PCAL20" HeaderText="Indice antidetonante (RON+MON)/2" />
                        <asp:BoundField DataField="PCAL21" HeaderText="Color" />
                        <asp:BoundField DataField="PCAL22" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL23" HeaderText="Poder calorifico" />
                        <asp:BoundField DataField="PCAL24" HeaderText="Destilaición Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL25" HeaderText="10% vol." />
                        <asp:BoundField DataField="PCAL26" HeaderText="50% vol." />
                        <asp:BoundField DataField="PCAL27" HeaderText="90% vol." />
                        <asp:BoundField DataField="PCAL28" HeaderText="Punto Final" />
                        <asp:BoundField DataField="PCAL29" HeaderText="Residuo" />
                        <asp:BoundField DataField="PCAL30" HeaderText="Contenido de Aromaticos Totales" />
                        <asp:BoundField DataField="PCAL31" HeaderText="Contenido de Olefinas" />
                        <asp:BoundField DataField="PCAL32" HeaderText="Contenido de Benceno" />
                        <asp:BoundField DataField="PCAL33" HeaderText="Contenido de Manganeso" />
                        <asp:BoundField DataField="PCAL34" HeaderText="Contenido de Oxigeno" />
                        <asp:BoundField DataField="PCAL35" HeaderText="Gravedad específica 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL36" HeaderText="Viscosidad cinemática a 40°C" />
                        <asp:BoundField DataField="PCAL37" HeaderText="Indice de viscosidad" />
                        <asp:BoundField DataField="PCAL38" HeaderText="Punto de inflamación" />
                        <asp:BoundField DataField="PCAL39" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL40" HeaderText="GRAVEDAD ESPECÍFICA-X08" />
                        <asp:BoundField DataField="PCAL41" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL42" HeaderText="Análisis de Metales componentes de los aditivos por Absorción Atómica" />
                        <asp:BoundField DataField="PCAL43" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL44" HeaderText="Corrosión lámina de cobre" />
                        <asp:BoundField DataField="PCAL45" HeaderText="Azufre total" />
                        <asp:BoundField DataField="PCAL46" HeaderText="Color Saybolt" />
                        <asp:BoundField DataField="PCAL47" HeaderText="Punto de inflamación" />
                        <asp:BoundField DataField="PCAL48" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL49" HeaderText="Agua y sedimientos" />
                        <asp:BoundField DataField="PCAL50" HeaderText="Viscosidad cinemática a 40 °C" />
                        <asp:BoundField DataField="PCAL51" HeaderText="Destilación Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL52" HeaderText="10% vol." />
                        <asp:BoundField DataField="PCAL53" HeaderText="Punto Final" />
                        <asp:BoundField DataField="PCAL54" HeaderText="Recuperado" />
                        <asp:BoundField DataField="PCAL55" HeaderText="Relación V/L = 20 (760 mmHg)" />
                        <asp:BoundField DataField="PCAL56" HeaderText="Tensión de vapor Reid a 100 °F (37.8 °C)" />
                        <asp:BoundField DataField="PCAL57" HeaderText="Contenido de plomo (**)" />
                        <asp:BoundField DataField="PCAL58" HeaderText="Corrosión lámina de Cobre (3h / 50 °C)" />
                        <asp:BoundField DataField="PCAL59" HeaderText="Gomas existentes" />
                        <asp:BoundField DataField="PCAL60" HeaderText="Azufre total" />
                        <asp:BoundField DataField="PCAL61" HeaderText="Octanaje RON" />
                        <asp:BoundField DataField="PCAL62" HeaderText="Octanaje MON" />
                        <asp:BoundField DataField="PCAL63" HeaderText="Índice antidetonante (RON + MON)/2" />
                        <asp:BoundField DataField="PCAL64" HeaderText="Color" />
                        <asp:BoundField DataField="PCAL65" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL66" HeaderText="Poder calorífico" />
                        <asp:BoundField DataField="PCAL67" HeaderText="Destilación Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL68" HeaderText="10% vol." />
                        <asp:BoundField DataField="PCAL69" HeaderText="50% vol." />
                        <asp:BoundField DataField="PCAL70" HeaderText="90% vol." />
                        <asp:BoundField DataField="PCAL71" HeaderText="Punto Final" />
                        <asp:BoundField DataField="PCAL72" HeaderText="Residuo" />
                        <asp:BoundField DataField="PCAL73" HeaderText="Contenido de Aromáticos Totales" />
                        <asp:BoundField DataField="PCAL74" HeaderText="Contenido de Olefinas" />
                        <asp:BoundField DataField="PCAL75" HeaderText="Contenido de benceno" />
                        <asp:BoundField DataField="PCAL76" HeaderText="Contenido de Manganeso" />
                        <asp:BoundField DataField="PCAL77" HeaderText="Contenido de oxígeno" />
                        <asp:BoundField DataField="PCAL78" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL79" HeaderText="Tensión de vapor Reid a 100 °F (37.8 °C)" />
                        <asp:BoundField DataField="PCAL80" HeaderText="Tetraetilo de plomo" />
                        <asp:BoundField DataField="PCAL81" HeaderText="Corrosión lámina de cobre (2h / 100 °C)" />
                        <asp:BoundField DataField="PCAL82" HeaderText="Gomas potenciales" />
                        <asp:BoundField DataField="PCAL83" HeaderText="Precipitado de plomo visible" />
                        <asp:BoundField DataField="PCAL84" HeaderText="Azufre total" />
                        <asp:BoundField DataField="PCAL85" HeaderText="Octanaje MON" />
                        <asp:BoundField DataField="PCAL86" HeaderText="Número de rendimiento (performance)" />
                        <asp:BoundField DataField="PCAL87" HeaderText="Color" />
                        <asp:BoundField DataField="PCAL88" HeaderText="Calor neto de combustión" />
                        <asp:BoundField DataField="PCAL89" HeaderText="Punto de congelamiento" />
                        <asp:BoundField DataField="PCAL90" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL91" HeaderText="Reacción al agua" />
                        <asp:BoundField DataField="PCAL92" HeaderText="Destilación Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL93" HeaderText="10% vol." />
                        <asp:BoundField DataField="PCAL94" HeaderText="40% vol." />
                        <asp:BoundField DataField="PCAL95" HeaderText="50% vol." />
                        <asp:BoundField DataField="PCAL96" HeaderText="90% vol." />
                        <asp:BoundField DataField="PCAL97" HeaderText="Punto Final" />
                        <asp:BoundField DataField="PCAL98" HeaderText="Residuo" />
                        <asp:BoundField DataField="PCAL99" HeaderText="Suma (10% vol + 50% vol)" />
                        <asp:BoundField DataField="PCAL100" HeaderText="Recuperado" />
                        <asp:BoundField DataField="PCAL101" HeaderText="Pérdidas" />
                        <asp:BoundField DataField="PCAL102" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL103" HeaderText="Corrosión lámina de cobre (2h / 100 °C)" />
                        <asp:BoundField DataField="PCAL104" HeaderText="Gomas existentes" />
                        <asp:BoundField DataField="PCAL105" HeaderText="Azufre total" />
                        <asp:BoundField DataField="PCAL106" HeaderText="Azufre Mercaptan" />
                        <asp:BoundField DataField="PCAL107" HeaderText="Calor neto de combustión" />
                        <asp:BoundField DataField="PCAL108" HeaderText="Punto de congelamiento" />
                        <asp:BoundField DataField="PCAL109" HeaderText="Punto de inflamación" />
                        <asp:BoundField DataField="PCAL110" HeaderText="Punto de humeo" />
                        <asp:BoundField DataField="PCAL111" HeaderText="Acidez total" />
                        <asp:BoundField DataField="PCAL112" HeaderText="Aromáticos" />
                        <asp:BoundField DataField="PCAL113" HeaderText="Viscosidad cinemática a -20 °C (4 °F)" />
                        <asp:BoundField DataField="PCAL114" HeaderText="Reacción al agua, separación" />
                        <asp:BoundField DataField="PCAL115" HeaderText="WSIM" />
                        <asp:BoundField DataField="PCAL116" HeaderText="Caída de presión en el filtro" />
                        <asp:BoundField DataField="PCAL117" HeaderText="Depósitos en precalentador" />
                        <asp:BoundField DataField="PCAL118" HeaderText="Particulas contaminantes (milipore)" />
                        <asp:BoundField DataField="PCAL119" HeaderText="Destilación Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL120" HeaderText="10% vol." />
                        <asp:BoundField DataField="PCAL121" HeaderText="50% vol." />
                        <asp:BoundField DataField="PCAL122" HeaderText="90% vol." />
                        <asp:BoundField DataField="PCAL123" HeaderText="Punto Final" />
                        <asp:BoundField DataField="PCAL124" HeaderText="Residuo" />
                        <asp:BoundField DataField="PCAL125" HeaderText="Perdidas" />
                        <asp:BoundField DataField="PCAL126" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL127" HeaderText="Corrosión lámina de cobre (3h / 100°C)" />
                        <asp:BoundField DataField="PCAL128" HeaderText="Azufre total" />
                        <asp:BoundField DataField="PCAL129" HeaderText="Punto de escurrimiento" />
                        <asp:BoundField DataField="PCAL130" HeaderText="Punto de inflamación" />
                        <asp:BoundField DataField="PCAL131" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL132" HeaderText="Viscosidad cinematica a 40 °C" />
                        <asp:BoundField DataField="PCAL133" HeaderText="Indice de Cetano" />
                        <asp:BoundField DataField="PCAL134" HeaderText="Numero de Cetano" />
                        <asp:BoundField DataField="PCAL135" HeaderText="Residuo Carbonoso Ramsbotton del 10% de residuo destilado" />
                        <asp:BoundField DataField="PCAL136" HeaderText="Cenizas" />
                        <asp:BoundField DataField="PCAL137" HeaderText="Agua y sedimentos" />
                        <asp:BoundField DataField="PCAL138" HeaderText="Destilación Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL139" HeaderText="90% vol." />
                        <asp:BoundField DataField="PCAL140" HeaderText="Poder calorífico" />
                        <asp:BoundField DataField="PCAL141" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL142" HeaderText="Contenido de Aromáticos Totales" />
                        <asp:BoundField DataField="PCAL143" HeaderText="API" />
                        <asp:BoundField DataField="PCAL144" HeaderText="Viscosidad Cinemática a 100 °C" />
                        <asp:BoundField DataField="PCAL145" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL146" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL147" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL148" HeaderText="Gravedad específica a 15.6/15.6 °C o Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL149" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL150" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL151" HeaderText="Clasificación NLGI" />
                        <asp:BoundField DataField="PCAL152" HeaderText="Penetración Trabajada (NLGI 1.2 y 3)" />
                        <asp:BoundField DataField="PCAL153" HeaderText="Punto de Goteo" />
                        <asp:BoundField DataField="PCAL154" HeaderText="API" />
                        <asp:BoundField DataField="PCAL155" HeaderText="Viscosidad Cinemática a 100 °C" />
                        <asp:BoundField DataField="PCAL156" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL157" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL158" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL159" HeaderText="Gravedad específica a 15.6/15.6 °C o Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL160" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL161" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL162" HeaderText="API" />
                        <asp:BoundField DataField="PCAL163" HeaderText="Viscosidad Cinemática a 100 °C" />
                        <asp:BoundField DataField="PCAL164" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL165" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL166" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL167" HeaderText="Gravedad específica a 15.6/15.6 °C o Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL168" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL169" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL170" HeaderText="API" />
                        <asp:BoundField DataField="PCAL171" HeaderText="Viscosidad Cinemática a 100 °C" />
                        <asp:BoundField DataField="PCAL172" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL173" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL174" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL175" HeaderText="Gravedad específica a 15.6/15.6 °C o Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL176" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL177" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL178" HeaderText="API" />
                        <asp:BoundField DataField="PCAL179" HeaderText="Viscosidad Cinemática a 100 °C" />
                        <asp:BoundField DataField="PCAL180" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL181" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL182" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL183" HeaderText="Gravedad específica a 15.6/15.6 °C o Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL184" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL185" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL186" HeaderText="API" />
                        <asp:BoundField DataField="PCAL187" HeaderText="Viscosidad Cinemática a 100 °C" />
                        <asp:BoundField DataField="PCAL188" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL189" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL190" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL191" HeaderText="Gravedad específica a 15.6/15.6 °C o Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL192" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL193" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL194" HeaderText="API" />
                        <asp:BoundField DataField="PCAL195" HeaderText="Viscosidad Cinemática a 100 °C" />
                        <asp:BoundField DataField="PCAL196" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL197" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL198" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL199" HeaderText="Gravedad específica a 15.6/15.6 °C o Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL200" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL201" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL202" HeaderText="Reacción al agua, interfase" />
                        <asp:BoundField DataField="PCAL203" HeaderText="Clasificación NLGI" />
                        <asp:BoundField DataField="PCAL204" HeaderText="Penetración Trabajada (NLGI 1.2 y 3)" />
                        <asp:BoundField DataField="PCAL205" HeaderText="Punto de Goteo" />
                        <asp:BoundField DataField="PCAL206" HeaderText="Clasificación NLGI" />
                        <asp:BoundField DataField="PCAL207" HeaderText="Penetración Trabajada (NLGI 1.2 y 3)" />
                        <asp:BoundField DataField="PCAL208" HeaderText="Punto de Goteo" />
                        <asp:BoundField DataField="PCAL209" HeaderText="Clasificación NLGI" />
                        <asp:BoundField DataField="PCAL210" HeaderText="Penetración Trabajada (NLGI 1.2 y 3)" />
                        <asp:BoundField DataField="PCAL211" HeaderText="Punto de Goteo" />
                        <asp:BoundField DataField="PCAL212" HeaderText="API" />
                        <asp:BoundField DataField="PCAL213" HeaderText="Viscosidad cinematica a 100°" />
                        <asp:BoundField DataField="PCAL214" HeaderText="Indice de viscosidad" />
                        <asp:BoundField DataField="PCAL215" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL216" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL217" HeaderText="Maxima Temperatura para viscosidad de 150.000 cP" />
                        <asp:BoundField DataField="PCAL218" HeaderText="Gravedad específica a 15.6/15.6°C o Densidad a 20°C" />
                        <asp:BoundField DataField="PCAL219" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL220" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL221" HeaderText="específicaciones minimas" />
                        <asp:BoundField DataField="PCAL222" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL223" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL224" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL225" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL226" HeaderText="Corrsion Lamina de Cobre (100°C 3h)" />
                        <asp:BoundField DataField="PCAL227" HeaderText="Gravedad específica a 15.6/15.6 °C (20 °C)" />
                        <asp:BoundField DataField="PCAL228" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL229" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL230" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL231" HeaderText="Residuo por evaporación 100 ml" />
                        <asp:BoundField DataField="PCAL232" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL233" HeaderText="Tension de vapor de Reid a 100 °F (38 °C)" />
                        <asp:BoundField DataField="PCAL234" HeaderText="Color" />
                        <asp:BoundField DataField="PCAL235" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL236" HeaderText="Destilación Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL237" HeaderText="10% vol." />
                        <asp:BoundField DataField="PCAL238" HeaderText="50% vol." />
                        <asp:BoundField DataField="PCAL239" HeaderText="90 % vol." />
                        <asp:BoundField DataField="PCAL240" HeaderText="Punto Final" />
                        <asp:BoundField DataField="PCAL241" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL242" HeaderText="Tensión de vapor de Reid a 100 °F (37.8 °C)" />
                        <asp:BoundField DataField="PCAL243" HeaderText="Color" />
                        <asp:BoundField DataField="PCAL244" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL245" HeaderText="Destilación Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL246" HeaderText="10 % vol" />
                        <asp:BoundField DataField="PCAL247" HeaderText="50 % vol" />
                        <asp:BoundField DataField="PCAL248" HeaderText="90 % vol" />
                        <asp:BoundField DataField="PCAL249" HeaderText="Punto final" />
                        <asp:BoundField DataField="PCAL250" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL251" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL252" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL253" HeaderText="Agua y sedimientos" />
                        <asp:BoundField DataField="PCAL254" HeaderText="Destilación Engler (760 mmHg) 90 % vol" />
                        <asp:BoundField DataField="PCAL255" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL256" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL257" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL258" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL259" HeaderText="Agua y sedimientos" />
                        <asp:BoundField DataField="PCAL260" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL261" HeaderText="Tensión vapor Reid a 100 °F (37.8 °C)" />
                        <asp:BoundField DataField="PCAL262" HeaderText="Color" />
                        <asp:BoundField DataField="PCAL263" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL264" HeaderText="Reacción al Agua" />
                        <asp:BoundField DataField="PCAL265" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL266" HeaderText="Punto de inflamación" />
                        <asp:BoundField DataField="PCAL267" HeaderText="Reacción al agua, separación" />
                        <asp:BoundField DataField="PCAL268" HeaderText="Partículas contaminantes (millipore)" />
                        <asp:BoundField DataField="PCAL269" HeaderText="C3" />
                        <asp:BoundField DataField="PCAL270" HeaderText="i-C4" />
                        <asp:BoundField DataField="PCAL271" HeaderText="n-C4" />
                        <asp:BoundField DataField="PCAL272" HeaderText="i-C5" />
                        <asp:BoundField DataField="PCAL273" HeaderText="n-C5" />
                        <asp:BoundField DataField="PCAL274" HeaderText="Gravedad específica" />
                        <asp:BoundField DataField="PCAL275" HeaderText="Cont. de H2O" />
                        <asp:BoundField DataField="PCAL276" HeaderText="Punto de Rocio" />
                        <asp:BoundField DataField="PCAL277" HeaderText="Poder calorífico" />
                        <asp:BoundField DataField="PCAL278" HeaderText="N2" />
                        <asp:BoundField DataField="PCAL279" HeaderText="CO2" />
                        <asp:BoundField DataField="PCAL280" HeaderText="C1" />
                        <asp:BoundField DataField="PCAL281" HeaderText="C2" />
                        <asp:BoundField DataField="PCAL282" HeaderText="C3" />
                        <asp:BoundField DataField="PCAL283" HeaderText="i-C4" />
                        <asp:BoundField DataField="PCAL284" HeaderText="n-C4" />
                        <asp:BoundField DataField="PCAL285" HeaderText="i-C5" />
                        <asp:BoundField DataField="PCAL286" HeaderText="n-C5" />
                        <asp:BoundField DataField="PCAL287" HeaderText="n-C6" />
                        <asp:BoundField DataField="PCAL288" HeaderText="C7" />
                        <asp:BoundField DataField="PCAL289" HeaderText="i-C4" />
                        <asp:BoundField DataField="PCAL290" HeaderText="n-C4" />
                        <asp:BoundField DataField="PCAL291" HeaderText="i-C5" />
                        <asp:BoundField DataField="PCAL292" HeaderText="n-C5" />
                        <asp:BoundField DataField="PCAL293" HeaderText="n-C6" />
                        <asp:BoundField DataField="PCAL294" HeaderText="C7" />
                        <asp:BoundField DataField="PCAL295" HeaderText="C1" />
                        <asp:BoundField DataField="PCAL296" HeaderText="C2" />
                        <asp:BoundField DataField="PCAL297" HeaderText="C3" />
                        <asp:BoundField DataField="PCAL298" HeaderText="Gravedad específica" />
                        <asp:BoundField DataField="PCAL299" HeaderText="Poder calorífico" />
                        <asp:BoundField DataField="PCAL300" HeaderText="N2" />
                        <asp:BoundField DataField="PCAL301" HeaderText="CO2" />
                        <asp:BoundField DataField="PCAL302" HeaderText="Gravedad específica" />
                        <asp:BoundField DataField="PCAL303" HeaderText="TVR" />
                        <asp:BoundField DataField="PCAL304" HeaderText="Temperatura" />
                        <asp:BoundField DataField="PCAL305" HeaderText="C2" />
                        <asp:BoundField DataField="PCAL306" HeaderText="API" />
                        <asp:BoundField DataField="PCAL307" HeaderText="API" />
                        <asp:BoundField DataField="PCAL308" HeaderText="API" />
                        <asp:BoundField DataField="PCAL309" HeaderText="API" />
                        <asp:BoundField DataField="PCAL310" HeaderText="API" />
                        <asp:BoundField DataField="PCAL311" HeaderText="API" />
                        <asp:BoundField DataField="PCAL312" HeaderText="Viscosidad Cinemática a 100 °C" />
                        <asp:BoundField DataField="PCAL313" HeaderText="Viscosidad Cinemática a 40 °C" />
                        <asp:BoundField DataField="PCAL314" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL315" HeaderText="Punto de inflamación" />
                        <asp:BoundField DataField="PCAL316" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL317" HeaderText="Gravedad específica a 15.6/15.6 °C o Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL318" HeaderText="Maxima Temperatura para viscosidad de 150.000 cP" />
                        <asp:BoundField DataField="PCAL319" HeaderText="Corrosión lámina de cobre (100°C 3h )" />
                        <asp:BoundField DataField="PCAL320" HeaderText="Penetración Trabajada (NLGI)" />
                        <asp:BoundField DataField="PCAL321" HeaderText="Punto de Goteo" />
                        <asp:BoundField DataField="PCAL322" HeaderText="SAE" />
                        <asp:BoundField DataField="PCAL323" HeaderText="Residuo" />
                        <asp:BoundField DataField="PCAL324" HeaderText="Residuo" />
                        <asp:BoundField DataField="PCAL325" HeaderText="SAE" />
                        <asp:BoundField DataField="PCAL326" HeaderText="Residuo" />
                        <asp:BoundField DataField="PCAL327" HeaderText="Residuo" />
                        <asp:BoundField DataField="PCAL328" HeaderText="Viscosidad cinematica a 40 °C" />
                        <asp:BoundField DataField="PCAL329" HeaderText="Gravedad específica a 15.6/15.6 °C" />
                        <asp:BoundField DataField="PCAL330" HeaderText="Corrosión lámina de cobre (3h / 100°C)" />
                        <asp:BoundField DataField="PCAL331" HeaderText="Azufre total" />
                        <asp:BoundField DataField="PCAL332" HeaderText="Punto de escurrimiento" />
                        <asp:BoundField DataField="PCAL333" HeaderText="Punto de inflamación" />
                        <asp:BoundField DataField="PCAL334" HeaderText="Apariencia" />
                        <asp:BoundField DataField="PCAL335" HeaderText="Indice de Cetano" />
                        <asp:BoundField DataField="PCAL336" HeaderText="Numero de Cetano" />
                        <asp:BoundField DataField="PCAL337" HeaderText="Residuo Carbonoso Ramsbotton del 10% de residuo destilado" />
                        <asp:BoundField DataField="PCAL338" HeaderText="Cenizas" />
                        <asp:BoundField DataField="PCAL339" HeaderText="Agua y sedimentos" />
                        <asp:BoundField DataField="PCAL340" HeaderText="Destilación Engler (760 mmHg)" />
                        <asp:BoundField DataField="PCAL341" HeaderText="90% vol." />
                        <asp:BoundField DataField="PCAL342" HeaderText="Poder calorífico" />
                        <asp:BoundField DataField="PCAL343" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL344" HeaderText="Contenido de Aromáticos Totales" />
                        <asp:BoundField DataField="PCAL345" HeaderText="API" />
                        <asp:BoundField DataField="PCAL346" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL347" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL348" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL349" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL350" HeaderText="Gravedad específica a 15.6/15.6 °C ó Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL351" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL352" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL353" HeaderText="API" />
                        <asp:BoundField DataField="PCAL354" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL355" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL356" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL357" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL358" HeaderText="Viscosidad Aparente CCS a -25°C" />
                        <asp:BoundField DataField="PCAL359" HeaderText="Gravedad específica a 15.6/15.6 °C (20 °C)" />
                        <asp:BoundField DataField="PCAL360" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL361" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL362" HeaderText="API" />
                        <asp:BoundField DataField="PCAL363" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL364" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL365" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL366" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL367" HeaderText="Viscosidad Aparente CCS a -20°C" />
                        <asp:BoundField DataField="PCAL368" HeaderText="Gravedad específica a 15.6/15.6 °C ó Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL369" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL370" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL371" HeaderText="API" />
                        <asp:BoundField DataField="PCAL372" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL373" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL374" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL375" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL376" HeaderText="Viscosidad Aparente CCS a -15°C" />
                        <asp:BoundField DataField="PCAL377" HeaderText="Gravedad específica a 15.6/15.6 °C ó Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL378" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL379" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL380" HeaderText="API" />
                        <asp:BoundField DataField="PCAL381" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL382" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL383" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL384" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL385" HeaderText="Viscosidad Aparente CCS" />
                        <asp:BoundField DataField="PCAL386" HeaderText="Gravedad específica a 15.6/15.6 °C ó Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL387" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL388" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL389" HeaderText="API" />
                        <asp:BoundField DataField="PCAL390" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL391" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL392" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL393" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL394" HeaderText="Viscosidad Aparente CCS" />
                        <asp:BoundField DataField="PCAL395" HeaderText="Gravedad específica a 15.6/15.6 °C ó Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL396" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL397" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL398" HeaderText="API" />
                        <asp:BoundField DataField="PCAL399" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL400" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL401" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL402" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL403" HeaderText="Maxima temperatura para viscosidad de 150.000 cP" />
                        <asp:BoundField DataField="PCAL404" HeaderText="Gravedad específica a 15.6/15.6 °C ó Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL405" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL406" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL407" HeaderText="API" />
                        <asp:BoundField DataField="PCAL408" HeaderText="Viscosidad Cinematica a 100°C" />
                        <asp:BoundField DataField="PCAL409" HeaderText="Indice de Viscosidad" />
                        <asp:BoundField DataField="PCAL410" HeaderText="Punto de Inflamación" />
                        <asp:BoundField DataField="PCAL411" HeaderText="Color ASTM" />
                        <asp:BoundField DataField="PCAL412" HeaderText="Maxima temperatura para viscosidad de 150.000 cP" />
                        <asp:BoundField DataField="PCAL413" HeaderText="Gravedad específica a 15.6/15.6 °C ó Densidad a 20 °C" />
                        <asp:BoundField DataField="PCAL414" HeaderText="Análisis FT-IR" />
                        <asp:BoundField DataField="PCAL415" HeaderText="Contenido de Metales" />
                        <asp:BoundField DataField="PCAL416" HeaderText="Humedad" />
                        <asp:BoundField DataField="PCAL417" HeaderText="Poder calorífico superior" />
                        <asp:BoundField DataField="PCAL418" HeaderText="Contenido de etano" />

                    </Columns>

        </asp:GridView>
   

</asp:Content>

