using AnhPersistenciaCore.Aplicacion.Gestion;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using Newtonsoft.Json;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhServicioWebOctano.ServiciosWeb.Gestion
{
    public class SvcRegistroDtep : IService
    {
        #region Variables de Entorno

        private CPersistenciaGestion gRegistro;

        public class EResultado
        {
            public decimal decCodigo { get; set; }
            public string strMensaje { get; set; }
            public object oResultado { get; set; }
        }

        #endregion

        #region Constructor de la Clase
        public SvcRegistroDtep()
        {
            gRegistro = new CPersistenciaGestion();
        }
        #endregion

        #region Métodos y Funciones

        #region Certificados de calidad

        #region SW externo
        /// <summary>
        /// Método que realiza la inserción de la prueba de calidad para carburantes (Servicio Externo).
        /// </summary>
        /// <param name="request">Objeto para el registro de la prueba de calidad.</param>
        /// <returns>Retorna un objeto complejo O_REF_REG_REPORTE_PLANO_CTY con el codigo de error.</returns>
        public List<O_REF_REG_REPORTE_PLANO_CTY> POST(RegistrarPruebaCalidad request)
        {
            string strMensajeError = "";
            //List<O_REF_REG_REPORTE_PLANO_CTY> oResultado = gRegistro.InsertarPruebaCalidad(request.Llave, request.Version, request.CodigoProyecto, request.DatosReporte, request.IdOperador, request.EstadoRegistro, request.ObjetoLote, ref strMensajeError);
            List<O_REF_REG_REPORTE_PLANO_CTY> oResultado = gRegistro.RegistroPruebaCalidadExt(request.Llave, request.Version, request.DatosReporte, request.IdOperador, request.EstadoRegistro, request.ObjetoLote, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado[0].RESULTADO = -1;
                oResultado[0].MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }
        #endregion

        #region SW Interno
        /// <summary>
        /// Método que realiza la inserción de la prueba de calidad para carburantes.
        /// </summary>
        /// <param name="request">Objeto para el registro de la prueba de calidad de carburantes.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_REF_REG_REPORTE_PLANO_CTY POST(RegistraPruebasCalidad request)
        {
            string strMensajeError = "";
            O_REF_REG_REPORTE_PLANO_CTY oResultado = gRegistro.RegistraPruebasCalidad(request.strLlave, request.strEncabezado, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que realiza la eliminación del registro de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar la eliminación del registro de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(EliminaRegistroCalidad request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.EliminaRegistroCalidad(request.strLlave, request.strCiteGenerado, request.decAppFechaRegistro, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que realiza la actualización del registro de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar la actualización del registro de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_REF_REG_REPORTE_PLANO_CTY POST(ActualizaPruebasCalidad request)
        {
            string strMensajeError = "";
            O_REF_REG_REPORTE_PLANO_CTY oResultado = gRegistro.ActualizaPruebasCalidad(request.strLlave, request.strCiteGenerado, request.strEncabezado, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que retorna el código de dirección.
        /// </summary>
        /// <param name="request">Objeto para realizar el retorno de la dirección.</param>
        /// <returns></returns>
        public List<O_RESULTADO_NUMBER_CTY> POST(RetornaIdDireccion request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_NUMBER_CTY> oResultado = gRegistro.RetornaIdDireccion(request.decIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                if (oResultado == null)
                {
                    oResultado = new List<O_RESULTADO_NUMBER_CTY>();
                    O_RESULTADO_NUMBER_CTY oResultado1 = new O_RESULTADO_NUMBER_CTY();
                    oResultado.Add(oResultado1);
                }
                oResultado[0].RESULTADO = -1;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que realiza el registro de alertas sobre registro de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar la registro de alertas sobre el registro de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(RegistraAlertaCalidad request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.RegistraAlertaCalidad(
                request.strLlave, request.decIdTipoAlerta, request.decIdPruebaCalidad, request.strCiteGenerado, request.strObservaciones, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que realiza el registro de documento para cambios de registros de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar registro de documentos al registro de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(RegistraDocumento request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.RegistraDocumento(request.strLlave, request.decIdTipRespaldo, request.strCite, request.byteDocumento,request.strObservacion, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }
        
        /// <summary>
        /// Método que realiza la actualizacion de documento para cambios de registros de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar registro de documentos al registro de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(ActualizaDocumento request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.ActualizaDocumento(request.strLlave, request.decIdDocumento, request.byteDocumento, request.strObservacion, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que realiza la eliminacion de documento para cambios de registros de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar eliminacion de documentos al registro de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(EliminaDocumento request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.EliminaDocumento(request.strLlave, request.decIdDocumento, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }
        
        /// <summary>
        /// Método que realiza la eliminacion de documento para cambios de registros de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar eliminacion de documentos al registro de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(GestionAmpliacion request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.GestionAmpliacion(request.strLlave, request.decIdAmpliacion, request.decIdEntidad, request.decIdTipoActividad, request.decFechaOperacion, request.decFechaInicio, request.decFechaFin, request.strBarcode, request.strObservaciones, request.decAppIdUsuario, request.decAppFechaRegistro, request.decTipo, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que realiza la gestion de volumenes sin certificados de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar la gestion de volumenes sin certificados de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_REF_REG_REPORTE_PLANO_CTY POST(GestionVolumen request)
        {
            string strMensajeError = "";
            O_REF_REG_REPORTE_PLANO_CTY oResultado = gRegistro.GestionVolumen(request.strLlave, request.decIdPruebaCalidad, request.decIdEntidad, request.decIdTipoActividad, request.decFechaImportacion, request.decAppIdUsuario, request.decAccion, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que realiza la gestion de propietarios de certificados de calidad.
        /// </summary>
        /// <param name="request">Objeto para realizar la gestion propietarios de certificados de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(GestionPropietario request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.GestionPropietario(request.strLlave, request.decIdPropietarioPrueba, request.decIdPruebaCalidad, request.decIdEntidad, request.decIdTipoActividad, request.decAppIdUsuario, request.decAccion, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Método que realiza la gestion de usuarios.
        /// </summary>
        /// <param name="request">Objeto para realizar la gestion de usuarios para registros de certificados de calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(GestionUsuario request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.GestionUsuario(request.strLlave, request.decIdGestionOctano, request.decIdEntidad,request.decIdTipoActividad,request.strIpPermiso,request.strAplicacion,request.decFechaFin,request.strResponsable,request.strCorreoAnh,request.strSiglaOrganigrama,request.strObjetoUsuarioPruebas,request.decAppIdUsuario,request.decAccion, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        #endregion

        #endregion

        #region Movimiento de volumenes

        /// <summary>
        /// Registro de los volúmenes del gas de alimento.
        /// </summary>
        /// <param name="request">Objeto para el registro de los volúmenes del gas de alimento.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> POST(RegistrarCorrientes request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = null;

            #region Manejo de variables

            decimal vGravedadEspecifica = 0;
            decimal vVolumen = 0;
            decimal vUmVolumen = 0;
            decimal vContenidoLicuables = 0;
            decimal vUmContenidoLicuables = 0;
            decimal vPoderCalorifico = 0;
            decimal vUmPoderCalorifico = 0;
            decimal vN2 = 0;
            decimal vCO2 = 0;
            decimal vC1 = 0;
            decimal vC2 = 0;
            decimal vC3 = 0;
            decimal vI_C4 = 0;
            decimal vN_C4 = 0;
            decimal vI_C5 = 0;
            decimal vN_C5 = 0;
            decimal vN_C6 = 0;
            decimal vC7 = 0;

            int vCantidadRegistros = 0;
            foreach (var item in request.ListaCorrientes)
            {
                item.Concepto = !string.IsNullOrEmpty(item.Concepto) ? item.Concepto.Trim() : null;
                if (!string.IsNullOrEmpty(item.Concepto))
                {
                    switch (item.Concepto)
                    {
                        case Constantes.cGravedadEspecifica:
                            vGravedadEspecifica = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cVolumen:
                            vVolumen = item.Valor;
                            vUmVolumen = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cContenidoLicuables:
                            vContenidoLicuables = item.Valor;
                            vUmContenidoLicuables = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cPoderCalorifico:
                            vPoderCalorifico = item.Valor;
                            vUmPoderCalorifico = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN2:
                            vN2 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cCO2:
                            vCO2 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC1:
                            vC1 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC2:
                            vC2 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC3:
                            vC3 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cI_C4:
                            vI_C4 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN_C4:
                            vN_C4 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cI_C5:
                            vI_C5 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN_C5:
                            vN_C5 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN_C6:
                            vN_C6 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC7:
                            vC7 = item.Valor;
                            vCantidadRegistros++;
                            break;
                    }
                }
                else
                {
                    oResultado = RespuetaError(oResultado, "Ingrese el concepto del registro.");
                    return oResultado;
                }
            }

            #endregion
            if (vCantidadRegistros >= 15)
            {
                if (vUmVolumen > 0 && vUmContenidoLicuables > 0 && vUmPoderCalorifico > 0)
                {
                    oResultado = gRegistro.RegistrarVolumenesAlimentoGLP(request.Llave, request.Planta, request.CorrienteCampo, vUmVolumen, vUmContenidoLicuables, vUmPoderCalorifico, request.Fecha, vGravedadEspecifica, vVolumen, vContenidoLicuables, vPoderCalorifico, vN2, vCO2, vC1, vC2, vC3, vI_C4, vN_C4, vI_C5, vN_C5, vN_C6, vC7, request.Observaciones, request.Justificacion, request.IdUsuario, "DIARIO", ref strMensajeError);
                    if (!string.IsNullOrEmpty(strMensajeError))
                    {
                        oResultado[0].RESULTADO = -1;
                        oResultado[0].MENSAJE_ERROR = strMensajeError;
                    }
                }
                else
                    oResultado = RespuetaError(oResultado, "Ingrese la unidad de medida.");
            }
            else
                oResultado = RespuetaError(oResultado, "Alguno de los conceptos del registro no corresponden.");
            return oResultado;
        }

        /// <summary>
        /// Registro de volúmenes producidos.
        /// </summary>
        /// <param name="request">Objeto para el registro de volúmenes producidos.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> POST(RegistrarProduccion request)
        {

            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = null;

            #region Manejo de variables

            decimal vGravedadEspecifica = 0;
            decimal vUmProduccionGlp = 0;
            decimal vUmProduccionPropano = 0;
            decimal vUmEntradaConsumoPropano = 0;
            decimal vUmEntregaCisterna = 0;
            decimal vUmRendimientoProduccionGlp = 0;
            decimal vUmQuemaGas = 0;
            decimal vUmEntregaDucto = 0;
            decimal vUmSaldoGlp = 0;
            decimal vUmSaldoPropano = 0;
            decimal vUmTemperatura = 0;
            decimal vUmGasCombustible = 0;
            decimal vUmGasolinaNatural = 0;
            decimal vProduccionGlp = 0;
            decimal vProduccionPropano = 0;
            decimal vGasolinaNatural = 0;
            decimal vEntradaConsumoPropano = 0;
            decimal vEntregaCisterna = 0;
            decimal vEntradaDuctos = 0;
            decimal vSaldoGlp = 0;
            decimal vSaldoPropano = 0;
            decimal vRendimientoProduccionGlp = 0;
            decimal vGasCombustible = 0;
            decimal vQuemaGas = 0;
            decimal vTVR = 0;
            decimal vTemperatura = 0;
            decimal vC2 = 0;
            decimal vC3 = 0;
            decimal vI_C4 = 0;
            decimal vN_C4 = 0;
            decimal vI_C5 = 0;
            decimal vN_C5 = 0;

            int vCantidadRegistros = 0;
            foreach (var item in request.ListaProduccion)
            {
                item.Concepto = !string.IsNullOrEmpty(item.Concepto) ? item.Concepto.Trim() : null;
                if (!string.IsNullOrEmpty(item.Concepto))
                {
                    switch (item.Concepto)
                    {
                        case Constantes.cProduccionGlp:
                            vProduccionGlp = item.Valor;
                            vUmProduccionGlp = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cProduccionPropano:
                            vProduccionPropano = item.Valor;
                            vUmProduccionPropano = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cEntregaConsumoPropano:
                            vEntradaConsumoPropano = item.Valor;
                            vUmEntradaConsumoPropano = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cEntregaGlpCisterna:
                            vEntregaCisterna = item.Valor;
                            vUmEntregaCisterna = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cEntregaGlpDucto:
                            vEntradaDuctos = item.Valor;
                            vUmEntregaDucto = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cSaldoGlp:
                            vSaldoGlp = item.Valor;
                            vUmSaldoGlp = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cSaldoPropano:
                            vSaldoPropano = item.Valor;
                            vUmSaldoPropano = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cTemperatura:
                            vTemperatura = item.Valor;
                            vUmTemperatura = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cRendimientoProduccionGlp:
                            vRendimientoProduccionGlp = item.Valor;
                            vUmRendimientoProduccionGlp = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cGasCombustible:
                            vGasCombustible = item.Valor;
                            vUmGasCombustible = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cQuemaGas:
                            vQuemaGas = item.Valor;
                            vUmQuemaGas = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cGasolinaNatural:
                            vGasolinaNatural = item.Valor;
                            vUmGasolinaNatural = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cGravedadEspecifica:
                            vGravedadEspecifica = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cTVR:
                            vTVR = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC2:
                            vC2 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC3:
                            vC3 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cI_C4:
                            vI_C4 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN_C4:
                            vN_C4 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cI_C5:
                            vI_C5 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN_C5:
                            vN_C5 = item.Valor;
                            vCantidadRegistros++;
                            break;
                    }
                }
                else
                {
                    oResultado = RespuetaError(oResultado, "Ingrese el concepto del registro.");
                    return oResultado;
                }
            }

            #endregion
            if (vCantidadRegistros >= 20)
            {
                if (vUmProduccionGlp > 0 && vUmProduccionPropano > 0 && vUmEntradaConsumoPropano > 0 && vUmEntregaCisterna > 0 && vUmRendimientoProduccionGlp > 0 && vUmQuemaGas > 0 && vUmEntregaDucto > 0 && vUmSaldoGlp > 0 && vUmSaldoPropano > 0 && vUmTemperatura > 0 && vUmGasCombustible > 0 && vUmGasolinaNatural > 0)
                {
                    oResultado = gRegistro.RegistrarVolumenesProduccionGLP(request.Llave, request.Planta, vUmProduccionGlp, vUmProduccionPropano, vUmEntradaConsumoPropano, vUmEntregaCisterna, vUmEntregaDucto, vUmSaldoGlp, vUmSaldoPropano, vUmTemperatura, request.Fecha, vProduccionGlp, vProduccionPropano, vEntradaConsumoPropano, vEntregaCisterna, vEntradaDuctos, vSaldoGlp, vSaldoPropano, vGravedadEspecifica, vTVR, vTemperatura, vC2, vC3, vI_C4, vN_C4, vI_C5, vN_C5, request.Observaciones, request.Justificacion, request.IdUsuario, "DIARIO", vRendimientoProduccionGlp, vGasCombustible, vQuemaGas, vGasolinaNatural, vUmRendimientoProduccionGlp, vUmGasCombustible, vUmQuemaGas, vUmGasolinaNatural, ref strMensajeError);
                    if (!string.IsNullOrEmpty(strMensajeError))
                    {
                        oResultado[0].RESULTADO = -1;
                        oResultado[0].MENSAJE_ERROR = strMensajeError;
                    }
                }
                else
                    oResultado = RespuetaError(oResultado, "Ingrese la unidad de medida.");
            }
            else
                oResultado = RespuetaError(oResultado, "Alguno de los conceptos del registro no corresponden.");
            return oResultado;
        }

        /// <summary>
        /// Registro de volúmenes de gas residual.
        /// </summary>
        /// <param name="request">Objeto para el registro de volúmenes de gas residual.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> POST(RegistrarGasResidual request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = null;

            #region Manejo de variables

            decimal vGravedadEspecifica = 0;
            decimal vVolumen = 0;
            decimal vUmVolumen = 0;
            decimal vPoderCalorifico = 0;
            decimal vUmPoderCalorifico = 0;
            decimal vUmPuntoRocio = 0;
            decimal vN2 = 0;
            decimal vH2O = 0;
            decimal vPuntoRocio = 0;
            decimal vCO2 = 0;
            decimal vC1 = 0;
            decimal vC2 = 0;
            decimal vC3 = 0;
            decimal vI_C4 = 0;
            decimal vN_C4 = 0;
            decimal vI_C5 = 0;
            decimal vN_C5 = 0;
            decimal vN_C6 = 0;
            decimal vC7 = 0;

            int vCantidadRegistros = 0;
            foreach (var item in request.ListaResidual)
            {
                item.Concepto = !string.IsNullOrEmpty(item.Concepto) ? item.Concepto.Trim() : null;
                if (!string.IsNullOrEmpty(item.Concepto))
                {
                    switch (item.Concepto)
                    {
                        case Constantes.cGravedadEspecifica:
                            vGravedadEspecifica = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cVolumen:
                            vVolumen = item.Valor;
                            vUmVolumen = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cH20:
                            vH2O = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cPuntoRocio:
                            vPuntoRocio = item.Valor;
                            vUmPuntoRocio = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cPoderCalorifico:
                            vPoderCalorifico = item.Valor;
                            vUmPoderCalorifico = item.UnidadMedida;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN2:
                            vN2 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cCO2:
                            vCO2 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC1:
                            vC1 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC2:
                            vC2 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC3:
                            vC3 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cI_C4:
                            vI_C4 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN_C4:
                            vN_C4 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cI_C5:
                            vI_C5 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN_C5:
                            vN_C5 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cN_C6:
                            vN_C6 = item.Valor;
                            vCantidadRegistros++;
                            break;
                        case Constantes.cC7:
                            vC7 = item.Valor;
                            vCantidadRegistros++;
                            break;
                    }
                }
                else
                {
                    oResultado = RespuetaError(oResultado, "Ingrese el concepto del registro.");
                    return oResultado;
                }
            }

            #endregion
            if (vCantidadRegistros >= 16)
            {
                if (vUmVolumen > 0 && vUmPuntoRocio > 0 && vUmPoderCalorifico > 0)
                {
                    oResultado = gRegistro.RegistrarVolumenesResidualGLP(request.Llave, request.Planta, vUmVolumen, vUmPuntoRocio, vUmPoderCalorifico, request.Fecha, vGravedadEspecifica, vVolumen, vH2O, vPuntoRocio, vPoderCalorifico, vN2, vCO2, vC1, vC2, vC3, vI_C4, vN_C4, vI_C5, vN_C5, vN_C6, vC7, request.Observaciones, request.Justificacion, request.IdUsuario, "DIARIO", ref strMensajeError);
                    if (!string.IsNullOrEmpty(strMensajeError))
                    {
                        oResultado[0].RESULTADO = -1;
                        oResultado[0].MENSAJE_ERROR = strMensajeError;
                    }
                }
                else
                    oResultado = RespuetaError(oResultado, "Ingrese la unidad de medida.");
            }
            else
                oResultado = RespuetaError(oResultado, "Alguno de los conceptos del registro no corresponden.");
            return oResultado;
        }

        /// <summary>
        /// Eliminación del Prode.
        /// </summary>
        /// <param name="request">Objeto para la eliminación del Prode.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY.</returns>
        public List<O_RESULTADO_CTY> POST(EliminarProde request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = gRegistro.EliminarProde(request.strLlave, request.decIdProde, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado[0].RESULTADO = -1;
                oResultado[0].MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Registro del Prode.
        /// </summary>
        /// <param name="request">Objeto para el registro del Prode.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY.</returns>
        public List<O_RESULTADO_CTY> POST(RegistrarProde request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = gRegistro.RegistrarProde(request.strLlave, request.decIdProde, request.decFechaProde, request.decValorProde, request.decIdEntidad, request.decIdCampo, request.decIdUnidadMedida, request.decIdTipoReporte, request.decIdProducto, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado[0].RESULTADO = -1;
                oResultado[0].MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Registro Campos Entidad.
        /// </summary>
        /// <param name="request">Objeto para el registro de la correspondencia de campo con una entidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY.</returns>
        public List<O_RESULTADO_CTY> POST(RegistrarCamposEntidad request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = gRegistro.RegistrarCamposEntidad(
                request.strCredencial, request.decIdCamposEntidad, request.decIdCampo, request.decIdEntidad, request.decAppFechaRegistro, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado[0].RESULTADO = -1;
                oResultado[0].MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// Eliminar Campos Entidad.
        /// </summary>
        /// <param name="request">Objeto para la eliminacion de la relacion campo entidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY.</returns>
        public List<O_RESULTADO_CTY> POST(EliminarCampoEntidad request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = gRegistro.EliminarCampoEntidad(
                request.strCredencial, request.decIdCamposEntidad, request.decIdCampo, request.decIdEntidad,
                request.decAppIdUsuario, request.decAppFechaRegistro,
                ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado[0].RESULTADO = -1;
                oResultado[0].MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// EliminarCampos.
        /// </summary>
        /// <param name="request">Objeto para la eliminacion de campos.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY.</returns>
        public List<O_RESULTADO_CTY> POST(EliminarCampos request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = gRegistro.EliminarCampos(
                request.strCredencial, request.decIdCampo, request.decAppIdUsuario, request.decAppFechaRegistro, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado[0].RESULTADO = -1;
                oResultado[0].MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        /// RegistrarCampos.
        /// </summary>
        /// <param name="request">Objeto para registrar campos.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY.</returns>
        public List<O_RESULTADO_CTY> POST(RegistrarCampos request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> oResultado = gRegistro.RegistrarCampos(
                request.strCredencial, request.decIdCampo, request.strNombreCampo, request.decAppFechaRegistro, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado[0].RESULTADO = -1;
                oResultado[0].MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }
        #endregion

        #region Parametricas
        /// <summary>
        ///  Método que realiza el registro de parametros.
        /// </summary>
        /// <param name="request">Objeto para realizar la registro de parametros para Octano Calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(RegistraParametrica request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.RegistraParametro(request.strLlave, request.strTipo, request.strCodigo, request.strDescripcion, request.strValor, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        ///  Método que realiza la actualizacion de parametros.
        /// </summary>
        /// <param name="request">Objeto para realizar la actualizacion de parametros para Octano Calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(ActualizaParametrica request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.ActualizaParametro(request.strLlave, request.decIdParametrica, request.strTipo, request.strCodigo, request.strDescripcion, request.strValor, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        ///  Método que realiza el registro de parametros.
        /// </summary>
        /// <param name="request">Objeto para realizar la eliminacion de parametros para Octano Calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(EliminaParametrica request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.EliminaParametro(request.strLlave, request.decIdParametrica, request.strTipo, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        ///  Método que realiza el registro de parametros.
        /// </summary>
        /// <param name="request">Objeto para realizar la registro de parametros para Octano Calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(RegistraParametroNpc request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.RegistraParametroNpc(request.strLlave, request.decIdEntidad, request.decIdTablaEspec, request.strCodigo, request.strNombre, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        ///  Método que realiza el actualizacion de parametros de nombre producto.
        /// </summary>
        /// <param name="request">Objeto para realizar la registro de parametros para Octano Calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(ActualizaParametroNpc request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.ActualizaParametroNpc(request.strLlave, request.decIdNombreProducto, request.decIdEntidad, request.decIdTablaEspec, request.strCodigo, request.strNombre, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        ///  Método que realiza la eliminacion de parametros de nombre producto.
        /// </summary>
        /// <param name="request">Objeto para realizar la registro de parametros para Octano Calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(EliminaParametroNpc request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.EliminaParametroNpc(request.strLlave, request.decIdNombreProducto, request.decAppIdUsuario, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        /// <summary>
        ///  Método que realiza la gestion de configuracion de validaciones por actividad.
        /// </summary>
        /// <param name="request">Objeto para realizar la gestion de configuracion de validaciones por actividad para Octano Calidad.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public O_RESULTADO_CTY POST(GestionConfiguracion request)
        {
            string strMensajeError = "";
            O_RESULTADO_CTY oResultado = gRegistro.GestionConfiguracion(request.strLlave, request.decIdConfiguracion, request.decIdTipoActividad, request.strValidacion, request.decAppIdUsuario, request.decAccion, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado.RESULTADO = -1;
                oResultado.MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }

        #endregion

        #endregion

        #region Declaracion de Servicios

        #region Certificados de calidad

        #region SW Externo
        [Route("/RegistrarPruebaCalidad", "POST")]
        public class RegistrarPruebaCalidad : IReturn<O_REF_REG_REPORTE_PLANO_CTY>//IReturn<EResultado>
        {
            public string Llave { get; set; }
            public decimal Version { get; set; }
            public ObjetoLote ObjetoLote { get; set; }
            //public decimal CodigoProyecto { get; set; }

            #region Valores auxiliares del sercicio externo

            public string DatosReporte { get; set; }
            public decimal IdOperador { get; set; }
            public decimal EstadoRegistro { get; set; }

            #endregion
        }
        #endregion

        #region SW interno
        [Route("/RegistraPruebasCalidad", "POST")]
        public class RegistraPruebasCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strEncabezado { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        [Route("/EliminaRegistroCalidad", "POST")]
        public class EliminaRegistroCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCiteGenerado { get; set; }
            public decimal decAppFechaRegistro { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        [Route("/ActualizaPruebasCalidad", "POST")]
        public class ActualizaPruebasCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCiteGenerado { get; set; }
            public string strEncabezado { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }


        [Route("/RetornaIdDireccion", "POST")]
        public class RetornaIdDireccion : IReturn<EResultado>
        {
            public decimal decIdUsuario { get; set; }
        }

        [Route("/RegistraAlertaCalidad", "POST")]
        public class RegistraAlertaCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdTipoAlerta { get; set; }
            public decimal decIdPruebaCalidad { get; set; }
            public string strCiteGenerado { get; set; }
            public string strObservaciones { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        [Route("/RegistraDocumento", "POST")]
        public class RegistraDocumento : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdTipRespaldo { get; set; }
            public string strCite { get; set; }
            public byte[] byteDocumento { get; set; }
            public string strObservacion { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }
        
        [Route("/EliminaDocumento", "POST")]
        public class EliminaDocumento : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdDocumento { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        [Route("/GestionAmpliacion", "POST")]
        public class GestionAmpliacion : IReturn<EResultado>
        {
            public string strLlave { get; set; } 
            public decimal decIdAmpliacion { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decFechaOperacion { get; set; }
            public decimal decFechaInicio { get; set; }
            public decimal decFechaFin { get; set; }
            public string strBarcode { get; set; }
            public string strObservaciones { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
            public decimal decTipo { get; set; }
        }
        
        [Route("/GestionVolumen", "POST")]
        public class GestionVolumen : IReturn<O_REF_REG_REPORTE_PLANO_CTY>
        {
            public string strLlave { get; set; } 
            public decimal decIdPruebaCalidad { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decFechaImportacion { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAccion { get; set; }
        }

        [Route("/GestionPropietario", "POST")]
        public class GestionPropietario : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdPropietarioPrueba { get; set; }
            public decimal decIdPruebaCalidad { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAccion { get; set; }
        }

        [Route("/ActualizaDocumento", "POST")]
        public class ActualizaDocumento : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdDocumento { get; set; }
            public byte[] byteDocumento { get; set; }
            public string strObservacion { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        [Route("/GestionUsuario", "POST")]
        public class GestionUsuario : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdGestionOctano  { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public string strIpPermiso { get; set; }
            public string strAplicacion { get; set; }
            public decimal decFechaFin { get; set; }
            public string strResponsable { get; set; }
            public string strCorreoAnh { get; set; }
            public string strSiglaOrganigrama { get; set; }
            public string strObjetoUsuarioPruebas { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAccion { get; set; }
        }

        #endregion

        #endregion

        #region Movimiento de volumenes

        #region Registrar corrientes

        [Route("/RegistrarCorrientes", "POST")]
        public class RegistrarCorrientes : IReturn<EResultado>
        {
            public string Llave { get; set; }
            public int Version { get; set; }
            public int CodigoProyecto { get; set; }
            public List<ObjetoDetalle> ListaCorrientes { get; set; }
            public decimal CorrienteCampo { get; set; }
            public decimal Planta { get; set; }
            public string Fecha { get; set; }
            public string Justificacion { get; set; }
            public string Observaciones { get; set; }
            #region Valores auxiliares del sercicio externo
            public decimal IdUsuario { get; set; }
            #endregion
        }

        #endregion

        #region RegistrarProduccion

        [Route("/RegistrarProduccion", "POST")]
        public class RegistrarProduccion : IReturn<EResultado>
        {
            public string Llave { get; set; }
            public int Version { get; set; }
            public int CodigoProyecto { get; set; }
            public List<ObjetoDetalle> ListaProduccion { get; set; }
            public decimal Planta { get; set; }
            public string Fecha { get; set; }
            public string Justificacion { get; set; }
            public string Observaciones { get; set; }
            #region Valores auxiliares del sercicio externo
            public decimal IdUsuario { get; set; }
            #endregion
        }

        #endregion

        #region RegistrarGasResidual

        [Route("/RegistrarGasResidual", "POST")]
        public class RegistrarGasResidual : IReturn<EResultado>
        {
            public string Llave { get; set; }
            public int Version { get; set; }
            public int CodigoProyecto { get; set; }
            public List<ObjetoDetalle> ListaResidual { get; set; }
            public decimal Planta { get; set; }
            public string Fecha { get; set; }
            public string Justificacion { get; set; }
            public string Observaciones { get; set; }
            #region Valores auxiliares del sercicio externo
            public decimal IdUsuario { get; set; }
            #endregion
        }

        #endregion

        [Route("/EliminarProde", "POST")]
        public class EliminarProde : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdProde { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        [Route("/RegistrarProde", "POST")]
        public class RegistrarProde : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdProde { get; set; }
            public decimal decFechaProde { get; set; }
            public decimal decValorProde { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdCampo { get; set; }
            public decimal decIdUnidadMedida { get; set; }
            public decimal decIdTipoReporte { get; set; }
            public decimal decIdProducto { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        public class ObjetoDetalle : IReturn<EResultado>
        {
            public decimal UnidadMedida { get; set; }
            public decimal Valor { get; set; }
            public string Concepto { get; set; }
        }

        #region RegistrarCamposEntidad
        [Route("/RegistrarCamposEntidad", "POST")]
        public class RegistrarCamposEntidad : IReturn<EResultado>
        {
            public string strCredencial { get; set; }
            public decimal decIdCamposEntidad { get; set; }
            public decimal decIdCampo { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decAppFechaRegistro { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }
        #endregion

        #region RegistrarCamposEntidad
        [Route("/EliminarCampoEntidad", "POST")]
        public class EliminarCampoEntidad : IReturn<EResultado>
        {
            public string strCredencial { get; set; }
            public decimal decIdCamposEntidad { get; set; }
            public decimal decIdCampo { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decAppFechaRegistro { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }
        #endregion

        #region EliminarCampos
        [Route("/EliminarCampos", "POST")]
        public class EliminarCampos : IReturn<EResultado>
        {
            public string strCredencial { get; set; }
            public decimal decIdCampo { get; set; }
            public decimal decAppFechaRegistro { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        #endregion

        #region RegistrarCampos
        [Route("/RegistrarCampos", "POST")]
        public class RegistrarCampos : IReturn<EResultado>
        {
            public string strCredencial { get; set; }
            public decimal decIdCampo { get; set; }
            public string strNombreCampo { get; set; }
            public decimal decAppFechaRegistro { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }
        #endregion
        #endregion

        #region RegistraParametros
        [Route("/RegistraParametrica", "POST")]
        public class RegistraParametrica : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strTipo { get; set; }
            public string strCodigo { get; set; }
            public string strDescripcion { get; set; }
            public string strValor { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        [Route("/ActualizaParametrica", "POST")]
        public class ActualizaParametrica : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdParametrica { get; set; }
            public string strTipo { get; set; }
            public string strCodigo { get; set; }
            public string strDescripcion { get; set; }
            public string strValor { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        [Route("/EliminaParametrica", "POST")]
        public class EliminaParametrica : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdParametrica { get; set; }
            public string strTipo { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        [Route("/RegistraParametroNpc", "POST")]
        public class RegistraParametroNpc : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTablaEspec { get; set; }
            public string strCodigo { get; set; }
            public string strNombre { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        [Route("/ActualizaParametroNpc", "POST")]
        public class ActualizaParametroNpc : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdNombreProducto { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTablaEspec { get; set; }
            public string strCodigo { get; set; }
            public string strNombre { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        [Route("/EliminaParametroNpc", "POST")]
        public class EliminaParametroNpc : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdNombreProducto { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        [Route("/GestionConfiguracion", "POST")]
        public class GestionConfiguracion : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdConfiguracion { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public string strValidacion { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAccion { get; set; }
        }
        
        #endregion
        
        #endregion

        #region Método(s) privado(s)

        /// <summary>
        /// Método que devuelve la respuesta del error en el caso de que existiese.
        /// </summary>
        /// <param name="vResultado">Objeto de tipo resultado.</param>
        /// <param name="pMensaje">Mensaje del Error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        private List<O_RESULTADO_CTY> RespuetaError(List<O_RESULTADO_CTY> vResultado, string pMensaje)
        {
            O_RESULTADO_CTY vObjResultado = new O_RESULTADO_CTY();
            vObjResultado.RESULTADO = -1;
            vObjResultado.MENSAJE_ERROR = pMensaje;
            vResultado = new List<O_RESULTADO_CTY>();
            vResultado.Add(vObjResultado);
            return vResultado;
        }

        #endregion
    }
}