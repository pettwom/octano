using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AnhAgenteServicios.ServicioHydroSesion;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroListados;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad;
using AnhServicioWebOctano.ServiciosWeb.Gestion;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraCharts.Web;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.PivotGrid;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using O_RESULTADO_CTY = AnhPersistenciaCore.Core.O_RESULTADO_CTY;
using Page = System.Web.UI.Page;
using PivotCustomChartDataSourceDataEventArgs = DevExpress.Web.ASPxPivotGrid.PivotCustomChartDataSourceDataEventArgs;
using PivotGridField = DevExpress.Web.ASPxPivotGrid.PivotGridField;
using AnhPresentacionDTEP.Entidades;
using AnhPersistenciaCore.Entidades;
using AnhPersistenciaCore.Core;
using Librerias.Anh.Us;
using System.Web.Security;


namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionVolumenes
{
    public partial class WfVolumenesGLP : System.Web.UI.Page
    {

        #region Clases y variables de entorno
        #region Corrientes Volumenes
        public class EResultadoInsersionVolumenes
        {
            public decimal decCodigo { get; set; }
            public string strMensaje { get; set; }
            public object oResultado { get; set; }
        }
        public class ERegistrarCorrientes : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decVersion { get; set; }
            public decimal decCodigoProyecto { get; set; }
            public List<ObjetoCorriente> vListCorriente { get; set; }
            public decimal decPlanta { get; set; }
            public decimal decCorrienteCampo { get; set; }
        }

        public class ObjetoCorriente : EtanoEspecificacion
        {
            public decimal decContenidoLicuables { get; set; }
        }


        #endregion

        #region Gas Residual
        public class EObjetoGasResidual : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decVersion { get; set; }
            public decimal decCodigoProyecto { get; set; }
            public List<ObjetoGasResidual> vListGasResidual { get; set; }
            public decimal decPlanta { get; set; }
        }

        public class ObjetoGasResidual : EtanoEspecificacion
        {
            public decimal decH2O { get; set; }
            public decimal decPuntoRosio { get; set; }
        }
        #endregion

        public class EtanoEspecificacion : PentanoEspecificacion
        {
            public decimal decVolumen { get; set; }
            public decimal decPoderCalorifico { get; set; }
            public decimal decN2 { get; set; }
            public decimal decCO2 { get; set; }
            public decimal decC1 { get; set; }
            public decimal decN_C6 { get; set; }
            public decimal decC7 { get; set; }
        }

        public class PentanoEspecificacion
        {
            public decimal decGravedadEspecifica { get; set; }
            public decimal decFecha { get; set; }
            public decimal decC2 { get; set; }
            public decimal decC3 { get; set; }
            public decimal decI_C4 { get; set; }
            public decimal decN_C4 { get; set; }
            public decimal decI_C5 { get; set; }
            public decimal decN_C5 { get; set; }
            public string strObservaciones { get; set; }
            public string strJustificacion { get; set; }
        }
        #endregion

        #region variables
        static string mensajeError1 = "";
        static decimal? resultado1 = 0;
        string strMensajeError = "";
        string strAccion = "";

        //private readonly IServicioCantidadesListado _servicioCantidadesListado =
        //    LocalizadorProxy.ServiciosWebCantidadesListado();

        private const string StrNamespace = "GestionVolumenes";
        private const string StrPgReporte = "pgReporte";
        private const string StrIdEntidad = "decIdEntidad";
        private const string StrFechaIni = "txtFechaIni";
        private const string StrFechaFin = "txtFechaFin";

        private const string StrPgRptSeguimiento = "pgRptSeguimiento";
        private const string StrIdEntidadSeg = "decIdEntidad";
        private const string StrFechaIniSeg = "txtFechaIni";
        private const string StrFechaFinSeg = "txtFechaFin";

        private const string StrPgRptProde = "pgRptProde";
        private const string StrIdEntidadProde = "decIdEntidad";
        private const string StrFechaIniProde = "txtFechaIni";
        private const string StrFechaFinProde = "txtFechaFin";
        private const string StrIdUnidadMedida = "decIdUnidadMedida";
        private const string StrUnMedReporte = "StrUnMedReporte";
        private const string StrUnMedProde = "StrUnMedProde";

        #endregion

        private decimal _decIdEntidad = -1;
        private static decimal _decIdTipoActividad = -1;

        #region Eventos

        static ERegistrarCorrientes objRegistroCorrientes = new ERegistrarCorrientes();
        static ObjetoCorriente vObjCorrienteInt = null;
        static EObjetoGasResidual objRegistroGasResidual = new EObjetoGasResidual();
        static ObjetoGasResidual vObjetoGasResidualInt = null;

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
                        Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]), Convert.ToDecimal(CParametrosHydro.decIdAplicacion), Path.GetFileName(Request.Path),
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

                    _decIdTipoActividad = Convert.ToDecimal(Session[CVariablesSesion.IdTipoActividad]);
                    divEntidad.Visible = false;
                    ddlEntidadOperadora.Visible = false;

                    objRegistroCorrientes.vListCorriente = new List<ObjetoCorriente>();
                    objRegistroGasResidual.vListGasResidual = new List<ObjetoGasResidual>();

                    if (Session[CVariablesSesion.PerfilUsuario] != null)
                    {
                        foreach (O_PERFILES_USUARIO_CTY perfil in (IEnumerable<O_PERFILES_USUARIO_CTY>)Session[CVariablesSesion.PerfilUsuario])
                        {
                            if (perfil.DESCRIPCION == "DTYP_PRODE ADMINISTRADOR" || perfil.DESCRIPCION == "DTYP_PRODE ADMINISTRADOR V2")
                            {
                                Session[CVariablesSesion.IsAdminDteyp] = true;
                            }
                        }
                    }

                    if (Session[CVariablesSesion.IsAdminDteyp] != null && !(bool)Session[CVariablesSesion.IsAdminDteyp])
                    {
                        pcOpciones.TabPages[4].Visible = false;
                        pcOpciones.TabPages[5].Visible = false;

                        pcOpciones.ActiveTabIndex = 0;
                    }
                    else
                    {
                        pcOpciones.TabPages[0].Visible = false;
                        pcOpciones.TabPages[1].Visible = false;
                        pcOpciones.TabPages[2].Visible = false;
                        pcOpciones.TabPages[4].Visible = false;
                        pcOpciones.TabPages[5].Visible = false;
                        pcOpciones.ActiveTabIndex = 0;
                    }


                    if (!IsPostBack)
                    {
                        Session.Remove(StrNamespace + StrFechaIniSeg);
                        Session.Remove(StrNamespace + StrFechaFinSeg);
                        Session.Remove(StrNamespace + StrIdEntidadSeg);
                        Session.Remove(StrNamespace + StrFechaIniProde);
                        Session.Remove(StrNamespace + StrFechaFinProde);
                        Session.Remove(StrNamespace + StrIdEntidadProde);
                        Session.Remove(StrNamespace + StrIdUnidadMedida);
                        Session.Remove(StrNamespace + StrFechaIni);
                        Session.Remove(StrNamespace + StrFechaFin);
                        Session.Remove(StrNamespace + StrIdEntidad);

                        LlenarUnidadesMedidaVolumen(ddlUnidadMedGLP, ddlUnidadMedGLP.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlUnidadMedPoderCalor, ddlUnidadMedPoderCalor.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlUnidadMedVolumen);
                        LlenarUnidadesMedidaVolumen(ddlSaldoGlpUnidadMedidad);
                        LlenarUnidadesMedidaVolumen(ddlSaldoPropanoUnidadMedidad);
                        LlenarUnidadesMedidaVolumen(ddlTemperatura, ddlTemperatura.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlResidualPoderCalorifico, ddlResidualPoderCalorifico.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlResidualPuntoRocio, ddlResidualPuntoRocio.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlResidualVolUnidadMedida);
                        // Nuevos
                        LlenarUnidadesMedidaVolumen(ddlUnidadMedidaDestinoReportesVolumen);
                        LlenarUnidadesMedidaVolumen(ddlUnidadMedidaDestinoReportesPeso);
                        LlenarUnidadesMedidaVolumen(ddlProdGlpUnidadMedidad);
                        LlenarUnidadesMedidaVolumen(ddlProdPropanoUnidadMedidad, ddlProdPropanoUnidadMedidad.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlEntregaPropanoUnidadMedidad, ddlEntregaPropanoUnidadMedidad.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlEntregaGlpCisternaUnidadMedidad, ddlEntregaGlpCisternaUnidadMedidad.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlEntregaGlpDuctoUnidadMedidad, ddlEntregaGlpDuctoUnidadMedidad.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlRendProdGlp, ddlRendProdGlp.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlGasCombustible, ddlGasCombustible.Attributes["umed"]);
                        LlenarUnidadesMedidaVolumen(ddlQuemaGas);
                        LlenarUnidadesMedidaVolumen(ddlGasolinaNatural, ddlGasolinaNatural.Attributes["umed"]);

                        txtFechaIni.Text =
                            txtFechaIniSeg.Text =
                            txtFechaIniProde.Text = DateTime.Now.ToString("01/MM/yyyy");
                        txtFechaFin.Text =
                            txtFechaFinSeg.Text =
                            txtFechaFinProde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtFechaIni.MaxDate =
                            txtFechaIniSeg.MaxDate =
                            txtFechaIniProde.MaxDate = DateTime.Now;
                        txtFechaFin.MinDate =
                            txtFechaFinSeg.MinDate =
                            txtFechaFinProde.MinDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));

                        SetChartType("Line", chartRptProde);
                        SetChartType("Line", chartRptSeguimiento);
                        LlenarComboGraficos();
                        LlenarUnidadesMedida();

                        pgReporte.DataSource = new List<O_REPORTE_VOL_DTEP>();
                        pgReporte.DataBind();
                    }
                    #region llenar los datos de los reportes

                    LLenarPlantas();

                    try
                    {
                        if (Session[StrNamespace + StrFechaIni] != null &&
                            Session[StrNamespace + StrFechaFin] != null &&
                            Session[StrNamespace + StrIdEntidad] != null)
                        {
                            ReporteDetalle(Convert.ToDecimal(Session[StrNamespace + StrIdEntidad]),
                                           Session[StrNamespace + StrFechaIni].ToString(),
                                           Session[StrNamespace + StrFechaFin].ToString());
                        }
                    }
                    catch
                    { }
                    try
                    {
                        if (Session[StrNamespace + StrFechaIniSeg] != null &&
                               Session[StrNamespace + StrFechaFinSeg] != null &&
                               Session[StrNamespace + StrIdEntidadSeg] != null)
                        {
                            //ReporteSeguimiento(Convert.ToDecimal(Session[StrNamespace + StrIdEntidadSeg]),Session[StrNamespace + StrFechaIniSeg].ToString(),Session[StrNamespace + StrFechaFinSeg].ToString());
                        }
                    }
                    catch { }
                    try
                    {
                        if (Session[StrNamespace + StrFechaIniProde] != null &&
                                Session[StrNamespace + StrFechaFinProde] != null &&
                                Session[StrNamespace + StrIdEntidadProde] != null &&
                                Session[StrNamespace + StrIdUnidadMedida] != null)
                        {
                            //ReporteProde(Session[StrNamespace + StrIdEntidadProde].ToString(),Session[StrNamespace + StrFechaIniProde].ToString(),Session[StrNamespace + StrFechaFinProde].ToString(),Convert.ToDecimal(Session[StrNamespace + StrIdUnidadMedida]));
                        }
                    }
                    catch { }

                    #endregion
                    if (IsCallback)
                    {
                        ddlCorriente.Items.Clear();
                        ddlCorriente.Items.Add(new ListItem("-- Primero seleccionar una planta --", ""));
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
        /// borra las variables de sesion para las pestañas que no estan activas
        /// </summary>
        /// <param name="opcion"></param>
        protected void HabilitarOpciones()
        {
            Session.Remove(StrNamespace + StrFechaIniSeg);
            Session.Remove(StrNamespace + StrFechaFinSeg);
            Session.Remove(StrNamespace + StrIdEntidadSeg);
            Session.Remove(StrNamespace + StrFechaIniProde);
            Session.Remove(StrNamespace + StrFechaFinProde);
            Session.Remove(StrNamespace + StrIdEntidadProde);
            Session.Remove(StrNamespace + StrIdUnidadMedida);
            Session.Remove(StrNamespace + StrFechaIni);
            Session.Remove(StrNamespace + StrFechaFin);
            Session.Remove(StrNamespace + StrIdEntidad);
        }

        #region reporte volúmenes registrados

        protected void lnkPdf_Click(object sender, EventArgs e)
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
                    ExportarReporte(sender, e, "PDF");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void lnkExcel_Click(object sender, EventArgs e)
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
                    ExportarReporte(sender, e, "XLS");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void lnkWord_Click(object sender, EventArgs e)
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
                    ExportarReporte(sender, e, "DOC");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void pgReporte_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
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
                    HabilitarOpciones();
                    String[] parametros = e.Parameters.Split('|');
                    ReporteDetalle(Convert.ToDecimal(parametros[0].PadLeft(1, '0')), parametros[1], parametros[2]);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        #endregion

        #region seguimiento a la produccion

        protected void lnkPdfSeg_Click(object sender, EventArgs e)
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
                    ExportarReporteSeg(sender, e, "PDF");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void lnkExcelSeg_Click(object sender, EventArgs e)
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
                    ExportarReporteSeg(sender, e, "XLS");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void lnkWordSeg_Click(object sender, EventArgs e)
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
                    ExportarReporteSeg(sender, e, "DOC");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void pgRptSeguimiento_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
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
                    HabilitarOpciones();
                    String[] parametros = e.Parameters.Split('|');
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        #region Grafica del PivotGrid

        protected void chartRptSeguimiento_CustomCallback(object sender, CustomCallbackEventArgs e)
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
                    if (e.Parameter == "GridChanged")
                    {
                        chartRptSeguimiento.DataBind();
                    }
                    else if (e.Parameter.ToUpper().Contains("CHARTCHANGED"))
                    {
                        SetChartType(e.Parameter.Split('|')[1], chartRptSeguimiento);

                        Session[StrUnMedReporte] = e.Parameter.Split('|')[2];
                        FiltrarDatosGraficoSeguimiento();
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void pgRptSeguimiento_CustomChartDataSourceData(object sender, PivotCustomChartDataSourceDataEventArgs e)
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
                    if (e.ItemType == DevExpress.XtraPivotGrid.PivotChartItemType.CellItem)
                    {
                        if (e.Value == DBNull.Value || (decimal)e.Value < Convert.ToDecimal(0))
                            e.Value = 0;
                    }
                    if (e.ItemType == DevExpress.XtraPivotGrid.PivotChartItemType.RowItem)
                    {
                        bool isCategoryField = object.Equals(e.FieldValueInfo.Field, rptTIPO);
                        switch (RowExportRule)
                        {
                            case RowFieldValueExportRule.tipoOperacion:
                                if (isCategoryField)
                                    e.Value = e.FieldValueInfo.Value + " Category";
                                else
                                    e.Value = e.FieldValueInfo.Value;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }


        }

        #endregion

        #endregion

        #region seguimiento al PRODE

        protected void lnkPdfProde_Click(object sender, EventArgs e)
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
                    ExportarProde(sender, e, "PDF");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void lnkExcelProde_Click(object sender, EventArgs e)
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
                    ExportarProde(sender, e, "XLS");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void lnkWordProde_Click(object sender, EventArgs e)
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
                    ExportarProde(sender, e, "DOC");
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void pgRptProde_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
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
                    HabilitarOpciones();
                    String[] parametros = e.Parameters.Split('|');
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        #region Grafica del PivotGrid

        protected void chartRptProde_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
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
                    if (e.Parameter == "GridChanged")
                        chartRptProde.DataBind();
                    else if (e.Parameter.ToUpper().Contains("CHARTCHANGED"))
                    {
                        SetChartType(e.Parameter.Split('|')[1], chartRptProde);
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void pgRptProde_CustomChartDataSourceData(object sender, PivotCustomChartDataSourceDataEventArgs e)
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
                    if (e.ItemType == PivotChartItemType.CellItem)
                    {
                        if (e.Value == DBNull.Value || (decimal)e.Value < Convert.ToDecimal(0))
                            e.Value = 0;
                    }
                    if (e.ItemType == PivotChartItemType.RowItem)
                    {
                        bool isCategoryField = object.Equals(e.FieldValueInfo.Field, rptTIPO);
                        switch (RowExportRule)
                        {
                            case RowFieldValueExportRule.tipoOperacion:
                                if (isCategoryField)
                                    e.Value = e.FieldValueInfo.Value + " Category";
                                else
                                    e.Value = e.FieldValueInfo.Value;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }


        }

        #endregion

        #endregion

        #endregion

        #region Metodos

        #region Graficos

        /// <summary>
        /// Llena la lista de los tipos de gráficos deponibles para la gráfica
        /// </summary>
        public void LlenarComboGraficos()
        {
            try
            {
                ddlTipoGrafico.Items.Clear();
                ddlTipoGrafico.Items.Add(new ListItem("Lineas", ViewType.Line.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("Barras", ViewType.Bar.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("StackedBar", ViewType.StackedBar.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("FullStackedBar", ViewType.FullStackedBar.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("SideBySideStackedBar", ViewType.SideBySideStackedBar.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("SideBySideFullStackedBar", ViewType.SideBySideFullStackedBar.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("Puntos", ViewType.Point.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("StepLine", ViewType.StepLine.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("Spline", ViewType.Spline.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("Area", ViewType.Area.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("SplineArea", ViewType.SplineArea.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("StackedArea", ViewType.StackedArea.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("StackedSplineArea", ViewType.StackedSplineArea.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("FullStackedArea", ViewType.FullStackedArea.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("FullStackedSplineArea", ViewType.FullStackedSplineArea.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("Rango Area", ViewType.RangeArea.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("Rango Area 3D", ViewType.RangeArea3D.ToString()));
                ddlTipoGrafico.Items.Add(new ListItem("RadarLine", ViewType.RadarLine.ToString()));

                List<ListItem> listCopy = ddlTipoGrafico.Items.Cast<ListItem>().ToList();
                ddlTipoGrafico.Items.Clear();
                ddlTipoGraficoProde.Items.Clear();
                foreach (ListItem item in listCopy.OrderBy(item => item.Text))
                {
                    if (item.Text == "Spline") item.Selected = true;
                    ddlTipoGrafico.Items.Add(item);
                    ddlTipoGraficoProde.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Cambia el tipo de grafico según el parámetro enviado
        /// </summary>
        /// <param name="text">nombre del nuevo tipo de gráfico</param>
        /// <param name="chartControl">control donde se muestra el grafico</param>
        public void SetChartType(string text, WebChartControl chartControl)
        {
            try
            {
                chartControl.SeriesTemplate.ChangeView((ViewType)Enum.Parse(typeof(ViewType), text));
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        private enum RowFieldValueExportRule
        {
            tipoOperacion = 0
        };

        private RowFieldValueExportRule RowExportRule
        {
            get { return 0; }
        }

        #endregion

        #region cargar datos para edicion

        /// <summary>
        /// Llenar un combo con la unidad de medida correspondiente
        /// </summary>
        /// <param name="ddl">DropDownList en el cual se llenara la lista de unidades</param>
        /// <param name="unidadSeleccionada">codigo de la unidad de medida seleccionada para buscar en la lista de unidades</param>
        protected void LlenarUnidadesMedidaVolumen(DropDownList ddl, string unidadSeleccionada)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var mensajeError = String.Empty;
                List<O_UNIDADES_MEDIDA_GRAL_CTY> lstResultado;

                #region Cache de datos para el combo

                if (Session["lstResultado"] == null)
                {
                    //lstResultado =_servicioListadosCalidad.ListarUnidadesMedidaGeneral(Parametros.VolumenesCalidad.cParametrosHydro.strCredencial,ref mensajeError);
                    lstResultado = clienteJson.Get<List<O_UNIDADES_MEDIDA_GRAL_CTY>>("/ListarUnidadesMedidaGeneral/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "?format=json");
                    Session.Add("lstResultado", lstResultado);
                }
                else
                    lstResultado = (List<O_UNIDADES_MEDIDA_GRAL_CTY>)Session["lstResultado"];

                #endregion

                lstResultado = (from tv in lstResultado
                                where tv.CODIGO.ToUpper().Equals(unidadSeleccionada.ToUpper())
                                select tv).ToList();

                ddl.DataSource = (from obj in lstResultado
                                  select new
                                  {
                                      ID_UNIDAD_MEDIDA = obj.ID_UNIDAD_MEDIDA,
                                      CODIGO = obj.CODIGO.ToUpper()
                                  }).ToList();
                ddl.DataValueField = "ID_UNIDAD_MEDIDA";
                ddl.DataTextField = "CODIGO";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void LlenarUnidadesMedidaVolumen(DropDownList ddl)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var mensajeError = String.Empty;
                List<O_UNIDADES_MEDIDA_GRAL_CTY> lstResultado;

                #region Cache de datos para el combo

                if (Session["lstResultado"] == null)
                {
                    //lstResultado =_servicioListadosCalidad.ListarUnidadesMedidaGeneral(Parametros.VolumenesCalidad.cParametrosHydro.strCredencial,ref mensajeError);
                    lstResultado = clienteJson.Get<List<O_UNIDADES_MEDIDA_GRAL_CTY>>("/ListarUnidadesMedidaGeneral/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "?format=json");
                    Session.Add("lstResultado", lstResultado);
                }
                else
                    lstResultado = (List<O_UNIDADES_MEDIDA_GRAL_CTY>)Session["lstResultado"];

                #endregion

                var selected = (from i in ddl.Items.Cast<ListItem>() select new { CODIGO = i.Text }).ToList();

                lstResultado = (from tv in lstResultado
                                join sel in selected on tv.CODIGO equals sel.CODIGO
                                select tv).ToList();

                ddl.DataSource = (from obj in lstResultado
                                  select new
                                  {
                                      ID_UNIDAD_MEDIDA = obj.ID_UNIDAD_MEDIDA,
                                      CODIGO = obj.CODIGO.ToUpper()
                                  }).ToList().OrderBy(co => co.CODIGO);
                ddl.DataValueField = "ID_UNIDAD_MEDIDA";
                ddl.DataTextField = "CODIGO";
                ddl.DataBind();

                switch (ddl.ID)
                {
                    case "ddlUnidadMedVolumen":
                        ddl.SelectedIndex = 3;
                        break;
                    case "ddlResidualVolUnidadMedida":
                        ddl.SelectedIndex = 3;
                        break;
                    case "ddlSaldoGlpUnidadMedidad":
                        ddl.SelectedIndex = 1;
                        break;
                    case "ddlSaldoPropanoUnidadMedidad":
                        ddl.SelectedIndex = 1;
                        break;
                    // nuevos
                    case "ddlUnidadMedidaDestinoReportesVolumen":
                        ddl.SelectedIndex = 3;
                        break;
                    case "ddlUnidadMedidaDestinoReportesPeso":
                        ddl.SelectedIndex = 1;
                        break;
                    // alter
                    case "ddlUnidadMedidaProde":
                        ddl.SelectedIndex = 5;
                        break;
                    //nuevos para gas produccion
                    case "ddlProdGlpUnidadMedidad":
                        ddl.SelectedIndex = 3;
                        break;
                    case "ddlProdPropanoUnidadMedidad":
                        ddl.SelectedIndex = 3;
                        break;
                    case "ddlEntregaPropanoUnidadMedidad":
                        ddl.SelectedIndex = 3;
                        break;
                    case "ddlEntregaGlpCisternaUnidadMedidad":
                        ddl.SelectedIndex = 3;
                        break;
                    case "ddlEntregaGlpDuctoUnidadMedidad":
                        ddl.SelectedIndex = 3;
                        break;

                    case "ddlRendProdGlp":
                        ddl.SelectedIndex = 2;
                        break;
                    case "ddlGasCombustible":
                        ddl.SelectedIndex = 2;
                        break;
                    case "ddlQuemaGas":
                        ddl.SelectedIndex = 3;
                        break;
                    case "ddlGasolinaNatural":
                        ddl.SelectedIndex = 2;
                        break;
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Buscar el nombre de una planta
        /// </summary>
        /// <param name="entidad">nombre de la entidad a buscar (puede enviarse solo una parte de l nombre)</param>
        /// <returns>lista de entidades que coinciden con los parametros de búsquedas</returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static ArrayList BuscarEntidad(string entidad)
        {
            
            string mensajeError = "";
            string strAccion = "";
            var resultado = new ArrayList();
            try
            {
                IServicioHydroListados servicioHydroListados = LocalizadorProxy.ServicioHydroListados();
                var lstResultado =
                    servicioHydroListados.ListadoEntidadesPorNombre(entidad, 0,
                                                                    Convert.ToDecimal(
                                                                        HttpContext.Current.Session[
                                                                            Parametros.CVariablesSesion
                                                                                .IdTipoActividad]), 10,
                                                                    Parametros.VolumenesCalidad.
                                                                        cParametrosHydro.strCredencial,
                                                                    ref mensajeError);
                foreach (var oEntidadCty in lstResultado)
                {
                    resultado.Add(new { idEntidad = oEntidadCty.ID_ENTIDAD, nombre = oEntidadCty.NOMBRE });
                }
            }
            catch (Exception ex)
            {
                resultado = null;
            }
            return resultado;

        }

        /// <summary>
        /// busca los campos/corrientes de una entidad (planta)
        /// </summary>
        /// <param name="idEntidad">identificador de la entidad</param>
        /// <returns>lista de campos de una entidad</returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static ArrayList BuscarCampos(decimal idEntidad)
        {
            string mensajeError = "";
            var resultado = new ArrayList();
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Get<List<O_LISTA_CAMPO_ENTIDAD_CTY>>("/ListarCamposPorEntidad/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/0/0/" + idEntidad + "?format=json");
                foreach (var oListaCampoEntidadCty in lstResultado)
                {
                    resultado.Add(
                        new { idCampo = oListaCampoEntidadCty.ID_CAMPO, nombre = oListaCampoEntidadCty.NOMBRE_CAMPO });
                }
            }
            catch (Exception)
            {
                resultado = null;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la fecha del último reporte registrado (por planta,campo,tipo operacion)
        /// </summary>
        /// <param name="idEntidad">identificador de la entidad</param>
        /// <param name="idCampo"></param>
        /// <returns>fecha del ultimo reporte en formato JSON</returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static ArrayList FechaUltimoReporte(decimal idEntidad, decimal idCampo)
        {
            string mensajeError = "";
            var resultado = new ArrayList();
            try
            {
                if (idCampo > 0 && idEntidad > 0)
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    var lstTipoReporte = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DIARIO?format=json");
                    decimal decIdTipoReporte = lstTipoReporte[0].ID_TIPO_REPORTE;
                    var lstTipoOperacion = clienteJson.Get<List<O_RESULTADO_CTY>>("/ObtenerTipoOperacion/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DTEYP/ALIMENTO?format=json");
                    decimal decIdTipoOperacion = lstTipoOperacion[0].ID_TABLA;
                    var lstResultado = clienteJson.Get<List<O_FECHA_ULTIMO_REPORTE_CTY>>("/ObtenerFechaUltimoReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/" + idCampo + "/" + idEntidad + "/" + decIdTipoReporte + "/" + decIdTipoOperacion + "/0?format=json");
                    foreach (var oListaCampoEntidadCty in lstResultado)
                    {
                        resultado.Add(
                            new { fecha = oListaCampoEntidadCty.FECHA_OPERACION });
                    }
                }
                else
                {
                    throw new ArgumentException("El idCampo e idEntidad son requeridos");
                }
            }
            catch (Exception)
            {
                resultado.Add(new { fecha = "" });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la fecha del ultimo reporte de produccion
        /// </summary>
        /// <param name="idEntidad">Identificador de la entidad</param>
        /// <returns>fecha del ultimo reporte en formato JSON</returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static ArrayList FechaUltimoReporteProduccion(decimal idEntidad)
        {
            string mensajeError = "";
            var resultado = new ArrayList();
            try
            {
                if (idEntidad > 0)
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    var lstTipoReporte = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DIARIO?format=json");
                    decimal decIdTipoReporte = lstTipoReporte[0].ID_TIPO_REPORTE;
                    var lstTipoOperacion = clienteJson.Get<List<O_RESULTADO_CTY>>("/ObtenerTipoOperacion/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DTEYP/PRODUCCION?format=json");
                    decimal decIdTipoOperacion = lstTipoOperacion[0].ID_TABLA;
                    var lstResultado = clienteJson.Get<List<O_FECHA_ULTIMO_REPORTE_CTY>>("/ObtenerFechaUltimoReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/0/" + idEntidad + "/" + decIdTipoReporte + "/" + decIdTipoOperacion + "/0?format=json");
                    foreach (var oListaCampoEntidadCty in lstResultado)
                    {
                        resultado.Add(
                            new { fecha = oListaCampoEntidadCty.FECHA_OPERACION });
                    }
                }
                else
                {
                    throw new ArgumentException("El idEntidad es requerido");
                }
            }
            catch (Exception)
            {
                resultado.Add(new { fecha = "" });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la fecha del ultimo reporte de gas residual
        /// </summary>
        /// <param name="idEntidad">Identificador de la entidad</param>
        /// <returns>fecha del ultimo reporte en formato JSON</returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static ArrayList FechaUltimoReporteResidual(decimal idEntidad)
        {
            string mensajeError = "";
            var resultado = new ArrayList();
            try
            {
                if (idEntidad > 0)
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    var lstTipoReporte = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DIARIO?format=json");
                    decimal decIdTipoReporte = lstTipoReporte[0].ID_TIPO_REPORTE;
                    var lstTipoOperacion = clienteJson.Get<List<O_RESULTADO_CTY>>("/ObtenerTipoOperacion/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DTEYP/RESIDUAL?format=json");
                    decimal decIdTipoOperacion = lstTipoOperacion[0].ID_TABLA;
                    var lstResultado = clienteJson.Get<List<O_FECHA_ULTIMO_REPORTE_CTY>>("/ObtenerFechaUltimoReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/0/" + idEntidad + "/" + decIdTipoReporte + "/" + decIdTipoOperacion + "/0?format=json");
                    foreach (var oListaCampoEntidadCty in lstResultado)
                    {
                        resultado.Add(
                            new { fecha = oListaCampoEntidadCty.FECHA_OPERACION });
                    }
                }
                else
                {
                    throw new ArgumentException("El idEntidad es requerido");
                }
            }
            catch (Exception)
            {
                resultado.Add(new { fecha = "" });
            }
            return resultado;
        }

        /// <summary>
        /// lista todas las entidades que sean plantas de GLP
        /// </summary>
        public void LLenarPlantas()
        {
            try
            {
                string mensajeError = "";
                cblPlantas.Items.Clear();
                IServicioHydroListados servicioHydroListados = LocalizadorProxy.ServicioHydroListados();

                decimal idEntidad;
                if (Session[CVariablesSesion.NombreEntidad] == null)
                // Los funcionarios ANH con sus perfiles no tienes asignado una Entidad
                {
                    idEntidad = 0;
                }
                else if (Session[CVariablesSesion.NombreEntidad].ToString().Contains("AGENCIA") &&
                    Session[CVariablesSesion.NombreEntidad].ToString().Contains("NACIONAL") &&
                    Session[CVariablesSesion.NombreEntidad].ToString().Contains("HIDROCARBUROS"))
                {
                    idEntidad = 0;
                }
                else
                {
                    idEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                }

                if (ddlEntidadOperadora.Visible)
                {
                    if (_decIdEntidad != -1)
                    {
                        if (_decIdEntidad > 0) idEntidad = _decIdEntidad;
                    }
                    else
                    {
                        idEntidad = _decIdEntidad;
                    }
                }

                var lstResultado =
                    servicioHydroListados.ListadoEntidadesPorNombre("PLANTA", idEntidad,
                                                                    Convert.ToDecimal(
                                                                        Session[CVariablesSesion.IdTipoActividad]),
                                                                        20,
                                                                    Parametros.VolumenesCalidad.
                                                                        cParametrosHydro.strCredencial,
                                                                    ref mensajeError);
                cblPlantas.DataSource = lstResultado;
                cblPlantas.ValueField = "ID_ENTIDAD";
                cblPlantas.TextField = "NOMBRE";
                cblPlantas.DataBind();

                if (lstResultado.Count > 1)
                {
                    var obj = new O_ENTIDAD_CTY { ID_ENTIDAD = 0, NOMBRE = "-- SELECCIONAR UNA PLANTA --" };
                    lstResultado.Add(obj);

                    lstResultado = lstResultado.OrderBy(p => p.ID_ENTIDAD).ToList();
                }
                else
                {
                    cblPlantas.SelectedIndex = 0;
                    hdnIdPlanta.Value = cblPlantas.Items[0].Value.ToString();
                }

                cmbPlantas.DataSource = lstResultado;
                cmbPlantas.DataValueField = "ID_ENTIDAD";
                cmbPlantas.DataTextField = "NOMBRE";
                cmbPlantas.DataBind();

                //cmbPlantas.SelectedIndex = cmbPlantas.Items.Count - 1;
                cmbPlantas.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        public void LlenarUnidadesMedida()
        {
            return;


            LlenarUnidadesMedidaVolumen(ddlUnidadMedidaProde);
            return;

            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                string mensajeError = "";
                //var lstResultado =_servicioListadosCalidad.ListarUnidadesMedidaGeneral(Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, ref mensajeError);
                var lstResultado = clienteJson.Get<List<O_UNIDADES_MEDIDA_GRAL_CTY>>("/ListarUnidadesMedidaGeneral/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "?format=json");
                if (lstResultado != null)
                {
                    int cantidad = lstResultado.Count;
                    ddlUnidadMedidaProde.Items.Clear();
                    for (int i = 0; i < cantidad; i++)
                    {
                        if (lstResultado[i].TIPO_UNIDAD != null &&
                            lstResultado[i].TIPO_UNIDAD.ToUpper().Contains("VOLUMEN"))
                            ddlUnidadMedidaProde.Items.Add(new ListItem(lstResultado[i].CODIGO,
                                                                        lstResultado[i].ID_UNIDAD_MEDIDA.ToString()));


                        //  ***HERE***
                        //if (lstResultado[i].TIPO_UNIDAD != null &&
                        //    lstResultado[i].TIPO_UNIDAD.ToUpper().Contains("VOLUMEN") &&
                        //    lstResultado[i].ID_UNIDAD_MEDIDA.ToString() == "90")
                        //    ddlUnidadMedidaProde.Items.Add(new ListItem(lstResultado[i].CODIGO,
                        //                                                lstResultado[i].ID_UNIDAD_MEDIDA.ToString()));
                    }

                    List<ListItem> listCopy = ddlUnidadMedidaProde.Items.Cast<ListItem>().ToList();
                    ddlUnidadMedidaProde.Items.Clear();
                    foreach (ListItem item in listCopy.OrderByDescending(item => item.Text))
                    {
                        ddlUnidadMedidaProde.Items.Add(item);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Persistencia de datos

        #region registrar gas de alimento

        /// <summary>
        /// registra los volumenes del gas de alimento de la planta
        /// </summary>
        /// <param name="planta">Identificador de la planta</param>
        /// <param name="corriente">Identificador de la corriente del gas de alimento</param>
        /// <param name="umVolumen">Unidad de medida del volumen del gas de alimento</param>
        /// <param name="umGLP">Unidad de medida del contenido de GLP</param>
        /// <param name="umPoderCalor">Unidad de medida del poder calorífico</param>
        /// <param name="fecha">fecha de reporte</param>
        /// <param name="gravEspec">Gravedad específica</param>
        /// <param name="volumen">Volumen del gas de alimento</param>
        /// <param name="contGLP">contenido de GLP</param>
        /// <param name="poderCalor">Poder Calorífico</param>
        /// <param name="n2"></param>
        /// <param name="co2"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <param name="iC4"></param>
        /// <param name="nC4"></param>
        /// <param name="iC5"></param>
        /// <param name="nC5"></param>
        /// <param name="nC6"></param>
        /// <param name="c7"></param>
        /// <param name="obs">Observaciones correspondientes al día</param>
        /// <param name="just">Justificación en case de que se trate de modificar los datos de un dia pasado (un dia que ya este registrado en el sistema)</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static ArrayList RegistrarVolumenesAlimentoGLP(
            decimal planta,
            decimal corriente, decimal umVolumen, decimal umGLP,
            decimal umPoderCalor, string fecha, decimal gravEspec,
            decimal volumen, decimal contGLP, decimal poderCalor,
            decimal n2, decimal co2, decimal c1, decimal c2,
            decimal c3, decimal iC4, decimal nC4, decimal iC5,
            decimal nC5, decimal nC6, decimal c7, string obs, string just, decimal i, decimal contador
            )
        {
            var respuesta = new ArrayList();
            string mensajeError = "";
            try
            {
                # region Registro datos
                AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle vObjObjetoDetalle = null;
                List<AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle> vColObjetoDetalle = new List<AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle>();

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
                vObjObjetoDetalle.Valor = gravEspec;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cVolumen;
                vObjObjetoDetalle.Valor = volumen;
                vObjObjetoDetalle.UnidadMedida = umVolumen;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cContenidoLicuables;
                vObjObjetoDetalle.Valor = contGLP;
                vObjObjetoDetalle.UnidadMedida = umGLP;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
                vObjObjetoDetalle.Valor = poderCalor;
                vObjObjetoDetalle.UnidadMedida = umPoderCalor;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN2;
                vObjObjetoDetalle.Valor = n2;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cCO2;
                vObjObjetoDetalle.Valor = co2;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC1;
                vObjObjetoDetalle.Valor = c1;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC2;
                vObjObjetoDetalle.Valor = c2;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC3;
                vObjObjetoDetalle.Valor = c3;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cI_C4;
                vObjObjetoDetalle.Valor = iC4;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN_C4;
                vObjObjetoDetalle.Valor = nC4;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cI_C5;
                vObjObjetoDetalle.Valor = iC5;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN_C5;
                vObjObjetoDetalle.Valor = nC5;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN_C6;
                vObjObjetoDetalle.Valor = nC6;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC7;
                vObjObjetoDetalle.Valor = c7;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.RegistrarCorrientes vObjRegistrarCorrientes = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.RegistrarCorrientes();
                vObjRegistrarCorrientes.Planta = planta;
                vObjRegistrarCorrientes.CorrienteCampo = corriente;
                vObjRegistrarCorrientes.IdUsuario = Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.UsuarioId]);
                vObjRegistrarCorrientes.Observaciones = obs;
                vObjRegistrarCorrientes.Justificacion = just;
                vObjRegistrarCorrientes.Fecha = fecha;/*Convert.ToDecimal(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(fecha)))*/
                vObjRegistrarCorrientes.Llave = Parametros.VolumenesCalidad.cParametrosHydro.strCredencial;
                vObjRegistrarCorrientes.ListaCorrientes = vColObjetoDetalle;
                #endregion

                #region Llamado al servicio
                var clienteJson =
                     new JsonServiceClient(
                         System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCorrientes/?format=json",
                    vObjRegistrarCorrientes);
                if (i == 0)
                { mensajeError1 = ""; }

                if (i == contador - 1)
                {
                    if (mensajeError1 != "" && resultado1 != 0)
                    {
                        respuesta.Add(new { RESULTADO = resultado1, MENSAJE = mensajeError1 });
                        respuesta.Add(new { RESULTADO = -1 });
                    }
                    else if (objResultado[0].MENSAJE_ERROR != "OK")
                    {
                        respuesta.Add(new { RESULTADO = objResultado[0].RESULTADO, MENSAJE = objResultado[0].MENSAJE_ERROR });
                    }
                }
                else
                {
                    if (objResultado[0].MENSAJE_ERROR != "OK")
                    {
                        mensajeError1 = objResultado[0].MENSAJE_ERROR;
                    }
                    resultado1 = objResultado[0].RESULTADO;
                }
                respuesta.Add(new { RESULTADO = objResultado[0].RESULTADO });


                #endregion
            }
            catch (Exception ex)
            {
                respuesta.Add(new { RESULTADO = -1, MENSAJE = ex.Message });
            }
            return respuesta;
        }

        #endregion

        #region produccion

        /// <summary>
        /// Registrar los volúmenes producidos
        /// </summary>
        /// <param name="planta">identificador de la planta</param>
        /// <param name="umProdGlp">Unidad de medida del volúmen producido de GLP</param>
        /// <param name="umProdPropano">Unidad de medida del volúmen producido de Propano</param>
        /// <param name="umEntregaPropano">Unidad de medida del volúmen entregado/consumido de Propano</param>
        /// <param name="umEntregaGlpCisterna">Unidad de medida del volúmen entregado de GLP por cisterna</param>
        /// <param name="umEntregaGlpDucto">Unidad de medida del volúmen entregado de GLP por Ducto</param>
        /// <param name="umSaldoGlp">Unidad de medida del saldo de GLP</param>
        /// <param name="umSaldoPropano">Unidad de medida del saldo de propano</param>
        /// <param name="umTemperatura">Unidad de medida de la temperatura</param>
        /// <param name="umGasolinaNatural">Unidad de medida de la produccion de Gasolina Natural</param>
        /// <param name="fecha">fecha en formato DD/MM/YYYY</param>
        /// <param name="produccionGLP">Volúmen producido de GLP</param>
        /// <param name="produccionPropano">Volúmen producido de propano</param>
        /// <param name="gasolinaNatural">Columen Gasolina Natural</param>
        /// <param name="entregaConsumoPropano">Volumen entregado/consumido de propano</param>
        /// <param name="entregaGlpCisterna">Volúmen entregado de GLP por cisterna</param>
        /// <param name="entregaGlpDucto">Volúmen entregado de GLP por Ducto</param>
        /// <param name="saldoGLP">Volúmen del saldo de GLP</param>
        /// <param name="saldoPropano">Volúmen del saldo de Propano</param>
        /// <param name="quemaGas">residuo de combustion de gas natural</param>
        /// <param name="gravedadEspecifica">Gravedad específica del GLP</param>
        /// <param name="tvr">Tension de vapor reid del GLP</param>
        /// <param name="temperatura">Temperatura del GLP</param>
        /// <param name="c2">C2 del GLP</param>
        /// <param name="c3">C3 del GLP</param>
        /// <param name="iC4">iC4 del GLP</param>
        /// <param name="nC4">nC4 del GLP</param>
        /// <param name="iC5">iC5 del GLP</param>
        /// <param name="nC5">nC5 del GLP</param>
        /// <param name="obs">Observaciones correspondientes al día</param>
        /// <param name="just">Justificación en case de que se trate de modificar los datos de un dia pasado (un dia que ya este registrado en el sistema)</param>
        /// <param name="umRendProdGlp">Unidad de medida de Rendimiento Produccion GLP</param>
        /// <param name="umGasCombustible">Unidad de Medida Gas combustible</param>
        /// <param name="umQuemaGas">Unidad de medida de Quema Gas</param>
        /// <param name="rendimientoProdGLP">Rendimiento de Produccion de GLP en %</param>
        /// <param name="gasCombustible">Gas Combustible del gas natural</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static ArrayList RegistrarVolumenesProduccionGLP(
            decimal planta,
            decimal umProdGlp,
            decimal umProdPropano,
            decimal umEntregaPropano,
            decimal umEntregaGlpCisterna,
            decimal umEntregaGlpDucto,
            decimal umSaldoGlp,
            decimal umSaldoPropano,
            decimal umTemperatura,
            decimal umRendProdGlp,
            decimal umGasCombustible,
            decimal umQuemaGas,
            decimal umGasolinaNatural,
            string fecha,
            decimal produccionGLP,
            decimal produccionPropano,
            decimal gasolinaNatural,
            decimal entregaConsumoPropano,
            decimal entregaGlpCisterna,
            decimal entregaGlpDucto,
            decimal saldoGLP,
            decimal saldoPropano,
            decimal rendimientoProdGLP,
            decimal gasCombustible,
            decimal quemaGas,
            decimal gravedadEspecifica,
            decimal tvr,
            decimal temperatura,
            decimal c2,
            decimal c3,
            decimal iC4,
            decimal nC4,
            decimal iC5,
            decimal nC5,
            string obs,
            string just,
            decimal i,
            decimal contador)
        {
            var respuesta = new ArrayList();
            string mensajeError = "";
            try
            {
                #region Cargando valores
                SvcRegistroDtep.ObjetoDetalle vObjObjetoDetalle = null;
                List<SvcRegistroDtep.ObjetoDetalle> vColObjetoDetalle = new List<SvcRegistroDtep.ObjetoDetalle>();

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cProduccionGlp;
                vObjObjetoDetalle.Valor = produccionGLP;
                vObjObjetoDetalle.UnidadMedida = umProdGlp;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cProduccionPropano;
                vObjObjetoDetalle.Valor = produccionPropano;
                vObjObjetoDetalle.UnidadMedida = umProdPropano;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cGasolinaNatural;
                vObjObjetoDetalle.Valor = gasolinaNatural;
                vObjObjetoDetalle.UnidadMedida = umGasolinaNatural;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cEntregaConsumoPropano;
                vObjObjetoDetalle.Valor = entregaConsumoPropano;
                vObjObjetoDetalle.UnidadMedida = umEntregaPropano;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpCisterna;
                vObjObjetoDetalle.Valor = entregaGlpCisterna;
                vObjObjetoDetalle.UnidadMedida = umEntregaGlpCisterna;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpDucto;
                vObjObjetoDetalle.Valor = entregaGlpDucto;
                vObjObjetoDetalle.UnidadMedida = umEntregaGlpDucto;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cSaldoGlp;
                vObjObjetoDetalle.Valor = saldoGLP;
                vObjObjetoDetalle.UnidadMedida = umSaldoGlp;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cSaldoPropano;
                vObjObjetoDetalle.Valor = saldoPropano;
                vObjObjetoDetalle.UnidadMedida = umSaldoPropano;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cRendimientoProduccionGlp;
                vObjObjetoDetalle.Valor = rendimientoProdGLP;
                vObjObjetoDetalle.UnidadMedida = umRendProdGlp;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cGasCombustible;
                vObjObjetoDetalle.Valor = gasCombustible;
                vObjObjetoDetalle.UnidadMedida = umGasCombustible;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cQuemaGas;
                vObjObjetoDetalle.Valor = quemaGas;
                vObjObjetoDetalle.UnidadMedida = umQuemaGas;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
                vObjObjetoDetalle.Valor = gravedadEspecifica;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cTVR;
                vObjObjetoDetalle.Valor = tvr;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cTemperatura;
                vObjObjetoDetalle.Valor = temperatura;
                vObjObjetoDetalle.UnidadMedida = umTemperatura;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC2;
                vObjObjetoDetalle.Valor = c2;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC3;
                vObjObjetoDetalle.Valor = c3;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cI_C4;
                vObjObjetoDetalle.Valor = iC4;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN_C4;
                vObjObjetoDetalle.Valor = nC4;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cI_C5;
                vObjObjetoDetalle.Valor = iC5;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN_C5;
                vObjObjetoDetalle.Valor = nC5;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                SvcRegistroDtep.RegistrarProduccion vObjRegistrarCorrientes = new SvcRegistroDtep.RegistrarProduccion();
                vObjRegistrarCorrientes.Planta = planta;
                vObjRegistrarCorrientes.Llave = Parametros.VolumenesCalidad.cParametrosHydro.strCredencial;
                vObjRegistrarCorrientes.IdUsuario = Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.UsuarioId]);
                vObjRegistrarCorrientes.ListaProduccion = vColObjetoDetalle;
                vObjRegistrarCorrientes.Observaciones = obs;
                vObjRegistrarCorrientes.Justificacion = just;
                vObjRegistrarCorrientes.Fecha = fecha;
                #endregion

                #region Llamado al servicio

                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarProduccion/?format=json", vObjRegistrarCorrientes);

                if (i == 0)
                { mensajeError1 = ""; }
                if (i == contador - 1)
                {
                    if (mensajeError1 != "" && resultado1 != 0)
                    {
                        respuesta.Add(new { RESULTADO = resultado1, MENSAJE = mensajeError1 });
                        respuesta.Add(new { RESULTADO = -1 });
                    }
                    else if (objResultado[0].MENSAJE_ERROR != "OK")
                    {
                        respuesta.Add(new { RESULTADO = objResultado[0].RESULTADO, MENSAJE = objResultado[0].MENSAJE_ERROR });
                    }
                }
                else
                {
                    if (objResultado[0].MENSAJE_ERROR != "OK")
                    {
                        mensajeError1 = objResultado[0].MENSAJE_ERROR;
                    }
                    resultado1 = objResultado[0].RESULTADO;
                }
                respuesta.Add(new { RESULTADO = objResultado[0].RESULTADO });

                #endregion
            }
            catch (Exception ex)
            {
                respuesta.Add(new { RESULTADO = -1, MENSAJE = ex.Message });
            }
            return respuesta;
        }

        #endregion

        #region gas residual

        /// <summary>
        /// 
        /// </summary>
        /// <param name="planta"></param>
        /// <param name="umReslVolumen"></param>
        /// <param name="umResPuntoRocio"></param>
        /// <param name="umResPoderCalorifico"></param>
        /// <param name="fecha"></param>
        /// <param name="gravedadEspecifica"></param>
        /// <param name="volumen"></param>
        /// <param name="h2O"></param>
        /// <param name="puntoRocio"></param>
        /// <param name="poderCalorifico"></param>
        /// <param name="n2"></param>
        /// <param name="co2"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <param name="iC4"></param>
        /// <param name="nC4"></param>
        /// <param name="iC5"></param>
        /// <param name="nC5"></param>
        /// <param name="nC6"></param>
        /// <param name="c7"></param>
        /// <param name="obs">Observaciones correspondientes al día</param>
        /// <param name="just">Justificación en case de que se trate de modificar los datos de un dia pasado (un dia que ya este registrado en el sistema)</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static ArrayList RegistrarVolumenesResidualGLP(
            decimal planta,
            decimal umReslVolumen,
            decimal umResPuntoRocio,
            decimal umResPoderCalorifico,
            string fecha,
            decimal gravedadEspecifica,
            decimal volumen,
            decimal h2O,
            decimal puntoRocio,
            decimal poderCalorifico,
            decimal n2,
            decimal co2,
            decimal c1,
            decimal c2,
            decimal c3,
            decimal iC4,
            decimal nC4,
            decimal iC5,
            decimal nC5,
            decimal nC6,
            decimal c7,
            string obs,
            string just,
            decimal i,
            decimal contador)
        {
            var respuesta = new ArrayList();
            string mensajeError = "";
            try
            {
                #region Registro Datos
                SvcRegistroDtep.ObjetoDetalle vObjObjetoDetalle = null;
                List<SvcRegistroDtep.ObjetoDetalle> vColObjetoDetalle = new List<SvcRegistroDtep.ObjetoDetalle>();

                vObjObjetoDetalle = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
                vObjObjetoDetalle.Valor = gravedadEspecifica;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cVolumen;
                vObjObjetoDetalle.Valor = volumen;
                vObjObjetoDetalle.UnidadMedida = umReslVolumen;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cH20;
                vObjObjetoDetalle.Valor = h2O;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cPuntoRocio;
                vObjObjetoDetalle.Valor = puntoRocio;
                vObjObjetoDetalle.UnidadMedida = umResPuntoRocio;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
                vObjObjetoDetalle.Valor = poderCalorifico;
                vObjObjetoDetalle.UnidadMedida = umResPoderCalorifico;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN2;
                vObjObjetoDetalle.Valor = n2;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cCO2;
                vObjObjetoDetalle.Valor = co2;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC1;
                vObjObjetoDetalle.Valor = c1;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC2;
                vObjObjetoDetalle.Valor = c2;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC3;
                vObjObjetoDetalle.Valor = c3;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cI_C4;
                vObjObjetoDetalle.Valor = iC4;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN_C4;
                vObjObjetoDetalle.Valor = nC4;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cI_C5;
                vObjObjetoDetalle.Valor = iC5;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN_C5;
                vObjObjetoDetalle.Valor = nC5;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cN_C6;
                vObjObjetoDetalle.Valor = nC6;
                vColObjetoDetalle.Add(vObjObjetoDetalle);

                vObjObjetoDetalle = new SvcRegistroDtep.ObjetoDetalle();
                vObjObjetoDetalle.Concepto = Constantes.cC7;
                vObjObjetoDetalle.Valor = c7;
                vColObjetoDetalle.Add(vObjObjetoDetalle);


                SvcRegistroDtep.RegistrarGasResidual vObjRegistrarCorrientes = new SvcRegistroDtep.RegistrarGasResidual();
                vObjRegistrarCorrientes.Planta = planta;
                vObjRegistrarCorrientes.Version = 2;
                vObjRegistrarCorrientes.CodigoProyecto = 0;
                vObjRegistrarCorrientes.IdUsuario = Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.UsuarioId]);
                vObjRegistrarCorrientes.Llave = Parametros.VolumenesCalidad.cParametrosHydro.strCredencial;
                vObjRegistrarCorrientes.ListaResidual = vColObjetoDetalle;
                vObjRegistrarCorrientes.Observaciones = obs;
                vObjRegistrarCorrientes.Justificacion = just;
                vObjRegistrarCorrientes.Fecha = fecha; // String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(fecha));                   
                #endregion

                #region Llamado al servicio

                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarGasResidual/?format=json", vObjRegistrarCorrientes);

                if (i == 0)
                { mensajeError1 = ""; }
                if (i == contador - 1)
                {
                    if (mensajeError1 != "" && resultado1 != 0)
                    {
                        respuesta.Add(new { RESULTADO = resultado1, MENSAJE = mensajeError1 });
                        respuesta.Add(new { RESULTADO = -1 });
                    }
                    else if (objResultado[0].MENSAJE_ERROR != "OK")
                    {
                        respuesta.Add(new { RESULTADO = objResultado[0].RESULTADO, MENSAJE = objResultado[0].MENSAJE_ERROR });
                    }
                }
                else
                {
                    if (objResultado[0].MENSAJE_ERROR != "OK")
                    {
                        mensajeError1 = objResultado[0].MENSAJE_ERROR;
                    }
                    resultado1 = objResultado[0].RESULTADO;
                }
                respuesta.Add(new { RESULTADO = objResultado[0].RESULTADO });

                #endregion

            }
            catch (Exception ex)
            {
                respuesta.Add(new { RESULTADO = -1, MENSAJE = ex.Message });
            }
            return respuesta;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<O_ENTIDAD_CTY> LLenarPlantasWeb(decimal decIdEntidad)
        {
            string mensajeError = "";

            IServicioHydroListados servicioHydroListados = LocalizadorProxy.ServicioHydroListados();
            List<O_ENTIDAD_CTY> lstResultado = servicioHydroListados.ListadoEntidadesPorNombre("PLANTA",
                decIdEntidad, _decIdTipoActividad, 20, Parametros.VolumenesCalidad.cParametrosHydro.strCredencial,
                ref mensajeError).ToList();

            return lstResultado;
        }

        #endregion

        #endregion

        #region reportes

        #region reporte de volumenes

        /// <summary>
        /// Reporte para visualizar lo que las plantas reportaron/registraron en el sistema
        /// </summary>
        /// <param name="decIdEntidad">Id de la entidad seleccionada</param>
        /// <param name="fechaIni">Fecha de inicio para el reporte</param>
        /// <param name="fechaFin">Fecha de finalizacion para el reprote</param>
        public void ReporteDetalle(decimal decIdEntidad, string fechaIni, string fechaFin)
        {
            string tipo = ddlTipoReporte.SelectedValue.ToString();
            if (decIdEntidad > 0)
            {
                try
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

                    bool okFechaIni = Session[StrNamespace + StrFechaIni] == null ||
                                      Session[StrNamespace + StrFechaIni].ToString() != fechaIni;
                    bool okFechaFin = Session[StrNamespace + StrFechaFin] == null ||
                                      Session[StrNamespace + StrFechaFin].ToString() != fechaFin;
                    bool okIdEntidad = Session[StrNamespace + StrIdEntidad] == null ||
                                       Session[StrNamespace + StrIdEntidad].ToString() != decIdEntidad.ToString();
                    if (okFechaIni || okFechaFin || okIdEntidad)
                    {
                        if (decIdEntidad != 0) Session[StrNamespace + StrIdEntidad] = decIdEntidad;
                        if (fechaIni.Trim() != "") Session[StrNamespace + StrFechaIni] = fechaIni;
                        if (fechaFin.Trim() != "") Session[StrNamespace + StrFechaFin] = fechaFin;
                        var decFechaIni =
                            Convert.ToDecimal(fechaIni.Split('/')[2] + fechaIni.Split('/')[1] +
                                              fechaIni.Split('/')[0] + "000000");
                        var decFechaFin =
                            Convert.ToDecimal(fechaFin.Split('/')[2] + fechaFin.Split('/')[1] +
                                              fechaFin.Split('/')[0] + "000000");
                        var strMensajeError = "";

                        //var objTipoReporte =_servicioCantidadesListado.ListarTiposReporte(Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, "DIARIO", ref strMensajeError).ToList();
                        var objTipoReporte = clienteJson.Get<List<AnhPersistenciaCore.Core.O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DIARIO?format=json");
                        var decIdTipoReporte = objTipoReporte[0].ID_TIPO_REPORTE;




                        if (tipo == "Todos")
                        {
                            //var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                            var objResultadoAlimento = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepGasAlimento/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial
                                + "/" + decIdTipoReporte + "/" + decIdEntidad + "/" + Convert.ToDecimal(ddlUnidadMedidaDestinoReportesVolumen.SelectedValue) + "/" + decFechaIni
                                + "/" + decFechaFin + "?format=json");

                            var objResultadoProduccion = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepProduccion/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial
                                + "/" + decIdTipoReporte + "/" + decIdEntidad + "/" + Convert.ToDecimal(ddlUnidadMedidaDestinoReportesPeso.SelectedValue)
                                + "/" + decFechaIni + "/" + decFechaFin + "?format=json");

                            var objResultadoResidual = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepGasResidual/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial
                                + "/" + decIdTipoReporte + "/" + decIdEntidad + "/" + Convert.ToDecimal(ddlUnidadMedidaDestinoReportesVolumen.SelectedValue)
                                + "/" + decFechaIni + "/" + decFechaFin + "?format=json");

                            objResultadoAlimento.AddRange(objResultadoProduccion);
                            objResultadoAlimento.AddRange(objResultadoResidual);
                            Session[StrNamespace + StrPgReporte] = objResultadoAlimento;
                        }
                        else if (tipo == "Corriestes de Produccion")
                        {
                            /*var resultado = _servicioCantidadesGestion.ReportarDtepGasAlimento(
                                Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, decIdTipoReporte, decIdEntidad,
                                Convert.ToDecimal(ddlUnidadMedidaDestinoReportesVolumen.SelectedValue),
                                decFechaIni, decFechaFin, ref strMensajeError).ToList();
                            */
                            var objResultado = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepGasAlimento/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial
                                + "/" + decIdTipoReporte + "/" + decIdEntidad + "/" + Convert.ToDecimal(ddlUnidadMedidaDestinoReportesVolumen.SelectedValue) + "/" + decFechaIni
                                + "/" + decFechaFin + "?format=json");


                            Session[StrNamespace + StrPgReporte] = objResultado;
                        }
                        else if (tipo == "Produccion")
                        {
                            /*var produccion = _servicioCantidadesGestion.ReportarDtepProduccion(
                                Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, decIdTipoReporte, decIdEntidad,
                                Convert.ToDecimal(ddlUnidadMedidaDestinoReportesPeso.SelectedValue),
                                decFechaIni, decFechaFin, ref strMensajeError).ToList();*/

                            var objResultado = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepProduccion/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial
                                + "/" + decIdTipoReporte + "/" + decIdEntidad + "/" + Convert.ToDecimal(ddlUnidadMedidaDestinoReportesPeso.SelectedValue)
                                + "/" + decFechaIni + "/" + decFechaFin + "?format=json");

                            Session[StrNamespace + StrPgReporte] = objResultado;

                        }
                        else
                        {
                            /*var gasResidual = _servicioCantidadesGestion.ReportarDtepGasResidual(
                                Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, decIdTipoReporte, decIdEntidad,
                                Convert.ToDecimal(ddlUnidadMedidaDestinoReportesVolumen.SelectedValue),
                                decFechaIni, decFechaFin, ref strMensajeError).ToList();
                            */
                            var objResultado = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepGasResidual/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial
                                + "/" + decIdTipoReporte + "/" + decIdEntidad + "/" + Convert.ToDecimal(ddlUnidadMedidaDestinoReportesVolumen.SelectedValue)
                                + "/" + decFechaIni + "/" + decFechaFin + "?format=json");

                            Session[StrNamespace + StrPgReporte] = objResultado;
                        }
                        //resultado.AddRange(produccion);
                        //resultado.AddRange(gasResidual);

                        //Session[StrNamespace + StrPgReporte] = resultado;
                    }
                    pgReporte.DataSource = Session[StrNamespace + StrPgReporte];
                    pgReporte.DataBind();
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Opcion para exportar el "ReporteDetalle" en Xls, Pdf, Word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Argumentos</param>
        /// <param name="strTipo">Tipo de reporte (Xls, Pdf, Word)</param>
        public void ExportarReporte(object sender, EventArgs e, string strTipo)
        {
            string fileName = "Reporte" +
                              DateTime.Now.ToString(Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ);
            switch (strTipo)
            {
                case "PDF":
                    pgeReporte.ExportPdfToResponse(fileName, true);
                    break;
                case "XLS":
                    pgeReporte.ExportXlsToResponse(fileName, true);
                    break;
                case "DOC":
                    pgeReporte.ExportRtfToResponse(fileName, true);
                    break;
            }
        }

        #endregion

        #region reporte de seguimiento a la produccion

        ///// <summary>
        ///// Reporte de seguimiento de volumenes de GLP
        ///// </summary>
        ///// <param name="decIdEntidad">Id de la entidad seleccionada</param>
        ///// <param name="fechaIni">Fecha de inicio para el reporte</param>
        ///// <param name="fechaFin">Fecha de finalizacion para el reprote</param>
        //public void ReporteSeguimiento(decimal decIdEntidad, string fechaIni, string fechaFin)
        //{
        //    if (decIdEntidad > 0)
        //    {
        //        bool okFechaIni = Session[StrNamespace + StrFechaIniSeg] == null ||
        //                          Session[StrNamespace + StrFechaIniSeg].ToString() != fechaIni;
        //        bool okFechaFin = Session[StrNamespace + StrFechaFinSeg] == null ||
        //                          Session[StrNamespace + StrFechaFinSeg].ToString() != fechaFin;
        //        bool okIdEntidad = Session[StrNamespace + StrIdEntidadSeg] == null ||
        //                           Session[StrNamespace + StrIdEntidadSeg].ToString() != decIdEntidad.ToString();
        //        if (okFechaIni || okFechaFin || okIdEntidad)
        //        {
        //            var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        //            if (decIdEntidad != 0) Session[StrNamespace + StrIdEntidadSeg] = decIdEntidad;
        //            if (fechaIni.Trim() != "") Session[StrNamespace + StrFechaIniSeg] = fechaIni;
        //            if (fechaFin.Trim() != "") Session[StrNamespace + StrFechaFinSeg] = fechaFin;
        //            decimal decFechaIni =
        //                Convert.ToDecimal(fechaIni.Split('/')[2] + fechaIni.Split('/')[1] +
        //                                  fechaIni.Split('/')[0] + "000000");
        //            decimal decFechaFin =
        //                Convert.ToDecimal(fechaFin.Split('/')[2] + fechaFin.Split('/')[1] +
        //                                  fechaFin.Split('/')[0] + "000000");
        //            string strMensajeError = "";

        //            //var objTipoReporte =_servicioCantidadesListado.ListarTiposReporte(Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, "DIARIO", ref strMensajeError).ToList();
        //            var objTipoReporte = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DIARIO?format=json");
        //            decimal decIdTipoReporte = objTipoReporte[0].ID_TIPO_REPORTE;

        //            var resultado = _servicioCantidadesGestion.ReportarDtepSegProdGa(Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, decIdTipoReporte, decIdEntidad,decFechaIni, decFechaFin, ref strMensajeError).ToList();

        //            Session[StrNamespace + StrPgRptSeguimiento] = resultado;
        //        }
        //        pgRptSeguimiento.DataSource = Session[StrNamespace + StrPgRptSeguimiento];
        //        pgRptSeguimiento.DataBind();
        //        pgChartRptSeguimiento.DataSource = Session[StrNamespace + StrPgRptSeguimiento];
        //        pgChartRptSeguimiento.DataBind();

        //        try
        //        {
        //            //// bloquear el control.
        //            //pgChartRptSeguimiento.BeginUpdate();
        //            //for (var i = 0; i < pgRptSeguimiento.Fields.Count; i++)
        //            //{
        //            //    PivotGridField chartField = pgChartRptSeguimiento.Fields[i];
        //            //    PivotGridField field = pgRptSeguimiento.Fields[i];
        //            //    chartField.FilterValues.Clear();
        //            //    chartField.FilterValues.Assign(field.FilterValues);
        //            //    chartField.FilterValues.FilterType = field.FilterValues.FilterType;
        //            //    chartField.Area = field.Area;
        //            //    chartField.AreaIndex = field.AreaIndex;
        //            //}
        //        }
        //        finally
        //        {
        //            //// desbloquear el control.
        //            //pgChartRptSeguimiento.EndUpdate();
        //        }
        //        var titulo = new ChartTitle
        //        {
        //            Dock = ChartTitleDockStyle.Bottom,
        //            Alignment = StringAlignment.Center,
        //            Text = "Del " + fechaIni + " al " + fechaFin + ("HYDRO - OCTANO").PadLeft(200, ' '),
        //            TextColor = Color.Black,
        //            Font = new Font(new FontFamily("Tahoma"), 8, GraphicsUnit.Point)
        //        };
        //        chartRptSeguimiento.Titles.Add(titulo);
        //    }
        //}

        public void FiltrarDatosGraficoSeguimiento()
        {
            try
            {
                // bloquear el control.
                pgChartRptSeguimiento.BeginUpdate();
                pgChartRptSeguimiento.Prefilter.CriteriaString = "[" + rptChartUNIDAD_MEDIDA.PrefilterColumnName +
                                                                 "] Like '%" +
                                                                 Convert.ToString(Session[StrUnMedReporte]) + "%'";
            }
            finally
            {
                // desbloquear el control.
                pgChartRptSeguimiento.EndUpdate();
                pgChartRptSeguimiento.DataBind();
            }
        }

        /// <summary>
        /// Opcion para exportar el "ReporteDetalle" en Xls, Pdf, Word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Argumentos</param>
        /// <param name="strTipo">Tipo de reporte (Xls, Pdf, Word)</param>
        public void ExportarReporteSeg(object sender, EventArgs e, string strTipo)
        {
            using (var ps = new PrintingSystem())
            {
                string fileName = "ReporteSeguimiento" +
                                  DateTime.Now.ToString(Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ);
                var link1 = new PrintableComponentLink { Component = pgeRptSeguimiento, PrintingSystem = ps };

                var link2 = new PrintableComponentLink();
                SetChartType(ddlTipoGrafico.SelectedValue, chartRptSeguimiento);

                FiltrarDatosGraficoSeguimiento();

                chartRptSeguimiento.DataBind();
                link2.Component = ((IChartContainer)chartRptSeguimiento).Chart;
                link2.PrintingSystem = ps;

                var compositeLink = new CompositeLink();
                compositeLink.Links.AddRange(new object[] { link1, link2 });
                compositeLink.PrintingSystem = ps;
                compositeLink.Landscape = true;
                compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;

                compositeLink.CreateDocument();
                using (var stream = new MemoryStream())
                {
                    Response.Clear();
                    Response.Buffer = false;
                    switch (strTipo)
                    {
                        case "PDF":
                            compositeLink.PrintingSystem.ExportOptions.Pdf.DocumentOptions.Author = "OCTANO";
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse(fileName, true, "pdf", "pdf", stream);
                            break;
                        case "XLS":
                            compositeLink.PrintingSystem.ExportToXlsx(stream);
                            WriteToResponse(fileName, true, "xlsx",
                                            "vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
                            break;
                        case "DOC":
                            compositeLink.PrintingSystem.ExportToRtf(stream);
                            WriteToResponse(fileName, true, "rtf", "rtf", stream);
                            break;
                    }
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }
            }
        }

        #endregion

        #region reporte de seguimiento al prode

        ///// <summary>
        ///// Reporte de seguimiento de volumenes de GLP
        ///// </summary>
        ///// <param name="decIdEntidad">Id de la entidad seleccionada</param>
        ///// <param name="fechaIni">Fecha de inicio para el reporte</param>
        ///// <param name="fechaFin">Fecha de finalizacion para el reprote</param>
        ///// <param name="idUnidadMedida">Identificador de la unidad de medidad en la que se debe generar el reporte</param>
        //public void ReporteProde(string decIdEntidad, string fechaIni, string fechaFin, decimal idUnidadMedida)
        //{
        //    if (decIdEntidad.Trim() != "")
        //    {
        //        bool okFechaIni = Session[StrNamespace + StrFechaIniProde] == null ||
        //                          Session[StrNamespace + StrFechaIniProde].ToString() != fechaIni;
        //        bool okFechaFin = Session[StrNamespace + StrFechaFinProde] == null ||
        //                          Session[StrNamespace + StrFechaFinProde].ToString() != fechaFin;
        //        bool okIdEntidad = Session[StrNamespace + StrIdEntidadProde] == null ||
        //                           Session[StrNamespace + StrIdEntidadProde].ToString() != decIdEntidad;
        //        bool okIdunidadMedida = Session[StrNamespace + StrIdUnidadMedida] == null ||
        //                                Convert.ToDecimal(Session[StrNamespace + StrIdUnidadMedida]) != idUnidadMedida;

        //        if (okFechaIni || okFechaFin || okIdEntidad || okIdunidadMedida)
        //        {
        //            var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
        //            if (decIdEntidad.Trim() != "") Session[StrNamespace + StrIdEntidadProde] = decIdEntidad;
        //            if (fechaIni.Trim() != "") Session[StrNamespace + StrFechaIniProde] = fechaIni;
        //            if (fechaFin.Trim() != "") Session[StrNamespace + StrFechaFinProde] = fechaFin;
        //            if (idUnidadMedida != 0) Session[StrNamespace + StrIdUnidadMedida] = idUnidadMedida;
        //            var resultado = new List<O_REPORTE_SEG_PROD_DTEP>();
        //            for (int i = 0; i < decIdEntidad.Split(';').Length; i++)
        //            {
        //                decimal idPlanta = Convert.ToDecimal(decIdEntidad.Split(';')[i]);
        //                decimal decFechaIni =
        //                    Convert.ToDecimal(fechaIni.Split('/')[2] + fechaIni.Split('/')[1] +
        //                                      fechaIni.Split('/')[0] + "000000");
        //                decimal decFechaFin =
        //                    Convert.ToDecimal(fechaFin.Split('/')[2] + fechaFin.Split('/')[1] +
        //                                      fechaFin.Split('/')[0] + "000000");
        //                string strMensajeError = "";

        //                //var objTipoReporte =_servicioCantidadesListado.ListarTiposReporte(Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, "DIARIO",ref strMensajeError).ToList();
        //                var objTipoReporte = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + Parametros.VolumenesCalidad.cParametrosHydro.strCredencial + "/DIARIO?format=json");
        //                decimal decIdTipoReporte = objTipoReporte[0].ID_TIPO_REPORTE;

        //                var resultadoAux = _servicioCantidadesGestion.ReportarDtepSegProde(
        //                    Parametros.VolumenesCalidad.cParametrosHydro.strCredencial, decIdTipoReporte, idPlanta,
        //                    idUnidadMedida,
        //                    decFechaIni, decFechaFin, ref strMensajeError).ToList();
        //                if (resultadoAux.Count > 0)
        //                    resultado.AddRange(resultadoAux);
        //            }
        //            Session[StrNamespace + StrPgRptProde] = resultado;
        //        }

        //        var abc = from x in (List<O_REPORTE_SEG_PROD_DTEP>)Session[StrNamespace + StrPgRptProde]
        //                  where x.TIPO == "1 PRODE (TMD)" || x.TIPO == "2 PRODUCCION (TMD)"
        //                  select x;

        //        pgRptProde.DataSource = Session[StrNamespace + StrPgRptProde];
        //        pgRptProde.DataBind();
        //        //pgChartRptProde.DataSource = Session[StrNamespace + StrPgRptProde];
        //        pgChartRptProde.DataSource = abc;
        //        pgChartRptProde.DataBind();

        //        try
        //        {
        //            //// bloquear el control.
        //            //pgChartRptProde.BeginUpdate();
        //            //for (var i = 0; i < pgRptProde.Fields.Count; i++)
        //            //{
        //            //    PivotGridField chartField = pgChartRptProde.Fields[i];
        //            //    PivotGridField field = pgRptProde.Fields[i];
        //            //    chartField.FilterValues.Clear();
        //            //    chartField.FilterValues.Assign(field.FilterValues);
        //            //    chartField.FilterValues.FilterType = field.FilterValues.FilterType;
        //            //    chartField.Area = field.Area;
        //            //    chartField.AreaIndex = field.AreaIndex;
        //            //}
        //        }
        //        finally
        //        {
        //            //// desbloquear el control.
        //            //pgChartRptProde.EndUpdate();
        //        }
        //        var titulo = new ChartTitle
        //        {
        //            Dock = ChartTitleDockStyle.Bottom,
        //            Alignment = StringAlignment.Center,
        //            Text = "Del " + fechaIni + " al " + fechaFin + ("HYDRO - OCTANO").PadLeft(200, ' '),
        //            TextColor = Color.Black,
        //            Font = new Font(new FontFamily("Tahoma"), 8, GraphicsUnit.Point)
        //        };
        //        chartRptProde.Titles.Add(titulo);
        //    }
        //}

        /// <summary>
        /// Opcion para exportar el "ReporteDetalle" en Xls, Pdf, Word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Argumentos</param>
        /// <param name="strTipo">Tipo de reporte (Xls, Pdf, Word)</param>
        public void ExportarProde(object sender, EventArgs e, string strTipo)
        {
            using (var ps = new PrintingSystem())
            {
                string fileName = "ReporteProde" +
                                  DateTime.Now.ToString(Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ);
                var link1 = new PrintableComponentLink();
                link1.Component = pgeRptProde;
                link1.PrintingSystem = ps;

                var link2 = new PrintableComponentLink();
                SetChartType(ddlTipoGraficoProde.SelectedValue, chartRptProde);
                chartRptProde.DataBind();
                link2.Component = ((IChartContainer)chartRptProde).Chart;
                link2.PrintingSystem = ps;

                var compositeLink = new CompositeLink();
                compositeLink.Links.AddRange(new object[] { link1, link2 });
                compositeLink.PrintingSystem = ps;
                compositeLink.Landscape = true;
                compositeLink.PaperKind = System.Drawing.Printing.PaperKind.Legal;

                compositeLink.CreateDocument();
                using (var stream = new MemoryStream())
                {
                    Response.Clear();
                    Response.Buffer = false;
                    switch (strTipo)
                    {
                        case "PDF":
                            compositeLink.PrintingSystem.ExportOptions.Pdf.DocumentOptions.Author = "OCTANO";
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse(fileName, true, "pdf", "pdf", stream);
                            break;
                        case "XLS":
                            compositeLink.PrintingSystem.ExportToXlsx(stream);
                            WriteToResponse(fileName, true, "xlsx",
                                            "vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
                            break;
                        case "DOC":
                            compositeLink.PrintingSystem.ExportToRtf(stream);
                            WriteToResponse(fileName, true, "rtf", "rtf", stream);
                            break;
                    }
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }
            }
        }

        #endregion

        #region exportar reporte como archivo

        /// <summary>
        /// Genera un archivo del reporte seleccionado
        /// </summary>
        /// <param name="fileName">Nombre del archivo para el reporte</param>
        /// <param name="saveAsFile">true muestra la opción guardar como en el navegador, false muestra la opción abrir del navegador</param>
        /// <param name="fileFormat">tipo de archivo (PDF,DOC,XLS)</param>
        /// <param name="application">nombre de la aplicacion que puede abrir el archivo</param>
        /// <param name="stream">contenido del reporte que esta almacenado en memoria</param>
        private void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, string application,
                                     MemoryStream stream)
        {
            if (Page == null)
                return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", application));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition",
                                       string.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat));
            Page.Response.BinaryWrite(stream.ToArray());
            Page.Response.End();
        }

        #endregion

        #endregion

        #endregion
    }

}