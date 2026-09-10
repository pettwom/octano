<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfGestionCertificadoCalidad.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad.wfGestionCertificadoCalidad" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/Comun/UControl/ucRegistroCertificadoCalidadCarburantes.ascx" TagPrefix="uc1" TagName="ucRegistroCertificadoCalidadCarburantes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
     function a(){ $.msgBox({
           title: "Confirmación",
           content: "Se eliminó correctamente el certificado de calidad.",
           type: "confirm",
           buttons: [{ value: "Aceptar" }, { value: "Cancelar" }],
           success: function(result) {
               if (result == "Aceptar") {
                   location.href = "~/Sitio/VolumenesCalidad/ReportesCalidad/wfRptCertificadoCalidad.aspx";
                   ;
               }
           }
     });
     }
</script>
        
<center>
<table width="80%">
    <tr>
        <td >
        <uc1:ucRegistroCertificadoCalidadCarburantes visible="false"  ID="CtrRegistroCertificadoCalidadCarburantes" runat="server" />
        </td>
    </tr>
    
</table>
</center>
</asp:Content>

