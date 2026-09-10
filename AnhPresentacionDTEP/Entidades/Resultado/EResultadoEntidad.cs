using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhPresentacionDTEP.Entidades.Resultado
{
    public class EResultadoEntidad <T>
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public T oResultado { get; set; }
    }
}
