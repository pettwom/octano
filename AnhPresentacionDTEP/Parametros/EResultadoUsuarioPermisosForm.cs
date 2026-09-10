using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Parametros
{
    public class EResultadoUsuarioPermisosForm
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<EResultadoPermisos> oResultado { get; set; }
    }
}