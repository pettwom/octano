<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="WfProde.aspx.cs" Inherits="AnhHydro.Sitio.VolumenesCalidad.GestionVolumenes.WfProde" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function idEntidad_SelectedIndexChanged(s, e) {
            grdProde.GetEditor("ID_CAMPO").PerformCallback(s.GetValue());
        }
        $(function () {
            $("#btnNuevo").on("click", function () {
                grdProde.AddNewRow();
            }); 
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>PRODE - Nominación de la Producción y Demanda</h1>
<div runat="server" id="divMensaje" style="display: none;"></div>
<hr class="Hydro_Linea_Verde" />
<input type="button" value="Nuevo PRODE" id="btnNuevo" />
<asp:LinkButton ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel"
        CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click"><span class="icon ui-icon-excel"></span><span class="label">Exportar a Excel</span></asp:LinkButton>
<dx:ASPxGridView ID="grdProde" ClientInstanceName="grdProde" runat="server"
        KeyFieldName="ID_PRODE" Width="100%" OnRowDeleting="grdProde_RowDeleting"
        OnRowInserting="grdProde_RowInserting" OnRowUpdating="grdProde_RowUpdating"
        AutoGenerateColumns="False" OnCellEditorInitialize="grdProde_CellEditorInitialize" 
        oncustomerrortext="grdProde_CustomErrorText" 
        oninitnewrow="grdProde_InitNewRow">
    <Columns>
        <%--<dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ButtonType="Image">
            <EditButton Visible="true" Text="Editar" Image-Url="~/UI/img/iconos/editar.png"><Image Url="~/UI/img/iconos/editar.png"></Image></EditButton>
            <DeleteButton Visible="true" Text="Eliminar" Image-Url="~/UI/img/iconos/eliminar.png"><Image Url="~/UI/img/iconos/eliminar.png"></Image></DeleteButton>
            <UpdateButton Visible="True" Text="Guardar este registro" Image-Url="~/UI/img/iconos/btn_inicio_aceptar.png"><Image Url="~/UI/img/iconos/btn_inicio_aceptar.png"></Image></UpdateButton>
            <CancelButton Visible="true" Text="Cancelar" Image-Url="~/UI/img/iconos/btn_inicio_cancelar.png"><Image Url="~/UI/img/iconos/btn_inicio_cancelar.png"></Image></CancelButton>
            <ClearFilterButton Visible="True" Text="Nueva búsqueda"></ClearFilterButton>
        </dx:GridViewCommandColumn>--%>
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" VisibleIndex="0">
        </dx:GridViewCommandColumn>
        <dx:GridViewDataComboBoxColumn Caption="Planta" FieldName="ID_ENTIDAD" 
            VisibleIndex="1">
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains" Width="270">
                <ClientSideEvents SelectedIndexChanged="idEntidad_SelectedIndexChanged" />

<ClearButton Visibility="Auto"></ClearButton>

                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="True" ErrorText="Seleccione una planta" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="Campo" FieldName="ID_CAMPO" 
            VisibleIndex="2" Visible="False">
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains">
<ClearButton Visibility="Auto"></ClearButton>

                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="False" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTimeEditColumn Caption="Mes" FieldName="FECHA_PRODE" 
            VisibleIndex="2">
            <PropertiesTimeEdit DisplayFormatString="MM/yyyy" EditFormat="Date" 
                EditFormatString="MM/yyyy">
<ClearButton Visibility="Auto"></ClearButton>

                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    EnableCustomValidation="True" ErrorDisplayMode="ImageWithText" 
                    ErrorText="El mes es requerido" ErrorTextPosition="Bottom" 
                    SetFocusOnError="True" ValidationGroup="ProdeValidationGroup">
                    <RequiredField IsRequired="True" ErrorText="El mes es requerido"/>
                </ValidationSettings>
            </PropertiesTimeEdit>
            <Settings HeaderFilterMode="CheckedList" />
        </dx:GridViewDataTimeEditColumn>
        <dx:GridViewDataComboBoxColumn Caption="Tipo de PRODE" 
            FieldName="ID_TIPO_REPORTE" VisibleIndex="3">
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains">
<ClearButton Visibility="Auto"></ClearButton>

                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="True"  
                        ErrorText="Seleccione un tipo de reporte" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="Producto" 
            FieldName="ID_PRODUCTO" VisibleIndex="4">
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains" Width="270">
<ClearButton Visibility="Auto"></ClearButton>

                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="True" ErrorText="Seleccione un producto" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataSpinEditColumn Caption="Valor del PRODE" 
            FieldName="VALOR_PRODE" VisibleIndex="5">
            <PropertiesSpinEdit DisplayFormatString="g" MaxLength="20" MaxValue="100000000" 
                MinValue="1">
<ClearButton Visibility="Auto"></ClearButton>

                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ErrorTextPosition="Bottom" SetFocusOnError="True"  ErrorDisplayMode="ImageWithText"
                    ValidationGroup="ProdeValidationGroup">
                    <RequiredField ErrorText="El valor del PRODE es requerido" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
            <Settings HeaderFilterMode="CheckedList" />
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataComboBoxColumn Caption="Unidad de Medida" 
            FieldName="ID_UNIDAD_MEDIDA" VisibleIndex="6">
            <Settings HeaderFilterMode="CheckedList"></Settings>
            <PropertiesComboBox IncrementalFilteringMode="Contains">
<ClearButton Visibility="Auto"></ClearButton>

                <ValidationSettings CausesValidation="True" Display="Dynamic" 
                    ValidationGroup="ProdeValidationGroup" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="True" 
                        ErrorText="Seleccione una unidad de medida" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
    </Columns>
    <SettingsEditing EditFormColumnCount="3"/>
    <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="True"/>
    <SettingsText ConfirmDelete="Eliminar este registro?" HeaderFilterSelectAll="(Seleccionar Todos)" HeaderFilterOkButton="Aceptar" HeaderFilterCancelButton="Cancelar" />
    <SettingsPager PageSize="20">
        <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="Todos"/>
    </SettingsPager>
    <Settings ShowFilterRow="false" ShowHeaderFilterButton="true" ShowHeaderFilterBlankItems="true"/>
    <SettingsCommandButton>
        <ClearFilterButton Text="Nueva Búsqueda">
        </ClearFilterButton>
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
        <Header CssClass="Hydro_Td_Titulo"></Header>
        <Row CssClass="Hydro_Tr_Contenido"></Row>
        <Table CssClass="Hydro_Table"></Table>
        <FocusedRow CssClass="Hydro_Tr_Seleccionado"></FocusedRow>
    </Styles>
</dx:ASPxGridView>

<dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdProde">
</dx:ASPxGridViewExporter>


</asp:Content>
