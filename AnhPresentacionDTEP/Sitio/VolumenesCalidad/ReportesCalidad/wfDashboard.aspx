<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="wfDashboard.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad.wfDashboard" %>

<%@ Register Assembly="DevExpress.Dashboard.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script  type="text/javascript" src="../../../UI/js/jquery-1.10.2.js"></script>
    <script type="text/javascript">

        function OnInit(s, e) {
            AdjustSize();

            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
        function AdjustSize() {
            var height = document.documentElement.clientHeight;
            pc.SetHeight(height);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center" width="90%">
        <tr>
            <td>
                <fieldset style="background-color: whitesmoke">
                    <h3 align="left">FORMULARIO DE CONSULTA</h3>
                    <hr />
                    <div class="tablaConsulta" align="center">
                        <table width="90%" align="center">
                          <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lblEntidad" runat="server" Text="ENTIDAD:"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    <dx:ASPxComboBox ID="cmbEntidad" runat="server" ClientInstanceName="cmbEntidad" Width="100%">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {cmbActividad.PerformCallback();}" />
                                </dx:ASPxComboBox>
                                </td>
                                <td style="width: 20%">
                                    <asp:Label ID="lblActividad" runat="server" Text="ACTIVIDAD:"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    <dx:ASPxComboBox ID="cmbActividad" runat="server" ClientInstanceName="cmbActividad" OnCallback="cmbActividad_Callback" Width="100%">
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFechaInicio" runat="server" Text="FECHA INICIAL:"></asp:Label>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dtFechaInicial" runat="server"></dx:ASPxDateEdit>
                                </td>
                                <td >
                                   <asp:Label ID="lblFechaFin" runat="server" Text="FECHA FINAL:"></asp:Label>
                                </td>
                                <td >
                                    <dx:ASPxDateEdit ID="dtFechaFinal" runat="server"></dx:ASPxDateEdit>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btnConsultar" runat="server" Text="CONSULTAR" OnClick="btnConsultar_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Label ID="lblMensajeConsulta" runat="server" Text=""></asp:Label>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="100%">
    <dx:ASPxDashboardViewer ID="ASPxDashboardViewer1" runat="server" DashboardSource="~/UI/xml/1_reporte.xml" Height="600px" Width="800px" AllowExportDashboardItems="True" Font-Size="XX-Small"></dx:ASPxDashboardViewer>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
