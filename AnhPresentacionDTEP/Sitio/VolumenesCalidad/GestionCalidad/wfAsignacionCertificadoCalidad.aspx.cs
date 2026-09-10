using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad
{

    using AnhPresentacionDTEP.Clases.VolumenesCalidad;
    using AnhPresentacionDTEP.Comun.UControl;
    using AnhPresentacionDTEP.Entidades;
    using AnhPresentacionDTEP.Parametros;
    using AnhPresentacionDTEP.Parametros.VolumenesCalidad;

    using DevExpress.Utils;
    using DevExpress.Web;

    using ServiceStack.ServiceClient.Web;

    public partial class wfAsignacionCertificadoCalidad : System.Web.UI.Page
    {
        public class O_LISTA_PROPIETARIOS_CTY
        {
            public string CITE { get; set; }
            public decimal ID_REGISTRO_CALIDAD { get; set; }
            public decimal ID_TIPO_ACTIVIDAD { get; set; }
            public string TIPO_ACTIVIDAD { get; set; }
            public decimal ID_PROPIETARIO_CERTIFICADO { get; set; }
            public string ESTADO { get; set; }
        }
        public class O_LISTA_ENTIDAD_PROP_CTY
        {
            public decimal ID_ENTIDAD { get; set; }
            public string ENTIDAD { get; set; }
            public decimal ID_TIPO_ACTIVIDAD { get; set; }
            public string TIPO_ACTIVIDAD { get; set; }
        }


        #region variables
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
        public static readonly string listaCalidadTemporal = "listaCalidadTemporal";
        string strAccion = "";
        string strMensajeError = "";

        private decimal IdUsaurio
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

        //private decimal IdTipoActividad
        //{
        //    get
        //    {
        //        return Convert.ToDecimal(Session[CVariablesSesion.IdTipoActividad]);
        //    }
        //}

        #endregion

        #region Metodos y funciones


        /// <summary>
        /// Obtener la lista de Entidades y sus actividades del servicio
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_ENTIDAD_PROP_CTY> ObtenerListaEntidadActividad()
        {
            var vColReporteCalidad = clienteJson.Get<List<O_LISTA_ENTIDAD_PROP_CTY>>("/ListarEntidadesPropietarios/" + cParametrosHydro.strCredencial + "/" + IdUsaurio + "?format=json");
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
                        }).GroupBy(p => p.ID_ENTIDAD).Select(g => g.First()).ToList();

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
                                        }).GroupBy(p => p.ID_TIPO_ACTIVIDAD).Select(f => f.First()).ToList();
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
        /// Lista los certificados de calidad entre fechas, entidad y tipo actividad
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_PROPIETARIOS_CTY> ListaCertificadosCalidad()
        {

            long decFechaInicial = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", deFechaDesde.Date));
            long decFechaFinal = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", deFechaHasta.Date));
            decimal decTipoActividad = Convert.ToDecimal(cmbActividadOrigen.Value);
            List<O_LISTA_PROPIETARIOS_CTY> listaCalidad = null;
            List<O_LISTA_PROPIETARIOS_CTY> ListaPropietariosCalidad = new List<O_LISTA_PROPIETARIOS_CTY>();
            if (cmbEntidadDestino.Visible && cmbEntidadOrigen.Visible)
            {

                ListaPropietariosCalidad =
                    clienteJson.Get<List<O_LISTA_PROPIETARIOS_CTY>>(
                        "/ListarPropietarios/" + cParametrosHydro.strCredencial + "/" + decFechaInicial + "/"
                        + decFechaFinal + "/" + Convert.ToDecimal(cmbEntidadOrigen.Value) + "/" + Convert.ToDecimal(cmbActividadOrigen.Value) + "/" + IdUsaurio + "?format=json");
            }
            else
            {
                ListaPropietariosCalidad =
                    clienteJson.Get<List<O_LISTA_PROPIETARIOS_CTY>>(
                        "/ListarPropietarios/" + cParametrosHydro.strCredencial + "/" + decFechaInicial + "/"
                        + decFechaFinal + "/" + IdEntidad + "/" + decTipoActividad + "/" + IdUsaurio + "?format=json");
            }

            if (ListaPropietariosCalidad != null)
            {
                listaCalidad = ListaPropietariosCalidad;

                //TODO:Lista de base Menos lista Temporal
                List<O_LISTA_PROPIETARIOS_CTY> listaTemporal =
                    (List<O_LISTA_PROPIETARIOS_CTY>)Session[listaCalidadTemporal];
                if (listaCalidad != null)
                {
                    listaCalidad =
                        listaCalidad.Where(
                            lc => !listaTemporal.Any(lt => lt.ID_PROPIETARIO_CERTIFICADO == lc.ID_PROPIETARIO_CERTIFICADO)).ToList();

                }
                else
                {
                    listaCalidad = new List<O_LISTA_PROPIETARIOS_CTY>();
                }

            }
            return listaCalidad;
        }
        private void CargarGrillaCalidadOrigen()
        {
            grdCertificadosOrigen.DataSource = this.ListaCertificadosCalidad();
            grdCertificadosOrigen.DataBind();
        }
        private void CargarGrillaCalidadTemporal()
        {
            List<O_LISTA_PROPIETARIOS_CTY> listaTemporal = (List<O_LISTA_PROPIETARIOS_CTY>)Session[listaCalidadTemporal];
            grdCertificadosTemporal.DataSource = listaTemporal;
            grdCertificadosTemporal.DataBind();
            if (grdCertificadosTemporal.VisibleRowCount > 0)
            {
                cmbActividadDestino.Enabled = false;
            }
            else
            {
                cmbActividadDestino.Enabled = true;
            }
        }

        private void MostrarComboEntidad(bool mostrar)
        {
            lblEntidadCertif.Visible = mostrar;
            cmbEntidadOrigen.Visible = mostrar;
            lblEntidadDestino.Visible = mostrar;
            cmbEntidadDestino.Visible = mostrar;
        }
        #endregion

        #region Eventos

        protected void page_Init()
        {
            //if (Session[CVariablesSesion.UsuarioId] == null)
            //{
            //    Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
            //    HttpContext.Current.ApplicationInstance.CompleteRequest();
            //}
            //else
            //{
            //    try
            //    {
            //        if (!CConsultaAccesos.AccesoFormulario(CParametrosHydro.strCredencialHydroAdmin,
            //            Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]),
            //            Convert.ToDecimal(CParametrosHydro.decIdAplicacion), Path.GetFileName(Request.Path),
            //            ref strMensajeError))
            //        {
            //            FormsAuthentication.SignOut();
            //            Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
            //            Session.RemoveAll();
            //            Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
            //        CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            //    }
            //}
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
                    if (!IsPostBack)
                    {
                        //Inicializa valores
                        Session[listaCalidadTemporal] = new List<O_LISTA_PROPIETARIOS_CTY>();
                        this.CargarComboEntidades(cmbEntidadOrigen, ObtenerListaEntidades(), IdEntidad);
                        List<O_LISTA_ENTIDAD_PROP_CTY> listaActividades = this.ObtenerListaActividades(Convert.ToDecimal(cmbEntidadOrigen.Value));

                        listaActividades.Insert(0, new O_LISTA_ENTIDAD_PROP_CTY() { ID_TIPO_ACTIVIDAD = 0, TIPO_ACTIVIDAD = "TODOS..." });
                        this.CargarComboActividades(cmbActividadOrigen, listaActividades);

                        this.CargarComboEntidades(cmbEntidadDestino, ObtenerListaEntidades(), IdEntidad);
                        cmbEntidadDestino.SelectedIndex = 0;
                        this.CargarComboActividades(cmbActividadDestino, this.ObtenerListaActividades(Convert.ToDecimal(cmbEntidadDestino.Value)));

                        MostrarComboEntidad(Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]));

                        //Valida fechas
                        int intMeses = -2;
                        deFechaDesde.MinDate = new DateTime(DateTime.Now.AddMonths(intMeses).Year, DateTime.Now.AddMonths(intMeses).Month, 1);
                        deFechaDesde.Date = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1);
                        deFechaHasta.Date = DateTime.Now.Date;
                    }
                    if (grdCertificadosOrigen.IsCallback)
                    {
                        this.CargarGrillaCalidadOrigen();
                    }
                    if (grdCertificadosTemporal.IsCallback)
                    {
                        this.CargarGrillaCalidadTemporal();
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        private bool EsValidoParaAsignar()
        {
            bool esValido = false;
            if (Convert.ToDecimal(cmbEntidadOrigen.Value) > 0 && Convert.ToDecimal(cmbEntidadDestino.Value) > 0
                && Convert.ToDecimal(cmbActividadOrigen.SelectedIndex) >= 0 && Convert.ToDecimal(cmbActividadDestino.Value) > 0)
            {
                if (cmbEntidadOrigen.Value.Equals(cmbEntidadDestino.Value)) esValido = !cmbActividadOrigen.Value.Equals(cmbActividadDestino.Value);
                else if (!cmbEntidadOrigen.Value.Equals(cmbEntidadDestino.Value)) esValido = true;
            }
            else
            {
                ucAlerta.MensajeAdvertencia("NO EXISTE ACTIVIDAD SELECCIONADA");
            }
            return esValido;
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
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
                    if (Convert.ToDecimal(cmbActividadDestino.Value) > 0 && EsValidoParaAsignar())
                    {
                        List<Object> listaSeleccionados =
                            grdCertificadosOrigen.GetSelectedFieldValues(grdCertificadosOrigen.KeyFieldName);
                        List<O_LISTA_PROPIETARIOS_CTY> ListaTemporal =
                            (List<O_LISTA_PROPIETARIOS_CTY>)Session[listaCalidadTemporal];

                        if (listaSeleccionados.Count > 0)
                        {
                            O_LISTA_PROPIETARIOS_CTY objItem = null;
                            foreach (var key in listaSeleccionados)
                            {
                                if (Convert.ToDecimal(key) < 0)
                                    continue;

                                objItem = new O_LISTA_PROPIETARIOS_CTY();
                                objItem.ID_REGISTRO_CALIDAD =
                                    Convert.ToDecimal(grdCertificadosOrigen.GetRowValuesByKeyValue(key, "ID_REGISTRO_CALIDAD"));
                                objItem.CITE = Convert.ToString(grdCertificadosOrigen.GetRowValuesByKeyValue(key, "CITE"));
                                objItem.ID_TIPO_ACTIVIDAD = Convert.ToDecimal(cmbActividadDestino.Value);
                                objItem.TIPO_ACTIVIDAD = Convert.ToString(cmbActividadDestino.Text);
                                objItem.ID_PROPIETARIO_CERTIFICADO = Convert.ToDecimal(grdCertificadosOrigen.GetRowValuesByKeyValue(key, "ID_PROPIETARIO_CERTIFICADO"));

                                if (!ListaTemporal.Any(w => w.ID_REGISTRO_CALIDAD == objItem.ID_REGISTRO_CALIDAD))
                                {
                                    ListaTemporal.Add(objItem);
                                }
                            }
                            this.CargarGrillaCalidadOrigen();
                            this.CargarGrillaCalidadTemporal();
                        }
                        else
                        {
                            ucAlerta.MensajeAdvertencia("NO EXISTE UNA CERTIFICADO SELECCIONADO EN LA GRILLA ORIGEN");
                        }
                    }
                    else
                    {
                        ucAlerta.MensajeAdvertencia("NO EXISTE UNA ACTIVIDAD SELECCIONADA EN EL DESTINO");
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
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
                    List<Object> listaSeleccionados = grdCertificadosTemporal.GetSelectedFieldValues(grdCertificadosTemporal.KeyFieldName);
                    if (listaSeleccionados.Count > 0)
                    {
                        List<O_LISTA_PROPIETARIOS_CTY> ListaTemporal =
                            (List<O_LISTA_PROPIETARIOS_CTY>)Session[listaCalidadTemporal];
                        foreach (var key in listaSeleccionados)
                        {
                            O_LISTA_PROPIETARIOS_CTY objPropietario =
                                ListaTemporal.FirstOrDefault(w => w.ID_REGISTRO_CALIDAD == Convert.ToDecimal(key));
                            //if (objPropietario.ID_PROPIETARIO_CERTIFICADO == 0)
                            ListaTemporal.Remove(objPropietario);
                        }
                        Session[listaCalidadTemporal] = ListaTemporal;
                        this.CargarGrillaCalidadOrigen();
                        this.CargarGrillaCalidadTemporal();
                    }
                    else
                    {
                        ucAlerta.MensajeAdvertencia("NO EXISTE FILAS SELECCIONADAS PARA ELIMINAR");
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }


        protected void cmbActividadOrigen_Callback(object sender, CallbackEventArgsBase e)
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
                    if (cmbEntidadOrigen.SelectedIndex >= 0)
                    {
                        this.CargarComboActividades(cmbActividadOrigen, this.ObtenerListaActividades(Convert.ToDecimal(cmbEntidadOrigen.Value)));
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void cmbActividadDestino_Callback(object sender, CallbackEventArgsBase e)
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
                    if (cmbEntidadDestino.SelectedIndex > 0)
                    {
                        this.CargarComboActividades(cmbActividadDestino, this.ObtenerListaActividades(Convert.ToDecimal(cmbEntidadDestino.Value)));
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void btnGuardarAsignados_Click(object sender, EventArgs e)
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
                    List<O_LISTA_PROPIETARIOS_CTY> listaTemporal = (List<O_LISTA_PROPIETARIOS_CTY>)Session[listaCalidadTemporal];
                    if (grdCertificadosTemporal.VisibleRowCount == listaTemporal.Count && listaTemporal.Count > 0 && grdCertificadosTemporal.VisibleRowCount > 0)
                    {
                        O_RESULTADO_CTY objResultado = new O_RESULTADO_CTY();
                        foreach (var obj in listaTemporal)
                        {
                            objResultado = clienteJson.Post<O_RESULTADO_CTY>(
                                   "/GestionPropietario/?format=json",
                                   new
                                       {
                                           strLlave = cParametrosHydro.strCredencial,
                                           decIdPropietarioPrueba = obj.ID_PROPIETARIO_CERTIFICADO,
                                           decIdPruebaCalidad = obj.ID_REGISTRO_CALIDAD, //solo en caso de eliminacion
                                           decIdEntidad = Convert.ToDecimal(cmbEntidadDestino.Value),
                                           decIdTipoActividad = obj.ID_TIPO_ACTIVIDAD,
                                           decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]),
                                           decAccion = 1 //1=registro/3=elimina
                                       });
                        }
                        if (objResultado.ID_TABLA > 0 && objResultado.MENSAJE_ERROR.Contains("OK"))
                        {
                            Session[listaCalidadTemporal] = new List<O_LISTA_PROPIETARIOS_CTY>();
                            ucAlerta.MensajeExito("LOS CERTIFICADOS DE CALIDAD FUERON REGISTRADOS CORRECTAMENTE.");
                        }
                        else
                        {
                            ucAlerta.MensajeError(objResultado.MENSAJE_ERROR);
                        }
                    }
                    else
                    {
                        ucAlerta.MensajeAdvertencia("NO EXISTEN DATOS PARA SER REGISTRADOS.");
                    }
                    cmbActividadDestino.SelectedIndex = -1;
                    this.CargarGrillaCalidadOrigen();
                    this.CargarGrillaCalidadTemporal();
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }


        protected void panelGrillas_Callback(object sender, CallbackEventArgsBase e)
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
                    btnEliminar.Visible = btnAgregar.Visible = this.EsValidoParaAsignar();
                    if (this.EsValidoParaAsignar())
                    {
                        if (cmbActividadOrigen.SelectedIndex >= 0)
                        {
                            this.CargarGrillaCalidadOrigen();
                        }
                        if (btnEliminar.Visible)
                        {
                            long decFechaInicial = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", deFechaDesde.Date));
                            long decFechaFinal = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", deFechaHasta.Date));
                            decimal decIdEntidadDestino = Convert.ToDecimal(cmbEntidadDestino.Value);
                            decimal dedIdActividadDestino = Convert.ToDecimal(cmbActividadDestino.Value);

                            var ListaPropietariosCalidad =
                                clienteJson.Get<List<O_LISTA_PROPIETARIOS_CTY>>(
                                    "/ListarPropietarios/" + cParametrosHydro.strCredencial + "/" + decFechaInicial + "/"
                                    + decFechaFinal + "/" + decIdEntidadDestino + "/" + dedIdActividadDestino + "/" + IdUsaurio
                                    + "?format=json");
                            if (ListaPropietariosCalidad != null)
                            {
                                List<O_LISTA_PROPIETARIOS_CTY> listaPropTemp = ListaPropietariosCalidad.Where(w => w.ESTADO.Equals("COPIA")).ToList();
                                Session[listaCalidadTemporal] = listaPropTemp;
                                if (listaPropTemp.Count == 0)
                                {
                                    ucAlerta.MensajeInformacion("NO EXISTEN NINGUNA COPIA DE CERTIFICADOS PARA LA ACTIVIDAD " + cmbActividadDestino.Text + ".");
                                }
                            }
                        }
                        else
                        {
                            ucAlerta.MensajeAdvertencia("ACCIÓN NO PERMITIDA.");
                        }
                    }
                    else
                    {
                        if (cmbActividadOrigen.SelectedIndex == -1 || cmbActividadDestino.SelectedIndex == -1)
                        {
                            ucAlerta.MensajeAdvertencia("DEBE SELECCIONAR UNA ACTIVIDAD VALIDAD EN AMBAS COLUMNAS.");
                        }
                        else
                        {
                            ucAlerta.MensajeAdvertencia("NO ES POSIBLE CREAR COPIAS DE CERTIFICADOS DE CALIDAD PARA LA MISMA ACTIVIDAD.");
                        }
                        Session[listaCalidadTemporal] = new List<O_LISTA_PROPIETARIOS_CTY>();
                    }
                    this.CargarGrillaCalidadOrigen();
                    this.CargarGrillaCalidadTemporal();
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdCertificadosOrigen_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;

            if (e.ButtonID == "Eliminar")
            {
                var dato = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "ESTADO");
                if (dato.ToString() == "ORIGINAL")
                {
                    e.Visible = DefaultBoolean.False;
                }
                if (dato.ToString() == "COPIA")
                {
                    e.Visible = DefaultBoolean.True;
                }
            }
        }

        protected void grdCertificadosOrigen_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
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
                    if (e.ButtonID == "Eliminar")
                    {
                        var row = (O_LISTA_PROPIETARIOS_CTY)grdCertificadosOrigen.GetRow(e.VisibleIndex);
                        if (row != null)
                        {
                            var objResultado = clienteJson.Post<O_RESULTADO_CTY>(
                               "/GestionPropietario/?format=json",
                                    new
                                    {
                                        strLlave = cParametrosHydro.strCredencial,
                                        decIdPropietarioPrueba = row.ID_PROPIETARIO_CERTIFICADO,
                                        decIdPruebaCalidad = row.ID_REGISTRO_CALIDAD, //solo en caso de eliminacion
                                        decIdEntidad = Convert.ToDecimal(cmbEntidadDestino.Value),
                                        decIdTipoActividad = row.ID_TIPO_ACTIVIDAD,
                                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]),
                                        decAccion = 3 //1=registro/3=elimina
                                    });
                            if (objResultado.ID_TABLA > 0 && objResultado.MENSAJE_ERROR.Contains("OK"))
                            {
                                ucAlerta.MensajeExito("REGISTRO ELIMINADO CORRECTAMENTE");
                            }
                            else
                            {
                                ucAlerta.MensajeError(objResultado.MENSAJE_ERROR);
                            }
                        }
                        this.CargarGrillaCalidadOrigen();
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
    }
}