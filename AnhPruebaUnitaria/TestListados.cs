using AnhPersistenciaCore.Aplicacion.Listados;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhPruebaUnitaria
{
    [TestClass]
    public class TestListados
    {
        private string strMensajeError = string.Empty;

        #region Pruebas de calidad de lubricantes

        [TestMethod]
        public void TestListarProductoEspecificacionLubricante()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PRODUCTO_ESPEC_CTY>>("/ListarProductoEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/2/124?format=json");
        }

        [TestMethod]
        public void TestListarProductoEspecificacionLubricanteDecimalNegativo()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PRODUCTO_ESPEC_CTY>>("/ListarProductoEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/-2/-124?format=json");
        }

        #endregion

        #region Pruebas de calidad de carburantes

        [TestMethod]
        public void TestListarProductoEspecificacionCarburante()
        {
            CPersistenciaListados vobjCPersistenciaListados = new CPersistenciaListados();
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PRODUCTO_ESPEC_CTY>>("/ListarProductoEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/2/123?format=json");
        }

        [TestMethod]
        public void TestListarProductoEspecificacionCarburanteDecimalNegativo()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PRODUCTO_ESPEC_CTY>>("/ListarProductoEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/-2/-123?format=json");
        }

        #endregion

        #region Pruebas de tablas de especificación

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionCarburantesAceiteIndustrial()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var vColFormularioEspecifico = clienteJson.Get<List<O_TABLA_ESPEC_FORMULARIO_CITY>>("/ListarPruebasCalidadTablaEspecFormulario/83809AD945F1F72D0EA9FDA0E599E0B3/4/13?format=json");
            var vUnidades = new ArrayList();
            var unidades = new ArrayList();
            var metodosAstm = new ArrayList();
            var rangoMultiple = new ArrayList();
            var vMetodosAstm = new ArrayList();
            var resultado = new ArrayList();
            String[] vRangoMultiple = null;
            string[] ccc = new string[1];
            String[] vUnidadMedida = null;
            String[] vMetodoAstm = null;
            String[] vSubMetodoAstm = null;
            bool esPadre = false;
            foreach (var vObjFormularioEspecifico in vColFormularioEspecifico)
            {
                esPadre = false;
                if (vObjFormularioEspecifico.UNIDAD_MEDIDA != null)
                {
                    vUnidadMedida = vObjFormularioEspecifico.UNIDAD_MEDIDA.Trim().Split(',');
                    vUnidades.Add(new
                    {
                        idUnidadMedida = Convert.ToDecimal(vUnidadMedida[0]),
                        codigo = vUnidadMedida[1]
                    });
                }
                if (vObjFormularioEspecifico.RANGOS_MULTIPLES != null)
                {
                    vRangoMultiple = vObjFormularioEspecifico.RANGOS_MULTIPLES.Trim().Split('|');
                    foreach (var item1 in vRangoMultiple)
                    {
                        vRangoMultiple = item1.Trim().Split(',');
                        rangoMultiple.Add(new
                        {
                            idRangoMultiple = Convert.ToDecimal(vRangoMultiple[0]),
                            valorReferencia = vRangoMultiple[1]
                        });
                    }

                }
                if (vObjFormularioEspecifico.METODO_ASTM != null)
                {
                    vMetodoAstm = vObjFormularioEspecifico.METODO_ASTM.Trim().Split('|');
                    foreach (var item3 in vMetodoAstm)
                    {
                        vSubMetodoAstm = item3.Trim().Split(',');
                        vMetodosAstm.Add(new
                        {
                            idMetodo = Convert.ToDecimal(vSubMetodoAstm[0]),
                            nombre = vSubMetodoAstm[1]
                        });
                    }
                }
                esPadre = (vObjFormularioEspecifico.ID_PRUEBA_CALIDAD_PADRE == 0);
                esPadre = false;
                if (vObjFormularioEspecifico.ESPEC_MINIMA != null)
                {
                    resultado.Add(new
                    {
                        idPruebaCalidad = vObjFormularioEspecifico.ID_PRUEBA_CALIDAD,
                        esPadre = (esPadre ? 1 : 0),
                        idPruebaCalidadPadre = vObjFormularioEspecifico.ID_PRUEBA_CALIDAD_PADRE,
                        descripcion = vObjFormularioEspecifico.DESCRIPCION,
                        espec_minima = Convert.ToString(vObjFormularioEspecifico.ESPEC_MINIMA),
                        espec_maxima = vObjFormularioEspecifico.ESPEC_MAXIMA,
                        unidades = vUnidades,
                        metodosAstm = vMetodosAstm,
                        rangoMultiple = rangoMultiple
                    });
                }
                else
                {
                    resultado.Add(new
                    {
                        idPruebaCalidad = vObjFormularioEspecifico.ID_PRUEBA_CALIDAD,
                        esPadre = (esPadre ? 1 : 0),
                        idPruebaCalidadPadre = vObjFormularioEspecifico.ID_PRUEBA_CALIDAD_PADRE,
                        descripcion = vObjFormularioEspecifico.DESCRIPCION,
                        espec_minima = vObjFormularioEspecifico.ESPEC_MINIMA,
                        espec_maxima = vObjFormularioEspecifico.ESPEC_MAXIMA,
                        unidades = vUnidades,
                        metodosAstm = vMetodosAstm,
                        rangoMultiple = rangoMultiple
                    });
                }
                vUnidades = new ArrayList();
                vMetodosAstm = new ArrayList();
                rangoMultiple = new ArrayList();
            }
        }

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionTipoProductos()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PROD_ESPEC_PACL_CTY>>("/ListarGruposProductoEspecificacionTipoCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/1/1041?format=json");
        }

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionTipoProductosDTEP()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PROD_ESPEC_PACL_CTY>>("/ListarGruposProductoEspecificacionTipoCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/2/1306?format=json");
        }

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionTipoProductosNegativos()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PROD_ESPEC_PACL_CTY>>("/ListarGruposProductoEspecificacionTipoCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/-2/-1041?format=json");
        }

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionProductos()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PROD_CAL_CTY>>("/ListarGruposProductoEspecificacionCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/1041/123/APP_OCT?format=json");
        }

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionProductosDTEP()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PROD_CAL_CTY>>("/ListarGruposProductoEspecificacionCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/1306/124/APP_SIREL?format=json");
        }

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionProductosNegativos()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PROD_CAL_CTY>>("/ListarGruposProductoEspecificacionCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/-1041/-123/APP_A?format=json");
        }

        #endregion

        #region Declaración de tabla(s) especifica(s)

        public class EListarPruebasCalidadTablaEspecifica : IReturn<EResultado>
        {
            public decimal decIdTablaEspecificacion { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdUbicacion { get; set; }
        }        

        #endregion         

        #region Lista Tanques por Entidad

        [TestMethod]
        public void TestListarTanquePorEntidad()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_LISTA_TANQUE_ENTIDAD_CTY>>("/ListarTanquePorEntidad/83809AD945F1F72D0EA9FDA0E599E0B3/36252?format=json");
        }

        [TestMethod]
        public void TestListarTanquePorEntidad_Testing()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_LISTA_TANQUE_ENTIDAD_CTY>>("/ListarTanquePorEntidad/83809AD945F1F72D0EA9FDA0E599E0B3/11?format=json");
        } 

        #endregion

        #region Lista Unidad de medidad por usuario

        [TestMethod]
        public void TestListarUnidadMedidaPorEntidad()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_UM_CANT_CAL_CTY>>("/ListarUnidadMedidaPorEntidad/83809AD945F1F72D0EA9FDA0E599E0B3/1041?format=json");
        }

        #endregion

        #region Pruebas de tablas de especificación

        [TestMethod]
        public void TestListarTablaEspecificacionCarburantesGLP()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/1/36252/20150817155634/ANH-RON-GLP%200010_2015?format=json");
        }

        [TestMethod]
        public void TestListarTablaEspecificacionCarburantesGasolinaEspecial()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/2/13/20150101000000/0?format=json");
        }

        [TestMethod]
        public void TestListarTablaEspecificacionCarburantesGasolinaPremium()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/3/13/20150101000000/0?format=json");
        }

        [TestMethod]
        public void TestListarTablaEspecificacionCarburantesAceiteIndustrial()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var vColFormularioEspecifico = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/4/13/20150101000000/0?format=json");
        }

        [TestMethod]
        public void TestListarTablaEspecificacionTipoProductos()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/2/1041/20150101000000/0?format=json");
        }

        [TestMethod]
        public void TestListarTablaEspecificacionTipoProductosDTEP()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/2/1306/20150101000000/0?format=json");
        }

        [TestMethod]
        public void TestListarTablaEspecificacionTipoProductosNegativos()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/-2/-1041/20150808000000/-1?format=json");
        }

        [TestMethod]
        public void TestListarTablaEspecificacionProductos()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/2/1041/123?format=json");
        }

        [TestMethod]
        public void TestListarTablaEspecificacionProductosDTEP()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/83809AD945F1F72D0EA9FDA0E599E0B3/2/1306/20150101000000/0?format=json");
        }

        #endregion

        #region Movimiento de volúmenes

        [TestMethod]
        public void TestListarUnidadesMedidaGeneral()
        {
            var clienteJson = new JsonServiceClient("https://vsrvuid006.anh.gob.bo:9443/WSOctanov2");
            try
            {
                var objResultado = clienteJson.Get<List<O_UNIDADES_MEDIDA_GRAL_CTY>>("/ListarUnidadesMedidaGeneral/83809AD945F1F72D0EA9FDA0E599E0B3?format=json");
            }
            catch (Exception ex)
            {
            }            
        }

        [TestMethod]
        public void TestListarProductosGLP()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PRODUCTOS_CTY>>("/ListarProductos/83809AD945F1F72D0EA9FDA0E599E0B3/GLP?format=json");
        }

        [TestMethod]
        public void TestListarTodosProductos()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PRODUCTOS_CTY>>("/ListarProductos/83809AD945F1F72D0EA9FDA0E599E0B3/0?format=json");
        }

        #endregion

        #region Tablas de especificación para DCD

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionDCD()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PROD_ESPEC_PACL_CTY>>("/ListarGruposProductoEspecificacionTipoCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/279/1755?format=json");
        }

        [TestMethod]
        public void TestListarPruebasCalidadTablaEspecificacionDetalleDCD()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_PROD_CAL_CTY>>("/ListarGruposProductoEspecificacionCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/279/1755/123?format=json");
        }

        #endregion

        #region Validaciones de calidad

        [TestMethod]
        public void TestValidacionesCalidad()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<O_VALIDA_CALIDAD_CTY>("/ObtenerValidacionCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/30?format=json");
        }

        [TestMethod]
        public void TestValidacionesCalidadNoExistente()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<O_VALIDA_CALIDAD_CTY>("/ObtenerValidacionCalidad/83809AD945F1F72D0EA9FDA0E599E0B3/-30?format=json");
        }

        #endregion

        #region Listado de alertas

        [TestMethod]
        public void TestListadoCertificadoCalidad()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            //var objResultado = clienteJson.Get<List<O_CERTIFICADOS_ALERTAS_CTY>>("ListarCertificadosConAlertas/83809AD945F1F72D0EA9FDA0E599E0B3/0/01_08_2015/11_08_2015?format=json");
            var objResultado = clienteJson.Get<List<O_CERTIFICADOS_ALERTAS_CTY>>("ListarCertificadosConAlertas/83809AD945F1F72D0EA9FDA0E599E0B3/1041/01_11_2015/30_11_2015?format=json");
        }

        #endregion

        #region Listado de marcas Lubricantes

        [TestMethod]
        public void TestListadoMarcaLubricantes()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_LISTADO_MARCA_LUB_CTY>>("ListarMarcasLubricantes/83809AD945F1F72D0EA9FDA0E599E0B3?format=json");
        }

        #endregion

        #region Listado de Monedas y tipo de cambio

        [TestMethod]
        public void TestListadoMonedas()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_LISTADO_MONEDA_CTY>>("ListarMonedas/83809AD945F1F72D0EA9FDA0E599E0B3?format=json");
        }

        #endregion
        
        #region Listado de Catalogos

        [TestMethod]
        public void TestListadoCatalogos()
        {
            //var clienteJson = new JsonServiceClient("http://localhost:32207");
            var clienteJson = new JsonServiceClient("https://vsrvuid006.anh.gob.bo:9443/WSOctanoDownstream");
            var objResultado = clienteJson.Get<List<E_CATALOGO_CALIDAD>>("/ListarCatalogosCalidad/613DADD56250D5EB3F554167D0728FA5?format=json");
            foreach (var item in objResultado)
            {
                Console.WriteLine("Tipo: {0}", item.TIPO);
                foreach (var item1 in item.vColCampoValor)
                {
                    Console.WriteLine("       - Codigo: {0}     , Descripcion: {1}", item1.CODIGO,item1.DESCRIPCION);
                }
                Console.WriteLine("  ");
            }
        }

        [TestMethod]
        public void TestListadoCatalogos_Test()
        {
            var clienteJson = new JsonServiceClient("https://vsrvuid006.anh.gob.bo:9443/WSOctanoDownstream");
            //var clienteJson = new JsonServiceClient("http://localhost:32789");
            var objResultado = clienteJson.Get<List<E_CATALOGO_CALIDAD>>("/ListarCatalogosCalidad/F235080075AA08787BF017135C764409BDF40CF18B23A734?format=json");// TESTING
            foreach (var item in objResultado)
            {
                Console.WriteLine("Tipo: {0}", item.TIPO);
                foreach (var item1 in item.vColCampoValor)
                {
                    Console.WriteLine("       - Codigo: {0}     , Descripcion: {1}", item1.CODIGO, item1.DESCRIPCION);
                }
                Console.WriteLine("  ");
            }
        }

        #endregion

        #region Parametricas
        [TestMethod]
        public void TestListadoParametricas_Test()
        {
            var clienteJson = new JsonServiceClient("http://localhost:32207");
            var objResultado = clienteJson.Get<List<O_LISTA_PARAMETRICA_CTY>>("/ListarParametricas/83809AD945F1F72D0EA9FDA0E599E0B3/MARCA_LUBRICANTES?format=json");// TESTING
            foreach (var item in objResultado)
            {
                Console.WriteLine("Tipo: {0}", item.TIPO);
                Console.WriteLine("  ");
            }
        }
        #endregion

        #region Listado Propietarios y etidades

        [TestMethod]
        public void TestListadoPropietarios()
        {
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Get<List<O_LISTA_PROPIETARIOS_CTY>>("ListarPropietarios/83809AD945F1F72D0EA9FDA0E599E0B3/0/0/37484/36/1687?format=json");
        }

        [TestMethod]
        public void TestListadoEntidadPropietario()
        {
            var clienteJson = new JsonServiceClient("http://localhost:60478");
            var objResultado = clienteJson.Get<List<O_LISTA_ENTIDAD_PROP_CTY>>("ListarEntidadesPropietarios/83809AD945F1F72D0EA9FDA0E599E0B3/1687?format=json");
        }

        #endregion


    }
}
