using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios.ServicioHydroListados;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Entidades;
using AnhPresentacionDTEP.Lib;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhServicioWebOctano.ServiciosWeb.Gestion;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using O_RESULTADO_CTY = AnhPersistenciaCore.Core.O_RESULTADO_CTY;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAlertas
{
    public partial class wfRegistroPDF : System.Web.UI.Page
    {
        #region variables
        string strAccion = "";
        string strMensajeError = "";
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        private decimal _decIdUsuario;
        #endregion

        #region atributos de Clase

        private string _mensajeError = string.Empty;
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                try
                {
                    #region Codigo a controlar

                    CCalidadLibreria.ValidarSesionUsuario(Response);
                    decimal _idCalPrincipal = Convert.ToDecimal(Request["idCalPrincipal"]);
                    _decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);

                    lblCite.Text = Request.QueryString["Cite"];

                    if (!IsPostBack)
                    {
                        CargarListado();
                    }

                    if (!IsPostBack)
                    {

                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(exp, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        private void CargarListado()
        {
            
        }

        protected void btnGrabarDocumento_Click(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                try
                {
                    #region Codigo a controlar

                    if (upArchivo.FileBytes.Count() > 0)
                    {
                        alert.Visible = false;
                        idmsjError.Text = "";
                        grabarInformacion();
                    }
                    else
                    {
                        alert.Visible = true;
                        idmsjError.Text = "Debe seleccionar el documento a grabar.<br>";
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.UsuarioId],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                }
            }
        }

        /// <summary>
        /// Guarda el archivo digital.
        /// </summary>
        private void grabarInformacion()
        {

            byte[] pdfBytes = upArchivo.FileBytes;

            SvcRegistroDtep.RegistraDocumento vObjRegistraDoc = new SvcRegistroDtep.RegistraDocumento();

            vObjRegistraDoc.strLlave = cParametrosHydro.strCredencial;
            vObjRegistraDoc.decIdTipRespaldo = 1; //Obs.
            vObjRegistraDoc.strCite = lblCite.Text;
            vObjRegistraDoc.byteDocumento = pdfBytes;
            vObjRegistraDoc.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
            vObjRegistraDoc.decAppFechaRegistro = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(DateTime.Now)));
            vObjRegistraDoc.strObservacion = txtObservacion.Text;

            var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistraDocumento/?format=json", vObjRegistraDoc);

            if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
            {
                throw new Exception(lstResultado == null ? "No se registró el producto comercial" : lstResultado[0].MENSAJE_ERROR);
            }
            else
            {
                //alert.Visible = true;
                lblMsj.Text = "Se registro el documento exitosamente.<br>";
                lblMsj.Visible = true;
                pnlDocumento.Visible = false;
            }

            if (lstResultado[0].ID_TABLA != 0)
            {
                Response.Redirect("~/Sitio/VolumenesCalidad/GestionAlertas/wfVeDocumento.aspx?idDocumento=" + lstResultado[0].ID_TABLA + "&idTipoRespaldo=" + 1, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;
            }
        }


        


    }
}