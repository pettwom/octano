<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfVeDocumento.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAlertas.wfVeDocumento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="background-color: white">
    <div>
    <asp:Panel runat="server" ID="panelDetalle" Width="920px">
            <h3 align="center">
                <asp:Label ID="lblTituloDetalle" runat="server"></asp:Label></h3>
            <iframe runat="server" id="iframeInforme" width="100%" height="370px" >
            </iframe>
        </asp:Panel>
        <center><h3 style="color: black;"><asp:Label ID="lblMensaje" runat="server" Text="NO EXISTE DOCUMENTO" Visible="False" Height="363px" Width="100%"></asp:Label></h3></center>
    </div>
    </form>
</body>
</html>
