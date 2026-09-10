using System;
using AnhServicioWebOctano.ServiciosWeb.Gestion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnhPersistenciaCore.Entidades;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using ServiceStack.ServiceClient.Web;
using Newtonsoft.Json;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Aplicacion.Listados;
using System.Diagnostics;

namespace AnhPruebaUnitaria
{
    [TestClass]
    public class TestGestion
    {
        private string strMensajeError = string.Empty;

        #region Declaración del objeto

        public class RegistrarPruebaCalidad : IReturn<EResultado>
        {
            public string Llave { get; set; }
            public decimal Version { get; set; }
            public ObjetoLote ObjetoLote { get; set; }
            public decimal CodigoProyecto { get; set; }
            #region Valores auxiliares del sercicio externo
            public string DatosReporte { get; set; }
            public decimal IdOperador { get; set; }
            public decimal EstadoRegistro { get; set; }
            #endregion
        }

        public class RegistrarCorrientes : IReturn<EResultado>
        {
            public string Llave { get; set; }
            public decimal Version { get; set; }
            public decimal CodigoProyecto { get; set; }
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
        public class ObjetoDetalle : IReturn<EResultado>
        {
            public decimal UnidadMedida { get; set; }
            public decimal Valor { get; set; }
            public string Concepto { get; set; }
        }

        public class RegistrarProduccion : IReturn<EResultado>
        {
            public string Llave { get; set; }
            public decimal Version { get; set; }
            public decimal CodigoProyecto { get; set; }
            public List<ObjetoDetalle> ListaProduccion { get; set; }
            public decimal Planta { get; set; }
            public string Fecha { get; set; }
            public string Justificacion { get; set; }
            public string Observaciones { get; set; }
            #region Valores auxiliares del sercicio externo
            public decimal IdUsuario { get; set; }
            #endregion
        }

        public class RegistrarGasResidual : IReturn<EResultado>
        {
            public string Llave { get; set; }
            public decimal Version { get; set; }
            public decimal CodigoProyecto { get; set; }
            public List<ObjetoDetalle> ListaResidual { get; set; }
            public decimal Planta { get; set; }
            public string Fecha { get; set; }
            public string Justificacion { get; set; }
            public string Observaciones { get; set; }
            #region Valores auxiliares del sercicio externo
            public decimal IdUsuario { get; set; }
            #endregion
        }

        public class RegistraPruebasCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strEncabezado { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        public class EliminaRegistroCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCiteGenerado { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        public class ActualizaPruebasCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCiteGenerado { get; set; }
            public string strEncabezado { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        public class ObjetoCalidadActualiza
        {
            public int valor_numerico { get; set; }
            public string valor_alfanumerico { get; set; }
            public int valor_referencial { get; set; }
            public int id_metodo_astm { get; set; }
            public int id_unidad_medida { get; set; }
            public int id_prueba_calidad { get; set; }
            public string justificacion { get; set; }
        }

        public class PruebaCalidadActualiza
        {
            public string valor_lote { get; set; }
            public int id_entidad { get; set; }
            public int id_producto { get; set; }
            public int id_usuario { get; set; }
            public int id_tabla_espec { get; set; }
            public int op_debe { get; set; }
            public int ope_haber { get; set; }
            public long fecha_operacion { get; set; }
            public int id_tipo_registro { get; set; }
            public int id_cantidad_padre { get; set; }
            public int id_unidad_medida { get; set; }
            public int id_entidad_origen { get; set; }
            public int id_entidad_destino { get; set; }
            public int id_tipo_operacion { get; set; }
            public int id_tipo_reporte { get; set; }
            public int id_volumen_datos { get; set; }
            public int id_tanque_alm { get; set; }
            public int id_medio_transporte { get; set; }
            public int id_registro_padre { get; set; }
            public int id_tipo_muestra { get; set; }
            public int fecha_muestra { get; set; }
            public string procedencia { get; set; }
            public string observaciones { get; set; }
            public int valida_cerrado { get; set; }
            public List<ObjetoCalidadActualiza> pruebas_calidad { get; set; }
        }

        public class RetornaIdDireccion : IReturn<EResultado>
        {
            public decimal decIdUsuario { get; set; }
        }

        #endregion

        #region Prueba de Registro de Calidad

        #region Servicio Externo

        #region Diesel Oil

        [TestMethod]
        public void TestRegistrarPruebaCalidadDieselOil()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 148;
            vObjLoteDetalle.valor_reportado = "0.33";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 1;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 150;
            vObjLoteDetalle.valor_reportado = "0.33";
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vObjLoteDetalle.valor_metodo_astm = 22;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 151;
            vObjLoteDetalle.valor_reportado = "30";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 103;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 152;
            vObjLoteDetalle.valor_reportado = "333";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 63;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 153;
            vObjLoteDetalle.valor_reportado = "0003 CRISTALINA";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 27;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 154;
            vObjLoteDetalle.valor_reportado = "3.33";
            vObjLoteDetalle.codigo_unidad_medida = 32;
            vObjLoteDetalle.valor_metodo_astm = 40;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 155;
            vObjLoteDetalle.valor_reportado = "33";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 104;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 156;
            vObjLoteDetalle.valor_reportado = "33";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 126;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 157;
            vObjLoteDetalle.valor_reportado = "3.33";
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vObjLoteDetalle.valor_metodo_astm = 107;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 158;
            vObjLoteDetalle.valor_reportado = "0.300";
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vObjLoteDetalle.valor_metodo_astm = 110;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 159;
            vObjLoteDetalle.valor_reportado = "0.33";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 111;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 161;
            vObjLoteDetalle.valor_reportado = "333";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 162;
            vObjLoteDetalle.valor_reportado = "30003";
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vObjLoteDetalle.valor_metodo_astm = 28;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 163;
            vObjLoteDetalle.valor_reportado = "0003 3.0";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 43;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 164;
            vObjLoteDetalle.valor_reportado = "33.33";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 30;
            vColLoteDetalle.Add(vObjLoteDetalle);

            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 130;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 292;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = 33;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = 2;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = "06800910847 3";
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = 2;
            objPruebaEntidad.ObjetoLote.volumen = 3000;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 68;
            objPruebaEntidad.ObjetoLote.numero_lote = "890000025893";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 103;
            objPruebaEntidad.EstadoRegistro = 1;

            //string vObjJasonEncabezado = null;
            //vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        [TestMethod]
        public void TestRegistrarPruebaCalidadDieselOil_TanqueTransporteNoDefinido()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 148;
            vObjLoteDetalle.valor_reportado = "0.33";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 1;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 150;
            vObjLoteDetalle.valor_reportado = "0.33";
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vObjLoteDetalle.valor_metodo_astm = 22;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 151;
            vObjLoteDetalle.valor_reportado = "30";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 103;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 152;
            vObjLoteDetalle.valor_reportado = "333";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 63;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 153;
            vObjLoteDetalle.valor_reportado = "0003 CRISTALINA";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 27;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 154;
            vObjLoteDetalle.valor_reportado = "3.33";
            vObjLoteDetalle.codigo_unidad_medida = 32;
            vObjLoteDetalle.valor_metodo_astm = 40;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 155;
            vObjLoteDetalle.valor_reportado = "33";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 104;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 156;
            vObjLoteDetalle.valor_reportado = "33";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 126;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 157;
            vObjLoteDetalle.valor_reportado = "3.33";
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vObjLoteDetalle.valor_metodo_astm = 107;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 158;
            vObjLoteDetalle.valor_reportado = "0.300";
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vObjLoteDetalle.valor_metodo_astm = 110;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 159;
            vObjLoteDetalle.valor_reportado = "0.33";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 111;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 161;
            vObjLoteDetalle.valor_reportado = "333";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 162;
            vObjLoteDetalle.valor_reportado = "30003";
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vObjLoteDetalle.valor_metodo_astm = 28;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 163;
            vObjLoteDetalle.valor_reportado = "0003 3.0";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 43;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 164;
            vObjLoteDetalle.valor_reportado = "33.33";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 30;
            vColLoteDetalle.Add(vObjLoteDetalle);

            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 130;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 3000;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 68;
            objPruebaEntidad.ObjetoLote.numero_lote = "890000025893";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 119;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        #endregion

        #region Gas Licuado de Petroleo

        [TestMethod]
        public void TestRegistrarPruebaCalidadConCorreo()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 1;
            vObjLoteDetalle.valor_reportado = "1.0000";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 4;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 2;
            vObjLoteDetalle.valor_reportado = "85.0000";
            vObjLoteDetalle.codigo_unidad_medida = 16;
            vObjLoteDetalle.valor_metodo_astm = 5;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 3;
            vObjLoteDetalle.valor_reportado = "11.0000";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 6;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 5;
            vObjLoteDetalle.valor_reportado = "1.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 6;
            vObjLoteDetalle.valor_reportado = "0.0020";
            vObjLoteDetalle.codigo_unidad_medida = 20;
            vObjLoteDetalle.valor_metodo_astm = 8;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 7;
            vObjLoteDetalle.valor_reportado = "0.50000";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 9;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 8;
            vObjLoteDetalle.valor_reportado = "100.0000";
            vObjLoteDetalle.codigo_unidad_medida = 21;
            vObjLoteDetalle.valor_metodo_astm = 10;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 9;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 11;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 10;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vObjLoteDetalle.valor_metodo_astm = 12;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 15;
            vObjLoteDetalle.valor_reportado = "2.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20151028103000;//
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20151028122500;//
            objPruebaEntidad.ObjetoLote.codigo_producto = 125;//
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 284;//
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 13095;//
            //objPruebaEntidad.ObjetoLote.codigo_transporte = 33;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = 2;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = "06800910847 3";
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = 2;
            objPruebaEntidad.ObjetoLote.volumen = 123;//
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;//
            objPruebaEntidad.ObjetoLote.numero_lote = "prueba rest";//
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "0DDC53A2CDB4328739682232A6C7F3964F9D4";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 1783;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        [TestMethod]
        public void TestRegistrarPruebaCalidadGasLicuadoPetroleo()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 1;
            vObjLoteDetalle.valor_reportado = "0.5300";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 4;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 2;
            vObjLoteDetalle.valor_reportado = "85.0000";
            vObjLoteDetalle.codigo_unidad_medida = 16;
            vObjLoteDetalle.valor_metodo_astm = 5;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 3;
            vObjLoteDetalle.valor_reportado = "11.0000";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 6;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 5;
            vObjLoteDetalle.valor_reportado = "1.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 6;
            vObjLoteDetalle.valor_reportado = "0.0020";
            vObjLoteDetalle.codigo_unidad_medida = 20;
            vObjLoteDetalle.valor_metodo_astm = 8;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 7;
            vObjLoteDetalle.valor_reportado = "0.50000";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 9;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 8;
            vObjLoteDetalle.valor_reportado = "100.0000";
            vObjLoteDetalle.codigo_unidad_medida = 21;
            vObjLoteDetalle.valor_metodo_astm = 10;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 9;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 11;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 10;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vObjLoteDetalle.valor_metodo_astm = 12;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 15;
            vObjLoteDetalle.valor_reportado = "2.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150518103000;//
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150518122500;//
            objPruebaEntidad.ObjetoLote.codigo_producto = 125;//
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 284;//
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 292;//
            //objPruebaEntidad.ObjetoLote.codigo_transporte = 33;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = 2;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = "06800910847 3";
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = 2;
            objPruebaEntidad.ObjetoLote.volumen = 123;//
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;//
            objPruebaEntidad.ObjetoLote.numero_lote = "medio dia";//
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 1041;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        [TestMethod]
        public void TestRegistrarPruebaCalidadGasLicuadoPetroleo_TanqueTransporteNoDefinido()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 1;
            vObjLoteDetalle.valor_reportado = "0.5300";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 4;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 2;
            vObjLoteDetalle.valor_reportado = "85.0000";
            vObjLoteDetalle.codigo_unidad_medida = 16;
            vObjLoteDetalle.valor_metodo_astm = 5;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 3;
            vObjLoteDetalle.valor_reportado = "11.0000";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 6;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 5;
            vObjLoteDetalle.valor_reportado = "1.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 6;
            vObjLoteDetalle.valor_reportado = "0.0020";
            vObjLoteDetalle.codigo_unidad_medida = 20;
            vObjLoteDetalle.valor_metodo_astm = 8;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 7;
            vObjLoteDetalle.valor_reportado = "0.50000";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 9;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 8;
            vObjLoteDetalle.valor_reportado = "100.0000";
            vObjLoteDetalle.codigo_unidad_medida = 21;
            vObjLoteDetalle.valor_metodo_astm = 10;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 9;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 11;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 10;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vObjLoteDetalle.valor_metodo_astm = 12;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 15;
            vObjLoteDetalle.valor_reportado = "2.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20130518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20130518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 125;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 21; // MARIO.CIANCAGLINI,GIOVANNI.GUZMAN@PETROBRAS.COM
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 27; //CARGUIO.CRC@YPFBCHACO.COM.BO
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;
            objPruebaEntidad.ObjetoLote.numero_lote = "LOTE 5";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            //objPruebaEntidad.Llave = "613DADD56250D5EB3F554167D0728FA5";
            //objPruebaEntidad.Llave = "F235080075AA08787BF017135C764409BDF40CF18B23A734";//GIOVANNI.GUZMAN@PETROBRAS.COM
            objPruebaEntidad.Llave = "78EE4C0D753FEA497393F7F5D655D0E93D4BBA31E604A865"; // MARIO.CIANCAGLINI
            //objPruebaEntidad.Llave = "7CA2B1325E98D3A1721EDEEE466E357E"; //CARGUIO.CRC@YPFBCHACO.COM.BO
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 106;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            //var clienteJson = new JsonServiceClient("http://localhost:60478");
            var clienteJson = new JsonServiceClient("https://vsrvuid006.anh.gob.bo:9443/WSOctanoDownstream");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        [TestMethod]
        public void TestRegistrarPruebaCalidadGasLicuadoPetroleo_InsercionYPFB()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 1;
            vObjLoteDetalle.valor_reportado = "0.5300";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 4;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 2;
            vObjLoteDetalle.valor_reportado = "85.0000";
            vObjLoteDetalle.codigo_unidad_medida = 16;
            vObjLoteDetalle.valor_metodo_astm = 5;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 3;
            vObjLoteDetalle.valor_reportado = "11.0000";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 6;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 5;
            vObjLoteDetalle.valor_reportado = "1.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 6;
            vObjLoteDetalle.valor_reportado = "0.0020";
            vObjLoteDetalle.codigo_unidad_medida = 20;
            vObjLoteDetalle.valor_metodo_astm = 8;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 7;
            vObjLoteDetalle.valor_reportado = "0.50000";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 9;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 8;
            vObjLoteDetalle.valor_reportado = "100.0000";
            vObjLoteDetalle.codigo_unidad_medida = 21;
            vObjLoteDetalle.valor_metodo_astm = 10;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 9;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 11;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 10;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vObjLoteDetalle.valor_metodo_astm = 12;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 15;
            vObjLoteDetalle.valor_reportado = "2.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20130518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20130518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 125;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 321;// TESTING
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;
            objPruebaEntidad.ObjetoLote.numero_lote = "medio dia";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "C9940637283C0C0A8EA999C029473A7D";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 106;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            //var clienteJson = new JsonServiceClient("http://localhost:60478");
            var clienteJson = new JsonServiceClient("https://vsrvuid006.anh.gob.bo:9443/WSOctanoUpstream");


            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        [TestMethod]
        public void TestRegistrarPruebaCalidadGasLicuadoPetroleo_InsercionPetroBrass()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 1;
            vObjLoteDetalle.valor_reportado = "0.5300";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 4;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 2;
            vObjLoteDetalle.valor_reportado = "85.0000";
            vObjLoteDetalle.codigo_unidad_medida = 16;
            vObjLoteDetalle.valor_metodo_astm = 5;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 3;
            vObjLoteDetalle.valor_reportado = "11.0000";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 6;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 5;
            vObjLoteDetalle.valor_reportado = "1.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 6;
            vObjLoteDetalle.valor_reportado = "0.0020";
            vObjLoteDetalle.codigo_unidad_medida = 20;
            vObjLoteDetalle.valor_metodo_astm = 8;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 7;
            vObjLoteDetalle.valor_reportado = "0.50000";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 9;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 8;
            vObjLoteDetalle.valor_reportado = "100.0000";
            vObjLoteDetalle.codigo_unidad_medida = 21;
            vObjLoteDetalle.valor_metodo_astm = 10;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 9;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 11;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 10;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vObjLoteDetalle.valor_metodo_astm = 12;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 15;
            vObjLoteDetalle.valor_reportado = "2.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 7;
            vColLoteDetalle.Add(vObjLoteDetalle);

            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20130518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20130518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 125;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = 414;// TESTING
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;
            objPruebaEntidad.ObjetoLote.numero_lote = "medio dia";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "1B8D99560C79798323C4BE649F364AF4";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 106;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            //var clienteJson = new JsonServiceClient("http://localhost:60478");
            var clienteJson = new JsonServiceClient("https://vsrvuid006.anh.gob.bo:9443/WSOctanoUpstream");


            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        #endregion

        #region Kerosene

        [TestMethod]
        public void TestRegistrarKerosene()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 61;
            vObjLoteDetalle.valor_reportado = "12";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 2;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 62;
            vObjLoteDetalle.valor_reportado = "2.0000";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 20;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 63;
            vObjLoteDetalle.valor_reportado = "3.0000";
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vObjLoteDetalle.valor_metodo_astm = 23;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 64;
            vObjLoteDetalle.valor_reportado = "4.0000";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 61;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 65;
            vObjLoteDetalle.valor_reportado = "5.0000";
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vObjLoteDetalle.valor_metodo_astm = 63;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 66;
            vObjLoteDetalle.valor_reportado = "6";
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vObjLoteDetalle.valor_metodo_astm = 27;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 67;
            vObjLoteDetalle.valor_reportado = "7.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 64;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 68;
            vObjLoteDetalle.valor_reportado = "8.0000";
            vObjLoteDetalle.codigo_unidad_medida = 32;
            vObjLoteDetalle.valor_metodo_astm = 39;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 70;
            vObjLoteDetalle.valor_reportado = "9.0000";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 72;
            vObjLoteDetalle.valor_reportado = "10.0000";
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vObjLoteDetalle.valor_metodo_astm = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 73;
            vObjLoteDetalle.valor_reportado = "11.0000";
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vObjLoteDetalle.valor_metodo_astm = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 131;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 654;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;
            objPruebaEntidad.ObjetoLote.numero_lote = "kerosene";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 107;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        #endregion

        #region Gasolina Premium

        [TestMethod]
        public void TestRegistrarGasolinaPremium()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 35;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.valor_metodo_astm = 2;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 74;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 65;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 75;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 16;
            vObjLoteDetalle.codigo_unidad_medida = 16;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 76;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 19;
            vObjLoteDetalle.codigo_unidad_medida = 27;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 78;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 20;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 79;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 21;
            vObjLoteDetalle.codigo_unidad_medida = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 80;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 23;
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 81;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 25;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 82;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 26;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 83;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 66;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 84;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 27;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 85;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 27;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 86;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 28;
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 88;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 89;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 90;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 93;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 94;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 95;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 31;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 96;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 31;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 97;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 34;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 99;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 38;
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vColLoteDetalle.Add(vObjLoteDetalle);
            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 127;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;
            objPruebaEntidad.ObjetoLote.numero_lote = "premium";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 108;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        #endregion

        #region Gasolina Especial

        [TestMethod]
        public void TestRegistrarGasolinaEspecial()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 4;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.valor_metodo_astm = 2;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 11;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 13;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 12;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 16;
            vObjLoteDetalle.codigo_unidad_medida = 16;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 13;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 19;
            vObjLoteDetalle.codigo_unidad_medida = 27;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 14;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 20;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 16;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 21;
            vObjLoteDetalle.codigo_unidad_medida = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 17;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 23;
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 18;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 25;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 19;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 26;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 20;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 27;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 21;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 27;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 22;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 27;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 23;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 28;
            vObjLoteDetalle.codigo_unidad_medida = 22;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 25;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 26;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 27;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 28;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 29;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 30;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 33;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 31;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 30;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 32;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 32;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 33;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 36;
            vObjLoteDetalle.codigo_unidad_medida = 31;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 34;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 37;
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vColLoteDetalle.Add(vObjLoteDetalle);
            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 126;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;
            objPruebaEntidad.ObjetoLote.numero_lote = "especial";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 109;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        #endregion

        #region Jet Fuel

        [TestMethod]
        public void TestRegistrarJetFuel()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 124;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.valor_metodo_astm = 2;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 125;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 20;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 126;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 21;
            vObjLoteDetalle.codigo_unidad_medida = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 127;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 23;
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 128;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 94;
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 129;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 89;
            vObjLoteDetalle.codigo_unidad_medida = 49;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 130;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 92;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 131;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 62;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 132;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 96;
            vObjLoteDetalle.codigo_unidad_medida = 50;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 133;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 97;
            vObjLoteDetalle.codigo_unidad_medida = 51;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 134;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 30;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 135;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 39;
            vObjLoteDetalle.codigo_unidad_medida = 32;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 136;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 93;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 234;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 93;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 137;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 98;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 138;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 99;
            vObjLoteDetalle.codigo_unidad_medida = 54;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 139;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 99;
            vObjLoteDetalle.codigo_unidad_medida = 53;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 140;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 101;
            vObjLoteDetalle.codigo_unidad_medida = 55;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 142;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 143;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 144;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 145;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 146;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 147;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);
            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150518103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150518122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 129;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;
            objPruebaEntidad.ObjetoLote.numero_lote = "jet fuel";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 110;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        #endregion

        #region Gasolina de aviacion grado 100

        [TestMethod]
        public void TestRegistrarGasolinaAviacionGrado100()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 100;
            vObjLoteDetalle.valor_reportado = "1";
            vObjLoteDetalle.valor_metodo_astm = 2;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 101;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 85;
            vObjLoteDetalle.codigo_unidad_medida = 16;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 102;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 19;
            vObjLoteDetalle.codigo_unidad_medida = 47;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 103;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 20;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 104;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 87;
            vObjLoteDetalle.codigo_unidad_medida = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 105;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 87;
            vObjLoteDetalle.codigo_unidad_medida = 29;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 106;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 24;
            vObjLoteDetalle.codigo_unidad_medida = 25;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 107;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 26;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 108;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 88;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 109;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 27;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 110;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 89;
            vObjLoteDetalle.codigo_unidad_medida = 49;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 111;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 92;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 112;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 27;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 113;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 93;
            vObjLoteDetalle.codigo_unidad_medida = 20;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 115;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 116;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 117;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 118;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 119;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 120;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 121;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 18;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 122;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 123;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 29;
            vObjLoteDetalle.codigo_unidad_medida = 19;
            vColLoteDetalle.Add(vObjLoteDetalle);
            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150618103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150618122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 128;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 23;
            objPruebaEntidad.ObjetoLote.numero_lote = "Grado 100";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 112;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        #endregion

        #region Aplicación Chasis (Prueba Calidad)
        [TestMethod]
        public void TestRegistrarAplicacionChasis()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();

            vObjLoteDetalle.codigo_prueba_espec = 257;
            vObjLoteDetalle.valor_reportado = "0.000";
            vObjLoteDetalle.valor_metodo_astm = 66;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 258;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 67;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 259;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 69;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);


            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150718103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150718122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 161;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 46;
            objPruebaEntidad.ObjetoLote.numero_lote = "Grado 100";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 1041;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }
        #endregion

        #region Aplicación Rodamientos (Prueba Calidad)

        [TestMethod]
        public void TestRegistrarAplicacionRodamientos()
        {
            #region Llenado de la lista de calidad

            List<LoteDetalle> vColLoteDetalle = new List<LoteDetalle>();
            LoteDetalle vObjLoteDetalle = new LoteDetalle();

            vObjLoteDetalle.codigo_prueba_espec = 260;
            vObjLoteDetalle.valor_reportado = "0.000";
            vObjLoteDetalle.valor_metodo_astm = 66;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 260;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 67;
            vObjLoteDetalle.codigo_unidad_medida = 24;
            vColLoteDetalle.Add(vObjLoteDetalle);

            vObjLoteDetalle = new LoteDetalle();
            vObjLoteDetalle.codigo_prueba_espec = 262;
            vObjLoteDetalle.valor_reportado = "0.0000";
            vObjLoteDetalle.valor_metodo_astm = 69;
            vObjLoteDetalle.codigo_unidad_medida = 17;
            vColLoteDetalle.Add(vObjLoteDetalle);


            #endregion

            RegistrarPruebaCalidad objPruebaEntidad = new RegistrarPruebaCalidad();
            objPruebaEntidad.ObjetoLote = new ObjetoLote();
            objPruebaEntidad.ObjetoLote.fecha_operacion_volumen = 20150718103000;
            objPruebaEntidad.ObjetoLote.fecha_operacion_calidad = 20150718122500;
            objPruebaEntidad.ObjetoLote.codigo_producto = 159;
            //objPruebaEntidad.ObjetoLote.codigo_tanque_alm = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_secundario = -99999;
            //objPruebaEntidad.ObjetoLote.codigo_transporte_barcode = null;
            objPruebaEntidad.ObjetoLote.tipo_medio_transporte = -99999;
            objPruebaEntidad.ObjetoLote.volumen = 123;
            objPruebaEntidad.ObjetoLote.volumen_unidad_medida = 46;
            objPruebaEntidad.ObjetoLote.numero_lote = "Aplicación Rodamientos";
            objPruebaEntidad.ObjetoLote.pruebas_calidad = vColLoteDetalle;
            objPruebaEntidad.Llave = "F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0";
            objPruebaEntidad.DatosReporte = "{}";
            objPruebaEntidad.IdOperador = 1041;
            objPruebaEntidad.EstadoRegistro = 1;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(objPruebaEntidad.ObjetoLote);

            var clienteJson = new JsonServiceClient("http://localhost:60478");

            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/RegistrarPruebaCalidad/?format=json", objPruebaEntidad);
        }

        #endregion

        #endregion

        #region Registro de calidad

        #region Carburantes
        [TestMethod]
        public void TestRegistraPruebasCalidadGLP()
        {
            #region Llenado del objeto complejo

            PruebaCalidad vObjPruebaCalidad = new PruebaCalidad();
            vObjPruebaCalidad.valor_lote = "l200";
            vObjPruebaCalidad.id_entidad = 13;
            vObjPruebaCalidad.id_producto = 125;
            vObjPruebaCalidad.id_usuario = 1041;
            vObjPruebaCalidad.id_tabla_espec = 1;
            vObjPruebaCalidad.op_debe = 123;
            vObjPruebaCalidad.ope_haber = 0;
            vObjPruebaCalidad.fecha_operacion = 20150608120000;
            vObjPruebaCalidad.id_tipo_registro = 2;
            vObjPruebaCalidad.id_cantidad_padre = 0;
            vObjPruebaCalidad.id_unidad_medida = 69;
            vObjPruebaCalidad.id_entidad_origen = 13;
            vObjPruebaCalidad.id_entidad_destino = -99999;
            vObjPruebaCalidad.id_tipo_operacion = 1;
            vObjPruebaCalidad.id_tipo_reporte = 3;
            vObjPruebaCalidad.id_volumen_datos = 0;
            vObjPruebaCalidad.id_medio_transporte = 0;
            vObjPruebaCalidad.id_registro_padre = 0;
            vObjPruebaCalidad.id_tipo_muestra = 0;
            vObjPruebaCalidad.fecha_muestra = 0;
            vObjPruebaCalidad.procedencia = "";
            vObjPruebaCalidad.observaciones = "";
            vObjPruebaCalidad.precio = 15;
            vObjPruebaCalidad.volumen_muestra = 10;

            List<ObjetoCalidad> vColObjetoCalidad = new List<ObjetoCalidad>();
            ObjetoCalidad vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0.53";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 3;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 1;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "82";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 5;
            vObjObjetoCalidad.id_unidad_medida = 16;
            vObjObjetoCalidad.id_prueba_unidad = 2;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "2";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 6;
            vObjObjetoCalidad.id_unidad_medida = 18;
            vObjObjetoCalidad.id_prueba_unidad = 3;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 7;
            vObjObjetoCalidad.id_unidad_medida = 19;
            vObjObjetoCalidad.id_prueba_unidad = 5;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0.04";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 8;
            vObjObjetoCalidad.id_unidad_medida = 20;
            vObjObjetoCalidad.id_prueba_unidad = 6;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0.5";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 9;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 7;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "156";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 10;
            vObjObjetoCalidad.id_unidad_medida = 21;
            vObjObjetoCalidad.id_prueba_unidad = 8;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "CUMPLE";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 11;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 9;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "INFORMAR";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 12;
            vObjObjetoCalidad.id_unidad_medida = 22;
            vObjObjetoCalidad.id_prueba_unidad = 10;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "2";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 7;
            vObjObjetoCalidad.id_unidad_medida = 19;
            vObjObjetoCalidad.id_prueba_unidad = 15;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);
            vObjPruebaCalidad.pruebas_calidad = vColObjetoCalidad;
            #endregion

            RegistraPruebasCalidad vObjRegistraCalidadCarburantes = new RegistraPruebasCalidad();
            vObjRegistraCalidadCarburantes.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistraCalidadCarburantes.decAppIdUsuario = 1041;
            vObjRegistraCalidadCarburantes.decAppFechaRegistro = 20150612123104;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(vObjPruebaCalidad);
            vObjRegistraCalidadCarburantes.strEncabezado = vObjJasonEncabezado;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/RegistraPruebasCalidad/?format=json", vObjRegistraCalidadCarburantes);
        }

        [TestMethod]
        public void TestRegistraGasolinaAviacionGrado100_AreaTesting()
        {
            #region Llenado del objeto complejo

            PruebaCalidad vObjPruebaCalidad = new PruebaCalidad();
            vObjPruebaCalidad.valor_lote = "432";
            vObjPruebaCalidad.id_entidad = 317;
            vObjPruebaCalidad.id_producto = 128;
            vObjPruebaCalidad.id_usuario = 1262;
            vObjPruebaCalidad.id_tabla_espec = 22;
            vObjPruebaCalidad.op_debe = 2134;
            vObjPruebaCalidad.ope_haber = 0;
            vObjPruebaCalidad.fecha_operacion = 20150701120000;
            vObjPruebaCalidad.id_tipo_registro = 2;
            vObjPruebaCalidad.id_cantidad_padre = 0;
            vObjPruebaCalidad.id_unidad_medida = 46;
            vObjPruebaCalidad.id_entidad_origen = 317;
            vObjPruebaCalidad.id_entidad_destino = -99999;
            vObjPruebaCalidad.id_tipo_operacion = 1;
            vObjPruebaCalidad.id_tipo_reporte = 3;
            vObjPruebaCalidad.id_volumen_datos = 0;
            vObjPruebaCalidad.id_medio_transporte = 0;
            vObjPruebaCalidad.id_registro_padre = 0;
            vObjPruebaCalidad.id_tipo_muestra = 0;
            vObjPruebaCalidad.fecha_muestra = 0;
            vObjPruebaCalidad.procedencia = "";
            vObjPruebaCalidad.observaciones = "";
            List<ObjetoCalidad> vColObjetoCalidad = new List<ObjetoCalidad>();
            ObjetoCalidad vObjObjetoCalidad = new ObjetoCalidad();

            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0.53";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 1;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 100;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "6";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 15;
            vObjObjetoCalidad.id_unidad_medida = 101;
            vObjObjetoCalidad.id_prueba_unidad = 2;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "2";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 86;
            vObjObjetoCalidad.id_unidad_medida = 47;
            vObjObjetoCalidad.id_prueba_unidad = 102;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 20;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 103;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0.04";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 8;
            vObjObjetoCalidad.id_unidad_medida = 20;
            vObjObjetoCalidad.id_prueba_unidad = 6;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0.5";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 87;
            vObjObjetoCalidad.id_unidad_medida = 29;
            vObjObjetoCalidad.id_prueba_unidad = 104;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 87;
            vObjObjetoCalidad.id_unidad_medida = 29;
            vObjObjetoCalidad.id_prueba_unidad = 105;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 22;
            vObjObjetoCalidad.id_unidad_medida = 25;
            vObjObjetoCalidad.id_prueba_unidad = 106;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "100";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 26;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 107;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "130";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 88;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 108;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 27;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 109;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "50";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 90;
            vObjObjetoCalidad.id_unidad_medida = 49;
            vObjObjetoCalidad.id_prueba_unidad = 110;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 91;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_unidad = 111;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 27;
            vObjObjetoCalidad.id_unidad_medida = 27;
            vObjObjetoCalidad.id_prueba_unidad = 112;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 93;
            vObjObjetoCalidad.id_unidad_medida = 20;
            vObjObjetoCalidad.id_prueba_unidad = 113;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_unidad = 115;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "75";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 18;
            vObjObjetoCalidad.id_prueba_unidad = 116;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_unidad = 117;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_unidad = 118;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_unidad = 119;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 19;
            vObjObjetoCalidad.id_prueba_unidad = 120;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "135";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_unidad = 121;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "100";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 19;
            vObjObjetoCalidad.id_prueba_unidad = 122;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 29;
            vObjObjetoCalidad.id_unidad_medida = 19;
            vObjObjetoCalidad.id_prueba_unidad = 123;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjPruebaCalidad.pruebas_calidad = vColObjetoCalidad;
            #endregion

            RegistraPruebasCalidad vObjRegistraCalidadCarburantes = new RegistraPruebasCalidad();
            vObjRegistraCalidadCarburantes.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistraCalidadCarburantes.decAppIdUsuario = 1262;
            vObjRegistraCalidadCarburantes.decAppFechaRegistro = 20150804083738;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(vObjPruebaCalidad);
            vObjRegistraCalidadCarburantes.strEncabezado = vObjJasonEncabezado;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/RegistraPruebasCalidad/?format=json", vObjRegistraCalidadCarburantes);
        }
        #endregion

        #region Lubricantes
        [TestMethod]
        public void TestRegistraPruebasCalidadAceitesIndustriales()
        {
            #region Llenado del objeto complejo

            PruebaCalidad vObjPruebaCalidad = new PruebaCalidad();
            vObjPruebaCalidad.valor_lote = "l10";
            vObjPruebaCalidad.id_entidad = 13;
            vObjPruebaCalidad.id_producto = 136;
            vObjPruebaCalidad.id_usuario = 1041;
            vObjPruebaCalidad.id_tabla_espec = 4;
            vObjPruebaCalidad.op_debe = 123;
            vObjPruebaCalidad.ope_haber = 0;
            vObjPruebaCalidad.fecha_operacion = 20150608120000;
            vObjPruebaCalidad.id_tipo_registro = 2;
            vObjPruebaCalidad.id_cantidad_padre = 0;
            vObjPruebaCalidad.id_unidad_medida = 69;
            vObjPruebaCalidad.id_entidad_origen = 13;
            vObjPruebaCalidad.id_entidad_destino = -99999;
            vObjPruebaCalidad.id_tipo_operacion = 1;
            vObjPruebaCalidad.id_tipo_reporte = 3;
            vObjPruebaCalidad.id_volumen_datos = 0;
            vObjPruebaCalidad.id_medio_transporte = 0;
            vObjPruebaCalidad.id_registro_padre = 0;
            vObjPruebaCalidad.id_tipo_muestra = 0;
            vObjPruebaCalidad.fecha_muestra = 0;
            vObjPruebaCalidad.procedencia = "";
            vObjPruebaCalidad.observaciones = "";
            List<ObjetoCalidad> vColObjetoCalidad = new List<ObjetoCalidad>();
            ObjetoCalidad vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "61.3";
            vObjObjetoCalidad.valor_referencial = 31;
            vObjObjetoCalidad.id_metodo_astm = 39;
            vObjObjetoCalidad.id_unidad_medida = 32;
            vObjObjetoCalidad.id_prueba_unidad = 36;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "200";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 42;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_unidad = 38;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "4";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 43;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 39;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "5";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 1;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 40;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "6";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 44;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 41;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidad();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "87";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 66;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_unidad = 42;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjPruebaCalidad.pruebas_calidad = vColObjetoCalidad;
            #endregion

            RegistraPruebasCalidad vObjRegistraCalidadCarburantes = new RegistraPruebasCalidad();
            vObjRegistraCalidadCarburantes.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistraCalidadCarburantes.decAppIdUsuario = 1041;
            vObjRegistraCalidadCarburantes.decAppFechaRegistro = 20150606113704;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(vObjPruebaCalidad);
            vObjRegistraCalidadCarburantes.strEncabezado = vObjJasonEncabezado;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/RegistraPruebasCalidad/?format=json", vObjRegistraCalidadCarburantes);
        }
        #endregion

        #endregion

        #region Elimina calidad

        [TestMethod]
        public void TestEliminaRegistroCalidad()
        {
            EliminaRegistroCalidad vObjEliminaRegistroCalidad = new EliminaRegistroCalidad();
            vObjEliminaRegistroCalidad.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjEliminaRegistroCalidad.decAppFechaRegistro = 20150606113704;
            vObjEliminaRegistroCalidad.strCiteGenerado = "ANH-RON-ACIN 0017/2015";
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<O_RESULTADO_CTY>("/EliminaRegistroCalidad/?format=json", vObjEliminaRegistroCalidad);
        }

        #endregion

        #region Actualiza calidad

        #region Carburantes
        [TestMethod]
        public void TestActualizaPruebasCalidadGLP()
        {
            #region Llenado del objeto complejo

            PruebaCalidadActualiza vObjPruebaCalidad = new PruebaCalidadActualiza();
            vObjPruebaCalidad.valor_lote = "l5";
            vObjPruebaCalidad.id_entidad = 13;
            vObjPruebaCalidad.id_producto = 125;
            vObjPruebaCalidad.id_usuario = 1041;
            vObjPruebaCalidad.id_tabla_espec = 4;
            vObjPruebaCalidad.op_debe = 123;
            vObjPruebaCalidad.ope_haber = 0;
            vObjPruebaCalidad.fecha_operacion = 20150612120000;
            vObjPruebaCalidad.id_tipo_registro = 2;
            vObjPruebaCalidad.id_cantidad_padre = 0;
            vObjPruebaCalidad.id_unidad_medida = 69;
            vObjPruebaCalidad.id_entidad_origen = 13;
            vObjPruebaCalidad.id_entidad_destino = -99999;
            vObjPruebaCalidad.id_tipo_operacion = 1;
            vObjPruebaCalidad.id_tipo_reporte = 3;
            vObjPruebaCalidad.id_volumen_datos = 0;
            vObjPruebaCalidad.id_tanque_alm = 287;
            vObjPruebaCalidad.id_medio_transporte = 0;
            vObjPruebaCalidad.id_registro_padre = 0;
            vObjPruebaCalidad.id_tipo_muestra = 0;
            vObjPruebaCalidad.fecha_muestra = 0;
            vObjPruebaCalidad.procedencia = "";
            vObjPruebaCalidad.observaciones = "";
            vObjPruebaCalidad.valida_cerrado = 0;

            List<ObjetoCalidadActualiza> vColObjetoCalidad = new List<ObjetoCalidadActualiza>();
            ObjetoCalidadActualiza vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 31;
            vObjObjetoCalidad.id_metodo_astm = 39;
            vObjObjetoCalidad.id_unidad_medida = 32;
            vObjObjetoCalidad.id_prueba_calidad = 36;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 41;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 37;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 42;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_calidad = 38;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 43;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 39;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 1;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 40;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "6.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 44;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 41;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "87.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 66;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 42;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjPruebaCalidad.pruebas_calidad = vColObjetoCalidad;
            #endregion

            ActualizaPruebasCalidad vObjActualizaCalidadCarburantes = new ActualizaPruebasCalidad();
            vObjActualizaCalidadCarburantes.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjActualizaCalidadCarburantes.strCiteGenerado = "ANH-RON-GLP 537445/2015";
            vObjActualizaCalidadCarburantes.decAppIdUsuario = 1041;
            vObjActualizaCalidadCarburantes.decAppFechaRegistro = 20150606113704;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(vObjPruebaCalidad);
            vObjActualizaCalidadCarburantes.strEncabezado = vObjJasonEncabezado;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/ActualizaPruebasCalidad/?format=json", vObjActualizaCalidadCarburantes);
        }

        [TestMethod]
        public void TestActualizaPruebasCalidadGLPMarioCiancaglini()
        {
            #region Llenado del objeto complejo

            PruebaCalidadActualiza vObjPruebaCalidad = new PruebaCalidadActualiza();
            vObjPruebaCalidad.valor_lote = "l5";
            vObjPruebaCalidad.id_entidad = 13;
            vObjPruebaCalidad.id_producto = 125;
            vObjPruebaCalidad.id_usuario = 1041;
            vObjPruebaCalidad.id_tabla_espec = 4;
            vObjPruebaCalidad.op_debe = 123;
            vObjPruebaCalidad.ope_haber = 0;
            vObjPruebaCalidad.fecha_operacion = 20150612120000;
            vObjPruebaCalidad.id_tipo_registro = 2;
            vObjPruebaCalidad.id_cantidad_padre = 0;
            vObjPruebaCalidad.id_unidad_medida = 69;
            vObjPruebaCalidad.id_entidad_origen = 13;
            vObjPruebaCalidad.id_entidad_destino = -99999;
            vObjPruebaCalidad.id_tipo_operacion = 1;
            vObjPruebaCalidad.id_tipo_reporte = 3;
            vObjPruebaCalidad.id_volumen_datos = 0;
            vObjPruebaCalidad.id_tanque_alm = 287;
            vObjPruebaCalidad.id_medio_transporte = 0;
            vObjPruebaCalidad.id_registro_padre = 0;
            vObjPruebaCalidad.id_tipo_muestra = 0;
            vObjPruebaCalidad.fecha_muestra = 0;
            vObjPruebaCalidad.procedencia = "";
            vObjPruebaCalidad.observaciones = "";
            vObjPruebaCalidad.valida_cerrado = 0;

            List<ObjetoCalidadActualiza> vColObjetoCalidad = new List<ObjetoCalidadActualiza>();
            ObjetoCalidadActualiza vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 31;
            vObjObjetoCalidad.id_metodo_astm = 39;
            vObjObjetoCalidad.id_unidad_medida = 32;
            vObjObjetoCalidad.id_prueba_calidad = 36;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 41;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 37;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 42;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_calidad = 38;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 43;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 39;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "0";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 1;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 40;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "6.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 44;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 41;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "87.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 66;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 42;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjPruebaCalidad.pruebas_calidad = vColObjetoCalidad;
            #endregion

            ActualizaPruebasCalidad vObjActualizaCalidadCarburantes = new ActualizaPruebasCalidad();
            vObjActualizaCalidadCarburantes.strLlave = "78EE4C0D753FEA497393F7F5D655D0E93D4BBA31E604A865";
            vObjActualizaCalidadCarburantes.strCiteGenerado = "ANH-CLP-GLP 0032/2015";
            vObjActualizaCalidadCarburantes.decAppIdUsuario = 1041;
            vObjActualizaCalidadCarburantes.decAppFechaRegistro = 20150606113704;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(vObjPruebaCalidad);
            vObjActualizaCalidadCarburantes.strEncabezado = vObjJasonEncabezado;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/ActualizaPruebasCalidad/?format=json", vObjActualizaCalidadCarburantes);
        }
        #endregion

        #region Lubricantes
        [TestMethod]
        public void TestActualizaPruebasCalidadAceitesIndustriales()
        {
            #region Llenado del objeto complejo

            PruebaCalidadActualiza vObjPruebaCalidad = new PruebaCalidadActualiza();
            vObjPruebaCalidad.valor_lote = "l5";
            vObjPruebaCalidad.id_entidad = 13;
            vObjPruebaCalidad.id_producto = 136;
            vObjPruebaCalidad.id_usuario = 1041;
            vObjPruebaCalidad.id_tabla_espec = 4;
            vObjPruebaCalidad.op_debe = 123;
            vObjPruebaCalidad.ope_haber = 0;
            vObjPruebaCalidad.fecha_operacion = 20150612120000;
            vObjPruebaCalidad.id_tipo_registro = 2;
            vObjPruebaCalidad.id_cantidad_padre = 0;
            vObjPruebaCalidad.id_unidad_medida = 69;
            vObjPruebaCalidad.id_entidad_origen = 13;
            vObjPruebaCalidad.id_entidad_destino = -99999;
            vObjPruebaCalidad.id_tipo_operacion = 1;
            vObjPruebaCalidad.id_tipo_reporte = 3;
            vObjPruebaCalidad.id_volumen_datos = 0;
            vObjPruebaCalidad.id_tanque_alm = 287;
            vObjPruebaCalidad.id_medio_transporte = 0;
            vObjPruebaCalidad.id_registro_padre = 0;
            vObjPruebaCalidad.id_tipo_muestra = 0;
            vObjPruebaCalidad.fecha_muestra = 0;
            vObjPruebaCalidad.procedencia = "";
            vObjPruebaCalidad.observaciones = "";

            List<ObjetoCalidadActualiza> vColObjetoCalidad = new List<ObjetoCalidadActualiza>();
            ObjetoCalidadActualiza vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "61.4";
            vObjObjetoCalidad.valor_referencial = 31;
            vObjObjetoCalidad.id_metodo_astm = 39;
            vObjObjetoCalidad.id_unidad_medida = 32;
            vObjObjetoCalidad.id_prueba_calidad = 36;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "92.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 41;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 37;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "200.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 42;
            vObjObjetoCalidad.id_unidad_medida = 17;
            vObjObjetoCalidad.id_prueba_calidad = 38;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "4.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 43;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 39;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "5.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 1;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 40;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "6.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 44;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 41;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjObjetoCalidad = new ObjetoCalidadActualiza();
            vObjObjetoCalidad.valor_numerico = 0;
            vObjObjetoCalidad.valor_alfanumerico = "87.1";
            vObjObjetoCalidad.valor_referencial = 0;
            vObjObjetoCalidad.id_metodo_astm = 66;
            vObjObjetoCalidad.id_unidad_medida = 24;
            vObjObjetoCalidad.id_prueba_calidad = 42;
            vObjObjetoCalidad.justificacion = "";
            vColObjetoCalidad.Add(vObjObjetoCalidad);

            vObjPruebaCalidad.pruebas_calidad = vColObjetoCalidad;
            #endregion

            ActualizaPruebasCalidad vObjActualizaCalidadLubricantes = new ActualizaPruebasCalidad();
            vObjActualizaCalidadLubricantes.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjActualizaCalidadLubricantes.strCiteGenerado = "ANH-RON-ACIN 0010/2015";
            vObjActualizaCalidadLubricantes.decAppIdUsuario = 1041;
            vObjActualizaCalidadLubricantes.decAppFechaRegistro = 20150606113704;

            string vObjJasonEncabezado = null;
            vObjJasonEncabezado = JsonConvert.SerializeObject(vObjPruebaCalidad);
            vObjActualizaCalidadLubricantes.strEncabezado = vObjJasonEncabezado;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/ActualizaPruebasCalidad/?format=json", vObjActualizaCalidadLubricantes);
        }
        #endregion

        #endregion

        #region Retorna id Direccion
        [TestMethod]
        public void TestRetornaIdDireccion()
        {
            RetornaIdDireccion vObjRetornaIdDireccion = new RetornaIdDireccion();
            vObjRetornaIdDireccion.decIdUsuario = 1041;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_NUMBER_CTY>>("/RetornaIdDireccion/?format=json", vObjRetornaIdDireccion);
        }
        #endregion

        #region Registra Alerta Calidad
        [TestMethod]
        public void TestRegistraAlertaCalidad()
        {
            SvcRegistroDtep.RegistraAlertaCalidad vObjRegistraAlertaCalidad = new SvcRegistroDtep.RegistraAlertaCalidad();
            vObjRegistraAlertaCalidad.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistraAlertaCalidad.decIdTipoAlerta = 4;
            vObjRegistraAlertaCalidad.decIdPruebaCalidad = 257;
            vObjRegistraAlertaCalidad.strCiteGenerado = "ANH-RON-GCH 0020/2015";
            vObjRegistraAlertaCalidad.strObservaciones = "Prueba2 Servicio";
            vObjRegistraAlertaCalidad.decAppIdUsuario = 1041;
            vObjRegistraAlertaCalidad.decAppFechaRegistro = 20150808131100;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_NUMBER_CTY>>("/RegistraAlertaCalidad/?format=json", vObjRegistraAlertaCalidad);
        }
        #endregion

        #region Gestion Volumen y propietario (CTRL+R+T)
        [TestMethod]
        public void TestGestionVolumenReg()
        {
            SvcRegistroDtep.GestionVolumen vObjGestionVolumen = new SvcRegistroDtep.GestionVolumen();
            vObjGestionVolumen.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjGestionVolumen.decIdPruebaCalidad = 0;
            vObjGestionVolumen.decIdEntidad = 37484;
            vObjGestionVolumen.decIdTipoActividad = 36;
            vObjGestionVolumen.decFechaImportacion = 20170801120000;
            vObjGestionVolumen.decAppIdUsuario = 1687;
            vObjGestionVolumen.decAccion = 1;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/GestionVolumen/?format=json", vObjGestionVolumen);
        }
        [TestMethod]
        public void TestGestionVolumenMod()
        {
            SvcRegistroDtep.GestionVolumen vObjGestionVolumen = new SvcRegistroDtep.GestionVolumen();
            vObjGestionVolumen.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjGestionVolumen.decIdPruebaCalidad = 156;
            vObjGestionVolumen.decIdEntidad = 37484;
            vObjGestionVolumen.decIdTipoActividad = 36;
            vObjGestionVolumen.decFechaImportacion = 20170901180000;
            vObjGestionVolumen.decAppIdUsuario = 1687;
            vObjGestionVolumen.decAccion = 2;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/GestionVolumen/?format=json", vObjGestionVolumen);
        }
        [TestMethod]
        public void TestGestionVolumenEli()
        {
            SvcRegistroDtep.GestionVolumen vObjGestionVolumen = new SvcRegistroDtep.GestionVolumen();
            vObjGestionVolumen.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjGestionVolumen.decIdPruebaCalidad = 188;
            vObjGestionVolumen.decIdEntidad = 37484;
            vObjGestionVolumen.decIdTipoActividad = 36;
            vObjGestionVolumen.decFechaImportacion = 20170901120000;
            vObjGestionVolumen.decAppIdUsuario = 1687;
            vObjGestionVolumen.decAccion = 3;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_REF_REG_REPORTE_PLANO_CTY>>("/GestionVolumen/?format=json", vObjGestionVolumen);
        }


        [TestMethod]
        public void TestGestionPropietarioReg()
        {
            SvcRegistroDtep.GestionPropietario vObjGestionPropietario = new SvcRegistroDtep.GestionPropietario();
            vObjGestionPropietario.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjGestionPropietario.decIdPropietarioPrueba = 0;
            vObjGestionPropietario.decIdPruebaCalidad = 102;
            vObjGestionPropietario.decIdEntidad = 37628;
            vObjGestionPropietario.decIdTipoActividad = 50;
            vObjGestionPropietario.decAppIdUsuario = 1687;
            vObjGestionPropietario.decAccion = 1;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/GestionPropietario/?format=json", vObjGestionPropietario);
        }
        [TestMethod]
        public void TestGestionPropietarioEli()
        {
            SvcRegistroDtep.GestionPropietario vObjGestionPropietario = new SvcRegistroDtep.GestionPropietario();
            vObjGestionPropietario.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjGestionPropietario.decIdPropietarioPrueba = 137;
            vObjGestionPropietario.decAppIdUsuario = 1687;
            vObjGestionPropietario.decAccion = 3;
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/GestionPropietario/?format=json", vObjGestionPropietario);
        }
        #endregion

        #endregion

        #region Prueba Movimiento de Volumenes

        #region Registro de gas de alimento

        [TestMethod]
        public void TestRegistrarGasAlimento()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.64M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cVolumen;
            vObjObjetoDetalle.Valor = 67.49M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cContenidoLicuables;
            vObjObjetoDetalle.Valor = 21.30M;
            vObjObjetoDetalle.UnidadMedida = 70;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
            vObjObjetoDetalle.Valor = 21.30M;
            vObjObjetoDetalle.UnidadMedida = 71;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN2;
            vObjObjetoDetalle.Valor = 0.61M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cCO2;
            vObjObjetoDetalle.Valor = 0.74M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC1;
            vObjObjetoDetalle.Valor = 0.2M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 4.51M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 2.16M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.68M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.18M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C6;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC7;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            RegistrarCorrientes vObjRegistrarCorrientes = new RegistrarCorrientes();
            //vObjRegistrarCorrientes.Planta = 36252;//Desarrollo
            vObjRegistrarCorrientes.Planta = 233;//Testing                
            vObjRegistrarCorrientes.CorrienteCampo = 29;
            vObjRegistrarCorrientes.IdUsuario = 1486;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "16/05/2015";
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaCorrientes = vColObjetoDetalle;
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCorrientes/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarGasAlimentoSinUnidadDeMedida()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.64M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cVolumen;
            vObjObjetoDetalle.Valor = 67.49M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cContenidoLicuables;
            vObjObjetoDetalle.Valor = 21.30M;
            vObjObjetoDetalle.UnidadMedida = 70;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
            vObjObjetoDetalle.Valor = 21.30M;
            vObjObjetoDetalle.UnidadMedida = 71;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN2;
            vObjObjetoDetalle.Valor = 0.61M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cCO2;
            vObjObjetoDetalle.Valor = 0.74M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC1;
            vObjObjetoDetalle.Valor = 0.2M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 4.51M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 2.16M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.68M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.18M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C6;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC7;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            RegistrarCorrientes vObjRegistrarCorrientes = new RegistrarCorrientes();
            vObjRegistrarCorrientes.Planta = 36254;
            vObjRegistrarCorrientes.CorrienteCampo = 29;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            vObjRegistrarCorrientes.ListaCorrientes = vColObjetoDetalle;
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCorrientes/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarGasAlimentoSinConcepto()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.64M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Valor = 67.49M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cContenidoLicuables;
            vObjObjetoDetalle.Valor = 21.30M;
            vObjObjetoDetalle.UnidadMedida = 70;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
            vObjObjetoDetalle.Valor = 21.30M;
            vObjObjetoDetalle.UnidadMedida = 71;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN2;
            vObjObjetoDetalle.Valor = 0.61M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cCO2;
            vObjObjetoDetalle.Valor = 0.74M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC1;
            vObjObjetoDetalle.Valor = 0.2M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 4.51M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 2.16M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.68M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.18M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C6;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC7;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);


            RegistrarCorrientes vObjRegistrarCorrientes = new RegistrarCorrientes();
            vObjRegistrarCorrientes.Planta = 36254;
            vObjRegistrarCorrientes.CorrienteCampo = 29;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaCorrientes = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCorrientes/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarGasAlimentoConceptoErroneo()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = "Hola";
            vObjObjetoDetalle.Valor = 0.64M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cVolumen;
            vObjObjetoDetalle.Valor = 67.49M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cContenidoLicuables;
            vObjObjetoDetalle.Valor = 21.30M;
            vObjObjetoDetalle.UnidadMedida = 70;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
            vObjObjetoDetalle.Valor = 21.30M;
            vObjObjetoDetalle.UnidadMedida = 71;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN2;
            vObjObjetoDetalle.Valor = 0.61M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cCO2;
            vObjObjetoDetalle.Valor = 0.74M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC1;
            vObjObjetoDetalle.Valor = 0.2M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 4.51M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 2.16M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.68M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.18M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C6;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC7;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);


            RegistrarCorrientes vObjRegistrarCorrientes = new RegistrarCorrientes();
            vObjRegistrarCorrientes.Planta = 36254;
            vObjRegistrarCorrientes.CorrienteCampo = 29;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaCorrientes = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarCorrientes/?format=json", vObjRegistrarCorrientes);
        }
        #endregion

        #region Registro de producción

        [TestMethod]
        public void TestRegistrarProduccion()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cProduccionGlp;
            vObjObjetoDetalle.Valor = 0.64M;
            vObjObjetoDetalle.UnidadMedida = 99;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cProduccionPropano;
            vObjObjetoDetalle.Valor = 70.92M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGasolinaNatural;
            vObjObjetoDetalle.Valor = 20.38M;
            vObjObjetoDetalle.UnidadMedida = 119;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaConsumoPropano;
            vObjObjetoDetalle.Valor = 1073.76M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpCisterna;
            vObjObjetoDetalle.Valor = 0.59M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpDucto;
            vObjObjetoDetalle.Valor = 0.8M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cSaldoGlp;
            vObjObjetoDetalle.Valor = 90.32M;
            vObjObjetoDetalle.UnidadMedida = 46;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cSaldoPropano;
            vObjObjetoDetalle.Valor = 4.46M;
            vObjObjetoDetalle.UnidadMedida = 46;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cRendimientoProduccionGlp;
            vObjObjetoDetalle.Valor = 0.4M;
            vObjObjetoDetalle.UnidadMedida = 19;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGasCombustible;
            vObjObjetoDetalle.Valor = 0.26M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cQuemaGas;
            vObjObjetoDetalle.Valor = 0.187M;
            vObjObjetoDetalle.UnidadMedida = 116;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cTVR;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cTemperatura;
            vObjObjetoDetalle.Valor = 0.19M;
            vObjObjetoDetalle.UnidadMedida = 18;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            RegistrarProduccion vObjRegistrarCorrientes = new RegistrarProduccion();
            //vObjRegistrarCorrientes.Planta = 36252;//Desarrollo
            vObjRegistrarCorrientes.Planta = 233;//Testing
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.IdUsuario = 1486;
            vObjRegistrarCorrientes.ListaProduccion = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "15/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarProduccion/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarProduccionSinUnidadMedida()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cProduccionGlp;
            vObjObjetoDetalle.Valor = 0.64M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cProduccionPropano;
            vObjObjetoDetalle.Valor = 70.92M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGasolinaNatural;
            vObjObjetoDetalle.Valor = 20.38M;
            vObjObjetoDetalle.UnidadMedida = 119;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaConsumoPropano;
            vObjObjetoDetalle.Valor = 1073.76M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpCisterna;
            vObjObjetoDetalle.Valor = 0.59M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpDucto;
            vObjObjetoDetalle.Valor = 0.8M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cSaldoGlp;
            vObjObjetoDetalle.Valor = 90.32M;
            vObjObjetoDetalle.UnidadMedida = 46;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cSaldoPropano;
            vObjObjetoDetalle.Valor = 4.46M;
            vObjObjetoDetalle.UnidadMedida = 46;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cRendimientoProduccionGlp;
            vObjObjetoDetalle.Valor = 0.4M;
            vObjObjetoDetalle.UnidadMedida = 19;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGasCombustible;
            vObjObjetoDetalle.Valor = 0.26M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cQuemaGas;
            vObjObjetoDetalle.Valor = 0.187M;
            vObjObjetoDetalle.UnidadMedida = 116;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cTVR;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cTemperatura;
            vObjObjetoDetalle.Valor = 0.19M;
            vObjObjetoDetalle.UnidadMedida = 18;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            RegistrarProduccion vObjRegistrarCorrientes = new RegistrarProduccion();
            vObjRegistrarCorrientes.Planta = 36252;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaProduccion = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarProduccion/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarProduccionSinConcepto()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cProduccionGlp;
            vObjObjetoDetalle.Valor = 0.64M;
            vObjObjetoDetalle.UnidadMedida = 99;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cProduccionPropano;
            vObjObjetoDetalle.Valor = 70.92M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGasolinaNatural;
            vObjObjetoDetalle.Valor = 20.38M;
            vObjObjetoDetalle.UnidadMedida = 119;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaConsumoPropano;
            vObjObjetoDetalle.Valor = 1073.76M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpCisterna;
            vObjObjetoDetalle.Valor = 0.59M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpDucto;
            vObjObjetoDetalle.Valor = 0.8M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cSaldoGlp;
            vObjObjetoDetalle.Valor = 90.32M;
            vObjObjetoDetalle.UnidadMedida = 46;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cSaldoPropano;
            vObjObjetoDetalle.Valor = 4.46M;
            vObjObjetoDetalle.UnidadMedida = 46;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cRendimientoProduccionGlp;
            vObjObjetoDetalle.Valor = 0.4M;
            vObjObjetoDetalle.UnidadMedida = 19;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGasCombustible;
            vObjObjetoDetalle.Valor = 0.26M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cQuemaGas;
            vObjObjetoDetalle.Valor = 0.187M;
            vObjObjetoDetalle.UnidadMedida = 116;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cTVR;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cTemperatura;
            vObjObjetoDetalle.Valor = 0.19M;
            vObjObjetoDetalle.UnidadMedida = 18;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            RegistrarProduccion vObjRegistrarCorrientes = new RegistrarProduccion();
            vObjRegistrarCorrientes.Planta = 36252;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaProduccion = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarProduccion/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarProduccionConceptoErroneo()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = "DieselOLI";
            vObjObjetoDetalle.Valor = 0.64M;
            vObjObjetoDetalle.UnidadMedida = 99;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cProduccionPropano;
            vObjObjetoDetalle.Valor = 70.92M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGasolinaNatural;
            vObjObjetoDetalle.Valor = 20.38M;
            vObjObjetoDetalle.UnidadMedida = 119;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaConsumoPropano;
            vObjObjetoDetalle.Valor = 1073.76M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpCisterna;
            vObjObjetoDetalle.Valor = 0.59M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cEntregaGlpDucto;
            vObjObjetoDetalle.Valor = 0.8M;
            vObjObjetoDetalle.UnidadMedida = 90;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cSaldoGlp;
            vObjObjetoDetalle.Valor = 90.32M;
            vObjObjetoDetalle.UnidadMedida = 46;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cSaldoPropano;
            vObjObjetoDetalle.Valor = 4.46M;
            vObjObjetoDetalle.UnidadMedida = 46;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cRendimientoProduccionGlp;
            vObjObjetoDetalle.Valor = 0.4M;
            vObjObjetoDetalle.UnidadMedida = 19;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGasCombustible;
            vObjObjetoDetalle.Valor = 0.26M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cQuemaGas;
            vObjObjetoDetalle.Valor = 0.187M;
            vObjObjetoDetalle.UnidadMedida = 116;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cTVR;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cTemperatura;
            vObjObjetoDetalle.Valor = 0.19M;
            vObjObjetoDetalle.UnidadMedida = 18;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.21M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            RegistrarProduccion vObjRegistrarCorrientes = new RegistrarProduccion();
            vObjRegistrarCorrientes.Planta = 36252;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaProduccion = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarProduccion/?format=json", vObjRegistrarCorrientes);
        }
        #endregion

        #region Registro de gas residual

        [TestMethod]
        public void TestRegistrarGasResidual()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.6M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cVolumen;
            vObjObjetoDetalle.Valor = 148.68M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cH20;
            vObjObjetoDetalle.Valor = 21.30M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPuntoRocio;
            vObjObjetoDetalle.Valor = 0;
            vObjObjetoDetalle.UnidadMedida = 18;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
            vObjObjetoDetalle.Valor = 1006.84M;
            vObjObjetoDetalle.UnidadMedida = 71;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN2;
            vObjObjetoDetalle.Valor = 0.61M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cCO2;
            vObjObjetoDetalle.Valor = 0.74M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC1;
            vObjObjetoDetalle.Valor = 90.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 4.51M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 2.16M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.68M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.18M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C6;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC7;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);


            RegistrarGasResidual vObjRegistrarCorrientes = new RegistrarGasResidual();
            //vObjRegistrarCorrientes.Planta = 36252;//Desarrollo
            vObjRegistrarCorrientes.Planta = 233;//Testing
            vObjRegistrarCorrientes.Version = 2;
            vObjRegistrarCorrientes.CodigoProyecto = 0;
            vObjRegistrarCorrientes.IdUsuario = 0;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaResidual = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "";
            vObjRegistrarCorrientes.Justificacion = "";
            vObjRegistrarCorrientes.Fecha = "18/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            //var clienteJson = new JsonServiceClient("https://vsrvuid004:7443");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarGasResidual/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarGasResidualSinUnidadMedida()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.6M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cVolumen;
            vObjObjetoDetalle.Valor = 148.68M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cH20;
            vObjObjetoDetalle.Valor = 21.30M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPuntoRocio;
            vObjObjetoDetalle.Valor = 0;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
            vObjObjetoDetalle.Valor = 1006.84M;
            vObjObjetoDetalle.UnidadMedida = 71;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN2;
            vObjObjetoDetalle.Valor = 0.61M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cCO2;
            vObjObjetoDetalle.Valor = 0.74M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC1;
            vObjObjetoDetalle.Valor = 90.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 4.51M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 2.16M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.68M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.18M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C6;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC7;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            RegistrarGasResidual vObjRegistrarCorrientes = new RegistrarGasResidual();
            vObjRegistrarCorrientes.Planta = 36253;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaResidual = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarGasResidual/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarGasResidualSinConcepto()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.6M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cVolumen;
            vObjObjetoDetalle.Valor = 148.68M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cH20;
            vObjObjetoDetalle.Valor = 21.30M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPuntoRocio;
            vObjObjetoDetalle.Valor = 0;
            vObjObjetoDetalle.UnidadMedida = 18;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
            vObjObjetoDetalle.Valor = 1006.84M;
            vObjObjetoDetalle.UnidadMedida = 71;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN2;
            vObjObjetoDetalle.Valor = 0.61M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cCO2;
            vObjObjetoDetalle.Valor = 0.74M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC1;
            vObjObjetoDetalle.Valor = 90.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 4.51M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 2.16M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.68M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.18M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C6;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            RegistrarGasResidual vObjRegistrarCorrientes = new RegistrarGasResidual();
            vObjRegistrarCorrientes.Planta = 36253;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaResidual = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarGasResidual/?format=json", vObjRegistrarCorrientes);
        }

        [TestMethod]
        public void TestRegistrarGasResidualConceptoErroneo()
        {
            #region Cargando valores
            ObjetoDetalle vObjObjetoDetalle = null;
            List<ObjetoDetalle> vColObjetoDetalle = new List<ObjetoDetalle>();

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cGravedadEspecifica;
            vObjObjetoDetalle.Valor = 0.6M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cVolumen;
            vObjObjetoDetalle.Valor = 148.68M;
            vObjObjetoDetalle.UnidadMedida = 69;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cH20;
            vObjObjetoDetalle.Valor = 21.30M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = "fsd";
            vObjObjetoDetalle.Valor = 0;
            vObjObjetoDetalle.UnidadMedida = 18;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cPoderCalorifico;
            vObjObjetoDetalle.Valor = 1006.84M;
            vObjObjetoDetalle.UnidadMedida = 71;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN2;
            vObjObjetoDetalle.Valor = 0.61M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cCO2;
            vObjObjetoDetalle.Valor = 0.74M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC1;
            vObjObjetoDetalle.Valor = 90.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC2;
            vObjObjetoDetalle.Valor = 4.51M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC3;
            vObjObjetoDetalle.Valor = 2.16M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C4;
            vObjObjetoDetalle.Valor = 0.26M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C4;
            vObjObjetoDetalle.Valor = 0.68M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cI_C5;
            vObjObjetoDetalle.Valor = 0.18M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C5;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cN_C6;
            vObjObjetoDetalle.Valor = 0.22M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);

            vObjObjetoDetalle = new ObjetoDetalle();
            vObjObjetoDetalle.Concepto = Constantes.cC7;
            vObjObjetoDetalle.Valor = 0.19M;
            vColObjetoDetalle.Add(vObjObjetoDetalle);


            RegistrarGasResidual vObjRegistrarCorrientes = new RegistrarGasResidual();
            vObjRegistrarCorrientes.Planta = 36252;
            vObjRegistrarCorrientes.IdUsuario = 1486;
            vObjRegistrarCorrientes.Llave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistrarCorrientes.ListaResidual = vColObjetoDetalle;
            vObjRegistrarCorrientes.Observaciones = "Observacion";
            vObjRegistrarCorrientes.Justificacion = "Justificacion";
            vObjRegistrarCorrientes.Fecha = "22/05/2015";
            #endregion

            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistrarGasResidual/?format=json", vObjRegistrarCorrientes);
        }

        #endregion

        #endregion

        #region Prueba Servicio de Operadores
        /*
        [TestMethod]
        public void TestListadoMedioTransporte()
        {
            CPersistenciaListados vobjCPersistenciaListados = new CPersistenciaListados();
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Get<List<O_REF_MEDIO_TRANSPORTE_CTY>>("/EObtenerMediosTransporte/F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0?format=json");
        }
        */
        [TestMethod]
        public void TestListadoProductos()
        {
            CPersistenciaListados vobjCPersistenciaListados = new CPersistenciaListados();
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Get<List<O_REF_CATALOGO_PRODUCTOS_CTY>>("/ObtenerCatalogoProductos/F7A1554B90CB67C2BB54BDFC28C42FFE88AD4DA48D74AAE0?format=json");
        }
        #endregion

        #region Parametricas
        [TestMethod]
        public void TestRegistraParametrica()
        {
            SvcRegistroDtep.RegistraParametrica vObjRegistraParametrica = new SvcRegistroDtep.RegistraParametrica();
            vObjRegistraParametrica.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistraParametrica.strTipo = "MARCA_LUBRICANTES";
            vObjRegistraParametrica.strCodigo = "REPSOL";
            vObjRegistraParametrica.strDescripcion = "REPSOL";
            vObjRegistraParametrica.strValor = "REPSOL";
            vObjRegistraParametrica.decAppIdUsuario = 1041;
            var clienteJson = new JsonServiceClient("http://localhost:1263");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistraParametrica/?format=json", vObjRegistraParametrica);
        }

        [TestMethod]
        public void TestActualizaParametrica()
        {
            SvcRegistroDtep.ActualizaParametrica vObjActualizaParametrica = new SvcRegistroDtep.ActualizaParametrica();
            vObjActualizaParametrica.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjActualizaParametrica.decIdParametrica = 3;
            vObjActualizaParametrica.strTipo = "MARCA_LUBRICANTES";
            vObjActualizaParametrica.strCodigo = "REPSOL";
            vObjActualizaParametrica.strDescripcion = "REPSOL";
            vObjActualizaParametrica.strValor = "REPSOL";
            vObjActualizaParametrica.decAppIdUsuario = 1041;
            var clienteJson = new JsonServiceClient("http://localhost:1263");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/ActualizaParametrica/?format=json", vObjActualizaParametrica);
        }

        [TestMethod]
        public void TestEliminaParametrica()
        {
            SvcRegistroDtep.EliminaParametrica vObjEliminaParametrica = new SvcRegistroDtep.EliminaParametrica();
            vObjEliminaParametrica.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjEliminaParametrica.decIdParametrica = 6;
            vObjEliminaParametrica.strTipo = "MARCA_LUBRICANTES";
            vObjEliminaParametrica.decAppIdUsuario = 1041;
            var clienteJson = new JsonServiceClient("http://localhost:1263");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/EliminaParametrica/?format=json", vObjEliminaParametrica);
        }

        [TestMethod]
        public void TestRegistraParametricaNpc()
        {
            SvcRegistroDtep.RegistraParametroNpc vObjRegistraParametrica = new SvcRegistroDtep.RegistraParametroNpc();

            vObjRegistraParametrica.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjRegistraParametrica.decIdEntidad = 13;
            vObjRegistraParametrica.decIdTablaEspec = 9;
            vObjRegistraParametrica.strCodigo = "L2T30";
            vObjRegistraParametrica.strNombre = "LUB-2T SAE30";
            vObjRegistraParametrica.decAppIdUsuario = 1041;
            var clienteJson = new JsonServiceClient("http://localhost:1263");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistraParametroNpc/?format=json", vObjRegistraParametrica);
        }

        [TestMethod]
        public void TestActualizaParametricaNpc()
        {
            SvcRegistroDtep.ActualizaParametroNpc vObjActualizaParametrica = new SvcRegistroDtep.ActualizaParametroNpc();
            vObjActualizaParametrica.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjActualizaParametrica.decIdNombreProducto = 3;
            vObjActualizaParametrica.decIdEntidad = 13;
            vObjActualizaParametrica.decIdTablaEspec = 9;
            vObjActualizaParametrica.strCodigo = "L2T30";
            vObjActualizaParametrica.strNombre = "LUB-2T SAE30";
            vObjActualizaParametrica.decAppIdUsuario = 1041;
            var clienteJson = new JsonServiceClient("http://localhost:1263");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/ActualizaParametroNpc/?format=json", vObjActualizaParametrica);
        }

        [TestMethod]
        public void TestEliminaParametricaNpc()
        {
            SvcRegistroDtep.EliminaParametroNpc vObjEliminaParametrica = new SvcRegistroDtep.EliminaParametroNpc();
            vObjEliminaParametrica.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjEliminaParametrica.decIdNombreProducto = 3;
            vObjEliminaParametrica.decAppIdUsuario = 1041;
            var clienteJson = new JsonServiceClient("http://localhost:1263");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/EliminaParametroNpc/?format=json", vObjEliminaParametrica);
        }

        [TestMethod]
        public void TestRegistraDocumento()
        {
            SvcRegistroDtep.RegistraDocumento vObjEliminaParametrica = new SvcRegistroDtep.RegistraDocumento();
            vObjEliminaParametrica.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
            vObjEliminaParametrica.decIdTipRespaldo = 3;
            vObjEliminaParametrica.strCite = "ANH-RON-GLP 0005/2016";
            vObjEliminaParametrica.strObservacion = "prueba";
            vObjEliminaParametrica.decAppIdUsuario = 1041;
            vObjEliminaParametrica.decAppFechaRegistro = 20160818000000;
            var clienteJson = new JsonServiceClient("https://vsrvuid004.anh.gob.bo:7443/WSOctanov2");
            var objResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistraDocumento/?format=json", vObjEliminaParametrica);
        }

        #endregion

        #region Inserciones cíclicas

        [TestMethod]
        public void TestRegistraPruebasCalidadGlpCiclicas()
        {
            string vNumeroLote = "LOTE_NRO_";
            for (int i = 0; i <= 1000000; i++)
            {
                Debug.WriteLine(string.Format("Inicio {0} - {1}", i, DateTime.Now));

                #region Llenado del objeto complejo

                long fecha_op = 20150608120000;
                if (i >= 0) fecha_op = 20150108120000;
                if (i >= 83333) fecha_op = 20150208120000;
                if (i >= 166667) fecha_op = 20150308120000;
                if (i >= 250000) fecha_op = 20150408120000;
                if (i >= 333333) fecha_op = 20150508120000;
                if (i >= 416667) fecha_op = 20150608120000;
                if (i >= 500000) fecha_op = 20150708120000;
                if (i >= 583333) fecha_op = 20150808120000;
                if (i >= 666667) fecha_op = 20150908120000;
                if (i >= 750000) fecha_op = 20151008120000;
                if (i >= 833333) fecha_op = 20151108120000;
                if (i >= 916667) fecha_op = 20151208120000;
                int j = i + 9909;

                PruebaCalidad vObjPruebaCalidad = new PruebaCalidad();
                vObjPruebaCalidad.valor_lote = vNumeroLote + j;
                vObjPruebaCalidad.id_entidad = 13;
                vObjPruebaCalidad.id_producto = 125;
                vObjPruebaCalidad.id_usuario = 1041;
                vObjPruebaCalidad.id_tabla_espec = 1;
                vObjPruebaCalidad.op_debe = 123;
                vObjPruebaCalidad.ope_haber = 0;
                vObjPruebaCalidad.fecha_operacion = fecha_op;
                vObjPruebaCalidad.id_tipo_registro = 2;
                vObjPruebaCalidad.id_cantidad_padre = 0;
                vObjPruebaCalidad.id_unidad_medida = 69;
                vObjPruebaCalidad.id_entidad_origen = 13;
                vObjPruebaCalidad.id_entidad_destino = -99999;
                vObjPruebaCalidad.id_tipo_operacion = 1;
                vObjPruebaCalidad.id_tipo_reporte = 3;
                vObjPruebaCalidad.id_volumen_datos = 0;
                vObjPruebaCalidad.id_medio_transporte = 0;
                vObjPruebaCalidad.id_registro_padre = 0;
                vObjPruebaCalidad.id_tipo_muestra = 0;
                vObjPruebaCalidad.fecha_muestra = 0;
                vObjPruebaCalidad.procedencia = "";
                vObjPruebaCalidad.observaciones = "";
                List<ObjetoCalidad> vColObjetoCalidad = new List<ObjetoCalidad>();
                ObjetoCalidad vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "0.53";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 3;
                vObjObjetoCalidad.id_unidad_medida = 24;
                vObjObjetoCalidad.id_prueba_unidad = 1;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "82";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 5;
                vObjObjetoCalidad.id_unidad_medida = 16;
                vObjObjetoCalidad.id_prueba_unidad = 2;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "2";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 6;
                vObjObjetoCalidad.id_unidad_medida = 18;
                vObjObjetoCalidad.id_prueba_unidad = 3;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "1";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 7;
                vObjObjetoCalidad.id_unidad_medida = 19;
                vObjObjetoCalidad.id_prueba_unidad = 5;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "0.04";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 8;
                vObjObjetoCalidad.id_unidad_medida = 20;
                vObjObjetoCalidad.id_prueba_unidad = 6;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "0.5";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 9;
                vObjObjetoCalidad.id_unidad_medida = 24;
                vObjObjetoCalidad.id_prueba_unidad = 7;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "156";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 10;
                vObjObjetoCalidad.id_unidad_medida = 21;
                vObjObjetoCalidad.id_prueba_unidad = 8;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "CUMPLE";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 11;
                vObjObjetoCalidad.id_unidad_medida = 24;
                vObjObjetoCalidad.id_prueba_unidad = 9;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "INFORMAR";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 12;
                vObjObjetoCalidad.id_unidad_medida = 22;
                vObjObjetoCalidad.id_prueba_unidad = 10;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);

                vObjObjetoCalidad = new ObjetoCalidad();
                vObjObjetoCalidad.valor_numerico = 0;
                vObjObjetoCalidad.valor_alfanumerico = "2";
                vObjObjetoCalidad.valor_referencial = 0;
                vObjObjetoCalidad.id_metodo_astm = 7;
                vObjObjetoCalidad.id_unidad_medida = 19;
                vObjObjetoCalidad.id_prueba_unidad = 15;
                vObjObjetoCalidad.justificacion = "";
                vColObjetoCalidad.Add(vObjObjetoCalidad);
                vObjPruebaCalidad.pruebas_calidad = vColObjetoCalidad;
                #endregion

                RegistraPruebasCalidad vObjRegistraCalidadCarburantes = new RegistraPruebasCalidad();
                vObjRegistraCalidadCarburantes.strLlave = "83809AD945F1F72D0EA9FDA0E599E0B3";
                vObjRegistraCalidadCarburantes.decAppIdUsuario = 1041;
                vObjRegistraCalidadCarburantes.decAppFechaRegistro = 20150612123104;

                string vObjJasonEncabezado = null;
                vObjJasonEncabezado = JsonConvert.SerializeObject(vObjPruebaCalidad);
                vObjRegistraCalidadCarburantes.strEncabezado = vObjJasonEncabezado;
                var clienteJson = new JsonServiceClient("http://localhost:60478");
                var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/RegistraPruebasCalidad/?format=json", vObjRegistraCalidadCarburantes);
                Debug.WriteLine(string.Format("Fin {0} - {1}", i, DateTime.Now));
                Debug.WriteLine("Fin {0}", objResultado.CORRELATIVO_REGISTRO);
            }
        }

        #endregion

    }
}

