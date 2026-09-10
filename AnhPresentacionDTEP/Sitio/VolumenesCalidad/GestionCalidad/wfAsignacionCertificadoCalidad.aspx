<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfAsignacionCertificadoCalidad.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad.wfAsignacionCertificadoCalidad" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/Comun/UControl/ucMensajeError.ascx" TagPrefix="uc1" TagName="ucMensajeError" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>ASIGNACIÓN DE CERTIFICADOS DE CALIDAD</h1>
    
    <hr class="Hydro_Linea_Verde" />
    
    <table width="100%">
        <tr>
            <td style="width: 47%">
                <fieldset>
                    <legend  style="border: none; float: none; text-align: left">Certificado Original</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblDesde" runat="server" Text="Fecha Desde"></asp:Label></td>
                            <td>
                                <dx:ASPxDateEdit ID="deFechaDesde" runat="server" ClientInstanceName="deFechaDesde" Width="100%">
                                    <TimeSectionProperties>
                                        <TimeEditProperties>
                                            <ClearButton Visibility="Auto"></ClearButton>
                                        </TimeEditProperties>
                                    </TimeSectionProperties>

                                    <ClearButton Visibility="Auto"></ClearButton>
                                    <ValidationSettings ValidationGroup="valida1">
                                        <RequiredField ErrorText="Requiere fecha de desde" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHasta" runat="server" Text="Fecha Hasta"></asp:Label></td>
                            <td>
                                <dx:ASPxDateEdit ID="deFechaHasta" runat="server" ClientInstanceName="deFechaHasta" OnClientClick="grdCertificadosOrigen.PerformCallback(); return false;" Width="100%">
                                    <TimeSectionProperties>
                                        <TimeEditProperties>
                                            <ClearButton Visibility="Auto"></ClearButton>
                                        </TimeEditProperties>
                                    </TimeSectionProperties>

                                    <ClearButton Visibility="Auto"></ClearButton>
                                    <ValidationSettings ValidationGroup="valida1">
                                        <RequiredField ErrorText="Requiere fecha hasta" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEntidadCertif" runat="server" Text="Entidad:" Visible="False"></asp:Label></td>
                            <td>
                                <dx:ASPxComboBox ID="cmbEntidadOrigen" runat="server" ClientInstanceName="cmbEntidadOrigen" Visible="False" Width="100%">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	cmbActividadOrigen.PerformCallback();
}" />
                                    <ClearButton Visibility="Auto"></ClearButton>
                                    <ValidationSettings ValidationGroup="valida1">
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblActividadCertif" runat="server" Text="Actividades:"></asp:Label></td>
                            <td>
                                <dx:ASPxComboBox ID="cmbActividadOrigen" runat="server" ClientInstanceName="cmbActividadOrigen" OnCallback="cmbActividadOrigen_Callback" Width="100%">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                        panelGrillas.PerformCallback();
}" />
                                    <ClearButton Visibility="Auto"></ClearButton>
                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="" ErrorTextPosition="Bottom" ValidationGroup="valida1">
                                        <RequiredField ErrorText="Debe seleccionar una actividad" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%--<dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="callbackButton" >
                                </dx:ASPxCallback>--%>
                                <dx:ASPxButton ID="btnBuscar" runat="server" Text="Buscar" AutoPostBack="False" ClientInstanceName="btnBuscar" Native="True" ValidationGroup="valida1">
                                    <ClientSideEvents Click="function(s, e) {
                                        panelGrillas.PerformCallback();
}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td style="width: 6%"></td>
            <td style="width: 47%">
                <fieldset>
                    <legend  style="border: none; float: none; text-align: left">Copia de Certificado</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <br/>
                                <br/>
                            </td>
                            <td>
                                <asp:Label ID="lblEntidadDestino" runat="server" Text="Entidad:" Visible="False"></asp:Label></td>
                            <td>
                                <dx:ASPxComboBox ID="cmbEntidadDestino" runat="server" ClientInstanceName="cmbEntidadDestino" Visible="False" Width="100%">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	cmbActividadDestino.PerformCallback();
}" />
                                    <ClearButton Visibility="Auto"></ClearButton>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Actividades:"></asp:Label></td>
                            <td>
                                <dx:ASPxComboBox ID="cmbActividadDestino" runat="server" ClientInstanceName="cmbActividadDestino" Width="100%" OnCallback="cmbActividadDestino_Callback">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	panelGrillas.PerformCallback();
}" />
                                    <ClearButton Visibility="Auto"></ClearButton>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <br />
    <dx:ASPxCallbackPanel ID="panelGrillas" runat="server" Width="100%" ClientInstanceName="panelGrillas" OnCallback="panelGrillas_Callback">
        <PanelCollection>
            <dx:PanelContent runat="server">
                <uc1:ucMensajeError runat="server" id="ucAlerta" />

                <table width="100%">
                    <tr>
                        <td style="border: 1px solid #598024" width="47%">
                            <dx:ASPxGridView ID="grdCertificadosOrigen" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdCertificadosOrigen" KeyFieldName="ID_REGISTRO_CALIDAD" Width="100%" OnCustomButtonInitialize="grdCertificadosOrigen_CustomButtonInitialize" OnCustomButtonCallback="grdCertificadosOrigen_CustomButtonCallback" Theme="Default">
                                <ClientSideEvents CustomButtonClick="function(s, e) {
	   
        e.processOnServer = confirm('¿Esta seguro de eliminar la copia del certificado?'); 
}" />
                                <SettingsPager PageSize="50">
                                    <PageSizeItemSettings Caption="Tamaño de páginas:" Items="50, 100, 200, 500, 1000" Position="Left" Visible="True">
                                    </PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                <SettingsSearchPanel Visible="True" />
                                <Columns>
                                    <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="CITE Certificado Calidad" VisibleIndex="3" FieldName="CITE">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Actividad" VisibleIndex="4" FieldName="TIPO_ACTIVIDAD">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID" VisibleIndex="1" FieldName="ID_REGISTRO_CALIDAD" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="IdTipoActividad" VisibleIndex="2" FieldName="ID_TIPO_ACTIVIDAD" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ID_PROPIETARIO_CERTIFICADO" Visible="False" VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="7" ButtonType="Image">
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="Eliminar">
                                                <Image Url="~/UI/img/iconos/eliminar.png" ToolTip="Eliminar Copia de Certificado de Calidad">
                                                </Image>
                                            </dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                    <Header BackColor="#598024" ForeColor="White">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>
                        </td>
                        <td align="center" width="6%">
                            <asp:Button ID="btnAgregar" runat="server" Text=">>" OnClick="btnAgregar_Click" Visible="True" />
                            <br />
                            <asp:Button ID="btnEliminar" runat="server" Text="<<" OnClick="btnEliminar_Click" Visible="True" />
                        </td>
                        <td style="border: 1px solid #598024" width="47%">
                            <dx:ASPxGridView ID="grdCertificadosTemporal" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdCertificadosTemporal" KeyFieldName="ID_REGISTRO_CALIDAD" Width="100%" Theme="Default">
                                <SettingsPager PageSize="50">
                                    <PageSizeItemSettings Caption="Tamaño de páginas:" Items="50, 100, 200, 500, 1000" Position="Left" Visible="True">
                                    </PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                <SettingsSearchPanel Visible="True" />
                                <Columns>
                                    <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="CITE Certificado Calidad" VisibleIndex="2" FieldName="CITE">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Actividad" VisibleIndex="3" FieldName="TIPO_ACTIVIDAD">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID" VisibleIndex="1" FieldName="ID_REGISTRO_CALIDAD" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ID_TIPO_ACTIVIDAD" Visible="False" VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ID_PROPIETARIO_CERTIFICADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                    <Header BackColor="#598024" ForeColor="White">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>
                            <asp:Button ID="btnGuardarAsignados" runat="server" Text="GUARDAR LISTA" OnClick="btnGuardarAsignados_Click" OnClientClick="return confirm('ESTA SEGURO DE REGISTRAR COPIAS DE LOS CERTIFICADOS SELECCIONADOS?');" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
