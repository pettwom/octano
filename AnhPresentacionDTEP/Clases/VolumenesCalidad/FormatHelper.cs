using System;
using System.Globalization;

namespace AnhPresentacionDTEP.Clases.VolumenesCalidad
{
    /// <summary>
    /// Helper estático para formateo y parseo de valores con soporte de cultura.
    /// Reemplaza el patrón repetido Convert.ToDecimal(s == "," ? texto.Replace(".", ",") : texto.Replace(",", ".")).
    /// </summary>
    public static class FormatHelper
    {
        /// <summary>
        /// Parsea un string a decimal detectando automáticamente el separador decimal
        /// según la cultura actual del hilo.
        /// </summary>
        /// <param name="valor">Cadena con el valor decimal.</param>
        /// <returns>Valor decimal parseado.</returns>
        /// <exception cref="FormatException">Si el valor no es un decimal válido.</exception>
        public static decimal ToDecimal(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                throw new FormatException("El valor no puede ser nulo o vacío.");

            var separador = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            valor = separador == ","
                ? valor.Replace(".", ",")
                : valor.Replace(",", ".");

            return Convert.ToDecimal(valor);
        }
    }
}
