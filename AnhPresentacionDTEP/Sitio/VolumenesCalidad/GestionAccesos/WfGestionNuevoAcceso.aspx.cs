using AnhPersistenciaCore.Aplicacion.Consulta;
using AnhPersistenciaCore.Entidades;
using AnhPresentacionDTEP.Entidades;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador;
using DevExpress.Web;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAccesos
{
    public partial class WfGestionNuevoAcceso : System.Web.UI.Page
    {
        readonly JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void pnlUsuarioModificarPermiso_Callback(object sender, CallbackEventArgsBase e)
        {
            if (txtUsuarioModificarPermiso.Text.Length > 2)
            {
                CargarUsuariosSistemaReplica(2);
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('DEBE ESCRIBIR MAS DE DOS LETRAS PARA BUSCAR USUARIO.');", true);
            }
        }

        protected void CargarUsuariosSistemaReplica(int filtro)
        {
            try
            {
                if (filtro == 1)
                {
                }
                else if (filtro == 2)
                {
                    var listaUsuariosNuevoReplica = client.Get<EResultadoUsuario>("/ListaUsuarios/" + CParametrosHydro.strCredencialHydroAdmin + "/0?format=json");
                    if (listaUsuariosNuevoReplica.oResultado != null)
                    {
                        List<O_USUARIOS_CTY> usuarioEncontradoNewReplica = listaUsuariosNuevoReplica.oResultado.Where(c => c.USUARIO.Contains(txtUsuarioModificarPermiso.Text.ToUpper()) || c.NOMBRES.Contains(txtUsuarioModificarPermiso.Text.ToUpper())).ToList(); //&& c.NOMBRES.Contains("@ANH.GOB.BO")).ToList();

                        List<O_LISTA_USUARIOS_ENTIDAD_CTY> lstUsuariosNewReplica = new List<O_LISTA_USUARIOS_ENTIDAD_CTY>();
                        foreach (var a in usuarioEncontradoNewReplica)
                        {
                            O_LISTA_USUARIOS_ENTIDAD_CTY usuario4 = new O_LISTA_USUARIOS_ENTIDAD_CTY();
                            usuario4.ID_USUARIO = a.ID_USUARIO;
                            usuario4.NOMBRE_USUARIO = a.NOMBRES;
                            usuario4.NOMBRE_USUARIO = a.USUARIO;
                            lstUsuariosNewReplica.Add(usuario4);
                        }

                        grvListaUsrModificarPermiso.DataSource = lstUsuariosNewReplica.OrderBy(o => o.NOMBRE_USUARIO);
                        grvListaUsrModificarPermiso.DataBind();
                        Session[CVariablesSesion.ListaUsuariosNewReplica] = lstUsuariosNewReplica;

                    }
                    else
                    {
                        grvListaUsrModificarPermiso.DataSource = new List<O_LISTA_USUARIOS_ENTIDAD_CTY>();
                        grvListaUsrModificarPermiso.DataBind();
                    }
                }    
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }

        protected void pnlListaPermisoCalidad_Callback(object sender, CallbackEventArgsBase e)
        {
            int index = grvListaUsrModificarPermiso.FocusedRowIndex;
            if (index >= 0)
            {
                var idUs = grvListaUsrModificarPermiso.GetRowValues(index, "ID_USUARIO").ToString();

                var objResultado = _consulta.ListadoPermisoCalidadIdUsuario(
                    new EListaControlesUsuario
                    {
                        strCredencial = "",
                        decIdUsuario = Convert.ToDecimal(idUs)
                    });
                if (objResultado.intCodigo == 1)
                {
                    var lstDato = (List<O_LISTA_PERMISO_CALIDAD_CTY>)objResultado.oResultado;

                    grvListaPermisos.DataSource = lstDato;
                    grvListaPermisos.DataBind();
                }
                else
                {
                    grvListaPermisos.DataSource = null;
                    grvListaPermisos.DataBind();
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                }
            }
        }

        protected void grvListaPermisos_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    int index = grvListaPermisos.FocusedRowIndex;
                    decimal decId = Convert.ToInt64(grvListaPermisos.GetRowValues(index, "ID_GESTION_OCTANO"));
                    if (decId > 0)
                    {
                        EResultado resultado;

                        if (e.ButtonID == "btnEliminar")
                        {
                            O_DATO_USUARIO_CALIDAD_CTY objUsuario = new O_DATO_USUARIO_CALIDAD_CTY();
                            objUsuario.strLlave = CParametrosHydro.StrCredencialOctCal;
                            objUsuario.decIdGestionOctano = decId;
                            objUsuario.decIdEntidad = 24;
                            objUsuario.decIdTipoActividad = 31;
                            objUsuario.strIpPermiso = "";
                            objUsuario.strAplicacion = "CALIDAD_DRP";
                            objUsuario.decFechaFin = 19000101000000;
                            objUsuario.strResponsable = "AARANCIBIA";
                            objUsuario.strCorreoAnh = "AARANCIBIA@ANH.GOB.BO";
                            objUsuario.strSiglaOrganigrama = "245";
                            objUsuario.strObjetoUsuarioPruebas = "";
                            objUsuario.decAppIdUsuario = 2475;
                            objUsuario.decAccion = 3;

                            JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioJson);
                            var objResultado = client.Post<O_RESULTADO_CALIDAD_CTY>("/GestionUsuario/?format=json", objUsuario);
                            if (objResultado.RESULTADO > 0)
                            {
                                pnlListaPermisoCalidad_Callback(null, null);
                            }
                        }
                        else
                        {

                        }

                        /*if (resultado.intCodigo == 1)
                        {
                            Session.Remove(CVariablesSesion.AuxActualiza);
                            dgvTransacciones_CustomCallback(null, null);
                        }
                        else
                        {
                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        }*/
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        private void MImprimirMensaje(string strMensaje, EnumTiposMensajeAlerta tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case EnumTiposMensajeAlerta.Error:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.DarkRed;
                    pnlMensaje.BackColor = Color.Pink;
                    strMensaje = "ERROR: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Alerta:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.OrangeRed;
                    pnlMensaje.BackColor = Color.LightSalmon;
                    strMensaje = "  ALERTA: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Correcto:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.DarkGreen;
                    pnlMensaje.BackColor = Color.DarkSeaGreen;
                    strMensaje = "  CORRECTO: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Limpiar:
                    pnlMensaje.Visible = false;
                    lblMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
                default:
                    pnlMensaje.Visible = false;
                    lblMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
            }
            if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }

        private bool MValidaSesion()
        {
            bool booResultado;
            if (Session[CVariablesSesion.IdUsuario] == null)
            {
                try
                {
                    Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    string script = string.Format("document.location.href = '{0}');", "~/Sitio/Persona/wfAutenticacion.aspx");
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
                }
                booResultado = false;
            }
            else
            {
                booResultado = true;
            }
            return booResultado;
        }

    }
}