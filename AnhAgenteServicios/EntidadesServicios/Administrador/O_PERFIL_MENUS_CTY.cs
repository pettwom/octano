using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhAgenteServicios.EntidadesServicios.Administrador
{
    public class O_PERFIL_MENUS_CTY
    {
        public decimal ID_MENU_PERFIL { get; set; }

        public Nullable<decimal> ID_PRIVILEGIO { get; set; }

        public Nullable<decimal> PRIORIDAD { get; set; }

        public string PRIVILEGIO { get; set; }

        public decimal ID_PERFIL { get; set; }

        public string NOMBRE_PERFIL { get; set; }

        public Nullable<decimal> ID_MODULO { get; set; }

        public string NOMBRE_MODULO { get; set; }

        public decimal ID_MENU { get; set; }

        public string TITULO { get; set; }

        public Nullable<decimal> ORDEN { get; set; }

        public Nullable<decimal> NIVEL { get; set; }

        public string TITULO_PADRE { get; set; }

        public Nullable<decimal> ID_PADRE_MENU { get; set; }
    }
}