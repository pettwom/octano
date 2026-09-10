using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using Librerias.Anh.Us;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnhPersistenciaCore.Aplicacion.Listados
{
    public class CPersistenciaListados
    {
        #region "Variables"
        private readonly CAuditoria.Logging _objDirectorio;
        private readonly string _directorioLog = ConfigurationManager.AppSettings["Directorio"];
        #endregion

        #region Variables

        private const string TipoMensajeError = "Repositorio";

        #endregion

        #region Constructor

        public CPersistenciaListados()
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
        /// Obtiene la ultima fecha en la cual se registro un movimiento de volumenes
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al modulo</param>
        /// <param name="decIdCampo">Identificador de la corriente/campo</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="decIdTipoReporte">Identificador del tipo de reporte</param>
        /// <param name="decIdTipoOperacion">Identificador del tipo de operacion</param>
        /// <param name="decFechaOperacion">fecha de operacion que se quiere buscar, 0 para no buscar por fecha</param>
        /// <param name="strMensajeError">Mensaje de error</param>
        /// <returns>Listado de objetos O_FECHA_ULTIMO_REPORTE_CTY</returns>
        public List<O_FECHA_ULTIMO_REPORTE_CTY> ObtenerFechaUltimoReporte(string strCredencial, decimal decIdCampo, decimal decIdEntidad, decimal decIdTipoReporte, decimal decIdTipoOperacion, decimal decFechaOperacion, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_FECHA_ULTIMO_REPORTE_CTY> objListado =
                        ctx.PCAN_LISTADOS_P_FECHA_ULTIMO_REPORTE(strCredencial, decIdCampo, decIdEntidad,
                                                                            decIdTipoReporte, decIdTipoOperacion, decFechaOperacion).
                            ToList();
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
        /// lista las tablas de especificacion de un producto dado segun su estado
        /// </summary>
        /// <param name="strCredencial">credencial de acceso al sistema</param>
        /// <param name="decIdEstadoTipoPrueba">identificador del tipo de prueba (completa, inicial, etc.)</param>
        /// <param name="decIdProductoPadre">identificador del producto padre</param>
        /// <param name="strMensajeError">mensaje de error</param>
        /// <returns>lista de objetos O_PRODUCTO_ESPEC_CTY</returns>
        public List<O_PRODUCTO_ESPEC_CTY> ListarProductoEspecificacion(string strCredencial, decimal decIdEstadoTipoPrueba, decimal decIdProductoPadre, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_PRODUCTO_ESPEC_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_PRODUCTO_ESPEC(decIdEstadoTipoPrueba, decIdProductoPadre).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdEstadoTipoPrueba, decIdProductoPadre };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }
        /*
        /// <summary>
        /// Nos lista los datos de prueba de calidad dado el identificador
        /// de la tabla ANH_CCC.TCCC_TABLAS_ESPEC
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema</param>
        /// <param name="decIdTablaEspecificacion">Identificador de la Tabla de Especificacion</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="strMensajeError">Mensaje de error</param>
        /// <returns>Lista de objetos ArrayList</returns>        
        public List<O_TABLA_ESPEC_FORMULARIO_CITY> ListarPruebasCalidadTablaEspecificacionFormulario(string strCredencial, decimal decIdTablaEspecificacion, decimal decIdEntidad, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {                    
                    List<O_TABLA_ESPEC_FORMULARIO_CITY> vColFormularioEspecifico = null;
                    vColFormularioEspecifico = ctx.PUSR_LISTADOS_P_LISTADO_PRUEBAS_TESPEC_FORM(decIdTablaEspecificacion, decIdEntidad).ToList();                    
                    return vColFormularioEspecifico;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdTablaEspecificacion };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }
        */
        /// <summary>
        /// Lista los grupos de productos como ser Carburantes, Lubricantes para plantas
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema</param>
        /// <param name="decIdEstadoTipoPrueba">Identificador del grupo</param>
        /// <param name="decIdEntidad">Identificador de la Entidad</param>
        /// <param name="decIdTipoActividad">Identificador de la Actividad</param>
        /// <param name="decIdUsuario">Identificador de Usuario</param>
        /// <param name="strMensajeError">Mensaje de error</param>
        /// <returns>Lista de objetos O_PROD_ESPEC_PACL_CTY</returns>
        public List<O_PROD_ESPEC_PACL_CTY> ListarGruposProductoEspecificacionPACL(string strCredencial, decimal decIdEntidad, decimal decIdTipoActividad, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_PROD_ESPEC_PACL_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_TIPO_PROD_CAL(strCredencial, decIdEntidad, decIdTipoActividad, decIdUsuario).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdEntidad };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista los grupos de productos como ser Carburantes, Lubricantes para plantas
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema</param>
        /// <param name="decIdUsuario">Identificador del usuario</param>
        /// <param name="decIdProductoPadre">Identificador del grupo padre</param>
        /// <param name="strAplicacionOrigen">Nombre de la aplicacion procedencia (ej:APP_OCT,APP_SIREL)</param>
        /// <param name="strMensajeError">Mensaje de error</param>
        /// <returns>Lista de objetos O_PROD_CAL_CTY</returns>
        public List<O_PROD_CAL_CTY> ListarGruposProductoEspecificacionCalidad(string strCredencial,  decimal decIdUsuario, decimal decIdTipoActividad, decimal decIdProductoPadre, string strAplicacionOrigen, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_PROD_CAL_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_PROD_CAL(strCredencial, decIdUsuario, decIdTipoActividad, decIdProductoPadre, strAplicacionOrigen).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdUsuario, decIdProductoPadre, strAplicacionOrigen };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista los tanques para pruebas de calidad de acuerdo a la entidad
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="strMensajeError">Mensaje de error</param>
        /// <returns>Lista de objetos O_LISTA_TANQUE_ENTIDAD_CTY</returns>
        public List<O_LISTA_TANQUE_ENTIDAD_CTY> ListarTanquePorEntidad(string strCredencial, decimal decIdEntidad, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_TANQUE_ENTIDAD_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_TANQUES_POR_ENTIDAD(strCredencial, decIdEntidad).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdEntidad };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista unidades de medida de cantidad (condicionadas) para pruebas de calidad de acuerdo a la entidad.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="decIdUsuario">Identificador del usuario.</param>
        /// <param name="strMensajeError">Mensaje de error</param>
        /// <returns>Lista de objetos O_UM_CANT_CAL_CTY</returns>
        public List<O_UM_CANT_CAL_CTY> ListarUnidadMedidaPorUsuario(string strCredencial, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_UM_CANT_CAL_CTY> objListado = 
                        ctx.PUSR_LISTADOS_P_LISTADO_UM_CANT_CAL(strCredencial, decIdUsuario).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdUsuario };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Nos lista los datos de prueba de calidad dado el identificador
        /// de la tabla ANH_CCC.TCCC_TABLAS_ESPEC
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema</param>
        /// <param name="decIdTablaEspecificacion">Identificador de la Tabla de Especificacion</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="strMensajeError">Mensaje de error</param>
        /// <returns>Lista de objetos ArrayList</returns>        
        public List<E_TABLA_ESPECIFICA> ListarPruebasCalidadTablaEspecificacion(string strCredencial, decimal decIdTablaEspecificacion, decimal decIdEntidad, decimal decFecha, string strCite, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_TABLA_ESPEC_FORMULARIO_CTY> vColFormularioEspecifico = null;
                    vColFormularioEspecifico = ctx.PUSR_LISTADOS_P_LISTADO_PRUEBAS_ESPEC(strCredencial, decIdTablaEspecificacion, decIdEntidad, decFecha, strCite).ToList();

                    E_TABLA_ESPECIFICA vFormularioEspecifico= new E_TABLA_ESPECIFICA();
                    List<E_TABLA_ESPECIFICA> vListFormularioEspecifico = new List<E_TABLA_ESPECIFICA>();

                    String[] vRangoMultiple = null;
                    String[] vUnidadMedida = null;
                    String[] vMetodoAstm = null;
                    String[] vEspecMinimaRango = null;
                    String[] vEspecMaximaRango = null;
                    
                    foreach (var vObjFormularioEspecifico in vColFormularioEspecifico)
                    {
                        vFormularioEspecifico = new E_TABLA_ESPECIFICA();

                        vFormularioEspecifico.decIdPruebaCalidad = Convert.ToDecimal(vObjFormularioEspecifico.ID_PRUEBA_CALIDAD);
                        vFormularioEspecifico.decIdPruebaCalidadPadre = Convert.ToDecimal(vObjFormularioEspecifico.ID_PRUEBA_CALIDAD_PADRE);
                        vFormularioEspecifico.strDescripcion = vObjFormularioEspecifico.DESCRIPCION.ToString();
                        if (vObjFormularioEspecifico.ESPEC_MINIMA != null)
                        {
                            vFormularioEspecifico.strEspecMinima = vObjFormularioEspecifico.ESPEC_MINIMA.Trim();
                        }
                        if (vObjFormularioEspecifico.ESPEC_MAXIMA !=null)
                        {
                            vFormularioEspecifico.strEspecMaxima = vObjFormularioEspecifico.ESPEC_MAXIMA.Trim();
                        }
                        if (vObjFormularioEspecifico.ESPEC_ALFANUMERICO !=null)
                        {
                            vFormularioEspecifico.strEspecAlfanumerico =vObjFormularioEspecifico.ESPEC_ALFANUMERICO.Trim();
                        }

                        if (vObjFormularioEspecifico.METODO_ASTM != null)
                        {
                            vMetodoAstm = vObjFormularioEspecifico.METODO_ASTM.Trim().Split('|');
                            E_CAMPO_VALOR objCampoValor= new E_CAMPO_VALOR();
                            vFormularioEspecifico.listMetodoAstm=new List<E_CAMPO_VALOR>();
                            foreach (var item1 in vMetodoAstm)
                            {
                                vMetodoAstm = item1.Trim().Split(',');
                                objCampoValor=new E_CAMPO_VALOR();

                                objCampoValor.intValor = Convert.ToDecimal(vMetodoAstm[0]);
                                objCampoValor.strCampo = vMetodoAstm[1];
                                vFormularioEspecifico.listMetodoAstm.Add(objCampoValor);
                            }   
                        }

                        if (vObjFormularioEspecifico.UNIDAD_MEDIDA != null)
                        {
                            vUnidadMedida = vObjFormularioEspecifico.UNIDAD_MEDIDA.Trim().Split('|');
                            E_CAMPO_VALOR objCampoValor = new E_CAMPO_VALOR();
                           vFormularioEspecifico.listUnidadMedida=new List<E_CAMPO_VALOR>();
                            foreach (var item1 in vUnidadMedida)
                            {
                                vUnidadMedida = item1.Trim().Split(',');
                                objCampoValor = new E_CAMPO_VALOR();

                                objCampoValor.intValor = Convert.ToDecimal(vUnidadMedida[0]);
                                objCampoValor.strCampo = vUnidadMedida[1];
                                vFormularioEspecifico.listUnidadMedida.Add(objCampoValor);
                            }
                        }

                       if (vObjFormularioEspecifico.RANGOS_MULTIPLES != null)
                        {
                            vRangoMultiple = vObjFormularioEspecifico.RANGOS_MULTIPLES.Trim().Split('|');
                            E_CAMPO_VALOR objCampoValor = new E_CAMPO_VALOR();
                            vFormularioEspecifico.listRangosMultiples=new List<E_CAMPO_VALOR>();
                            foreach (var item1 in vRangoMultiple)
                            {
                                vRangoMultiple = item1.Trim().Split(',');
                                objCampoValor = new E_CAMPO_VALOR();

                                objCampoValor.intValor = Convert.ToDecimal(vRangoMultiple[0]);
                                objCampoValor.strCampo = vRangoMultiple[1];
                                vFormularioEspecifico.listRangosMultiples.Add(objCampoValor);
                            }
                        }

                        if (vObjFormularioEspecifico.ESPEC_MINIMA_RANGO != null)
                        {
                            vEspecMinimaRango = vObjFormularioEspecifico.ESPEC_MINIMA_RANGO.Trim().Split('|');
                            E_CAMPO_VALOR objCampoValor = new E_CAMPO_VALOR();
                            vFormularioEspecifico.listEspecMinimaRango=new List<E_CAMPO_VALOR>();
                            foreach (var item1 in vEspecMinimaRango)
                            {
                                vEspecMinimaRango = item1.Trim().Split(',');
                                objCampoValor = new E_CAMPO_VALOR();

                                objCampoValor.intValor = Convert.ToDecimal(vEspecMinimaRango[0]);
                                objCampoValor.strCampo = vEspecMinimaRango[1];
                                vFormularioEspecifico.listEspecMinimaRango.Add(objCampoValor);
                            }
                        }

                        if (vObjFormularioEspecifico.ESPEC_MAXIMA_RANGO != null)
                        {
                            vEspecMaximaRango = vObjFormularioEspecifico.ESPEC_MAXIMA_RANGO.Trim().Split('|');
                            E_CAMPO_VALOR objCampoValor = new E_CAMPO_VALOR();
                            vFormularioEspecifico.listEspecMaximaRango=new List<E_CAMPO_VALOR>();
                            foreach (var item1 in vEspecMaximaRango)
                            {
                                vEspecMaximaRango = item1.Trim().Split(',');
                                objCampoValor = new E_CAMPO_VALOR();

                                objCampoValor.intValor = Convert.ToDecimal(vEspecMaximaRango[0]);
                                objCampoValor.strCampo = vEspecMaximaRango[1];
                                vFormularioEspecifico.listEspecMaximaRango.Add(objCampoValor);
                            }
                        }
                        vListFormularioEspecifico.Add(vFormularioEspecifico);
                    }

                    //return vColFormularioEspecifico;
                    return vListFormularioEspecifico;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdTablaEspecificacion };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Listado general de unidades de medida.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Lista de objetos O_UNIDADES_MEDIDA_GRAL_CTY.</returns>
        public List<O_UNIDADES_MEDIDA_GRAL_CTY> ListarUnidadesMedidaGeneral(string strCredencial, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_UNIDADES_MEDIDA_GRAL_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_UNIDADES_MEDIDA_GRAL().ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista la datos de TPAR_PRODUCTOS.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Lista de objetos O_PRODUCTOS_CTY.</returns>
        public List<O_PRODUCTOS_CTY> ListarProductos(string strCredencial, string strCodigo, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_PRODUCTOS_CTY> objListado=null;
                    if (strCodigo!=null && strCodigo.Trim() == "0")
                        objListado = ctx.PUSR_LISTADOS_P_LISTADO_PRODUCTOS("").ToList();
                    else
                        objListado = ctx.PUSR_LISTADOS_P_LISTADO_PRODUCTOS(strCodigo).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, strCodigo };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista la datos de TPAC_CAMPOS.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="decIdCamposEntidad">Identificador de la tabla.</param>
        /// <param name="decIdCampo">Identificador del campo.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Lista de objetos O_LISTA_CAMPO_ENTIDAD_CTY.</returns>
        public List<O_LISTA_CAMPO_ENTIDAD_CTY> ListarCamposPorEntidad(string strCredencial, decimal decIdCamposEntidad, decimal decIdCampo,
                                                                      decimal decIdEntidad, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_CAMPO_ENTIDAD_CTY> objListado =
                        ctx.PCAN_LISTADOS_P_LISTADO_CAMPOS_POR_ENTIDAD(strCredencial, decIdCamposEntidad, decIdCampo,
                                                                                  decIdEntidad).ToList();
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
        /// Listar los registro del prode.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="decIdCampo">Identificador del campo.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdTipoReporte">Identificador del tipo de reporte.</param>
        /// <param name="decIdProducto">Identificador del producto.</param>
        /// <param name="decFechaProde">Fecha del prode (para filtrar el prode de una fecha especifica).</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Lista de Objetos O_LISTA_PRODE_CTY.</returns>
        public List<O_LISTA_PRODE_CTY> ListarProde(string strCredencial, decimal decIdCampo, decimal decIdEntidad, decimal decIdTipoReporte, decimal decIdProducto, decimal decFechaProde, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_PRODE_CTY> objListado =
                        ctx.PCAN_LISTADOS_P_LISTADO_PRODE(strCredencial, decIdCampo,
                                                          decIdEntidad, decIdTipoReporte, decIdProducto, decFechaProde).ToList();
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
        /// Listar los tipos de actividad de las entidades.
        /// </summary>
        /// <param name="strCredencial">credencial de acceso al módulo.</param>
        /// <param name="strDescripcion">descripcion del tipo de actividad.</param>
        /// <param name="decIdCategoriaActividad">Identificador de la categoria de la actividad.</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de actividad.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Listados de objetos O_TIPOS_ACTIVIDAD_CTY.</returns>
        public List<O_TIPOS_ACTIVIDAD_CTY> ListarTiposActividad(string strCredencial, string strDescripcion, decimal decIdCategoriaActividad, decimal decIdTipoActividad, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_TIPOS_ACTIVIDAD_CTY> objListado =
                        ctx.PCAN_LISTADOS_P_LISTADO_TIPO_ACTIVIDAD(strCredencial, strDescripcion,
                                                                   decIdCategoriaActividad, decIdTipoActividad).
                            ToList();
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
        /// Lista la datos de TPAR_VOL_TIPOS_OPERACION.
        /// </summary>
        /// <param name="strCredencial">credencial de acceso al sistema.</param>
        /// <param name="strMensajeError">mensaje de error.</param>
        /// <returns>lista de objetos O_VOL_TIPOS_OPERACION_CTY.</returns>
        public List<O_ESTADO_TIPO_PRUEBA_CTY> ListarVolumenTipoOperacion(string strCredencial, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_ESTADO_TIPO_PRUEBA_CTY> objListado = ctx.PUSR_LISTADOS_P_LISTADO_TIPOS_OPERACION_TE(decIdUsuario).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que verifica las validaciones de calidad.
        /// </summary>
        /// <param name="strCredencial">credencial de acceso al sistema.</param>
        /// <param name="decIdTipoActividad">Código del tipo de actividad.</param>
        /// <param name="decIdUsuario">Identificador de usuario.</param>
        /// <param name="strMensajeError">mensaje de error.</param>
        /// <returns>Lista de objetos O_VALIDA_CALIDAD_CTY.</returns>
        public List<O_VALIDA_CALIDAD_CTY> ObtenerValidacionCalidad(string strCredencial, decimal decIdTipoActividad, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_VALIDA_CALIDAD_CTY> objListado = ctx.PUSR_LISTADOS_P_LISTADO_VALIDA_CALIDAD(strCredencial, decIdTipoActividad, decIdUsuario).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista los certificados de calidad observados.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="decIdUsuario">Código de usuario.</param>
        /// <param name="strFechaInicial">Fecha inicial del reporte.</param>
        /// <param name="strFechaFinal">Fecha final del reporte.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_CERTIFICADOS_ALERTAS_CTY.</returns>
        public List<O_CERTIFICADOS_ALERTAS_CTY> ListarCertificadosConAlertas(string strCredencial, decimal decIdUsuario, string strFechaInicial, string strFechaFinal, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    strFechaInicial = strFechaInicial.Replace("_","/");
                    strFechaFinal = strFechaFinal.Replace("_", "/");
                    List<O_CERTIFICADOS_ALERTAS_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_CERTIFICADOS_ALERTA(decIdUsuario, strFechaInicial, strFechaFinal).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, decIdUsuario };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }    
        }

        /// <summary>
        /// Lista las pruebas del certificado de calidad con alertas.
        /// </summary>
        /// <param name="decIdRegistroCalPrincipal">Identificador del registro de calidad principal.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_CERTIFICADO_ALERTAS_CTY.</returns>
        public List<O_CERTIFICADO_ALERTAS_CTY> ListarCertificadoConAlertas(decimal decIdRegistroCalPrincipal, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_CERTIFICADO_ALERTAS_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_CERTIFICADO_ALERTA(decIdRegistroCalPrincipal).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Listados: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { decIdRegistroCalPrincipal };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lista de cátalogos para el servicio web Externo.
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_CATALOGO_CALIDAD_CTY.</returns>
        public List<E_CATALOGO_CALIDAD> ListarCatalogosCalidad(string strLlave, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    E_CATALOGO_CALIDAD vObjCatalogoCalidad = new E_CATALOGO_CALIDAD();
                    List<E_CATALOGO_CALIDAD> vColCatalogoCalidad = new List<E_CATALOGO_CALIDAD>();                   
                    List<O_CATALOGO_CALIDAD_CTY> objListado = ctx.PUSR_LISTADOS_P_LISTADO_CATALOGO_CALIDAD(strLlave).ToList();
                    vObjCatalogoCalidad.vColCampoValor = new List<CAMPO_VALOR>();
                    #region Cargado de los listados
                    for (int i = 0; i < objListado.Count; i++)
                    {
                        vObjCatalogoCalidad.TIPO = objListado[i].TIPO;
                        CAMPO_VALOR vObjCampoValor = new CAMPO_VALOR();
                        vObjCampoValor.CODIGO = objListado[i].CODIGO;
                        if (vObjCatalogoCalidad.TIPO =="TANQUES")
                            vObjCampoValor.DESCRIPCION = objListado[i].DESCRIPCION + objListado[i].DESCRIPCION;
                        else
                            vObjCampoValor.DESCRIPCION = objListado[i].DESCRIPCION;
                        vObjCatalogoCalidad.vColCampoValor.Add(vObjCampoValor);
                        if (i >= objListado.Count - 1 || objListado[i].TIPO != objListado[i + 1].TIPO)
                        {
                            vColCatalogoCalidad.Add(vObjCatalogoCalidad);
                            vObjCatalogoCalidad = new E_CATALOGO_CALIDAD();
                            vObjCatalogoCalidad.vColCampoValor = new List<CAMPO_VALOR>();
                        }
                    }
                    #endregion
                    return vColCatalogoCalidad;
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
        /// Lista de marca productos para Lubricantes.
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTADO_MARCA_LUB_CTY.</returns>
        public List<O_LISTADO_MARCA_LUB_CTY> ListarMarcasLubricantes(string strLlave, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTADO_MARCA_LUB_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_MARCA_LUB(strLlave).ToList();
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
        /// Lista de Tipos de Modena y Cambio
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTADO_MONEDA_CTY</returns>
        public List<O_LISTADO_MONEDA_CTY> ListarMonedas(string strLlave, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTADO_MONEDA_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_MONEDAS(strLlave).ToList();
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
        /// Lista de Tipos de Modena y Cambio
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="strTipo">Tipo de parametrica.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTADO_MONEDA_CTY</returns>
        public List<O_LISTA_PARAMETRICA_CTY> ListarParametricas(string strLlave, string strTipo , ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_PARAMETRICA_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_PARAMETRICA(strLlave, strTipo).ToList();
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
        /// Lista Nombres de producto
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdTablaEspec">Identificador de la tabla especifica.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTA_NOMBRE_PRODUCTO_CTY</returns>
        public List<O_LISTA_NOMBRE_PRODUCTO_CTY> ListarNombreProductos(string strLlave, decimal decIdEntidad, decimal decIdTablaEspec, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_NOMBRE_PRODUCTO_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_NOMBRE_PRODUCTO(strLlave, decIdEntidad, decIdTablaEspec).ToList();
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
        /// Lista Dependientes
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdUsuario">Identificador del usuario.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTA_DEPENDIENTES_CTY</returns>
        public List<O_LISTA_DEPENDIENTES_CTY> ListarDependientes(string strLlave, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_DEPENDIENTES_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_DEPENDIENTES(strLlave, decIdUsuario).ToList();
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
        /// Lista Documentos
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdTipoRespaldo">Identificador del tipo de respaldo.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTA_DOCUMENTOS_CTY</returns>
        public List<O_LISTA_DOCUMENTOS_CTY> ListarDocumentos(string strLlave, string strCite, decimal decIdTipoRespaldo, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_DOCUMENTOS_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_DOCUMENTOS(strLlave, strCite, decIdTipoRespaldo).ToList();
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
        /// Lista Documentos
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdTipoRespaldo">Identificador del tipo de respaldo.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTA_DOCUMENTOS_CTY</returns>
        public List<O_LISTA_DOCUMENTOS_CTY> ListarDocumentosDetalle(string strLlave, decimal decIdDocumento, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_DOCUMENTOS_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_DOC_DETALLE(strLlave, decIdDocumento).ToList();
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
        /// Lista Documentos
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de actividad.</param>
        /// <param name="decIdAmpliacion">Identificador de la ampliacion.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTA_AMPLIACIONES_CTY</returns>
        public List<O_LISTA_AMPLIACIONES_CTY> ListarAmpliaciones(string strLlave, decimal decIdEntidad, decimal decIdTipoActividad, decimal decIdAmpliacion, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_AMPLIACIONES_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_AMPLIACIONES(strLlave, decIdEntidad, decIdTipoActividad, decIdAmpliacion).ToList();
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
        /// Lista Documentos
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de actividad.</param>
        /// <param name="decIdUsuario">Identificador del usuario.</param>
        /// <param name="decIdTipo">Identificador del tipo de consulta.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTA_ARBOL_ENTIDADES_CTY</returns>
        public List<O_LISTA_ARBOL_ENTIDADES_CTY> ListarArbolEntidades(string strLlave, decimal decIdTipoActividad, decimal decIdEntidad, decimal decIdUsuario, decimal decIdTipo, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_ARBOL_ENTIDADES_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_ARBOL_ENTIDADES(strLlave, decIdEntidad, decIdTipoActividad, decIdUsuario, decIdTipo).ToList();
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
        /// Lista propietarios de los cites de certificados de calidad
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decFechaInicial">Fecha inicial para filtro.</param>
        /// <param name="decFechaFinal">Fecha final para filtro.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de actividad.</param>
        /// <param name="decIdUsuario">Identificador del usuario.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTA_PROPIETARIOS_CTY</returns>
        public List<O_LISTA_PROPIETARIOS_CTY> ListarPropietarios(string strLlave, decimal decFechaInicial, decimal decFechaFinal, decimal decIdEntidad, decimal decIdTipoActividad, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_PROPIETARIOS_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_PROPIETARIOS(strLlave, decFechaInicial, decFechaFinal, decIdEntidad, decIdTipoActividad, decIdUsuario).ToList();
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
        /// Lista entidades y actividad de propietarios de certificados de calidad
        /// </summary>
        /// <param name="strLlave">Credencial de acceso al sistema.</param>
        /// <param name="decIdUsuario">Identificador del usuario.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>lista de objetos O_LISTA_ENTIDAD_PROP_CTY</returns>
        public List<O_LISTA_ENTIDAD_PROP_CTY> ListarEntidadesPropietarios(string strLlave, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_LISTA_ENTIDAD_PROP_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_ENTIDAD_PARA_PROP(strLlave, decIdUsuario).ToList();
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

    }
}
