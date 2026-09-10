using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios.ServicioHydroListados;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador
{
    public partial class WfAmpliacion : System.Web.UI.Page
    {
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mCargarArbol();
                mLimpiar();
                Session.Remove(CVariablesSesion.IdEntidadArbol);
                Session.Remove(CVariablesSesion.IdActividadArbol);
            }
            if (treeOperadores.IsCallback)
            {
                mCargarArbol();
            }
        }

        private void mCargarGrilla(decimal entidad, decimal actividad)
        {
           List<O_LISTA_AMPLIACIONES_CTY> lstResultado = new List<O_LISTA_AMPLIACIONES_CTY>();
           lstResultado = clienteJson.Get<List<O_LISTA_AMPLIACIONES_CTY>>("/ListarAmpliaciones/" + cParametrosHydro.strCredencial+"/"+entidad+"/"+actividad+"/0?format=json");
            if (lstResultado.Count>0)
            {
                grvAmpliaciones.DataSource = lstResultado;
                grvAmpliaciones.DataBind();
            }
            else
            {
                grvAmpliaciones.DataSource = new List<O_LISTA_AMPLIACIONES_CTY>();
                grvAmpliaciones.DataBind();
            }
        }

        private void mCargarArbol()
        {
            List<O_LISTA_ARBOL_ENTIDADES_CTY> lstResultado= new List<O_LISTA_ARBOL_ENTIDADES_CTY>();
            

            lstResultado = clienteJson.Get<List<O_LISTA_ARBOL_ENTIDADES_CTY>>("/ListarArbolEntidades/" + cParametrosHydro.strCredencial + "/0/0/" + Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]) + "/1?format=json");
            if (lstResultado!=null && lstResultado.Count > 0)
            {
                 treeOperadores.DataSource = lstResultado;
                 treeOperadores.DataBind();
            }
            else
            {
                treeOperadores.DataSource = new List<O_LISTA_ARBOL_ENTIDADES_CTY>();
                treeOperadores.DataBind();
            }

        }

        private void mLimpiar()
        {
            dtAmpliacion.Text = null;
            dtApertura.Text = null;
            dtCierre.Text = null;
            txtBarcode.Text = null;
            txtObservaciones.Text = null;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            AnhPersistenciaCore.Core.O_RESULTADO_CTY oResultado = new AnhPersistenciaCore.Core.O_RESULTADO_CTY();
            
            O_GESTION_AMPLIACION objAmpliacion =new O_GESTION_AMPLIACION();
            objAmpliacion.strLlave=cParametrosHydro.strCredencial;
            objAmpliacion.decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidadArbol])  ;
            objAmpliacion.decIdTipoActividad = Convert.ToDecimal(Session[CVariablesSesion.IdActividadArbol]);
            objAmpliacion.decFechaOperacion= CFechas.ConvierteDateTimeLong(Convert.ToDateTime(dtAmpliacion.Text));
            objAmpliacion.decFechaInicio=CFechas.ConvierteDateTimeLong(Convert.ToDateTime(dtApertura.Text));
            objAmpliacion.decFechaFin=CFechas.ConvierteDateTimeLong(Convert.ToDateTime(dtCierre.Text));
            objAmpliacion.strBarcode= txtBarcode.Text;
            objAmpliacion.strObservaciones=txtObservaciones.Text;
            objAmpliacion.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
            objAmpliacion.decAppFechaRegistro=CFechas.ConvierteDateTimeLong(System.DateTime.Now);
            objAmpliacion.decTipo=1;

            oResultado = clienteJson.Post<AnhPersistenciaCore.Core.O_RESULTADO_CTY>("/GestionAmpliacion/?format=json", objAmpliacion);

            if (!oResultado.MENSAJE_ERROR.Contains("OK"))
            {
                mCargarGrilla(Convert.ToDecimal(Session[CVariablesSesion.IdEntidadArbol]),
                    Convert.ToDecimal(Session[CVariablesSesion.IdActividadArbol]));
                mLimpiar();
            }
            else
            {
                
            }
            
        }

        protected void pnlDatos_OnCallback(object sender, CallbackEventArgsBase e)
        {
            Session.Remove(CVariablesSesion.IdEntidadArbol);
            Session.Remove(CVariablesSesion.IdActividadArbol);
            mCargarArbol();
            //selecciona solo entidades
            if (treeOperadores.FocusedNode.Level == 3)
            {
                PanelContent2.Visible = true;
            var key = e.Parameter.ToString();
            O_LISTA_ARBOL_ENTIDADES_CTY node = new O_LISTA_ARBOL_ENTIDADES_CTY();
            node = (O_LISTA_ARBOL_ENTIDADES_CTY) treeOperadores.FocusedNode.DataItem;
            

            O_LISTA_ARBOL_ENTIDADES_CTY entidad = new O_LISTA_ARBOL_ENTIDADES_CTY();
            O_LISTA_ARBOL_ENTIDADES_CTY actividad = new O_LISTA_ARBOL_ENTIDADES_CTY();
            O_LISTA_ARBOL_ENTIDADES_CTY departamento = new O_LISTA_ARBOL_ENTIDADES_CTY();
            TreeListNode hijo = treeOperadores.FindNodeByKeyValue(key);
            entidad = (O_LISTA_ARBOL_ENTIDADES_CTY)hijo.DataItem;
            TreeListNode padre = hijo.ParentNode;
            actividad = (O_LISTA_ARBOL_ENTIDADES_CTY)padre.DataItem;
            TreeListNode padre1 = padre.ParentNode;
            departamento = (O_LISTA_ARBOL_ENTIDADES_CTY)padre1.DataItem;

            //cargar grilla
            mCargarGrilla(entidad.IDENTIFICADOR, actividad.IDENTIFICADOR);
            Session.Add(CVariablesSesion.IdEntidadArbol,entidad.IDENTIFICADOR);
            Session.Add(CVariablesSesion.IdActividadArbol, actividad.IDENTIFICADOR);
            }
            else
            {
                PanelContent2.Visible = false;
            }
            
        }
    }

    internal class O_LISTA_AMPLIACIONES_CTY:AnhPersistenciaCore.Core.O_LISTA_AMPLIACIONES_CTY
    {
            //public decimal ID_AMPLIACION { get; set; }
            //public decimal ID_ENTIDAD { get; set; }
            //public string ENTIDAD { get; set; }
            //public decimal ID_TIPO_ACTIVIDAD { get; set; }
            //public string TIPO_ACTIVIDAD { get; set; }
            //public decimal FECHA_OPERACION { get; set; }
            //public decimal FECHA_INICIO { get; set; }
            //public decimal FECHA_FIN { get; set; }
            //public string BARCODE { get; set; }
            //public string OBSERVACION { get; set; }
        public DateTime FECHA_OPERACION_FORMAT
        {
            get { return CFechas.ConvierteLongDateTime(FECHA_OPERACION); }
            set
            {
                FECHA_OPERACION = CFechas.ConvierteDateTimeLong(value);
            }
        }
        public DateTime FECHA_INICIO_FORMAT
        {
            get { return CFechas.ConvierteLongDateTime(FECHA_INICIO); }
            set
            {
                FECHA_INICIO = CFechas.ConvierteDateTimeLong(value);
            }
        }
        public DateTime FECHA_FIN_FORMAT
        {
            get { return CFechas.ConvierteLongDateTime(FECHA_FIN); }
            set
            {
                FECHA_FIN = CFechas.ConvierteDateTimeLong(value);
            }
        }
    }
    internal class O_GESTION_AMPLIACION
    {
        public string strLlave { get; set; }
        public decimal decIdAmpliacion { get; set; }
        public decimal decIdEntidad { get; set; }
        public decimal decIdTipoActividad { get; set; }
        public decimal decFechaOperacion { get; set; }
        public decimal decFechaInicio { get; set; }
        public decimal decFechaFin { get; set; }
        public string strBarcode { get; set; }
        public string strObservaciones { get; set; }
        public decimal decAppIdUsuario { get; set; }
        public decimal decAppFechaRegistro { get; set; }
        public decimal decTipo { get; set; }
    }

    internal class O_LISTA_ARBOL_ENTIDADES_CTY
    {
        public string HIJO { get; set; }
        public string PADRE { get; set; }
        public decimal IDENTIFICADOR { get; set; }
        public string DESCRIPCION { get; set; }
    }

}