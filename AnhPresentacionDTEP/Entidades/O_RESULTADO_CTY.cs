using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhPresentacionDTEP.Entidades
{
    public class O_RESULTADO_CTY
    {
        public decimal ID_TABLA { get; set; }
        public string MENSAJE_ERROR { get; set; }
        public Nullable<decimal> RESULTADO { get; set; }
    }
}