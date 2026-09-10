using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroSesion;
using AnhAgenteServicios.EntidadesServicios.Sirh;
using AnhPresentacionDTEP.Entidades.Resultado;
using AnhPresentacionDTEP.Lib;
using AnhPresentacionDTEP.Parametros;
using CPersonaSesion = AnhPresentacionDTEP.Clases.Persona.CPersonaSesion;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad
{
    using System.Reflection;
    using System.Web.Security;
    using System.Web.UI.HtmlControls;

    using ServiceStack.ServiceClient.Web;

    public partial class CantidadesMaster : System.Web.UI.MasterPage
    {
        #region Variables de entorno
        /// <summary>
        /// El identificador del usuario sesionado.
        /// </summary>
        private int _intIdUsuario;

        /// <summary>
        /// El gestor del servicio Hydro Sesion
        /// </summary>
        private CPersonaSesion _sesion;

        /// <summary>
        /// Recurso de registro de errores de la aplicación.
        /// </summary>
        private string _mensajeError;
        private IServicioHydroSesion _servicio;
        #endregion
        #region Metodos
        private void MCargarImagenPerfil()
        {
            try
            {
                JsonServiceClient clienteSirh = new JsonServiceClient(ServiciosRest.ServicioJsonSIRH);
                var datosFuncionario = CVariablesSesion.FuncionarioAnh(this.Context);
                if (datosFuncionario != null)
                {
                    lblUnidad.Text = datosFuncionario.DIRECCION;
                    lblCorreoInstitucional.Text = datosFuncionario.USUARIO_DOMINIO.ToLower();
                    var objImagen =
                        clienteSirh.Get<EResultadoEntidad<O_ARCHIVO_DIGITAL_CTY>>(
                            ServiciosRest.SvcFotoPerfilFuncionario + CParametrosHydro.StrCredencialRrhh + "/"
                            + Convert.ToDecimal(datosFuncionario.FOTO_DIGITAL_ID) + "?format=json");
                    if (objImagen.oResultado != null)
                    {
                        if (objImagen.oResultado.ARCHIVO_BINARIO.Length > 0)
                        {
                            imgImagenPerfil.ImageUrl = "data:image/jpeg;base64,"
                                                       + Convert.ToBase64String(objImagen.oResultado.ARCHIVO_BINARIO);

                        }
                        else
                        {
                            imgImagenPerfil.ImageUrl = @"~/UI/img/silueta.jpg";
                        }
                    }
                }
                else
                {
                    imgImagenPerfil.ImageUrl = @"~/UI/img/silueta.jpg";
                    //cargar datos del usuario regulado.
                }
            }
            catch (Exception exp)
            {
                CLog.Error(this.Context, "MCargarImagenPerfil", exp);
            }
        }
        private void MCargarModulos()
        {
            try
            {
                List<O_MODULOS_USUARIO_CTY> modulos = _sesion.ObtenerModulos(_intIdUsuario);
                if (modulos != null)
                {
                    menuHydro.Controls.Clear();
                    foreach (O_MODULOS_USUARIO_CTY modulo in modulos)
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        HtmlGenericControl enlace = new HtmlGenericControl("a");
                        enlace.Attributes.Add("href", modulo.URL);
                        enlace.InnerText = modulo.DESCRIPCION;
                        li.Controls.Add(enlace);
                        menuHydro.Controls.Add(li);
                    }
                    //lblModulo.Text =
                    //    modulos.FirstOrDefault(w => w.ID_MODULO == CParametrosHydro.decIdAplicacion).DESCRIPCION.ToUpper();
                }
            }
            catch (Exception exp)
            {
                CLog.Error(this.Context, "MCargarModulos", exp);
            }
        }

        private void MCargarUsuario()
        {
            lblUsuario.Text = string.Empty;
            _servicio = LocalizadorProxy.HydroSesion();
            if (Session[CVariablesSesion.ObjUsuario] != null)
            {
                lblUsuario.Text = CVariablesSesion.Usuario(this.Context).NOMBRE_COMPLETO.ToString();
                try
                {
                    menu1.Items.Clear();
                    List<O_MENUS_USUARIO_CTY> menues = _sesion.ObtenerMenus(CParametrosHydro.decIdAplicacion, _intIdUsuario);
                    //List<O_MENUS_USUARIO_CTY> menues = _servicio.ObtenerMenusUsuario(_intIdUsuario, cParametrosHydro.decIdAplicacion, 0, cParametrosHydro.strCredencialDOIH,ref _mensajeError);
                    if (menues != null && menues.Count > 0)
                    {
                        List<O_PERFILES_USUARIO_CTY> perfiles = _sesion.ObtenerPerfiles(_intIdUsuario, CParametrosHydro.decIdAplicacion);
                        if (perfiles != null)
                        {
                            foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
                            {
                                if (perfil.ID_PERFIL == CParametrosHydro.IdFuncionarioAnh)
                                {
                                    List<O_MENUS_USUARIO_CTY> menusPerfil = _sesion.ObtenerMenusPerfil(CParametrosHydro.IdFuncionarioAnh);
                                    foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfil)
                                    {
                                        if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                        {
                                            int intPosicion = 0;
                                            foreach (O_MENUS_USUARIO_CTY tempMenu in menues)
                                            {
                                                if (tempMenu.ORDEN >= menuPerfil.ORDEN)
                                                {
                                                    break;
                                                }
                                                intPosicion++;
                                            }
                                            menues.Insert(intPosicion, menuPerfil);
                                        }
                                    }
                                }
                            }
                        }
                        menues = EliminaDuplicado(menues);
                        List<O_MENUS_USUARIO_CTY> menuesPadre = menues.FindAll(m => m.ID_MENU_PADRE == 0);
                        string texto = string.Empty;
                        foreach (O_MENUS_USUARIO_CTY menuPadre in menuesPadre)
                        {
                            MenuItem itemPadre = MCargarMenu(menues, menuPadre);
                            menu1.Items.Add(itemPadre);
                        }
                        //panelMenu.Visible = true;
                    }
                    else
                    { // SI NO TIENE MENUS ASIGNADOS - EJ. REGULADOS Y FUNCIONARIOS SIN ASIGNACION ESPECIFICA
                        menues = new List<O_MENUS_USUARIO_CTY>();
                        List<O_PERFILES_USUARIO_CTY> perfiles = _sesion.ObtenerPerfiles(_intIdUsuario,
                                                                                        CParametrosHydro.decIdAplicacion);
                        if (perfiles != null && perfiles.Count > 0)
                        {
                            foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
                            {
                                if (perfil.ID_PERFIL == CParametrosHydro.IdFuncionarioAnh)
                                {
                                    List<O_MENUS_USUARIO_CTY> menusPerfil = _sesion.ObtenerMenusPerfil(CParametrosHydro.IdFuncionarioAnh);
                                    foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfil)
                                    {
                                        if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                        {
                                            menues.Add(menuPerfil);
                                        }
                                    }
                                }
                                if (perfil.ID_PERFIL == CParametrosHydro.IdRegulado)
                                {
                                    // obtener perfiles hijo de regulado
                                    List<O_PERFILES_USUARIO_CTY> perfilesHijo = perfiles.FindAll(p => /*ID_PADRE*/p.ID_PERFIL == CParametrosHydro.IdRegulado);
                                    // sumar menu PERFIL_MENU de regulado (for)
                                    foreach (O_PERFILES_USUARIO_CTY perfilHijo in perfilesHijo)
                                    {
                                        List<O_MENUS_USUARIO_CTY> menusPerfilHijo = _sesion.ObtenerMenusPerfil(perfilHijo.ID_PERFIL);
                                        foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfilHijo)
                                        {
                                            if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                            {
                                                menues.Add(menuPerfil);
                                            }
                                        }
                                    }
                                    // sumar menu PERFIL_MENU de regulado
                                    /*List<O_MENUS_USUARIO_CTY> menusPerfil = _sesion.obtenerMenusPerfil(CVariablesSesion.IdRegulado);
                                    foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfil)
                                    {
                                        if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                        {
                                            menues.Add(menuPerfil);
                                        }
                                    }*/
                                }
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx?NPFL=0");
                        }

                        menues = EliminaDuplicado(menues);
                        menues = menues.OrderBy(m => m.ORDEN).ToList();
                        List<O_MENUS_USUARIO_CTY> menuesPadre = menues.FindAll(m => m.ID_MENU_PADRE == 0);
                        foreach (O_MENUS_USUARIO_CTY menuPadre in menuesPadre)
                        {
                            MenuItem itemPadre = MCargarMenu(menues, menuPadre);
                            menu1.Items.Add(itemPadre);
                        }

                    }
                }
                catch (Exception exp)
                {
                    CLog.Error(this.Context, "MCargarUsuario", exp);
                    Response.Redirect("~/Sitio/Persona/wfSalir.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private List<O_MENUS_USUARIO_CTY> EliminaDuplicado(List<O_MENUS_USUARIO_CTY> menuesPadre)
        {
            Dictionary<decimal, decimal> uniqueStore = new Dictionary<decimal, decimal>();
            List<O_MENUS_USUARIO_CTY> finalList = new List<O_MENUS_USUARIO_CTY>();
            foreach (O_MENUS_USUARIO_CTY gra in menuesPadre)
            {
                if (!uniqueStore.ContainsKey(gra.ID_MENU))
                {
                    uniqueStore.Add(gra.ID_MENU, 0);
                    finalList.Add(gra);
                }
            }
            return finalList;
        }

        public MenuItem MCargarMenu(List<O_MENUS_USUARIO_CTY> menues, O_MENUS_USUARIO_CTY menuPadre)
        {

            MenuItem itemPadre = new MenuItem(menuPadre.TITULO) { NavigateUrl = menuPadre.ENLACE };
            List<O_MENUS_USUARIO_CTY> menuesHijo = menues.FindAll(m => m.ID_MENU_PADRE == menuPadre.ID_MENU);
            foreach (O_MENUS_USUARIO_CTY menuHijo in menuesHijo)
            {
                itemPadre.Selectable = false;
                MenuItem itemHijo = MCargarMenu(menues, menuHijo);
                itemPadre.ChildItems.Add(itemHijo);
            }
            return itemPadre;
        }

        protected void btnSalir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Sitio/Persona/wfSalir.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            string direccionIp = "la Direccion IP: " + HttpContext.Current.Request.UserHostAddress;
            FormsAuthentication.SignOut();
            Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
            Session.RemoveAll();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session[CVariablesSesion.ObjUsuario] != null)
                {
                    _intIdUsuario = Int32.Parse(CVariablesSesion.UsuarioSession(this.Context).ToString());

                    _sesion = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                    MCargarUsuario();
                    MCargarModulos();
                    MCargarImagenPerfil();
                    /*if (!IsPostBack)
                    {
                        MCargarUsuario();
                        panelUsuario.Visible = true;
                        MCargarModulos();
                    }
                    if (Session[CVariablesSesion.IdEntidad] != null)
                    {
                        panelMenu.Visible = true;
                        menuRegulado.Visible = true;
                    }*/
                }
                else
                {
                    Response.Redirect("~/Sitio/Persona/wfSalir.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + MethodBase.GetCurrentMethod().Name, ex);
                Response.Redirect("~/Sitio/Persona/wfSalir.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }


        }

        #endregion
    }
}