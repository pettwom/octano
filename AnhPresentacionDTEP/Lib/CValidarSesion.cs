
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using AnhPresentacionDTEP.Parametros;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Librerias
{
    using AnhHydroTalleresOpeGarrafasPresentacion.Parametros;

    public class CValidarSesion
    {
        #region validar session usuario
        public static void ValidarSesionUsuario(HttpContext contexto)
        {
            const string strPaginaAutenticacion = "~/Sitio/Persona/wfSalir.aspx";
            try
            {
                
                if (contexto.Session[CVariablesSesion.ObjUsuario] == null)
                {
                    FormsAuthentication.SignOut();                    
                    contexto.Session.RemoveAll();
                    contexto.Session.Abandon();
                    contexto.Response.Redirect(strPaginaAutenticacion,false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {

                contexto.Response.Redirect(strPaginaAutenticacion,false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }        
        #endregion
    }
}