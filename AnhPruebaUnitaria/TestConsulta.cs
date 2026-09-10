using AnhPersistenciaCore.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceClient.Web;

namespace AnhPruebaUnitaria
{
    [TestClass]
    public class TestConsulta
    {
        private string strMensajeError = string.Empty;

        #region Consulta Entidad Octano

        [TestMethod]
        public void TestConsultaEntidadPorUsuario()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<O_CONSULTA_ENTIDAD_CTY>("/ObtenerEntidadOctano/83809AD945F1F72D0EA9FDA0E599E0B3/1041?format=json");
            
            Console.WriteLine("Entidad: {0}", objResultado.ID_ENTIDAD);
        }

        [TestMethod]
        public void TestConsultaEntidadPorUsuarioAnh()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<O_CONSULTA_ENTIDAD_CTY>("/ObtenerEntidadOctano/83809AD945F1F72D0EA9FDA0E599E0B3/1627?format=json");

            Console.WriteLine("Entidad: {0}", objResultado.ID_ENTIDAD);
        }

        [TestMethod]
        public void TestConsultaEntidadPorUsuarioNoExistente()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<O_CONSULTA_ENTIDAD_CTY>("/ObtenerEntidadOctano/83809AD945F1F72D0EA9FDA0E599E0B3/6484684961684684?format=json");

            Console.WriteLine("Entidad: {0}", objResultado.ID_ENTIDAD);
        }

        #endregion
    }

}
