using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhPersistenciaCore.Entidades
{
    public partial class E_TABLA_ESPECIFICA 
    {
        public decimal decIdPruebaCalidad { get; set; }
        public decimal decIdPruebaCalidadPadre{ get; set; }
        public string strDescripcion{ get; set; }
        public List<E_CAMPO_VALOR> listMetodoAstm{ get; set; }
        public List<E_CAMPO_VALOR> listUnidadMedida{ get; set; }
        public List<E_CAMPO_VALOR> listRangosMultiples{ get; set; }
        public string strEspecMinima{ get; set; }
        public string strEspecMaxima{ get; set; }
        public string strEspecAlfanumerico{ get; set; }
        public List<E_CAMPO_VALOR> listEspecMinimaRango{ get; set; }
        public List<E_CAMPO_VALOR> listEspecMaximaRango{ get; set; }
    }
}
