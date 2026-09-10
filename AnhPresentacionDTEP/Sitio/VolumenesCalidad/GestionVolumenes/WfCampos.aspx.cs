using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhServicioWebOctano.ServiciosWeb.Gestion;
using DevExpress.Web;
using DevExpress.Web.Data;
using ServiceStack.ServiceClient.Web;
using Librerias.Anh.Us;
using System.Web.Security;
using System.IO;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionVolumenes
{
    public partial class WfCampos : System.Web.UI.Page
    {
        #region variables

        /// <summary>
        /// Nombre del grid para la guardarlo en sesion
        /// </summary>
        private const string Grid = "gridCampos";
        private decimal _decIdEntidad;
        private string _mensajeError = string.Empty;

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
                    if (!IsPostBack)
                    {
                        Session.Remove(Grid);
                        _decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                        ListarCampos(0, 0, _decIdEntidad);
                    }
                    //ListarCampos();
                    grdCampo.ForceDataRowType(typeof(System.Data.DataRowView));
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdCampo_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                    EliminarCampo(e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdCampo_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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
                    AgregarCampo(sender, e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }

        }

        protected void grdCampo_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                    ModificarCampo(sender, e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdCampo_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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

        protected void grdCampo_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
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
        protected void ListarCampos(int opcion, decimal decIdCampo, decimal decIdEntidad)
        {
            try
            {
                if (Session[Grid] == null || opcion == 1)
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    var lstResultado = clienteJson.Get<List<O_LISTA_CAMPO_ENTIDAD_CTY>>("/ListarCamposPorEntidad/" + cParametrosHydro.strCredencial + "/0/" + decIdCampo + "/" + decIdEntidad + "?format=json");
                    Session[Grid] = lstResultado;
                }
                grdCampo.DataSource = Session[Grid];
                grdCampo.DataBind();
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
        protected void EliminarCampo(ASPxDataDeletingEventArgs e)
        {

            try
            {
                Decimal decIdCampo = Convert.ToDecimal(e.Keys[grdCampo.KeyFieldName]);
                e.Cancel = true;

                SvcRegistroDtep.EliminarCampos vObjEliminaCampos = new SvcRegistroDtep.EliminarCampos();
                vObjEliminaCampos.strCredencial = cParametrosHydro.strCredencial;
                vObjEliminaCampos.decIdCampo = decIdCampo;
                vObjEliminaCampos.decAppIdUsuario = Convert.ToDecimal(
                            Session[Parametros.CVariablesSesion.UsuarioId]);
                vObjEliminaCampos.decAppFechaRegistro = Convert.ToDecimal(
                            DateTime.Now.ToString(
                                Parametros.VolumenesCalidad.cParametrosHydro.
                                    strFormatoFechaServ));
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/EliminarCampos/?format=json", vObjEliminaCampos);


                if (lstResultado == null || lstResultado[0].ID_TABLA < 0 || lstResultado[0].RESULTADO < 0)
                {
                    throw new Exception(lstResultado == null ? "No se eliminó el campo" : lstResultado[0].MENSAJE_ERROR);
                }
                //ListarCampos(1);
                ListarCampos(1, 0, _decIdEntidad);
                grdCampo.Settings.ShowTitlePanel = true;
                grdCampo.SettingsText.Title = "Campo eliminado";
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
        protected void AgregarCampo(object sender, ASPxDataInsertingEventArgs e)
        {

            try
            {
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                SvcRegistroDtep.RegistrarCampos vObjRegistrarCampos = new SvcRegistroDtep.RegistrarCampos();

                vObjRegistrarCampos.strCredencial = cParametrosHydro.strCredencial;
                vObjRegistrarCampos.decIdCampo = 0;
                vObjRegistrarCampos.strNombreCampo = e.NewValues["NOMBRE_CAMPO"].ToString().ToUpper().Trim();
                vObjRegistrarCampos.decAppFechaRegistro = Convert.ToDecimal(
                            DateTime.Now.ToString(
                                Parametros.VolumenesCalidad.cParametrosHydro.
                                    strFormatoFechaServ));
                vObjRegistrarCampos.decAppIdUsuario = Convert.ToDecimal(
                            Session[Parametros.CVariablesSesion.UsuarioId]);

                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCampos/?format=json", vObjRegistrarCampos);


                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el campo" : lstResultado[0].MENSAJE_ERROR);
                }
                grdCampo.FilterExpression = String.Empty;
                //ListarCampos(1);

                ListarCampos(1, 0, _decIdEntidad);
                grdCampo.Settings.ShowTitlePanel = true;
                grdCampo.SettingsText.Title = "Campo registrado";
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
        protected void ModificarCampo(object sender, ASPxDataUpdatingEventArgs e)
        {

            try
            {
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                SvcRegistroDtep.RegistrarCampos vObjRegistrarCampos = new SvcRegistroDtep.RegistrarCampos();

                vObjRegistrarCampos.strCredencial = cParametrosHydro.strCredencial;
                vObjRegistrarCampos.decIdCampo = Convert.ToDecimal(e.Keys[grdCampo.KeyFieldName]);
                vObjRegistrarCampos.strNombreCampo = e.NewValues["NOMBRE_CAMPO"].ToString().ToUpper().Trim();
                vObjRegistrarCampos.decAppFechaRegistro = Convert.ToDecimal(
                            DateTime.Now.ToString(
                                Parametros.VolumenesCalidad.cParametrosHydro.
                                    strFormatoFechaServ));
                vObjRegistrarCampos.decAppIdUsuario = Convert.ToDecimal(
                            Session[Parametros.CVariablesSesion.UsuarioId]);

                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCampos?format=json", vObjRegistrarCampos);


                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el campo" : lstResultado[0].MENSAJE_ERROR);
                }
                grdCampo.FilterExpression = String.Empty;
                //ListarCampos(1);
                ListarCampos(1, 0, _decIdEntidad);
                grdCampo.Settings.ShowTitlePanel = true;
                grdCampo.SettingsText.Title = "Campo Actualizado";
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