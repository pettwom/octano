using AnhPresentacionDTEP.Parametros;
using Librerias.Anh.Us;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace AnhPresentacionDTEP.Clases.VolumenesCalidad
{
    public class CCalidadLibreria
    {
        #region mesajes en pantalla

        /// <summary>
        /// Unidad de Programa: MostrarMensaje
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: mostrar mensaje de error en una etiqueta Label
        /// </summary>
        /// <param name="divMensajePantalla">Etiqueta html en la cual se mostrara el mensaje en el formulario.</param>
        /// <param name="strMensaje">mensaje a mostrar en el mensaje de error.</param>
        /// <param name="intTipoMensaje">tipo de mensaje: 0 = mensaje común, 1 = error, 2 = informacion, 3 = ok, 4 = advertencia.</param>
        public static void MostrarMensaje(HtmlGenericControl divMensajePantalla, string strMensaje, int intTipoMensaje)
        {
            divMensajePantalla.InnerHtml = strMensaje;
            divMensajePantalla.Attributes["class"] = (intTipoMensaje == 1
                                       ? "Hydro_Div_Aviso_Rojo"
                                       : intTipoMensaje == 2
                                             ? "Hydro_Div_Aviso_Azul"
                                             : intTipoMensaje == 3
                                                   ? "Hydro_Div_Aviso_Verde"
                                                   : intTipoMensaje == 4
                                                         ? "Hydro_Div_Aviso_Naranja"
                                                         : "Hydro_Div_Aviso_Azul");
            divMensajePantalla.Style.Remove("display");
        }

        #endregion

        #region Validar sesion de usuario

        /// <summary>
        /// Valida que la sesion del usuario no haya expirado o se haya iniciado
        /// </summary>
        /// <param name="response">Respuesta HTTP para redireccionar a la página de autenticación</param>
        public static void ValidarSesionUsuario(HttpResponse response)
        {
            const string strPaginaAutenticacion = "~/Sitio/Persona/wfAutenticacion.aspx";
            try
            {
                if (!ValidarSesionUsuario())
                {
                    response.Redirect(strPaginaAutenticacion);
                }
            }
            catch (Exception)
            {
                response.Redirect(strPaginaAutenticacion);
            }
        }

        /// <summary>
        /// Valida que la sesion del usuario no haya expirado o se haya iniciado
        /// </summary>
        /// <param name="response">Respuesta HTTP para redireccionar a la página de autenticación</param>
        public static void ValidarSesionUsuarioPopUp(HttpResponse response)
        {
            const string strScript = "<script type='text/javascript'>var pagina = self.parent.location.href; self.parent.location.href = pagina+'?v=1';</script>";
            try
            {
                if (!ValidarSesionUsuario())
                {
                    response.Clear();
                    response.Write(strScript);
                    response.End();
                }
            }
            catch (Exception)
            {
                response.Clear();
                response.Write(strScript);
                response.End();
            }
        }

        /// <summary>
        /// Valida que la sesion del usuario no haya expirado o se haya iniciado
        /// </summary>
        /// <returns>True si la sesion esta iniciada, False si ha caducado la sesion</returns>
        public static bool ValidarSesionUsuario()
        {
            bool autenticado = true;
            try
            {
                if (HttpContext.Current.Session[CVariablesSesion.UsuarioId] == null)
                {
                    autenticado = false;
                }
            }
            catch (Exception)
            {
                autenticado = false;
            }
            return autenticado;
        }

        /// <summary>
        /// Método que realiza validaciones por excepción (dll ALibrerias.Anh)
        /// </summary>
        /// <returns></returns>
        public static void ValidacionAdministradorHydro(Exception ex, object objUsuarioId, string pAccion)
        {
            CLogTraza.MensajeUsuario oMensajeUsuario = new CLogTraza.MensajeUsuario();
            oMensajeUsuario.decUsuarioId = objUsuarioId;
            oMensajeUsuario.decIdModulo = CParametrosHydro.decIdAplicacion;
            oMensajeUsuario.strAccion = pAccion;
            oMensajeUsuario.strIp = HttpContext.Current.Request.UserHostAddress;
            oMensajeUsuario.decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion;
            CLogTraza.Error(oMensajeUsuario, ex);
        }

        public static bool EsPermitido(string modulo, string acceso)
        {
            bool permitido = true;
            try
            {
                permitido = VerificarPermiso(modulo, acceso,
                                             Convert.ToDecimal(
                                                 HttpContext.Current.Session[Parametros.CVariablesSesion.UsuarioId]));
            }
            catch (Exception)
            {
                permitido = false;
            }
            return permitido;
        }
        public static bool VerificarPermiso(string modulo, string acceso, decimal usuario)
        {
            return true;
        }

        #endregion
    }
}