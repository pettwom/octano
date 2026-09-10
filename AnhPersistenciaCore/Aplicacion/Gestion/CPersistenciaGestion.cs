using AnhPersistenciaCore.Aplicacion.Listados;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using AnhPresentacionDTEP.Parametros;
using Librerias.Anh.Us;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnhPersistenciaCore.Aplicacion.Gestion
{
    public class CPersistenciaGestion
    {

        #region Constructor

        public CPersistenciaGestion()
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

        #region variables

        private readonly CAuditoria.Logging _objDirectorio;
        private readonly string _directorioLog = ConfigurationManager.AppSettings["Directorio"];
        private const string TipoMensajeError = "Repos.";

        #endregion

        #region campos

        /// <summary> 
        /// Registra un campo en la tabla TPAC_CAMPOS.
        /// </summary>
        /// <param name="strCredencial">Credencial del sistema.</param>
        /// <param name="decIdCampo">Identificador del campo, 0 si es un nuevo registro.</param>
        /// <param name="strNombreCampo">Nombre del campo.</param>
        /// <param name="decAppFechaRegistro">Fecha de regsitro.</param>
        /// <param name="decAppIdUsuario">Id del usuario del aplicativo.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> RegistrarCampos(string strCredencial, decimal decIdCampo, string strNombreCampo,
                                                     decimal decAppFechaRegistro, decimal decAppIdUsuario,
                                                     ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    List<O_RESULTADO_CTY> listadoResultado = ctx.PCAN_GESTION_P_REGISTRA_CAMPO(strCredencial,
                                                                                               decIdCampo,
                                                                                               strNombreCampo,
                                                                                               decAppIdUsuario,
                                                                                               decAppFechaRegistro)
                        .ToList();
                    if (listadoResultado.Capacity > 0)
                    {
                        resultado = listadoResultado;
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        /// <summary>
        /// Registra la relacion entre entidades y campos.
        /// </summary>
        /// <param name="strCredencial">Credencial del sistema.</param>
        /// <param name="decIdCamposEntidad">Identificador correlativo de la table.</param>
        /// <param name="decIdCampo">Identificador del campo.</param>
        /// <param name="decIdEntidad">Identificador de la Entidad.</param>
        /// <param name="decAppFechaRegistro">Fecha de regsitro.</param>
        /// <param name="decAppIdUsuario">Id del usuario del aplicativo.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> RegistrarCamposEntidad(string strCredencial, decimal decIdCamposEntidad,
                                                            decimal decIdCampo,
                                                            decimal decIdEntidad, decimal decAppFechaRegistro,
                                                            decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    List<O_RESULTADO_CTY> listadoResultado =
                        ctx.PCAN_GESTION_P_REGISTRA_CAMPOS_ENTIDAD(strCredencial,
                                                                   decIdCamposEntidad,
                                                                   decIdCampo,
                                                                   decIdEntidad,
                                                                   decAppIdUsuario,
                                                                   decAppFechaRegistro)
                            .ToList();
                    if (listadoResultado.Capacity > 0)
                    {
                        resultado = listadoResultado;
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        /// <summary>
        /// Registra la relacion entre Cantidades y campos.
        /// </summary>
        /// <param name="strCredencial">Credencial del sistema.</param>
        /// <param name="decIdCampo">Identificador del campo.</param>
        /// <param name="decIdCantidad">Identificador de la cantidad registrada.</param>
        /// <param name="decFechaOperacion">Fecha a la cual pertenece el reporte.</param>
        /// <param name="strObservaciones">Observaciones enviadas por el operador o usuario del sistema.</param>
        /// <param name="strJustificacion">Justificacion del por que se hizo un cambio a un registro.</param>
        /// <param name="decIdTipoMedioTransporte">Identificador del tipo de medio de transporete.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro.</param>
        /// <param name="decAppIdUsuario">Id del usuario del aplicativo.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> RegistrarCantidadesCampo(string strCredencial, decimal decIdCampo,
                                                              decimal decIdCantidad, decimal decFechaOperacion,
                                                              string strObservaciones, string strJustificacion,
                                                              decimal decIdTipoMedioTransporte,
                                                              decimal decAppFechaRegistro,
                                                              decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    List<O_RESULTADO_CTY> listadoResultado =
                        ctx.PCAN_GESTION_P_REGISTRA_CANTIDADES_CAMPO(strCredencial,
                                                                     decIdCampo,
                                                                     decIdCantidad,
                                                                     decFechaOperacion,
                                                                     strObservaciones,
                                                                     strJustificacion,
                                                                     decIdTipoMedioTransporte,
                                                                     decAppIdUsuario,
                                                                     decAppFechaRegistro)
                            .ToList();
                    if (listadoResultado.Capacity > 0)
                    {
                        resultado = listadoResultado;
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        /// <summary>
        /// Registra el prode por entidad y/o por campo (de explotación).
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="decIdProde">Identificador del prode (proyeccion de demanda).</param>
        /// <param name="decFechaProde">Fecha en la cual se aplicara el prode, la fecha se aplica de acuerdo al tipo de reporte.</param>
        /// <param name="decValorProde">Valor de la proyeccion de demanda.</param>
        /// <param name="decIdEntidad">Identificador de la entidad (planta).</param>
        /// <param name="decIdCampo">Identificador del campo.</param>
        /// <param name="decIdUnidadMedida">Identificador de la unidad de medida en la cual se registro el prode.</param>
        /// <param name="decIdTipoReporte">Identificador del tipo de reporte (díario,mensual,anual).</param>
        /// <param name="decIdProducto">Identificador del producto.</param>
        /// <param name="decAppIdUsuario">Identificador del usuario que este registrando el prode.</param>
        /// <param name="decAppFechaRegistro">Fecha en la que el usuario esta registrando el prode.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> RegistrarProde(string strCredencial, decimal decIdProde, decimal decFechaProde,
                                                    decimal decValorProde, decimal decIdEntidad, decimal decIdCampo,
                                                    decimal decIdUnidadMedida, decimal decIdTipoReporte,
                                                    decimal decIdProducto, decimal decAppIdUsuario,
                                                    decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    List<O_RESULTADO_CTY> listadoResultado =
                        ctx.PCAN_GESTION_P_REGISTRA_CAMP_ENT_PRODE(strCredencial, decIdProde, decFechaProde,
                                                                   decValorProde, decIdEntidad, decIdCampo,
                                                                   decIdUnidadMedida, decIdTipoReporte, decIdProducto,
                                                                   decAppIdUsuario,
                                                                   decAppFechaRegistro)
                            .ToList();
                    if (listadoResultado.Capacity > 0)
                    {
                        resultado = listadoResultado;
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        /// <summary>
        /// Elimna un campo en la tabla TPAC_CAMPOS.
        /// </summary>
        /// <param name="strCredencial">Credencial del sistema.</param>
        /// <param name="decIdCampo">Identificador del campo.</param>
        /// <param name="decAppIdUsuario">Id del usuario del aplicativo.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> EliminarCampos(string strCredencial, decimal decIdCampo, decimal decAppIdUsuario,
                                                    decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    List<O_RESULTADO_CTY> listadoResultado = ctx.PCAN_GESTION_P_ELIMINA_CAMPO(strCredencial,
                                                                                              decIdCampo,
                                                                                              decAppIdUsuario,
                                                                                              decAppFechaRegistro)
                        .ToList();
                    if (listadoResultado.Capacity > 0)
                    {
                        resultado = listadoResultado;
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        /// <summary>
        /// Elimna un campo en la tabla TPAC_CAMPOS_ENTIDAD.
        /// </summary>
        /// <param name="strCredencial">Credencial del sistema.</param>
        /// <param name="decIdCamposEntidad">Identificador correlativo de la tabla.</param>
        /// <param name="decIdCampo">Identificador del campo.</param>
        /// <param name="decIdEntidad">Identificador de la entidad.</param>
        /// <param name="decAppIdUsuario">Id del usuario del aplicativo.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> EliminarCampoEntidad(string strCredencial, decimal decIdCamposEntidad,
                                                          decimal decIdCampo, decimal decIdEntidad,
                                                          decimal decAppIdUsuario, decimal decAppFechaRegistro,
                                                          ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    List<O_RESULTADO_CTY> listadoResultado = ctx.PCAN_GESTION_P_ELIMINA_CAMPO_ENTIDAD(strCredencial,
                                                                                                      decIdCamposEntidad,
                                                                                                      decIdCampo,
                                                                                                      decIdEntidad,
                                                                                                      decAppIdUsuario,
                                                                                                      decAppFechaRegistro)
                        .ToList();
                    if (listadoResultado.Capacity > 0)
                    {
                        resultado = listadoResultado;
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        /// <summary>
        /// Elimna un campo en la tabla TPAC_CAMPO_ENTIDAD_PRODE.
        /// </summary>
        /// <param name="strCredencial">Credencial del sistema.</param>
        /// <param name="decIdProde">Identificador de la proyeccion de la demanda PRODE.</param>
        /// <param name="decAppIdUsuario">Id del usuario del aplicativo.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> EliminarProde(string strCredencial, decimal decIdProde, decimal decAppIdUsuario,
                                                   decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    List<O_RESULTADO_CTY> listadoResultado = ctx.PCAN_GESTION_P_ELIMINA_CAMP_ENT_PRODE(strCredencial,
                                                                                                       decIdProde,
                                                                                                       decAppIdUsuario,
                                                                                                       decAppFechaRegistro)
                        .ToList();
                    if (listadoResultado.Capacity > 0)
                    {
                        resultado = listadoResultado;
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        #endregion                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    

        #region funciones comunes

        /// <summary>
        /// Obtener la diferencia de 2 fechas en días.
        /// </summary>
        /// <param name="fecha1">fecha inicial en formato DD/MM/YYYY.</param>
        /// <param name="fecha2">fecha final en formato DD/MM/YYYY.</param>
        /// <returns>diferencia de fechas en días.</returns>
        public static int RestarFechas(string fecha1, string fecha2)
        {
            int diferencia;
            try
            {
                if (fecha1 != null && fecha2 != null &&
                    fecha1.Trim() != "" && fecha2.Trim() != "")
                {
                    String[] fechaIni1 = fecha1.Split('/');
                    String[] fechaFin1 = fecha2.Split('/');
                    var fechaIni = new DateTime(Convert.ToInt32(fechaIni1[2]), Convert.ToInt32(fechaIni1[1]),
                                                Convert.ToInt32(fechaIni1[0]));
                    var fechaFin = new DateTime(Convert.ToInt32(fechaFin1[2]), Convert.ToInt32(fechaFin1[1]),
                                                Convert.ToInt32(fechaFin1[0]));
                    TimeSpan ts = fechaIni - fechaFin;
                    diferencia = ts.Days;
                }
                else if (fecha1 != null && fecha1.Trim() == "")
                {
                    throw new Exception("La fecha inicial no puede ser nula");
                }
                else
                {
                    diferencia = -1;
                }
            }
            catch (Exception)
            {
                diferencia = 0;
            }
            return diferencia;
            //return 1;
        }

        /// <summary>
        /// Obtener el Id de la primera unidad de medida encontrada para una prueba de calidad.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al módulo.</param>
        /// <param name="decIdPruebaCalidad">Identificador de la prueba de calidad.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>id de la primera unidad de medida encontrada para la prueba de calidad.</returns>
        public decimal ObtenerIdUnidadMedidaPruebaCalidad(string strCredencial, decimal decIdPruebaCalidad,
                                                          ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_UNIDADES_MEDIDA_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_UNIDADES_PRU_CA(decIdPruebaCalidad).ToList();
                    return objListado[0].ID_UNIDAD_MEDIDA;
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                              ex,
                                                              TipoMensajeError);
                    return 0;
                }
            }
        }
        /* 
        /// <summary>
        /// Obtener el id del primer método ASTM de una prueba de calidad
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al módulo.</param>
        /// <param name="decIdPruebaCalidad">Identificador de la prueba de calidad</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns></returns>
        public decimal ObtenerMetodoAstmPruebaCalidad(string strCredencial, decimal decIdPruebaCalidad,
                                                      ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_METODOS_ASTM_CTY> objListado =
                        ctx.PUSR_LISTADOS_P_LISTADO_METODOS_ASTM_PRU_CA(decIdPruebaCalidad).ToList();
                    return objListado[0].ID_METODO_ASTM;
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                              ex,
                                                              TipoMensajeError);
                    return 0;
                }
            }
        }
        */

        /// <summary>
        /// Obtener el Id de una prueba de unidad.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al módulo.</param>
        /// <param name="decIdTablaEspec">Identificador de la tabla de especificación.</param>
        /// <param name="strCodigo">codigo de la prueba de calidad.</param>
        /// <param name="strNombre">nombre de la prueba de calidad.</param>
        /// <param name="strDescripcion">descripcion de la prueba de calidad.</param>
        /// <param name="decIdPruebaCalidad">Identificador de la prueba de calidad.</param>
        /// <param name="decIdPruebaUnidad">Identificador de la prueba de unidad.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>id de la primera prueba de unidad encontrada para una prueba de calidad.</returns>
        public void ObtenerIdPruebaCalidad(string strCredencial, decimal decIdTablaEspec, string strCodigo,
                                           string strNombre, string strDescripcion, out decimal decIdPruebaCalidad,
                                           out decimal decIdPruebaUnidad, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_PRUEBA_UNIDAD_CALIDAD_CTY> objListado =
                        ctx.PCAN_LISTADOS_P_LISTADO_PRUEBA_UNIDAD(strCredencial, decIdTablaEspec, strCodigo,
                                                                  strNombre, strDescripcion).ToList();
                    decIdPruebaCalidad = objListado[0].ID_PRUEBA_CALIDAD;
                    decIdPruebaUnidad = objListado[0].ID_PRUEBA_UNIDAD;
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                              ex,
                                                              TipoMensajeError);
                    decIdPruebaCalidad = decIdPruebaUnidad = 0;
                }
            }
        }

        #endregion

        #region Certificados de calidad

        /// <summary>
        /// Metodo que realiza la inserción de la prueba de calidad para carburantes (Servicio Externo).
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decVersion">Versión del proyecto.</param>
        /// <param name="decCodigoProyecto">Código del proyecto.</param>
        /// <param name="strDatosReporte">Cadena en formato Json.</param>
        /// <param name="decIdOperador">Identificador del operador.</param>
        /// <param name="decEstadoRegistro">Estado del registro ABM.</param>
        /// <param name="vObjetoLote">Objeto de la información para el almacenamiento de carburantes.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_REF_REG_REPORTE_PLANO_CTY con el codigo de error.</returns>
        public List<O_REF_REG_REPORTE_PLANO_CTY> InsertarPruebaCalidad(string strLlave, decimal decVersion, decimal decCodigoProyecto, string strDatosReporte, decimal decIdOperador, decimal decEstadoRegistro, ObjetoLote vObjetoLote, ref string strMensajeError)
        {
            List<O_REF_REG_REPORTE_PLANO_CTY> resultado = null;
            List<O_REG_IN_REPORTE_PLANO_CTY> listadoResultado = null;
            using (var contexto = new EntidadesOctano())
            {                
                string vObjJasonEncabezado = null;
                decimal? decAppFechaRegistro = null;
                try
                {                    
                    vObjJasonEncabezado = JsonConvert.SerializeObject(vObjetoLote);
                    //listadoResultado = contexto.PUSR_REFINERIAS_P_REG_REPORTE_CARBURANTES_ID(strLlave, vObjJasonEncabezado, strDatosReporte, decIdOperador, decEstadoRegistro, Convert.ToDecimal(DateTime.Now.ToString(cParametrosHydro.strFormatoFechaServ))).ToList();
                    listadoResultado = contexto.PUSR_REFINERIAS_P_REG_REPORTE_CARBURANTES_ID(strLlave, vObjJasonEncabezado, strDatosReporte, decIdOperador, decEstadoRegistro, Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss"))).ToList();
                    if (listadoResultado[0].CANTIDAD_ALERTAS > 0)
                    { 
                        decAppFechaRegistro = CFechas.ConvierteDateTimeLong(System.DateTime.Now);
                        try
                        {
                            string strMensajeCorreo = "<html><head></head><body>" +
                             "<span style='font-size:18px; font-family:'Century Gothic'; text-align:justify;'>" +
                             "<table cellpadding='5' style='border:#3F5C30 solid thin; width:650px; background-color: #006600;'>" +
                             "<tbody><tr><td style='text-align:center;'>" +
                             "<span style='font-size:18px; text-align:center; color:#FFFFFF;'>" +
                             "AGENCIA NACIONAL DE HIDROCARBUROS" +
                             "</span></td></tr></tbody></table>" +
                             "<table cellpadding='15' style='border: #3F5C30 solid thin; background-color: #faFfff; width:650px;'>" +
                             "<tbody><tr><td>Sr(a) usuario del sistema OCTANO - Módulo: Control de Calidad, el certificado con el siguiente Nro. Cite:<span>" +
                             "<table cellpadding='8' style='border: #3F5C30 solid thin; background-color:#FaFaFa;'>" +
                             "<tbody><tr><td style='font-size:14px;font-weight:bold;'><br>" +
                             "<span style='color:#060;'>NRO. CITE:</span>" + " " + listadoResultado[0].CORRELATIVO_REGISTRO +
                             "<br>" +
                                //"<span style='color:#060;'>CONTRASEÑA:</span> " +
                                //Session[CVariablesSesion.UsuarioPassword].ToString() + "</td>" +
                             "</tr></tbody></table><br><br>" +
                             "Se encuentra observado." +
                             "<br><br>Si usted tiene consultas puede  escribirnos a la siguiente dirección de correo: " +
                             "sistemas@anh.gob.bo o comunicarse con el telefono (591-2) 214000 (DTIC).</span>" +
                             "<br><br><span style='font-size:12px;'>Atte.: Administrador de Sistemas HYDRO 2016<br>" +
                             "Fecha: " + DateTime.Now + "</span>" +
                             "</td></tr></tbody></table></span></body></html>";
                            CCorreo.mEnviarEmail(
                            CParametrosHydro.StrServidorDireccion,
                            CParametrosHydro.IntServidorPuerto,
                            CParametrosHydro.StrUsuarioLogin,
                            CParametrosHydro.StrUsuarioPassword,
                            CParametrosHydro.StrUsuarioDe,
                            listadoResultado[0].ALERTA_EMAIL,
                            CParametrosHydro.StrUsuarioCc,
                            CParametrosHydro.StrUsuarioCco,
                            "Reporte Control Calidad HYDRO_OCTANO_CALIDAD NRO: " + listadoResultado[0].CORRELATIVO_REGISTRO,
                            strMensajeCorreo,
                            null,
                            true,
                            CParametrosHydro.bolHabilitarSsl,
                            CParametrosHydro.bolNotificarError);
                        }
                        catch (Exception ex)
                        {
                            strMensajeError = "Repos. RegistrarReporteCarburantes Error: Error: Servicio de envio de correos no disponible - " + ex.Message + " - " + ex.InnerException;

                            var parametrosMetodo = new { strLlave, vObjetoLote, strDatosReporte, decIdOperador, decEstadoRegistro, decAppFechaRegistro };
                            CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                                      MethodBase.GetCurrentMethod().Name,
                                                      ex.Message + " - " + ex.InnerException, parametrosMetodo);
                        }          
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message +
                                      " - " + ex.InnerException + " - " + ex.StackTrace;
                    var parametrosMetodo = new { strLlave, vObjetoLote, strDatosReporte, decIdOperador, decEstadoRegistro, decAppFechaRegistro };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                }
                if (listadoResultado.Capacity > 0)
                {
                    resultado = new List<O_REF_REG_REPORTE_PLANO_CTY>();
                    resultado.Add(new O_REF_REG_REPORTE_PLANO_CTY
                    {
                        CORRELATIVO_REGISTRO = listadoResultado[0].CORRELATIVO_REGISTRO,
                        MENSAJE_ERROR = listadoResultado[0].MENSAJE_ERROR,
                        RESULTADO = listadoResultado[0].RESULTADO
                    });
                }
                return resultado;
            }
        }

        /// <summary>
        /// Metodo que realiza la inserción de la prueba de calidad para carburantes Y lubricantes (Servicio Externo 2016).
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decVersion">Versión del proyecto.</param>
        /// <param name="strDatosReporte">Cadena en formato Json.</param>
        /// <param name="decIdOperador">Identificador del operador.</param>
        /// <param name="decEstadoRegistro">Estado del registro ABM.</param>
        /// <param name="vObjetoLote">Objeto de la información para el almacenamiento de carburantes.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_REG_IN_REPORTE_PLANO_CTY con el codigo de error.</returns>
        public List<O_REF_REG_REPORTE_PLANO_CTY> RegistroPruebaCalidadExt(string strLlave, decimal decVersion, string strDatosReporte, decimal decIdOperador, decimal decEstadoRegistro, ObjetoLote vObjetoLote, ref string strMensajeError)
        {
            List<O_REF_REG_REPORTE_PLANO_CTY> resultado = null;
            List<O_REG_IN_REPORTE_PLANO_CTY> listadoResultado = null;
            using (var contexto = new EntidadesOctano())
            {
                decimal? decAppFechaRegistro = null;
                try
                {
                    string vObjJsonEncabezado = JsonConvert.SerializeObject(vObjetoLote);
                    listadoResultado = contexto.PUSR_REFINERIAS_P_REGISTRO_CERTIFICADOS_WS(strLlave, vObjJsonEncabezado, strDatosReporte, decIdOperador, decEstadoRegistro, Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss"))).ToList();
                    if (listadoResultado[0].CANTIDAD_ALERTAS > 0)
                    {
                        decAppFechaRegistro = CFechas.ConvierteDateTimeLong(System.DateTime.Now);
                        try
                        {
                            string strMensajeCorreo = "<html><head></head><body>" +
                             "<span style='font-size:18px; font-family:'Century Gothic'; text-align:justify;'>" +
                             "<table cellpadding='5' style='border:#3F5C30 solid thin; width:650px; background-color: #006600;'>" +
                             "<tbody><tr><td style='text-align:center;'>" +
                             "<span style='font-size:18px; text-align:center; color:#FFFFFF;'>" +
                             "AGENCIA NACIONAL DE HIDROCARBUROS" +
                             "</span></td></tr></tbody></table>" +
                             "<table cellpadding='15' style='border: #3F5C30 solid thin; background-color: #faFfff; width:650px;'>" +
                             "<tbody><tr><td>Sr(a) usuario del sistema OCTANO - Módulo: Control de Calidad, el certificado con el siguiente Nro. Cite:<span>" +
                             "<table cellpadding='8' style='border: #3F5C30 solid thin; background-color:#FaFaFa;'>" +
                             "<tbody><tr><td style='font-size:14px;font-weight:bold;'><br>" +
                             "<span style='color:#060;'>NRO. CITE:</span>" + " " + listadoResultado[0].CORRELATIVO_REGISTRO +
                             "<br>" +
                                //"<span style='color:#060;'>CONTRASEÑA:</span> " +
                                //Session[CVariablesSesion.UsuarioPassword].ToString() + "</td>" +
                             "</tr></tbody></table><br><br>" +
                             "Se encuentra observado." +
                             "<br><br>Si usted tiene consultas puede  escribirnos a la siguiente dirección de correo: " +
                             "sistemas@anh.gob.bo o comunicarse con el telefono (591)-2-2614000 (DTIC).</span>" +
                             "<br><br><span style='font-size:12px;'>Atte.: Administrador de Sistemas HYDRO 2016<br>" +
                             "Fecha: " + DateTime.Now + "</span>" +
                             "</td></tr></tbody></table></span></body></html>";
                            CCorreo.mEnviarEmail(
                            CParametrosHydro.StrServidorDireccion,
                            CParametrosHydro.IntServidorPuerto,
                            CParametrosHydro.StrUsuarioLogin,
                            CParametrosHydro.StrUsuarioPassword,
                            CParametrosHydro.StrUsuarioDe,
                            listadoResultado[0].ALERTA_EMAIL,
                            CParametrosHydro.StrUsuarioCc,
                            CParametrosHydro.StrUsuarioCco,
                            "Reporte Control Calidad OCTANO_CALIDAD NRO: " + listadoResultado[0].CORRELATIVO_REGISTRO,
                            strMensajeCorreo,
                            null,
                            true,
                            CParametrosHydro.bolHabilitarSsl,
                            CParametrosHydro.bolNotificarError);
                        }
                        catch (Exception ex)
                        {
                            strMensajeError = "Repos. RegistrarReporteCarburantes Error: Error: Servicio de envio de correos no disponible - " + ex.Message + " - " + ex.InnerException;

                            var parametrosMetodo = new { strLlave, vObjetoLote, strDatosReporte, decIdOperador, decEstadoRegistro, decAppFechaRegistro };
                            CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                                      MethodBase.GetCurrentMethod().Name,
                                                      ex.Message + " - " + ex.InnerException, parametrosMetodo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message +
                                      " - " + ex.InnerException + " - " + ex.StackTrace;
                    var parametrosMetodo = new { strLlave, vObjetoLote, strDatosReporte, decIdOperador, decEstadoRegistro, decAppFechaRegistro };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                }
                if (listadoResultado.Capacity > 0)
                {
                    resultado = new List<O_REF_REG_REPORTE_PLANO_CTY>();
                    resultado.Add(new O_REF_REG_REPORTE_PLANO_CTY
                    {
                        CORRELATIVO_REGISTRO = listadoResultado[0].CORRELATIVO_REGISTRO,
                        MENSAJE_ERROR = listadoResultado[0].MENSAJE_ERROR,
                        RESULTADO = listadoResultado[0].RESULTADO
                    });
                }
                return resultado;
            }
        }
        
        /// <summary>
        /// Método que realiza la inserción del registro de calidad de carburantes y lubricantes (validacion interna).
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="strEncabezado">Cadena en formato Json.</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_REF_REG_REPORTE_PLANO_CTY con el codigo de error.</returns>
        public O_REF_REG_REPORTE_PLANO_CTY RegistraPruebasCalidad(string strLlave, string strEncabezado, decimal decAppIdUsuario, decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_REF_REG_REPORTE_PLANO_CTY objListado =
                        
                        ctx.PUSR_GESTION_P_REGISTRA_CALIDAD_CARB(strLlave, strEncabezado, decAppIdUsuario, decAppFechaRegistro).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que elimina el registro de calidad insertado a través del cite generado.
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="strCiteGenerado">Número de cite generado.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="decAppIdUsuario">Identificadore del usuario desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY EliminaRegistroCalidad(string strLlave, string strCiteGenerado, decimal decAppFechaRegistro, decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =
                        ctx.PUSR_GESTION_P_ELIMINA_CALIDAD(strLlave, strCiteGenerado, decAppFechaRegistro, decAppIdUsuario).SingleOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que realiza la actualización/modificación del registro de calidad de carburantes y lubricantes (validacion interna).
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="strCiteGenerado">Número de cite del registro que se pretende modificar.</param>
        /// <param name="strEncabezado">Cadena en formato Json.</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_REF_REG_REPORTE_PLANO_CTY ActualizaPruebasCalidad(string strLlave, string strCiteGenerado, string strEncabezado, decimal decAppIdUsuario, decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_REF_REG_REPORTE_PLANO_CTY objListado =
                        ctx.PUSR_GESTION_P_ACTUALIZA_CALIDAD_CARB(strLlave, strCiteGenerado, strEncabezado, decAppIdUsuario, decAppFechaRegistro).SingleOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decIdUsuario"></param>
        /// <param name="strMensajeError"></param>
        /// <returns></returns>
        public List<O_RESULTADO_NUMBER_CTY> RetornaIdDireccion(decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                List<O_RESULTADO_NUMBER_CTY> resultado = null;
                try
                {
                     resultado = ctx.PUSR_GESTION_P_DIRECCION_PARA_REPORTE(decIdUsuario).ToList();          
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion:" + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { decIdUsuario };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                }
                return resultado;
            }
        }

        /// <summary>
        /// Método que realiza el registro de alertas en pruebas de calidad de carburantes y lubricantes(validacion interna).
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdTipoAlerta">Identificador de la paramétrica tipo alerta.</param>
        /// /// <param name="decIdPruebaCalidad">Identificador de la prueba de calidad.</param>
        /// <param name="strCiteGenerado">Número de cite del registro que se pretende modificar.</param>
        /// <param name="strObservaciones">Observaciones del tipo de alerta.</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY RegistraAlertaCalidad(string strLlave, decimal decIdTipoAlerta, decimal decIdPruebaCalidad, string strCiteGenerado, string strObservaciones, decimal decAppIdUsuario, decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_REGISTRA_ALERTA(strLlave, decIdTipoAlerta, decIdPruebaCalidad, strCiteGenerado, strObservaciones, decAppIdUsuario, decAppFechaRegistro).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que realiza el registro de documentos en pdf a pruebas de calidad de carburantes y lubricantes.
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdTipRespaldo">Identificador de la paramétrica tipo respaldo.</param>
        /// <param name="strCite">Número de cite del registro al que pertenece el documento.</param>
        /// <param name="byteDocumento">Documento en pdf.</param>
        /// <param name="strObservacion">Observacion que justifique el estado del certificado.</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY RegistraDocumento(string strLlave, decimal decIdTipRespaldo, string strCite, byte[] byteDocumento, string strObservacion, decimal decAppIdUsuario, decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_REGISTRA_DOCUMENTO(strLlave, decIdTipRespaldo, strCite, byteDocumento, strObservacion, decAppIdUsuario, decAppFechaRegistro).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que realiza la actualizacion de documentos en pdf a pruebas de calidad de carburantes y lubricantes.
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdDocumento">Identificador del documento registrado.</param>
        /// <param name="byteDocumento">Documento en pdf.</param>
        /// <param name="strObservacion">Observacfion que justifica el estado del certificado de calidad.</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY ActualizaDocumento(string strLlave, decimal decIdDocumento, byte[] byteDocumento, string strObservacion,  decimal decAppIdUsuario, decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_ACTUALIZA_DOCUMENTO(strLlave, decIdDocumento, byteDocumento, strObservacion, decAppIdUsuario, decAppFechaRegistro).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que realiza la eliminacion de documentos en pdf a pruebas de calidad de carburantes y lubricantes.
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdDocumento">Identificador del registro de documento a eliminar.</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY EliminaDocumento(string strLlave, decimal decIdDocumento, decimal decAppIdUsuario, decimal decAppFechaRegistro, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_ELIMINA_DOCUMENTO(strLlave, decIdDocumento, decAppIdUsuario, decAppFechaRegistro).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        #region Servicio Operadores

        public List<O_REF_MEDIO_TRANSPORTE_CTY> ListarRefMediosTransporte(string strCredencial, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_REF_MEDIO_TRANSPORTE_CTY> objListado =
                        ctx.PUSR_REFINERIAS_P_LISTADO_MEDIO_TRANSPORTE(strCredencial).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        public List<O_REF_CATALOGO_PRODUCTOS_CTY> ListarRefProductos(string strCredencial, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_REF_CATALOGO_PRODUCTOS_CTY> objListado =
                        ctx.PUSR_REFINERIAS_P_LISTADO_PRODUCTOS(strCredencial).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        public List<ASTM_PRUEBA_CALIDAD> ListarRefMetodosASTMPruebaCalidad(string strCredencial, string strCodigoPruebaCalidad, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<ASTM_PRUEBA_CALIDAD> objListado =
                        ctx.PUSR_REFINERIAS_P_LISTADO_METODO_ASTM_CALIDAD(strCredencial, strCodigoPruebaCalidad).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, strCodigoPruebaCalidad };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }
        
        public List<LISTA_TANQUE_ENTIDAD> ListarRefTanques(string strCredencial, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<LISTA_TANQUE_ENTIDAD> objListado =
                        ctx.PUSR_REFINERIAS_P_LISTADO_TANQUES(strCredencial).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }
        
        public List<LISTA_TABLAS_CALIDAD> ListarRefTablasCalidad(string strCredencial, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<LISTA_TABLAS_CALIDAD> objListado =
                        ctx.PUSR_REFINERIAS_P_LISTADO_TABLAS_CALIDAD(strCredencial).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        public List<LISTA_PRUEBAS_CALIDAD> ListarRefPruebaCalidad(string strCredencial, string strCodigoProducto, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<LISTA_PRUEBAS_CALIDAD> objListado =
                        ctx.PUSR_REFINERIAS_P_LISTADO_PRUEBA_CALIDAD(strCredencial, strCodigoProducto).ToList();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;

                    var parametrosMetodo = new { strCredencial, strCodigoProducto };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }
        
        #endregion

        public O_RESULTADO_CTY GestionAmpliacion(string strLlave, decimal decIdAmpliacion, decimal decIdEntidad, decimal decIdTipoActividad, decimal decFechaOperacion, decimal decFechaInicio, decimal decFechaFin, string strBarcode, string strObservaciones, decimal decAppIdUsuario, decimal decAppFechaRegistro, decimal decAccion, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =
                        ctx.PUSR_GESTION_P_GESTION_AMPLIACION(strLlave, decIdAmpliacion, decIdEntidad,decIdTipoActividad,  decFechaOperacion, decFechaInicio, decFechaFin, strBarcode, strObservaciones, decAppIdUsuario, decAppFechaRegistro, decAccion).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que gestiona volumenes en caso de existir importacion
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdPruebaCalidad">Identificador de la prueba de calidad</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de actividad</param>
        /// <param name="decFechaImportacion">Fecha de importacion y operacion</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="decAccion">Accion para realizar la gestion 1=registro/2=modificacion/3=eliminacion</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_REF_REG_REPORTE_PLANO_CTY con el codigo de error.</returns>
        public O_REF_REG_REPORTE_PLANO_CTY GestionVolumen(string strLlave, decimal decIdPruebaCalidad, decimal decIdEntidad, decimal decIdTipoActividad, decimal decFechaImportacion, decimal decAppIdUsuario, decimal decAccion, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_REF_REG_REPORTE_PLANO_CTY objListado =
                        ctx.PUSR_GESTION_P_GESTION_VOLUMEN(strLlave, decIdPruebaCalidad, decIdEntidad, decIdTipoActividad, decFechaImportacion, decAppIdUsuario, decAccion).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que gestiona volumenes en caso de existir importacion
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdPropietarioPrueba">Identificador del propietario de la prueba de calidad</param>
        /// <param name="decIdPruebaCalidad">Identificador de la prueba de calidad</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de actividad</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="decAccion">Accion para realizar la gestion 1=registro/2=modificacion/3=eliminacion</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY GestionPropietario(string strLlave, decimal decIdPropietarioPrueba, decimal decIdPruebaCalidad, decimal decIdEntidad, decimal decIdTipoActividad, decimal decAppIdUsuario, decimal decAccion, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =
                        ctx.PUSR_GESTION_P_GESTION_PROPIETARIO(strLlave, decIdPropietarioPrueba, decIdPruebaCalidad, decIdEntidad, decIdTipoActividad, decAppIdUsuario, decAccion).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que gestiona volumenes en caso de existir importacion
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdGestionOctano">Identificador del registro de Gestion Calidad</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de actividad</param>
        /// <param name="strIpPermiso">Direccion IP del responsable del sistema</param>
        /// <param name="strAplicacion">Texto comentario para identificar del registro por actividad</param>
        /// <param name="decFechaFin">Fecha final para caducidad de los permisos</param>
        /// <param name="strResponsable">Texto comentario del usuario administrador del sistema</param>
        /// <param name="strCorreoAnh">Correo electronico del Supervisor o Administrador del usuario y prueba</param>
        /// <param name="strSiglaOrganigrama">Sigla de la unidad dependiente del operador segun el organigrama</param>
        /// <param name="strObjetoUsuarioPruebas">Objeto que contiene el identificador de usuario operador, usuario anh que supervisa, tabla especifica y tipo de documento</param>
        /// <param name="decAppIdUsuario">Identificador del usuario de aplicación.</param>
        /// <param name="decAccion">Accion para realizar la gestion 1=registro/2=modificacion(para cambiar fecha fin, entidad y tipo actividad)/3=eliminacion</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY GestionUsuario(string strLlave, decimal decIdGestionOctano, decimal decIdEntidad, decimal decIdTipoActividad, string strIpPermiso, string strAplicacion, decimal decFechaFin, string strResponsable, string strCorreoAnh, string strSiglaOrganigrama, string strObjetoUsuarioPruebas, decimal decAppIdUsuario, decimal decAccion, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =
                        ctx.PUSR_GESTION_P_GESTION_USUARIO(strLlave, decIdGestionOctano, decIdEntidad, decIdTipoActividad, strIpPermiso, strAplicacion, decFechaFin,  strResponsable, strCorreoAnh, strSiglaOrganigrama, strObjetoUsuarioPruebas, decAppIdUsuario, decAccion).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }
        #endregion

        #region Parametros
        /// <summary>
        /// Método que registra nuevos parametros
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="strTipo">Tipo de parametrica (MARCA_LUBRICANTES)</param>
        /// <param name="strCodigo">Codigo de parametrica</param>
        /// <param name="strDescripcion">Descripcion de la Parametrica</param>
        /// <param name="strValor">El Valor o nombre de la parametrica</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY RegistraParametro(string strLlave, string strTipo, string strCodigo, string strDescripcion, string strValor, decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_REGISTRA_PARAMETRICA(strLlave, strTipo, strCodigo, strDescripcion, strValor, decAppIdUsuario).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }
       
        /// <summary>
        /// Método que actualiza parametros
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdParametrica">IDentificador de la paramtrica</param>
        /// <param name="strTipo">Tipo de parametrica (MARCA_LUBRICANTES)</param>
        /// <param name="strCodigo">Codigo de parametrica</param>
        /// <param name="strDescripcion">Descripcion de la Parametrica</param>
        /// <param name="strValor">El Valor o nombre de la parametrica</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY ActualizaParametro(string strLlave, decimal decIdParametrica, string strTipo, string strCodigo, string strDescripcion, string strValor, decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_ACTUALIZA_PARAMETRICA(strLlave, decIdParametrica, strTipo, strCodigo, strDescripcion, strValor, decAppIdUsuario).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que elimina parametros
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdParametrica">Identificador de la parametrica.</param>
        /// <param name="strTipo">Tipo de parametrica(MARCA_LUBRICANTE)</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY EliminaParametro(string strLlave, decimal decIdParametrica, string strTipo, decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_ELIMINA_PARAMETRICA(strLlave, decIdParametrica, strTipo, decAppIdUsuario).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que registra nuevos parametros NOMBRE_PRODUCTO_COMERCIAL
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="decIdTablaEspec">Identificador de la Tabla Especifica</param>
        /// <param name="strCodigo">Codigo de la parametrica</param>
        /// <param name="strNombre">El Valor o nombre de la parametrica</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY RegistraParametroNpc(string strLlave, decimal decIdEntidad, decimal decIdTablaEspec, string strCodigo, string strNombre, decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_REGISTRA_PARAMETRICA_NPC(strLlave, decIdEntidad, decIdTablaEspec, strCodigo, strNombre, decAppIdUsuario).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que actualiza nuevos parametros NOMBRE_PRODUCTO_COMERCIAL
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdNombreProducto">Identificador del registro a modificar</param>
        /// <param name="decIdEntidad">Identificador de la entidad</param>
        /// <param name="decIdTablaEspec">Identificador de la Tabla Especifica</param>
        /// <param name="strCodigo">Codigo de la parametrica</param>
        /// <param name="strNombre">El Valor o nombre de la parametrica</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY ActualizaParametroNpc(string strLlave,decimal decIdNombreProducto, decimal decIdEntidad, decimal decIdTablaEspec, string strCodigo, string strNombre, decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_ACTUALIZA_PARAMETRICA_NPC(strLlave, decIdNombreProducto, decIdEntidad, decIdTablaEspec, strCodigo, strNombre, decAppIdUsuario).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que elimina parametros NOMBRE_PRODUCTO_COMERCIAL
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdNombreProducto">Identificador del registro a modificar</param>
        /// <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY EliminaParametroNpc(string strLlave, decimal decIdNombreProducto, decimal decAppIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_ELIMINA_PARAMETRICA_NPC(strLlave, decIdNombreProducto,decAppIdUsuario).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        /// <summary>
        /// Método que gestiona la configuracion para validaciones por actividad
        /// </summary>
        /// <param name="strLlave">Llave de acceso al módulo.</param>
        /// <param name="decIdConfiguracion">Identificador del registro a gstionar</param>
        /// <param name="decIdTipoActividad">Identificador del tipo de actividad</param>
        /// <param name="strValidacion">Texto de validacion segun diccionario de datos de BD</param>
        ///  <param name="decAppIdUsuario">Identificador del operador enviada desde la capa de aplicación.</param>
        ///  <param name="decAccion">Tipo de recurso a utilizar 1=registro, 2=modificacion, 3=eliminacion.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
       
        public O_RESULTADO_CTY GestionConfiguracion(string strLlave, decimal decIdConfiguracion, decimal decIdTipoActividad, string strValidacion, decimal decAppIdUsuario, decimal decAccion, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    O_RESULTADO_CTY objListado =

                        ctx.PUSR_GESTION_P_GESTION_CONFIGURACION(strLlave, decIdConfiguracion,decIdTipoActividad,strValidacion, decAppIdUsuario, decAccion).FirstOrDefault();
                    return objListado;
                }
                catch (Exception ex)
                {
                    strMensajeError = "ERROR namespace AnhPersistenciaCore.Aplicacion.Gestion: " + ex.Message + " - " + ex.InnerException;
                    var parametrosMetodo = new { strLlave };
                    CRegistraLog.RegistrarLog(_objDirectorio, Assembly.GetExecutingAssembly().FullName,
                                              MethodBase.GetCurrentMethod().Name,
                                              ex.Message + " - " + ex.InnerException, parametrosMetodo);
                    return null;
                }
            }
        }

        #endregion

        #region Movimiento de volumenes

        #region volumenes gas de alimento - GLP

        /// <summary>
        /// Registro de los volúmenes del gas de alimento.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al módulo.</param>
        /// <param name="planta">Identificador de la entidad (planta).</param>
        /// <param name="corriente">Identificador de la correinte (campo).</param>
        /// <param name="umVolumen">Identificado de la unidad de medida del volumen.</param>
        /// <param name="umGLP">Identificado de la unidad de medida del GLP.</param>
        /// <param name="umPoderCalor">Identificador de la unidad de medida del poder calorífico.</param>
        /// <param name="fecha">Fecha en formato DD/MM/YYYY.</param>
        /// <param name="gravEspec">Valor de la gravedad específica.</param>
        /// <param name="volumen">Valor del volúmen del gas de alimento.</param>
        /// <param name="contGLP">Valor del volúmen del GLP.</param>
        /// <param name="poderCalor">valor del poder calorífico.</param>
        /// <param name="n2">N2.</param>
        /// <param name="co2">CO2.</param>
        /// <param name="c1">C1.</param>
        /// <param name="c2">C2.</param>
        /// <param name="c3">C3.</param>
        /// <param name="iC4">iC4.</param>
        /// <param name="nC4">nC4.</param>
        /// <param name="iC5">iC5.</param>
        /// <param name="nC5">nC5.</param>
        /// <param name="nC6">nC6.</param>
        /// <param name="c7">C7.</param>
        /// <param name="obs">Observaciones del día.</param>
        /// <param name="jus">Justificacion del cambio realizado en el reporte de un día.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="decAppIdUsuario">Identificador del usuario que envia los datos.</param>
        /// <param name="strTipoReporte">Tipo de reprote en texto (díario,semanal,mensual).</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> RegistrarVolumenesAlimentoGLP(string strCredencial, decimal planta,
                                                                   decimal corriente, decimal umVolumen, decimal umGLP,
                                                                   decimal umPoderCalor, string fecha, decimal gravEspec,
                                                                   decimal volumen, decimal contGLP, decimal poderCalor,
                                                                   decimal n2, decimal co2, decimal c1, decimal c2,
                                                                   decimal c3, decimal iC4, decimal nC4, decimal iC5,
                                                                   decimal nC5, decimal nC6, decimal c7, string obs,
                                                                   string jus, decimal decAppIdUsuario,
                                                                   string strTipoReporte, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                decimal decAppFechaRegistro = 0;
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    #region Verificación de unidades de medida

                    List<O_UNIDADES_MEDIDA_GRAL_CTY> vListUnidadesMedida = null;
                    vListUnidadesMedida = ctx.PUSR_LISTADOS_P_LISTADO_UNIDADES_MEDIDA_GRAL().ToList();
                    resultado = VerificaUnidadMedida(umGLP, CParametrosHydro.StrGlp, vListUnidadesMedida, Constantes.cContenidoLicuables);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umVolumen, CParametrosHydro.StrVolumen, vListUnidadesMedida, Constantes.cVolumen);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umPoderCalor, CParametrosHydro.StrPoderCalorifico, vListUnidadesMedida, Constantes.cPoderCalorifico);
                    if (resultado != null)
                        return resultado;
                    vListUnidadesMedida = null;

                    #endregion

                    decAppFechaRegistro = CFechas.ConvierteDateTimeLong(System.DateTime.Now);
                    #region registrar datos en la BD

                    decimal decIdTipoOperacionGasAlimento, decIdTipoOperacionPadre;
                    decimal decIdTipoReporte;
                    decimal decIdCantidadVol = 0;

                    String[] listaFecha = fecha.Split('/');

                    #region Obtener el ID del tipo de operación

                    try
                    {
                        List<O_VOL_TIPOS_OPERACION_CTY> objListado =
                            ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_OPERACION(strCredencial, "", "", "").ToList();

                        List<O_VOL_TIPOS_OPERACION_CTY> objListadoAuxiliar = (from tOp in objListado
                                                                              where
                                                                                  tOp.NOMBRE.ToUpper().Contains("DTEYP")
                                                                              select tOp).ToList();

                        decIdTipoOperacionPadre = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                        objListadoAuxiliar = (from tOp in objListado
                                              where
                                                  tOp.NOMBRE.ToUpper().Contains("ALIMENTO") &&
                                                  tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                              select tOp).ToList();
                        decIdTipoOperacionGasAlimento = objListadoAuxiliar[0].ID_TIPO_OPERACION;
                    }
                    catch (Exception)
                    {
                        decIdTipoOperacionGasAlimento = decIdTipoOperacionPadre = 0;
                    }

                    #endregion

                    #region Obtener el tipo de reporte (díario, semanal, mensual, etc)

                    try
                    {
                        List<O_TIPOS_REPORTE_CTY> objTipoReporte =
                            ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_REPORTE(strCredencial, strTipoReporte).ToList();
                        decIdTipoReporte = objTipoReporte[0].ID_TIPO_REPORTE;
                    }
                    catch (Exception)
                    {
                        decIdTipoReporte = 0;
                    }

                    #endregion

                    #region Buscar la fecha del ultimo reporte

                    var objListados = new CPersistenciaListados();

                    var lstResultado = objListados.ObtenerFechaUltimoReporte(strCredencial,
                                                                             corriente, planta, decIdTipoReporte,
                                                                             decIdTipoOperacionGasAlimento, Convert.ToDecimal(listaFecha[2] + listaFecha[1] + listaFecha[0] + "000000"),
                                                                             ref strMensajeError);

                    string fechaultimaOperacion = lstResultado[0].FECHA_OPERACION;

                    #endregion

                    //if (RestarFechas(fecha, fechaultimaOperacion) < 0 && (jus.Trim() == "" || jus.Length < 5))
                    //if (RestarFechas(fecha, fechaultimaOperacion) >= 0 && (jus.Trim() == "" || jus.Length < 5))
                    if (RestarFechas(fecha, fechaultimaOperacion) >= 0)
                    {
                        #region mensaje de error al verificar la fecha del ultimo reporte

                        var obj = new O_RESULTADO_CTY
                        {
                            ID_TABLA = -9999,
                            MENSAJE_ERROR =
                                //"La justificación es requerida para reemplazar los datos observados",//"del día " + fecha,
                                "No se pudieron registrar los datos observados debido a que ya existe un registro de los mismo. Por favor comuniquese con el administrador.",
                            RESULTADO = -1
                        };
                        resultado = new List<O_RESULTADO_CTY> { obj };

                        #endregion
                    }
                    else
                    {
                        fecha = listaFecha[2] + listaFecha[1] + listaFecha[0] + "000000";

                        #region Obtener el tipo de registro

                        decimal decIdTipoRegistro;
                        try
                        {
                            //TIPO DE OPERACION DATOS REGISTRADOS POR FORMULARIO
                            List<O_TIPOS_REGISTRO_CTY> objTipoReporte =
                                ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_REGISTRO(strCredencial, "CRFANH", "").
                                    ToList();
                            decIdTipoRegistro = objTipoReporte[0].ID_TIPO_REGISTRO;
                        }
                        catch (Exception)
                        {
                            decIdTipoRegistro = 0;
                        }

                        #endregion

                        #region Obtener id de los productos

                        decimal decIdGLP;
                        decimal decIdGasAlimento;
                        try
                        {
                            List<O_PRODUCTOS_CTY> objProducto =
                                ctx.PUSR_LISTADOS_P_LISTADO_PRODUCTOS("").ToList();

                            List<O_PRODUCTOS_CTY> objProductoAuxiliar = (from pr in objProducto
                                                                         where pr.CODIGO.ToUpper().Contains("GLP")
                                                                         select pr).ToList();
                            decIdGLP = objProductoAuxiliar[0].ID_PRODUCTO;

                            objProductoAuxiliar = (from pr in objProducto
                                                   where
                                                       pr.DESCRIPCION.ToUpper().Contains("GAS") &&
                                                       pr.DESCRIPCION.ToUpper().Contains("ALIMENTO")
                                                   select pr).ToList();
                            decIdGasAlimento = objProductoAuxiliar[0].ID_PRODUCTO;
                        }
                        catch (Exception)
                        {
                            decIdGasAlimento = decIdGLP = 0;
                        }

                        #endregion

                        #region Obtener la tabla de especificación

                        decimal decIdTablaEspec;
                        try
                        {
                            List<O_PRODUCTO_ESPEC_CTY> objTablaEspec =
                                ctx.PCAN_LISTADOS_P_LISTADO_TABLA_ESPEC_PRODUCTO(strCredencial,
                                                                                 decIdGasAlimento,
                                                                                 "PRUEBA INCIAL DTEyP").
                                    ToList();
                            decIdTablaEspec = objTablaEspec[0].ID_TABLA_ESPEC;
                        }
                        catch (Exception)
                        {
                            decIdTablaEspec = 0;
                        }

                        #endregion

                        #region medios de transporte

                        decimal decIdDucto;
                        try
                        {
                            List<O_LISTA_TIPO_MEDIO_TRANS_CTY> objMedioTransporte =
                                ctx.PCAN_LISTADOS_P_LISTADO_TIPO_MEDIO_TRANSPORT(strCredencial, "",0).
                                    ToList();

                            List<O_LISTA_TIPO_MEDIO_TRANS_CTY> objProductoAuxiliar = (from mt in objMedioTransporte
                                                                                      where
                                                                                          mt.DESCRIPCION.ToUpper().
                                                                                          Contains("DUCTO")
                                                                                      select mt).ToList();
                            decIdDucto = objProductoAuxiliar[0].ID_TIPO_MEDIO_TRANSPORTE;
                        }
                        catch (Exception)
                        {
                            decIdDucto = 0;
                        }

                        #endregion

                        if (decIdTipoOperacionGasAlimento > 0 && decIdTipoOperacionPadre > 0 && decIdTipoReporte > 0 &&
                            decIdGasAlimento > 0
                            && decIdGLP > 0 && decIdTipoRegistro > 0 && decIdTablaEspec > 0)
                        {
                            try
                            {
                                #region registrar cantidades gas de alimento

                                resultado = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                    strCredencial,
                                    0, volumen,
                                    Convert.ToDecimal(fecha),
                                    decIdTipoRegistro, null,
                                    decIdGasAlimento, umVolumen,
                                    planta, null,
                                    decIdTipoOperacionGasAlimento,
                                    decIdTipoReporte,
                                    null,
                                    decAppIdUsuario, decAppFechaRegistro).ToList();

                                decimal decIdCantidad = resultado[0].ID_TABLA > 0 ? resultado[0].ID_TABLA : 0;

                                RegistrarCantidadesCampo(strCredencial, corriente, decIdCantidad,
                                                         Convert.ToDecimal(fecha),
                                                         obs, jus, decIdDucto, decAppFechaRegistro, decAppIdUsuario,
                                                         ref strMensajeError);

                                #endregion

                                #region registrar cantidades GLP

                                List<O_RESULTADO_CTY> resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                    strCredencial,
                                    0, contGLP,
                                    Convert.ToDecimal(fecha),
                                    decIdTipoRegistro, decIdCantidad,
                                    decIdGLP, umGLP,
                                    planta, null,
                                    decIdTipoOperacionGasAlimento,
                                    decIdTipoReporte,
                                    null,
                                    decAppIdUsuario, decAppFechaRegistro).ToList();
                                if (resultadoCantidad[0].ID_TABLA > 0)
                                {
                                    decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                }
                                else
                                {
                                    decIdCantidad = 0;
                                }
                                RegistrarCantidadesCampo(strCredencial, corriente, decIdCantidadVol,
                                                         Convert.ToDecimal(fecha),
                                                         "", "", decIdDucto, decAppFechaRegistro, decAppIdUsuario,
                                                         ref strMensajeError);

                                #endregion

                                #region Registrar certificado de calidad (principal)

                                if (decIdCantidad > 0)
                                {
                                    List<O_RESULTADO_CTY> resultadoCertificado =
                                        ctx.PUSR_GESTION_P_REGISTRA_REPORTE_PRINCIPAL(
                                            strCredencial,
                                            Convert.ToDecimal(fecha),
                                            "DTEyP-" + decIdCantidad.ToString(),
                                            decIdCantidad, null, decIdDucto, null,
                                            decIdTablaEspec, null,
                                            decIdTipoRegistro, planta,
                                            null, null, null, null,
                                            decAppFechaRegistro,
                                            decAppIdUsuario).ToList();

                                    decimal decIdRegistroCalPrincipal = resultadoCertificado[0].ID_TABLA > 0
                                                                            ? resultadoCertificado[0].ID_TABLA
                                                                            : 0;

                                    #region registrar el detalle del certificado de calidad

                                    if (decIdRegistroCalPrincipal > 0)
                                    {
                                        var lstItems = new ArrayList();

                                        #region Completar información de las pruebas de calidad

                                        decimal decIdPruebaCalidad;
                                        decimal decIdPruebaUnidad;
                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "GRAVESP-GAAL-PDTEP", "",
                                                               "", out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = gravEspec,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "PODCAL-GAAL-PDTEP", "",
                                                               "", out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = poderCalor,
                                                metodo = 0,
                                                unidad = umPoderCalor,
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "N2-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = n2,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "CO2-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = co2,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C1-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = c1,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C2-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = c2,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C3-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = c3,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "IC4-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = iC4,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "NC4-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = nC4,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "IC5-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = iC5,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "NC5-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = nC5,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "NC6-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = nC6,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C7-GAAL-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = c7,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        #endregion

                                        #region registrar el detalle de las pruebas de calidad

                                        string mensaje = "";
                                        foreach (var obj in lstItems)
                                        {
                                            object id = obj.GetType().GetProperty("id").GetValue(obj, null);
                                            object valor =
                                                obj.GetType().GetProperty("valor").GetValue(obj, null);
                                            string metodo =
                                                obj.GetType().GetProperty("metodo").GetValue(obj, null).ToString();
                                            object unidad =
                                                obj.GetType().GetProperty("unidad").GetValue(obj, null);
                                            object rango =
                                                obj.GetType().GetProperty("rango").GetValue(obj, null);

                                            List<O_RESULTADO_CTY> resultadoRegistroCalidad =
                                                ctx.PUSR_GESTION_P_REGISTRA_REPORTE_CALIDAD
                                                    (
                                                        strCredencial,
                                                        Convert.ToDecimal(valor),
                                                        "",
                                                        Convert.ToDecimal(rango),
                                                        metodo,
                                                        Convert.ToDecimal(id), decIdRegistroCalPrincipal,
                                                        Convert.ToDecimal(unidad), "",
                                                        Convert.ToDecimal(decAppFechaRegistro),
                                                        Convert.ToDecimal(decAppIdUsuario)).ToList();
                                            if (resultadoRegistroCalidad[0].ID_TABLA < 1 &&
                                                resultadoRegistroCalidad[0].MENSAJE_ERROR.Trim() != "OK")
                                                mensaje += resultadoRegistroCalidad[0].MENSAJE_ERROR;
                                        }
                                        strMensajeError += (mensaje.Trim() != "" ? "Error al registrar el análisis de calidad: " : "") + mensaje;

                                        #endregion
                                    }

                                    #endregion
                                }

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                strMensajeError =
                                    CMensajeError.FormatearMensajeDeError(
                                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                                        ex,
                                        TipoMensajeError);
                            }
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        #endregion

        #region volúmenes producidos GLP



        /// <summary>
        /// Registro de volúmenes producidos.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al módulo.</param>
        /// <param name="planta">Identificador de la planta.</param>
        /// <param name="umProdGlp">Unidad de medida de la producción de GLP.</param>
        /// <param name="umProdPropano">Unidad de medida de la producción de Propano.</param>
        /// <param name="umEntregaPropano">Unidad de medida del propano entregado/consumido.</param>
        /// <param name="umEntregaGlpCisterna">Unidad de medida del volumen de GLP entregado por cisterna.</param>
        /// <param name="umEntregaGlpDucto">Unidad de medida del volumen de GLP entregado por ducto.</param>
        /// <param name="umSaldoGlp">Unidad de medida del saldo de GLP.</param>
        /// <param name="umSaldoPropano">Unidad de medida del saldo de propano.</param>
        /// <param name="umTemperatura">Unidad de medida de la temperatura.</param>
        /// <param name="fecha">Fecha de reporte.</param>
        /// <param name="produccionGLP">Volúmen de la producción de GLP.</param>
        /// <param name="produccionPropano">Volúmen de la producción de propano.</param>
        /// <param name="entregaConsumoPropano">Volúmen de la entrega/consumo de propano.</param>
        /// <param name="entregaGlpCisterna">Volúmen de la entrega de GLP por cisterna.</param>
        /// <param name="entregaGlpDucto">Volúmen de la entrega de GLP por ducto.</param>
        /// <param name="saldoGLP">Volúmen del saldo de GLP.</param>
        /// <param name="saldoPropano">Volúmen del saldo de Propano.</param>
        /// <param name="gravedadEspecifica">Valor de la gravedad específica.</param>
        /// <param name="tvr">Valor de la Tensión de Vapor Reid.</param>
        /// <param name="temperatura">Valor de la temperatura.</param>
        /// <param name="c2">C2.</param>
        /// <param name="c3">C3.</param>
        /// <param name="iC4">iC4.</param>
        /// <param name="nC4">nC4.</param>
        /// <param name="iC5">iC5.</param>
        /// <param name="nC5">nC5.</param>
        /// <param name="obs">Observaciones reprotadas por la planta.</param>
        /// <param name="just">justificacion del cambio realizado en el reporte de un día.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="decAppIdUsuario">Identificador del usuario que envia los datos.</param>
        /// <param name="strTipoReporte">tipo de reprote en texto (díario,semanal,mensual).</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> RegistrarVolumenesProduccionGLP(string strCredencial,
                                                                     decimal planta,
                                                                     decimal umProdGlp,
                                                                     decimal umProdPropano,
                                                                     decimal umEntregaPropano,
                                                                     decimal umEntregaGlpCisterna,
                                                                     decimal umEntregaGlpDucto,
                                                                     decimal umSaldoGlp,
                                                                     decimal umSaldoPropano,
                                                                     decimal umTemperatura,
                                                                     string fecha,
                                                                     decimal produccionGLP,
                                                                     decimal produccionPropano,
                                                                     decimal entregaConsumoPropano,
                                                                     decimal entregaGlpCisterna,
                                                                     decimal entregaGlpDucto,
                                                                     decimal saldoGLP,
                                                                     decimal saldoPropano,
                                                                     decimal gravedadEspecifica,
                                                                     decimal tvr,
                                                                     decimal temperatura,
                                                                     decimal c2,
                                                                     decimal c3,
                                                                     decimal iC4,
                                                                     decimal nC4,
                                                                     decimal iC5,
                                                                     decimal nC5,
                                                                     string obs,
                                                                     string just,
                                                                     decimal decAppIdUsuario,
                                                                     string strTipoReporte,
                                                                     decimal rendProdGlp,
                                                                     decimal gasCombustible,
                                                                     decimal quemaGas,
                                                                     decimal produccionGasolinaNatural,
                                                                     decimal umProdRendProdGlp,
                                                                     decimal umGasCombustible,
                                                                     decimal umQuemaGas,
                                                                     decimal umGasolinaNatural,
                                                                     ref string strMensajeError)
        {

            using (var ctx = new EntidadesOctano())
            {
                decimal decAppFechaRegistro = 0;
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    #region Verificación de unidades de medida

                    List<O_UNIDADES_MEDIDA_GRAL_CTY> vListUnidadesMedida = null;
                    vListUnidadesMedida = ctx.PUSR_LISTADOS_P_LISTADO_UNIDADES_MEDIDA_GRAL().ToList();
                    resultado = VerificaUnidadMedida(umProdGlp, CParametrosHydro.StrProduccionGlp, vListUnidadesMedida, Constantes.cProduccionGlp);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umProdPropano, CParametrosHydro.StrProduccionPropano, vListUnidadesMedida, Constantes.cProduccionPropano);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umEntregaPropano, CParametrosHydro.StrEntregaConsumoPropano, vListUnidadesMedida, Constantes.cEntregaConsumoPropano);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umEntregaGlpCisterna, CParametrosHydro.StrEntregaGlpCisterna, vListUnidadesMedida, Constantes.cEntregaGlpCisterna);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umProdRendProdGlp, CParametrosHydro.StrRendimientoProduccionGlp, vListUnidadesMedida, Constantes.cRendimientoProduccionGlp);
                    if (resultado != null)
                        return resultado;

                    resultado = VerificaUnidadMedida(umEntregaGlpDucto, CParametrosHydro.StrEntregaGlpDucto, vListUnidadesMedida, Constantes.cEntregaGlpDucto);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umSaldoGlp, CParametrosHydro.StrSaldoGlp, vListUnidadesMedida, Constantes.cSaldoGlp);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umSaldoPropano, CParametrosHydro.StrSaldoPropano, vListUnidadesMedida, Constantes.cSaldoPropano);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umTemperatura, CParametrosHydro.StrTemperatura, vListUnidadesMedida, Constantes.cTemperatura);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umGasCombustible, CParametrosHydro.StrGasCombustible, vListUnidadesMedida, Constantes.cGasCombustible);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umGasolinaNatural, CParametrosHydro.StrGasolinaNatural, vListUnidadesMedida, Constantes.cGasolinaNatural);
                    if (resultado != null)
                        return resultado;
                    vListUnidadesMedida = null;

                    #endregion

                    decAppFechaRegistro = CFechas.ConvierteDateTimeLong(System.DateTime.Now);
                    #region registrar datos en la BD

                    decimal decIdTipoOperacionProduccion,
                            decIdTipoOperacionEntrega,
                            decIdTipoOperacionSaldo,
                            decIdTipoOperacionPadre,
                            decIdTipoOperacionRendProd,
                            decIdTipoOperacionGasComb,
                            decIdTipoOperacionQuemaGas;
                    decimal decIdTipoReporte;
                    String[] listaFecha = fecha.Split('/');
                    decimal fechaTipoReporte = 0;

                    #region Obtener el ID del tipo de operación

                    try
                    {
                        List<O_VOL_TIPOS_OPERACION_CTY> objListado =
                            ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_OPERACION(strCredencial, "", "", "").ToList();

                        List<O_VOL_TIPOS_OPERACION_CTY> objListadoAuxiliar = (from tOp in objListado
                                                                              where
                                                                                  tOp.NOMBRE.ToUpper().Contains("DTEYP")
                                                                              select tOp).ToList();

                        decIdTipoOperacionPadre = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                        objListadoAuxiliar = (from tOp in objListado
                                              where
                                                  tOp.NOMBRE.ToUpper().Contains("PRODUCCION") &&
                                                  tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                              select tOp).ToList();
                        decIdTipoOperacionProduccion = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                        objListadoAuxiliar = (from tOp in objListado
                                              where
                                                  tOp.NOMBRE.ToUpper().Contains("ENTREGA") &&
                                                  tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                              select tOp).ToList();
                        decIdTipoOperacionEntrega = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                        objListadoAuxiliar = (from tOp in objListado
                                              where
                                                  tOp.NOMBRE.ToUpper().Contains("SALDO") &&
                                                  tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                              select tOp).ToList();
                        decIdTipoOperacionSaldo = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                        objListadoAuxiliar = (from tOp in objListado
                                              where
                                                  tOp.NOMBRE.ToUpper().Contains("RENDIMIENTO PRODUCCION GLP") &&
                                                  tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                              select tOp).ToList();
                        decIdTipoOperacionRendProd = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                        objListadoAuxiliar = (from tOp in objListado
                                              where
                                                  tOp.NOMBRE.ToUpper().Contains("GAS COMBUSTIBLE") &&
                                                  tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                              select tOp).ToList();
                        decIdTipoOperacionGasComb = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                        objListadoAuxiliar = (from tOp in objListado
                                              where
                                                  tOp.NOMBRE.ToUpper().Contains("QUEMA GAS") &&
                                                  tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                              select tOp).ToList();
                        decIdTipoOperacionQuemaGas = objListadoAuxiliar[0].ID_TIPO_OPERACION;
                    }
                    catch (Exception)
                    {
                        decIdTipoOperacionProduccion =
                            decIdTipoOperacionEntrega = decIdTipoOperacionSaldo = decIdTipoOperacionRendProd = decIdTipoOperacionGasComb = decIdTipoOperacionQuemaGas = decIdTipoOperacionPadre = 0;
                    }

                    #endregion

                    #region Obtener el tipo de reporte (díario, semanal, mensual, etc)

                    try
                    {
                        List<O_TIPOS_REPORTE_CTY> objTipoReporte =
                            ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_REPORTE(strCredencial, strTipoReporte).ToList();
                        decIdTipoReporte = objTipoReporte[0].ID_TIPO_REPORTE;

                        if (strTipoReporte.ToUpper().Contains("DIARIO"))
                        {
                            fechaTipoReporte =
                                Convert.ToDecimal(listaFecha[2] + listaFecha[1] + listaFecha[0] + "000000");
                        }
                        else if (strTipoReporte.ToUpper().Contains("MENSUAL"))
                        {
                            fechaTipoReporte = Convert.ToDecimal(listaFecha[2] + listaFecha[1] + "01" + "000000");
                        }
                    }
                    catch (Exception)
                    {
                        decIdTipoReporte = 0;
                    }

                    #endregion

                    #region obtener la fecha del ultimo reporte

                    var objListados = new CPersistenciaListados();

                    var lstResultado = objListados.ObtenerFechaUltimoReporte(strCredencial,
                                                                             0, planta, decIdTipoReporte,
                                                                             decIdTipoOperacionProduccion, Convert.ToDecimal(listaFecha[2] + listaFecha[1] + listaFecha[0] + "000000"),
                                                                             ref strMensajeError);

                    string fechaultimaOperacion = lstResultado[0].FECHA_OPERACION;

                    #endregion

                    //if (RestarFechas(fecha, fechaultimaOperacion) < 0 && (just.Trim() == "" || just.Length < 5))
                    //if (RestarFechas(fecha, fechaultimaOperacion) >= 0 && (just.Trim() == "" || just.Length < 5))
                    if (RestarFechas(fecha, fechaultimaOperacion) >= 0)
                    {
                        #region Mensaje de error al verificar la fecha

                        var obj = new O_RESULTADO_CTY
                        {
                            ID_TABLA = -9999,
                            MENSAJE_ERROR =
                                //"La justificación es requerida para reemplazar los datos observados",// del día " + fecha,
                               "No se pudieron registrar los datos observados debido a que ya existe un registro de los mismo. Por favor comuniquese con el administrador.",
                            RESULTADO = -1
                        };
                        resultado = new List<O_RESULTADO_CTY> { obj };

                        #endregion
                    }
                    else
                    {
                        fecha = listaFecha[2] + listaFecha[1] + listaFecha[0] + "000000";

                        #region Obtener el tipo de registro

                        decimal decIdTipoRegistro;
                        try
                        {
                            //TIPO DE OPERACION DATOS REGISTRADOS POR FORMULARIO
                            List<O_TIPOS_REGISTRO_CTY> objTipoReporte =
                                ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_REGISTRO(strCredencial, "CRFANH", "").
                                    ToList();
                            decIdTipoRegistro = objTipoReporte[0].ID_TIPO_REGISTRO;
                        }
                        catch (Exception)
                        {
                            decIdTipoRegistro = 0;
                        }

                        #endregion

                        #region Obtener id de los productos

                        decimal decIdPropano;
                        decimal decIdGLP;
                        decimal decIdGasolinasNatural;
                        decimal decIdGasNatural;
                        try
                        {
                            List<O_PRODUCTOS_CTY> objProducto =
                                ctx.PUSR_LISTADOS_P_LISTADO_PRODUCTOS("").ToList();

                            List<O_PRODUCTOS_CTY> objProductoAuxiliar = (from pr in objProducto
                                                                         where pr.CODIGO.ToUpper().Contains("GLP")
                                                                         select pr).ToList();
                            decIdGLP = objProductoAuxiliar[0].ID_PRODUCTO;

                            objProductoAuxiliar = (from pr in objProducto
                                                   where
                                                       pr.DESCRIPCION.ToUpper().Contains("PROPANO")
                                                   select pr).ToList();
                            decIdPropano = objProductoAuxiliar[0].ID_PRODUCTO;

                            objProductoAuxiliar = (from pr in objProducto
                                                   where
                                                       pr.DESCRIPCION.ToUpper().Contains("GASOLINA NATURAL")
                                                   select pr).ToList();
                            decIdGasolinasNatural = objProductoAuxiliar[0].ID_PRODUCTO;

                            objProductoAuxiliar = (from pr in objProducto
                                                   where
                                                       pr.DESCRIPCION.ToUpper().Contains("GAS NATURAL")
                                                   select pr).ToList();
                            decIdGasNatural = objProductoAuxiliar[0].ID_PRODUCTO;

                        }
                        catch (Exception)
                        {
                            decIdPropano = decIdGLP = decIdGasolinasNatural = decIdGasNatural = 0;
                        }

                        #endregion

                        #region Obtener la tabla de especificación

                        decimal decIdTablaEspec;
                        try
                        {
                            List<O_PRODUCTO_ESPEC_CTY> objTablaEspec =
                                ctx.PCAN_LISTADOS_P_LISTADO_TABLA_ESPEC_PRODUCTO(strCredencial, decIdGLP,
                                                                                 "PRUEBA INCIAL DTEyP").
                                    ToList();
                            decIdTablaEspec = objTablaEspec[0].ID_TABLA_ESPEC;
                        }
                        catch (Exception)
                        {
                            decIdTablaEspec = 0;
                        }

                        #endregion

                        #region medios de transporte

                        decimal decIdCisterna;
                        decimal decIdDucto;
                        decimal decIdOtro;
                        decimal decIdNoId = -99999;
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
                            decIdCisterna = objProductoAuxiliar[0].ID_TIPO_MEDIO_TRANSPORTE;

                            objProductoAuxiliar = (from mt in objMedioTransporte
                                                   where mt.DESCRIPCION.ToUpper().Contains("DUCTO")
                                                   select mt).ToList();
                            decIdDucto = objProductoAuxiliar[0].ID_TIPO_MEDIO_TRANSPORTE;

                            objProductoAuxiliar = (from mt in objMedioTransporte
                                                   where mt.DESCRIPCION.ToUpper().Contains("OTROS MEDIOS DE TRANSPORTE")
                                                   select mt).ToList();
                            decIdOtro = objProductoAuxiliar[0].ID_TIPO_MEDIO_TRANSPORTE;

                        }
                        catch (Exception)
                        {
                            decIdCisterna = decIdDucto = decIdOtro = 0;
                        }

                        #endregion


                        if (decIdTipoOperacionProduccion > 0 && decIdTipoOperacionEntrega > 0 &&
                            decIdTipoOperacionSaldo > 0 && decIdTipoOperacionPadre > 0 &&
                            decIdTipoReporte > 0 && decIdPropano > 0 && decIdGLP > 0 &&
                            decIdGasolinasNatural > 0 &&
                            decIdTipoRegistro > 0 && decIdTablaEspec > 0 &&
                            decIdCisterna > 0 && decIdDucto > 0 && decIdOtro > 0)
                        {
                            try
                            {
                                #region buscar valor del prode

                                var listaProde = ctx.PCAN_LISTADOS_P_LISTADO_PRODE(
                                    strCredencial,
                                    0, planta,
                                    decIdTipoReporte,
                                    decIdGLP, fechaTipoReporte).ToList();

                                if (listaProde.Count < 1)
                                {
                                    List<O_TIPOS_REPORTE_CTY> objTipoReporte =
                                        ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_REPORTE(strCredencial,
                                                                                  strTipoReporte.ToUpper().Contains(
                                                                                      "DIARIO")
                                                                                      ? "MENSUAL"
                                                                                      : "DIARIO").ToList();
                                    decimal decIdTipoReporteAux = objTipoReporte[0].ID_TIPO_REPORTE;

                                    if (strTipoReporte.ToUpper().Contains("DIARIO"))
                                    {
                                        fechaTipoReporte =
                                            Convert.ToDecimal(listaFecha[2] + listaFecha[1] + "01" + "000000");
                                    }
                                    else if (strTipoReporte.ToUpper().Contains("MENSUAL"))
                                    {
                                        fechaTipoReporte =
                                            Convert.ToDecimal(listaFecha[2] + listaFecha[1] + listaFecha[0] + "000000");
                                    }
                                    listaProde = ctx.PCAN_LISTADOS_P_LISTADO_PRODE(
                                        strCredencial,
                                        0, planta,
                                        decIdTipoReporteAux,
                                        decIdGLP, fechaTipoReporte).ToList();
                                }

                                #endregion

                                if (listaProde != null && listaProde.Count > 0 &&
                                    (
                                        produccionGLP >= listaProde[0].VALOR_PRODE ||
                                        (produccionGLP < listaProde[0].VALOR_PRODE && just.Trim() != "")
                                    )
                                    )
                                {

                                    #region registrar cantidades PRODUCIDAS

                                    resultado = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        0, produccionGLP,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, null,
                                        decIdGLP, umProdGlp,
                                        planta, null,
                                        decIdTipoOperacionProduccion,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    decimal decIdCantidad;
                                    if (resultado[0].ID_TABLA > 0)
                                    {
                                        decIdCantidad = resultado[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidad,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdDucto, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                        //Solo se registraran las observaciones y justificacion para el primer registro
                                        obs = just = "";
                                    }

                                    else
                                    {
                                        decIdCantidad = 0;
                                    }

                                    List<O_RESULTADO_CTY> resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        0, produccionPropano,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdPropano, umProdPropano,
                                        planta, null,
                                        decIdTipoOperacionProduccion,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    decimal decIdCantidadVol;
                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdDucto, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }

                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        0, produccionGasolinaNatural,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdGasolinasNatural, umGasolinaNatural,
                                        planta, null,
                                        decIdTipoOperacionProduccion,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdOtro, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }


                                    #endregion

                                    #region registrar cantidades ENTREGADAS

                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        entregaConsumoPropano, 0,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdPropano, umEntregaPropano,
                                        planta, null,
                                        decIdTipoOperacionEntrega,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdDucto, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }

                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        entregaGlpCisterna, 0,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdGLP, umEntregaGlpCisterna,
                                        planta, null,
                                        decIdTipoOperacionEntrega,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdCisterna, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }

                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        entregaGlpDucto, 0,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdGLP, umEntregaGlpDucto,
                                        planta, null,
                                        decIdTipoOperacionEntrega,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdDucto, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }

                                    #endregion

                                    #region registrar SALDOS

                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        0, saldoPropano,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdPropano, umSaldoPropano,
                                        planta, null,
                                        decIdTipoOperacionSaldo,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdDucto, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }

                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        0, saldoGLP,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdGLP, umSaldoGlp,
                                        planta, null,
                                        decIdTipoOperacionSaldo,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdDucto, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }

                                    #endregion

                                    #region registrar RENDIMIENTO PRODUCCION GLP
                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        0, rendProdGlp,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdGLP, umProdRendProdGlp,
                                        planta, null,
                                        decIdTipoOperacionRendProd,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdNoId, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }
                                    #endregion
                                    #region registrar GAS COMBUSTIBLE
                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        0, gasCombustible,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdGasNatural, umGasCombustible,
                                        planta, null,
                                        decIdTipoOperacionGasComb,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdNoId, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }
                                    #endregion
                                    #region registrar QUEMA GAS
                                    resultadoCantidad = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                        strCredencial,
                                        0, quemaGas,
                                        Convert.ToDecimal(fecha),
                                        decIdTipoRegistro, decIdCantidad,
                                        decIdGasNatural, umQuemaGas,
                                        planta, null,
                                        decIdTipoOperacionQuemaGas,
                                        decIdTipoReporte,
                                        null,
                                        decAppIdUsuario, decAppFechaRegistro).ToList();

                                    if (resultadoCantidad[0].ID_TABLA > 0)
                                    {
                                        decIdCantidadVol = resultadoCantidad[0].ID_TABLA;
                                        RegistrarCantidadesCampo(strCredencial, 0, decIdCantidadVol,
                                                                 Convert.ToDecimal(fecha),
                                                                 obs, just, decIdNoId, decAppFechaRegistro,
                                                                 decAppIdUsuario,
                                                                 ref strMensajeError);
                                    }
                                    #endregion

                                    #region Registrar certificado de calidad (principal)

                                    if (decIdCantidad > 0)
                                    {
                                        List<O_RESULTADO_CTY> resultadoCertificado =
                                            ctx.PUSR_GESTION_P_REGISTRA_REPORTE_PRINCIPAL(
                                                strCredencial,
                                                Convert.ToDecimal(fecha),
                                                "DTEyP-" + decIdCantidad.ToString(),
                                                decIdCantidad, null, decIdDucto, null,
                                                decIdTablaEspec, null,
                                                decIdTipoRegistro, planta,
                                                null, null, null, null,
                                                decAppFechaRegistro,
                                                decAppIdUsuario).ToList();

                                        decimal decIdRegistroCalPrincipal = resultadoCertificado[0].ID_TABLA > 0
                                                                                ? resultadoCertificado[0].ID_TABLA
                                                                                : 0;

                                        #region registrar el detalle del certificado de calidad

                                        if (decIdRegistroCalPrincipal > 0)
                                        {
                                            var lstItems = new ArrayList();

                                            #region Completar información de las pruebas de calidad

                                            decimal decIdPruebaCalidad;
                                            decimal decIdPruebaUnidad;
                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "GRAVESP-GLP-PDTEP",
                                                                   "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = gravedadEspecifica,
                                                    metodo = 0,
                                                    unidad =
                                                ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                                   ref strMensajeError),
                                                    rango = 0
                                                });

                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "TVR-GLP-PDTEP", "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = tvr,
                                                    metodo = 0,
                                                    unidad =
                                                ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                                   ref strMensajeError),
                                                    rango = 0
                                                });

                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "TEMP-GLP-PDTEP", "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = temperatura,
                                                    metodo = 0,
                                                    unidad = umTemperatura,
                                                    rango = 0
                                                });

                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C2-GLP-PDTEP", "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = c2,
                                                    metodo = 0,
                                                    unidad =
                                                ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                                   ref strMensajeError),
                                                    rango = 0
                                                });

                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C3-GLP-PDTEP", "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = c3,
                                                    metodo = 0,
                                                    unidad =
                                                ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                                   ref strMensajeError),
                                                    rango = 0
                                                });

                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "IC4-GLP-PDTEP", "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = iC4,
                                                    metodo = 0,
                                                    unidad =
                                                ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                                   ref strMensajeError),
                                                    rango = 0
                                                });

                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "NC4-GLP-PDTEP", "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = nC4,
                                                    metodo = 0,
                                                    unidad =
                                                ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                                   ref strMensajeError),
                                                    rango = 0
                                                });

                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "IC5-GLP-PDTEP", "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = iC5,
                                                    metodo = 0,
                                                    unidad =
                                                ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                                   ref strMensajeError),
                                                    rango = 0
                                                });

                                            ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "NC5-GLP-PDTEP", "",
                                                                   "",
                                                                   out decIdPruebaCalidad,
                                                                   out decIdPruebaUnidad, ref strMensajeError);
                                            lstItems.Add(
                                                new
                                                {
                                                    id = decIdPruebaCalidad,
                                                    valor = nC5,
                                                    metodo = 0,
                                                    unidad =
                                                ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                                   ref strMensajeError),
                                                    rango = 0
                                                });

                                            #endregion

                                            #region registrar el detalle de las pruebas de calidad

                                            string mensaje = "";
                                            foreach (var obj in lstItems)
                                            {
                                                object id = obj.GetType().GetProperty("id").GetValue(obj, null);
                                                object valor =
                                                    obj.GetType().GetProperty("valor").GetValue(obj, null);
                                                string metodo =
                                                    obj.GetType().GetProperty("metodo").GetValue(obj, null).ToString();
                                                object unidad =
                                                    obj.GetType().GetProperty("unidad").GetValue(obj, null);
                                                object rango =
                                                    obj.GetType().GetProperty("rango").GetValue(obj, null);

                                                List<O_RESULTADO_CTY> resultadoRegistroCalidad =
                                                    ctx.PUSR_GESTION_P_REGISTRA_REPORTE_CALIDAD
                                                        (
                                                            strCredencial,
                                                            Convert.ToDecimal(valor),
                                                            "",
                                                            Convert.ToDecimal(rango),
                                                            metodo,
                                                            Convert.ToDecimal(id), decIdRegistroCalPrincipal,
                                                            Convert.ToDecimal(unidad), "",
                                                            Convert.ToDecimal(decAppFechaRegistro),
                                                            Convert.ToDecimal(decAppIdUsuario)).ToList();
                                                if (resultadoRegistroCalidad[0].ID_TABLA < 1 &&
                                                    resultadoRegistroCalidad[0].MENSAJE_ERROR.Trim() != "OK")
                                                    mensaje += resultadoRegistroCalidad[0].MENSAJE_ERROR;
                                            }

                                            strMensajeError += mensaje;

                                            #endregion
                                        }

                                        #endregion
                                    }

                                    #endregion

                                }
                                else
                                {
                                    #region mensaje error al verificar el PRODE

                                    strMensajeError = "[" + listaFecha[0] + "/" + listaFecha[1] + "/" + listaFecha[2] +
                                                      "] " +
                                                      (listaProde == null || listaProde.Count < 1
                                                           ? "no se puede determinar el PRODE para este día, consulte con el administrador"
                                                           : just.Trim() == ""
                                                                 ? "La justificación es requerida por una baja producción.<br/>PRODE: " +
                                                                   listaProde[0].VALOR_PRODE.ToString() +
                                                                   " Vol. Producido: " + produccionGLP.ToString()
                                                                 : "Error al registrar.");

                                    var obj = new O_RESULTADO_CTY
                                    {
                                        ID_TABLA = -9999,
                                        MENSAJE_ERROR = strMensajeError,
                                        RESULTADO = -1
                                    };
                                    resultado = new List<O_RESULTADO_CTY> { obj };

                                    #endregion
                                }
                            }
                            catch (Exception ex)
                            {
                                strMensajeError =
                                    CMensajeError.FormatearMensajeDeError(
                                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                                        ex,
                                        TipoMensajeError);
                            }
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        #endregion

        #region volúmenes gas residual GLP

        /// <summary>
        /// Volúmenes de gas residual.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="planta">Identificador de la planta.</param>
        /// <param name="umReslVolumen">Unidad de medida del volumen de gas residual.</param>
        /// <param name="umResPuntoRocio">Unidad de medida del Punto de rocio.</param>
        /// <param name="umResPoderCalorifico">Unidad de medida del Poder calorífico.</param>
        /// <param name="fecha">Fecha de reporte.</param>
        /// <param name="gravedadEspecifica">Valor de la gravedad específica.</param>
        /// <param name="volumen">Valor del volúmen de gas residual.</param>
        /// <param name="h2O">Valor del H2O.</param>
        /// <param name="puntoRocio">Valor del punto de rocío.</param>
        /// <param name="poderCalorifico">Valor del poder calorífico.</param>
        /// <param name="n2">N2.</param>
        /// <param name="co2">CO2.</param>
        /// <param name="c1">C1.</param>
        /// <param name="c2">C2.</param>
        /// <param name="c3">C3.</param>
        /// <param name="iC4">i-C4.</param>
        /// <param name="nC4">n-C4.</param>
        /// <param name="iC5">i-C5.</param>
        /// <param name="nC5">n-C5.</param>
        /// <param name="nC6">n-C6.</param>
        /// <param name="c7">C7.</param>
        /// <param name="obs">Observaciones del día.</param>
        /// <param name="just">Justificacion del cambio realizado en el reporte de un día.</param>
        /// <param name="decAppFechaRegistro">Fecha de registro enviada desde la capa de aplicación.</param>
        /// <param name="decAppIdUsuario">Identificador del usuario que envia los datos</param>
        /// <param name="strTipoReporte">tipo de reprote en texto (díario,semanal,mensual)</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> RegistrarVolumenesResidualGLP(string strCredencial, decimal planta,
                                                                   decimal umReslVolumen, decimal umResPuntoRocio,
                                                                   decimal umResPoderCalorifico, string fecha,
                                                                   decimal gravedadEspecifica, decimal volumen,
                                                                   decimal h2O, decimal puntoRocio,
                                                                   decimal poderCalorifico, decimal n2, decimal co2,
                                                                   decimal c1, decimal c2, decimal c3, decimal iC4,
                                                                   decimal nC4, decimal iC5, decimal nC5, decimal nC6,
                                                                   decimal c7, string obs, string just,
                                                                   decimal decAppIdUsuario,
                                                                   string strTipoReporte, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                decimal decAppFechaRegistro = 0;
                List<O_RESULTADO_CTY> resultado = null;
                try
                {
                    #region Verificación de unidades de medida

                    List<O_UNIDADES_MEDIDA_GRAL_CTY> vListUnidadesMedida = null;
                    vListUnidadesMedida = ctx.PUSR_LISTADOS_P_LISTADO_UNIDADES_MEDIDA_GRAL().ToList();
                    resultado = VerificaUnidadMedida(umResPuntoRocio, CParametrosHydro.StrPuntoRocio, vListUnidadesMedida, Constantes.cPuntoRocio);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umReslVolumen, CParametrosHydro.StrVolumen, vListUnidadesMedida, Constantes.cVolumen);
                    if (resultado != null)
                        return resultado;
                    resultado = VerificaUnidadMedida(umResPoderCalorifico, CParametrosHydro.StrPoderCalorifico, vListUnidadesMedida, Constantes.cPoderCalorifico);
                    if (resultado != null)
                        return resultado;
                    vListUnidadesMedida = null;

                    #endregion

                    decAppFechaRegistro = CFechas.ConvierteDateTimeLong(System.DateTime.Now);
                    #region registrar datos en la BD

                    decimal decIdTipoOperacionResidual,
                            decIdTipoOperacionPadre;
                    decimal decIdTipoReporte;
                    String[] listaFecha = fecha.Split('/');

                    #region Obtener el ID del tipo de operación

                    try
                    {
                        List<O_VOL_TIPOS_OPERACION_CTY> objListado =
                            ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_OPERACION(strCredencial, "", "", "").ToList();

                        List<O_VOL_TIPOS_OPERACION_CTY> objListadoAuxiliar = (from tOp in objListado
                                                                              where
                                                                                  tOp.NOMBRE.ToUpper().Contains("DTEYP")
                                                                              select tOp).ToList();

                        decIdTipoOperacionPadre = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                        objListadoAuxiliar = (from tOp in objListado
                                              where
                                                  tOp.NOMBRE.ToUpper().Contains("RESIDUAL") &&
                                                  tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                              select tOp).ToList();
                        decIdTipoOperacionResidual = objListadoAuxiliar[0].ID_TIPO_OPERACION;
                    }
                    catch (Exception)
                    {
                        decIdTipoOperacionResidual = decIdTipoOperacionPadre = 0;
                    }

                    #endregion

                    #region Obtener el tipo de reporte (díario, semanal, mensual, etc)

                    try
                    {
                        List<O_TIPOS_REPORTE_CTY> objTipoReporte =
                            ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_REPORTE(strCredencial, strTipoReporte).ToList();
                        decIdTipoReporte = objTipoReporte[0].ID_TIPO_REPORTE;
                    }
                    catch (Exception)
                    {
                        decIdTipoReporte = 0;
                    }

                    #endregion

                    #region buscar la fecha del ultimo reporte

                    var objListados = new CPersistenciaListados();

                    var lstResultado = objListados.ObtenerFechaUltimoReporte(strCredencial,
                                                                             0, planta, decIdTipoReporte,
                                                                             decIdTipoOperacionResidual, Convert.ToDecimal(listaFecha[2] + listaFecha[1] + listaFecha[0] + "000000"),
                                                                             ref strMensajeError);

                    string fechaultimaOperacion = lstResultado[0].FECHA_OPERACION;

                    #endregion

                    //if (RestarFechas(fecha, fechaultimaOperacion) < 0 && (just.Trim() == "" || just.Length < 5))
                    //if (RestarFechas(fecha, fechaultimaOperacion) >= 0 && (just.Trim() == "" || just.Length < 5))
                    if (RestarFechas(fecha, fechaultimaOperacion) >= 0)
                    {
                        #region mensaje de error al verificar la fecha

                        var obj = new O_RESULTADO_CTY
                        {
                            ID_TABLA = -9999,
                            MENSAJE_ERROR =
                                //"La justificación es requerida para reemplazar los datos observados",//del día " + fecha,
                                "No se pudieron registrar los datos observados debido a que ya existe un registro de los mismo. Por favor comuniquese con el administrador.",
                            RESULTADO = -1
                        };
                        resultado = new List<O_RESULTADO_CTY> { obj };

                        #endregion
                    }
                    else
                    {
                        fecha = listaFecha[2] + listaFecha[1] + listaFecha[0] + "000000";

                        #region Obtener el tipo de registro

                        decimal decIdTipoRegistro;
                        try
                        {
                            //TIPO DE OPERACION DATOS REGISTRADOS POR FORMULARIO
                            List<O_TIPOS_REGISTRO_CTY> objTipoReporte =
                                ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_REGISTRO(strCredencial, "CRFANH", "").
                                    ToList();
                            decIdTipoRegistro = objTipoReporte[0].ID_TIPO_REGISTRO;
                        }
                        catch (Exception)
                        {
                            decIdTipoRegistro = 0;
                        }

                        #endregion

                        #region Obtener id de los productos

                        decimal decIdGasResidual;
                        try
                        {
                            List<O_PRODUCTOS_CTY> objProducto =
                                ctx.PUSR_LISTADOS_P_LISTADO_PRODUCTOS("").ToList();

                            List<O_PRODUCTOS_CTY> objProductoAuxiliar = (from pr in objProducto
                                                                         where pr.DESCRIPCION.ToUpper().Contains("GAS")
                                                                               &&
                                                                               pr.DESCRIPCION.ToUpper().Contains(
                                                                                   "RESIDUAL")
                                                                         select pr).ToList();
                            decIdGasResidual = objProductoAuxiliar[0].ID_PRODUCTO;
                        }
                        catch (Exception)
                        {
                            decIdGasResidual = 0;
                        }

                        #endregion

                        #region Obtener la tabla de especificación

                        decimal decIdTablaEspec;
                        try
                        {
                            List<O_PRODUCTO_ESPEC_CTY> objTablaEspec =
                                ctx.PCAN_LISTADOS_P_LISTADO_TABLA_ESPEC_PRODUCTO(strCredencial,
                                                                                 decIdGasResidual,
                                                                                 "PRUEBA INCIAL DTEyP").
                                    ToList();
                            decIdTablaEspec = objTablaEspec[0].ID_TABLA_ESPEC;
                        }
                        catch (Exception)
                        {
                            decIdTablaEspec = 0;
                        }

                        #endregion

                        #region medios de transporte

                        decimal decIdCisterna;
                        decimal decIdDucto;
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
                            decIdCisterna = objProductoAuxiliar[0].ID_TIPO_MEDIO_TRANSPORTE;

                            objProductoAuxiliar = (from mt in objMedioTransporte
                                                   where mt.DESCRIPCION.ToUpper().Contains("DUCTO")
                                                   select mt).ToList();
                            decIdDucto = objProductoAuxiliar[0].ID_TIPO_MEDIO_TRANSPORTE;
                        }
                        catch (Exception)
                        {
                            decIdCisterna = decIdDucto = 0;
                        }

                        #endregion


                        if (decIdTipoOperacionResidual > 0 && decIdTipoOperacionPadre > 0 &&
                            decIdTipoReporte > 0 && decIdGasResidual > 0 &&
                            decIdTipoRegistro > 0 && decIdTablaEspec > 0 &&
                            decIdCisterna > 0 && decIdDucto > 0)
                        {
                            try
                            {
                                #region registrar cantidades PRODUCIDAS

                                resultado = ctx.PUSR_GESTION_P_REGISTRA_CANTIDADES(
                                    strCredencial,
                                    0, volumen,
                                    Convert.ToDecimal(fecha),
                                    decIdTipoRegistro, null,
                                    decIdGasResidual, umReslVolumen,
                                    planta, null,
                                    decIdTipoOperacionResidual,
                                    decIdTipoReporte,
                                    null,
                                    decAppIdUsuario, decAppFechaRegistro).ToList();

                                decimal decIdCantidad;
                                if (resultado[0].ID_TABLA > 0)
                                {
                                    decIdCantidad = resultado[0].ID_TABLA;
                                    RegistrarCantidadesCampo(strCredencial, 0, decIdCantidad, Convert.ToDecimal(fecha),
                                                             obs, just, decIdDucto, decAppFechaRegistro, decAppIdUsuario,
                                                             ref strMensajeError);
                                }
                                else
                                {
                                    decIdCantidad = 0;
                                }

                                #endregion

                                #region Registrar certificado de calidad (principal)

                                if (decIdCantidad > 0)
                                {
                                    List<O_RESULTADO_CTY> resultadoCertificado =
                                        ctx.PUSR_GESTION_P_REGISTRA_REPORTE_PRINCIPAL(
                                            strCredencial,
                                            Convert.ToDecimal(fecha),
                                            "DTEyP-" + decIdCantidad.ToString(),
                                            decIdCantidad, null, decIdDucto, null,
                                            decIdTablaEspec, null,
                                            decIdTipoRegistro, planta,
                                            null, null, null, null,
                                            decAppFechaRegistro,
                                            decAppIdUsuario).ToList();

                                    decimal decIdRegistroCalPrincipal = resultadoCertificado[0].ID_TABLA > 0
                                                                            ? resultadoCertificado[0].ID_TABLA
                                                                            : 0;

                                    #region registrar el detalle del certificado de calidad

                                    if (decIdRegistroCalPrincipal > 0)
                                    {
                                        var lstItems = new ArrayList();

                                        #region Completar información de las pruebas de calidad

                                        decimal decIdPruebaUnidad;
                                        decimal decIdPruebaCalidad;
                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "GRAVESP-GARE-PDTEP", "",
                                                               "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = gravedadEspecifica,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "H2O-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = h2O,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "PURO-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = puntoRocio,
                                                metodo = 0,
                                                unidad = umResPuntoRocio,
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "PODCAL-GARE-PDTEP", "",
                                                               "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = poderCalorifico,
                                                metodo = 0,
                                                unidad = umResPoderCalorifico,
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "N2-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = n2,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "CO2-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = co2,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C1-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = c1,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C2-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = c2,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C3-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = c3,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "IC4-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = iC4,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "NC4-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = nC4,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "IC5-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = iC5,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "NC5-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = nC5,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "NC6-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = nC6,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        ObtenerIdPruebaCalidad(strCredencial, decIdTablaEspec, "C7-GARE-PDTEP", "", "",
                                                               out decIdPruebaCalidad,
                                                               out decIdPruebaUnidad, ref strMensajeError);
                                        lstItems.Add(
                                            new
                                            {
                                                id = decIdPruebaCalidad,
                                                valor = c7,
                                                metodo = 0,
                                                unidad =
                                            ObtenerIdUnidadMedidaPruebaCalidad(strCredencial, decIdPruebaCalidad,
                                                                               ref strMensajeError),
                                                rango = 0
                                            });

                                        #endregion

                                        #region registrar el detalle de las pruebas de calidad

                                        string mensaje = "";
                                        foreach (var obj in lstItems)
                                        {
                                            object id = obj.GetType().GetProperty("id").GetValue(obj, null);
                                            object valor =
                                                obj.GetType().GetProperty("valor").GetValue(obj, null);
                                            string metodo =
                                                obj.GetType().GetProperty("metodo").GetValue(obj, null).ToString();
                                            object unidad =
                                                obj.GetType().GetProperty("unidad").GetValue(obj, null);
                                            object rango =
                                                obj.GetType().GetProperty("rango").GetValue(obj, null);

                                            List<O_RESULTADO_CTY> resultadoRegistroCalidad =
                                                ctx.PUSR_GESTION_P_REGISTRA_REPORTE_CALIDAD
                                                    (
                                                        strCredencial,
                                                        Convert.ToDecimal(valor),
                                                        "",
                                                        Convert.ToDecimal(rango),
                                                        metodo,
                                                        Convert.ToDecimal(id), decIdRegistroCalPrincipal,
                                                        Convert.ToDecimal(unidad), "",
                                                        Convert.ToDecimal(decAppFechaRegistro),
                                                        Convert.ToDecimal(decAppIdUsuario)).ToList();
                                            if (resultadoRegistroCalidad[0].ID_TABLA < 1 &&
                                                resultadoRegistroCalidad[0].MENSAJE_ERROR.Trim() != "OK")
                                                mensaje += resultadoRegistroCalidad[0].MENSAJE_ERROR;
                                        }
                                        strMensajeError += mensaje;

                                        #endregion
                                    }

                                    #endregion
                                }

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                strMensajeError =
                                    CMensajeError.FormatearMensajeDeError(
                                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                                        ex,
                                        TipoMensajeError);
                            }
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                }
                return resultado;
            }
        }

        #endregion
        
        #endregion                

        #region Método(s) privado(s)

        /// <summary>
        /// Método que verifica la unidad de medida de registro de volumenes.
        /// </summary>
        /// <param name="decUnidadMedida">Unidad de medida.</param>
        /// <param name="strMedioVerificacion">Medio de verificación de la unidad de medida.</param>
        /// <param name="pListUnidadesMedida">Lista de unidades de medida a ser verificadas.</param>
        /// <param name="vTipoUnidadMedida">Tipo de unidad de medida.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        private List<O_RESULTADO_CTY> VerificaUnidadMedida(decimal decUnidadMedida, string strMedioVerificacion, List<O_UNIDADES_MEDIDA_GRAL_CTY> pListUnidadesMedida, string vTipoUnidadMedida)
        {
            List<O_RESULTADO_CTY> resultado = null;

            string vMedioVerificacion = strMedioVerificacion;
            char[] splitchar = { ',' };
            string[] splitVolumen = vMedioVerificacion.Split(splitchar);
            int intCantidad = 0;
            foreach (var item in splitVolumen)
            {
                if (pListUnidadesMedida.Where(x => x.ID_UNIDAD_MEDIDA == decUnidadMedida).FirstOrDefault() != null)
                {
                    if (pListUnidadesMedida.Where(x => x.ID_UNIDAD_MEDIDA == decUnidadMedida).FirstOrDefault().CODIGO == item)
                        intCantidad++;
                }
                else
                    intCantidad = -1;
            }
            if (intCantidad == 0 || intCantidad == -1)
            {
                resultado = new List<O_RESULTADO_CTY>();
                O_RESULTADO_CTY vObjResultado = new O_RESULTADO_CTY();
                //vObjResultado.MENSAJE_ERROR = "El identificador de la unidad de medida no corresponde.";
                vObjResultado.RESULTADO = -1;
                resultado.Add(vObjResultado);
                if (intCantidad == 0)
                    vObjResultado.MENSAJE_ERROR = "El identificador de la unidad de medida para '" + vTipoUnidadMedida + "' no corresponde.";
                else
                    vObjResultado.MENSAJE_ERROR = "No existe la unidad de medida para: '" + vTipoUnidadMedida + "'.";
            }
            return resultado;
        }

        #endregion
    }
}
