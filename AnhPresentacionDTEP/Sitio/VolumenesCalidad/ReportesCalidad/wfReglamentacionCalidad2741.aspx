<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfReglamentacionCalidad2741.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad.wfReglamentacionCalidad2741" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
<!--

#pdf {
	width: 850px;
	max-width: 100%;
	height: 500px;
	margin: 2em auto;
	border: 10px solid #86A034;
        border-radius: 8px;
        background: #fff;
        box-shadow: 0 1px 4px rgba(0,0,0,0.06);
        overflow: hidden;
}

#pdf p {
   padding: 1em;
}

#pdf object {
   display: block;
   border: solid 1px #666;
}

/* ─── Títulos ─── */
.section-title {
    font-weight: 600;
    letter-spacing: 0.3px;
    margin-bottom: 4px;
}
.subtitle {
    font-weight: 500;
    letter-spacing: 0.2px;
    margin-top: 0;
    margin-bottom: 16px;
    display: flex;
    align-items: center;
    gap: 8px;
}

/* ─── Content card ─── */
.content-card {
    border: 1px solid #e9ecef;
    border-radius: 8px;
    background: #f8f9fa;
    padding: 20px 24px;
    margin-bottom: 20px;
    box-shadow: 0 1px 4px rgba(0,0,0,0.06);
}

/* ─── Responsive ─── */
@media (max-width: 900px) {
    #pdf { width: 100%; height: 400px; }
}
@media (max-width: 480px) {
    #pdf { height: 300px; border-width: 5px; }
}

-->
</style>

<script src="../../../UI/js/pdfobject.js" type="text/javascript"></script>
<script type="text/javascript">

    window.onload = function () {
        var myPDF = new PDFObject({
            url: "../../../UI/Docs/DS_5619.pdf",

            pdfOpenParams: {
                view: 'FitB', pagemode: 'bookmarks',
                scrollbars: '1', toolbar: '1', statusbar: '1', messages: '1', navpanes: '1'
            }

        }).embed("pdf");

    };
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Decreto Supermo D.S. 5619</h1>
<hr class="Hydro_Linea_Verde"/>
<div style="margin-left: 40px;">
<p class="Hydro_Contenido_Negro_Normal">El presente Decreto Supremo tiene por objeto modificar y complementar el Reglamento de Calidad de Carburantes y el Reglamento de Lubricantes, aprobados por Decreto Supremo N° 5619, de 14 de mayo de 2026. 
<div style="margin-left: 40px;">    
    <%--<li class="Hydro_Contenido_Negro_Normal">4 tablas de especificación de calidad para Carburantes sustituidas.</li>
    <li class="Hydro_Contenido_Negro_Normal">22 tablas de especificación de calidad de Lubricantes sustituidas. </li>--%>
    <li class="Hydro_Contenido_Negro_Normal">Incorpora lineamientos para la implementación y cumplimiento de calidad para productos terminados.</li>
</div>
</p>
</div>
<div id="pdf" > Haga click para descargar el documento ..  <a href="../../../UI/Docs/DS_5619.pdf">../../../UI/Docs/DS_5619.pdf</a></div>
</asp:Content>
