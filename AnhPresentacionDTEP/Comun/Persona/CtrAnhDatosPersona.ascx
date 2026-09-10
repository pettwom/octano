<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrAnhDatosPersona.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrAnhDatosPersona" %>
<%@ Register Src="CtrDatoPersona.ascx" TagName="CtrDatoPersona" TagPrefix="uc1" %>
<style type="text/css">
    .panelPersona
    {
        position: fixed;
        top: 250px;
        width: 500px;
        background: white;
    }
    .hydro_smart_container
    {
        margin: 0;
        padding: 0;
    }
    .hydro_smart_header
    {
        margin: 0;
        padding: 5px 10px;
        /*background: orange;*/
        background: rgb(0, 132, 38);
        color: white;
        font-size: medium;
        font-weight: bold;
    }
    .hydro_smart_table
    {
        font-size: smaller;
        width: 95%;
        margin: auto;
    }
    .hydro_smart_info
    {
        /*border: #FFCE4F solid thin;*/
        border: #1d9915 solid thin;
        margin: 5px;
        /*background: #FFF8F0;*/
        background: #f0fff8;
        border-radius: 3px;
    }
</style>
<uc1:CtrDatoPersona ID="datosPersona" runat="server" OnNroDocumentoTextChanged="nroIdentificacionTextChanged" OnTipoPersonaSelectedIndexChanged="tipoPersonaSelectedIndexChanged" />
<asp:Panel runat="server" ID="panelPersona" CssClass="panelPersona" Visible="false">
    <fieldset class="hydro_smart_container">
        <h3 class="hydro_smart_header">
            Esta registrando alguna de las siguientes personas?</h3>
        <div class="clear">
            &nbsp;</div>
        <asp:GridView ID="dtgPersonas" runat="server" AutoGenerateColumns="False" CssClass="hydro_smart_table"
            OnRowCommand="dtgPersonas_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="NRO. DOC.">
                    <ItemTemplate>
                        <%# Eval("CI")+" "+Eval("EXPEDICION")+ " "+ (Eval("COMPLEMENTO").ToString()=="0"?"":Eval("COMPLEMENTO")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NOMBRE_COMPLETO" HeaderText="NOMBRE" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="ELEGIR" CommandArgument=' <%# Eval("ID_PERSONA") %>'
                            Width="80" ImageUrl="~/UI/img/empadronamiento/btn_inicio_siguiente.png"
                            CausesValidation="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <p class="hydro_smart_info">
            * Elija alguna de las opciones.</p>
    </fieldset>
</asp:Panel>