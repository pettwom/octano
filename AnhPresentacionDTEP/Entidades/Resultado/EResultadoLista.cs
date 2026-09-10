using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhPresentacionDTEP.Entidades.Resultado
{
    public class EResultadoLista <T>
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<T> oResultado { get; set; }
    }
}