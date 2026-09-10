using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using DevExpress.Web;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad
{
    public partial class wfDashboard : System.Web.UI.Page
    {
        string strAccion = "";
        private bool bFechaCorrecta;
        
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
        
        private decimal IdUsuario
        {
            get
            {
                return Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
            }
        }
        private decimal IdEntidad
        {
            get { return Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dtFechaFinal.MaxDate = System.DateTime.Now;
            RecuperarFechas();
            if (!bFechaCorrecta)
            {
                ConfigurarFechaInicial();
            }
            else if (!IsPostBack)
            {
                ConfigurarFechaInicial();
            }


            this.CargarComboEntidades(cmbEntidad, ObtenerListaEntidades(), IdEntidad);
            List<O_LISTA_ENTIDAD_PROP_CTY> listaActividades = this.ObtenerListaActividades(Convert.ToDecimal(cmbEntidad.Value));
            cmbEntidad.Items.Insert(0, new ListEditItem("TODOS...", "0"));
            if (!IsCallback)
            {
                cmbEntidad.SelectedIndex = 0;
            }

            listaActividades.Insert(0, new O_LISTA_ENTIDAD_PROP_CTY() { ID_TIPO_ACTIVIDAD = 0, TIPO_ACTIVIDAD = "TODOS..." });
            this.CargarComboActividades(cmbActividad, listaActividades);
           /* if (!IsPostBack)
            {
            // instantiate XmlDocument and load XML from file
            XmlDocument doc = new XmlDocument();
            //doc.Load(@"D:\Sistemas_2015\AnhHydroOctano\AnhOctanoV2\AnhPresentacionDTEP\UI\xml\2_reporte.xml");
            string ruta = AppDomain.CurrentDomain.BaseDirectory + "UI\\xml\\1_reporte.xml";
            doc.Load(ruta);
                
            // get a list of nodes - in this case, I'm selecting all <AID> nodes under
            // the <GroupAIDs> node - change to suit your needs
            XmlNodeList aNodes = doc.SelectNodes("/Dashboard/DataSources/SqlDataSource/Query/Parameter");

            // loop through all AID nodes
            foreach (XmlNode aNode in aNodes)
            {
                // grab the "id" attribute
                XmlAttribute idAttribute = aNode.Attributes["Name"];

                // check if that attribute even exists...
                if (idAttribute.Value == "I_CREDENCIAL")
                {
                    // if yes - read its current value
                    string currentValue = aNode.FirstChild.InnerText;

                    aNode.FirstChild.InnerText = "83809AD945F1F72D0EA9FDA0E599E0B3";
                    doc.Save(ruta);
                }
                if (idAttribute.Value == "I_ID_USUARIO")
                {
                    // if yes - read its current value
                    string currentValue = aNode.FirstChild.InnerText;

                    aNode.FirstChild.InnerText = IdUsuario.ToString();
                    doc.Save(ruta);
                }
                
            }
            }*/
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            // instancia XmlDocument y cargado XML de archivo
            XmlDocument doc = new XmlDocument();
            string ruta = AppDomain.CurrentDomain.BaseDirectory + "UI\\xml\\1_reporte.xml";
            doc.Load(ruta);

            // obtiene lista de nodos
            XmlNodeList aNodes = doc.SelectNodes("/Dashboard/DataSources/SqlDataSource/Query/Parameter");

            // recorre todos los nodos seleccionados
            foreach (XmlNode aNode in aNodes)
            {
                // guarda atributo "Name"
                XmlAttribute idAttribute = aNode.Attributes["Name"];

                switch (idAttribute.Value)
                {
                    case "I_CREDENCIAL": aNode.FirstChild.InnerText = "83809AD945F1F72D0EA9FDA0E599E0B3";
                        break;
                    case "I_ID_USUARIO": aNode.FirstChild.InnerText = IdUsuario.ToString();
                        break;
                    case "I_FECHA_INICIO": aNode.FirstChild.InnerText = CFechas.ConvierteDateTimeLong((Convert.ToDateTime(dtFechaInicial.Text))).ToString();
                        break;
                    case "I_FECHA_FIN": aNode.FirstChild.InnerText = CFechas.ConvierteDateTimeLong((Convert.ToDateTime(dtFechaFinal.Text))).ToString();
                        break;
                    case "I_ID_ENTIDAD": aNode.FirstChild.InnerText = cmbEntidad.SelectedItem.Value.ToString();
                        break;
                    case "I_ID_TIPO_ACTIVIDAD": aNode.FirstChild.InnerText = cmbActividad.SelectedItem != null ? cmbActividad.SelectedItem.Value.ToString() : "0";
                        break;
                }

                /*
                // verifica el nombre del atributo
                if (idAttribute.Value == "I_CREDENCIAL")
                {
                    // si coincide lee el valor
                    string currentValue = aNode.FirstChild.InnerText;
                    aNode.FirstChild.InnerText = "83809AD945F1F72D0EA9FDA0E599E0B3";
                }
                if (idAttribute.Value == "I_ID_USUARIO")
                {
                    string currentValue = aNode.FirstChild.InnerText;
                    aNode.FirstChild.InnerText = IdUsuario.ToString();
                }
                if (idAttribute.Value == "I_FECHA_INICIO")
                {
                    string currentValue = aNode.FirstChild.InnerText;
                    aNode.FirstChild.InnerText = CFechas.ConvierteDateTimeLong((Convert.ToDateTime(dtFechaInicial.Text))).ToString();
                }
                if (idAttribute.Value == "I_FECHA_FIN")
                {
                    string currentValue = aNode.FirstChild.InnerText;
                    aNode.FirstChild.InnerText = CFechas.ConvierteDateTimeLong((Convert.ToDateTime(dtFechaFinal.Text))).ToString();
                }
                if (idAttribute.Value == "I_ID_ENTIDAD")
                {
                    string currentValue = aNode.FirstChild.InnerText;

                    aNode.FirstChild.InnerText = cmbEntidad.SelectedItem.Value.ToString();
                }
                if (idAttribute.Value == "I_ID_TIPO_ACTIVIDAD")
                {
                    string currentValue = aNode.FirstChild.InnerText;
                    aNode.FirstChild.InnerText = cmbActividad.SelectedItem!=null ? cmbActividad.SelectedItem.Value.ToString() : "0";
                }*/
            }
            doc.Save(ruta);
        }


        protected void cmbActividad_Callback(object sender, CallbackEventArgsBase e)
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
                    if (cmbEntidad.SelectedIndex >= 0)
                    {
                        this.CargarComboActividades(cmbActividad, this.ObtenerListaActividades(Convert.ToDecimal(cmbEntidad.Value)));
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        /// <summary>
        /// Carga cualquier combo con actividades
        /// </summary>
        /// <param name="combo">Combo a ser cargado con la lista</param>
        /// <param name="lista">Lista de actividades</param>
        /// <param name="decIdSeleccionado">Si el dato es diferente de cero selecciona por defecto el id</param>
        private void CargarComboActividades(ASPxComboBox combo, List<O_LISTA_ENTIDAD_PROP_CTY> lista, decimal decIdSeleccionado = 0)
        {
            combo.DataSource = lista;
            combo.ValueField = "ID_TIPO_ACTIVIDAD";
            combo.TextField = "TIPO_ACTIVIDAD";
            combo.DataBind();
            if (decIdSeleccionado > 0)
            {
                O_LISTA_ENTIDAD_PROP_CTY objeto = lista.FirstOrDefault(w => w.ID_TIPO_ACTIVIDAD == decIdSeleccionado);
                if (objeto != null)
                {
                    int intIndice = lista.IndexOf(objeto);
                    combo.SelectedIndex = intIndice;
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de actividades de la entidad
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_ENTIDAD_PROP_CTY> ObtenerListaActividades(decimal decIdEntidad)
        {
            List<O_LISTA_ENTIDAD_PROP_CTY> listaEntidadActividad = this.ObtenerListaEntidadActividad();
            List<O_LISTA_ENTIDAD_PROP_CTY> listaActividades = null;
            if (listaEntidadActividad != null)
            {
                listaActividades = (from lea in listaEntidadActividad
                                    where lea.ID_ENTIDAD == decIdEntidad
                                    select
                                        new O_LISTA_ENTIDAD_PROP_CTY()
                                        {
                                            ID_TIPO_ACTIVIDAD = lea.ID_TIPO_ACTIVIDAD,
                                            TIPO_ACTIVIDAD = lea.TIPO_ACTIVIDAD
                                        }).GroupBy(p => p.ID_TIPO_ACTIVIDAD).Select(f => f.First()).ToList();
            }
            return listaActividades != null ? listaActividades : new List<O_LISTA_ENTIDAD_PROP_CTY>();
        }

        /// <summary>
        /// Obtener la lista de Entidades y sus actividades del servicio
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_ENTIDAD_PROP_CTY> ObtenerListaEntidadActividad()
        {
            var vColReporteCalidad = clienteJson.Get<List<O_LISTA_ENTIDAD_PROP_CTY>>("/ListarEntidadesPropietarios/" + cParametrosHydro.strCredencial + "/" + IdUsuario + "?format=json");
            List<O_LISTA_ENTIDAD_PROP_CTY> lista = null;
            if (vColReporteCalidad != null)
            {
                lista = vColReporteCalidad;
            }
            else
            {
                lista = new List<O_LISTA_ENTIDAD_PROP_CTY>();
            }
            return lista;
        }

        /// <summary>
        /// Configurar las fechas de busquedas
        /// </summary>
        private void ConfigurarFechaInicial()
        {
            try
            {
                if (!IsPostBack)
                {
                    dtFechaInicial.Text = DateTime.Now.ToString("01/MM/yyyy");
                    dtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-ES");
                    dtFechaInicial.CalendarProperties.ClearButtonText = "Limpiar";
                    dtFechaInicial.CalendarProperties.TodayButtonText = "<< Hoy >> ";
                    dtFechaInicial.UseMaskBehavior = true;

                    dtFechaFinal.CalendarProperties.ClearButtonText = "Limpiar";
                    dtFechaFinal.CalendarProperties.TodayButtonText = "<< Hoy >> ";
                    dtFechaFinal.UseMaskBehavior = true;
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }

        /// <summary>
        /// Carga el cualquier combo con la información de entidades, con los datos de la lista.
        /// En caso de ser necesario se puede enviar el valor de id que debe ser seleccionado por defecto.
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="lista"></param>
        /// <param name="decIdSeleccionado"></param>
        private void CargarComboEntidades(ASPxComboBox combo, List<O_LISTA_ENTIDAD_PROP_CTY> lista, decimal decIdSeleccionado = 0)
        {

            combo.DataSource = lista;
            combo.ValueField = "ID_ENTIDAD";
            combo.TextField = "ENTIDAD";
            combo.DataBind();
            if (decIdSeleccionado > 0)
            {
                O_LISTA_ENTIDAD_PROP_CTY objeto = lista.FirstOrDefault(w => w.ID_ENTIDAD == decIdSeleccionado);
                if (objeto != null)
                {
                    int intIndice = lista.IndexOf(objeto);
                    combo.SelectedIndex = intIndice;
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de entidades que tienen opciones para cargar certificados de calidad
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_ENTIDAD_PROP_CTY> ObtenerListaEntidades()
        {
            List<O_LISTA_ENTIDAD_PROP_CTY> listaEntidadActividad = this.ObtenerListaEntidadActividad();
            List<O_LISTA_ENTIDAD_PROP_CTY> listaEntidades = null;
            if (listaEntidadActividad.Count != null)
            {

                listaEntidades = (from lea in listaEntidadActividad
                                  select
                                      new O_LISTA_ENTIDAD_PROP_CTY()
                                      {
                                          ID_ENTIDAD = lea.ID_ENTIDAD,
                                          ENTIDAD = lea.ENTIDAD
                                      }).GroupBy(p => p.ID_ENTIDAD).Select(g => g.First()).ToList();

            }

            return listaEntidades != null ? listaEntidades : new List<O_LISTA_ENTIDAD_PROP_CTY>();
        }

        private void RecuperarFechas()
        {
            try
            {
                string fi = Request.Params["fi"].ToString();
                string ff = Request.Params["ff"].ToString();

                DateTime result;
                var formats = new[] { "dd/MM/yyyy" };

                if (!DateTime.TryParseExact(fi, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    bFechaCorrecta = false;
                }
                if (!DateTime.TryParseExact(ff, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    bFechaCorrecta = false;
                }

                if (bFechaCorrecta)
                {
                    dtFechaInicial.Text = fi;
                    dtFechaFinal.Text = ff;
                }
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }
    }
}