using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhPersistenciaCore.Entidades
{
    public class E_CATALOGO_CALIDAD
    {
        public string TIPO { get; set; }
        public List<CAMPO_VALOR> vColCampoValor { get; set; }
    }

    public class CAMPO_VALOR
    {
        public decimal CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
    }
}
