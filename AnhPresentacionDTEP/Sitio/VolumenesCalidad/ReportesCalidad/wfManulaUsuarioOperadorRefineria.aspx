<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfManulaUsuarioOperadorRefineria.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad.wfManulaUsuarioOperadorRefineria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
<!--

#pdf {
	width: 850px;
	height: 500px;
	margin: 2em auto;
	border: 10px solid #86A034;
}

#pdf p {
   padding: 1em;
}

#pdf object {
   display: block;
   border: solid 1px #666;
}

-->
</style>

<script src="../../../UI/js/pdfobject.js" type="text/javascript"></script>
<script type="text/javascript">

    window.onload = function () {
        var myPDF = new PDFObject({
            url: "<%=url%>",

            pdfOpenParams: {
                view: 'FitB', pagemode: 'bookmarks',
                scrollbars: '1', toolbar: '1', statusbar: '1', messages: '1', navpanes: '1'
            }

        }).embed("pdf");

    };
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Manual de usuario para el perfil Operador</h1>
<hr class="Hydro_Linea_Verde"/>
<div style="margin-left: 40px;">
<p class="Hydro_Contenido_Negro_Normal">Manual de usuario con la operaciones asignados dentro del control de calidad para el perfil Operador, cuyo objetivo principal es:
    <div style="margin-left: 30px;">
    <li class="Hydro_Contenido_Negro_Normal">Registrar las especificaciones de calidad de productos Carburantes y Lubricantes de acuerdo a su certificado de verificación.</li>
    <li class="Hydro_Contenido_Negro_Normal">Listar los certificados de verificación registrados.</li>
    <li class="Hydro_Contenido_Negro_Normal">Generar el detalle del certificado de verificación registrado.</li>
    </div>
</p>
</div>
<div id="pdf" > Haga click para descargar el documento : <a href="../../../UI/Docs/ManualUsuario_Operador_v1.0.pdf">ManualUsuario_Operador_v1.0.pdf</a></div>
</asp:Content>
