using AnhAgenteServicios;
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAccesos
{
    public partial class WfGestionReplicaUsuario : System.Web.UI.Page
    {

        readonly JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);

        protected void Page_Load(object sender, EventArgs e)
        {
            var strMensajeError = "";
            var servicio = LocalizadorProxy.ServicioParametricas();
            var datoTipoActividad = servicio.listadoTiposActividad("", 99999, ref strMensajeError);
            if (datoTipoActividad != null)
            {
                cmbTipoActividad.DataSource = datoTipoActividad;
                cmbTipoActividad.DataBind();
            }
        }

        protected void pnlUsuarioNuevoReplica_Callback(object sender, CallbackEventArgsBase e)
        {
            if (txtUsuarioNuevoReplica.Text.Length > 2)
            {
                CargarUsuariosSistemaReplica(4);
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
                    
                }
                else if (filtro == 4)
                {
                    var listaUsuariosNuevoReplica = client.Get<EResultadoUsuario>("/ListaUsuarios/" + CParametrosHydro.strCredencialHydroAdmin + "/0?format=json");
                    if (listaUsuariosNuevoReplica.oResultado != null)
                    {
                        List<O_USUARIOS_CTY> usuarioEncontradoNewReplica = listaUsuariosNuevoReplica.oResultado.Where(c => c.USUARIO.Contains(txtUsuarioNuevoReplica.Text.ToUpper()) || c.NOMBRES.Contains(txtUsuarioNuevoReplica.Text.ToUpper())).ToList(); //&& c.NOMBRES.Contains("@ANH.GOB.BO")).ToList();

                        List<O_LISTA_USUARIOS_ENTIDAD_CTY> lstUsuariosNewReplica = new List<O_LISTA_USUARIOS_ENTIDAD_CTY>();
                        foreach (var a in usuarioEncontradoNewReplica)
                        {
                            O_LISTA_USUARIOS_ENTIDAD_CTY usuario4 = new O_LISTA_USUARIOS_ENTIDAD_CTY();
                            usuario4.ID_USUARIO = a.ID_USUARIO;
                            usuario4.NOMBRE_USUARIO = a.NOMBRES;
                            usuario4.NOMBRE_USUARIO = a.USUARIO;
                            lstUsuariosNewReplica.Add(usuario4);
                        }

                        grvListaUsuarioNuevoReplica.DataSource = lstUsuariosNewReplica.OrderBy(o => o.NOMBRE_USUARIO);
                        grvListaUsuarioNuevoReplica.DataBind();
                        Session[CVariablesSesion.ListaUsuariosNewReplica] = lstUsuariosNewReplica;

                    }
                    else
                    {
                        grvListaUsuarioNuevoReplica.DataSource = new List<O_LISTA_USUARIOS_ENTIDAD_CTY>();
                        grvListaUsuarioNuevoReplica.DataBind();
                    }
                }
                else if (filtro == 5)
                {
                    var listaUsuariosReplica = client.Get<EResultadoUsuario>("/ListaUsuarios/" + CParametrosHydro.strCredencialHydroAdmin + "/0?format=json");
                    if (listaUsuariosReplica.oResultado != null)
                    {
                        List<O_USUARIOS_CTY> usuarioEncontradoReplica = listaUsuariosReplica.oResultado.Where(c => c.USUARIO.Contains(txtUsuarioReplica.Text.ToUpper()) || c.NOMBRES.Contains(txtUsuarioReplica.Text.ToUpper())).ToList(); //&& c.NOMBRES.Contains("@ANH.GOB.BO")).ToList();

                        List<O_LISTA_USUARIOS_ENTIDAD_CTY> lstUsuariosReplica = new List<O_LISTA_USUARIOS_ENTIDAD_CTY>();
                        foreach (var a in usuarioEncontradoReplica)
                        {
                            O_LISTA_USUARIOS_ENTIDAD_CTY usuario5 = new O_LISTA_USUARIOS_ENTIDAD_CTY();
                            usuario5.ID_USUARIO = a.ID_USUARIO;
                            usuario5.NOMBRE_USUARIO = a.NOMBRES;
                            usuario5.NOMBRE_USUARIO = a.USUARIO;
                            lstUsuariosReplica.Add(usuario5);
                        }

                        grvListaUsuariosReplica.DataSource = lstUsuariosReplica.OrderBy(o => o.NOMBRE_USUARIO);
                        grvListaUsuariosReplica.DataBind();
                        Session[CVariablesSesion.ListaUsuariosReplica] = lstUsuariosReplica;
                    }
                    else
                    {
                        grvListaUsuariosReplica.DataSource = new List<O_LISTA_USUARIOS_ENTIDAD_CTY>();
                        grvListaUsuariosReplica.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }

        protected void pnlUsuarioReplica_Callback(object sender, CallbackEventArgsBase e)
        {
            if (txtUsuarioReplica.Text.Length > 2)
            {
                CargarUsuariosSistemaReplica(5);
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('DEBE ESCRIBIR MAS DE DOS LETRAS PARA BUSCAR USUARIO.');", true);
            }
        }

        protected void pnlUsuarioOctCalNuevo_Callback(object sender, CallbackEventArgsBase e)
        {
            int index = grvListaUsuarioNuevoReplica.FocusedRowIndex;
            if (index >= 0)
            {

                var strUs = grvListaUsuarioNuevoReplica.GetRowValues(index, "NOMBRE_USUARIO").ToString();

                txtUsuarioOctCalNuevo.Text = strUs;

            }
        }

        protected void pnlUsuarioReplicaOctCal_Callback(object sender, CallbackEventArgsBase e)
        {
            int index = grvListaUsuariosReplica.FocusedRowIndex;
            if (index >= 0)
            {

                var strUs = grvListaUsuariosReplica.GetRowValues(index, "NOMBRE_USUARIO").ToString();

                txtUsuarioOctCalReplica.Text = strUs;

            }
        }

        protected void pnlIdUsuarioOctCalNuevo_Callback(object sender, CallbackEventArgsBase e)
        {

            int index = grvListaUsuarioNuevoReplica.FocusedRowIndex;
            if (index >= 0)
            {
                var idUs = grvListaUsuarioNuevoReplica.GetRowValues(index, "ID_USUARIO").ToString();

                txtIdUsuarioOctCalNuevo.Text = idUs;
            }
        }

        protected void pnlIdUsuarioReplicaOctCal_Callback(object sender, CallbackEventArgsBase e)
        {
            int index = grvListaUsuariosReplica.FocusedRowIndex;
            if (index >= 0)
            {
                var idUs = grvListaUsuariosReplica.GetRowValues(index, "ID_USUARIO").ToString();

                txtIdUsuarioOctCalReplica.Text = idUs;
            }
        }

        protected void btnActualizarPermisoOctCalidad_Click(object sender, EventArgs e)
        {
            if (txtIdUsuarioOctCalNuevo.Text != "" && txtIdUsuarioOctCalReplica.Text != "")
            {
                int idAct = Convert.ToInt32(cmbTipoActividad.Value);

                O_DATO_USUARIO_CALIDAD_CTY objUsuario = new O_DATO_USUARIO_CALIDAD_CTY();
                objUsuario.strLlave = CParametrosHydro.StrCredencialOctCal;
                objUsuario.decIdGestionOctano = 6;
                objUsuario.decIdEntidad = 24;
                objUsuario.decIdTipoActividad = 31;
                objUsuario.strIpPermiso = "";
                objUsuario.strAplicacion = "CALIDAD_DRP";
                objUsuario.decFechaFin = 19000101000000;
                objUsuario.strResponsable = "AARANCIBIA";
                objUsuario.strCorreoAnh = "AARANCIBIA@ANH.GOB.BO";
                objUsuario.strSiglaOrganigrama = "245";
                objUsuario.strObjetoUsuarioPruebas = "{'ListaUsuarios':[{'ID_USUARIO_ANH':" + txtIdUsuarioOctCalReplica.Text + ",'ListaReplicas':[{'ID_NUEVO_USUARIO_ANH':" + txtIdUsuarioOctCalNuevo.Text + ",'ID_TIPO_ACTIVIDAD':" + idAct + ",'ID_ENTIDAD':-99999}]}]}";
                objUsuario.decAppIdUsuario = 2475;
                objUsuario.decAccion = 4;

                JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioJson);
                var objResultado = client.Post<O_RESULTADO_CALIDAD_CTY>("/GestionUsuario/?format=json", objUsuario);
                if (objResultado.RESULTADO > 0)
                { MImprimirMensaje(objResultado.MENSAJE_ERROR, EnumTiposMensajeAlerta.Correcto); }
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

    }
}