using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhHydroTalleresOpeGarrafasPresentacion.Librerias;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Lib;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using DevExpress.Web;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad
{
    using ServiceStack.Common;
    
    public class O_REPORTE_CALIDAD_PIVOTE_AUX: O_REPORTE_CALIDAD_PIVOTE_CTY
    { 
        public DateTime FechaOperacionFormato
        {
            get { return CFechas.ConvierteLongDateTime(FECHA_OPERACION); }
            set
            {
                FECHA_OPERACION = CFechas.ConvierteDateTimeLong(value);
            }
        }
        public DateTime FechaImMuFormato
        {
            get { return CFechas.ConvierteLongDateTime(FECHA_IMMU); }
            set
            {
                FECHA_IMMU = CFechas.ConvierteDateTimeLong(value);
            }
        }
    }

    public class O_REPORTE_CALIDAD_PIVOTE_EXPO
    {
        public string ENTIDAD { get; set; }
        public string ACTIVIDAD { get; set; }
        public string CORRELATIVO { get; set; }
        public string TABLA_ESPECIFICA { get; set; }
        public DateTime FECHA_OPERACION { get; set; }
        public decimal VOLUMEN { get; set; }
        public decimal VOLUMEN_MUESTRA { get; set; }
        public string UNIDAD_MEDIDA_VOLUMEN { get; set; }
        public string PUNTO_CUSTODIO { get; set; }
        public DateTime FECHA_IMPORTACION { get; set; }
        public string RESOLUCION { get; set; }
        public decimal PRECIO { get; set; }
        public string MONEDA { get; set; }
        public string MARCA_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public decimal ORDEN { get; set; }
        public string PRUEBA_CALIDAD { get; set; }
        public string METODO_ASTM { get; set; }
        public string UM_PRUEBA { get; set; }
        public string VALOR_REPORTADO { get; set; }
        public string RANGOS_MULTIPLES { get; set; }
        public string ALERTA { get; set; }
    }
    public class O_REPORTE_CALIDAD_PIVOTE_REDUC_EXPO
    {
        public DateTime FECHA_OPERACION { get; set; }
        public string ENTIDAD { get; set; }
        public string ACTIVIDAD { get; set; }
        public string CORRELATIVO { get; set; }
        public string PUNTO_CUSTODIO { get; set; }
        public decimal VOLUMEN { get; set; }
        public string UNIDAD_MEDIDA_VOLUMEN { get; set; }
        public string TABLA_ESPECIFICA { get; set; }
        public string PRUEBA_CALIDAD { get; set; }
        public string METODO_ASTM { get; set; }
        public string UM_PRUEBA { get; set; }
        public string VALOR_REPORTADO { get; set; }
        public string ALERTA { get; set; }
    }
    
    public partial class wfRptCalidadGeneral : System.Web.UI.Page
    {
        #region Variables

        string strMensajeError = "";
        string strAccion = "";
        private bool bFechaCorrecta;
        private decimal decIdEntidad;
        private decimal decIdTipoActividad;
        List<O_REPORTE_CALIDAD_PIVOTE_AUX> lstResultado = null;
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        private decimal IdUsuario
        {
            get
            {
                return Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
            }
        }
        private decimal IdEntidad
        {
            get { return Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]); }
        }

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
                    if (CConsultaAccesos.AccesoFormulario(CParametrosHydro.strCredencialHydroAdmin,
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
                    dtFechaFinal.MaxDate = System.DateTime.Now;
                    //RecuperarFechas();
                    //if (!bFechaCorrecta)
                    //   {
                    //    ConfigurarFechaInicial();
                    //   }
                    //else 
                    if (!IsPostBack)
                       {
                           ConfigurarFechaInicial();
                       }


                    this.CargarComboEntidades(cmbEntidad, ObtenerListaEntidades(), IdEntidad);
                    List<O_LISTA_ENTIDAD_PROP_CTY> listaActividades = this.ObtenerListaActividades(Convert.ToDecimal(cmbEntidad.Value));
                    cmbEntidad.Items.Insert(0, new ListEditItem("TODOS...", "0"));
                    if (!IsCallback)
                    {
                        cmbEntidad.SelectedIndex = 0;
                    }
                    
                    listaActividades.Insert(0, new O_LISTA_ENTIDAD_PROP_CTY() { ID_TIPO_ACTIVIDAD = 0, TIPO_ACTIVIDAD = "TODOS..." });
                    this.CargarComboActividades(cmbActividad, listaActividades);
                    //if (!IsCallback)
                    //{
                        GenerarReporte();    
                    //}

                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
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
                    bFechaCorrecta = false;
                }
                if (!DateTime.TryParseExact(ff, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    bFechaCorrecta = false;
                }

                if (bFechaCorrecta)
                {
                    dtFechaInicial.Text = fi;
                    dtFechaFinal.Text = ff;
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Configurar las fechas de busquedas
        /// </summary>
        private void ConfigurarFechaInicial()
        {
            try
            {
                if (!IsPostBack)
                {
                    dtFechaInicial.Text = DateTime.Now.ToString("01/MM/yyyy");
                    dtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-ES");
                    dtFechaInicial.CalendarProperties.ClearButtonText = "Limpiar";
                    dtFechaInicial.CalendarProperties.TodayButtonText = "<< Hoy >> ";
                    dtFechaInicial.UseMaskBehavior = true;

                    dtFechaFinal.CalendarProperties.ClearButtonText = "Limpiar";
                    dtFechaFinal.CalendarProperties.TodayButtonText = "<< Hoy >> ";
                    dtFechaFinal.UseMaskBehavior = true;
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }
        
        /// <summary>
        /// Obtener la lista de Entidades y sus actividades del servicio
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_ENTIDAD_PROP_CTY> ObtenerListaEntidadActividad()
        {
            var vColReporteCalidad = clienteJson.Get<List<O_LISTA_ENTIDAD_PROP_CTY>>("/ListarEntidadesPropietarios/" + cParametrosHydro.strCredencial + "/" + IdUsuario + "?format=json");
            List<O_LISTA_ENTIDAD_PROP_CTY> lista = null;
            if (vColReporteCalidad != null)
            {
                lista = vColReporteCalidad;
            }
            else
            {
                lista = new List<O_LISTA_ENTIDAD_PROP_CTY>();
            }
            return lista;
        }
        /// <summary>
        /// Obtiene la lista de entidades que tienen opciones para cargar certificados de calidad
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_ENTIDAD_PROP_CTY> ObtenerListaEntidades()
        {
            List<O_LISTA_ENTIDAD_PROP_CTY> listaEntidadActividad = this.ObtenerListaEntidadActividad();
            List<O_LISTA_ENTIDAD_PROP_CTY> listaEntidades = null;
            if (listaEntidadActividad.Count != null)
            {

                listaEntidades = (from lea in listaEntidadActividad
                                  select
                                      new O_LISTA_ENTIDAD_PROP_CTY()
                                      {
                                          ID_ENTIDAD = lea.ID_ENTIDAD,
                                          ENTIDAD = lea.ENTIDAD
                                      }).GroupBy(p=>p.ID_ENTIDAD).Select(g=> g.First()).ToList();
                
            }
           
            return listaEntidades != null ? listaEntidades : new List<O_LISTA_ENTIDAD_PROP_CTY>();
        }

        /// <summary>
        /// Obtiene la lista de actividades de la entidad
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_ENTIDAD_PROP_CTY> ObtenerListaActividades(decimal decIdEntidad)
        {
            List<O_LISTA_ENTIDAD_PROP_CTY> listaEntidadActividad = this.ObtenerListaEntidadActividad();
            List<O_LISTA_ENTIDAD_PROP_CTY> listaActividades = null;
            if (listaEntidadActividad != null)
            {
                listaActividades = (from lea in listaEntidadActividad
                                    where lea.ID_ENTIDAD == decIdEntidad
                                    select
                                        new O_LISTA_ENTIDAD_PROP_CTY()
                                        {
                                            ID_TIPO_ACTIVIDAD = lea.ID_TIPO_ACTIVIDAD,
                                            TIPO_ACTIVIDAD = lea.TIPO_ACTIVIDAD
                                        }).GroupBy(p=>p.ID_TIPO_ACTIVIDAD).Select(f=>f.First()).ToList();
            }
            return listaActividades != null ? listaActividades : new List<O_LISTA_ENTIDAD_PROP_CTY>();
        }


        /// <summary>
        /// Carga el cualquier combo con la información de entidades, con los datos de la lista.
        /// En caso de ser necesario se puede enviar el valor de id que debe ser seleccionado por defecto.
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="lista"></param>
        /// <param name="decIdSeleccionado"></param>
        private void CargarComboEntidades(ASPxComboBox combo, List<O_LISTA_ENTIDAD_PROP_CTY> lista, decimal decIdSeleccionado = 0)
        {
            
            combo.DataSource = lista;
            combo.ValueField = "ID_ENTIDAD";
            combo.TextField = "ENTIDAD";
            combo.DataBind();
            if (decIdSeleccionado > 0)
            {
                O_LISTA_ENTIDAD_PROP_CTY objeto = lista.FirstOrDefault(w => w.ID_ENTIDAD == decIdSeleccionado);
                if (objeto != null)
                {
                    int intIndice = lista.IndexOf(objeto);
                    combo.SelectedIndex = intIndice;
                }
            }
        }
        /// <summary>
        /// Carga cualquier combo con actividades
        /// </summary>
        /// <param name="combo">Combo a ser cargado con la lista</param>
        /// <param name="lista">Lista de actividades</param>
        /// <param name="decIdSeleccionado">Si el dato es diferente de cero selecciona por defecto el id</param>
        private void CargarComboActividades(ASPxComboBox combo, List<O_LISTA_ENTIDAD_PROP_CTY> lista, decimal decIdSeleccionado = 0)
        {
            combo.DataSource = lista;
            combo.ValueField = "ID_TIPO_ACTIVIDAD";
            combo.TextField = "TIPO_ACTIVIDAD";
            combo.DataBind();
            if (decIdSeleccionado > 0)
            {
                O_LISTA_ENTIDAD_PROP_CTY objeto = lista.FirstOrDefault(w => w.ID_TIPO_ACTIVIDAD == decIdSeleccionado);
                if (objeto != null)
                {
                    int intIndice = lista.IndexOf(objeto);
                    combo.SelectedIndex = intIndice;
                }
            }
        }

        /// <summary>
        /// Genera reporte en base a los parametros de busqueda
        /// </summary>
        protected void GenerarReporte()
        {
            try
            {
                
                decIdEntidad = cmbEntidad.SelectedItem==null ? 0 : Convert.ToDecimal(cmbEntidad.SelectedItem.Value);
                decIdTipoActividad = cmbActividad.SelectedItem == null ? 0 : Convert.ToDecimal(cmbActividad.SelectedItem.Value);

                
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                string url = "/ReportarCertificadosPivote/" + cParametrosHydro.strCredencial + "/" + decIdEntidad + "/" + decIdTipoActividad + "/"
                    + (txtCite.Text.IsEmpty() ? "NULL" : txtCite.Text) + "/"
                             + Convert.ToDecimal(
                                 CFechas.ConvierteDateTimeLong((Convert.ToDateTime(dtFechaInicial.Text)))) + "/"
                             + Convert.ToDecimal(CFechas.ConvierteDateTimeLong((Convert.ToDateTime(dtFechaFinal.Text))))
                             + "/" + Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]) + "?format=json";
 
                lstResultado = clienteJson.Get<List<O_REPORTE_CALIDAD_PIVOTE_AUX>>(url);
 
                if (lstResultado != null && lstResultado.Count != 0)
                {
                    pvGrdCalidad.DataSource = lstResultado;
                    pvGrdCalidad.DataBind();
                }
                else
                {
                    pvGrdCalidad.DataSource = null;
                    pvGrdCalidad.DataBind();
                }

            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void btnExportar_OnClick(object sender, EventArgs e)
        {
            CValidarSesion.ValidarSesionUsuario(this.Context);
            try
            {
                exrptPivotCalidad.ExportXlsToResponse("ReporteOctCalidad");
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected void cmbActividad_Callback(object sender, CallbackEventArgsBase e)
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
                    if (cmbEntidad.SelectedIndex >= 0)
                    {
                        this.CargarComboActividades(cmbActividad, this.ObtenerListaActividades(Convert.ToDecimal(cmbEntidad.Value)));
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void btnExportarPlano_Click(object sender, EventArgs e)
        {

                if (Session[CVariablesSesion.UsuarioId] == null || !Request.IsAuthenticated)
                {
                    Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
                }
            
                string fileName = "RptCertCalidadPlano";

                GridView dg = new GridView();
                dg.AllowPaging = false;

                List<O_REPORTE_CALIDAD_PIVOTE_AUX> listAux = (List<O_REPORTE_CALIDAD_PIVOTE_AUX>)pvGrdCalidad.DataSource;

                List<O_REPORTE_CALIDAD_PIVOTE_EXPO> listaReporte = new List<O_REPORTE_CALIDAD_PIVOTE_EXPO>();
                foreach (O_REPORTE_CALIDAD_PIVOTE_AUX oDeclaracionCty in listAux)
                {
                    listaReporte.Add(new O_REPORTE_CALIDAD_PIVOTE_EXPO
                    {
                        ENTIDAD = oDeclaracionCty.ENTIDAD,
                        ACTIVIDAD = oDeclaracionCty.ACTIVIDAD,
                        CORRELATIVO = oDeclaracionCty.CORRELATIVO,
                        TABLA_ESPECIFICA = oDeclaracionCty.TABLA_ESPECIFICA,
                        VOLUMEN = oDeclaracionCty.VOLUMEN,
                        VOLUMEN_MUESTRA = oDeclaracionCty.VOLUMEN_MUESTRA,
                        UNIDAD_MEDIDA_VOLUMEN = oDeclaracionCty.VALOR_UNIDAD_MEDIDA,
                        PUNTO_CUSTODIO = oDeclaracionCty.VALOR_PUNTO_CUSTODIO,
                        RESOLUCION = oDeclaracionCty.RESOLUCION,
                        PRECIO = oDeclaracionCty.PRECIO,
                        MONEDA = oDeclaracionCty.CODIGO_MONEDA,
                        MARCA_PRODUCTO = oDeclaracionCty.VALOR_MARCA_PRODUCTO,
                        NOMBRE_PRODUCTO = oDeclaracionCty.NOMBRE_PRODUCTO,
                        ORDEN = oDeclaracionCty.ORDEN,
                        PRUEBA_CALIDAD = oDeclaracionCty.PRUEBA_CALIDAD,
                        METODO_ASTM = oDeclaracionCty.VALOR_METODO_ASTM,
                        UM_PRUEBA = oDeclaracionCty.VALOR_UM_PRUEBA,
                        VALOR_REPORTADO = oDeclaracionCty.VALOR_REPORTADO,
                        RANGOS_MULTIPLES = oDeclaracionCty.RANGOS_MULTIPLES,
                        ALERTA = oDeclaracionCty.ALERTA,
                        FECHA_OPERACION = CFechas.ConvierteLongDateTime(oDeclaracionCty.FECHA_OPERACION),
                        FECHA_IMPORTACION = CFechas.ConvierteLongDateTime(oDeclaracionCty.FECHA_IMMU)
                    });
                }
            
                dg.DataSource = listaReporte;
                dg.DataBind();

                Response.Buffer = true;
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                //Response.ContentType = "application/vnd.ms-excel";
                Response.ContentType = "application/" + ".xls";
                Response.AddHeader("content-disposition", "attachment;filename= " + "RptOctCalidad"+System.DateTime.Now + ".xls");

                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                var sw = new StringWriter();
                var hw = new HtmlTextWriter(sw);

                for (int i = 0; i < dg.Rows.Count; i++)
                {
                    //Apply text style to each Row
                    dg.Rows[i].Attributes.Add("class", "textmode");
                }

                dg.RenderControl(hw);

                //style to format numbers to string
                const string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();

        }

        protected void btnExportarPlanoReducido_Click(object sender, EventArgs e)
        {

            if (Session[CVariablesSesion.UsuarioId] == null || !Request.IsAuthenticated)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
            }

            const string fileName = "RptCertCalidadObservadosPlano";

            GridView dg = new GridView();
            dg.AllowPaging = false;

            List<O_REPORTE_CALIDAD_PIVOTE_AUX> listAux = (List<O_REPORTE_CALIDAD_PIVOTE_AUX>)pvGrdCalidad.DataSource;

            List<O_REPORTE_CALIDAD_PIVOTE_REDUC_EXPO> listaReporte = (from oDeclaracionCty in listAux
                where !oDeclaracionCty.ALERTA.Contains("OK")
                select new O_REPORTE_CALIDAD_PIVOTE_REDUC_EXPO
                {
                    FECHA_OPERACION = CFechas.ConvierteLongDateTime(oDeclaracionCty.FECHA_OPERACION), ENTIDAD = oDeclaracionCty.ENTIDAD, 
                    ACTIVIDAD = oDeclaracionCty.ACTIVIDAD, 
                    CORRELATIVO = oDeclaracionCty.CORRELATIVO, 
                    TABLA_ESPECIFICA = oDeclaracionCty.TABLA_ESPECIFICA, 
                    PRUEBA_CALIDAD = oDeclaracionCty.PRUEBA_CALIDAD, 
                    METODO_ASTM = oDeclaracionCty.VALOR_METODO_ASTM, 
                    UM_PRUEBA = oDeclaracionCty.VALOR_UM_PRUEBA, 
                    VALOR_REPORTADO = oDeclaracionCty.VALOR_REPORTADO, 
                    ALERTA = oDeclaracionCty.ALERTA, 
                    VOLUMEN = oDeclaracionCty.VOLUMEN, 
                    UNIDAD_MEDIDA_VOLUMEN = oDeclaracionCty.VALOR_UNIDAD_MEDIDA, 
                    PUNTO_CUSTODIO = oDeclaracionCty.VALOR_PUNTO_CUSTODIO
                }).ToList();

            dg.DataSource = listaReporte;
            dg.DataBind();

            Response.Buffer = true;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            //Response.ContentType = "application/vnd.ms-excel";
            Response.ContentType = "application/" + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename= " + fileName + System.DateTime.Now + ".xls");

            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var sw = new StringWriter();
            var hw = new HtmlTextWriter(sw);

            for (int i = 0; i < dg.Rows.Count; i++)
            {
                //Apply text style to each Row
                dg.Rows[i].Attributes.Add("class", "textmode");
            }

            dg.RenderControl(hw);

            //style to format numbers to string
            const string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }
    }
}