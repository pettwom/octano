using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroListados;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes;
using DevExpress.Web;
using ServiceStack.ServiceClient.Web;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad
{
    public partial class wfRptCertificadoCalidadBajas : System.Web.UI.Page
    {
        #region Variables

        private decimal decIdDireccion = 0;
        private bool _fechaCorrecta = true;
        string strMensajeError = "";
        string strAccion = "";
        readonly IServicioHydroListados _servicioHydroListado = LocalizadorProxy.ServicioHydroListados();
        string mensajehydro = "";
        List<O_REPORTE_CALIDAD_PRINCIPAL> lstResultado = null;

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
                    Session["observado"] = 0;
                    CCalidadLibreria.ValidarSesionUsuario(Response);
                    txtFechaFinal.MaxDate = System.DateTime.Now;
                    txtFechaInicial.MaxDate = System.DateTime.Now;
                    string perfil = Session[CVariablesSesion.PerfilUsuario].ToString();

                    RecuperarIdDireccion();

                    if (Request.Params["fi"] != null && Request.Params["ff"] != null)
                    {
                        RecuperarFechas();
                        if (!_fechaCorrecta)
                        {
                            ConfigurarFechaInicial();
                        }
                    }
                    else if (!IsPostBack)
                    {
                        ConfigurarFechaInicial();
                    }

                    GenerarReporte();
                    if (Session["elimina"] != null)
                    {
                        if (Session["elimina"].ToString() != "")
                            ClientScript.RegisterStartupScript(GetType(), "myScript",
                                "MsgEliminar('" + Session["elimina"].ToString() + "');", true);
                        Session["elimina"] = "";
                    }
                    if (Session["modifica"] != null)
                    {
                        if (Session["modifica"].ToString() != "" && Session["modifica"].ToString() != "-99999")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "myScript",
                                "MsgModificar('" + Session["modifica"].ToString() + "');", true);
                        }
                        else if (Session["modifica"].ToString() == "-99999")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "myScript",
                                                   "MsgModificarError('');", true);
                        }
                        Session["modifica"] = null;
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdCertificadoCalidad_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID == "Editar")
                {
                    var row = (O_REPORTE_CALIDAD_PRINCIPAL)grdCertificadoCalidad.GetRow(e.VisibleIndex);

                    Session["citeGenerado"] = row.CITE_GENERADO.ToString();
                    Session["tipoRegistro"] = 2;
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(
                        "~/Sitio/VolumenesCalidad/GestionCalidad/wfGestionCertificadoCalidad.aspx");
                }
                else if (e.ButtonID == "Eliminar")
                {
                    var row = (O_REPORTE_CALIDAD_PRINCIPAL)grdCertificadoCalidad.GetRow(e.VisibleIndex);

                    Session["citeGenerado"] = row.CITE_GENERADO.ToString();
                    Session["tipoRegistro"] = 3;
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(
                        "~/Sitio/VolumenesCalidad/GestionCalidad/wfGestionCertificadoCalidad.aspx");
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void grdCertificadoCalidad_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.Parameters);
                var row = (O_REPORTE_CALIDAD_PRINCIPAL)grdCertificadoCalidad.GetRow(index);
                if (row.CITE_GENERADO != null)
                {
                    Session["citeGenerado"] = row.CITE_GENERADO.ToString();
                    Session["tipoRegistro"] = 3;
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(
                        "~/Sitio/VolumenesCalidad/GestionCalidad/wfGestionCertificadoCalidad.aspx");
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                grdExportar.WriteXlsxToResponse("ListaCertificadosCalidad" +
                                                DateTime.Now.ToString(
                                                    Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ));
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                grdExportar.Landscape = true;
                grdExportar.LeftMargin = 20;
                grdExportar.RightMargin = 20;
                grdExportar.TopMargin = 20;
                grdExportar.BottomMargin = 20;

                grdExportar.WritePdfToResponse("ListaCertificadosCalidad" +
                                                DateTime.Now.ToString(
                                                    Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ));
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void btnWord_Click(object sender, EventArgs e)
        {
            try
            {
                grdExportar.WriteRtfToResponse("ListaCertificadosCalidad" +
                                                DateTime.Now.ToString(
                                                    Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ));
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }

        #region Metodos
        protected void GenerarReporte()
        {
            try
            {
                if (Session[CVariablesSesion.NombreEntidad] == null)
                {
                    if (/*Convert.ToDecimal(Session[CVariablesSesion.UsuarioAdministrador]) == 1 ||*/ Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]))
                    {
                        var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                        lstResultado = clienteJson.Get<List<O_REPORTE_CALIDAD_PRINCIPAL>>("/ReportarCalidadPrincipal/" + cParametrosHydro.strCredencial +
                        "/" + 0 +
                        "/" + 0 +
                        "/" + txtFechaInicial.Text.Replace("/", "-") +
                        "/" + txtFechaFinal.Text.Replace("/", "-") +
                        "/3?format=json");
                    }
                    else
                    {
                        var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                        lstResultado = clienteJson.Get<List<O_REPORTE_CALIDAD_PRINCIPAL>>("/ReportarCalidadPrincipal/" + cParametrosHydro.strCredencial +
                        "/" + 0 +
                        "/" + Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]) +
                        "/" + txtFechaInicial.Text.Replace("/", "-") +
                        "/" + txtFechaFinal.Text.Replace("/", "-") +
                        "/3?format=json");
                    }
                }
                else
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    lstResultado = clienteJson.Get<List<O_REPORTE_CALIDAD_PRINCIPAL>>("/ReportarCalidadPrincipal/" + cParametrosHydro.strCredencial +
                    "/" + Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]) +
                    "/" + Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]) +
                    "/" + txtFechaInicial.Text.Replace("/", "-") +
                    "/" + txtFechaFinal.Text.Replace("/", "-") +
                    "/3?format=json");
                }

                if (lstResultado != null && lstResultado.Count != 0)
                {
                    //if (Session[CVariablesSesion.IdEntidad] != null)
                    //{
                        //if ((decimal)Session[CVariablesSesion.IdEntidad] > 0)
                        //{
                        //    Session[CParametrosCalidad.cColListadoActividades] = _servicioHydroListado.ListadoActividades((decimal)Session[CVariablesSesion.IdEntidad], CParametrosHydro.strCredencialHydroAdmin, ref mensajehydro);

                        //    if (Session[CParametrosCalidad.cColListadoActividades] != null && ((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades]).Where(x => x.TIPO_ACTIVIDAD == "IMPORTACION DE ACEITES Y/O LUBRICANTES").FirstOrDefault() != null)
                        //    {
                        //        grdCertificadoCalidad.Columns[7].Visible = true;
                        //        grdCertificadoCalidad.Columns[6].Visible = false;
                        //    }
                        //    else
                        //    {
                        //        grdCertificadoCalidad.Columns[7].Visible = false;
                        //        grdCertificadoCalidad.Columns[6].Visible = true;
                        //    }
                        //}
                        //else
                        //{
                        //    grdCertificadoCalidad.Columns[6].Visible = true;
                        //    grdCertificadoCalidad.Columns[7].Visible = true;
                        //}
                    //}
                    //else
                    //{
                    //    grdCertificadoCalidad.Columns[6].Visible = true;
                    //    grdCertificadoCalidad.Columns[7].Visible = true;
                    //}

                    //Session[CParametrosCalidad.cColListadoActividades] = _servicioHydroListado.ListadoActividades((decimal)Session[CVariablesSesion.IdEntidad], CParametrosHydro.strCredencialHydroAdmin, ref mensajehydro);

                    //var clienteJson1 = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    //if (Session[CParametrosCalidad.cValidacionCalidad] == null && Session[CParametrosCalidad.cColListadoActividades] != null)
                    //    Session[CParametrosCalidad.cValidacionCalidad] = clienteJson1.Get<List<O_VALIDA_CALIDAD_CTY>>("/ObtenerValidacionCalidad/" + cParametrosHydro.strCredencial + "/" + ((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades])[0].TIPO_ACTIVIDAD_ID + "?format=json");
                    
                    //if (Session[CParametrosCalidad.cValidacionCalidad] != null)
                    //{
                    //    if (((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).Where(x => x.VALIDA_CALIDAD == "PERMITE_MODIFICAR").FirstOrDefault() != null)
                    //    {
                    //        grdCertificadoCalidad.Columns[1].Visible = true;
                    //    }
                    //    else
                    //    {
                    //        grdCertificadoCalidad.Columns[1].Visible = false;
                    //    }
                    //}

                    grdCertificadoCalidad.DataSource = lstResultado;
                    grdCertificadoCalidad.DataBind();
                }
                else
                {
                    grdCertificadoCalidad.DataSource = null;
                    grdCertificadoCalidad.DataBind(); 
                }

            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        private void RecuperarIdDireccion()
        {
            try
            {
                wfReporteCalidad frm = new wfReporteCalidad();
                decIdDireccion = frm.ObtenerIdDireccion();

                var columnaComandos = (GridViewDataColumn)grdCertificadoCalidad.Columns["Comandos"];
                if (columnaComandos != null)
                {
                    if (decIdDireccion == DireccionesAnh.svc_obtener_idRefinacion)
                    {
                        columnaComandos.Width = 85;
                        grdCertificadoCalidad.Columns["ComandosEdiEli"].Visible = true;
                        //columnaCapacidadMax.Caption = "druin";
                    }
                    else if (decIdDireccion == DireccionesAnh.svc_obtener_idImportadores)
                    {
                        columnaComandos.Width = 10;
                        grdCertificadoCalidad.Columns["ComandosEdiEli"].Visible = false;
                        //columnaCapacidadMax.Caption = "Dcd";
                    }
                    else
                    {
                        grdCertificadoCalidad.Columns["ComandosEdiEli"].Visible = false;
                    }
                }
                var columnaOpciones = (GridViewDataColumn)grdCertificadoCalidad.Columns["Opciones"];
                if (columnaOpciones != null)
                {
                    if (decIdDireccion == DireccionesAnh.svc_obtener_idProcGasNatEnCampo)
                    {
                        grdCertificadoCalidad.Columns["Opciones"].Visible = true;
                    }
                    else
                    {
                        grdCertificadoCalidad.Columns["Opciones"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        private void RecuperarFechas()
        {
            try
            {
                string fi = Request.Params["fi"].ToString();
                string ff = Request.Params["ff"].ToString();

                DateTime result;
                var formats = new[] { "dd/MM/yyyy" };

                if (!DateTime.TryParseExact(fi, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    _fechaCorrecta = false;
                }
                if (!DateTime.TryParseExact(ff, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    _fechaCorrecta = false;
                }

                if (_fechaCorrecta)
                {
                    txtFechaInicial.Text = fi;
                    txtFechaFinal.Text = ff;
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        private void ConfigurarFechaInicial()
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

        #endregion

    }
}