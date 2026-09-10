using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhPersistenciaCore.Entidades
{
    public class ObjetoCalidad
    {
        public int valor_numerico { get; set; }
        public string valor_alfanumerico { get; set; }
        public int valor_referencial { get; set; }
        public int id_metodo_astm { get; set; }
        public decimal id_unidad_medida { get; set; }
        public decimal id_prueba_unidad { get; set; }
        public decimal id_prueba_calidad { get; set; }
        public string justificacion { get; set; }
    }

    public class PruebaCalidad
    {
        public string valor_lote { get; set; }
        public int id_entidad { get; set; }
        public int id_producto { get; set; }
        public int id_usuario { get; set; }
        public int id_tabla_espec { get; set; }
        public decimal op_debe { get; set; }
        public int ope_haber { get; set; }
        public long fecha_operacion { get; set; }
        public int id_tipo_registro { get; set; }
        public int id_cantidad_padre { get; set; }
        public int id_unidad_medida { get; set; }
        public int id_entidad_origen { get; set; }
        public int id_entidad_destino { get; set; }
        public int id_tipo_operacion { get; set; }
        public int id_tipo_reporte { get; set; }
        public int id_volumen_datos { get; set; }
        public string punto_custodio { get; set; }
        public decimal precio { get; set; }
        public int id_actividad { get; set; }
        public decimal volumen_muestra { get; set; }
        public int id_moneda { get; set; }

        //public decimal id_tanque_alm { get; set; }
        public int id_punto_custodio { get; set; }
        public int id_medio_transporte { get; set; }
        public int id_registro_padre { get; set; }
        public int id_tipo_muestra { get; set; }
        public decimal fecha_muestra { get; set; }
        public int valida_cerrado { get; set; }
        public string procedencia { get; set; }
        public string observaciones { get; set; }
        public int id_marca_producto { get; set; }
        public string nombre_producto { get; set; }
        public int id_organigrama { get; set; }
        public long fecha_ra { get; set; }
        public string ruta_internacion { get; set; }
        public string empresa_proveedora { get; set; }
        public string tanque_externo { get; set; }
        public string nro_lote_Verif { get; set; }
        public List<ObjetoCalidad> pruebas_calidad { get; set; }        
    }
}
