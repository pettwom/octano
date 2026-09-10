//===================================================================================
// Agencia Nacional de Hidrocarburos
// Unidad de Sistemas
//=================================================================================== 
// Clase "CGestion.cs" implementa un Servicio de la Aplicación.
// Realiza altas, bajas, modificaciones y lista información de la entidad Enlaces
//===================================================================================
// Autor: Adolfo Jarsún Maire, Jesus Callisaya, Ruben Guancollo, Versión 1.
// Derechos Reservado Agencia Nacional de Hidrocraburos. 
// http://www.anh.gob.bo
//===================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioConsultasHydro;
using AnhHydroTalleresOpeGarrafasPresentacion.Parametros;
using AnhPresentacionDTEP.Lib;
using AnhPresentacionDTEP.Parametros;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Clases.Empadronamiento
{
    public class CHydroConsultas
    {
        #region Variables de Entorno
        /// <summary>
        /// Mensaje de error capturado de la invocación al servicio.
        /// </summary>
        private string _mensajeDeError = string.Empty;
        /// <summary>
        /// Dirección base de la aplicación en el servidor.
        /// </summary>
        private string _mapPath;
        /// <summary>
        /// Gestor del servicio Hydro Consultas
        /// </summary>
        private IServicioHydroConsultas _servicio;
        /// <summary>
        /// Recurso de registro de errores de la aplicación en su llamada al servicio.
        /// </summary>
        private CLogs _logs;
        #endregion

        #region Metodos y Funciones
        /// <summary>
        /// Inicializa los recursos necesarios para el funcionamiento de la clase, entre ellos el servicio y los logs.
        /// </summary>
        /// <param name="mapPath">Dirección base del servidor.</param>
        public CHydroConsultas(string mapPath, string strIp)
        {
            _servicio = LocalizadorProxy.ServicioConsultarHydro();
            _mapPath = mapPath;
            _logs = new CLogs(mapPath, strIp);
        }

        /// <summary>
        /// Devuelve el servicio Hydro Consultas.
        /// </summary>
        /// <returns>El servicio</returns>
        public IServicioHydroConsultas obtenerServicio()
        {
            return _servicio;
        }

        /// <summary>
        /// Busca personas su número de carnet de identidad.
        /// </summary>
        /// <param name="ci">El CI de la persona.</param>
        /// <returns>Las lista de personas encontradas.</returns>
        public List<O_PERSONA_CTY> obtenerPersonaPorCi(string ci)
        {
            try  
            {
                List<O_PERSONA_CTY> listadoPersonas = _servicio.ObtenerPersonaPorCI(CParametrosHydro.StrCredencialEmpadronamiento, ci, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    _logs.Error(_mensajeDeError);
                }
                return listadoPersonas;
            }
            catch (Exception exp)
            {
                _logs.Error(exp);
                return null;
            }
        }

        public List<O_ENTIDAD_CTY> obtenerEntidadesPorDocumento(decimal idTipoDocumento, string cite)
        {
            try
            {
                List<O_ENTIDAD_CTY> listadoEntidades = _servicio.ObtenerEntidadPorDocumento(CParametrosHydro.StrCredencialEmpadronamiento, cite, idTipoDocumento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    _logs.Error(_mensajeDeError);
                }
                return listadoEntidades;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<O_ENTIDAD_CTY> obtenerEntidadesPorNombre(string strNombre)
        {
            try
            {
                List<O_ENTIDAD_CTY> listadoEntidades = _servicio.ObtenerEntidadPorNombre(CParametrosHydro.StrCredencialEmpadronamiento, strNombre, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    _logs.Error(_mensajeDeError);
                }
                return listadoEntidades;
            }
            catch (Exception exp)
            {
                _logs.Error(exp);
                return null;
            }
        }

        public O_CORRELATIVO_CTY generarCorrelativo(decimal decIdTipoDocumento)
        {
            try
            {
                O_CORRELATIVO_CTY correlativo = _servicio.GenerarCorrelativo(0, 1, decIdTipoDocumento, CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    _logs.Error(_mensajeDeError);
                }
                return correlativo;
            }
            catch (Exception exp)
            {
                _logs.Error(exp);
                return null;
            }
        }
        #endregion
    }
}