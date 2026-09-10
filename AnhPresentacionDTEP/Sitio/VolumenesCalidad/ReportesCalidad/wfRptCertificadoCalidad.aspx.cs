using System.Drawing;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroListados;
using AnhAgenteServicios.ServicioHydroSesion;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraCharts.Native;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text; 
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes;
using System.Web.Security;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad
{
    using ServiceStack.Common.Utils;
    using System.Reflection;
    using Newtonsoft.Json;

    public partial class wfRptCertificadoCalidad : System.Web.UI.Page
    {

        #region Variables

        private decimal decIdDireccion = 0;
        private bool _fechaCorrecta = true;
        string strMensajeError = "";
        string strAccion = "";
        readonly IServicioHydroListados _servicioHydroListado = LocalizadorProxy.ServicioHydroListados();
        string mensajehydro = "";
        List<O_REPORTE_CALIDAD_PRINCIPAL> lstResultado = null;
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        #endregion

        #region Eventos
        /// <summary>
        /// Metodo para controlar accesibilidad al formulario
        /// </summary>
        protected void page_Init()
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                //try
                //{
                //    if (!CConsultaAccesos.AccesoFormulario(CParametrosHydro.strCredencialHydroAdmin,
                //        Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]), Convert.ToDecimal(CParametrosHydro.decIdAplicacion), Path.GetFileName(Request.Path),
                //        ref strMensajeError))
                //    {
                //        FormsAuthentication.SignOut();
                //        Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
                //        Session.RemoveAll();
                //        Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                //    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                //}
            }
        }

        /// <summary>
        /// Cargado de datos al inicio del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                   // Button1.Enabled = false;
                    txtFechaFinal.MaxDate = System.DateTime.Now;
                    txtFechaInicial.MaxDate = System.DateTime.Now;
                    Session["observado"] = 0;
                    CCalidadLibreria.ValidarSesionUsuario(Response);

                    //string perfil = Session[CVariablesSesion.PerfilUsuario].ToString();


                    //RecuperarIdDireccion();

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


        /// <summary>
        /// Llamada interna para configurar la edicion y eliminacion de certificados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCertificadoCalidad_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID == "Editar")
                {
                    var row = (O_REPORTE_CALIDAD_PRINCIPAL)grdCertificadoCalidad.GetRow(e.VisibleIndex);
                    Session["citeGenerado"] = row.CITE_GENERADO.ToString();
                    Session["tipoRegistro"] = 2;
                    Session["conteo"] = 0;
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(
                        "~/Sitio/VolumenesCalidad/GestionCalidad/wfGestionCertificadoCalidad.aspx");
                   
                }
                else if (e.ButtonID == "Eliminar")
                {
                    var row = (O_REPORTE_CALIDAD_PRINCIPAL)grdCertificadoCalidad.GetRow(e.VisibleIndex);

                    Session["citeGenerado"] = row.CITE_GENERADO.ToString();
                    Session["tipoRegistro"] = 3;
                    Session["conteo"] = 0;
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(
                        "~/Sitio/VolumenesCalidad/GestionCalidad/wfGestionCertificadoCalidad.aspx");
                }
                else if (e.ButtonID == "EliminaDirecto")
                {
                    var row = (O_REPORTE_CALIDAD_PRINCIPAL)grdCertificadoCalidad.GetRow(e.VisibleIndex);
                    if (row != null)
                    {
                        var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>(
                            "/GestionVolumen/?format=json",
                            new
                                {
                                    strLlave = cParametrosHydro.strCredencial,
                                    decIdPruebaCalidad = row.ID_REGISTROCAL_PRINCIPAL,
                                    decIdEntidad = row.ID_ENTIDAD,
                                    decIdTipoActividad = row.ID_TIPO_ACTIVIDAD,
                                    decFechaImportacion=0,
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]),
                                    decAccion = 3 //1=registro/3=elimina
                                });
                    }
                }
                
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Llamada interna para configurar parametros de eliminacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCertificadoCalidad_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.Parameters);
                var row = (O_REPORTE_CALIDAD_PRINCIPAL)grdCertificadoCalidad.GetRow(index);
                if (row.CITE_GENERADO != null && row.VOLUMEN_OP_DEBE > 0)
                {
                    Session["citeGenerado"] = row.CITE_GENERADO.ToString();
                    Session["tipoRegistro"] = 3;
                    Session["conteo"] = 0;
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

        /// <summary>
        /// Exportacion de datos de grila a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Exportacion de datos de grilla pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Exportacion de datos de grilla a word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #endregion

        #region Metodos
        /// <summary>
        /// Metodo para generar le certificado de calidad segun la actividad
        /// </summary>
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
                        "/1?format=json");
                    }
                    else
                    {
                        var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                        lstResultado = clienteJson.Get<List<O_REPORTE_CALIDAD_PRINCIPAL>>("/ReportarCalidadPrincipal/" + cParametrosHydro.strCredencial +
                        "/" + 0 +
                        "/" + Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]) +
                        "/" + txtFechaInicial.Text.Replace("/", "-") +
                        "/" + txtFechaFinal.Text.Replace("/", "-") +
                        "/1?format=json");
                    }
                }
                else
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    lstResultado = clienteJson.Get<List<O_REPORTE_CALIDAD_PRINCIPAL>>("/ReportarCalidadPrincipal/" + cParametrosHydro.strCredencial +
                    "/" + -1 +
                    "/" + Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]) +
                    "/" + txtFechaInicial.Text.Replace("/", "-") +
                    "/" + txtFechaFinal.Text.Replace("/", "-") +
                    "/1?format=json");
                }

                if (lstResultado != null && lstResultado.Count != 0)
                {
                    if (Session[CVariablesSesion.IdEntidad] != null)
                    {
                        //if (Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]) > 0)
                        if (!Convert.ToBoolean(Session[CVariablesSesion.IsFuncionario]))
                        {
                            Session[CParametrosCalidad.cColListadoActividades] = _servicioHydroListado.ListadoActividades(Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]), CParametrosHydro.strCredencialHydroAdmin, ref mensajehydro);

                            if (Session[CParametrosCalidad.cColListadoActividades] != null && ((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades]).FirstOrDefault(x => x.TIPO_ACTIVIDAD == "IMPORTACION DE ACEITES Y/O LUBRICANTES") != null)
                            {
                                grdCertificadoCalidad.Columns["PUNTO_CUSTODIO"].Visible = true;
                                grdCertificadoCalidad.Columns["OBSERVACIONES"].Visible = true;
                            }
                            else
                            {
                                grdCertificadoCalidad.Columns["PUNTO_CUSTODIO"].Visible = true;
                                grdCertificadoCalidad.Columns["OBSERVACIONES"].Visible = false;
                            }

                            //Verifico si puedo modificar como DT&P
                            Session[CParametrosCalidad.cColListadoActividades] = _servicioHydroListado.ListadoActividades(Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]), CParametrosHydro.strCredencialHydroAdmin, ref mensajehydro);

                            var clienteJson1 = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                            if (Session[CParametrosCalidad.cValidacionCalidad] == null && Session[CParametrosCalidad.cColListadoActividades] != null)
                                Session[CParametrosCalidad.cValidacionCalidad] = clienteJson1.Get<List<O_VALIDA_CALIDAD_CTY>>("/ObtenerValidacionCalidad/" + cParametrosHydro.strCredencial + "/" + ((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades])[0].TIPO_ACTIVIDAD_ID + "/" + Session[CVariablesSesion.UsuarioId] + "?format=json");

                            if (Session[CParametrosCalidad.cValidacionCalidad] != null)
                            {
                                if (((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "PERMITE_MODIFICAR") != null || Convert.ToBoolean(Session[CVariablesSesion.IsFuncionario]))
                                {
                                    grdCertificadoCalidad.Columns["ComandosEdiEli"].Visible = true;
                                }
                                else
                                {
                                    grdCertificadoCalidad.Columns["ComandosEdiEli"].Visible = false;
                                }
                            }

                            grdCertificadoCalidad.DataSource = lstResultado;
                            grdCertificadoCalidad.DataBind();                        
                        }
                        else
                        {
                            decimal pdf = 0;
                            foreach (var vEntidad in lstResultado.GroupBy(p => p.ID_ENTIDAD).Select(g => g.First()).ToList())
                            {
                                Session[CParametrosCalidad.cColListadoActividades] = _servicioHydroListado.ListadoActividades(vEntidad.ID_ENTIDAD, CParametrosHydro.strCredencialHydroAdmin, ref mensajehydro);
                                var clienteJson1 = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                                Session[CParametrosCalidad.cValidacionCalidad] = null;
                                if (Session[CParametrosCalidad.cValidacionCalidad] == null && Session[CParametrosCalidad.cColListadoActividades] != null)
                                    Session[CParametrosCalidad.cValidacionCalidad] =
                                        clienteJson1.Get<List<O_VALIDA_CALIDAD_CTY>>("/ObtenerValidacionCalidad/" + cParametrosHydro.strCredencial + "/" + ((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades])[0].TIPO_ACTIVIDAD_ID + "/" + Session[CVariablesSesion.UsuarioId] + "?format=json");
                                if ((((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "ADJUNTA_DOCUMENTO_CTR") != null) || Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]))
                                {
                                    // grdCertificadoCalidad.Columns[12].Visible = true;
                                    grdCertificadoCalidad.Columns["Ver"].Visible = true;
                                    pdf = 1;
                                }
                                {
                                    if (pdf == 0)
                                    {
                                        //   grdCertificadoCalidad.Columns[12].Visible = false;
                                        grdCertificadoCalidad.Columns["Ver"].Visible = false;
                                    }
                                }

                            }

                            grdCertificadoCalidad.Columns["ComandosEdiEli"].Visible = true;
                            //grdCertificadoCalidad.Columns["ComandosDetalle"].Visible = true;
                            grdCertificadoCalidad.Columns["PUNTO_CUSTODIO"].Visible = true;
                            grdCertificadoCalidad.Columns["OBSERVACIONES"].Visible = true;

                            grdCertificadoCalidad.DataSource = lstResultado;
                            grdCertificadoCalidad.DataBind();
                        }
                    }
                    else
                    {
                        grdCertificadoCalidad.Columns["Certificado"].Visible = true;

                        Session[CParametrosCalidad.cValidacionCalidad] = clienteJson.Get<List<O_VALIDA_CALIDAD_CTY>>("/ObtenerValidacionCalidad/" + cParametrosHydro.strCredencial + "/" + 0 + "/" + Session[CVariablesSesion.UsuarioId] + "?format=json");

                        if (Session[CParametrosCalidad.cValidacionCalidad] != null)
                        {
                            if (((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "ADJUNTA_DOCUMENTO_CTR") != null)
                            {
                                grdCertificadoCalidad.Columns["Ver"].Visible = true;
                            }
                            else
                            {
                                if (Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]))
                                {
                                    grdCertificadoCalidad.Columns["Ver"].Visible = true;
                                }
                                else
                                {
                                    //var fecha = lstResultado[''];
                                    grdCertificadoCalidad.Columns["Ver"].Visible = false;    
                                }
                            }
                        }

                        grdCertificadoCalidad.DataSource = lstResultado;
                        grdCertificadoCalidad.DataBind();
                    }


                    //if (Convert.ToDecimal(Session[CVariablesSesion.UsuarioAdministrador]) == 1 || Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador])) //SuperAdmin
                    //{
                    //    grdCertificadoCalidad.DataSource = lstResultado;
                    //    grdCertificadoCalidad.DataBind();
                    //}
                    //else //Admin 
                    //{
                    //    //Session[CParametrosCalidad.cColListadoActividades] = _servicioHydroListado.ListadoActividades((decimal)Session[CVariablesSesion.IdEntidad], CParametrosHydro.strCredencialHydroAdmin, ref mensajehydro);

                    //    //var clienteJson1 = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    //    //if (Session[CParametrosCalidad.cValidacionCalidad] == null && Session[CParametrosCalidad.cColListadoActividades] != null)
                    //    //    Session[CParametrosCalidad.cValidacionCalidad] = clienteJson1.Get<List<O_VALIDA_CALIDAD_CTY>>("/ObtenerValidacionCalidad/" + cParametrosHydro.strCredencial + "/" + ((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades])[0].TIPO_ACTIVIDAD_ID + "?format=json");

                    //    //if (Session[CParametrosCalidad.cValidacionCalidad] != null)
                    //    //{
                    //    //    if (((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).Where(x => x.VALIDA_CALIDAD == "PERMITE_MODIFICAR").FirstOrDefault() != null)
                    //    //    {
                    //            grdCertificadoCalidad.Columns[1].Visible = true;
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        grdCertificadoCalidad.Columns[1].Visible = false;
                    //    //    }
                    //    //}
                    //    grdCertificadoCalidad.DataSource = lstResultado;
                    //    grdCertificadoCalidad.DataBind();
                    //}
                }
                else
                {
                    grdCertificadoCalidad.DataSource = new List<O_REPORTE_CALIDAD_PRINCIPAL>();
                    grdCertificadoCalidad.DataBind();
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Metodo para obtener la direccion de dependencia del reporte sue se solicita
        /// </summary>
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
                        if (Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]))
                        {
                            columnaComandos.Width = 10;
                            grdCertificadoCalidad.Columns["ComandosEdiEli"].Visible = true;
                            //columnaCapacidadMax.Caption = "Dcd";
                        }
                        else
                        {
                            grdCertificadoCalidad.Columns["ComandosEdiEli"].Visible = false;    
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Metodo que da formato a los parametros de fechas para generar el listado de reporte
        /// </summary>
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

        /// <summary>
        /// Metodo que configura la fecha inicial por defecto del listado de reportes
        /// </summary>
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

        protected void grdCertificadoCalidad_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;
            var dato = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "VOLUMEN_OP_DEBE");
            if (e.ButtonID == "Editar")
            {
                //var dato = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "VOLUMEN_OP_DEBE");
                if (dato != null)
                {
                    if (dato.ToString() == "0")
                    {
                        e.Visible = DefaultBoolean.False;
                    }
                }
                else { e.Visible = DefaultBoolean.False; }
            }
            if (e.ButtonID == "EliminaDirecto")
            {
                //var dato = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "VOLUMEN_OP_DEBE");
                if (dato != null)
                {
                    if (dato.ToString() == "0")
                        e.Visible = DefaultBoolean.True;
                }
                else { e.Visible = DefaultBoolean.True; }
            }
            if (e.ButtonID == "Eliminar")
            {
                //var dato = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "VOLUMEN_OP_DEBE");
                if (dato != null)
                {
                    if (dato.ToString() == "0")
                        e.Visible = DefaultBoolean.False;
                }
                else { e.Visible = DefaultBoolean.False;}
            }
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {

         
        }

      
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            //dgDatosExcel.DataSource = null;
            //dgDatosExcel.DataBind();

            //var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

            //var objResultado = clienteJson.Post<List<O_REPORTE_CALIDAD_REG_ADM>>("/ReportarCalidadPrincipalExcel/",
            //               new
            //               {
            //                   strLlave = cParametrosHydro.strCredencial,
            //                   decIdEntidad = 0,
            //                   decIdUsuarioANH = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]),
            //                   strFechaInicial = txtFechaInicial.Text.Replace("/", "-"),
            //                   strFechaFinal = txtFechaFinal.Text.Replace("/", "-"),
            //                   decEstado = 1

            //               });

            //dgDatosExcel.DataSource = objResultado;
            //dgDatosExcel.DataBind();
            ////GridView1.Visible = false;
            ////para la habilitacion del boton de descarga documento excel
            //if (dgDatosExcel.Rows.Count != 0)
            //{
            //    btnExportar.Enabled = true;
            //    btnExportar.Text = "Descargar documento de excel";
            //    //LabelErrorExcel.Text = "si hay datos!!!";
            //    //LabelErrorExcel.Visible = true;

            //}
            //else
            //{
            //    btnExportar.Text = "No es posible desacargar el documento de excel";
            //    btnExportar.Enabled = false;
            //  //  LabelErrorExcel.Visible = true;
            //}    
		
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=archivo.xls");
            //Response.Charset = "";
            //System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //dgDatosExcel.RenderControl(hw);
            //Response.Write(tw.ToString());
            //Response.End();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (chkMostrarColumnas == null) return;
            bool mostrarColumas = chkMostrarColumnas.Checked;
            foreach (GridViewColumn col in grdCertificadoCalidad.Columns)
            {
                if (col.Name == "NOMBRE_PRODUCTO_COM" || col.Name == "EMPRESA_PROVEEDORA" ||
                    col.Name == "RUTA_INTERNACION" || col.Name == "OBS_DOCUMENTO" || col.Name == "FECHA_RA" ||
                    col.Name == "TK_ORIG_EXTERNO" || col.Name == "NRO_LOTE_VERIF")
                {
                    col.Visible = mostrarColumas;
                }
            }
        }

    }
}