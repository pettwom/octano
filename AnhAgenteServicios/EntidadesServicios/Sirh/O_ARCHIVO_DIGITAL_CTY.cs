using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhAgenteServicios.EntidadesServicios.Sirh
{
    public class O_ARCHIVO_DIGITAL_CTY
    {
        public decimal ID_ARCHIVO_DIGITAL { get; set; }
        public byte[] ARCHIVO_BINARIO { get; set; }
        public string NOMBRE_ARCH { get; set; }
        public string EXTENSION_ARCH { get; set; }
        public string OBSERVACION { get; set; }
    }
}
