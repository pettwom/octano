<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfReglamentacionCalidad.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad.wfReglamentacionCalidad" %>
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
            url: "../../../UI/Docs/DS_1499.pdf",

            pdfOpenParams: {
                view: 'FitB', pagemode: 'bookmarks',
                scrollbars: '1', toolbar: '1', statusbar: '1', messages: '1', navpanes: '1'
            }

        }).embed("pdf");

    };
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Decreto Supermo 1499</h1>
<hr class="Hydro_Linea_Verde"/>
<div style="margin-left: 40px;">
<p class="Hydro_Contenido_Negro_Normal">Reglamento de calidad de carburantes y lubricantes en el que se especificacan:
<div style="margin-left: 40px;">    
    <li class="Hydro_Contenido_Negro_Normal">7 tablas de especificación de calidad de prueba completa para Carburantes.</li>
    <li class="Hydro_Contenido_Negro_Normal">14 tablas de especificación de calidad de prueba completa para Lubricantes.</li>
    <li class="Hydro_Contenido_Negro_Normal">Lineamientos para la implementación y cumplimiento de calidad para productos terminados.</li>
</div>
</p>
</div>
<div id="pdf" > Haga click para descargar el documento ..  <a href="../../../UI/Docs/DS_1499.pdf">../../../UI/Docs/DS_1499.pdf</a></div>
</asp:Content>
