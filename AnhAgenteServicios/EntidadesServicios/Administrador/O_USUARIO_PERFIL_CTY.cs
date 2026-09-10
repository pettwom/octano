using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhAgenteServicios.EntidadesServicios.Administrador
{
    public class O_USUARIO_PERFIL_CTY
    {
        public decimal ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string NOMBRES_USUARIO { get; set; }
        public string SISTEMA { get; set; }
        public string NOMBRE_PERFIL { get; set; }
        public decimal ID_PERFIL { get; set; }
    }
}