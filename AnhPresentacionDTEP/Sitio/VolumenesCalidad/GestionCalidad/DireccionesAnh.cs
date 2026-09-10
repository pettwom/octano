using System;
using System.Configuration;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad
{
    //public enum IdDireccionAnh
    //{
    //    Druin = 17,
    //    Dcd = 18,
    //    //Dcd = 29,
    //    DteyP = 5,
    //    Anh = 0
    //}
    public static class DireccionesAnh
    {

        public static decimal svc_obtener_idImportadores { get { return Convert.ToDecimal(GetConfigSettingItem("idImportadores")); } }
        public static decimal svc_obtener_idTerminalesAlmacenaje { get { return Convert.ToDecimal(GetConfigSettingItem("idTerminalesAlmacenaje")); } }
        public static decimal svc_obtener_idIndustrializacion { get { return Convert.ToDecimal(GetConfigSettingItem("idIndustrializacion")); } }
        public static decimal svc_obtener_idRefinacion { get { return Convert.ToDecimal(GetConfigSettingItem("idRefinacion")); } }
        public static decimal svc_obtener_idAeropuerto { get { return Convert.ToDecimal(GetConfigSettingItem("idAeropuerto")); } }
        public static decimal svc_obtener_idProcGasNatEnCampo { get { return Convert.ToDecimal(GetConfigSettingItem("idProcGasNatEnCampo")); } }
        public static decimal svc_obtener_idMayorista { get { return Convert.ToDecimal(GetConfigSettingItem("idMayorista")); } }
        public static decimal svc_obtener_idPSLs { get { return Convert.ToDecimal(GetConfigSettingItem("idPSLs")); } }
        public static decimal svc_obtener_idAnh { get { return Convert.ToDecimal(GetConfigSettingItem("idAnh")); } }
        
        private const string MISSING_CONFIG = "Invalid configuration. Required AppSettings section is missing";
        private const string INVALID_CONFIG_SETTING = "Invalid configuration setting name: {0}";


        private static string GetConfigSettingItem(string name)
        {
            if (ConfigurationManager.AppSettings == null)
                throw new ConfigurationErrorsException(MISSING_CONFIG);

            string value = null;
            if (ConfigurationManager.AppSettings.Count != 0)
            {
                try
                {
                    value = ConfigurationManager.AppSettings.Get(name);
                }
                catch (Exception exception)
                {
                    throw new ConfigurationErrorsException(SettingItemErrorMessage(name, exception));
                }
            }
            return value;
        }

        private static string SettingItemErrorMessage(string name, Exception exception)
        {
            return string.Format(INVALID_CONFIG_SETTING, name) + exception.Message;
        }
    }
}