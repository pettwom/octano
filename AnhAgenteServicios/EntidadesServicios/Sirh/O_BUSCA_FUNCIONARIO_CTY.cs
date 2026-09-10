using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhAgenteServicios.EntidadesServicios.Sirh
{
    public class O_BUSCA_FUNCIONARIO_CTY
    {
        public decimal ID_PERSONA { get; set; }
        public decimal USUARIO_ID { get; set; }
        public string NOMBRES { get; set; }
        public string P_APELLIDO { get; set; }
        public string S_APELLIDO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string NUM_IDENTIDAD { get; set; }
        public string LUGAR_EXPD { get; set; }
        public string NUM_COMPLEMENTO { get; set; }
        public string GENERO { get; set; }
        public string USUARIO_DOMINIO { get; set; }
        public string ESTADO_FUNCIONARIO { get; set; }
        public Nullable<decimal> FOTO_DIGITAL_ID { get; set; }
    }
}
