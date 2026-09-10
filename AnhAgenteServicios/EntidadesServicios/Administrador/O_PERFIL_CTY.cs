using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhAgenteServicios.EntidadesServicios.Administrador
{
    public class O_PERFIL_CTY
    {
        public decimal ID_PERFIL { get; set; }
        public string NOMBRE_PERFIL { get; set; }
        public decimal MODULO_ID { get; set; }
        public decimal PRIORIDAD { get; set; }
        public string DESCRIPCION { get; set; }
    }
}