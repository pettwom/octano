<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfRptCalidadGeneral.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad.wfRptCalidadGeneral" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
        .filter-card label {
            font-weight: 600;
            color: #495057;
            font-size: 13px;
        }

        /* ─── Wrapper responsive para PivotGrid ─── */
        .grid-responsive-wrap {
            overflow-x: auto;
            width: 100%;
            -webkit-overflow-scrolling: touch;
            border: 1px solid #e9ecef;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 1px 4px rgba(0,0,0,0.06);
        }

        /* ─── Botón CONSULTAR ─── */
        #btnConsultar {
            transition: background 0.2s ease, box-shadow 0.2s ease, transform 0.1s ease;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-weight: 600;
            font-size: 13px;
            padding: 8px 28px !important;
            letter-spacing: 0.5px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.12);
        }
        #btnConsultar:hover {
            background-color: #236832 !important;
            box-shadow: 0 3px 8px rgba(45,126,62,0.35);
        }
        #btnConsultar:active {
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
        .export-toolbar input[type="submit"] {
            transition: background 0.2s ease, box-shadow 0.2s ease, transform 0.1s ease;
            border-radius: 4px;
            font-weight: 600;
            font-size: 12px;
            padding: 7px 18px;
            border: none;
            letter-spacing: 0.3px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.12);
        }
        .export-toolbar input[type="submit"]:hover {
            background-color: #236832 !important;
            box-shadow: 0 2px 6px rgba(45,126,62,0.25);
        }
        .export-toolbar input[type="submit"]:active {
            transform: scale(0.97);
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

        /* ─── Títulos ─── */
        .section-title {
            font-weight: 600;
            letter-spacing: 0.3px;
            margin-bottom: 4px;
            color: #2D7E3E;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center" width="90%">
        <tr>
            <td>
                    <h3 class="section-title">FORMULARIO DE CONSULTA</h3>
                    <hr class="Hydro_Linea_Verde" />
                    <div class="filter-card">
                        <table width="90%" align="center">
                          <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblEntidad" runat="server" Text="ENTIDAD:"></asp:Label>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxComboBox ID="cmbEntidad" runat="server" ClientInstanceName="cmbEntidad" Width="80%">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {cmbActividad.PerformCallback();}" />
                                </dx:ASPxComboBox>
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lblActividad" runat="server" Text="ACTIVIDAD:"></asp:Label>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxComboBox ID="cmbActividad" runat="server" ClientInstanceName="cmbActividad" OnCallback="cmbActividad_Callback" Width="80%">
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFechaInicio" runat="server" Text="FECHA INICIAL:"></asp:Label>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dtFechaInicial" runat="server"  Width="80%"></dx:ASPxDateEdit>
                                </td>
                                <td >
                                   <asp:Label ID="lblFechaFin" runat="server" Text="FECHA FINAL:"></asp:Label>
                                </td>
                                <td >
                                    <dx:ASPxDateEdit ID="dtFechaFinal" runat="server"  Width="80%"></dx:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCite" runat="server" Text="CITE/CORRELATIVO:"></asp:Label>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtCite" runat="server" Width="80%"></dx:ASPxTextBox>
                                </td>
                                <td >
                                   
                                </td>
                                <td >
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btnConsultar" runat="server" Text="CONSULTAR" OnClick="btnConsultar_Click" BackColor="#2D7E3E" ForeColor="White" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Label ID="lblMensajeConsulta" runat="server" Text=""></asp:Label>
                    </div>
            </td>
        </tr>
    </table>
    <table align="center" width="100%">
        <tr>
            <td>
                    <h3 class="section-title">RESUMEN EJECUTIVO DE PRUEBAS DE CALIDAD.</h3>
                    <hr class="Hydro_Linea_Verde" />
                    <div class="export-toolbar">
                        <asp:Button ID="btnExportar" runat="server" Text="EXPORTAR CONSULTA" OnClick="btnExportar_OnClick" BackColor="#2D7E3E" ForeColor="White"/>
                        <asp:Button ID="btnExportarPlano" runat="server" Text="EXPORTAR TODO" CssClass="button" OnClick="btnExportarPlano_Click" BackColor="#2D7E3E" ForeColor="White"/>
                        <asp:Button ID="btnExportarObsPlano" runat="server" Text="EXPORTAR OBSERVADOS" CssClass="button" OnClick="btnExportarPlanoReducido_Click" BackColor="#2D7E3E" ForeColor="White"/>
                    </div>
                    <dx:ASPxPivotGridExporter ID="exrptPivotCalidad" ASPxPivotGridID="pvGrdCalidad" runat="server"></dx:ASPxPivotGridExporter>
                    <div class="grid-responsive-wrap">
                        <dx:ASPxPivotGrid ID="pvGrdCalidad" runat="server" ClientIDMode="AutoID" Width="100%" Horizontal-align="center" Font-Overline="False" Font-Size="XX-Small" OptionsFilter-GroupFilterMode="List" Font-Strikeout="False" Font-Underline="False" ClientInstanceName="pivotGrid" Theme="Default">
                        <Fields>
                            <dx:PivotGridField ID="fieldIDREGISTROCALIDAD" Area="DataArea" AreaIndex="0" Caption="#" FieldName="ID_REGISTRO_CALIDAD" CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Custom" SummaryType="Count" TotalCellFormat-FormatType="Numeric">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVOLUMEN" Area="RowArea" AreaIndex="4" FieldName="VOLUMEN" CellFormat-FormatType="Numeric" Caption="Volumen lote">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldENTIDAD" Area="RowArea" AreaIndex="0" FieldName="ENTIDAD" CellFormat-FormatType="Custom" Caption="Entidad">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldFechaOperacionFormato" AreaIndex="0" FieldName="FechaOperacionFormato" Caption="Fecha de Operacion" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" Options-GroupFilterMode="List">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldACTIVIDAD" Area="RowArea" AreaIndex="1" FieldName="ACTIVIDAD" CellFormat-FormatType="Custom" Caption="Actividad">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldCORRELATIVO" Area="RowArea" AreaIndex="2" FieldName="CORRELATIVO" CellFormat-FormatType="Custom" Caption="Cite">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldTABLAESPECIFICA" Area="RowArea" AreaIndex="3" FieldName="TABLA_ESPECIFICA" CellFormat-FormatType="Custom" Caption="Producto">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVALORUNIDADMEDIDA" Area="RowArea" AreaIndex="5" FieldName="VALOR_UNIDAD_MEDIDA" CellFormat-FormatType="Custom" Caption="Unidad Volumetrica">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVALORPUNTOCUSTODIO" AreaIndex="1" FieldName="VALOR_PUNTO_CUSTODIO" CellFormat-FormatType="Custom" Caption="Punto Transferencia Custodio">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldRESOLUCION" AreaIndex="2" FieldName="RESOLUCION" CellFormat-FormatType="Custom" Caption="Resolucion">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldFechaImMuFormato" AreaIndex="4" FieldName="FechaImMuFormato" Caption="Fecha de Importacion/Muestra" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVOLUMENMUESTRA" AreaIndex="5" FieldName="VOLUMEN_MUESTRA" CellFormat-FormatType="Numeric" Caption="Volumen de Muestra">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldPRECIO" AreaIndex="6" FieldName="PRECIO" CellFormat-FormatType="Numeric" Caption="Precio">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldCODIGOMONEDA" AreaIndex="7" FieldName="CODIGO_MONEDA" CellFormat-FormatType="Custom" Caption="Moneda">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVALORMARCAPRODUCTO" AreaIndex="8" FieldName="VALOR_MARCA_PRODUCTO" CellFormat-FormatType="Custom" Caption="Marca Producto">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldNOMBREPRODUCTO" AreaIndex="9" FieldName="NOMBRE_PRODUCTO" CellFormat-FormatType="Custom" Caption="Tanques de Origen">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldORDEN" AreaIndex="0" FieldName="ORDEN" Visible="False" CellFormat-FormatType="Numeric">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldPRUEBACALIDAD" Area="RowArea" AreaIndex="6" FieldName="PRUEBA_CALIDAD" CellFormat-FormatType="Custom" Caption="Prueba de Calidad">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVALORMETODOASTM" Area="RowArea" AreaIndex="7" FieldName="VALOR_METODO_ASTM" CellFormat-FormatType="Custom" Caption="ASTM">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVALORUMPRUEBA" Area="RowArea" AreaIndex="8" FieldName="VALOR_UM_PRUEBA" CellFormat-FormatType="Custom" Caption="Unidad de Medida">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVALORREPORTADO" Area="RowArea" AreaIndex="9" FieldName="VALOR_REPORTADO" CellFormat-FormatType="Custom" Caption="Valor Reportado">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldRANGOSMULTIPLES" AreaIndex="12" FieldName="RANGOS_MULTIPLES" CellFormat-FormatType="Custom" Caption="Rangos Multiples">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldALERTA" Area="ColumnArea" AreaIndex="0" FieldName="ALERTA" CellFormat-FormatType="Custom" Caption="Alerta">
                            </dx:PivotGridField>
                            <dx:PivotGridField FieldName="LOTE" ID="fieldLOTE" AreaIndex="10" Caption="Lote" CellFormat-FormatType="Custom"></dx:PivotGridField>
                            <dx:PivotGridField ID="fieldVALORMARCAPRODUCTO1" AreaIndex="11" Caption="Tipo Operacion" FieldName="VALOR_MARCA_PRODUCTO">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldFECHARA" AreaIndex="3" Caption="Fecha R.A." FieldName="FECHA_RA">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldENTIDADDEST" AreaIndex="13" Caption="Entidad Destino" FieldName="ENTIDAD_DEST">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldRUTAINTERNACION" AreaIndex="14" Caption="Ruta de Internacion" FieldName="RUTA_INTERNACION">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldEMPRESAPROVEEDORA" AreaIndex="15" Caption="Empresa Proveedora" FieldName="EMPRESA_PROVEEDORA">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldTKORIGEXTERNO" AreaIndex="16" Caption="Tanque Org. Externo" FieldName="TK_ORIG_EXTERNO">
                            </dx:PivotGridField>
                            <dx:PivotGridField ID="fieldNROLOTEVERIF" AreaIndex="17" Caption="Nro Lote/Verificación" FieldName="NRO_LOTE_VERIF">
                            </dx:PivotGridField>
                        </Fields>
                        <OptionsView ShowRowTotals="True" EnableContextMenuScrolling="True" EnableFilterControlPopupMenuScrolling="True" HorizontalScrollBarMode="Auto" />
                            <OptionsCustomization FilterPopupWindowMinWidth="200px" />
                            <OptionsPager RowsPerPage="250">
                                <PageSizeItemSettings ShowAllItem="True" Visible="True" AllItemText="Todos" Caption="Tamaño de Página" Items="10, 20, 50, 100, 200, 300, 500" Position="Left">
                                </PageSizeItemSettings>
                            </OptionsPager>

                            <OptionsData AutoExpandGroups="True" />
                            <OptionsFilter GroupFilterMode="List" />
                        <Styles CssPostfix="DevGlp">
                        </Styles>
                    </dx:ASPxPivotGrid>
                    </div>
                    <br/>
                    <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server" ScrollBars="Auto" Width="100%" Wrap="False">
                    </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
