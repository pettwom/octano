using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroListados;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using DevExpress.DashboardCommon.DB;
using DevExpress.Web;
using DevExpress.Web.Data;
using Librerias.Anh.Us;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using O_RESULTADO_CTY = AnhPersistenciaCore.Core.O_RESULTADO_CTY;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador
{
    public partial class WfParametricaProducto : System.Web.UI.Page
    {
        #region Declaración de Variables
        private string _mensajeError = string.Empty;
        /// <summary>
        /// Nombre del grid para la guardarlo en sesion
        /// </summary>
        private const string Grid = "gridProde";
        private  IServicioHydroListados _servicioHydroListados = LocalizadorProxy.ServicioHydroListados();

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

                    ListarProde();

                    _decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);

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
            try
            {
                if (!grdProde.IsEditing || e.Column.FieldName != "ID_TABLA_ESPEC") return;
                string entidad = "";
                if (e.KeyValue != null)
                {
                    string val = grdProde.GetRowValuesByKeyValue(e.KeyValue, "ID_ENTIDAD").ToString();
                    if (string.IsNullOrEmpty(val)) return;
                    entidad = (string)val;
                    Session.Add("idEntidadCmb", Convert.ToDecimal(entidad));
                }

                ASPxComboBox combo = e.Editor as ASPxComboBox;
                LlenarTablaCombo(combo, entidad);

                combo.Callback += new CallbackEventHandlerBase(cmbID_TABLA_ESPEC_OnCallback);
            }
            catch (Exception ex)
            {

            }
        }

        void cmbID_TABLA_ESPEC_OnCallback(object source, CallbackEventArgsBase e)
        {
            LlenarTablaCombo(source as ASPxComboBox, e.Parameter);

            string aa = e.GetId().ToString();
        }

        protected void LlenarTablaCombo(ASPxComboBox cmbTabla, string entidad)
        {
            if (string.IsNullOrEmpty(entidad)) return;

            List<O_PARAMETRO_CMB> tablaEspecList = ObtenerListaTabla(entidad); // ObtenerListaTabla(entidad);
            cmbTabla.Items.Clear();
            foreach (O_PARAMETRO_CMB oParametrosCty in tablaEspecList)
            {
                cmbTabla.Items.Add(oParametrosCty.TABLA_ESPEC, oParametrosCty.ID_TABLA_ESPEC);
            }
        }

        List<O_PARAMETRO_CMB> ObtenerListaTabla(string entidad)
        {
            var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

            decimal decEntidad = Convert.ToDecimal(entidad);

            List<O_LISTA_NOMBRE_PRODUCTO_CTY> listaTablaEsp = clienteJson.Get<List<O_LISTA_NOMBRE_PRODUCTO_CTY>>("/ListarNombreProductos/" + cParametrosHydro.strCredencial + "/" + decEntidad + "/" + 0 + "?format=json");
            List<O_PARAMETRO_CMB> res = null;

            var resEnt = (from t in listaTablaEsp select new { ID_TABLA_ESPEC = t.ID_TABLA_ESPEC, TABLA_ESPEC = t.TABLA_ESPEC }).Distinct().ToList();
            res = (from l in resEnt
                   select new O_PARAMETRO_CMB()
                   {
                       ID_TABLA_ESPEC = l.ID_TABLA_ESPEC,
                       TABLA_ESPEC = l.TABLA_ESPEC,
                   }).ToList();

            return res;
        }

        public static List<O_PARAMETRO_CMB> ListaParametrica(decimal decIdEntidad, string strDominioParametro)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                List<O_PARAMETRO_CMB> res = null;
                
                switch (strDominioParametro)
                {
                    case "ENTIDAD":

                        var oResultadoServicio1 = clienteJson.Get<List<O_LISTA_NOMBRE_PRODUCTO_CTY>>("/ListarNombreProductos/" + cParametrosHydro.strCredencial + "/" + decIdEntidad + "/" + 0 + "?format=json");
                        var resEnt = (from t in oResultadoServicio1 select new { ID_ENTIDAD = t.ID_ENTIDAD, ENTIDAD = t.ENTIDAD }).Distinct().ToList();
                        res = (from l in resEnt
                               select new O_PARAMETRO_CMB()
                               {
                                   ID_ENTIDAD = l.ID_ENTIDAD,
                                   ENTIDAD = l.ENTIDAD,
                               }).ToList();
                        break;
                    case "TABLA":
                        var oResultadoServicio2 = clienteJson.Get<List<O_LISTA_NOMBRE_PRODUCTO_CTY>>("/ListarNombreProductos/" + cParametrosHydro.strCredencial + "/" + decIdEntidad + "/" + 0 + "?format=json");
                        var resTab = (from t in oResultadoServicio2 select new { ID_TABLA_ESPEC = t.ID_TABLA_ESPEC, TABLA_ESPEC = t.TABLA_ESPEC }).Distinct().ToList();
                        res = (from l in resTab
                               select new O_PARAMETRO_CMB()
                               {
                                   ID_TABLA_ESPEC = l.ID_TABLA_ESPEC,
                                   TABLA_ESPEC = l.TABLA_ESPEC,
                               }).Distinct().ToList();
                        break;
                }
                
                return res;
            }
            catch (Exception exp)
            {
                CLogTraza.MensajeUsuario oMensajeUsuario = new CLogTraza.MensajeUsuario();
                oMensajeUsuario.decUsuarioId = 0;
                oMensajeUsuario.decIdModulo = CParametrosHydro.decIdAplicacion;
                oMensajeUsuario.strAccion = strDominioParametro + System.Reflection.MethodBase.GetCurrentMethod().Name;
                oMensajeUsuario.strIp = HttpContext.Current.Request.UserHostAddress;
                oMensajeUsuario.decNivelCapa = Convert.ToInt16(CLogTraza.CapasNivel.Presentacion);
                CLogTraza.Error(oMensajeUsuario, exp);
                return null;
            }
        }

        public class O_PARAMETRO_CMB
        {
            public decimal ID_ENTIDAD { get; set; }
            public string ENTIDAD { get; set; }

            public decimal ID_TABLA_ESPEC { get; set; }
            public string TABLA_ESPEC { get; set; }
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

        #endregion

        #region Métodos
        
        protected void ListarProde()
        {
            try
            {
                _decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                
                if (/*Convert.ToDecimal(Session[CVariablesSesion.UsuarioAdministrador]) == 1 ||*/ Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]))
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    List<O_LISTA_NOMBRE_PRODUCTO_CTY> lstResultado = clienteJson.Get<List<O_LISTA_NOMBRE_PRODUCTO_CTY>>("/ListarNombreProductos/" + cParametrosHydro.strCredencial + "/" + _decIdEntidad + "/0?format=json");

                    lstResultado = lstResultado.Where(p => p.ID_NOMBRE_PRODUCTO != 0).ToList();

                    if (lstResultado != null)
                    {
                        grdProde.DataSource = lstResultado;
                        grdProde.DataBind();
                    }
                }
                else
                {
                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    List<O_LISTA_NOMBRE_PRODUCTO_CTY> lstResultado = clienteJson.Get<List<O_LISTA_NOMBRE_PRODUCTO_CTY>>("/ListarNombreProductos/" + cParametrosHydro.strCredencial + "/" + _decIdEntidad + "/0?format=json");

                    lstResultado = lstResultado.Where(p => p.ID_NOMBRE_PRODUCTO != 0).ToList();

                    if (lstResultado != null)
                    {
                        grdProde.DataSource = lstResultado;
                        grdProde.DataBind();
                    }
                }
                
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        #endregion

        #region Persistencia de datos

        protected void AgregarProde(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.RegistraParametroNpc vObjRegistrarProde = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.RegistraParametroNpc();
                vObjRegistrarProde.strLlave = cParametrosHydro.strCredencial;
                vObjRegistrarProde.decIdEntidad = Convert.ToDecimal(e.NewValues["ID_ENTIDAD"]);
                vObjRegistrarProde.decIdTablaEspec = Convert.ToDecimal(e.NewValues["ID_TABLA_ESPEC"]);
                vObjRegistrarProde.strCodigo = e.NewValues["CODIGO_PRODUCTO"].ToString();
                vObjRegistrarProde.strNombre = e.NewValues["NOMBRE_PRODUCTO"].ToString();
                vObjRegistrarProde.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);;

                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistraParametroNpc/?format=json", vObjRegistrarProde);
                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el producto comercial" : lstResultado[0].MENSAJE_ERROR);
                }
                ListarProde();
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void ModificarProde(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var gridView = (ASPxGridView)sender;
                gridView.CancelEdit();
                e.Cancel = true;

                AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ActualizaParametroNpc vObjRegistrarProde = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.ActualizaParametroNpc();
                vObjRegistrarProde.strLlave = cParametrosHydro.strCredencial;
                vObjRegistrarProde.decIdNombreProducto = Convert.ToDecimal(e.Keys[grdProde.KeyFieldName]);
                vObjRegistrarProde.decIdEntidad = Convert.ToDecimal(e.NewValues["ID_ENTIDAD"]);
                vObjRegistrarProde.decIdTablaEspec = Convert.ToDecimal(e.NewValues["ID_TABLA_ESPEC"]);
                vObjRegistrarProde.strCodigo = e.NewValues["CODIGO_PRODUCTO"].ToString();
                vObjRegistrarProde.strNombre = e.NewValues["NOMBRE_PRODUCTO"].ToString();
                vObjRegistrarProde.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]); ;

                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/ActualizaParametroNpc/?format=json", vObjRegistrarProde);
                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el producto comercial" : lstResultado[0].MENSAJE_ERROR);
                }
                ListarProde();
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        protected void EliminarProde(ASPxDataDeletingEventArgs e)
        {
            try
            {
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                Decimal decIdProComer = Convert.ToDecimal(e.Keys[grdProde.KeyFieldName]);
                e.Cancel = true;

                AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.EliminaParametroNpc vObjRegistrarProde = new AnhServicioWebOctano.ServiciosWeb.Gestion.SvcRegistroDtep.EliminaParametroNpc();
                vObjRegistrarProde.strLlave = cParametrosHydro.strCredencial;
                vObjRegistrarProde.decIdNombreProducto = decIdProComer;
                vObjRegistrarProde.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]); ;

                var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/EliminaParametroNpc/?format=json", vObjRegistrarProde);
                if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el producto comercial" : lstResultado[0].MENSAJE_ERROR);
                }
                ListarProde();
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        #endregion

        protected void grdProde_OnBeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["EntidadId"] = _decIdEntidad;
        }
    }
}