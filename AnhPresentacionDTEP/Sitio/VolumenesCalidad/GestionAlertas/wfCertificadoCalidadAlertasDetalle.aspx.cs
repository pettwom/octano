using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AnhAgenteServicios.ServicioHydroSesion;
using AnhPresentacionDTEP.Parametros;
using DevExpress.Web;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhPersistenciaCore.Core;
using Librerias.Anh.Us;
using ServiceStack.Common;
using ServiceStack.ServiceClient.Web;

namespace AnhHydro.Sitio.VolumenesCalidad.GestionAlertas
{
    public partial class wfCertificadoCalidadAlertasDetalle : System.Web.UI.Page
    {
        #region variables

        string strAccion = "";

        #endregion

        #region atributos de Clase

        private string _mensajeError = string.Empty;

        //private IServicioReportesCalidad _servicioListadosCalidad = LocalizadorProxy.ServicioReportesCalidad();
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        #endregion

        #region eventos del formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            CCalidadLibreria.ValidarSesionUsuario(Response);
            if (!IsPostBack)
            {
                ViewState["idCalPrincipal"] = Request.QueryString["idCalPrincipal"];
                if (!Request.QueryString["cite"].IsNullOrEmpty())
                { ViewState["cite"] = Request.QueryString["cite"]; }
                else
                { ViewState["cite"] = ""; }
            }
            GenerarReporte(Convert.ToDecimal(ViewState["idCalPrincipal"]), ViewState["cite"].ToString());

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //grdExportar.WriteXlsxToResponse("ListaCertificadosCalidad" +DateTime.Now.ToString(Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ));
            UpdateExportMode();
            grdExportar.WriteXlsxToResponse("ListaCertificadosCalidad" + DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            UpdateExportMode();
            grdExportar.WritePdfToResponse("ListaCertificadosCalidad" + DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));
        }

        protected void btnWord_Click(object sender, EventArgs e)
        {
            UpdateExportMode();
            grdExportar.WriteRtfToResponse("ListaCertificadosCalidad" + DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));
        }

        protected void UpdateExportMode()
        {
            grdCertificadoObservado.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), "Expanded");
        }

        protected void grid_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            if (e.Expanded)
            {
                ASPxGridView grdAcciones =
                    this.grdCertificadoObservado.FindDetailRowTemplateControl(e.VisibleIndex, "grdCertificadoCalidad")
                    as ASPxGridView;
                string idCalPrincipal = grdCertificadoObservado.GetRowValues(e.VisibleIndex, "CITE_DOCUMENTO").ToString();
                if (grdAcciones != null)
                {
                    List<O_CERTIFICADO_ALERTAS_CTY> lstResultadoDet = clienteJson.Get<List<O_CERTIFICADO_ALERTAS_CTY>>("ListarCertificadoConAlertas/" + Session["idCalPrincipal"] + "?format=json");
                    grdAcciones.DataSource = lstResultadoDet;
                    grdAcciones.DataBind();

                }
            }
        }

        protected void grdCertificadoCalidad_HtmlRowPrepared1(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                var observacion = e.GetValue("DESCRIPCION");
                //if (observacion == null) return;
                var indice = grdCertificadoObservado.FindVisibleIndexByKeyValue(observacion);

                ASPxGridView grvCompart =
                    (ASPxGridView)grdCertificadoObservado.FindDetailRowTemplateControl(indice, "grdCertificadoCalidad");

                string estadoVerif = (string)grvCompart.GetRowValues(e.VisibleIndex, "OBSERVACION");

                if (estadoVerif != null)
                {
                    e.Row.CssClass = "Hydro_Div_Aviso_Rojo Hydro_Alerta_Rojo";
                }

            }
            catch (Exception ex)
            {
                CLogTraza.MensajeUsuario oMensajeUsuario = new CLogTraza.MensajeUsuario();
                oMensajeUsuario.decUsuarioId = Session[CVariablesSesion.UsuarioId];
                oMensajeUsuario.decIdModulo = CParametrosHydro.decIdAplicacion;
                oMensajeUsuario.strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                oMensajeUsuario.strIp = HttpContext.Current.Request.UserHostAddress;
                oMensajeUsuario.decNivelCapa = Convert.ToInt16(CLogTraza.CapasNivel.Presentacion);

                CLogTraza.Error(oMensajeUsuario, ex);
            }
        }

        #endregion

        #region Metodos y Funciones

        protected void GenerarReporte(decimal idCalPrincipal, string cite)
        {

            List<O_REPORTE_CALIDAD> lstResultadoDet = clienteJson.Get<List<O_REPORTE_CALIDAD>>("/ReportarCalidad/" + cParametrosHydro.strCredencial + "/" + cite.Replace(" ", "%20").Replace("/", "_") + "?format=json");

            List<O_REPORTE_CALIDAD> lstResultadoDetAux = new List<O_REPORTE_CALIDAD>();
            lstResultadoDetAux.Add(lstResultadoDet[0]);

            if (lstResultadoDet != null)
            {
                grdCertificadoObservado.DataSource = lstResultadoDetAux;
                grdCertificadoObservado.DataBind();
                grdCertificadoObservado.ExpandAll();
            }
            List<O_CERTIFICADO_ALERTAS_CTY> lstResultado = clienteJson.Get<List<O_CERTIFICADO_ALERTAS_CTY>>("ListarCertificadoConAlertas/" + idCalPrincipal + "?format=json");
            Session["idCalPrincipal"] = idCalPrincipal;

            if (lstResultado != null)
            {
                //grdCertificadoCalidad.DataSource = lstResultado;
                //grdCertificadoCalidad.DataBind();
            }

        }

        #endregion

    }
}