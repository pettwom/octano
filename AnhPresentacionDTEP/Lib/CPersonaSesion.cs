
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroSesion;
using AnhPresentacionDTEP.Lib;
using AnhPresentacionDTEP.Parametros;
using Llaves.Anh.Dtic;
using System;
using System.Collections.Generic;
using System.Web;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Librerias
{
    public class CPersonaSesion
    {
        private readonly IServicioHydroSesion _servicio;        
        private string _mensajeError;

        public CPersonaSesion(string mapPath, string strIp)
        {
            _servicio = LocalizadorProxy.HydroSesion();            
        }      
        public O_USUARIO_CTY Autenticar(string strUsuario, string strClave)
        {
            try
            {
                O_USUARIO_CTY resultado = _servicio.Autenticar(CParametrosHydro.StrCredencialEmpadronamiento, strUsuario,strClave, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current,_mensajeError);
                    return null;
                }

            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name,exp);
                return null;
            }
        }

        public O_USUARIO_CTY AutenticarActDir(string strUsuario, string strClave)
        {
            try
            {
                var llaveSesion = new CSesion();
                string strLlaveAutenticacion = llaveSesion.GenerarLlaveAutenticacion(strUsuario, strClave);

                O_USUARIO_CTY usuario = _servicio.AutenticarActDir(CParametrosHydro.StrCredencialEmpadronamiento,strLlaveAutenticacion, ref _mensajeError);
                if (usuario != null)
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return null;
            }
        }

        public bool ExisteUsuario(string strUsuario)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.ExisteUsuario(CParametrosHydro.StrCredencialEmpadronamiento,
                    strUsuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO > 0;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return false;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return false;
            }
        }

        public bool CambiarClave(decimal decIdUsuario, string strClaveNueva)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.CambiarClave(2, decIdUsuario, "", strClaveNueva,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO > 0;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return false;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return false;
            }
        }

        public bool CambiarClave(decimal decIdUsuario, string strClaveAntigua, string strClaveNueva)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.CambiarClave(1, decIdUsuario, strClaveAntigua, strClaveNueva,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO > 0;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return false;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return false;
            }
        }

        public decimal RegistrarUsuario(string usuario)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.RegistrarUsuario(CParametrosHydro.StrCredencialEmpadronamiento,usuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return 0;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return 0;
            }
        }

        public bool ActivarUsuario(decimal decIdUsuario)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.ActivarUsuario(CParametrosHydro.StrCredencialEmpadronamiento,
                    decIdUsuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO > 0;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return false;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return false;
            }
        }

        public decimal ObtenerUsuario(string strUsuario)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.ExisteUsuario(CParametrosHydro.StrCredencialEmpadronamiento,
                    strUsuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return -1;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return -1;
            }
        }

        public O_DETALLE_USUARIO_CTY ObtenerUsuario(decimal decIdUsuario)
        {
            try
            {
                O_DETALLE_USUARIO_CTY resultado = _servicio.ObtenerUsuario(CParametrosHydro.StrCredencialEmpadronamiento, decIdUsuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return null;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return null;
            }
        }

        public List<O_MODULOS_USUARIO_CTY> ObtenerModulos(decimal decIdUsuario)
        {
            try
            {
                var resultado = _servicio.ObtenerModulosUsuario(decIdUsuario,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return null;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return null;
            }
        }

        public List<O_MENUS_USUARIO_CTY> ObtenerMenus(decimal decIdModulo, decimal decIdUsuario)
        {
            try
            {
                List<O_MENUS_USUARIO_CTY> resultado = _servicio.ObtenerMenusUsuario(decIdUsuario, decIdModulo, 0,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return null;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return null;
            }
        }

        public List<O_MENUS_USUARIO_CTY> ObtenerMenusPerfil(decimal decIdPerfil)
        {
            try
            {
                List<O_MENUS_USUARIO_CTY> resultado = _servicio.ObtenerMenusPerfil(decIdPerfil,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null && resultado.Count > 0)
                {
                    return resultado;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return null;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return null;
            }
        }

        public List<O_PERFILES_USUARIO_CTY> ObtenerPerfiles(decimal decIdUsuario, decimal decIdModulo)
        {
            try
            {
                List<O_PERFILES_USUARIO_CTY> resultado = _servicio.ObternerPerfilesUsuario(decIdUsuario, decIdModulo,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    CLog.Informacion(HttpContext.Current, _mensajeError);
                    return null;
                }
            }
            catch (Exception exp)
            {
                CLog.Error(HttpContext.Current, System.Reflection.MethodBase.GetCurrentMethod().Name, exp);
                return null;
            }
        }

        [Serializable]
        public class Usuario
        {
            #region Variables

            public int idPersona { get; set; }
            public string numeroIdentificacion { get; set; }
            public int expedicion { get; set; }
            public string complemento { get; set; }
            public string nombre { get; set; }
            public string primerApellido { get; set; }
            public string segundoApellido { get; set; }
            public long fechaNacimiento { get; set; }
            public int genero { get; set; }
            public string email { get; set; }
            public string clave { get; set; }
            public int idPerfil { get; set; }
            public int tipoIdentificacion { get; set; }
            public int idUsuario { get; set; }
            #endregion
        }

    }
}