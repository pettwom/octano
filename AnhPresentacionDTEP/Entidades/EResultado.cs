using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhPresentacionDTEP.Entidades
{
    public class EResultado
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public Object oResultado { get; set; }
    }

    public enum EnumTiposMensajeAlerta
    {
        Error,
        Alerta,
        Correcto,
        Limpiar
    }

    public class EResultadoConfigHidrocarburo
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_AUTORIZACION_COND_CTY> OResultado { get; set; }
    }

    public partial class O_LISTA_AUTORIZACION_COND_CTY
    {
        public decimal ID_AUTORIZACION_CONDICION { get; set; }
        public decimal TIPO_CONDICION { get; set; }

        public decimal ID_SUB_IDENTIFICADOR { get; set; }
        public string SUBID_NOMBRE { get; set; }

        public decimal ID_IDENTIFICADOR { get; set; }
        public string NOMBRE { get; set; }
        public string CODIGO_NOMBRE { get; set; }

        public string OBSERVACIONES { get; set; }
        public decimal ESTADO { get; set; }

        public string DESCRIPCION { get; set; }
        public string CODIGO { get; set; }

        public System.DateTime APP_FECHA_REGISTRO { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
    }
}