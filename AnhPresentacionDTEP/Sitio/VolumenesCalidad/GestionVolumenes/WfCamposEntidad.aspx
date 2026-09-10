<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/ReportesMaster.Master" AutoEventWireup="true" CodeBehind="WfCamposEntidad.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionVolumenes.WfCamposEntidad" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#btnNuevo").on("click", function () {
                grdCampoEntidad.AddNewRow();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<h1>Plantas del campo</h1>--%>
    <div runat="server" id="divMensaje" style="display: none;">
    </div>
    <hr class="Hydro_Linea_Verde" />
    <asp:HiddenField runat="server" ID="hdnIdCampo" />
    <input type="button" value="Asignar nueva planta al campo seleccionado" id="btnNuevo" />
    <asp:LinkButton ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel"
        CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click"><span class="icon ui-icon-excel"></span><span class="label">Exportar a Excel</span></asp:LinkButton>
    <dx:ASPxGridView ID="grdCampoEntidad" ClientInstanceName="grdCampoEntidad" runat="server"
        KeyFieldName="ID_CAMPOS_ENTIDAD" Width="100%" OnRowDeleting="grdCampoEntidad_RowDeleting"
        OnRowInserting="grdCampoEntidad_RowInserting" OnRowUpdating="grdCampoEntidad_RowUpdating"
        AutoGenerateColumns="False" OnCellEditorInitialize="grdCampoEntidad_CellEditorInitialize"
        OnCustomErrorText="grdCampoEntidad_CustomErrorText">
        <Columns>
            <%--<dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ButtonType="Image">
                <EditButton Visible="true" Text="Editar" Image-Url="~/UI/img/iconos/editar.png">
                    <Image Url="~/UI/img/iconos/editar.png"></Image>
                </EditButton>
                <DeleteButton Visible="true" Text="Eliminar" Image-Url="~/UI/img/iconos/eliminar.png">
                    <Image Url="~/UI/img/iconos/eliminar.png"></Image>
                </DeleteButton>
                <UpdateButton Visible="True" Text="Guardar este registro" Image-Url="~/UI/img/iconos/btn_inicio_aceptar.png">
                    <Image Url="~/UI/img/iconos/btn_inicio_aceptar.png"></Image>
                </UpdateButton>
                <CancelButton Visible="true" Text="Cancelar" Image-Url="~/UI/img/iconos/btn_inicio_cancelar.png">
                    <Image Url="~/UI/img/iconos/btn_inicio_cancelar.png"></Image>
                </CancelButton>
                <ClearFilterButton Visible="True" Text="Nueva búsqueda">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>--%>
            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataComboBoxColumn Caption="Planta" FieldName="ID_ENTIDAD" VisibleIndex="1">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" AllowHeaderFilter="True">
                </Settings>
                <PropertiesComboBox IncrementalFilteringMode="Contains">
<ClearButton Visibility="Auto"></ClearButton>

                    <ValidationSettings CausesValidation="True" Display="Dynamic" ValidationGroup="ProdeValidationGroup"
                        ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="Debe seleccionar una planta" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
        </Columns>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsText ConfirmDelete="Eliminar este registro?" HeaderFilterSelectAll="(Seleccionar Todos)"
            HeaderFilterOkButton="Aceptar" HeaderFilterCancelButton="Cancelar" />
        <SettingsPager PageSize="5">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" />
        </SettingsPager>
        <Settings ShowFilterRow="false" />
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
            <Header CssClass="Hydro_Td_Titulo">
            </Header>
            <Row CssClass="Hydro_Tr_Contenido">
            </Row>
            <Table CssClass="Hydro_Table">
            </Table>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdCampoEntidad">
    </dx:ASPxGridViewExporter>
</asp:Content>
