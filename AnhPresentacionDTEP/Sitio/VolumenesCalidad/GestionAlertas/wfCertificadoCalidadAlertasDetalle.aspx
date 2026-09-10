<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/ReportesMaster.Master" AutoEventWireup="true" CodeBehind="wfCertificadoCalidadAlertasDetalle.aspx.cs" Inherits="AnhHydro.Sitio.VolumenesCalidad.GestionAlertas.wfCertificadoCalidadAlertasDetalle" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
        .grid-responsive-wrap {
            overflow-x: auto;
            width: 100%;
            -webkit-overflow-scrolling: touch;
            border: 1px solid #e9ecef;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 1px 4px rgba(0,0,0,0.06);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="export-toolbar">
    <asp:LinkButton ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel" CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click"><span class="icon ui-icon-excel"></span><span class="label">Exportar a Excel</span></asp:LinkButton>
    <asp:LinkButton ID="btnPdf" runat="server" title="Exportar datos de la grilla a Pdf" CssClass="Hydro_Boton_Blanco" OnClick="btnPdf_Click"><span class="icon ui-icon-pdf"></span><span class="label">Exportar a PDF</span></asp:LinkButton>
    <asp:LinkButton ID="btnWord" runat="server" title="Exportar datos de la grilla a Word" CssClass="Hydro_Boton_Blanco" OnClick="btnWord_Click"><span class="icon ui-icon-word"></span><span class="label">Exportar a Word</span></asp:LinkButton>
    </div>

    <div class="grid-responsive-wrap">
    <dx:ASPxGridView ID="grdCertificadoObservado" runat="server" KeyFieldName="CITE_DOCUMENTO" OnDetailRowExpandedChanged="grid_DetailRowExpandedChanged">
        <Settings ShowFilterRow="False" />
        <SettingsDetail ShowDetailRow="True" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="CITE" FieldName="CITE_DOCUMENTO" VisibleIndex="2" Width="25%"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Entidad" FieldName="ENTIDAD" VisibleIndex="2" Width="25%"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Producto" FieldName="PRODUCTO" VisibleIndex="2" Width="25%"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tanque" FieldName="COD_TANQUE" VisibleIndex="2" Width="25%"></dx:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior AutoExpandAllGroups="True" />

        <SettingsPager PageSize="1">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" />
        </SettingsPager>
        <SettingsDetail ShowDetailRow="True" ExportIndex="1" ExportMode="All" />
        <Paddings Padding="5px" />
        <Styles>
            <Header CssClass="Hydro_Td_Titulo"></Header>
            <Row CssClass="Hydro_Tr_Contenido"></Row>
            <Table CssClass="Hydro_Table"></Table>
        </Styles>
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="grdCertificadoCalidad" runat="server" AutoGenerateColumns="False" OnHtmlRowCreated="grdCertificadoCalidad_HtmlRowCreated" OnHtmlRowPrepared="grdCertificadoCalidad_HtmlRowPrepared1" KeyFieldName="DESCRIPCION">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Prueba / Ensayo" FieldName="DESCRIPCION" VisibleIndex="1" ShowInCustomizationForm="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Valor" FieldName="VALOR_ALFANUMERICO" VisibleIndex="2" ShowInCustomizationForm="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="M&eacute;todo ASTM" FieldName="ASTM" VisibleIndex="3" ShowInCustomizationForm="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="M&iacute;nimo" FieldName="PU_MIN" VisibleIndex="4" ShowInCustomizationForm="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="M&aacute;xmino" FieldName="PU_MAX" VisibleIndex="5" ShowInCustomizationForm="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Unidad" FieldName="UNIDAD_MEDIDA" VisibleIndex="6" ShowInCustomizationForm="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Observaciones" FieldName="OBSERVACION" VisibleIndex="7" ShowInCustomizationForm="True"></dx:GridViewDataTextColumn>
                        
                    </Columns>
                    <SettingsPager PageSize="30">
                        <PageSizeItemSettings Visible="False" ShowAllItem="False" />
                    </SettingsPager>
                    <Settings ShowFilterRow="true" />
                    <Paddings Padding="5px" />
                    <Styles>
                        <Header CssClass="Hydro_Td_Titulo"></Header>
                        <Row CssClass="Hydro_Tr_Contenido"></Row>
                        <Table CssClass="Hydro_Table"></Table>
                    </Styles>
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>

    </dx:ASPxGridView>
    </div>

    <dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdCertificadoObservado" ReportHeader="{\rtf1\ansi\ansicpg1252\deff0\deflang16394{\fonttbl{\f0\fnil\fcharset0 Times New Roman;}}
\viewkind4\uc1\pard\qc\f0\fs20\par
ANH Hola Mundo\par
}
"></dx:ASPxGridViewExporter>
</asp:Content>
