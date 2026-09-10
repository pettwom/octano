using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnhPresentacionDTEP.Entidades;
using ServiceStack.ServiceHost;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Entidades
{
   
        public class EParametroEntidad<T>
        {
            public string strCredencial { get; set; }
            public T objEntidad { get; set; }
        }

        public class EResultadoI : IReturn<EResultado>
        {
            public decimal decCodigo { get; set; }
            public object oResultado { get; set; }
            public string strMensaje { get; set; }
        } 
   
}