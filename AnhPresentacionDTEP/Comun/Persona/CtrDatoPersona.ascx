<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrDatoPersona.ascx.cs" Inherits="AnhHydroTalleresOpeGarrafasPresentacion.Controles.CtrDatoPersona" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Panel runat="server" ID="panelFormulario" Width="550px">
    <fieldset>
        <legend>
            <h3>
                <asp:Label ID="lblTitulo" runat="server" Text="Información de la Persona" /></h3>
        </legend>
        <asp:Label runat="server" ID="lblMensaje" ForeColor="#2e8a04" BackColor="#d5fdc3">
        </asp:Label>
        <table style="width: 100%;">
            <tr>
                <td width="50%">

                    <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación" Width="40%"></asp:Label>
                    <asp:Label ID="lblIdentificaciona" runat="server" CssClass="obligatorio" >*</asp:Label>

                    <asp:Label ID="lblNumeroCI" runat="server" Text="Nro. Documento" Width="40%"></asp:Label>
                    <asp:Label ID="lblNumeroCIa" runat="server" CssClass="obligatorio">*</asp:Label>

                    <asp:DropDownList ID="cmbTipoIdentificacion" runat="server" DataTextField="NOMBRE"
                        DataValueField="ID" AutoPostBack="True" Width="45%" OnSelectedIndexChanged="cmbTipoIdentificacion_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:TextBox ID="txtNroIdentificacion" runat="server" Width="50%" MaxLength="20" AutoPostBack="true"
                        ToolTip="Número de cedula de identidad" OnTextChanged="txtCi_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="txtNroIdentificacionRequiredValidator0" runat="server" ValidationGroup="EmpresaValidation"
                        ErrorMessage="Este campo es requerido" Display="None" ControlToValidate="txtNroIdentificacion"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                        runat="server" TargetControlID="txtNroIdentificacionRequiredValidator0"></ajaxToolkit:ValidatorCalloutExtender>

                </td>
                <td width="25%">
                    <asp:Label ID="lblExpedido" runat="server" Text="Expedido en"></asp:Label>
                    <asp:Label ID="Label13" runat="server" CssClass="obligatorio">*</asp:Label>
                    <asp:DropDownList ID="cmbExpedicion" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                        Width="98%">
                    </asp:DropDownList>

                </td>
                <td width="25%">
                    <asp:Label ID="Label4" runat="server" Text="Compl."></asp:Label>
                    <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAIiSURBVDhPY/wPBAxkAkZ0zd9+fmHYdmoBw/k7+xm+/vgENpaPS4jBVN2NwcMkloGVhQ1uFYrmc3cOMEzdVMKgIqXP4KAfwiAlpMjwHwgfv77FsO/CKoYnb+4wlIbMYFCTMYQYALIZBM7e3v8/vFX1/6mbu2FCGPS+C6v/R7Sp/b/99CJYDqz507f3/2O7dMEGwMCus8v+p02w+J8+0er/lhPz4OJ7zq8Ei//8/eM/E8j2rSfnMejIWzIYqTiAXXPm1l6GxiVRDMJ8kgyi/NIM7SuTGI5f3w6WczYIA4sfvLSOAaz5HDBwXIwi4QHx9tNzBgsNT4a2xHUMIbZ5YPEX7x7A5R31QxlO3tgB0fzj1zcGcUE5uKS7SQxDT9o2hptPzjFUzw8CB6CtbgBcXlpYmeHD1zcQzeCA+/cPLgljTFiXB4wadoaJmXsZRIBOhYF///8xMAIhWDM3Bx/Ds3f3MDS7GUczlIRMZ+DnFkaRA0WdEK84RLO5hjvD9tOLUBT8/feXYePxmQyHr2zAMHTP+eUMNjp+EM3uxrEMj17dYDh0GaGQiZEJHFj2ukEomjccm8Hw+88vBistH0Qiufn4HDiRgOIXF1h7eOr/6A6t/0/f3AMrQUmewJTDMGF9Hjgt2+r4M0iLKDP8AwbkQ6CrjlzZyPDn72+G0rCZDNLCSmDXYGQMkOIDl9YynLq5i+H9l1fgUAUlCistbwZrbaBTkQAAyDxCk7nAOgIAAAAASUVORK5CYII="
                        alt="Es el código de complemento otorgado por el SEGIP <br/>para personas con problemas en su Documento de  Identidad.<br/> No es obligatorio" class="showtooltip" />
                    <asp:TextBox ID="txtComplemento" runat="server" MaxLength="2" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Nombres"></asp:Label>
                    <asp:Label ID="Label14" runat="server" CssClass="obligatorio">*</asp:Label>
                    <asp:TextBox ID="txtNombres" runat="server" MaxLength="45" Width="98%"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="txtNombresRequiredValidator0" runat="server" ValidationGroup="EmpresaValidation"
                        ErrorMessage="Este campo es requerido" Display="None" ControlToValidate="txtNombres"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2"
                        runat="server" TargetControlID="txtNombresRequiredValidator0"></ajaxToolkit:ValidatorCalloutExtender>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label6" runat="server" Text="Primer Apellido"></asp:Label>
                    <asp:Label ID="Label15" runat="server" CssClass="obligatorio">*</asp:Label>
                    <asp:TextBox ID="txtPrimerApellido" runat="server" MaxLength="40" Width="98%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="txtPrimerApellidoRequiredValidator0" runat="server" ValidationGroup="EmpresaValidation"
                        ErrorMessage="Este campo es requerido" Display="None" ControlToValidate="txtPrimerApellido"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                        runat="server" TargetControlID="txtPrimerApellidoRequiredValidator0"></ajaxToolkit:ValidatorCalloutExtender>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Segundo Apellido"></asp:Label>
                    <asp:TextBox ID="txtSegundoApellido" runat="server" MaxLength="40" Width="98%"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label8" runat="server" Text="Fecha de Nacimiento"></asp:Label>
                    <asp:Label ID="Label16" runat="server" CssClass="obligatorio">*</asp:Label>
                    <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAIiSURBVDhPY/wPBAxkAkZ0zd9+fmHYdmoBw/k7+xm+/vgENpaPS4jBVN2NwcMkloGVhQ1uFYrmc3cOMEzdVMKgIqXP4KAfwiAlpMjwHwgfv77FsO/CKoYnb+4wlIbMYFCTMYQYALIZBM7e3v8/vFX1/6mbu2FCGPS+C6v/R7Sp/b/99CJYDqz507f3/2O7dMEGwMCus8v+p02w+J8+0er/lhPz4OJ7zq8Ei//8/eM/E8j2rSfnMejIWzIYqTiAXXPm1l6GxiVRDMJ8kgyi/NIM7SuTGI5f3w6WczYIA4sfvLSOAaz5HDBwXIwi4QHx9tNzBgsNT4a2xHUMIbZ5YPEX7x7A5R31QxlO3tgB0fzj1zcGcUE5uKS7SQxDT9o2hptPzjFUzw8CB6CtbgBcXlpYmeHD1zcQzeCA+/cPLgljTFiXB4wadoaJmXsZRIBOhYF///8xMAIhWDM3Bx/Ds3f3MDS7GUczlIRMZ+DnFkaRA0WdEK84RLO5hjvD9tOLUBT8/feXYePxmQyHr2zAMHTP+eUMNjp+EM3uxrEMj17dYDh0GaGQiZEJHFj2ukEomjccm8Hw+88vBistH0Qiufn4HDiRgOIXF1h7eOr/6A6t/0/f3AMrQUmewJTDMGF9Hjgt2+r4M0iLKDP8AwbkQ6CrjlzZyPDn72+G0rCZDNLCSmDXYGQMkOIDl9YynLq5i+H9l1fgUAUlCistbwZrbaBTkQAAyDxCk7nAOgIAAAAASUVORK5CYII="
                        alt="Ej. (día/mes/año) Ej. 25/09/1990" class="showtooltip" />
                    <asp:TextBox ID="txtFechaNacimiento" runat="server" Width="98%"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="txtFechaNacimiento"
                        Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="txtFechaNacimientoRequiredValidator0" runat="server" ValidationGroup="EmpresaValidation"
                        ErrorMessage="Este campo es requerido" Display="None" ControlToValidate="txtFechaNacimiento"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4"
                        runat="server" TargetControlID="txtFechaNacimientoRequiredValidator0"></ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Género"></asp:Label>
                    <asp:Label ID="Label17" runat="server" CssClass="obligatorio">*</asp:Label>
                    <asp:DropDownList ID="cmbGenero" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                        Width="98%">
                    </asp:DropDownList>

                </td>
                <td colspan="2">
                    <asp:Label ID="Label11" runat="server" Text="Correo Electrónico:"></asp:Label>
                    <%-- <asp:Label ID="Label18" runat="server" CssClass="obligatorio"></asp:Label>--%>
                    <asp:TextBox ID="txtEmail" runat="server" Width="98%" Style="text-transform: lowercase;" MaxLength="50"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderEm" runat="server"
                        TargetControlID="txtEmail" FilterType="Custom"
                        ValidChars="1234567890 ABCDEFGHIJKLMNÑOPQRSTUVWXYZ@_-.+abcdefghijklmnñopqrstuvwxyz"></ajaxToolkit:FilteredTextBoxExtender>

                    <%--<asp:RequiredFieldValidator ID="txtEmailRequiredValidator0" runat="server" ValidationGroup="EmpresaValidation"
                                            ErrorMessage="Este campo es requerido" Display="None" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="txtEmailRequiredValidator0_ValidatorCalloutExtender"
                                            runat="server" TargetControlID="txtEmailRequiredValidator0">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                </td>
            </tr>
        </table>
    </fieldset>
    <!--TIPO DE IDENTIFICACION-->
    <asp:RequiredFieldValidator ID="cmbTipoIdentificacionRequiredFieldValidator" runat="server"
        ErrorMessage="El Tipo de Documento es Obligatorio." ControlToValidate="cmbTipoIdentificacion"
        ValidationGroup="PersonaValidationGroup" Display="None" />
    <!--NUMERO DOCUMENTO-->
    <asp:RequiredFieldValidator ID="txtPropietarioCiRequiredValidator" runat="server"
        ErrorMessage="El Número de Documento es Obligatorio." ControlToValidate="txtNroIdentificacion"
        ValidationGroup="PersonaValidationGroup" Display="None" />
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server"
        TargetControlID="txtNroIdentificacion" FilterType="Custom" ValidChars="0123456789ABCDEFGHIJKLMNÑOPEQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz -/" />
    <!--EXPEDICION-->
    <asp:RequiredFieldValidator ID="cmbCiExpedicionRequiredValidator" runat="server"
        ErrorMessage="El Lugar de Expedición es Obligatorio." ControlToValidate="cmbExpedicion"
        ValidationGroup="PersonaValidationGroup" Display="None" />
    <!--COMPLEMENTO-->
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        FilterType="Custom" TargetControlID="txtComplemento" ValidChars="ABCDEFGHIJKLMNÑOPEQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz123456789" />
    <!--NOMBRES-->
    <asp:RequiredFieldValidator ID="txtPropietarioNombresRequired" runat="server" ControlToValidate="txtNombres"
        ErrorMessage="El Nombre de la Persona es Obligatorio." ValidationGroup="PersonaValidationGroup"
        Display="None" />
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        FilterType="Custom" TargetControlID="txtNombres" ValidChars="ABCDEFGHIJKLMNÑOPEQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombres"
        ErrorMessage="El Primer Apellido de la Persona es Obligatorio." ValidationGroup="PersonaValidationGroup"
        Display="None" />
    <!--PRIMER APELLIDO-->
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
        FilterType="Custom" TargetControlID="txtPrimerApellido" ValidChars="ABCDEFGHIJKLMNÑOPEQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
    <!--SEGUNDO APELLIDO-->
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
        FilterType="Custom" TargetControlID="txtSegundoApellido" ValidChars="ABCDEFGHIJKLMNÑOPEQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
    <!--FECHA DE NACIMIENTO-->
    <asp:RequiredFieldValidator ID="txtFechaNacimientoRequired" runat="server" ControlToValidate="txtFechaNacimiento"
        ErrorMessage="La Fecha de Nacimiento de la Persona es Obligatorio." ValidationGroup="PersonaValidationGroup"
        Display="None" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"
        ErrorMessage="El Formato de Fecha debe ser: dia/mes/año Ej.25/09/1990" ValidationGroup="PersonaValidationGroup"
        ControlToValidate="txtFechaNacimiento" ValidationExpression="^(?:(?:0?[1-9]|1\d|2[0-8])(\/|-)(?:0?[1-9]|1[0-2]))(\/|-)(?:[1-9]\d\d\d|\d[1-9]\d\d|\d\d[1-9]\d|\d\d\d[1-9])$|^(?:(?:31(\/|-)(?:0?[13578]|1[02]))|(?:(?:29|30)(\/|-)(?:0?[1,3-9]|1[0-2])))(\/|-)(?:[1-9]\d\d\d|\d[1-9]\d\d|\d\d[1-9]\d|\d\d\d[1-9])$|^(29(\/|-)0?2)(\/|-)(?:(?:0[48]00|[13579][26]00|[2468][048]00)|(?:\d\d)?(?:0[48]|[2468][048]|[13579][26]))$">
    </asp:RegularExpressionValidator>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
        FilterType="Custom" TargetControlID="txtFechaNacimiento" ValidChars="0123456789/" />
    <!--EMAIL-->
    <%--    <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="None" ControlToValidate="txtEmail"
        ErrorMessage="El Formato de Correo Electrónico debe ser: miusuario@dominio.com" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="None" ControlToValidate="txtEmail"
        ErrorMessage="El Correo Electrónico es Obligatorio" ValidationGroup="PersonaValidationGroup" />--%>
</asp:Panel>
