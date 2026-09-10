using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhPersistenciaCore.Entidades
{
    public class ObjetoLote
    {
        /// <summary>
        /// fecha_operacion_volumen
        /// </summary>
        public decimal fecha_operacion_volumen { get; set; }

        /// <summary>
        /// fecha_operacion_calidad
        /// </summary>
        public decimal fecha_operacion_calidad { get; set; }

        /// <summary>
        /// codigo_producto
        /// </summary>
        public decimal codigo_producto { get; set; }

        ///// <summary>
        ///// codigo_tanque_alm
        ///// </summary>
        //public decimal codigo_tanque_alm { get; set; }

        /// <summary>
        /// codigo_punto_trans_custodio
        /// </summary>
        public string codigo_punto_trans_custodio { get; set; }

        ///// <summary>
        ///// codigo_transporte
        ///// </summary>
        //public decimal codigo_transporte { get; set; }

        ///// <summary>
        ///// codigo_transporte_secundario
        ///// </summary>
        //public decimal codigo_transporte_secundario { get; set; }

        ///// <summary>
        ///// codigo_transporte_barcode
        ///// </summary>
        //public string codigo_transporte_barcode { get; set; }

        /// <summary>
        /// tipo_medio_transporte
        /// </summary>
        public decimal tipo_medio_transporte { get; set; }

        /// <summary>
        /// volumen
        /// </summary>
        public decimal volumen { get; set; }

        /// <summary>
        /// volumen_unidad_medida
        /// </summary>
        public decimal volumen_unidad_medida { get; set; }

        /// <summary>
        /// numero_lote
        /// </summary>
        public string numero_lote { get; set; }

        /// <summary>
        /// Volúmen de la muestra del lote.
        /// </summary>
        public decimal volumen_muestra { get; set; }

        /// <summary>
        /// codigo_marca_producto para importadores lubricantes.
        /// </summary>
        public decimal codigo_marca_producto { get; set; }

        /// <summary>
        /// nombre_producto para lubricantes.
        /// </summary>
        public string nombre_producto { get; set; }

        /// <summary>
        /// codigo_moneda que incluye el lote.
        /// </summary>
        public decimal codigo_moneda { get; set; }

        /// <summary>
        /// Precio que incluye el lote.
        /// </summary>
        public decimal precio { get; set; }

        /// <summary>
        /// Resolución que incluye el lote.
        /// </summary>
        public string resolución { get; set; }

        /// <summary>
        /// Lista de detalle del lote
        /// </summary>
        public List<LoteDetalle> pruebas_calidad { get; set; }

    }

    public class LoteDetalle
    {

        /// <summary>
        /// codigo_prueba_espec
        /// </summary>
        public decimal codigo_prueba_espec { get; set; }

        /// <summary>
        /// valor_reportado
        /// </summary>
        public string valor_reportado { get; set; }

        /// <summary>
        /// codigo_unidad_medida
        /// </summary>
        public decimal codigo_unidad_medida { get; set; }

        /// <summary>
        /// valor_metodo_astm
        /// </summary>
        public decimal valor_metodo_astm { get; set; }

        /// <summary>
        /// valor_metodo_astm
        /// </summary>
        public decimal valor_referencial { get; set; }
    }
}
