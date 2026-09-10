using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhPresentacionDTEP.Parametros
{
    public partial class O_OBTIENE_ENTIDADES_USR_CTY
    {
        public string NIT { get; set; }//*
        public Nullable<decimal> ID_ENTIDAD_PADRE { get; set; }//*
        public string DENOMINACION { get; set; }//*
        public string DIRECCION { get; set; }//*
        public Nullable<decimal> LATITUD { get; set; }//*
        public Nullable<decimal> LONGITUD { get; set; }//*
        public string ACTIVIDAD { get; set; }//*
        public string NRO_LICENCIA { get; set; }
        public string REPRESENTANTE_LEGAL { get; set; }//*
        public string DEPARTAMENTO { get; set; }//*
        public decimal ID_ENTIDAD { get; set; }//*
        public string VIGENCIA { get; set; }
        public string DENOMINACION_PADRE { get; set; }//*
        public decimal ID_ACTIVIDAD { get; set; }//*
        public decimal ID_CONSUMIDOR { get; set; }//*
        public Nullable<decimal> ID_MUNICIPIO { get; set; }
        public Nullable<decimal> ID_LOCALIDAD { get; set; }
        public Nullable<decimal> ID_DEPARTAMENTO { get; set; }//*
        public Nullable<decimal> ID_MUNICIPIO_PADRE { get; set; }
        public Nullable<decimal> ID_LOCALIDAD_PADRE { get; set; }
        public Nullable<decimal> ID_TIPO_SOCIEDAD { get; set; }
        public Nullable<decimal> ID_TIPO_SOCIEDAD_PADRE { get; set; }
        public string MUNICIPIO { get; set; }
        public string LOCALIDAD { get; set; }
        public string MUNICIPIO_PADRE { get; set; }
        public string LOCALIDAD_PADRE { get; set; }
        public string OBJETO { get; set; }//*
        public string TELEFONOS { get; set; }//*
        public string AMBITO_OPERACION { get; set; }
        public string AMBITO_OPERACION_PADRE { get; set; }
        public string TIPO_SOCIEDAD { get; set; }
        public string TIPO_SOCIEDAD_PADRE { get; set; }
        public string NOMBRES_PROPIETARIO { get; set; }//*
        public string CI_PROPIETARIO { get; set; }//*
        public string NOMBRES_REPRESENTANTE { get; set; }//*
        public string CI_REPRESENTANTE { get; set; }//*
        public Nullable<decimal> ID_USUARIO_ASIGNADO { get; set; }
        public string USUARIO_ASIGNADO { get; set; }
        public string LIC_VENCIMIENTO { get; set; }
        public string LIC_PROD { get; set; }
        public string LIC_ID_PROD { get; set; }
    }
}