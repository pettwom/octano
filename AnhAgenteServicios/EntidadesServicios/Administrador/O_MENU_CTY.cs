using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhAgenteServicios.EntidadesServicios.Administrador
{
    public class O_MENU_CTY
    {
        public decimal ID_MENU { get; set; }
        public string TITULO { get; set; }
        public string ABREVIACION { get; set; }
        public string ENLACE { get; set; }
        public Nullable<decimal> ID_MENU_PADRE { get; set; }
        public decimal ID_MODULO { get; set; }
        public decimal ORDEN { get; set; }
        public Nullable<decimal> NIVEL { get; set; }
        public byte[] ICONO { get; set; }
        public string DESCRIPCION { get; set; }
    }
}