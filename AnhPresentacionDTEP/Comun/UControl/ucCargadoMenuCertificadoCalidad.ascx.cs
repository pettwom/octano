using AnhClases;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using Librerias.Anh.Us;
using Librerias.Anh.Us.Encriptacion.RSA;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AnhPresentacionDTEP.Comun.UControl
{
    public partial class ucCargadoMenuCertificadoCalidad : System.Web.UI.UserControl
    {
        public string Llave { get; set; }
        public decimal IdUsuario { get; set; }
        public decimal TipoProducto { get; set; }
        string strMensajeError = "";
        decimal decIdEstadoTipoPrueba = 0;
        string strAccion = "";
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if (!IsPostBack)
                {
                    decIdEstadoTipoPrueba = ObtenerTipoPrueba();
                    List<O_PROD_ESPEC_PACL_CTY> objResultado = null;
                    try
                    {
                        objResultado = clienteJson.Get<List<O_PROD_ESPEC_PACL_CTY>>("/ListarGruposProductoEspecificacionTipoCalidad/" + Llave + "/" + decIdEstadoTipoPrueba + "/" + IdUsuario + "?format=json");
                    }
                    catch (Exception ex)
                    {
                        strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                        CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                    }
                    MenuItem vMenuPadre = null;
                    if (objResultado != null)
                    {
                        if (objResultado.Count > 0)
                        {
                            foreach (var item in objResultado)
                            {
                                if (item.ID_PRODUCTO == TipoProducto || TipoProducto == 0)
                                {
                                    if (item.NOMBRE != "")
                                    {
                                        vMenuPadre = new MenuItem(item.NOMBRE);

                                        List<O_PROD_CAL_CTY> vColProductoEspecificacion = clienteJson.Get<List<O_PROD_CAL_CTY>>("/ListarGruposProductoEspecificacionCalidad/" + Llave + "/" + IdUsuario + "/0/0/" + item.ID_PRODUCTO + "/APP_OCT?format=json");
                                        if (vColProductoEspecificacion != null)
                                        {
                                            foreach (var vObjProductoEspecificacion in vColProductoEspecificacion)
                                            {
                                                vMenuPadre.ChildItems.Add(new MenuItem
                                                {
                                                    Text = vObjProductoEspecificacion.NOMBRE,
                                                    Value = Convert.ToString(vObjProductoEspecificacion.ID_PRODUCTO + "|" + vObjProductoEspecificacion.ID_TABLA_ESPEC + "|" + vObjProductoEspecificacion.NOMBRE),
                                                });
                                            }
                                            MenuCertificadoCalidad.Items.Add(vMenuPadre);
                                        }
                                        else
                                            LabelError.Text = "NO SE ENCONTRARON RESULTADOS.";
                                    }
                                }
                            }
                        }
                        else
                        {
                            LabelError.Text = "NO SE ENCONTRARON RESULTADOS.";
                        }
                    }
                    else
                    {
                        LabelError.Text = "NO SE ENCONTRARON RESULTADOS.";
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el estado de tipo de prueba de calidad.
        /// </summary>
        /// <returns>Retorna un entero.</returns>
        protected int ObtenerTipoPrueba()
        {
            try
            {
                List<O_ESTADO_TIPO_PRUEBA_CTY> lstResultado = clienteJson.Get<List<O_ESTADO_TIPO_PRUEBA_CTY>>("/ObtenerTipoPrueba/" + Llave + "/" + IdUsuario + "?format=json");
                if (lstResultado != null && lstResultado.Count == 1)
                {
                    return Convert.ToInt32(lstResultado[0].ESTADO_TIPO_PRUEBA);
                }
            }
            catch (Exception)
            {
            }
            return 0;
        }

        #region Declaración de objetos complejos

        public partial class O_PRODUCTO_ESPEC_CTY
        {
            public decimal ID_TABLA_ESPEC { get; set; }
            public decimal ID_PRODUCTO { get; set; }
            public string NOMBRE { get; set; }
        }

        //public partial class O_PROD_CAL_CTY
        //{
        //    public decimal ID_TABLA_ESPEC { get; set; }
        //    public decimal ID_PRODUCTO { get; set; }
        //    public string NOMBRE { get; set; }
        //}

        public partial class O_PROD_ESPEC_PACL_CTY
        {
            public decimal ID_PRODUCTO { get; set; }
            public string NOMBRE { get; set; }
        }

        public partial class O_ESTADO_TIPO_PRUEBA_CTY
        {
            public decimal ESTADO_TIPO_PRUEBA { get; set; }
        }

        #endregion

        public void MenuItem_Click(object sender, MenuEventArgs e)
        {
            if (e.Item.Value != null)
            {
                String[] vValorDatosGrilla = e.Item.ValuePath.ToString().Split('/'); ;
                if (vValorDatosGrilla.Count() > 1)
                    Session[CParametrosCalidad.cObjValoresCargarGrilla] = e.Item.Value + "|" + vValorDatosGrilla[0];
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}