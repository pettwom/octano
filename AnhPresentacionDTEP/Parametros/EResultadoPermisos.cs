using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Parametros
{
    public class EResultadoPermisos
    {
        public decimal ID_FORMULARIO_ASP { get; set; }
        public string NOMBRE_FORMULARIO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public decimal MODULO_ID { get; set; }
        public string NOMBRE_MODULO { get; set; }
        public decimal CONSULTA { get; set; }
        public decimal ALTAS { get; set; }
        public decimal BAJAS { get; set; }
        public decimal MODIFICACIONES { get; set; }
        public decimal REVISION { get; set; }
        public decimal APROBACION { get; set; }
        public string OTROS { get; set; }
    }
}