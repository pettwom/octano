using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhPersistenciaCore.Entidades
{
    public class E_REPORTE_CALIDAD
    {
        public decimal CODIGO_CERTIFICADO_CALIDAD { get; set; }
        public string CORRELATIVO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public decimal VOLUMEN { get; set; }
        public string VOLUMEN_UNIDAD_MEDIDA { get; set; }
        public string PUNTO_TRANS_CUSTODIO { get; set; }
        public string RESOLUCION { get; set; }
        public Nullable<decimal> VOLUMEN_MUESTRA { get; set; }
        public Nullable<decimal> PRECIO { get; set; }
        public string MONEDA { get; set; }
        public string MARCA_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public List<TABLA_ESPECIFICA> TABLA_ESPECIFICA { get; set; }
    }

    public class TABLA_ESPECIFICA
    {
        public string PRUEBA_CALIDAD { get; set; }
        public string METODO_ASTM { get; set; }
        public string UNIDAD_MEDIDA { get; set; }
        public string VALOR_REPORTADO { get; set; }
        public string RANGOS_MULTIPLES { get; set; }
    }

}
