using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhPersistenciaCore.Entidades.Parametros.VolumenesCalidad
{
    public class CParametrosCalidad
    {                
        public const string cColListadoActividades = "ColListadoActividades";
        public const string cValidacionCalidad = "ValidacionCalidad";
        public const string cValoresMenu = "ValoresMenu";
        public const string cColListadoPutoCustodio = "ColListadoPutoCustodio";
        public const string cColListadoUnidadesMedida = "ColListadoUnidadesMedida";
        public const string cColListarPruebasCalidadTablaEspecificacion = "ColListarPruebasCalidadTablaEspecificacion";
        public const string cColPruebasCalidadTablaEspecificacion = "ColPruebasCalidadTablaEspecificacion";
        public const string cObjValoresCargarGrilla = "ObjValoresCargarGrilla";
        public const string cColListadoMarcaLubricantes = "ColListadoMarcaLubricantes";
        public const string cColListadoNombreProductos = "ColListadoNombreProductos";

        #region Caracteres de cadena sin espacio

        public const string cExpresionRegularLetrasNumeros = "^[A-Z0-9]*$";
        public const string cExpresionRegularLetrasNumerosSimbolo = "^[A-Z0-9/]*$";

        #endregion

        #region Caracteres de cadena con espacio

        public const string cAlfanumericoBasicoConTildesConEspacios = "^[a-zA-Z<>0-9ñ áéúíóÁÉÚÍÓÑ,./-]*$";

        #endregion
    }
}
