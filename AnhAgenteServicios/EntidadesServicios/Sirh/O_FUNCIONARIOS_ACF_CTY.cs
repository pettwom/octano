using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhAgenteServicios.EntidadesServicios.Sirh
{
    public class O_FUNCIONARIOS_ACF_CTY
    {
        public Nullable<decimal> ID_USUARIO { get; set; }
        public string NOMBRES { get; set; }
        public string P_APELLIDO { get; set; }
        public string S_APELLIDO { get; set; }
        public string NUM_IDENTIDAD { get; set; }
        public string NUM_COMPLEMENTO { get; set; }
        public string LUGAR_EXPD { get; set; }
        public decimal ID_PUESTO { get; set; }
        public string CARGO { get; set; }
        public decimal ID_ORGANIGRAMA { get; set; }
        public string UNIDAD_ORGANIZACIONAL { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public Nullable<System.DateTime> FECHA_BAJA { get; set; }
        public string ESTADO_CONTRATO { get; set; }
        public string CONDICION { get; set; }
        public string ITEM_CONTRATO { get; set; }
        public decimal LUGAR_TRABAJO_ID { get; set; }
        public string NIVEL_LUGAR_TRABAJO { get; set; }
    }
}
