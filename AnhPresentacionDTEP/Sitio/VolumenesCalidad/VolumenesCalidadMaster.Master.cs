using AnhAgenteServicios.ServicioHydroSesion;
using AnhPresentacionDTEP.Clases.Persona;
using AnhPresentacionDTEP.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad
{
    public partial class VolumenesCalidadMaster : System.Web.UI.MasterPage
    {

        /// <summary>
        /// El identificador del usuario sesionado.
        /// </summary>
        private int _intIdUsuario;
        /// <summary>
        /// El gestor del servicio Hydro Sesion
        /// </summary>
        private CPersonaSesion _sesion;

        /// <summary>
        /// Valida la sesion de usuario e inicializa los parámetros y demás recursos necesarios para mostrar en la pantalla.
        /// </summary>
        /// <param name="sender">El objeto que instancia a esta pantalla.</param>
        /// <param name="e">La instancia de System.EventArgs que inicia esta pantalla.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[CVariablesSesion.UsuarioId] != null && Request.IsAuthenticated)
                //if (Session[CVariablesSesion.UsuarioId] != null)
                {
                    _intIdUsuario = Int32.Parse(Session[CVariablesSesion.UsuarioId].ToString());
                    _sesion = new CPersonaSesion(Server.MapPath("~"));
                    mCargarUsuario();
                    panelUsuario.Visible = true;
                    mCargarModulos();
                    menuRegulado.Visible = false;
                    if (Session[CVariablesSesion.IdEntidad] != null)
                    {
                        panelMenu.Visible = true;
                        //menuRegulado.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Muestra los módulos del sistema a los que el usuario sesionado tiene acceso.
        /// </summary>
        private void mCargarModulos()
        {
            try
            {
                List<O_MODULOS_USUARIO_CTY> modulos = _sesion.obtenerModulos(_intIdUsuario);
                if (modulos != null)
                {
                    foreach (O_MODULOS_USUARIO_CTY modulo in modulos)
                    {
                        MenuItem itemModulo = new MenuItem(modulo.DESCRIPCION);
                        itemModulo.NavigateUrl = modulo.URL;
                        menuModulo.Items.Add(itemModulo);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Muestra los datos del usuario sesionado e inicia el cargado de menus.
        /// </summary>
        private void mCargarUsuario()
        {
            if (Session[CVariablesSesion.DatosUsuario] != null)
            {
                lblUsuario.Text = Session[CVariablesSesion.DatosUsuario].ToString();

                if (Session[CVariablesSesion.NombreEntidad] != null)
                {
                    lblEntidad.Text = Session[CVariablesSesion.NombreEntidad].ToString();    
                }
                else
                {
                    lblEntidad.Text = "AGENCIA NACIONAL DE HIDROCARBUROS";
                    Session[CVariablesSesion.NombreEntidadAgencia] = lblEntidad.Text;
                }

                try
                {
                    menu.Items.Clear();
                    //obtener menus asignados en USUARIO_MENU
                    List<O_MENUS_USUARIO_CTY> menues = _sesion.obtenerMenus(CParametrosHydro.decIdAplicacion, _intIdUsuario);
                    if (menues != null && menues.Count > 0)
                    {
                        // SI NO TIENE MENUS ASIGNADOS - EJ. FUNCIONARIOS CON ASIGNACION ESPECIFICA
                        List<O_PERFILES_USUARIO_CTY> perfiles = _sesion.obtenerPerfiles(_intIdUsuario,
                                                                                        CParametrosHydro.decIdAplicacion);
                        List<O_PERFILES_USUARIO_CTY> buscaAdministrador =
                            perfiles.FindAll((p => p.DESCRIPCION == "OCTANO ADMINISTRADOR"));

                        if (buscaAdministrador.Count > 0)
                        {
                            Session.Add(CVariablesSesion.UsuarioAdministrador, 1);
                        }
                        else
                        {
                            Session.Add(CVariablesSesion.UsuarioAdministrador, 0);
                        }
                        
                        if (perfiles != null)
                        {
                            foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
                            {
                                if (perfil.ID_PERFIL == CVariablesSesion.IdFuncionarioAnh)
                                {
                                    List<O_MENUS_USUARIO_CTY> menusPerfil = _sesion.obtenerMenusPerfil(CVariablesSesion.IdFuncionarioAnh);
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

                        List<O_MENUS_USUARIO_CTY> menuesPadre = menues.FindAll(m => m.ID_MENU_PADRE == 0);
                        var menuesPadreEli = new List<O_MENUS_USUARIO_CTY>();
                        menuesPadreEli = EliminaDuplicado(menuesPadre);
                        foreach (O_MENUS_USUARIO_CTY menuPadre in menuesPadreEli)
                        {
                            MenuItem itemPadre = mCargarMenu(menues, menuPadre);
                            menu.Items.Add(itemPadre);
                        }
                        panelMenu.Visible = true;
                    }
                    else
                    { // SI NO TIENE MENUS ASIGNADOS - EJ. REGULADOS Y FUNCIONARIOS SIN ASIGNACION ESPECIFICA
                        menues = new List<O_MENUS_USUARIO_CTY>();
                        List<O_PERFILES_USUARIO_CTY> perfiles = _sesion.obtenerPerfiles(_intIdUsuario,
                                                                                        CParametrosHydro.decIdAplicacion);
                        if (perfiles != null)
                        {
                            foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
                            {
                                if (perfil.ID_PERFIL == CVariablesSesion.IdFuncionarioAnh)
                                {
                                    List<O_MENUS_USUARIO_CTY> menusPerfil = _sesion.obtenerMenusPerfil(CVariablesSesion.IdFuncionarioAnh);
                                    foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfil)
                                    {
                                        if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                        {
                                            menues.Add(menuPerfil);
                                        }
                                    }
                                }
                                if (perfil.ID_PERFIL_PADRE == CVariablesSesion.IdRegulado)
                                {
                                    // obtener perfiles hijo de regulado
                                    List<O_PERFILES_USUARIO_CTY> perfilesHijo =
                                        perfiles.FindAll(p => p.ID_PERFIL_PADRE == CVariablesSesion.IdRegulado);
                                    // sumar menu PERFIL_MENU de regulado (for)
                                    foreach (O_PERFILES_USUARIO_CTY perfilHijo in perfilesHijo)
                                    {
                                        List<O_MENUS_USUARIO_CTY> menusPerfilHijo =
                                            _sesion.obtenerMenusPerfil(perfilHijo.ID_PERFIL);
                                        foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfilHijo)
                                        {
                                            if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                            {
                                                menues.Add(menuPerfil);
                                            }
                                        }
                                    }
                                    // sumar menu PERFIL_MENU de regulado
                                    List<O_MENUS_USUARIO_CTY> menusPerfilRegulado =
                                        _sesion.obtenerMenusPerfil(CVariablesSesion.IdRegulado);

                                    if (menusPerfilRegulado != null)
                                    {
                                        foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfilRegulado)
                                        {
                                            if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                            {
                                                menues.Add(menuPerfil);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        menues = menues.OrderBy(m => m.ORDEN).ToList();
                        List<O_MENUS_USUARIO_CTY> menuesPadre = menues.FindAll(m => m.ID_MENU_PADRE == 0);
                        var menuesPadreEli = new List<O_MENUS_USUARIO_CTY>();
                        menuesPadreEli = EliminaDuplicado(menuesPadre);
                        foreach (O_MENUS_USUARIO_CTY menuPadre in menuesPadreEli)
                        {
                            MenuItem itemPadre = mCargarMenu(menues, menuPadre);
                            menu.Items.Add(itemPadre);
                        }
                        panelMenu.Visible = true;
                    }
                }
                catch (Exception)
                {
                    //_logs.Error(exp);
                }
            }
        }

        /// <summary>
        /// Crea recursivamente los menus del usuario.
        /// </summary>
        /// <param name="menues">Todos los menús del usuario.</param>
        /// <param name="menuPadre">El menu padre para el inicio del proceso de creación.</param>
        /// <returns></returns>
        public MenuItem mCargarMenu(List<O_MENUS_USUARIO_CTY> menues, O_MENUS_USUARIO_CTY menuPadre)
        {
            MenuItem itemPadre = new MenuItem(menuPadre.TITULO);
            itemPadre.NavigateUrl = menuPadre.ENLACE;
            List<O_MENUS_USUARIO_CTY> menuesHijo = menues.FindAll(m => m.ID_MENU_PADRE == menuPadre.ID_MENU);
            foreach (O_MENUS_USUARIO_CTY menuHijo in menuesHijo)
            {
                itemPadre.Selectable = false;
                MenuItem itemHijo = mCargarMenu(menues, menuHijo);
                itemPadre.ChildItems.Add(itemHijo);
            }
            return itemPadre;
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

        /// <summary>
        /// Redirige a la pantalla de salida de la aplicación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Sitio/Persona/wfSalir.aspx");
        }

        /// <summary>
        /// Cierra la sesión del usuario y elimina las cookies creadas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
            Session.RemoveAll();
        }

    }
}