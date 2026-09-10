using System.Configuration;
using System.Drawing;
using Librerias.Anh.Us;

namespace AnhPresentacionDTEP.Parametros.VolumenesCalidad
{
    public static class cParametrosHydro
    {
        #region Variables de Aplicacion

        /// <summary>
        /// Direccion IP del servidor
        /// </summary>
        //public static string strCredencial = CEncriptacion.generarMD5("OCTANO");
        public static string strCredencial = "83809AD945F1F72D0EA9FDA0E599E0B3"; // HYDRO-OACTANO.APP.CONTROLCALIDAD MD5
        //public static string strCredencial = "GEBELL.2070.20130509"; // HYDRO-OACTANO.APP.CONTROLCALIDAD MD5
        public static string strCredencialEmpadronamiento = CEncriptacion.generarMD5("anh321.");

        /// <summary>
        /// Nombre de Dominio donde se ejecuta loa aplicacion LDAP
        /// </summary>
        public static string strDominioLdap = "LDAP://anh.gob.bo";

        /// <summary>
        /// Nombre de Dominio 
        /// </summary>
        public static string strDominio = "anh";

        /// <summary>
        /// Formato de fecha para el envio a los servicios
        /// </summary>
        public static string strFormatoFechaServ = "yyyyMMddHHmmss";
        public static string fechaIso2015 = ConfigurationManager.AppSettings["fechaIso2015"];
        #endregion
    }
}