using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhPresentacionDTEP.Clases.VolumenesCalidad
{
    public static class CCParametricas
    {
        //public static List<O_PARAMETROS_CTY> ListaParametros(string strDominioParametro)
        //{
        //    if (Session[CVariablesSesion.UsuarioId] == null)
        //    {
        //        Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
        //        HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
        //            var lstResultadoNpc = clienteJson.Get<List<O_LISTA_NOMBRE_PRODUCTO_CTY>>("/ListarNombreProductos/" + cParametrosHydro.strCredencial + "/0/0?format=json");
        //            var lstResultadoTE = clienteJson.Get<List<O_PROD_CAL_CTY>>("/ListarGruposProductoEspecificacionCalidad/" + cParametrosHydro.strCredencial + "/" + 0 + "/" + 124 + "/APP_OCT?format=json");


        //            //var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
        //            //var lstResultado = clienteJson.Get<List<O_TIPOS_REPORTE_CTY>>("/ListarTiposReporte/" + cParametrosHydro.strCredencial + "/MENSUAL?format=json");
        //            //var lstProducto = clienteJson.Get<List<O_PRODUCTOS_CTY>>("/ListarProductos/" + cParametrosHydro.strCredencial + "/GLP?format=json");
        //            //var lstUnidadMedida = clienteJson.Get<List<O_UNIDADES_MEDIDA_GRAL_CTY>>("/ListarUnidadesMedidaGeneral/" + cParametrosHydro.strCredencial + "?format=json");
        //            //lstUnidadMedida = (from tv in lstUnidadMedida where tv.CODIGO.Contains("TMD") select tv).ToList();

        //            //e.NewValues["FECHA_PRODE"] = DateTime.Now;
        //            //e.NewValues["ID_TIPO_REPORTE"] = lstResultado.Count > 0 ? lstResultado[0].ID_TIPO_REPORTE : 0;
        //            //e.NewValues["VALOR_PRODE"] = 0;
        //            //e.NewValues["ID_PRODUCTO"] = lstProducto.Count > 0 ? lstProducto[0].ID_PRODUCTO : 0;
        //            //e.NewValues["ID_UNIDAD_MEDIDA"] = lstUnidadMedida.Count > 0 ? lstUnidadMedida[0].ID_UNIDAD_MEDIDA : 0;
        //        }
        //        catch (Exception ex)
        //        {
        //            strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
        //            CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
        //        }
        //    }
        //}
    }
}