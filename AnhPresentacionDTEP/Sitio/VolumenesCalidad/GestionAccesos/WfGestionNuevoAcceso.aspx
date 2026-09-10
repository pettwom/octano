<%@ Page Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="WfGestionNuevoAcceso.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAccesos.WfGestionNuevoAcceso" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxRoundPanel ID="pnlAccionModificar" ClientInstanceName="pnlAccionModificar" runat="server" HeaderText="Actualizar Permisos" Visible="true" Width="100%">
                                <PanelCollection>
                                    <dx:PanelContent runat="server">
                                        <table style="width:100%">
                                            <tr>
                                                        <td colspan="7" style="text-align: center;">
                                                            <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                                <PanelCollection>
                                                                    <dx:PanelContent ID="PanelContent1" runat="server">
                                                                        <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666">
                                                                        </dx:ASPxLabel>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                                <Border BorderStyle="Ridge" BorderWidth="1px" />
                                                            </dx:ASPxRoundPanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:100%">                                                                    
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="2" style="padding-bottom: 5px">
                                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="Dato Usuario:" Width="100%">
                                                                            <BorderBottom BorderStyle="Solid" />
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 70%">
                                                                        <dx:ASPxTextBox ID="txtUsuarioModificarPermiso" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();" Width="100%" Height="26px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 30%">
                                                                        <dx:ASPxButton ID="ASPxButton5" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Buscar" Theme="DevEx" Width="100%" Height="20px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                    pnlUsuarioModificarPermiso.PerformCallback();
                                    }" />
                                                                            <Image IconID="zoom_zoom_16x16">
                                                                            </Image>
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="padding-top: 5px">
                                                                        <dx:ASPxCallbackPanel ID="pnlUsuarioModificarPermiso" runat="server" Width="100%" ClientInstanceName="pnlUsuarioModificarPermiso" OnCallback="pnlUsuarioModificarPermiso_Callback">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent runat="server">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxGridView ID="grvListaUsrModificarPermiso" runat="server" AutoGenerateColumns="False" ClientInstanceName="grvListaUsrModificarPermiso" KeyFieldName="ID_USUARIO" Theme="Metropolis" Width="100%">
                                                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
switch(e.buttonID) {
case&nbsp; 'btnListadoPermiso':
pnlListaPermisoCalidad.PerformCallback();
break;
} 


}" />
                                                                                                    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="150" />
                                                                                                    <SettingsBehavior AllowFocusedRow="True" ProcessSelectionChangedOnServer="True" />
                                                                                                    <Columns>
                                                                                                        <dx:GridViewDataTextColumn Caption="ID_USUARIO" FieldName="ID_USUARIO" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="NOMBRE_USUARIO" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="NOMBRE_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                                                        </dx:GridViewDataTextColumn>                                                                                                                    
                                                                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="50">
                                                                                                            <CustomButtons>
                                                                                                                <dx:GridViewCommandColumnCustomButton ID="btnListadoPermiso" Text=" ">
                                                                                                                    <Image IconID="scheduling_timelineview_16x16office2013" ToolTip="Registrar Saldo Fisico (Medición)">
                                                                                                                    </Image>
                                                                                                                </dx:GridViewCommandColumnCustomButton>                                                  
                                                                                                            </CustomButtons>
                                                                                                        </dx:GridViewCommandColumn>
                                                                                                    </Columns>
                                                                                                </dx:ASPxGridView>
                                                                                            </td>
                                                                                        </tr>                                                                                                    
                                                                                    </table>
                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dx:ASPxCallbackPanel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width:100%">                                                                    
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="2" style="padding-bottom: 5px">
                                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="Permisos Usuario:" Width="100%">
                                                                            <BorderBottom BorderStyle="Solid" />
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>                                                                            
                                                                <tr>
                                                                    <td colspan="2" style="padding-top: 5px">
                                                                        <dx:ASPxCallbackPanel ID="pnlListaPermisoCalidad" runat="server" Width="100%" ClientInstanceName="pnlListaPermisoCalidad" OnCallback="pnlListaPermisoCalidad_Callback" >
                                                                            <PanelCollection>
                                                                                <dx:PanelContent runat="server">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxGridView ID="grvListaPermisos" runat="server" AutoGenerateColumns="False" ClientInstanceName="grvListaPermisos" KeyFieldName="ID_GESTION_OCTANO" Theme="Metropolis" Width="100%" OnCustomButtonCallback="grvListaPermisos_CustomButtonCallback">                                                                                                                
                                                                                                    <ClientSideEvents CustomButtonClick="function (s, e) {
                                                                    switch(e.buttonID) {
                                                                        case&nbsp; 'btnEliminar':
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('ATENCION: Si elimina el registro no podrá revertir el proceso. ¿Desea eliminar el registro?'); 
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
                                                                    } 
                                                                    }" BeginCallback="function(s, e) {
	                                                                    LoadingPanel.Show();
                                                                    }" EndCallback="function(s, e) {
	                                                                    LoadingPanel.Hide();
                                                                    }" />
                                                                                                    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="150" />
                                                                                                    <SettingsBehavior AllowFocusedRow="True" ProcessSelectionChangedOnServer="True" />
                                                                                                    <Columns>
                                                                                                        <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" ToolTip="Funciones" VisibleIndex="50">
                                                                                                            <CustomButtons>                                                                                                                
                                                                                                                <dx:GridViewCommandColumnCustomButton ID="btnEliminar" Text=" ">
                                                                                                                    <Image IconID="reports_deletegroupfooter_16x16office2013" ToolTip="Eliminar">
                                                                                                                    </Image>
                                                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                                                            </CustomButtons>
                                                                                                            <CellStyle ForeColor="#006600">
                                                                                                            </CellStyle>
                                                                                                        </dx:GridViewCommandColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ID_GESTION_OCTANO" FieldName="ID_GESTION_OCTANO" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ID_USUARIO_ANH" FieldName="ID_USUARIO_ANH" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="NOMBRE_COMPLETO" FieldName="NOMBRE_COMPLETO" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ACTIVDIRECCIONIDAD" FieldName="DIRECCION" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="FECHA_FIN" FieldName="FECHA_FIN" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="APLICACION" FieldName="APLICACION" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="ACTIVIDAD" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ID_ENTIDAD" FieldName="ID_ENTIDAD" ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                                        </dx:GridViewDataTextColumn>      
                                                                                                        <dx:GridViewDataTextColumn Caption="ENTIDAD" FieldName="ENTIDAD" ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                                        </dx:GridViewDataTextColumn>                                                                                                                    
                                                                                                    </Columns>
                                                                                                </dx:ASPxGridView>
                                                                                            </td>
                                                                                        </tr>                                                                                                    
                                                                                    </table>
                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dx:ASPxCallbackPanel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                            </table>
                                    </dx:PanelContent>
                                </PanelCollection>
                                <Border BorderStyle="Ridge" BorderWidth="1px" />
                            </dx:ASPxRoundPanel>
    </asp:Content>
