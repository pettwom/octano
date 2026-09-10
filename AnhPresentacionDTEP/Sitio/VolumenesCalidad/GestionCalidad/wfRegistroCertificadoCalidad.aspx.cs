using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI.WebControls;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioConsultasHydro;
using Librerias.Anh.Us;
using ServiceStack.ServiceHost;
using AnhPresentacionDTEP.Entidades;
using ServiceStack.ServiceClient.Web;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Comun.UControl;
using System.Web.Security;
using System.IO;



namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad
{
    using System.Drawing;

    using AnhPersistenciaCore.Core;
    using AnhPersistenciaCore.Entidades.Parametros.VolumenesCalidad;

    using DevExpress.Web;
    using DevExpress.XtraCharts.Native;

    public partial class wfRegistroCertificadoCalidad : System.Web.UI.Page
    {
        #region variables
        JsonServiceClient cliente = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
        string strMensajeError = "";
        string strAccion = "";

        public decimal decIdUsuario
        {
            get { return Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.UsuarioId]); }
        }

        public decimal decIdEntidad
        {
            get { return Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.IdEntidad]); }
        }
        #endregion

        #region Metodos Cargado de arbol
        private List<O_LISTA_ARBOL_ENTIDADES_CTY> ListarArbolEntidades()
        {
            List<O_LISTA_ARBOL_ENTIDADES_CTY> lstResultado = new List<O_LISTA_ARBOL_ENTIDADES_CTY>();
            if (Convert.ToDecimal(Session[CVariablesSesion.IsSuperAdministrador]) == 1 && Convert.ToBoolean(Session[CVariablesSesion.IsFuncionario])) // es usuaio anh carga el siguiente menu
            {
                lstResultado =
                    cliente.Get<List<O_LISTA_ARBOL_ENTIDADES_CTY>>(
                        "/ListarArbolEntidades/" + cParametrosHydro.strCredencial + "/0/0/" + decIdUsuario
                        + "/1?format=json");
            }
            else
            {

                lstResultado =
                    cliente.Get<List<O_LISTA_ARBOL_ENTIDADES_CTY>>(
                        "/ListarArbolEntidades/" + cParametrosHydro.strCredencial + "/" + 0 + "/0/" + decIdUsuario
                        + "/2?format=json");
            }
            return lstResultado;
        }
        private List<O_PROD_ESPEC_PACL_CTY> ListarGruposProductoEspecificacionTipoCalidad(decimal idEntidad, decimal idTipoActividad)
        {
            List<O_PROD_ESPEC_PACL_CTY> objResultado = new List<O_PROD_ESPEC_PACL_CTY>();
            objResultado = cliente.Get<List<O_PROD_ESPEC_PACL_CTY>>("/ListarGruposProductoEspecificacionTipoCalidad/" + cParametrosHydro.strCredencial + "/" + idEntidad + "/" + idTipoActividad + "/" + decIdUsuario + "?format=json");
            return objResultado;
        }

        private List<O_PROD_CAL_CTY> ListarGruposProductoEspecificacionCalidad(decimal idTipoActividad, decimal tipoProducto)
        {
            List<O_PROD_CAL_CTY> vColProductoEspecificacion = new List<O_PROD_CAL_CTY>();
            vColProductoEspecificacion = cliente.Get<List<O_PROD_CAL_CTY>>("/ListarGruposProductoEspecificacionCalidad/" + cParametrosHydro.strCredencial + "/" + decIdUsuario + "/" + idTipoActividad + "/" + tipoProducto + "/APP_OCT?format=json");
            return vColProductoEspecificacion;
        }

        //protected int ObtenerTipoPrueba()
        //{
        //    List<O_ESTADO_TIPO_PRUEBA_CTY> lstResultado = cliente.Get<List<O_ESTADO_TIPO_PRUEBA_CTY>>("/ObtenerTipoPrueba/" + cParametrosHydro.strCredencial + "/" + decIdUsuario + "?format=json");
        //    if (lstResultado != null && lstResultado.Count == 1)
        //    {
        //        return Convert.ToInt32(lstResultado[0].ESTADO_TIPO_PRUEBA);
        //    }
        //    return 0;
        //}


        /// <summary>
        /// Carga nodos con los departamentos de bolivia
        /// </summary>
        private void CargarMenuArbol()
        {
            int nivel = 1;

            List<O_LISTA_ARBOL_ENTIDADES_CTY> listaArbol = ListarArbolEntidades();
            List<O_LISTA_ARBOL_ENTIDADES_CTY> ListaHijos = listaArbol.Where(w => w.PADRE.Equals("0")).ToList();
            foreach (var item in ListaHijos)
            {
                TreeViewNode menuArbol = new TreeViewNode(item.DESCRIPCION, item.IDENTIFICADOR.ToString());
                CargarArbolOpciones(listaArbol, item.HIJO, menuArbol, nivel);
                //menuArbol.ClientEnabled = false;
                menuArbol.TextStyle.ForeColor = Color.Black;
                arbolMenu.Nodes.Add(menuArbol);
            }
            arbolMenu.ExpandAll(); 
        }
        /// <summary>
        /// Funcion recursiva para cargar el menu.
        /// </summary>
        /// <param name="listaArbol"></param>
        /// <param name="padre"></param>
        /// <param name="nodoPadre"></param>
        /// <param name="nivel"></param>
        private void CargarArbolOpciones(List<O_LISTA_ARBOL_ENTIDADES_CTY> listaArbol, string padre, TreeViewNode nodoPadre, int nivel)
        {
            List<O_LISTA_ARBOL_ENTIDADES_CTY> ListaHijos = listaArbol.Where(w => w.PADRE.Equals(padre)).ToList();
            foreach (O_LISTA_ARBOL_ENTIDADES_CTY item in ListaHijos)
            {
                TreeViewNode nodo = new TreeViewNode(item.DESCRIPCION, item.IDENTIFICADOR.ToString());
                nodo.ClientEnabled = false;
                nodo.TextStyle.ForeColor = Color.Black;
                nodoPadre.Nodes.Add(nodo);
                if (nivel == 2)
                {
                    decimal decIdEntidad = Convert.ToDecimal(nodo.Name);
                    decimal decIdActividad = Convert.ToDecimal(nodoPadre.Name);
                    List<O_PROD_ESPEC_PACL_CTY> tipoProducto = ListarGruposProductoEspecificacionTipoCalidad(decIdEntidad, decIdActividad);
                    if (tipoProducto != null)
                        foreach (var tp in tipoProducto)
                        {
                            TreeViewNode nodoHijo1 = new TreeViewNode(tp.NOMBRE, tp.ID_PRODUCTO.ToString());
                            nodoHijo1.ClientEnabled = false;
                            nodoHijo1.TextStyle.ForeColor = Color.Black;
                            nodo.Nodes.Add(nodoHijo1);
                            List<O_PROD_CAL_CTY> listaEsp = ListarGruposProductoEspecificacionCalidad(decIdActividad, tp.ID_PRODUCTO);
                            //nivel++;
                            if (listaEsp != null)
                                foreach (var ep in listaEsp)
                                {
                                    TreeViewNode nodoHijo2 = new TreeViewNode(ep.NOMBRE, ep.ID_PRODUCTO.ToString() + "|" + ep.ID_TABLA_ESPEC);
                                    nodoHijo2.TextStyle.Font.Bold = true;
                                    nodoHijo1.Nodes.Add(nodoHijo2);
                                }

                        }
                }
                CargarArbolOpciones(listaArbol, item.HIJO, nodo, nivel + 1);
            }

        }
        #endregion
        
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
                        Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]),
                        Convert.ToDecimal(CParametrosHydro.decIdAplicacion), Path.GetFileName(Request.Path),
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
                    if (!IsPostBack)
                    {
                        Session["conteo"] = 0;
                    }
                    //else
                    //{
                    //    CtrRegistroCertificadoCalidadCarburantes.InicializarControlPredefinido();
                    //}

                    if (arbolMenu.Nodes != null)
                    {
                        if (arbolMenu.Nodes.Count == 0)
                        {
                            CargarMenuArbol();
                        }
                        TreeViewNode nodo = arbolMenu.SelectedNode;
                        if (nodo != null && nodo.Name.Contains("|"))
                        {
                            var r = nodo.Parent.Parent.Parent.Name;
                            string[] producto = nodo.Name.Split('|');
                            string idProducto = producto[0];
                            string idTablaEspecifica = producto[1];
                            string Producto = nodo.Text;
                            string TipoProducto = nodo.Parent.Text;
                            string valorDatosGrilla = idProducto + "|" + idTablaEspecifica + "|" + Producto + "|"
                                                      + TipoProducto;
                            Session[CParametrosCalidad.cObjValoresCargarGrilla] = valorDatosGrilla;


                            CtrRegistroCertificadoCalidadCarburantes.TipoDeRegistro = 1;
                            if (CtrRegistroCertificadoCalidadCarburantes.TipoDeRegistro == 2
                                || CtrRegistroCertificadoCalidadCarburantes.TipoDeRegistro == 3)
                            {
                                //ucCargadoMenuCertificadoCalidad.Visible = false;
                                CtrRegistroCertificadoCalidadCarburantes.Visible = true;
                                CtrRegistroCertificadoCalidadCarburantes.NumeroCorrelativoReporte =
                                    Session["idCalPrincipal"].ToString().Replace("/", "_").Replace(" ", "%20");
                            }
                            CtrRegistroCertificadoCalidadCarburantes.Llave = cParametrosHydro.strCredencial;
                            CtrRegistroCertificadoCalidadCarburantes.GridSelectorChanged +=
                                new ucRegistroCertificadoCalidadCarburantes.GuardarRegistroEventHandler(
                                    ObtenerNumeroCite);
                            this.CtrRegistroCertificadoCalidadCarburantes.IdUsuario =
                                Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.UsuarioId]);

                            this.CtrRegistroCertificadoCalidadCarburantes.IdEntidad =
                                Convert.ToDecimal(nodo.Parent.Parent.Name);
                            //    Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.IdEntidad]);
                            //Response.Redirect(Request.RawUrl);
                            Session[CVariablesSesion.IdEntidad] = nodo.Parent.Parent.Name;
                            Session[CVariablesSesion.IdTipoActividad] = nodo.Parent.Parent.Parent.Name;
                            Session[CVariablesSesion.TipoActividad] = nodo.Parent.Parent.Parent.Text;
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

        void ObtenerNumeroCite(ucRegistroCertificadoCalidadCarburantes.GuardarRegistroCommandEventArgs e)
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
                    CtrRegistroCertificadoCalidadCarburantes.NumeroCorrelativo = e.NumeroCorrelativo;
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        
        protected void arbolMenu_NodeClick(object source, TreeViewNodeEventArgs e)
        {
            CtrRegistroCertificadoCalidadCarburantes.LimpiarFormularioRegistroCalidadActual();
            CtrRegistroCertificadoCalidadCarburantes.InicializarControlPredefinido();
            CtrRegistroCertificadoCalidadCarburantes.CargarParametroGrillaCalidad();
            //Session["conteo"] = 1;
            //Session[CParametrosCalidad.cColListadoPutoCustodio] = null;
        }
        
    }
}

