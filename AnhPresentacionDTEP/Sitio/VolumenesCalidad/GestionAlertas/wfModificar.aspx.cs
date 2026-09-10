using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using DevExpress.Web;
using ServiceStack.ServiceClient.Web;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAlertas
{
    public partial class wfModificar : System.Web.UI.Page
    {
        string strAccion = "";
        string strMensajeError = "";
        private string _mensajeError = string.Empty;
        private string _cite;
        private string _citeConvert;

        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

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

                    _cite = Request.QueryString["Cite"];
                    _citeConvert = (_cite.Replace("%20", " ")).Replace("/", "_");

                    if (!IsPostBack)
                    {
                       cargarDatos();
                    }
                    else
                    {
                       cargarDatos();
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

        private void cargarDatos()
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

                    List<O_LISTA_DOCUMENTOS_CTY> lstResultado = clienteJson.Get<List<O_LISTA_DOCUMENTOS_CTY>>("/ListarDocumentos/" + cParametrosHydro.strCredencial + "/" + _citeConvert + "/" + 1 + "?format=json");

                    if (lstResultado != null)
                    {
                        dtgDocumentos.DataSource = lstResultado;
                        dtgDocumentos.DataBind();
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

        protected void btnGrabar_Click(object sender, EventArgs e)
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
                        byte[] byteContenido = upArchivo.FileBytes;

                        AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ActualizaDocumento vObjActualizarProde
                            = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ActualizaDocumento();
                        vObjActualizarProde.strLlave = cParametrosHydro.strCredencial;
                        vObjActualizarProde.decIdDocumento = Convert.ToDecimal(hfIdDocumento.Value);
                        vObjActualizarProde.byteDocumento = byteContenido;
                        vObjActualizarProde.strObservacion = txtObservacion.Text;
                        vObjActualizarProde.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                        vObjActualizarProde.decAppFechaRegistro =
                            Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(DateTime.Now)));

                        var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/ActualizaDocumento/?format=json", vObjActualizarProde);
                        if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                        {
                            throw new Exception(lstResultado == null
                                ? "No se actualizo correctamente el registro"
                                : lstResultado[0].MENSAJE_ERROR);
                        }
                        
                        if (lstResultado[0].ID_TABLA != 0)
                        {
                            Response.Redirect("~/Sitio/VolumenesCalidad/GestionAlertas/wfVeDocumento.aspx?idDocumento=" + Convert.ToDecimal(hfIdDocumento.Value) + "&Cite=" + lblCite.Text + "&idTipoRespaldo=" + 1, false);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                            return;
                        }
                    }
                    else
                    {
                        alert.Visible = true;
                        idmsjError.Text = "Debe seleccionar el documento a Actualizar.<br>";
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

        protected void btnCancelar_Click(object sender, EventArgs e)
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

                    pnlActualizacion.Visible = false;
                    hfIdDocumento.Value = "";
                    hfidTipoRespaldo.Value = "";

                    #endregion
                }
                catch (Exception exp)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(exp, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
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

                    AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.EliminaDocumento vObjEliminaDoc = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.EliminaDocumento();
                    vObjEliminaDoc.strLlave = cParametrosHydro.strCredencial;
                    vObjEliminaDoc.decIdDocumento = Convert.ToDecimal(hfIdDocumentoEli.Value);
                    vObjEliminaDoc.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]); ;
                    vObjEliminaDoc.decAppFechaRegistro = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(DateTime.Now)));

                    var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/EliminaDocumento/?format=json", vObjEliminaDoc);
                    if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                    {
                        throw new Exception(lstResultado == null ? "No se elimino correctamente el registro." : lstResultado[0].MENSAJE_ERROR);
                    }
                    else
                    {
                        pnlActualizacion.Visible = false;
                        pnlEliminar.Visible = false;
                        cargarDatos();
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

        protected void btnCancelarEli_Click(object sender, EventArgs e)
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

                    pnlEliminar.Visible = false;
                    hfIdDocumento.Value = "";
                    hfidTipoRespaldo.Value = "";

                    #endregion
                }
                catch (Exception exp)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(exp, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void dtgEmpresas_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
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

                    string strArgs = e.CommandArgs.CommandArgument.ToString();
                    string[] strParametros = strArgs.Split('-');

                    switch (strParametros[0].ToString(CultureInfo.InvariantCulture))
                    {
                        case "Modificar":
                            lblCite.Text = _cite;
                            txtObservacion.Text = strParametros[3];
                            hfidTipoRespaldo.Value = strParametros[2];
                            hfIdDocumento.Value = strParametros[1];
                            pnlActualizacion.Visible = true;
                            pnlEliminar.Visible = false;
                            break;

                        case "Eliminar":
                            lblCitesEli.Text = _cite;
                            lblObsEli.Text = strParametros[3];
                            hfidTipoRespaldoEli.Value = strParametros[2];
                            hfIdDocumentoEli.Value = strParametros[1];
                            pnlActualizacion.Visible = false;
                            pnlEliminar.Visible = true;
                            break;
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
    }
}