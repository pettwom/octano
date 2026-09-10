<%@ Page Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="WfGestionReplicaUsuario.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAccesos.WfGestionReplicaUsuario" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxRoundPanel ID="pnlAccionCopiar" ClientInstanceName="pnlAccionCopiar" runat="server" HeaderText="Replicar Permisos" Visible="true" Width="100%">                
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
                                                        <td style="width:40%">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="2" style="padding-bottom: 5px">
                                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="Usuario Nuevo:" Width="100%">
                                                                            <BorderBottom BorderStyle="Solid" />
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 70%">
                                                                        <dx:ASPxTextBox ID="txtUsuarioNuevoReplica" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();" Width="100%" Height="26px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 30%">
                                                                        <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Buscar" Theme="DevEx" Width="100%" Height="20px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                    pnlUsuarioNuevoReplica.PerformCallback();
                                    }" />
                                                                            <Image IconID="zoom_zoom_16x16">
                                                                            </Image>
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="padding-top: 5px">
                                                                        <dx:ASPxCallbackPanel ID="pnlUsuarioNuevoReplica" runat="server" Width="100%" ClientInstanceName="pnlUsuarioNuevoReplica" OnCallback="pnlUsuarioNuevoReplica_Callback">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent runat="server">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxGridView ID="grvListaUsuarioNuevoReplica" runat="server" AutoGenerateColumns="False" ClientInstanceName="grvListaUsuarioNuevoReplica" KeyFieldName="ID_USUARIO" Theme="Metropolis" Width="100%">
                                                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
switch(e.buttonID) {
case&nbsp; 'btnUsuarioNuevo':
pnlUsuarioOctCalNuevo.PerformCallback();
pnlIdUsuarioOctCalNuevo.PerformCallback();                                                                         
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
                                                                                                                <dx:GridViewCommandColumnCustomButton ID="btnUsuarioNuevo" Text=" ">
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
                                                        <td style="width:20%"></td>
                                                        <td style="width:40%">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="2" style="padding-bottom: 5px">
                                                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="Usuario de Replica:" Width="100%">
                                                                            <BorderBottom BorderStyle="Solid" />
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 70%">
                                                                        <dx:ASPxTextBox ID="txtUsuarioReplica" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();" Width="100%" Height="26px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 30%">
                                                                        <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Buscar" Theme="DevEx" Width="100%" Height="20px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                    pnlUsuarioReplica.PerformCallback();
                                    }" />
                                                                            <Image IconID="zoom_zoom_16x16">
                                                                            </Image>
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="padding-top: 5px">
                                                                        <dx:ASPxCallbackPanel ID="pnlUsuarioReplica" runat="server" Width="100%" ClientInstanceName="pnlUsuarioReplica" OnCallback="pnlUsuarioReplica_Callback">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent runat="server">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td> 
                                                                                                <dx:ASPxGridView ID="grvListaUsuariosReplica" runat="server" AutoGenerateColumns="False" ClientInstanceName="grvListaUsuariosReplica" KeyFieldName="ID_USUARIO" Theme="Metropolis" Width="100%">                                                                                                                
                                                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
switch(e.buttonID) {
case&nbsp; 'btnUsuarioCopia':
pnlUsuarioReplicaOctCal.PerformCallback();
pnlIdUsuarioReplicaOctCal.PerformCallback();                                                                         
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
                                                                                                                <dx:GridViewCommandColumnCustomButton ID="btnUsuarioCopia" Text=" ">
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
                                                        <td style="width:40%">&nbsp;</td>
                                                        <td style="width:20%">&nbsp;</td>
                                                        <td style="width:40%">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:40%">
                                                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="Nombre de Usuario Nuevo:" Width="100%">
                                                                            <BorderBottom BorderStyle="Solid" />
                                                                        </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width:20%"></td>
                                                        <td style="width:40%">
                                                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="Nombre de Usuario de Replica:" Width="100%">
                                                                            <BorderBottom BorderStyle="Solid" />
                                                                        </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:40%">
                                                            <dx:ASPxCallbackPanel ID="pnlUsuarioOctCalNuevo" runat="server" Width="100%" ClientInstanceName="pnlUsuarioOctCalNuevo" OnCallback="pnlUsuarioOctCalNuevo_Callback">
                                                                <PanelCollection>
                                                                    <dx:PanelContent runat="server">
                                                                        <dx:ASPxTextBox runat="server" Width="80%" ID="txtUsuarioOctCalNuevo" ClientInstanceName="txtUsuarioOctCalNuevo"></dx:ASPxTextBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </td>
                                                        <td style="width:20%; align-content:center; text-align:center;">
                                                            <dx:ASPxLabel runat="server" Text="---->" Font-Size="X-Large" Font-Bold="true"></dx:ASPxLabel>
                                                        </td>
                                                        <td style="width:40%">
                                                            <dx:ASPxCallbackPanel ID="pnlUsuarioReplicaOctCal" runat="server" Width="100%" ClientInstanceName="pnlUsuarioReplicaOctCal" OnCallback="pnlUsuarioReplicaOctCal_Callback">
                                                                <PanelCollection>
                                                                    <dx:PanelContent runat="server">
                                                                        <dx:ASPxTextBox runat="server" Width="80%" ID="txtUsuarioOctCalReplica" ClientInstanceName="txtUsuarioOctCalReplica"></dx:ASPxTextBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:40%">  
                                                            <dx:ASPxCallbackPanel ID="pnlIdUsuarioOctCalNuevo" runat="server" Width="100%" ClientInstanceName="pnlIdUsuarioOctCalNuevo" OnCallback="pnlIdUsuarioOctCalNuevo_Callback">
                                                                <PanelCollection>
                                                                    <dx:PanelContent runat="server">
                                                                        <dx:ASPxTextBox runat="server" ID="txtIdUsuarioOctCalNuevo" ClientInstanceName="txtIdUsuarioOctCalNuevo"></dx:ASPxTextBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel> 
                                                        </td>
                                                        <td style="width:20%">
                                                        </td>
                                                        <td style="width:40%">
                                                            <dx:ASPxCallbackPanel ID="pnlIdUsuarioReplicaOctCal" runat="server" Width="100%" ClientInstanceName="pnlIdUsuarioReplicaOctCal" OnCallback="pnlIdUsuarioReplicaOctCal_Callback">
                                                                <PanelCollection>
                                                                    <dx:PanelContent runat="server">
                                                                        <dx:ASPxTextBox runat="server" ID="txtIdUsuarioOctCalReplica" ClientInstanceName="txtIdUsuarioOctCalReplica"></dx:ASPxTextBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:40%">&nbsp;</td>
                                                        <td style="width:20%">&nbsp;</td>
                                                        <td style="width:40%">&nbsp;</td>
                                                    </tr>                                                                
                                                    <tr>
                                                        <td style="width:40%">
                                                            <table style="width:100%">                                                                
                                                                <tr>
                                                                    <td style="width:100%">
                                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="Tipo de Actividad:" Width="100%">
                                                                            <BorderBottom BorderStyle="Solid" />
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:100%">
                                                                        <dx:ASPxComboBox ID="cmbTipoActividad" runat="server" ClientInstanceName="cmbTipoActividad" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID" Width="50%">
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>                                                                                
                                                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                                                            </ReadOnlyStyle>
                                                                            <FocusedStyle BackColor="#FFFFCC">
                                                                            </FocusedStyle>
                                                                            <DisabledStyle BackColor="#DEE8DB">
                                                                            </DisabledStyle>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>                                                                                                                               
                                                                <tr>
                                                                    <td style="width:100%">
                                                                        <dx:ASPxButton ID="btnActualizarPermisoOctCalidad" ClientInstanceName="btnActualizarPermisoOctCalidad" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="ActualizarRegistros" Theme="DevEx" Width="30%" OnClick="btnActualizarPermisoOctCalidad_Click">                                                                           
                                                                            <Image IconID="businessobjects_bocontact2_16x16">
                                                                            </Image>
                                                                        </dx:ASPxButton>
                                                                    </td>                                                                    
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width:20%"></td>
                                                        <td style="width:40%"></td>
                                                    </tr>
                                                </table>
                                                            
                                            </dx:PanelContent>
                                        </PanelCollection>
                                        <Border BorderStyle="Ridge" BorderWidth="1px" />
                                    </dx:ASPxRoundPanel>
    </asp:Content>
