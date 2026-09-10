<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfModificar.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAlertas.wfModificar" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>


<link href="../../../UI/css/Hydro.css" rel="stylesheet" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            height: 23px;
        }
        .auto-style2
        {
            width: 171px;
        }
        .auto-style4
        {
            width: 241px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="Hydro_Form">
    <div>
        <asp:ScriptManager ID="ScriptManager2" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true"/>
        <div id="Titulo" runat="server"><h2 align="center">Lista de Documentos</h2></div>
        <asp:Panel ID="pnlActualizacion" runat="server" Visible="False">
            <h2 align="center">Modificación de Documento</h2>
            <div class="alert-box error" runat="server" Visible="false" id="alert"><span>error: </span> <asp:Label ID="idmsjError" runat="server" Text=""></asp:Label></div>
            <div class="alert-box success" runat="server" Visible="False" id="Registro"><span>registro: </span><asp:Label ID="idMsjRegistro" runat="server" Text=""></asp:Label></div>
            <table align="center" style="width: 522px">
                <tr>
                    <td align="right" ><b>Cite:</b></td>
                    <td >
                        <asp:Label ID="lblCite" runat="server" Text="..."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" ><b>Observación:<asp:HiddenField ID="hfidTipoRespaldo" runat="server" />
                        </b></td>
                    <td >
                        <asp:TextBox ID="txtObservacion" runat="server" Height="62px" TextMode="MultiLine" onkeyup="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>
                        <asp:HiddenField ID="hfIdDocumento" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" ><asp:UpdatePanel ID="upnlDocumentoDig" runat="server" UpdateMode="Conditional">
                        <ContentTemplate><b><asp:Label ID="lbldoc" runat="server" Text="Documen en formato PDF:"></asp:Label></b>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td align="left">
                        <dx:ASPxUploadControl ID="upArchivo" runat="server">
                            <ValidationSettings AllowedFileExtensions=".PDF">
                            </ValidationSettings>
                        </dx:ASPxUploadControl>
                    </td>
                </tr>
                         
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnGrabar" runat="server" Text="Actualizar Documento" OnClick="btnGrabar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                    </td>

                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlEliminar" runat="server" Visible="False">
            <h2 align="center" style="color: red">ESTA SEGURO DE ELIMINAR EL DOCUMENTO:</h2>
            <table align="center">
                <tr>
                    <td align="right" ><b>Cite:</b></td>
                    <td >
                        <asp:Label ID="lblCitesEli" runat="server" Text="..."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right"><b>Observación:</b>
                        <asp:HiddenField ID="hfidTipoRespaldoEli" runat="server" />
                    </td>

                    <td>
                        <asp:Label ID="lblObsEli" runat="server" Text="..."></asp:Label>
                        <asp:HiddenField ID="hfIdDocumentoEli" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Documento" OnClick="btnEliminar_Click" Width="230px" />
                        <asp:Button ID="btnCancelarEli" runat="server" Text="Cancelar" OnClick="btnCancelarEli_Click" Width="230px"/>
                    </td>

                </tr>
                <tr>
                    <td align="center" colspan="2">&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <div align="center">
            <dx:ASPxGridView ID="dtgDocumentos" runat="server" AutoGenerateColumns="False" EnableTheming="True" 
                OnRowCommand="dtgEmpresas_RowCommand" Width="100%" Theme="Default">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Id_documento" FieldName="ID_DOCUMENTO" VisibleIndex="1" Visible="False" >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cite" FieldName="CITE" VisibleIndex="2" >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Observacion" FieldName="OBSERVACION" VisibleIndex="3" >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ID_TIPO_RESPALDO" FieldName="ID_TIPO_RESPALDO" VisibleIndex="4" Visible="False">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tipo Respaldo" FieldName="RESPALDO" VisibleIndex="5" >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn VisibleIndex="6" Width="125px">
                    <DataItemTemplate>
                        <a runat="server" id="lnkVer" href='<%# "wfVeDocumento.aspx?Cite="+Eval("CITE")+"&idTipoRespaldo="+Eval("ID_TIPO_RESPALDO") + "&idDocumento="+Eval("ID_DOCUMENTO") %>' class="lnkVentana" title="Licencia">Ver documento<br/></a>
                        <asp:LinkButton ID="lblModificar" runat="server" CommandArgument='<%# "Modificar"+"-"+Eval("ID_DOCUMENTO")+"-"+Eval("ID_TIPO_RESPALDO")+"-"+Eval("OBSERVACION")%>' CommandName="Modificar" >Modificar <br/></asp:LinkButton>
                        <asp:LinkButton ID="lblEliminar" runat="server" CommandArgument='<%# "Eliminar"+"-"+Eval("ID_DOCUMENTO")+"-"+Eval("ID_TIPO_RESPALDO")+"-"+Eval("OBSERVACION")%>' CommandName="Modificar" >Eliminar</asp:LinkButton>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
    </form>
</body>
</html>
