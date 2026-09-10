using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhAgenteServicios.EntidadesServicios.Administrador
{
    public class O_USUARIO_MENU_CTY
    {
        public Nullable<decimal> ID_MENU_USUARIO { get; set; }
        public decimal ID_MENU { get; set; }
        public string TITULO { get; set; }
        public Nullable<decimal> ID_MENU_PADRE { get; set; }
        public decimal ID_USUARIO { get; set; }
        public Nullable<decimal> ESTADO_ACTIVO { get; set; }
        public Nullable<decimal> ORDEN { get; set; }
    }
}