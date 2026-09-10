using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using AnhAgenteServicios.ServicioHydroSesion;
using AnhPersistenciaCore.Entidades.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Clases.Persona;
using DevExpress.Web;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;


namespace AnhHydro.Sitio.VolumenesCalidad.GestionAlertas
{
    public partial class wfCertificadoCalidadAlertas : System.Web.UI.Page
    {
        #region variables

        string strAccion = "";
        string strMensajeError = "";

        #endregion

        #region atributos de Clase
        /// <summary>
        /// El gestor del servicio Hydro Sesion
        /// </summary>
        private CPersonaSesion _sesion;
        private string _mensajeError = string.Empty;
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        #endregion

        #region eventos del formulario
        protected void page_Init()
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
                    if (!CConsultaAccesos.AccesoFormulario(CParametrosHydro.strCredencialHydroAdmin,
                        Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]),
                        Convert.ToDecimal(CParametrosHydro.decIdAplicacion), Path.GetFileName(Request.Path),
                        ref strMensajeError))
                    {
                        FormsAuthentication.SignOut();
                        Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
                        Session.RemoveAll();
                        Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
                    }
                }
                catch (Exception ex)
                {

                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

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
                    CCalidadLibreria.ValidarSesionUsuario(Response);
                    if (!IsPostBack)
                    {
                        ViewState["idCalPrincipal"] = Request.QueryString["idCalPrincipal"];
                    }
                    ConfiguracionInicialFechas();
                    GenerarReporte(Convert.ToDecimal(ViewState["idCalPrincipal"]));
                    Session["observado"] = 1;
                    var a = Session["observado"];
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        private void ConfiguracionInicialFechas()
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
                    if (!IsPostBack)
                    {
                        txtFechaInicial.Text = DateTime.Now.ToString("01/MM/yyyy");
                        txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-ES");
                        txtFechaInicial.CalendarProperties.ClearButtonText = "Limpiar";
                        txtFechaInicial.CalendarProperties.TodayButtonText = "<< Hoy >> ";
                        txtFechaInicial.UseMaskBehavior = true;

                        txtFechaFinal.CalendarProperties.ClearButtonText = "Limpiar";
                        txtFechaFinal.CalendarProperties.TodayButtonText = "<< Hoy >> ";
                        txtFechaFinal.UseMaskBehavior = true;
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
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

                    grdExportar.WriteXlsxToResponse("ListaCertificadosCalidad" + DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void btnPdf_Click(object sender, EventArgs e)
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
                    grdExportar.WritePdfToResponse("ListaCertificadosCalidad" + DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void btnWord_Click(object sender, EventArgs e)
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
                    grdExportar.WriteRtfToResponse("ListaCertificadosCalidad" + DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdCertificadoCalidad_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
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

                    //
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdCertificadoCalidad_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
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

                    //
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
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
                    //
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        
        #endregion

        #region Metodos y Funciones

        protected void GenerarReporte(decimal idCalPrincipal)
        {
            List<O_CERTIFICADOS_ALERTAS_CTY> lstResultado;
            //TODO: borrar
            //Session[CVariablesSesion.UsuarioAdministrador] = 1;
            //TODO: borrar hasta aqui

            if (/*Convert.ToDecimal(Session[CVariablesSesion.UsuarioAdministrador]) == 1 ||*/ Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]))
            {
                lstResultado = clienteJson.Get<List<O_CERTIFICADOS_ALERTAS_CTY>>("ListarCertificadosConAlertas/" + cParametrosHydro.strCredencial + "/0/" + txtFechaInicial.Text.Replace("/", "_") + "/" + txtFechaFinal.Text.Replace("/", "_") + "?format=json");
            }
            else
            {
                lstResultado = clienteJson.Get<List<O_CERTIFICADOS_ALERTAS_CTY>>("ListarCertificadosConAlertas/" + cParametrosHydro.strCredencial + "/" + Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]) + "/" + txtFechaInicial.Text.Replace("/", "_") + "/" + txtFechaFinal.Text.Replace("/", "_") + "?format=json");
            }
            
            if (lstResultado != null && lstResultado.Count != 0)
            {

                lstResultado = (lstResultado.GroupBy(t => t.ID_REGISTROCAL_PRINCIPAL).Select(a => a.First()).Distinct().ToList());

                //var perfil = Session[CVariablesSesion.PerfilUsuario];
                //foreach (var item in perfil)
                //foreach (O_PERFILES_USUARIO_CTY item in (IEnumerable<O_PERFILES_USUARIO_CTY>)Session[CVariablesSesion.PerfilUsuario])
                //{
                    if (Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]))
                    {
                        grdCertificadoCalidad.Columns["resp"].Visible = true;
                    }
                    else if (Convert.ToBoolean(Session[CVariablesSesion.IsSupervisor]))
                    {
                        grdCertificadoCalidad.Columns["resp"].Visible = false;
                    }
                    else
                    {
                        Session[CParametrosCalidad.cValidacionCalidad] = clienteJson.Get<List<O_VALIDA_CALIDAD_CTY>>("/ObtenerValidacionCalidad/" + cParametrosHydro.strCredencial + "/" + 0 + "/" + Session[CVariablesSesion.UsuarioId] + "?format=json");

                        if (Session[CParametrosCalidad.cValidacionCalidad] != null)
                        {
                            if (((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).Where(x => x.VALIDA_CALIDAD == "ADJUNTA_DOCUMENTO_OBS").FirstOrDefault() != null)
                            {
                                grdCertificadoCalidad.Columns["resp"].Visible = true;
                            }
                            else
                            {
                                grdCertificadoCalidad.Columns["resp"].Visible = false;
                            }
                        }
                    }
                //}

                grdCertificadoCalidad.DataSource = lstResultado;
                grdCertificadoCalidad.SortBy(grdCertificadoCalidad.Columns[0], 1);
                grdCertificadoCalidad.DataBind();
            }
            else
            {
                grdCertificadoCalidad.DataSource = new List<O_CERTIFICADOS_ALERTAS_CTY>();
                grdCertificadoCalidad.DataBind();
            }
        }

        #endregion

    }
}