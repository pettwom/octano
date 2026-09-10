<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/ReportesMaster.Master" AutoEventWireup="true" CodeBehind="wfReporteCalidad.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes.wfReporteCalidad" %>
<%@ Register assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ReportToolbar ID="ReportToolbar1" runat="server" 
    ReportViewerID="ReportViewer1" ShowDefaultButtons="False">
    <Items>
        <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" ToolTip="Primero" />
        <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" ToolTip="Anterior" />
        <dx:ReportToolbarLabel ItemKind="PageLabel" Text="Página" />
        <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
        </dx:ReportToolbarComboBox>
        <dx:ReportToolbarLabel ItemKind="OfLabel" Text="de" />
        <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
        <dx:ReportToolbarButton ItemKind="NextPage" ToolTip="Siguiente" />
        <dx:ReportToolbarButton ItemKind="LastPage" ToolTip="Último" />
        <dx:ReportToolbarSeparator />
        <dx:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Exportar y grabarlo en disco" />
        <dx:ReportToolbarButton ItemKind="SaveToWindow" ToolTip="Exportar y mostrarlo en una nueva ventana" />
        <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
            <elements>
                <dx:ListElement Value="pdf" />
                <dx:ListElement Value="xls" />
                <dx:ListElement Value="xlsx" />
            </elements>
        </dx:ReportToolbarComboBox>
    </Items>
    <styles>
        <LabelStyle>
        <Margins MarginLeft='3px' MarginRight='3px' /> 
        </LabelStyle>
    </styles>
</dx:ReportToolbar>
<dx:ReportViewer ID="ReportViewer1" runat="server" Theme="iOS" >
</dx:ReportViewer>
</asp:Content>