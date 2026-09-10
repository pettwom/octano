using System.IO;
using System.Web.Security;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Comun.UControl;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Librerias.Anh.Us;
using ServiceStack.Messaging;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad
{
    public partial class wfGestionCertificadoCalidad : System.Web.UI.Page
    {
        #region variables

        string strAccion = "";
        string strMensajeError = "";

        #endregion

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
                    
                    Session["elimina"] = "";
                    Session["modifica"] = "";
                    // 1=Insertar, 2=Modificar y 3=Eliminar
                    CtrRegistroCertificadoCalidadCarburantes.TipoDeRegistro = Convert.ToInt32(Session["tipoRegistro"]); //1;
                    if (CtrRegistroCertificadoCalidadCarburantes.TipoDeRegistro == 2 || CtrRegistroCertificadoCalidadCarburantes.TipoDeRegistro == 3)
                    {
                        //ucCargadoMenuCertificadoCalidad.Visible = false;
                        CtrRegistroCertificadoCalidadCarburantes.Visible = true;
                        CtrRegistroCertificadoCalidadCarburantes.NumeroCorrelativoReporte = Session["citeGenerado"].ToString().Replace("/", "_").Replace(" ", "%20");
                    }
                    CtrRegistroCertificadoCalidadCarburantes.Llave = cParametrosHydro.strCredencial;
                    CtrRegistroCertificadoCalidadCarburantes.GridSelectorChanged += new ucRegistroCertificadoCalidadCarburantes.GuardarRegistroEventHandler(ObtenerNumeroCite);
                    if (!IsPostBack)
                    {

                        this.CtrRegistroCertificadoCalidadCarburantes.IdUsuario = Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.UsuarioId]);
                        this.CtrRegistroCertificadoCalidadCarburantes.IdEntidad = Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.IdEntidad]);
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        void ObtenerNumeroCite(ucRegistroCertificadoCalidadCarburantes.GuardarRegistroCommandEventArgs e)
        {
            CtrRegistroCertificadoCalidadCarburantes.NumeroCorrelativo = e.NumeroCorrelativo;
            if (CtrRegistroCertificadoCalidadCarburantes.TipoDeRegistro == 3)
                Session["elimina"] = e.NumeroCorrelativo;
            if (CtrRegistroCertificadoCalidadCarburantes.TipoDeRegistro == 2)
                Session["modifica"] = e.NumeroCorrelativo;

            if (Session[CVariablesSesion.NombreEntidadAgencia]!=null && Session[CVariablesSesion.NombreEntidadAgencia].ToString() == "AGENCIA NACIONAL DE HIDROCARBUROS")
            {
                Response.Redirect("~/Sitio/VolumenesCalidad/ReportesCalidad/wfRptCertificadoCalidad.aspx");
            }
            else
            {
                Response.Redirect("~/Sitio/VolumenesCalidad/ReportesCalidad/wfRptCertificadoCalidadConsultas.aspx");
            }
        }

    }
}