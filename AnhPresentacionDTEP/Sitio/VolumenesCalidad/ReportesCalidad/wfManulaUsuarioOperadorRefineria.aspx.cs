using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios.ServicioHydroSesion;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes;
using DevExpress.Utils.OAuth;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.ReportesCalidad
{
    public partial class wfManulaUsuarioOperadorRefineria : System.Web.UI.Page
    {

        #region variables

        string strMensajeError = "";
        string strAccion = "";
        public string url;

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
                    decimal direccion = ObtenerIdDireccion();
                    if (Session[CVariablesSesion.PerfilUsuario] != null)
                    {
                        foreach (O_PERFILES_USUARIO_CTY perfil in (IEnumerable<O_PERFILES_USUARIO_CTY>)Session[CVariablesSesion.PerfilUsuario])
                        {
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "OPERADOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idProcGasNatEnCampo) //DTE&P - PROD *
                            {
                                url = "../../../UI/Docs/ManualUsuario_OperadorDTEP_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "OPERADOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idImportadores)//DCD -IMP - UGM *
                            {
                                url = "../../../UI/Docs/ManualUsuario_OperadorDCD_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "OPERADOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idRefinacion)//DRI -REF *
                            {
                                url = "../../../UI/Docs/ManualUsuario_OperadorDRI_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "OPERADOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idIndustrializacion) //DRI - IND 
                            {
                                url = "../../../UI/Docs/ManualUsuario_OperadorDRI_IND_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "OPERADOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idTerminalesAlmacenaje) //DCD - UGM
                            {
                                url = "../../../UI/Docs/ManualUsuario_OperadorDCD_TA_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "OPERADOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idAeropuerto) //DCD
                            {
                                url = "../../../UI/Docs/ManualUsuario_OperadorDCD_PSCAA_v1.0.pdf";
                            }


                            if ((perfil.DESCRIPCION == "SUPERVISOR OCTANO" || perfil.DESCRIPCION == "SUPERVISOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idProcGasNatEnCampo)
                            {
                                url = "../../../UI/Docs/ManualUsuario_SupervisorDTEP_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "SUPERVISOR IMPORTADORES OCTANO" || perfil.DESCRIPCION == "SUPERVISOR IMPORTADORES OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idImportadores)
                            {
                                url = "../../../UI/Docs/ManualUsuario_SupervisorDCD_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "SUPERVISOR OCTANO" || perfil.DESCRIPCION == "SUPERVISOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idRefinacion)
                            {
                                url = "../../../UI/Docs/ManualUsuario_SupervisorDRI_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "SUPERVISOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idIndustrializacion) 
                            {
                                url = "../../../UI/Docs/ManualUsuario_SupervisorDRI_IND_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "SUPERVISOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idTerminalesAlmacenaje) 
                            {
                                url = "../../../UI/Docs/ManualUsuario_SupervisorDCD_TA_v1.0.pdf";
                            }
                            if ((perfil.DESCRIPCION == "OPERADOR OCTANO" || perfil.DESCRIPCION == "SUPERVISOR OCTANO V2") && direccion == DireccionesAnh.svc_obtener_idAeropuerto)
                            {
                                url = "../../../UI/Docs/ManualUsuario_SupervisorDCD_PSCAA_v1.0.pdf";
                            }


                            if (perfil.DESCRIPCION == "ADMINISTRADOR OCTANO (DTEP)")
                            {
                                url = "../../../UI/Docs/ManualUsuario_AdministradorDTEP_v1.0.pdf";
                            }
                            if (perfil.DESCRIPCION == "ADMINISTRADOR OCTANO (IMP)")
                            {
                                url = "../../../UI/Docs/ManualUsuario_AdministradorDCD_v1.0.pdf";
                            }
                            if (perfil.DESCRIPCION == "ADMINISTRADOR OCTANO (REF)")
                            {
                                url = "../../../UI/Docs/ManualUsuario_AdministradorDRI_v1.0.pdf";
                            }
                            if (perfil.DESCRIPCION == "ADMINISTRADOR OCTANO (DRI)") 
                            {
                                url = "../../../UI/Docs/ManualUsuario_AdministradorDRI_IND_v1.0.pdf";
                            }
                            if (perfil.DESCRIPCION == "ADMINISTRADOR OCTANO (DCD)") 
                            {
                                url = "../../../UI/Docs/ManualUsuario_AdministradorDCD_TA_v1.0.pdf";
                            }
                            if (perfil.DESCRIPCION == "ADMINISTRADOR OCTANO (DCD - AERO)")
                            {
                                url = "../../../UI/Docs/ManualUsuario_AdministradorDCD_PSCAA_v1.0.pdf";
                            }


                            if (perfil.DESCRIPCION == "SUPER ADMINISTRADOR")
                            {
                                url = "../../../UI/Docs/ManualUsuario_AdministradorDRI_v1.0.pdf";
                            }

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

        public decimal ObtenerIdDireccion()
        {
            decimal decIdDireccion=0;
            try
            {
                decimal decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                wfReporteCalidad.RetornaIdDireccion vObjRetornaIdDireccion = new wfReporteCalidad.RetornaIdDireccion();
                vObjRetornaIdDireccion.decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
                var lstDirecciones = clienteJson.Post<List<AnhPersistenciaCore.Core.O_RESULTADO_NUMBER_CTY>>("/RetornaIdDireccion/?format=json", vObjRetornaIdDireccion);

                if (lstDirecciones.Count == 1)
                {
                    decIdDireccion = lstDirecciones[0].RESULTADO;
                }
                else
                {
                    decIdDireccion = 0;
                }
                
            }
            catch (Exception ex)
            {
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
            }
            return decIdDireccion;
        }
    }
}