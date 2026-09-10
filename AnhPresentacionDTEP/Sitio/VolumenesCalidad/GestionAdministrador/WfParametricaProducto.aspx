<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="WfParametricaProducto.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador.WfParametricaProducto" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="">
        
        var lastEntidad;

        //function idEntidad_SelectedIndexChanged(s, e) {
        //    grdProde.GetEditor("ID_CAMPO").PerformCallback(s.GetValue());
        //}
        $(function () {
            $("#btnNuevo").on("click", function () {
                grdProde.AddNewRow();
            });
        });        
    
        //PopUps Equipos ANH
        //function OnRemoteAnhlick(element) {
        //    ppRemotes.ShowAtElement(element);
        //}


        function OnEntidadChanged(cmbEntidad) {
            


            if (grdProde.GetEditor("ID_TABLA_ESPEC").InCallback()) {
                lastEntidad = cmbEntidad.GetValue().toString();
                
            }
            else {
                grdProde.GetEditor("ID_TABLA_ESPEC").PerformCallback(cmbEntidad.GetValue().toString())
            };
        }
        function OnEndTablaEspCallback(s, e) {
            if (lastEntidad) {
                grdProde.GetEditor("ID_ENTIDAD").PerformCallback(lastEntidad);
                lastEntidad = null;
            }
        }        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>PRODUCTOS COMERCIALES</h2>
<div runat="server" id="divMensaje" style="display: none;"></div>
<hr class="Hydro_Linea_Verde" />

<asp:LinkButton ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel" CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-excel"></span><span class="label" style="font-family: Calibri; font-size: small">Exportar a Excel</span></asp:LinkButton>
<dx:ASPxGridView ID="grdProde" ClientInstanceName="grdProde" runat="server"
        KeyFieldName="ID_NOMBRE_PRODUCTO" Width="100%" OnRowDeleting="grdProde_RowDeleting"
        OnRowInserting="grdProde_RowInserting" OnRowUpdating="grdProde_RowUpdating"
        AutoGenerateColumns="False" OnCellEditorInitialize="grdProde_CellEditorInitialize" 
        oncustomerrortext="grdProde_CustomErrorText" OnBeforePerformDataSelect="grdProde_OnBeforePerformDataSelect">
    <Columns>
        

        <%--<dx:GridViewDataComboBoxColumn Caption="ID_ENTIDAD" FieldName="ID_ENTIDAD" 
            VisibleIndex="1" Visible="False"> 
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains" Width="270">
                <ClientSideEvents SelectedIndexChanged="idEntidad_SelectedIndexChanged" />
                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="True" ErrorText="Seleccione una planta" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        
        <dx:GridViewDataComboBoxColumn Caption="ENTIDAD" FieldName="ENTIDAD" 
            VisibleIndex="1">
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains" Width="270">
                <ClientSideEvents SelectedIndexChanged="idEntidad_SelectedIndexChanged" />
                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="True" ErrorText="Seleccione una planta" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>

        <dx:GridViewDataComboBoxColumn Caption="ID_TABLA_ESPEC" FieldName="ID_TABLA_ESPEC" 
            VisibleIndex="2" Visible="False">
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains">
                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="False" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        
        <dx:GridViewDataComboBoxColumn Caption="TABLA ESPECIFICA" FieldName="TABLA_ESPEC" 
            VisibleIndex="2" >
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains">
                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="False" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>--%>
        
        
        
        <%--<dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ButtonType="Image">
            <EditButton Visible="true" Text="Editar" Image-Url="~/UI/img/iconos/editar.png"><Image Url="~/UI/img/iconos/editar.png"></Image></EditButton>
            <DeleteButton Visible="true" Text="Eliminar" Image-Url="~/UI/img/iconos/eliminar.png"><Image Url="~/UI/img/iconos/eliminar.png"></Image></DeleteButton>
            <UpdateButton Visible="True" Text="Guardar este registro" Image-Url="~/UI/img/iconos/btn_inicio_aceptar.png"><Image Url="~/UI/img/iconos/btn_inicio_aceptar.png"></Image></UpdateButton>
            <CancelButton Visible="true" Text="Cancelar" Image-Url="~/UI/img/iconos/btn_inicio_cancelar.png"><Image Url="~/UI/img/iconos/btn_inicio_cancelar.png"></Image></CancelButton>
            <ClearFilterButton Visible="True" Text="Nueva búsqueda"></ClearFilterButton>
        </dx:GridViewCommandColumn>--%>

        
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
        </dx:GridViewCommandColumn>
        
        
        <dx:GridViewDataComboBoxColumn Caption="ENTIDAD" FieldName="ID_ENTIDAD" VisibleIndex="1">
            <PropertiesComboBox DataSourceID="odsEntidad" TextField="ENTIDAD" ValueField="ID_ENTIDAD" EnableSynchronization="False" IncrementalFilteringMode="StartsWith">
            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnEntidadChanged(s); }"></ClientSideEvents>

<ClearButton Visibility="Auto"></ClearButton>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="TABLA ESPECIFICA" FieldName="ID_TABLA_ESPEC" VisibleIndex="2">
            <PropertiesComboBox DataSourceID="odsTablaEsp" TextField="TABLA_ESPEC" ValueField="ID_TABLA_ESPEC" EnableSynchronization="False" IncrementalFilteringMode="Contains" >
                <ClientSideEvents EndCallback="OnEndTablaEspCallback"/>
                <%--<ValidationSettings CausesValidation="True" ErrorDisplayMode="ImageWithText">
                    <RequiredField IsRequired="True" />
                </ValidationSettings>--%>

<ClearButton Visibility="Auto"></ClearButton>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>

        <%--<dx:GridViewDataComboBoxColumn Caption="ID_NOMBRE_PRODUCTO" FieldName="ID_NOMBRE_PRODUCTO" 
            VisibleIndex="2" >
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains">
                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="False" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>--%>

        <dx:GridViewDataTextColumn FieldName="NOMBRE_PRODUCTO" Caption="NOMBRE PRODUCTO COMERCIAL" VisibleIndex="3">
                <%--<Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" AllowHeaderFilter="True"></Settings>
                <PropertiesTextEdit MaxLength="100" Width="100%">
                    <ClientSideEvents Validation="function(s,e) { e.isValid = s.GetText().length > 2}" />
                    <ValidationSettings ValidationGroup="ProductoValidationGroup" CausesValidation="True"
                        Display="Dynamic" SetFocusOnError="True" ErrorDisplayMode="ImageWithText"
                        ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="El valor de la parametrica es requerido" />
                    </ValidationSettings>
                </PropertiesTextEdit>--%>
            </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="CODIGO_PRODUCTO" Caption="CODIGO PRODUCTO COMERCIAL" VisibleIndex="4">
                <%--<Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" AllowHeaderFilter="True"></Settings>
                <PropertiesTextEdit MaxLength="100" Width="100%">
                    <ClientSideEvents Validation="function(s,e) { e.isValid = s.GetText().length > 2}" />
                    <ValidationSettings ValidationGroup="ProductoValidationGroup" CausesValidation="True"
                        Display="Dynamic" SetFocusOnError="True" ErrorDisplayMode="ImageWithText"
                        ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="El valor de la parametrica es requerido" />
                    </ValidationSettings>
                </PropertiesTextEdit>--%>
            </dx:GridViewDataTextColumn>
        
        
    </Columns>
    <SettingsEditing EditFormColumnCount="3"/>
    <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="True"/>
    <SettingsText ConfirmDelete="Eliminar este registro?" HeaderFilterSelectAll="(Seleccionar Todos)" HeaderFilterOkButton="Aceptar" HeaderFilterCancelButton="Cancelar" />
    <SettingsPager PageSize="100">
        <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="Todos"/>
    </SettingsPager>
    <Settings ShowFilterRow="false" ShowHeaderFilterButton="true" ShowHeaderFilterBlankItems="true"/>
    <SettingsCommandButton>
        <ClearFilterButton Text="Nueva Búsqueda">
        </ClearFilterButton>
        <NewButton ButtonType="Image">
                <Image AlternateText="Nuevo Producto Comercial" Url="~/UI/img/iconos/Add-icon.png">
                </Image>
        </NewButton>
        <UpdateButton ButtonType="Image">
            <Image AlternateText="Guardar" Url="~/UI/img/iconos/btn_inicio_aceptar.png">
            </Image>
        </UpdateButton>
        <CancelButton ButtonType="Image">
            <Image AlternateText="Cancelar" Url="~/UI/img/iconos/btn_inicio_cancelar.png">
            </Image>
        </CancelButton>
        <EditButton ButtonType="Image">
            <Image AlternateText="Editar" Url="~/UI/img/iconos/editar.png">
            </Image>
        </EditButton>
        <DeleteButton ButtonType="Image">
            <Image AlternateText="Borrar" Url="~/UI/img/iconos/eliminar.png">
            </Image>
        </DeleteButton>
    </SettingsCommandButton>
    <SettingsPopup>
        <HeaderFilter Height="300px" Width="300" />
    </SettingsPopup>
    <Paddings Padding="5px" />
    <Styles>
        <GroupPanel BackColor="White"  ForeColor="#000000"></GroupPanel>
            <Header CssClass="Hydro_Td_Titulo" BackColor="#2D7E3E" ForeColor="White"></Header>
            <FilterRow BackColor="#2D7E3E" ForeColor="White"></FilterRow>
            <Row CssClass="Hydro_Tr_Contenido"></Row>
            <Table CssClass="Hydro_Content_Table" BackColor="White"></Table>
            <AlternatingRow BackColor="#EEF3E4">
            </AlternatingRow>
            <PagerBottomPanel BackColor="White"  ForeColor="Black"></PagerBottomPanel>
            <FocusedRow ForeColor="Black" BackColor="#e5e5e5"></FocusedRow>
    </Styles>
</dx:ASPxGridView>

<dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdProde">
</dx:ASPxGridViewExporter>
    
    <asp:ObjectDataSource ID="odsEntidad" runat="server" SelectMethod="ListaParametrica" TypeName="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador.WfParametricaProducto" >
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="decIdEntidad" SessionField="EntidadId" Type="Decimal" />
            <asp:Parameter DefaultValue="ENTIDAD" Name="strDominioParametro" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="odsTablaEsp" runat="server" SelectMethod="ListaParametrica" TypeName="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador.WfParametricaProducto">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="decIdEntidad" SessionField="EntidadId" Type="Decimal" />
            <asp:Parameter DefaultValue="TABLA" Name="strDominioParametro" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
</asp:Content>

