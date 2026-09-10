<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="WfCampos.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionVolumenes.WfCampos" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/UI/css/jquery-ui-1.10.3.custom.min.css" />
    <script type="text/javascript">
        $(function () {
            $(document).tooltip();
            $("#btnNuevo").on("click", function () {
                grdCampo.AddNewRow();
            });
        });
        /*grid*/
        function habilitarEdicion(s, e1) {
            if ($(".lnkEditar").length > 0) {
                $(".lnkEditar").off("click");
                $(".lnkEditar").on("click", function (e) {
                    e.preventDefault();
                    self.parent.presentacion.mostrarFancybox(this, 'Plantas a las que pertenece un campo', 800, 450);
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Campos de producci&oacute;n y/o exploraci&oacute;n</h1>
<hr class="Hydro_Linea_Verde" />
<input type="button" value="Nuevo campo" id="btnNuevo" />
<asp:LinkButton ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel"
        CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click"><span class="icon ui-icon-excel"></span><span class="label">Exportar a Excel</span></asp:LinkButton>
<dx:ASPxGridView ID="grdCampo" ClientInstanceName="grdCampo" runat="server"
        KeyFieldName="ID_CAMPO" Width="100%" OnRowDeleting="grdCampo_RowDeleting"
        OnRowInserting="grdCampo_RowInserting" OnRowUpdating="grdCampo_RowUpdating"
        AutoGenerateColumns="False" 
        OnCellEditorInitialize="grdCampo_CellEditorInitialize" 
        oncustomerrortext="grdCampo_CustomErrorText">
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

        <dx:GridViewDataTextColumn VisibleIndex="1" Settings-AllowAutoFilter="False" Width="80px">
            <Settings AllowAutoFilter="False" AllowSort="False"></Settings>
            <EditFormSettings Visible="False" />
            <DataItemTemplate>
                <a href="wfCamposEntidad.aspx?idCampo=<%# DataBinder.Eval(Container.DataItem, "ID_CAMPO") %>" class="lnkEditar Hydro_Link_Boton_Verde" title="Ver las plantas a las que pertenece el campo">Ver Plantas</a>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="NOMBRE_CAMPO" Caption="NOMBRE DEL CAMPO" VisibleIndex="2">
            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" AllowHeaderFilter="True"></Settings>
            <PropertiesTextEdit MaxLength="100" Width="500px">
                <ClientSideEvents Validation="function(s,e) { e.isValid = s.GetText().length > 4}" />
                <ValidationSettings ValidationGroup="ProductoValidationGroup" CausesValidation="True"
                    Display="Dynamic" SetFocusOnError="True" ErrorDisplayMode="ImageWithText" 
                    ErrorTextPosition="Bottom">
                    <RequiredField IsRequired="True" ErrorText="El nombre del campo es requerido"/>
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn FieldName="APP_FECHA_REGISTRO" Caption="FECHA DE REGISTRO" Width="150px" VisibleIndex="3">
            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
<TimeSectionProperties>
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
</TimeSectionProperties>

<ClearButton Visibility="Auto"></ClearButton>
            </PropertiesDateEdit>
            <Settings AllowAutoFilter="False"></Settings>
            <EditFormSettings Visible="False" />
        </dx:GridViewDataDateColumn>
    </Columns>
    <ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />
    <SettingsBehavior ConfirmDelete="true"  AllowFocusedRow="True"  />
    <SettingsText ConfirmDelete="Eliminar este registro?" HeaderFilterSelectAll="(Seleccionar Todos)" HeaderFilterOkButton="Aceptar" HeaderFilterCancelButton="Cancelar" />
    <SettingsPager PageSize="20">
        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
    </SettingsPager>
    <Settings ShowFilterRow="false" ShowFilterRowMenu="false"/>
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

<dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdCampo">
</dx:ASPxGridViewExporter>


</asp:Content>

