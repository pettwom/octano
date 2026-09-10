using System.Web;
using AnhPersistenciaCore.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhPruebaUnitaria
{
    [TestClass]
    public class TestReporte
    {
        private string strMensajeError = string.Empty;
        
        #region Lista Unidad de medidad por usuario

        [TestMethod]
        public void TestReportarCalidad()
        {
            var clienteJson = new JsonServiceClient("http://localhost:17515");
            var objResultado = clienteJson.Get<List<O_REPORTE_CALIDAD>>("/ReportarCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/ANH-RON-GLP%20537514_2015?format=json");
        }

        [TestMethod]
        public void TestReportarCalidadGLP()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<List<O_REPORTE_CALIDAD>>("/ReportarCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/ANH-RGV-ATA%200022_2015?format=json");
        }

        [TestMethod]
        public void TestReportarCalidadPrincipalGLP()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<List<O_REPORTE_CALIDAD_PRINCIPAL>>("/ReportarCalidadPrincipal/83809AD945F1F72D0EA9FDA0E599E0B3/13/1041/01-06-2014/01-06-2015?format=json");
        }

        [TestMethod]
        public void TestReportarAlertaCertificado()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_REPORTE_ALERTA_CERT_CTY>>("/ReportarAlertaCertificado/83809AD945F1F72D0EA9FDA0E599E0B3/13/1041/20150801000000/20160801000000?format=json");
        }

        #endregion

        #region Movimiento de volúmenes

        [TestMethod]
        public void TestReportarDtepGasAlimento()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepGasAlimento/83809AD945F1F72D0EA9FDA0E599E0B3/1/36253/69/20141001000000/20141001000000?format=json");
        }

        [TestMethod]
        public void TestReportarDtepProduccion()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepProduccion/83809AD945F1F72D0EA9FDA0E599E0B3/1/36253/46/20141001000000/20141001000000?format=json");
        }

        [TestMethod]
        public void TestReportarDtepGasResidual()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<List<O_REPORTE_VOL_DTEP>>("/ReportarDtepGasResidual/83809AD945F1F72D0EA9FDA0E599E0B3/1/36253/69/20141001000000/20141001000000?format=json");
        }

        [TestMethod]
        public void TestListarTiposReporte()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/83809AD945F1F72D0EA9FDA0E599E0B3/DIARIO?format=json");
        }

        #endregion
    }
}
