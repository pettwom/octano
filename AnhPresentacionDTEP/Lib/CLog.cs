using System;
using System.Web;
using AnhPresentacionDTEP.Parametros;
using Librerias.Anh.Us;

namespace AnhPresentacionDTEP.Lib
{
    public class CLog
    {
        public static void Error(HttpContext contexto, string metodo,Exception ex)
        {
            try
            {
                CLogTraza.MensajeUsuario oMensajeUsuario = new CLogTraza.MensajeUsuario();
                oMensajeUsuario.decUsuarioId = CVariablesSesion.Usuario(contexto).ID_USUARIO;
                // contexto.Session[CVariablesSesion.IdUsuario];
                oMensajeUsuario.decIdModulo = CParametrosHydro.decIdAplicacion;
                oMensajeUsuario.strAccion = metodo;
                oMensajeUsuario.strIp = HttpContext.Current.Request.UserHostAddress;

                CLogTraza.Error(oMensajeUsuario, ex);
            }
            catch (Exception e)
            {
                
            }
        }
        public static void Depuracion(HttpContext contexto,string mensaje,string metodo,string detalleAccion)
        {
            decimal idModulo=CParametrosHydro.decIdAplicacion;
            decimal idUsuario=CVariablesSesion.Usuario(contexto).ID_USUARIO;
            string strIp=HttpContext.Current.Request.UserHostAddress;
            CLogTraza.Depuracion(mensaje, metodo, idModulo, idUsuario, strIp, detalleAccion);
        }
        public static void Alerta(HttpContext contexto,string mensaje,string metodo, string ex)
        {
            decimal idModulo = CParametrosHydro.decIdAplicacion;
            decimal idUsuario = CVariablesSesion.Usuario(contexto).ID_USUARIO;
            string strIp=HttpContext.Current.Request.UserHostAddress;
            CLogTraza.Advertencia(mensaje,metodo,idModulo,idUsuario,strIp,ex,"0");
        }
        public static void Informacion(HttpContext contexto,string mensaje)
        {
            decimal idModulo = CParametrosHydro.decIdAplicacion;
            decimal idUsuario = CVariablesSesion.Usuario(contexto).ID_USUARIO;
            string strIp=HttpContext.Current.Request.UserHostAddress;
            CLogTraza.Informacion(mensaje, idModulo, idUsuario, strIp);
        }
    }
}