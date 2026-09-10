using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhServicioWebOctano.ServiciosWeb.Gestion;
using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.XtraPrinting;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador
{
    public partial class WfParametricas : System.Web.UI.Page
    {
        #region variables
        string strMensajeError = "";
        string strAccion = "";
        private decimal _decIdEntidad;
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
                        _decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                        ListarParametricas("MARCA_LUBRICANTES");
                    }
                    grdParametrica.ForceDataRowType(typeof(System.Data.DataRowView));
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdParametrica_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
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

        protected void grdParametrica_CustomErrorText(object sender, DevExpress.Web.ASPxGridViewCustomErrorTextEventArgs e)
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

        protected void grdParametrica_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            //OnRowUpdating="grdParametrica_RowUpdating"
        
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                try
                {
                    ModificarParametrica(sender, e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdParametrica_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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
                    AgregarParametrica(sender, e);
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void grdParametrica_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
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
                    //strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    //CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        
        #endregion

        #region Metodos
        
        protected void ListarParametricas(string strTipo)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Get<List<O_LISTA_PARAMETRICA_CTY>>("/ListarParametricas/" + cParametrosHydro.strCredencial + "/" + strTipo + "?format=json");

                grdParametrica.DataSource = lstResultado;
                grdParametrica.DataBind();
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void EliminarCampo(ASPxDataDeletingEventArgs e)
        {
            try
            {
                Decimal decIdParametrica = Convert.ToDecimal(e.Keys[grdParametrica.KeyFieldName]);
                e.Cancel = true;

                SvcRegistroDtep.EliminaParametrica vObjEliminaParametrica = new SvcRegistroDtep.EliminaParametrica();
                vObjEliminaParametrica.strLlave = cParametrosHydro.strCredencial;
                vObjEliminaParametrica.decIdParametrica = decIdParametrica;
                vObjEliminaParametrica.decAppIdUsuario = Convert.ToDecimal(
                            Session[Parametros.CVariablesSesion.UsuarioId]);
                vObjEliminaParametrica.strTipo = "MARCA_LUBRICANTES";
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/EliminaParametrica/?format=json", vObjEliminaParametrica);


                if (lstResultado == null || lstResultado[0].ID_TABLA < 0 || lstResultado[0].RESULTADO < 0)
                {
                    throw new Exception(lstResultado == null ? "No se eliminó la parametrica" : lstResultado[0].MENSAJE_ERROR);
                }

                ListarParametricas("MARCA_LUBRICANTES");
                grdParametrica.Settings.ShowTitlePanel = true;
                grdParametrica.SettingsText.Title = "Parametrica eliminada";

            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void AgregarParametrica(object sender, ASPxDataInsertingEventArgs e)
        {

            try
            {
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                SvcRegistroDtep.RegistraParametrica vObjRegistraParametrica = new SvcRegistroDtep.RegistraParametrica();

                vObjRegistraParametrica.strLlave = cParametrosHydro.strCredencial;
                vObjRegistraParametrica.strCodigo = e.NewValues["CODIGO"].ToString().ToUpper().Trim();
                vObjRegistraParametrica.strValor = e.NewValues["VALOR"].ToString().ToUpper().Trim();
                vObjRegistraParametrica.strDescripcion = e.NewValues["DESCRIPCION"].ToString().ToUpper().Trim();
                //vObjRegistraParametrica.strTipo = e.NewValues["TIPO"].ToString().ToUpper().Trim();
                vObjRegistraParametrica.decAppIdUsuario = Convert.ToDecimal(Session[Parametros.CVariablesSesion.UsuarioId]);

                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistraParametrica/?format=json", vObjRegistraParametrica);

                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró la parametrica" : lstResultado[0].MENSAJE_ERROR);
                }
                grdParametrica.FilterExpression = String.Empty;
                //ListarCampos(1);

                ListarParametricas("MARCAS_LUBRICANTES");
                grdParametrica.Settings.ShowTitlePanel = true;
                grdParametrica.SettingsText.Title = "Campo registrado";
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void ModificarParametrica(object sender, ASPxDataUpdatingEventArgs e)
        {

            try
            {
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                SvcRegistroDtep.ActualizaParametrica vObjActualizaParametrica = new SvcRegistroDtep.ActualizaParametrica();

                vObjActualizaParametrica.strLlave = cParametrosHydro.strCredencial;
                vObjActualizaParametrica.decIdParametrica = Convert.ToDecimal(e.Keys[grdParametrica.KeyFieldName]);
                vObjActualizaParametrica.strCodigo = e.NewValues["CODIGO"].ToString().ToUpper().Trim();
                vObjActualizaParametrica.strDescripcion = e.NewValues["DESCRIPCION"].ToString().ToUpper().Trim();
                vObjActualizaParametrica.strValor = e.NewValues["VALOR"].ToString().ToUpper().Trim();
                vObjActualizaParametrica.decAppIdUsuario = Convert.ToDecimal(Session[Parametros.CVariablesSesion.UsuarioId]);

                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/ActualizaParametrica?format=json", vObjActualizaParametrica);


                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el parametro" : lstResultado[0].MENSAJE_ERROR);
                }
                grdParametrica.FilterExpression = String.Empty;
                //ListarCampos(1);
                ListarParametricas("MARCA_LUBRICANTES");
                grdParametrica.Settings.ShowTitlePanel = true;
                grdParametrica.SettingsText.Title = "Campo Actualizado";
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