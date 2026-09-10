<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master"
    AutoEventWireup="true" CodeBehind="wfRegistroCertificadoCalidad.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad.wfRegistroCertificadoCalidad" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/Comun/UControl/ucCargadoMenuCertificadoCalidad.ascx" TagPrefix="uc1" TagName="ucCargadoMenuCertificadoCalidad" %>
<%@ Register Src="~/Comun/UControl/ucRegistroCertificadoCalidadCarburantes.ascx" TagPrefix="uc1" TagName="ucRegistroCertificadoCalidadCarburantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* ─── Títulos ─── */
        .section-title {
            font-weight: 600;
            letter-spacing: 0.3px;
            margin-bottom: 4px;
        }
        .subtitle {
            font-weight: 500;
            letter-spacing: 0.2px;
            margin-top: 0;
            margin-bottom: 16px;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .subtitle img {
            vertical-align: middle;
        }

        /* ─── Splitter card ─── */
        .splitter-wrapper {
            border: 1px solid #e9ecef;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 1px 4px rgba(0,0,0,0.06);
            overflow: hidden;
        }
        .splitter-wrapper .dxpnlControl_Office2003Olive {
            border: none !important;
        }

        /* ─── TreeView más legible ─── */
        .dxdtl_Office2003Olive a {
            padding: 5px 10px !important;
            border-radius: 3px !important;
            transition: background 0.15s ease !important;
        }
        .dxdtl_Office2003Olive a:hover {
            background-color: #e8f5e9 !important;
        }
        .dxdtl_Office2003Olive a.dxdtlSelectedNode_Office2003Olive {
            background-color: #2D7E3E !important;
            color: #fff !important;
            border-radius: 3px !important;
        }

        /* ─── Content pane ─── */
        .splitter-wrapper .dxpnlControl_Office2003Olive td {
            padding: 6px;
        }

        /* ─── Responsive ─── */
        @media (max-width: 768px) {
            .section-title {
                font-size: 18px;
            }
            .subtitle {
                font-size: 14px;
                flex-wrap: wrap;
            }
            .subtitle img {
                width: 50px;
            }
            /* Pane izquierdo más angosto en tablets */
            .splitter-wrapper .dxpnlControl_Office2003Olive td:first-child {
                max-width: 180px !important;
                min-width: 120px !important;
            }
        }

        @media (max-width: 480px) {
            .section-title {
                font-size: 16px;
            }
            .subtitle {
                font-size: 13px;
            }
            .subtitle img {
                width: 36px;
            }
            /* Pane izquierdo ocupa ancho completo, apilado */
            .splitter-wrapper .dxpnlControl_Office2003Olive table.dxpnlControl_Office2003Olive {
                table-layout: fixed !important;
            }
            .splitter-wrapper .dxpnlControl_Office2003Olive table.dxpnlControl_Office2003Olive tbody,
            .splitter-wrapper .dxpnlControl_Office2003Olive table.dxpnlControl_Office2003Olive tbody tr {
                display: block !important;
            }
            .splitter-wrapper .dxpnlControl_Office2003Olive table.dxpnlControl_Office2003Olive tbody tr td {
                display: block !important;
                width: 100% !important;
                min-width: 100% !important;
                max-width: 100% !important;
            }
            .dxpnlCtrlSep_Office2003Olive {
                display: none !important;
            }
            .dxpnlCtrlSep_Office2003Olive + td {
                display: none !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="section-title" style="color: #2D7E3E">Registro de Certificados de Control de Calidad</h1>
    <h3 class="subtitle" style="color: #2D7E3E">
        <img src="../../../UI/img/iconos/click.gif" width="80px" />
        Seleccione la prueba de Calidad
    </h3>
    <div class="splitter-wrapper">
    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server">
        <Panes>
            <dx:SplitterPane AllowResize="True" AutoHeight="True" MaxSize="300px" ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                        <dx:ASPxTreeView ID="arbolMenu" runat="server" AllowSelectNode="True" AutoPostBack="True" Font-Size="XX-Small" Theme="Office2003Olive" OnNodeClick="arbolMenu_NodeClick" ForeColor="Black">
                        </dx:ASPxTreeView>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                        <dx:ASPxCallbackPanel ID="panelCertificado" runat="server" Width="100%" ClientInstanceName="panelCertificado">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <uc1:ucRegistroCertificadoCalidadCarburantes Visible="false" ID="CtrRegistroCertificadoCalidadCarburantes" runat="server" />
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
    </div>
</asp:Content>


