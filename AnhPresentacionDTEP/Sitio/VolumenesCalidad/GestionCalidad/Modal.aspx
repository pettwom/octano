<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modal.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad.Modal" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<link href="../../../UI/css/Hydro.css" rel="stylesheet" />

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
</head>
    <link href="/SirelV2/UI/css/hydro.css" rel="stylesheet" type="text/css" />
<body>
    <form id="form1" runat="server" class="Hydro_Form">
    <div >
        <asp:ScriptManager ID="ScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true"/>
        <p runat="server" id="lblMensaje" class="mensaje_error"></p>
        
        <asp:Panel ID="pnlDocumento1" runat="server">

        <h2 align="center">Registro Certificado Calidad</h2>
        <div class="alert-box error" runat="server" Visible="false" id="alert"><span>error: </span> <asp:Label ID="idmsjError" runat="server" Text=""></asp:Label></div>
        <div class="alert-box success" runat="server" Visible="False" id="Registro"><span>registro: </span><asp:Label ID="idMsjRegistro" runat="server" Text=""></asp:Label></div>
            <asp:Label ID="lblMsj" runat="server" Text="" Visible="False"></asp:Label>
        <asp:Panel ID="pnlDocumento" runat="server">
            <table align="center">
            <tr>
                <td align="right"><b>Documen en formato PDF:</b></td>
                <td align="left">
                    <dx:ASPxUploadControl ID="upArchivo" runat="server" Width="300px">
                        <validationsettings allowedfileextensions=".PDF">
                        </validationsettings>
                        <ValidationSettings MaxFileSize="20480000" AllowedFileExtensions=".pdf" NotAllowedFileExtensionErrorText="Solo se admiten *.pdf" MaxFileSizeErrorText="Error en archivo, verifique el tamaño (max: 15 Mb) y formato (.pdf)" />
                    </dx:ASPxUploadControl>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" class="auto-style1">
                    <asp:Button ID="btnGrabar" runat="server" OnClick="btnGrabarDocumento_Click" Text="Guargar Cambios" ClientIDMode="Static" />
                </td>
            </tr>
        </table>
        <br/>
       </asp:Panel>
      </asp:Panel>
    </div>
    </form>
</body>
</html>

