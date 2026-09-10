using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhPersistenciaCore.Entidades
{
    class EResultado
    {
        public decimal intCodigo { get; set; }
        public string strMensaje { get; set; }
        public object oResultado { get; set; }
    }

    public class EPResultado
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public object oResultado { get; set; }
    }

    public class O_LISTA_PERMISO_CALIDAD_CTY
    {
        public decimal ID_GESTION_OCTANO { get; set; }
        public decimal ID_USUARIO_ANH { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string DIRECCION { get; set; }
        public string FECHA_FIN { get; set; }
        public string APLICACION { get; set; }
        public string ACTIVIDAD { get; set; }
        public decimal ID_ENTIDAD { get; set; }
        public string ENTIDAD { get; set; }
    }

    public class EListaControlesUsuario
    {
        public string strCredencial { get; set; }
        public decimal decIdUsuario { get; set; }
    }
}
