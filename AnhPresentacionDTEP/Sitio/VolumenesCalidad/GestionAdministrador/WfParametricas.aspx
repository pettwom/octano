<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="WfParametricas.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador.WfParametricas" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/UI/css/jquery-ui-1.10.3.custom.min.css" />
    <script type="text/javascript">
        $(function () {
            $(document).tooltip();
            $("#btnNuevo").on("click", function () {
                grdParametrica.AddNewRow();
            }); 
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="color: #2D7E3E">MARCAS DE LUBRICANTES</h2>
    <hr class="Hydro_Linea_Verde" />
    <table width="100%">
        <tr style="width: 100%">
            <td>
                <asp:LinkButton Visible="False" ID="btnExcel" runat="server" title="Exportar datos de la grilla a Excel"
                    CssClass="Hydro_Boton_Blanco" OnClick="btnExcel_Click" BackColor="#2D7E3E" BorderWidth="1px"><span class="icon ui-icon-excel"></span><span class="label">Exportar a Excel</span></asp:LinkButton>
            </td>
        </tr>
    </table>

    <dx:ASPxGridView ID="grdParametrica" ClientInstanceName="grdParametrica" runat="server"
        KeyFieldName="ID_PARAMETRICA" Width="100%" OnRowDeleting="grdParametrica_RowDeleting"
        OnRowInserting="grdParametrica_RowInserting"
        AutoGenerateColumns="False"
        OnCellEditorInitialize="grdParametrica_CellEditorInitialize"
        OnCustomErrorText="grdParametrica_CustomErrorText" OnRowUpdating="grdParametrica_RowUpdating">
        <Columns>


            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn FieldName="ID_PARAMETRICA" Caption="ID_PARAMETRICA" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="CODIGO" Caption="CODIGO" VisibleIndex="2">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" AllowHeaderFilter="True"></Settings>
                <PropertiesTextEdit MaxLength="100" Width="500px">
                    <ClientSideEvents Validation="function(s,e) { e.isValid = s.GetText().length > 2}" />
                    <ValidationSettings ValidationGroup="ProductoValidationGroup" CausesValidation="True"
                        Display="Dynamic" SetFocusOnError="True" ErrorDisplayMode="ImageWithText"
                        ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="El codigo de la parametrica es requerido" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="VALOR" Caption="VALOR" VisibleIndex="2">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" AllowHeaderFilter="True"></Settings>
                <PropertiesTextEdit MaxLength="100" Width="500px">
                    <ClientSideEvents Validation="function(s,e) { e.isValid = s.GetText().length > 2}" />
                    <ValidationSettings ValidationGroup="ProductoValidationGroup" CausesValidation="True"
                        Display="Dynamic" SetFocusOnError="True" ErrorDisplayMode="ImageWithText"
                        ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="El valor de la parametrica es requerido" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="DESCRIPCION" Caption="DESCRIPCION" VisibleIndex="3">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" AllowHeaderFilter="True"></Settings>
                <PropertiesTextEdit MaxLength="100" Width="500px">
                    <ClientSideEvents Validation="function(s,e) { e.isValid = s.GetText().length > 4}" />
                    <ValidationSettings ValidationGroup="ProductoValidationGroup" CausesValidation="True"
                        Display="Dynamic" SetFocusOnError="True" ErrorDisplayMode="ImageWithText"
                        ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="La descripcion de la parametrica es requerido" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="TIPO" Caption="TIPO" VisibleIndex="4" Visible="False"></dx:GridViewDataTextColumn>

        </Columns>
        <%--<ClientSideEvents Init="habilitarEdicion" EndCallback="habilitarEdicion" />--%>
        <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="True" />
        <SettingsText ConfirmDelete="Eliminar este registro?" HeaderFilterSelectAll="(Seleccionar Todos)" HeaderFilterOkButton="Aceptar" HeaderFilterCancelButton="Cancelar" />
        <SettingsPager PageSize="20">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" />
        </SettingsPager>
        <Settings ShowFilterRow="false" ShowFilterRowMenu="false" />
        <SettingsCommandButton NewButton-ButtonType="Image">
            <ClearFilterButton Text="Nueva Búsqueda">
            </ClearFilterButton>
            <NewButton>
                <Image AlternateText="Nuevo" Url="../../../UI/img/iconos/Add-icon.png">
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
            <PagerBottomPanel BackColor="White"  ForeColor="#000000"></PagerBottomPanel>
            <FocusedRow CssClass="Hydro_Tr_Seleccionado" BackColor="transparent" ForeColor="black"></FocusedRow>
        </Styles>
    </dx:ASPxGridView>


    <dx:ASPxGridViewExporter ID="grdExportar" runat="server" GridViewID="grdParametrica">
    </dx:ASPxGridViewExporter>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
