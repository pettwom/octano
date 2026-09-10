using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroListados;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhServicioWebOctano.ServiciosWeb.Gestion;
using DevExpress.Web;
using DevExpress.Web.Data;
using ServiceStack.ServiceClient.Web;
using O_RESULTADO_CTY = AnhPersistenciaCore.Core.O_RESULTADO_CTY;
using Librerias.Anh.Us;
using System.Web.Security;
using System.IO;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionVolumenes
{
    public partial class WfCamposEntidad : System.Web.UI.Page
    {
        #region variables

        private string _mensajeError = string.Empty;
        private decimal _idEntidad;
        /// <summary>
        /// Nombre del grid para la guardarlo en sesion
        /// </summary>
        private const string Grid = "gridCampoEntidad";
        private const string ListaEntidad = "gridCampoEntidadListaEntidad";
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
                    CCalidadLibreria.ValidarSesionUsuarioPopUp(Response);
                    if (!IsPostBack)
                    {
                        Session.Remove(Grid);
                        hdnIdCampo.Value = Request.QueryString["idCampo"];
                        grdCampoEntidad.ForceDataRowType(typeof(System.Data.DataRowView));
                    }
                    _idEntidad = Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.IdEntidad]);

                    ListarCampos(1, Convert.ToDecimal(hdnIdCampo.Value), _idEntidad);

                    if (!IsPostBack)
                        ListarEntidad(grdCampoEntidad.Columns["ID_ENTIDAD"] as GridViewDataComboBoxColumn);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdCampoEntidad_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                    EliminarCampoEntidad(e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdCampoEntidad_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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
                    AgregarCampoEntidad(sender, e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdCampoEntidad_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                    ModificarCampoEntidad(sender, e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdCampoEntidad_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdCampoEntidad_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
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
                    grdExportar.WriteXlsxToResponse(GetType().Namespace +
                                                                DateTime.Now.ToString(
                                                                    Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ));
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
        /// Unidad de Programa: ListarEntidad
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Llenar el combo de entidades para el formulario de edicion
        /// </summary>
        /// <param name="col">columna tipo combo</param>
        /// <param name="opcion">si es uno fuerza a actualizar la lista desde la base de datos</param>
        public void ListarEntidad(GridViewDataComboBoxColumn col, decimal opcion = 0)
        {
            try
            {
                decimal idEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                List<O_ENTIDAD_CTY> lstResultado = null;
                if (Session[ListaEntidad] == null || opcion == 1)
                {
                    lstResultado =
                        _servicioHydroListados.ListadoEntidadesPorNombre("PLANTA", idEntidad,
                                                                         Convert.ToDecimal(
                                                                             Session[CVariablesSesion.IdTipoActividad]),
                                                                         10,
                                                                         Parametros.VolumenesCalidad.
                                                                             cParametrosHydro.strCredencial,
                                                                         ref _mensajeError);


                    Session[ListaEntidad] = lstResultado;
                }
                lstResultado = (List<O_ENTIDAD_CTY>)Session[ListaEntidad];
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
                string strAccion = "";
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
        /// <param name="decIdCampo">Identificador del campo</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="opcion">si es 1 fuerza a actualizar la lista de productos, si es 0 se retorna la lista que esta guardada en sesion</param>
        protected void ListarCampos(int opcion = 0, decimal decIdCampo = 0, decimal decIdEntidad = 0)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

                if (decIdCampo != 0) decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                if (Session[Grid] == null || opcion == 1)
                {
                    var lstResultado = clienteJson.Get<List<O_LISTA_CAMPO_ENTIDAD_CTY>>("/ListarCamposPorEntidad/" +
                    cParametrosHydro.strCredencial +
                    "/0" +
                    "/" + decIdCampo +
                    "/" + decIdEntidad +
                    "?format=json");
                    
                    lstResultado = lstResultado.Where(p => p.ID_ENTIDAD != 0).ToList();
                    Session[Grid] = lstResultado;
                }
                grdCampoEntidad.DataSource = Session[Grid];
                grdCampoEntidad.DataBind();
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
        protected void EliminarCampoEntidad(ASPxDataDeletingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var decIdCampoEntidad = Convert.ToDecimal(e.Keys[grdCampoEntidad.KeyFieldName]);
                var decIdCampo = Convert.ToDecimal(hdnIdCampo.Value);
                e.Cancel = true;

                SvcRegistroDtep.EliminarCampoEntidad vObjEliminarCampoEntidad = new SvcRegistroDtep.EliminarCampoEntidad();
                vObjEliminarCampoEntidad.strCredencial = cParametrosHydro.strCredencial;
                vObjEliminarCampoEntidad.decIdCamposEntidad = decIdCampoEntidad;
                vObjEliminarCampoEntidad.decIdCampo = decIdCampo;
                vObjEliminarCampoEntidad.decIdEntidad = 0;
                vObjEliminarCampoEntidad.decAppIdUsuario = Convert.ToDecimal(Session[Parametros.CVariablesSesion.UsuarioId]);
                vObjEliminarCampoEntidad.decAppFechaRegistro = Convert.ToDecimal(DateTime.Now.ToString(Parametros.VolumenesCalidad.cParametrosHydro.strFormatoFechaServ));
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/EliminarCampoEntidad/?format=json",
                    vObjEliminarCampoEntidad);

                if (lstResultado == null || lstResultado[0].ID_TABLA < 0 || lstResultado[0].RESULTADO < 0)
                {
                    throw new Exception(lstResultado == null ? "No se eliminó el campo" : lstResultado[0].MENSAJE_ERROR);
                }
                ListarCampos(1, Convert.ToDecimal(hdnIdCampo.Value));
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
        protected void AgregarCampoEntidad(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                SvcRegistroDtep.RegistrarCamposEntidad vObjRegistrarCamposEntidad = new SvcRegistroDtep.RegistrarCamposEntidad();
                vObjRegistrarCamposEntidad.strCredencial = cParametrosHydro.strCredencial;
                vObjRegistrarCamposEntidad.decIdCamposEntidad = 0;
                vObjRegistrarCamposEntidad.decIdCampo = Convert.ToDecimal(hdnIdCampo.Value);
                vObjRegistrarCamposEntidad.decIdEntidad = Convert.ToDecimal(e.NewValues["ID_ENTIDAD"].ToString().Trim().PadLeft(1, '0'));
                vObjRegistrarCamposEntidad.decAppFechaRegistro = Convert.ToDecimal(
                            DateTime.Now.ToString(
                                Parametros.VolumenesCalidad.cParametrosHydro.
                                    strFormatoFechaServ));
                vObjRegistrarCamposEntidad.decAppIdUsuario = Convert.ToDecimal(
                            Session[Parametros.CVariablesSesion.UsuarioId]);
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCamposEntidad/?format=json", vObjRegistrarCamposEntidad);

                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null
                                            ? "No se registró la Planta para el campo"
                                            : lstResultado[0].MENSAJE_ERROR);
                }
                grdCampoEntidad.FilterExpression = String.Empty;
                ListarCampos(1, Convert.ToDecimal(hdnIdCampo.Value));
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
        protected void ModificarCampoEntidad(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var decIdCampoEntidad = Convert.ToDecimal(e.Keys[grdCampoEntidad.KeyFieldName]);
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;
                
                SvcRegistroDtep.RegistrarCamposEntidad vObjRegistrarCamposEntidad = new SvcRegistroDtep.RegistrarCamposEntidad();
                vObjRegistrarCamposEntidad.strCredencial = cParametrosHydro.strCredencial;
                vObjRegistrarCamposEntidad.decIdCamposEntidad = decIdCampoEntidad;
                vObjRegistrarCamposEntidad.decIdCampo = Convert.ToDecimal(hdnIdCampo.Value);
                vObjRegistrarCamposEntidad.decIdEntidad = Convert.ToDecimal(e.NewValues["ID_ENTIDAD"].ToString().Trim().PadLeft(1, '0'));
                vObjRegistrarCamposEntidad.decAppFechaRegistro = Convert.ToDecimal(
                            DateTime.Now.ToString(
                                Parametros.VolumenesCalidad.cParametrosHydro.
                                    strFormatoFechaServ));
                vObjRegistrarCamposEntidad.decAppIdUsuario = Convert.ToDecimal(
                            Session[Parametros.CVariablesSesion.UsuarioId]);
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCamposEntidad/?format=json", vObjRegistrarCamposEntidad);


                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null
                                            ? "No se registró la Planta para el campo"
                                            : lstResultado[0].MENSAJE_ERROR);
                }
                grdCampoEntidad.FilterExpression = String.Empty;
                ListarCampos(1, Convert.ToDecimal(hdnIdCampo.Value));
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