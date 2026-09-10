using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using Librerias.Anh.Us;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnhPersistenciaCore.Aplicacion.Reportes
{
    public class CPersistenciaReportes
    { 
        #region Variables
        private readonly CAuditoria.Logging _objDirectorio;
        private readonly string _directorioLog = ConfigurationManager.AppSettings["Directorio"];

        private const string TipoMensajeError = "Repositorio";

        #endregion

        #region Constructor

        public CPersistenciaReportes()
        {
            try
            {
                _objDirectorio = new CAuditoria.Logging { Ruta = _directorioLog };
            }
            catch (Exception exp)
            {
                _objDirectorio = new CAuditoria.Logging("CONTR_critico_");
                _objDirectorio.RegistrarError(DateTime.Now.ToString() + "|" + exp.Message + "|" + exp.StackTrace);
            }
        }

        #endregion

        /// <summary>
        /// Registra los datos de detalle del reporte de calidad.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema</param>
        /// <param name="decIdRegistroCalidadPrincipal">Identificador del registro principal de calidad</param>
        /// <param name="strMensajeError">Mensaje de error</param>
        /// <returns></returns>
        public List<O_REPORTE_CALIDAD> ReportarCalidad(string strCredencial, string decIdRegistroCalidadPrincipal, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_REPORTE_CALIDAD> objListado =
                        ctx.PUSR_REPORTES_P_REPORTE_CALIDAD(strCredencial,decIdRegistroCalidadPrincipal).ToList();
                    return objListado;
                } 
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Reportes: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdRegistroCalidadPrincipal };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Registra los datos principales del reporte de calidad.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdUsuarioANH">Identificador de usuario ANH.</param>
        /// <param name="strFechaInicial">Fecha inicial para el reporte.</param>
        /// <param name="strFechaFinal">Fecha final para el reporte.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns></returns>
        public List<O_REPORTE_CALIDAD_PRINCIPAL> ReportarCalidadPrincipal(string strCredencial, decimal decIdEntidad, decimal decIdUsuarioANH, string strFechaInicial, string strFechaFinal, decimal decEstado, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_REPORTE_CALIDAD_PRINCIPAL> objListado =
                        ctx.PUSR_REPORTES_P_REPORTE_CALIDAD_PRINCIPAL(decIdEntidad, decIdUsuarioANH, strFechaInicial, strFechaFinal, decEstado).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Reportes: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdEntidad };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }
        public List<O_REPORTE_CALIDAD_REG_ADM> ReportarCalidadPrincipalExcel(string strCredencial, decimal decIdEntidad, decimal decIdUsuarioANH, string strFechaInicial, string strFechaFinal, decimal decEstado, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano()) 
            {
                try
                {
                    List<O_REPORTE_CALIDAD_REG_ADM> objListado =
                        ctx.PUSR_REPORTES_P_LISTADO_REGISTROS_ADM(decIdEntidad, decIdUsuarioANH, strFechaInicial, strFechaFinal, decEstado).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Reportes: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdEntidad };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista reportes de calidad por correlativo o parametros de fecha para el servicio web Externo.
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="strCorrelativo">Correlativo del certificado de calidad.</param>
        /// <param name="decFechaInicial">Parametro de fecha inicial.</param>
        /// <param name="decFechaFinal">Parametro de fecha final.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos E_REPORTE_CALIDAD.</returns>
        public List<E_REPORTE_CALIDAD> ReportarRegistroCalidad(string strLlave, string strCorrelativo, decimal decFechaInicial, decimal decFechaFinal,ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    E_REPORTE_CALIDAD vObjReporteCalidad = new E_REPORTE_CALIDAD();
                    List<E_REPORTE_CALIDAD> vColReporteCalidad = new List<E_REPORTE_CALIDAD>();
                    List<O_REPORTE_REG_CALIDAD_CTY> objListado = ctx.PUSR_REPORTES_P_REPORTE_REGISTRO_CALIDAD(strLlave,strCorrelativo,decFechaInicial,decFechaFinal).ToList();
                    vObjReporteCalidad.TABLA_ESPECIFICA = new List<TABLA_ESPECIFICA>();
                   
                    #region Cargado de los listados
                    for (int i = 0; i < objListado.Count; i++)
                    {
                        vObjReporteCalidad.CORRELATIVO = objListado[i].CORRELATIVO;
                        vObjReporteCalidad.CODIGO_CERTIFICADO_CALIDAD = objListado[i].CODIGO_CERTIFICADO_CALIDAD;
                        vObjReporteCalidad.PRODUCTO = objListado[i].VALOR_PRODUCTO;
                        vObjReporteCalidad.FECHA_OPERACION = objListado[i].FECHA_OPERACION;
                        vObjReporteCalidad.VOLUMEN = objListado[i].VOLUMEN;
                        vObjReporteCalidad.VOLUMEN_UNIDAD_MEDIDA = objListado[i].VALOR_VOLUMEN_UNIDAD_MEDIDA;
                        vObjReporteCalidad.PUNTO_TRANS_CUSTODIO = objListado[i].VALOR_PUNTO_CUSTODIO;
                        vObjReporteCalidad.RESOLUCION = objListado[i].RESOLUCION;
                        vObjReporteCalidad.VOLUMEN_MUESTRA = objListado[i].VOLUMEN_MUESTRA;
                        vObjReporteCalidad.PRECIO = objListado[i].PRECIO;
                        vObjReporteCalidad.MONEDA = objListado[i].CODIGO_MONEDA;
                        vObjReporteCalidad.MARCA_PRODUCTO = objListado[i].VALOR_MARCA_PRODUCTO;
                        vObjReporteCalidad.NOMBRE_PRODUCTO = objListado[i].NOMBRE_PRODUCTO;
                        
                        TABLA_ESPECIFICA vObjTablaEspecifica = new TABLA_ESPECIFICA();
                        vObjTablaEspecifica.PRUEBA_CALIDAD = objListado[i].PRUEBA_CALIDAD;
                        vObjTablaEspecifica.METODO_ASTM = objListado[i].VALOR_METODO_ASTM;
                        vObjTablaEspecifica.UNIDAD_MEDIDA = objListado[i].VALOR_UNIDAD_MEDIDA;
                        vObjTablaEspecifica.VALOR_REPORTADO = objListado[i].VALOR_REPORTADO;
                        vObjTablaEspecifica.RANGOS_MULTIPLES = objListado[i].RANGOS_MULTIPLES;
                        
                        vObjReporteCalidad.TABLA_ESPECIFICA.Add(vObjTablaEspecifica);

                        if (i >= objListado.Count - 1 || objListado[i].CORRELATIVO != objListado[i + 1].CORRELATIVO)
                        {
                            vColReporteCalidad.Add(vObjReporteCalidad);
                            vObjReporteCalidad = new E_REPORTE_CALIDAD();
                            vObjReporteCalidad.TABLA_ESPECIFICA = new List<TABLA_ESPECIFICA>();
                        }
                    }
                    #endregion
                    return vColReporteCalidad;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista reportes de alertas (lotes) de certificados por parametros de fecha.
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdEntidad">Identificador de la Entidad.</param>
        /// <param name="decIdUsuarioAnh">Usuario ANH Supervisor y Administrador.</param>
        /// <param name="decFechaInicial">Parametro de fecha inicial.</param>
        /// <param name="decFechaFinal">Parametro de fecha final.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_REPORTE_ALERTA_CERT_CTY.</returns>
        public List<O_REPORTE_ALERTA_CERT_CTY> ReportarAlertaCertificado(string strLlave, decimal decIdEntidad, decimal decIdUsuarioAnh, decimal decFechaInicial, decimal decFechaFinal, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_REPORTE_ALERTA_CERT_CTY> objListado = ctx.PUSR_REPORTES_P_REPORTE_ALERTA_CERTIFICADO(strLlave, decIdEntidad, decIdUsuarioAnh, decFechaInicial, decFechaFinal).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista reportes certificados de calidad para pivote.
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdEntidad">Identificador de la Entidad.</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de Actividad.</param>
        /// <param name="strCite">Correlativo de los certificados de claidad.</param>
        /// <param name="decFechaInicio">Parametro de fecha inicial.</param>
        /// <param name="decFechaFin">Parametro de fecha final.</param>
        /// <param name="decIdUsuario">Identificador del usuario.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_REPORTE_ALERTA_CERT_CTY.</returns>
        public List<O_REPORTE_CALIDAD_PIVOTE_CTY> ReportarCertificadosPivote(string strLlave, decimal decIdEntidad, decimal decIdTipoActividad, string strCite, decimal decFechaInicio, decimal decFechaFin, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_REPORTE_CALIDAD_PIVOTE_CTY> objListado = ctx.PUSR_REPORTES_P_REPORTE_CALIDAD_PIVOTE(strLlave, decIdEntidad, decIdTipoActividad, strCite, decFechaInicio, decFechaFin, decIdUsuario).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        public O_RESULTADO_INF_CTY GestionModificaDatos(string strConsulta, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                O_RESULTADO_INF_CTY resultado = null;
                try
                {
                    O_RESULTADO_INF_CTY listadoResultado = ctx.PUSR_GESTION_P_MODIFICAR_INFORMACION(strConsulta).FirstOrDefault();
                    resultado = listadoResultado;
                }
                catch (Exception ex)
                {
                    resultado = new O_RESULTADO_INF_CTY
                    {
                        ID_RESULTADO = 1
                    };
                }
                return resultado;
            }
        }



        #region Movimiento de volúmenes

        /// <summary>
        /// Reporte del gas de alimento regitrado para una entidad (planta).
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al módulo.</param>
        /// <param name="decIdTipoReporte">Identificador del reporte.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdUnidadMedidaDestino">Representa a la unidad de medida.</param>
        /// <param name="decFechaIni">Fecha de inicio para el reporte.</param>
        /// <param name="decFechaFin">Fecha de finalizacion para el reporte.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Lista de objetos O_REPORTE_VOL_DTEP.</returns>
        public List<O_REPORTE_VOL_DTEP> ReportarDtepGasAlimento(string strCredencial, decimal decIdTipoReporte, decimal decIdEntidad, decimal decIdUnidadMedidaDestino, decimal decFechaIni, decimal decFechaFin, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_REPORTE_VOL_DTEP> objListado =
                        ctx.PCAN_REPORTES_REPORTE_DTEP_GAS_ALIMENTO(strCredencial, decIdTipoReporte,
                                                                    decIdEntidad, decIdUnidadMedidaDestino, decFechaIni, decFechaFin).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                    return null;
                }
            }
        }

        /// <summary>
        /// Reporte de la producción regitrada para una entidad (planta).
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al módulo.</param>
        /// <param name="decIdTipoReporte">Identificador del reporte.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdUnidadMedidaDestino">Representa a la unidad de medida.</param>
        /// <param name="decFechaIni">Fecha de inicio para el reporte.</param>
        /// <param name="decFechaFin">Fecha de finalizacion para el reporte.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Lista de objetos O_REPORTE_VOL_DTEP.</returns>
        public List<O_REPORTE_VOL_DTEP> ReportarDtepProduccion(string strCredencial, decimal decIdTipoReporte, decimal decIdEntidad, decimal decIdUnidadMedidaDestino, decimal decFechaIni, decimal decFechaFin, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_TIPO_MEDIO_TRANS_CTY> objMedioTransporte =
                        ctx.PCAN_LISTADOS_P_LISTADO_TIPO_MEDIO_TRANSPORT(strCredencial, "",0).
                            ToList();

                    List<O_LISTA_TIPO_MEDIO_TRANS_CTY> objProductoAuxiliar = (from mt in objMedioTransporte
                                                                              where
                                                                                  mt.DESCRIPCION.ToUpper().
                                                                                  Contains(
                                                                                      "CISTERNA")
                                                                              select mt).ToList();
                    decimal decIdCisterna = objProductoAuxiliar[0].ID_TIPO_MEDIO_TRANSPORTE;

                    objProductoAuxiliar = (from mt in objMedioTransporte
                                           where mt.DESCRIPCION.ToUpper().Contains("DUCTO")
                                           select mt).ToList();
                    decimal decIdDucto = objProductoAuxiliar[0].ID_TIPO_MEDIO_TRANSPORTE;

                    List<O_REPORTE_VOL_DTEP> objListado =
                        ctx.PCAN_REPORTES_REPORTE_DTEP_PRODUCCION(strCredencial, decIdTipoReporte, decIdCisterna,
                                                                  decIdDucto, decIdEntidad, decIdUnidadMedidaDestino,
                                                                  decFechaIni, decFechaFin).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                    return null;
                }
            }
        }

        /// <summary>
        /// Reporte del gas residual regitrado para una entidad (planta).
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al módulo.</param>
        /// <param name="decIdTipoReporte">Identificador del reporte.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdUnidadMedidaDestino">Representa a la unidad de medida.</param>
        /// <param name="decFechaIni">Fecha de inicio para el reporte.</param>
        /// <param name="decFechaFin">Fecha de finalizacion para el reporte.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Lista de objetos O_REPORTE_VOL_DTEP.</returns>
        public List<O_REPORTE_VOL_DTEP> ReportarDtepGasResidual(string strCredencial, decimal decIdTipoReporte, decimal decIdEntidad, decimal decIdUnidadMedidaDestino, decimal decFechaIni, decimal decFechaFin, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_REPORTE_VOL_DTEP> objListado =
                        ctx.PCAN_REPORTES_REPORTE_DTEP_GAS_RESIDUAL(strCredencial, decIdTipoReporte,
                                                                    decIdEntidad, decIdUnidadMedidaDestino, decFechaIni, decFechaFin).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                    return null;
                }
            }
        }

        /// <summary>
        /// Listar los tipos de reporte.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al modulo.</param>
        /// <param name="strDescripcion">Descripcion del tipo de reporte, si se envia vacio listara todos los tipos de reporte.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Listado de objetos O_TIPOS_REPORTE_CTY.</returns>
        public List<O_TIPOS_REPORTE_CTY> ListarTiposReporte(string strCredencial, string strDescripcion,
                                                            ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_TIPOS_REPORTE_CTY> objListado =
                        ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_REPORTE(strCredencial, strDescripcion).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                    return null;
                }
            }
        }

        #endregion
    }
}
