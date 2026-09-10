using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhAgenteServicios.EntidadesServicios.Administrador
{
    public class O_USUARIOS_CTY
    {
        public decimal ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string NOMBRES { get; set; }
        public Nullable<System.DateTime> VIGENTE_DESDE { get; set; }
        public Nullable<System.DateTime> VIGENTE_HASTA { get; set; }
        public string ESTADO { get; set; }
        public Nullable<decimal> ASIGNADO { get; set; }
        public string VIGENCIA { get; set; }
    }
}