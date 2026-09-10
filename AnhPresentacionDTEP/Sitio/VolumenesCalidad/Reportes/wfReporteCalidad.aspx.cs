using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroSesion;
using DevExpress.Web.Internal;
using ServiceStack.ServiceClient.Web;
using AnhHydro.Sitio.VolumenesCalidad.Reportes;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Entidades;
using ServiceStack.ServiceHost;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using Librerias.Anh.Us;
using System.IO;
using System.Web.Security;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes
{

    public partial class wfReporteCalidad : System.Web.UI.Page
    {

        #region Clases y variables de entorno

        public class RetornaIdDireccion : IReturn<EResultado>
        {
            public decimal decIdUsuario { get; set; }
        }

        #endregion

        #region Atributos de Clase

        private string _mensajeError = string.Empty;
        private string strAccion = string.Empty;
        private readonly IServicioHydroSesion _servicioHydroUsuario = LocalizadorProxy.ServicioAutenticarUsuario();
        private List<O_REPORTE_CALIDAD> lstResultado;
        private List<O_CERTIFICADO_ALERTAS_CTY> lstResultadoObs;
        private bool EsMarca = false;
        private bool EsVolumenMuestra = false;
        private bool EsPrecio = false;
        private bool EsResolucion = false;
        private bool EsNombreProducto = false;
        string strMensajeError = "";

        #endregion

        #region Metodos y funciones

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
                    decimal idCalPrincipal = Convert.ToDecimal(Request["idCalPrincipal"]);
                    string cite = Convert.ToString(Request["cite"]);
                    decimal idTipoActividad = Convert.ToDecimal(Request["act"]);
                    DateTime fecha = Convert.ToDateTime(Convert.ToString(Request["fec"]).Replace("_","/"));

                    var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                    lstResultado = clienteJson.Get<List<O_REPORTE_CALIDAD>>("/ReportarCalidad/" +
                        cParametrosHydro.strCredencial + "/" + cite.Replace(" ", "%20").Replace("/", "_") + "?format=json");

                  
                    //decimal decIdDireccion = idTipoActividad;
                    //ObtenerIdDireccion();

                    if (idTipoActividad == DireccionesAnh.svc_obtener_idRefinacion || idTipoActividad == DireccionesAnh.svc_obtener_idPSLs || idTipoActividad == DireccionesAnh.svc_obtener_idIndustrializacion) //(decimal)IdDireccionAnh.Druin)
                    {
                        devReporteCalidad rep = mGeneraReporte(idCalPrincipal, Convert.ToDecimal(lstResultado[0].APP_ID_USUARIO.ToString()));
                        if (fecha > Convert.ToDateTime(cParametrosHydro.fechaIso2015)) 
                        {
                        rep.xrPictureBox3.ImageUrl = "~/UI/img/anh/logo_iso_9001_2015.png";
                        rep.xrPictureBox2.ImageUrl = "~/UI/img/anh/logo_ISO_2015.png";
                        //rep.xrPictureBox2.Location = new Point(445, 96);
                        }
                        else
                        {
                            rep.xrPictureBox3.ImageUrl = "~/UI/img/anh/logosISO2008.jpg";
                        }
                        ReportViewer1.Report = rep;
                    }
                    else if (idTipoActividad == DireccionesAnh.svc_obtener_idImportadores || idTipoActividad == DireccionesAnh.svc_obtener_idMayorista || idTipoActividad == DireccionesAnh.svc_obtener_idTerminalesAlmacenaje)//(decimal)IdDireccionAnh.Dcd)
                    {
                        //devReporteVerificacion rep = mGeneraReporte(idCalPrincipal, Convert.ToDecimal(lstResultado[0].APP_ID_USUARIO.ToString()), "Verificacion");
                        //ReportViewer1.Report = rep;

                        devReporteCalidadImportadores repI = mGeneraReporteImportadores(idCalPrincipal, Convert.ToDecimal(lstResultado[0].APP_ID_USUARIO.ToString()));
                        if (fecha > Convert.ToDateTime(cParametrosHydro.fechaIso2015))
                        {
                            repI.xrPictureBox3.ImageUrl = "~/UI/img/anh/logo_iso_9001_2015.png";
                            // repI.xrPictureBox2.Location = new Point(445, 96);
                            repI.xrPictureBox2.ImageUrl = "~/UI/img/anh/logo_ISO_2015.png";
                        }
                        ReportViewer1.Report = repI;
                    }
                    else if (idTipoActividad == DireccionesAnh.svc_obtener_idProcGasNatEnCampo || idTipoActividad == DireccionesAnh.svc_obtener_idAnh)
                    {
                       

                        devReporteCalidadDTEP rep = mGeneraReporteDTEP(idCalPrincipal, Convert.ToDecimal(lstResultado[0].APP_ID_USUARIO.ToString()));
                        if (fecha > Convert.ToDateTime(cParametrosHydro.fechaIso2015))
                        {
                            //rep.xrPictureBox3.Location = new Point(485, 75);
                            rep.xrPictureBox3.ImageUrl = "~/UI/img/anh/logo_iso_9001_2015.png";
                            //rep.xrPictureBox2.Location = new Point(435, 75);
                            rep.xrPictureBox2.ImageUrl = "~/UI/img/anh/logo_ISO_2015.png";
                        }
                        ReportViewer1.Report = rep;
                    }
                    else if (idTipoActividad == DireccionesAnh.svc_obtener_idAeropuerto || idTipoActividad == DireccionesAnh.svc_obtener_idAeropuerto)
                    {
                        devReporteCalidadDTEP rep = mGeneraReporteDTEP(idCalPrincipal, Convert.ToDecimal(lstResultado[0].APP_ID_USUARIO.ToString()));
                        if (fecha > Convert.ToDateTime(cParametrosHydro.fechaIso2015))
                        {
                            rep.xrPictureBox3.ImageUrl = "~/UI/img/anh/logo_iso_9001_2015.png";
                            //rep.xrPictureBox2.Location = new Point(445, 96);
                            rep.xrPictureBox2.ImageUrl = "~/UI/img/anh/logo_ISO_2015.png";
                        }
                        ReportViewer1.Report = rep;
                    }
                    if (Convert.ToDecimal(Session["observado"]) == 1)
                    {
                        lstResultadoObs = clienteJson.Get<List<O_CERTIFICADO_ALERTAS_CTY>>("ListarCertificadoConAlertas/" + idCalPrincipal + "?format=json");

                        devReporteCalidadObservados rep = mGeneraReporteObservados(idCalPrincipal, Convert.ToDecimal(lstResultado[0].APP_ID_USUARIO.ToString()));
                        //if (fecha > Convert.ToDateTime(cParametrosHydro.fechaIso2015))
                        //{
                            rep.xrPictureBox3.ImageUrl = "~/UI/img/anh/logo_iso_9001_2015.png";
                            //rep.xrPictureBox2.Location = new Point(445, 96);
                            rep.xrPictureBox2.ImageUrl = "~/UI/img/anh/logo_ISO_2015.png";
                        //}
                        ReportViewer1.Report = rep;
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        public decimal ObtenerIdDireccion()
        {
            decimal decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);

            RetornaIdDireccion vObjRetornaIdDireccion = new RetornaIdDireccion();
            vObjRetornaIdDireccion.decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
            var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
            var lstDirecciones = clienteJson.Post<List<AnhPersistenciaCore.Core.O_RESULTADO_NUMBER_CTY>>("/RetornaIdDireccion/?format=json", vObjRetornaIdDireccion);

            decimal decIdDireccion;
            if (lstDirecciones.Count == 1)
            {
                decIdDireccion = lstDirecciones[0].RESULTADO;
            }
            else
            {
                decIdDireccion = 0;
            }
            return decIdDireccion;
        }

        private devReporteCalidad mGeneraReporte(decimal idCalPrincipal, decimal idUsuario)
        {
            lstResultado = OReporteCalidadDetalles(lstResultado);
            O_DETALLE_USUARIO_CTY user = _servicioHydroUsuario.ObtenerUsuario("", idUsuario, ref _mensajeError);
            VerificacionCampo(lstResultado);
            var reporte = new devReporteCalidad(user.LOGIN.ToLower(), EsPrecio, EsVolumenMuestra, EsMarca, EsNombreProducto);
            try
            {
                DataSet dsReporte = UtilReport.mToDataSet(lstResultado);
                reporte.DataSource = dsReporte;
                //dsReporte.WriteXmlSchema(@"d:/DataReporte/CalidadGeneraReporte.xsd");
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
            return reporte;
        }

        private devReporteVerificacion mGeneraReporte(decimal idCalPrincipal, decimal idUsuario, string strVerificacion)
        {
            lstResultado = OReporteCalidadDetalles(lstResultado);
            O_DETALLE_USUARIO_CTY user = _servicioHydroUsuario.ObtenerUsuario("", idUsuario, ref _mensajeError);
            var reporte = new devReporteVerificacion(user.LOGIN.ToLower());
            try
            {
                DataSet dsReporte = UtilReport.mToDataSet(lstResultado);
                reporte.DataSource = dsReporte;
               // dsReporte.WriteXmlSchema(@"d:/DataReporte/CalidadGeneraReporte1.xsd");
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
            return reporte;
        }

        private void  VerificacionCampo (List<O_REPORTE_CALIDAD> lstResultado)
        {
            if (lstResultado[0].MARCA_PRODUCTO != null)
                EsMarca = true;
            if (lstResultado[0].IMPORTE != null)
                EsPrecio = true;
            if (lstResultado[0].VOLUMEN_OP_DEBE_MUESTRA != null)
                EsVolumenMuestra = true;
            if (lstResultado[0].OBSERVACIONES != null && lstResultado[0].OBSERVACIONES!= "SIN_OBSERVACION")
                EsResolucion = true;
            if (lstResultado[0].NOMBRE_PRODUCTO != null)
                EsNombreProducto = true;
        }

        private devReporteCalidadDTEP mGeneraReporteDTEP(decimal idCalPrincipal, decimal idUsuario)
        {
            lstResultado = OReporteCalidadDetalles(lstResultado);
            O_DETALLE_USUARIO_CTY user = _servicioHydroUsuario.ObtenerUsuario("", idUsuario, ref _mensajeError);            
            VerificacionCampo(lstResultado);
            var reporte = new devReporteCalidadDTEP(user.LOGIN.ToLower(), EsPrecio, EsVolumenMuestra, EsMarca, EsResolucion, EsNombreProducto);
            try
            {
                DataSet dsReporte = UtilReport.mToDataSet(lstResultado);
                reporte.DataSource = dsReporte;
                //dsReporte.WriteXmlSchema(@"d:/DataReporte/CalidadDTEP.xsd");
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
            return reporte;
        }
        private devReporteCalidadImportadores mGeneraReporteImportadores(decimal idCalPrincipal, decimal idUsuario)
        {
            lstResultado = OReporteCalidadDetalles(lstResultado);
            O_DETALLE_USUARIO_CTY user = _servicioHydroUsuario.ObtenerUsuario("", idUsuario, ref _mensajeError);
            VerificacionCampo(lstResultado);
            var reporte = new devReporteCalidadImportadores(user.LOGIN.ToLower(), EsPrecio, EsVolumenMuestra, EsMarca, EsResolucion, EsNombreProducto);
            try
            {
                DataSet dsReporte = UtilReport.mToDataSet(lstResultado);
                reporte.DataSource = dsReporte;
                //dsReporte.WriteXmlSchema(@"d:/DataReporte/CalidadDTEP.xsd");
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
            return reporte;
        }

        private devReporteCalidadObservados mGeneraReporteObservados(decimal idCalPrincipal, decimal idUsuario)
        {
            lstResultadoObs = OReporteCalidadDetallesObs(lstResultadoObs);
            O_DETALLE_USUARIO_CTY user = _servicioHydroUsuario.ObtenerUsuario("", idUsuario, ref _mensajeError);
            var volumen = decimal.MinValue;

            if (lstResultado[0].VOLUMEN_OP_DEBE != null)
                volumen = (decimal)lstResultado[0].VOLUMEN_OP_DEBE;

            devReporteCalidadObservados reporte = new devReporteCalidadObservados(user.LOGIN.ToLower(),
                                                            Session[CVariablesSesion.DatosUsuario].ToString(),
                                                            lstResultado[0].CITE_DOCUMENTO,
                                                            lstResultado[0].PRODUCTO,
                                                            lstResultado[0].ENTIDAD,
                                                            lstResultado[0].PUNTO_CUSTODIO,
                                                            volumen,
                                                            lstResultado[0].CODIGO_UNIDAD_VOL,
                                                            lstResultado[0].VALOR_LOTE,
                                                            (DateTime)lstResultado[0].FECHA_OPERACION,
                                                            (DateTime)lstResultado[0].APP_FECHA_REGISTRO,
                                                            lstResultado[0].MARCA_PRODUCTO,
                                                            lstResultado[0].IMPORTE,
                                                            lstResultado[0].CODIGO_IMPORTE,
                                                            lstResultado[0].VOLUMEN_OP_DEBE_MUESTRA,
                                                            lstResultado[0].NOMBRE_PRODUCTO);
            try
            {
                DataSet dsReporte = UtilReport.mToDataSet(lstResultadoObs);
                reporte.DataSource = dsReporte;
                //dsReporte.WriteXmlSchema(@"d:/DataReporte/CalidadObservados.xsd");
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
            return reporte;
        }

        private static List<O_REPORTE_CALIDAD> OReporteCalidadDetalles(List<O_REPORTE_CALIDAD> lstResultado)
        {
            for (int i = 0; i < lstResultado.Count; i++)
            {
                if (lstResultado[i].ID_PRUEBA_CALIDAD_PADRE != 0)
                {
                    lstResultado[i].DESCRIPCION = "      " + lstResultado[i].DESCRIPCION;
                }
            }
            return lstResultado;
        }

        private static List<O_CERTIFICADO_ALERTAS_CTY> OReporteCalidadDetallesObs(List<O_CERTIFICADO_ALERTAS_CTY> lstResultadoObs)
        {
            for (int i = 0; i < lstResultadoObs.Count; i++)
            {
                if (lstResultadoObs[i].DESCRIPCION != null)
                {
                    lstResultadoObs[i].DESCRIPCION = "      " + lstResultadoObs[i].DESCRIPCION;
                }
            }
            return lstResultadoObs;
        }

        #endregion
    }

}