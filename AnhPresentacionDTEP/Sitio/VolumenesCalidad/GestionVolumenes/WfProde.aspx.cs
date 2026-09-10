using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DevExpress.Web;
using DevExpress.Web.Data;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using ServiceStack.ServiceClient.Web;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhAgenteServicios.ServicioHydroListados;
using AnhAgenteServicios;
using O_RESULTADO_CTY = AnhPersistenciaCore.Core.O_RESULTADO_CTY;
using Librerias.Anh.Us;
using System.Web.Security;
using System.IO;
using System.Web;

namespace AnhHydro.Sitio.VolumenesCalidad.GestionVolumenes
{
    public partial class WfProde : Page
    {

        #region variables

        private string _mensajeError = string.Empty;
        /// <summary>
        /// Nombre del grid para la guardarlo en sesion
        /// </summary>
        private const string Grid = "gridProde";
        private readonly IServicioHydroListados _servicioHydroListados = LocalizadorProxy.ServicioHydroListados();

        string strMensajeError = "";
        string strAccion = "";

        #endregion

        #region Eventos

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
                    ListarProde();
                    if (!IsPostBack)
                    {
                        ListarEntidad(grdProde.Columns["ID_ENTIDAD"] as GridViewDataComboBoxColumn);
                        //ListarCampos(grdProde.Columns["ID_CAMPO"] as GridViewDataComboBoxColumn, 0);
                        ListarUnidadesMedida(grdProde.Columns["ID_UNIDAD_MEDIDA"] as GridViewDataComboBoxColumn);
                        ListarTiposReporte(grdProde.Columns["ID_TIPO_REPORTE"] as GridViewDataComboBoxColumn);
                        ListarProductos(grdProde.Columns["ID_PRODUCTO"] as GridViewDataComboBoxColumn);
                    }
                    grdProde.ForceDataRowType(typeof(System.Data.DataRowView));
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdProde_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                    EliminarProde(e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdProde_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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
                    AgregarProde(sender, e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdProde_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                    ModificarProde(sender, e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdProde_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
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
                    Exception ex = e.Exception;
                    if (ex.Message.Trim() != "")
                    {
                        e.ErrorText = ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdProde_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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
                    e.Editor.ReadOnly = false;
                    if (e.Column.FieldName == "ID_CAMPO")
                    {
                        var combo = (ASPxComboBox)e.Editor;
                        combo.Callback += new CallbackEventHandlerBase(idCampo_OnCallback);
                        var grid = e.Column.Grid;
                        if (!combo.IsCallback)
                        {
                            var idEntidad = -1;
                            if (!grid.IsNewRowEditing)
                                int.TryParse(grid.GetRowValues(e.VisibleIndex, "ID_ENTIDAD").ToString(), out idEntidad);
                            ListarCampos(combo, idEntidad);
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

        protected void grdProde_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
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
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    var lstResultado = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + cParametrosHydro.strCredencial + "/MENSUAL?format=json");
                    var lstProducto = clienteJson.Get<List<AnhAgenteServicios.ServicioHydroListados.O_PRODUCTOS_CTY>>("/ListarProductos/" + cParametrosHydro.strCredencial + "/GLP?format=json");
                    var lstUnidadMedida = clienteJson.Get<List<O_UNIDADES_MEDIDA_GRAL_CTY>>("/ListarUnidadesMedidaGeneral/" + cParametrosHydro.strCredencial + "?format=json");
                    lstUnidadMedida = (from tv in lstUnidadMedida where tv.CODIGO.Contains("TMD") select tv).ToList();

                    e.NewValues["FECHA_PRODE"] = DateTime.Now;
                    e.NewValues["ID_TIPO_REPORTE"] = lstResultado.Count > 0 ? lstResultado[0].ID_TIPO_REPORTE : 0;
                    e.NewValues["VALOR_PRODE"] = 0;
                    e.NewValues["ID_PRODUCTO"] = lstProducto.Count > 0 ? lstProducto[0].ID_PRODUCTO : 0;
                    e.NewValues["ID_UNIDAD_MEDIDA"] = lstUnidadMedida.Count > 0 ? lstUnidadMedida[0].ID_UNIDAD_MEDIDA : 0;
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
                    grdExportar.WriteXlsxToResponse(GetType().Namespace + DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        private void idCampo_OnCallback(object sender, CallbackEventArgsBase e)
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
                    var idCampo = -1;
                    Int32.TryParse(e.Parameter, out idCampo);
                    ListarCampos(sender as ASPxComboBox, idCampo);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        #endregion

        #region Métodos

        #region combos

        /// <summary>
        /// Unidad de Programa: ListarProductos
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Llenar el combo de productos para el formulario de edicion
        /// </summary>
        protected void ListarProductos(GridViewDataComboBoxColumn col)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Get<List<AnhAgenteServicios.ServicioHydroListados.O_PRODUCTOS_CTY>>("/ListarProductos/" + cParametrosHydro.strCredencial + "/0?format=json");
                if (lstResultado != null)
                {
                    var lstPadre =
                        (from tv in lstResultado where tv.NOMBRE.ToUpper().Contains("CARBURANTE") select tv).ToList();
                    lstResultado =
                        (from tv in lstResultado where tv.ID_PRODUCTO_PADRE == lstPadre[0].ID_PRODUCTO select tv).ToList
                            ();
                    int cantidad = lstResultado.Count;
                    col.PropertiesComboBox.Items.Clear();
                    for (int i = 0; i < cantidad; i++)
                    {
                        if (lstResultado[i].NOMBRE.Contains("GAS") && lstResultado[i].NOMBRE.Contains("LICUADO") &&
                            lstResultado[i].NOMBRE.Contains("PETR"))
                            col.PropertiesComboBox.Items.Add(lstResultado[i].NOMBRE, lstResultado[i].ID_PRODUCTO);
                    }
                    if (col.PropertiesComboBox.Items.Count == 0)
                    {
                        col.PropertiesComboBox.Items.Add("-- NINGUNO --", null);
                    }
                }
            }
            catch (Exception ex)
            {
                CCalidadLibreria.MostrarMensaje(divMensaje, ex.Message, 1);
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Unidad de Programa: ListarUnidadesMedida
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Llenar el combo de unidades de medida para el formulario de edicion
        /// </summary>
        protected void ListarUnidadesMedida(GridViewDataComboBoxColumn col)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Get<List<O_UNIDADES_MEDIDA_GRAL_CTY>>("/ListarUnidadesMedidaGeneral/" + cParametrosHydro.strCredencial + "?format=json");

                if (lstResultado != null)
                {
                    int cantidad = lstResultado.Count;
                    col.PropertiesComboBox.Items.Clear();
                    col.PropertiesComboBox.Items.Add("-- NINGUNO --", null);
                    for (int i = 0; i < cantidad; i++)
                    {
                        if (lstResultado[i].TIPO_UNIDAD != null &&
                            lstResultado[i].TIPO_UNIDAD.ToUpper().Contains("VOLUMEN"))
                            col.PropertiesComboBox.Items.Add(lstResultado[i].CODIGO.ToUpper(), lstResultado[i].ID_UNIDAD_MEDIDA);
                    }
                }
            }
            catch (Exception ex)
            {
                CCalidadLibreria.MostrarMensaje(divMensaje, ex.Message, 1);
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Unidad de Programa: ListarTiposReporte
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Llenar el combo de tipos de reporte para el formulario de edicion
        /// </summary>
        protected void ListarTiposReporte(GridViewDataComboBoxColumn col)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + cParametrosHydro.strCredencial + "/MENSUAL?format=json");
                if (lstResultado != null)
                {
                    int cantidad = lstResultado.Count;
                    if (lstResultado.Count > 1)
                    {
                        col.PropertiesComboBox.Items.Clear();
                        col.PropertiesComboBox.Items.Add("-- NINGUNO --", null);
                    }
                    for (int i = 0; i < cantidad; i++)
                    {
                        col.PropertiesComboBox.Items.Add(lstResultado[i].DESCRIPCION, lstResultado[i].ID_TIPO_REPORTE);
                    }
                }
            }
            catch (Exception ex)
            {
                CCalidadLibreria.MostrarMensaje(divMensaje, ex.Message, 1);
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Unidad de Programa: ListarCampos
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Llenar el combo de campos para el formulario de edicion
        /// </summary>
        protected void ListarCampos(GridViewDataComboBoxColumn col, decimal decIdEntidad)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Get<List<O_LISTA_CAMPO_ENTIDAD_CTY>>("/ListarCamposPorEntidad/" + cParametrosHydro.strCredencial + "/0/0/" + decIdEntidad + "?format=json");
                if (lstResultado != null)
                {
                    int cantidad = lstResultado.Count;
                    col.PropertiesComboBox.Items.Clear();
                    col.PropertiesComboBox.Items.Add("-- NINGUNO --", null);
                    for (int i = 0; i < cantidad; i++)
                    {
                        col.PropertiesComboBox.Items.Add(lstResultado[i].NOMBRE_CAMPO, lstResultado[i].ID_CAMPO);
                    }
                }
            }
            catch (Exception ex)
            {
                CCalidadLibreria.MostrarMensaje(divMensaje, ex.Message, 1);
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Unidad de Programa: ListarCampos
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Llenar el combo de campos para el formulario de edicion
        /// </summary>
        protected void ListarCampos(ASPxComboBox col, decimal decIdEntidad)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Get<List<O_LISTA_CAMPO_ENTIDAD_CTY>>("/ListarCamposPorEntidad/" + cParametrosHydro.strCredencial + "/0/0/" + decIdEntidad + "?format=json");
                if (lstResultado != null)
                {
                    int cantidad = lstResultado.Count;
                    col.Items.Clear();
                    col.Items.Add("-- NINGUNO --", null);
                    for (int i = 0; i < cantidad; i++)
                    {
                        col.Items.Add(lstResultado[i].NOMBRE_CAMPO, lstResultado[i].ID_CAMPO);
                    }
                }
            }
            catch (Exception ex)
            {
                CCalidadLibreria.MostrarMensaje(divMensaje, ex.Message, 1);
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Unidad de Programa: ListarEntidad
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Llenar el combo de entidades para el formulario de edicion
        /// </summary>
        /// <param name="col">columna tipo combo</param>
        public void ListarEntidad(GridViewDataComboBoxColumn col)
        {
            try
            {
                decimal decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                var lstResultado = _servicioHydroListados.ListadoEntidadesPorNombre("PLANTA ", decIdEntidad, Convert.ToDecimal(Session[CVariablesSesion.IdTipoActividad]), 10, cParametrosHydro.strCredencial, ref _mensajeError);

                if (lstResultado != null)
                {
                    int cantidad = lstResultado.Count;
                    col.PropertiesComboBox.Items.Clear();
                    col.PropertiesComboBox.Items.Add("-- NINGUNO --", null);
                    for (int i = 0; i < cantidad; i++)
                    {
                        col.PropertiesComboBox.Items.Add(lstResultado[i].NOMBRE, lstResultado[i].ID_ENTIDAD);
                    }
                }
            }
            catch (Exception ex)
            {
                CCalidadLibreria.MostrarMensaje(divMensaje, ex.Message, 1);
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        #endregion

        #region listados

        /// <summary>
        /// Unidad de Programa: ListarProductos
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: lista los productos
        /// </summary>
        /// <param name="decIdCampo"></param>
        /// <param name="decIdEntidad"></param>
        /// <param name="opcion">si es 1 fuerza a actualizar la lista de productos, si es 0 se retorna la lista que esta guardada en sesion</param>
        protected void ListarProde(int opcion = 0, decimal decIdCampo = 0, decimal decIdEntidad = 0)
        {
            try
            {
                if (Session[Grid] == null || opcion == 1)
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                    List<O_LISTA_PRODE_CTY> lstResultado = clienteJson.Get<List<O_LISTA_PRODE_CTY>>("/ListarProde/" + cParametrosHydro.strCredencial + "/" + decIdCampo + "/" + decIdEntidad + "/0/0/0?format=json");
                    Session[Grid] = lstResultado;
                }
                grdProde.DataSource = Session[Grid];
                var columna = grdProde.Columns["FECHA_PRODE"] as GridViewDataDateColumn;
                if (columna != null) columna.PropertiesDateEdit.DisplayFormatString = "dd/MM/yyyy";
                grdProde.DataBind();
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        #endregion

        #region Persistencia de datos

        /// <summary>
        /// Unidad de Programa: EliminarCampo
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Elimina un producto de la lista
        /// </summary>
        /// <param name="e">parametros enviados por el evento</param>
        protected void EliminarProde(ASPxDataDeletingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                Decimal decIdProde = Convert.ToDecimal(e.Keys[grdProde.KeyFieldName]);
                e.Cancel = true;

                AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.EliminarProde vObjEliminarProde = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.EliminarProde();
                vObjEliminarProde.strLlave = cParametrosHydro.strCredencial;
                vObjEliminarProde.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                vObjEliminarProde.decAppFechaRegistro = Convert.ToDecimal(DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));
                vObjEliminarProde.decIdProde = decIdProde;
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/EliminarProde/?format=json", vObjEliminarProde);
                if (lstResultado == null || lstResultado[0].ID_TABLA < 0 || lstResultado[0].RESULTADO < 0)
                {
                    throw new Exception(lstResultado == null ? "No se eliminó el PRODE " : lstResultado[0].MENSAJE_ERROR);
                }
                ListarProde(1);
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Unidad de Programa: AgregarCampo
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Registrar un producto de la lista
        /// </summary>
        /// <param name="sender">objeto sender del evento</param>
        /// <param name="e">parametros enviados por el evento</param>
        protected void AgregarProde(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                var combo = grdProde.Columns["ID_TIPO_REPORTE"] as GridViewDataComboBoxColumn;

                var fecha =
                    (combo == null
                         ? ""
                         : combo.PropertiesComboBox.Items.FindByValue(e.NewValues["ID_TIPO_REPORTE"]).Text.ToUpper().
                               Contains(
                                   "MENSUAL")
                               ? Convert.ToDateTime(e.NewValues["FECHA_PRODE"]).ToString("yyyyMM01000000")
                               : Convert.ToDateTime(e.NewValues["FECHA_PRODE"]).ToString(
                                   cParametrosHydro.strFormatoFechaServ));
                AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.RegistrarProde vObjRegistrarProde = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.RegistrarProde();
                vObjRegistrarProde.strLlave = cParametrosHydro.strCredencial;
                vObjRegistrarProde.decIdProde = 0;
                vObjRegistrarProde.decFechaProde = Convert.ToDecimal(fecha);
                vObjRegistrarProde.decValorProde = Convert.ToDecimal(e.NewValues["VALOR_PRODE"]);
                vObjRegistrarProde.decIdEntidad = Convert.ToDecimal(e.NewValues["ID_ENTIDAD"]);
                vObjRegistrarProde.decIdCampo = Convert.ToDecimal(e.NewValues["ID_CAMPO"]);
                vObjRegistrarProde.decIdUnidadMedida = Convert.ToDecimal(e.NewValues["ID_UNIDAD_MEDIDA"]);
                vObjRegistrarProde.decIdTipoReporte = Convert.ToDecimal(e.NewValues["ID_TIPO_REPORTE"]);
                vObjRegistrarProde.decIdProducto = Convert.ToDecimal(e.NewValues["ID_PRODUCTO"]);
                vObjRegistrarProde.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                vObjRegistrarProde.decAppFechaRegistro = Convert.ToDecimal(DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));

                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarProde/?format=json", vObjRegistrarProde);
                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el PRODE" : lstResultado[0].MENSAJE_ERROR);
                }
                ListarProde(1);
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Unidad de Programa: ModificaCampo
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Actualizar un Producto de la lista
        /// </summary>
        /// <param name="sender">objeto sender del evento</param>
        /// <param name="e">parametros enviados por el evento</param>
        protected void ModificarProde(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                var combo = grdProde.Columns["ID_TIPO_REPORTE"] as GridViewDataComboBoxColumn;

                var fecha =
                    (combo == null
                         ? ""
                         : combo.PropertiesComboBox.Items.FindByValue(e.NewValues["ID_TIPO_REPORTE"]).Text.ToUpper().
                               Contains(
                                   "MENSUAL")
                               ? Convert.ToDateTime(e.NewValues["FECHA_PRODE"]).ToString("yyyyMM01000000")
                               : Convert.ToDateTime(e.NewValues["FECHA_PRODE"]).ToString(
                                   cParametrosHydro.strFormatoFechaServ));

                AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.RegistrarProde vObjRegistrarProde = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.RegistrarProde();
                vObjRegistrarProde.strLlave = cParametrosHydro.strCredencial;
                vObjRegistrarProde.decIdProde = Convert.ToDecimal(e.Keys[grdProde.KeyFieldName]);
                vObjRegistrarProde.decFechaProde = Convert.ToDecimal(fecha);
                vObjRegistrarProde.decValorProde = Convert.ToDecimal(e.NewValues["VALOR_PRODE"]);
                vObjRegistrarProde.decIdEntidad = Convert.ToDecimal(e.NewValues["ID_ENTIDAD"]);
                vObjRegistrarProde.decIdCampo = Convert.ToDecimal(e.NewValues["ID_CAMPO"]);
                vObjRegistrarProde.decIdUnidadMedida = Convert.ToDecimal(e.NewValues["ID_UNIDAD_MEDIDA"]);
                vObjRegistrarProde.decIdTipoReporte = Convert.ToDecimal(e.NewValues["ID_TIPO_REPORTE"]);
                vObjRegistrarProde.decIdProducto = Convert.ToDecimal(e.NewValues["ID_PRODUCTO"]);
                vObjRegistrarProde.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                vObjRegistrarProde.decAppFechaRegistro = Convert.ToDecimal(DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ));

                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarProde/?format=json", vObjRegistrarProde);

                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el PRODE" : lstResultado[0].MENSAJE_ERROR);
                }
                ListarProde(1);
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        #endregion

        #endregion

    }
}