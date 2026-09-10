using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroSesion;
using AnhClases;
using AnhPresentacionDTEP.Clases.Persona;
using AnhPresentacionDTEP.Entidades.Resultado;
using AnhPresentacionDTEP.Lib;
using AnhPresentacionDTEP.Parametros;
using ServiceStack.ServiceClient.Web;
using DevExpress.Web;

namespace AnhPresentacionDTEP.Sitio.Persona
{
    public partial class wfAutenticacion : System.Web.UI.Page
    {
        #region Variables
        O_USUARIO_CTY _usuario = null;
        private CPersonaSesion _sesionPersona;
        string direccionIp = string.Empty;
        #endregion
        #region Metodos de clase
        public void MostrarMensajeError(string mensaje, bool visible)
        {

            lblMensajeError.Text = mensaje;
            divMensaje.Visible = visible;
        }
        public void Redireccionar()
        {
            //Response.Redirect("~/wfInicio.aspx");
            _sesionPersona = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
            List<O_MODULOS_USUARIO_CTY> modulos = _sesionPersona.ObtenerModulos(_usuario.ID_USUARIO);
            if (modulos != null && modulos.Count > 0)
            {
                O_MODULOS_USUARIO_CTY moduloPrincipal =
                    modulos.Find(m => m.ID_MODULO == CParametrosHydro.decIdAplicacion);
                if (moduloPrincipal != null)
                {
                    List<O_PERFILES_USUARIO_CTY> perfiles = _sesionPersona.ObtenerPerfiles(_usuario.ID_USUARIO, moduloPrincipal.ID_MODULO);
                    if (perfiles != null && perfiles.Count > 0)
                    {
                        Session.Add(CVariablesSesion.objPerfilesUsauario, perfiles);
                        foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
                        {
                            if (perfiles.Find(x => x.DESCRIPCION.Contains("ADMINISTRADOR")) != null)
                            {
                                Session.Add(CVariablesSesion.UsuarioAdministrador, true);
                            }
                            else if (perfil.DESCRIPCION.ToUpper().Trim() == "DTYP_PRODE ADMINISTRADOR")
                            {
                                Session.Add(CVariablesSesion.IsAdminDteyp, true);
                            }

                            if (perfiles.Find(x => x.DESCRIPCION.Contains("SUPER ADMINISTRADOR")) != null)
                            {
                                Session.Add(CVariablesSesion.IsSuperAdministrador, true);
                            }
                            if (perfiles.Find(x => x.DESCRIPCION.Contains("SUPERVISOR OCTANO")) != null)
                            {
                                Session.Add(CVariablesSesion.IsSupervisor, true);
                            }
                            if (perfiles.Find(x => x.DESCRIPCION == "FUNCIONARIO ANH") != null)
                            {
                                Session.Add(CVariablesSesion.IsFuncionario, true);
                            }

                            List<O_MENUS_USUARIO_CTY> menues = _sesionPersona.ObtenerMenusPerfil(perfil.ID_PERFIL);

                            if (menues != null && menues.Count > 0)
                            {
                                List<O_MENUS_USUARIO_CTY> menusPerfilModulo =
                                    menues.FindAll(m => m.ID_MODULO == moduloPrincipal.ID_MODULO);
                                foreach (O_MENUS_USUARIO_CTY menu in menusPerfilModulo.OrderBy(x => x.ORDEN))
                                {
                                    if (menu.ENLACE == "#")
                                    {
                                        continue;
                                    }
                                    //Response.Redirect("~/Inicio/wfInicio.aspx");
                                    Response.Redirect(menu.ENLACE);
                                    break;
                                }
                                foreach (O_MENUS_USUARIO_CTY menu in menues)
                                {
                                    if (menu.ENLACE == "#")
                                    {
                                        continue;
                                    }
                                    //Response.Redirect("~/Inicio/wfInicio.aspx");
                                    Response.Redirect(menu.ENLACE);
                                    break;
                                }
                            }
                        }
                    }
                }
                /*REGISTRA LOG DE ACCESO FALLIDO*/
                //LogAccesos.RegistroAccesoFallido(this.Context, loginUsuario.UserName.ToUpper().Trim(), _usuario.NOMBRE_COMPLETO, moduloPrincipal, modulos[0].URL);
                Response.Redirect(modulos[0].URL);

            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            direccionIp = "de la Direccion IP: " + HttpContext.Current.Request.UserHostAddress;
            HttpBrowserCapabilities browser = Request.Browser;

            //decimal version = Convert.ToDecimal(browser.Version);
            var separador = new char[] { ',', '.' };
            string[] version = browser.Version.Split(separador,
                         StringSplitOptions.RemoveEmptyEntries);

            if ((browser.Browser == "Firefox" && Convert.ToDecimal(version[0]) > 15) || (browser.Browser == "Chrome" && Convert.ToDecimal(version[0]) > 20) || (browser.Browser == "Safari"))
            {

                _sesionPersona = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                if (!IsPostBack)
                {
                    if (Request.QueryString["NPFL"] != null)
                    {
                        MostrarMensajeError("No tiene privilegios suficientes para acceder al sistema, contactese con el administrador.", true);
                    }
                    else
                    {
                        MostrarMensajeError("", false);
                    }

                    loginUsuario.Controls[0].Controls[1].Focus();
                    //if (Request.IsAuthenticated && Session[CVariablesSesion.ObjUsuario] != null)
                    if (Request.IsAuthenticated)
                    {
                        var httpCookie = Request.Cookies[CVariablesSesion.IdAutenticacion];
                        if (httpCookie != null)
                        {
                            decimal decIdUsuario = Convert.ToDecimal(cEncriptacion.DecryptString(httpCookie.Value));

                            O_DETALLE_USUARIO_CTY detalleUsuario = _sesionPersona.ObtenerUsuario(decIdUsuario);

                            if (detalleUsuario != null)
                            {
                                _usuario = new O_USUARIO_CTY
                                {
                                    ID_USUARIO = detalleUsuario.ID_USUARIO,
                                    ID_ENTIDAD = detalleUsuario.ENTIDAD_ID,
                                    NOMBRE_COMPLETO = detalleUsuario.NOMBRE
                                };
                                Session.Add(CVariablesSesion.NombreEntidad, detalleUsuario.ENTIDAD_ID);
                                Session.Add(CVariablesSesion.ObjUsuario, _usuario);
                                Session.Add(CVariablesSesion.IdUsuario, detalleUsuario.ID_USUARIO);
                                Session.Add(CVariablesSesion.DatosUsuario, detalleUsuario.NOMBRE + " " + detalleUsuario.PRIMER_APELLIDO + " " + detalleUsuario.SEGUNDO_APELLIDO);

                                Redireccionar();
                            }
                        }
                        else
                        {
                            FormsAuthentication.SignOut();
                            Session.RemoveAll();
                        }
                    }
                }

            }
            else
            {
                Response.Redirect("~/Sitio/Persona/wfNavegadores.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        protected void loginUsuario_Authenticate(object sender, AuthenticateEventArgs e)
        {
             var captcha = (sender as Login).FindControl("CaptchaLogin") as ASPxCaptcha;
            bool resultadocaptcha = captcha.IsValid;
            if (!resultadocaptcha) {
                e.Authenticated = false;
                MostrarMensajeError("*Captcha Incorrecto", true);
            }
            else {

                Page.Validate("loginValidation");
                try
                {
                    if (Page.IsValid)
                    {
                        string strUsuario = loginUsuario.UserName.ToUpper().Trim();
                        string strUsuarioEntidad = strUsuario;
                        string strClave = loginUsuario.Password;

                        if (!strUsuario.Contains("@") || strUsuario.EndsWith("ANH.GOB.BO"))
                        { // SI ES FUNCIONARIO
                            _usuario = _sesionPersona.AutenticarActDir(strUsuario, strClave);

                            if (_usuario != null)
                            {
                                Session.Add(CVariablesSesion.PerfilUsuario, _usuario.PERFIL);
                                //Obtiene datos del funcionario y almacena en variable de session
                                JsonServiceClient clienteSirh = new JsonServiceClient(ServiciosRest.ServicioJsonSIRH);
                                var listaResultado =
                                    clienteSirh.Get<EResultadoLista<O_DATOS_FUNCIONARIO_CTY>>(
                                        ServiciosRest.SvcDatosFuncionarioANH + CParametrosHydro.StrCredencialRrhh + "/ "
                                        + _usuario.ID_USUARIO + "?format=json");
                                O_DATOS_FUNCIONARIO_CTY funcionarioAnh = listaResultado.oResultado.FirstOrDefault();
                                if (funcionarioAnh != null)
                                {
                                    Session.Add(CVariablesSesion.ObjFuncionarioANH, funcionarioAnh);
                                    Session.Add(CVariablesSesion.srtCorreo, funcionarioAnh.USUARIO_DOMINIO.ToUpper());
                                }
                                //Guarda los datos del usuaio en una variable de sessión
                                if (_usuario.PERFIL_ID >= 0 && _usuario.ESTADO > 0)
                                {
                                    e.Authenticated = true;
                                    Session[CVariablesSesion.UsuarioId] = _usuario.ID_USUARIO;
                                    Session.Add(CVariablesSesion.ObjUsuario, _usuario);
                                    Session.Add(CVariablesSesion.DatosUsuario, _usuario.NOMBRE_COMPLETO);
                                }
                                else
                                {
                                    switch (Convert.ToInt32(_usuario.ID_USUARIO))
                                    {
                                        case -1:
                                            Session.Add("login", strUsuario);
                                            Session.Add("Pass", strClave);
                                            Response.Redirect("~/Sitio/Autenticacion/wfSalir.aspx");
                                            CLog.Informacion(
                                                this.Context,
                                                "Usuario que debe Registrarse: usuario" + strUsuario + " Perfil"
                                                + _usuario.PERFIL + " Nombre Completo" + _usuario.NOMBRE_COMPLETO + " "
                                                + direccionIp);

                                            break;
                                        default:
                                            MostrarMensajeError(
                                                "El usuario ingresado no tiene permisos para acceder a este módulo.",
                                                true);
                                            e.Authenticated = false;
                                            CLog.Error(
                                                this.Context,
                                                GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                null);
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                MostrarMensajeError("* Usuario y/o clave incorrectos.", true);
                                e.Authenticated = false;
                            }
                            //    else
                            //    {
                            //        strClave = cEncriptacion.generarMD5(loginUsuario.Password).ToUpper();
                            //        _usuario = _sesionPersona.Autenticar(strUsuarioEntidad, strClave);
                            //        if (_usuario != null)
                            //        {

                            //            if (_usuario.ID_USUARIO > 0)
                            //            {
                            //                if (_usuario.ESTADO > 0)
                            //                {
                            //                    e.Authenticated = true;
                            //                    Session.Add(CVariablesSesion.ObjUsuario, _usuario);
                            //                    if (_usuario.ID_ENTIDAD > 0)
                            //                    {
                            //                        Session.Add(CVariablesSesion.IdEntidad, _usuario.ID_ENTIDAD);
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    MostrarMensajeError("Esta cuenta  no esta activada, ingrese a su cuenta de correo para activar su cuenta.", true);
                            //                    loginUsuario.FailureAction = LoginFailureAction.Refresh;
                            //                    //_log.Error("Esta cuenta  no esta activada, ingrese a su cuenta de correo para activar su cuenta. " + strUsuario + " " + direccionIp);                                        
                            //                }
                            //            }
                            //            else
                            //            {
                            //                MostrarMensajeError("* Usuario y/o clave incorrectos.", true);
                            //                e.Authenticated = false;
                            //                //_log.Error("* Usuario y/o clave incorrectos. " + strUsuario + " " + direccionIp);                                    
                            //            }
                            //        }
                            //        else
                            //        {
                            //            MostrarMensajeError("* Usuario y/o clave incorrectos.", true);
                            //            e.Authenticated = false;
                            //            //_log.Error("* Usuario y/o clave incorrectos. " + strUsuario + " " + direccionIp);
                            //        }
                            //    }
                        }
                        else
                        {
                            strClave = cEncriptacion.generarMD5(loginUsuario.Password).ToUpper();
                            _usuario = _sesionPersona.Autenticar(strUsuarioEntidad, strClave);
                            if (_usuario != null)
                            {
                                if (_usuario.ID_USUARIO > 0)
                                {
                                    if (_usuario.ESTADO > 0)
                                    {
                                        e.Authenticated = true;
                                        Session[CVariablesSesion.UsuarioId] = _usuario.ID_USUARIO;
                                        Session.Add(CVariablesSesion.ObjUsuario, _usuario);
                                        Session.Add(CVariablesSesion.srtCorreo, strUsuario.ToUpper());
                                        Session.Add(CVariablesSesion.DatosUsuario, _usuario.NOMBRE_COMPLETO);
                                        //_log.Info(
                                        //    "Usuario que Ingreso al Sistema: usuario: " + strUsuario + " Perfil: "
                                        //    + _usuario.PERFIL + " Nombre Completo: " + _usuario.NOMBRE_COMPLETO + " "
                                        //    + direccionIp);
                                        if (_usuario.ID_ENTIDAD > 0)
                                        {
                                            Session.Add(CVariablesSesion.IdEntidad, _usuario.ID_ENTIDAD);
                                            Session.Add(CVariablesSesion.NombreEntidad, _usuario.ID_ENTIDAD);
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensajeError(
                                            "Esta cuenta  no esta activada, ingrese a su cuenta de correo para activar su cuenta.",
                                            true);
                                        loginUsuario.FailureAction = LoginFailureAction.Refresh;
                                        //_log.Error("Esta cuenta  no esta activada, ingrese a su cuenta de correo para activar su cuenta. " + strUsuario + " " + direccionIp);

                                    }
                                }
                                else
                                {
                                    MostrarMensajeError("* Usuario y/o clave incorrectos.", true);
                                    e.Authenticated = false;
                                    //_log.Error("* Usuario y/o clave incorrectos. " + strUsuario + " " + direccionIp);
                                }
                            }
                            else
                            {
                                MostrarMensajeError("* Usuario y/o clave incorrectos.", true);
                                e.Authenticated = false;
                                //_log.Error("* Usuario y/o clave incorrectos. " + strUsuario + " " + direccionIp);
                            }
                        }

                    }
                }

                catch (System.Threading.ThreadAbortException)
                { }
                catch (Exception exp)
                {

                    MostrarMensajeError("** Usuario y/o clave incorrectos.", true);
                    loginUsuario.FailureAction = LoginFailureAction.Refresh;
                    CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                }

            }
            
       
        }

        protected void loginUsuario_LoggedIn(object sender, EventArgs e)
        {
            if (_usuario != null)
            {
                HttpCookie cookie = new HttpCookie(CVariablesSesion.IdAutenticacion, cEncriptacion.EncryptString(_usuario.ID_USUARIO.ToString()));
                if (loginUsuario.RememberMeSet)
                {
                    cookie.Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                }
                Response.Cookies.Add(cookie);

                Redireccionar();
            }
        }
        #endregion


        //public void mRedireccionar()
        //{
        //    Session.Add(CVariablesSesion.IsSuperAdministrador, false);
        //    Session.Add(CVariablesSesion.IsAdminDteyp, false);

        //    List<O_MODULOS_USUARIO_CTY> modulos = _sesion.ObtenerModulos(_usuario.ID_USUARIO);
        //    if (modulos != null && modulos.Count > 0)
        //    {
        //        O_MODULOS_USUARIO_CTY moduloPrincipal = modulos.Find(m => m.ID_MODULO == CParametrosHydro.decIdAplicacion);
        //        if (moduloPrincipal != null)
        //        {
        //            List<O_PERFILES_USUARIO_CTY> perfiles = _sesion.ObtenerPerfiles(_usuario.ID_USUARIO, moduloPrincipal.ID_MODULO);
        //            if (perfiles != null && perfiles.Count > 0)
        //            {
        //                Session.Add(CVariablesSesion.PerfilUsuario, perfiles);
        //                foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
        //                {
        //                    //if (perfil.DESCRIPCION.ToUpper().Trim() == "ADMINISTRADOR DUCTOS")
        //                    if (perfiles.Find(x => x.DESCRIPCION.Contains("ADMINISTRADOR")) != null)
        //                    {
        //                        Session.Add(CVariablesSesion.UsuarioAdministrador, true);
        //                        //Session.Add(CVariablesSesion.IsSuperAdministrador, true);
        //                        //Session.Add(CVariablesSesion.IsAdminDteyp, true);
        //                    }
        //                    else if (perfil.DESCRIPCION.ToUpper().Trim() == "DTYP_PRODE ADMINISTRADOR")
        //                    {
        //                        //Session.Add(CVariablesSesion.IsAdminTransporte, true);
        //                        Session.Add(CVariablesSesion.IsAdminDteyp, true);
        //                    }

        //                    if (perfiles.Find(x => x.DESCRIPCION.Contains("SUPER ADMINISTRADOR")) != null)
        //                    {
        //                        Session.Add(CVariablesSesion.IsSuperAdministrador, true);
        //                    }

        //                    if (perfiles.Find(x => x.DESCRIPCION == "FUNCIONARIO ANH") != null)
        //                    {
        //                        Session.Add(CVariablesSesion.IsFuncionario, true);
        //                    }


        //                    List<O_MENUS_USUARIO_CTY> menues = _sesion.ObtenerMenusPerfil(perfil.ID_PERFIL);
        //                    if (menues != null && menues.Count > 0)
        //                    {
        //                        List<O_MENUS_USUARIO_CTY> menusPerfilModulo = menues.FindAll(m => m.ID_MODULO == moduloPrincipal.ID_MODULO);
        //                        foreach (O_MENUS_USUARIO_CTY menu in menusPerfilModulo)
        //                        {
        //                            if (menu.ENLACE == "#")
        //                            {
        //                                continue;
        //                            }
        //                            //Response.Redirect(menu.ENLACE);
        //                            Response.Redirect("~/Sitio/wfInicio.aspx");
        //                            break;
        //                        }
        //                        foreach (O_MENUS_USUARIO_CTY menu in menues)
        //                        {
        //                            if (menu.ENLACE == "#")
        //                            {
        //                                continue;
        //                            }
        //                            Response.Redirect(menu.ENLACE);
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            //TODO: verificar el funcionamiento de la siguiente linea. Posible no uso. TALVES A CORRESPONDENCIA
        //            //Response.Redirect("~/Sitio/ANH/wfAnhListaRegulados.aspx");
        //        }
        //        Response.Redirect(modulos[0].URL);
        //    }
        //    if (_usuario.ID_ENTIDAD == 0)
        //    {
        //        Response.Redirect(CVariablesSesion.LinkSireHydro.ToString());
        //    }
        //    else
        //    {
        //        Session.Add(CVariablesSesion.IdEntidad, _usuario.ID_ENTIDAD);
        //        Response.Redirect("~/Sitio/VolumenesCalidad/ReportesCalidad/wfRptCertificadoCalidad.aspx");
        //    }
        //}

    }

       
}