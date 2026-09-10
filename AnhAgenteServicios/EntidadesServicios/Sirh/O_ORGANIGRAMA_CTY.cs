using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhAgenteServicios.EntidadesServicios.Sirh
{
    public class O_ORGANIGRAMA_CTY
    {
        public decimal ID_ORGANIGRAMA { get; set; }
        public Nullable<decimal> SIRH_ID { get; set; }
        public Nullable<decimal> PADRE_ID { get; set; }
        public string GESTION { get; set; }
        public decimal VERSION_ORG_ID { get; set; }
        public Nullable<decimal> REGIONAL_ID { get; set; }
        public string UNIDAD_ORGANIZACIONAL { get; set; }
        public string NIVEL_ESTRUCTURA { get; set; }
        public string RESPONSABLE { get; set; }
        public string SIGLA { get; set; }
        public Nullable<System.DateTime> FECHA_INI { get; set; }
        public Nullable<System.DateTime> FECHA_FIN { get; set; }
        public string CODIGO { get; set; }
        public string OBSERVACIONES { get; set; }
        public Nullable<decimal> ORDEN { get; set; }
        public string REALIZADO_POR { get; set; }
        public Nullable<System.DateTime> AUD_FECHA { get; set; }
    }
}
