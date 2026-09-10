using Librerias.Anh.Us;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AnhPresentacionDTEP.Parametros
{
    public class CParametrosHydro
    {
        #region Variables de entorno para Configuracion de Servidor de Correo

        /// <summary>
        /// Direccion IP del servidor
        /// </summary>
        public static readonly string StrServidorDireccion =ConfigurationManager.AppSettings["CorreoServidor"];//172.17.20.41
        //public static string strServidorDireccion = "172.17.20.14";//172.17.20.41

        /// <summary>
        /// Puerto SMTP del servidor
        /// </summary>
        public static int IntServidorPuerto = Convert.ToInt32(ConfigurationManager.AppSettings["CorreoPuerto"]);
        //public static int intServidorPuerto = 25;

        /// <summary>
        /// Cuenta de origen
        /// </summary>
        public static string StrUsuarioDe = ConfigurationManager.AppSettings["CorreoDe"];
        //public static string strUsuarioDe = "sistemas@anh.gob.bo";

        /// <summary>
        /// Cuenta de copia de email
        /// </summary>
        public static string StrUsuarioCc = "";

        /// <summary>
        /// Cuenta de copia oculta
        /// </summary>
        public static string StrUsuarioCco = ConfigurationManager.AppSettings["CorreoCco"];
       // public static string strUsuarioCco = "jpflores@anh.gob.bo, ebanda@anh.gob.bo";

        //public static string strUsuarioCco = "sistemas@anh.gob.bo, abenitez@anh.gob.bo, mpaco@anh.gob.bo";

        /// <summary>
        /// Login de cuenta origen
        /// </summary>
        public static string StrUsuarioLogin = ConfigurationManager.AppSettings["CorreoLogin"];

        /// <summary>
        /// Passord de cuenta origen
        /// </summary>
        public static string StrUsuarioPassword = ConfigurationManager.AppSettings["CorreoClave"];

        /// <summary>
        /// Habilitar SSl email
        /// </summary>
        public static bool bolHabilitarSsl = false;

        /// <summary>
        /// Mostrar Excepcion de error envio email
        /// </summary>
        public static bool bolNotificarError = true;

        #endregion

        #region Variables de entorno para el Capcha

        /// <summary>
        /// Cantidad d caracteres visualizados en el capcha
        /// </summary>
        public static int intCantidadCaracteres = 6;

        /// <summary>
        /// Ancho de la imagen capcha
        /// </summary>
        public static int intAncho = 300;

        /// <summary>
        /// Alto de la imagen capcha
        /// </summary>
        public static int intAlto = 60;

        /// <summary>
        /// Tamaño de la fuente capcha
        /// </summary>
        public static int intTamañoFuente = 50;

        /// <summary>
        /// Color del capcha
        /// </summary>
        //public static Color colorCapcha = Color.YellowGreen;

        #endregion

        #region Variables ActiveDirectory

        /// <summary>
        /// Nombre de Dominio donde se ejecuta loa aplicacion LDAP
        /// </summary>
        public static string strDominioLdap = "LDAP://anh.gob.bo";

        /// <summary>
        /// Nombre de Dominio 
        /// </summary>
        public static string strDominio = "anh";

        #endregion

        #region Credenciales

        public static string strCredencialEmpadronamiento = CEncriptacion.generarMD5("anh321.");
        public static string strCredencialSirasat = CEncriptacion.generarMD5("anh321.");
        public static string strCredencialInstaladoras = CEncriptacion.generarMD5("anh321.");
        public static string strCredencialHydroAdmin = "EBF8FDEFA7ED24CF8C47EE0B149FD4F5";
        public static string StrCredencialSisConDoc = "EBF8FDEFA7ED24CF8C47EE0B149FD4F5";



        #endregion

        #region Variables de aplicacion

        public static decimal decIdAplicacion = Convert.ToDecimal(ConfigurationManager.AppSettings["idAplicacion"]);
        //public static string strUrlRaiz = ConfigurationManager.AppSettings["UrlRaiz"] + "";

        #endregion

        public static string StrVolumen = ConfigurationManager.AppSettings["Volumen"];
        public static string StrPoderCalorifico = ConfigurationManager.AppSettings["PoderCalorifico"];
        public static string StrGlp = ConfigurationManager.AppSettings["Glp"];
        public static string StrPuntoRocio = ConfigurationManager.AppSettings["PuntoRocio"];
        public static string StrProduccionGlp = ConfigurationManager.AppSettings["ProduccionGlp"];
        public static string StrProduccionPropano = ConfigurationManager.AppSettings["ProduccionPropano"];
        public static string StrRendimientoProduccionGlp = ConfigurationManager.AppSettings["RendimientoProduccionGlp"];

        public static string StrGasCombustible = ConfigurationManager.AppSettings["GasCombustible"];
        public static string StrEntregaConsumoPropano = ConfigurationManager.AppSettings["EntregaConsumoPropano"];
        public static string StrQuemaGas = ConfigurationManager.AppSettings["QuemaGas"];        
        public static string StrEntregaGlpCisterna = ConfigurationManager.AppSettings["EntregaGlpCisterna"];
        public static string StrEntregaGlpDucto = ConfigurationManager.AppSettings["EntregaGlpDucto"];
        public static string StrSaldoGlp = ConfigurationManager.AppSettings["SaldoGlp"];
        public static string StrSaldoPropano = ConfigurationManager.AppSettings["SaldoPropano"];
        public static string StrTemperatura = ConfigurationManager.AppSettings["Temperatura"];
        public static string StrGasolinaNatural = ConfigurationManager.AppSettings["GasolinaNatural"];

        #region Variables de Aplicacion

        /// <summary>
        /// Direccion IP del servidor
        /// </summary>
        //public static string strCredencial = CEncriptacion.generarMD5("OCTANO");
        public static string strCredencial = "83809AD945F1F72D0EA9FDA0E599E0B3"; // HYDRO-OACTANO.APP.CONTROLCALIDAD MD5
        //public static string strCredencial = "GEBELL.2070.20130509"; // HYDRO-OACTANO.APP.CONTROLCALIDAD MD5

        /// <summary>
        /// Formato de fecha para el envio a los servicios
        /// </summary>
        public static string strFormatoFechaServ = "yyyyMMddHHmmss";

        #endregion
    }
}