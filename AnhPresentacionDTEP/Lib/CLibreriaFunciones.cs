

using AnhHydroTalleresOpeGarrafasPresentacion.Entidades;
using AnhHydroTalleresOpeGarrafasPresentacion.Parametros;
using DevExpress.Web;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Librerias
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;

   // using Ionic.Zip;

    public class CLibreriaFunciones
    {
        #region Metodos
        
        public static Dictionary<decimal, string> ListaMeses()
        {
            Dictionary<decimal, string> dicMeses = new Dictionary<decimal, string>();
            dicMeses.Add(0, "SELECCIONE...");
            dicMeses.Add(1,"ENERO");
            dicMeses.Add(2, "FEBRERO");
            dicMeses.Add(3, "MARZO");
            dicMeses.Add(4, "ABRIL");
            dicMeses.Add(5, "MAYO");
            dicMeses.Add(6, "JUNIO");
            dicMeses.Add(7, "JULIO");
            dicMeses.Add(8, "AGOSTO");
            dicMeses.Add(9, "SEPTIEMBRE");
            dicMeses.Add(10, "OCTUBRE");
            dicMeses.Add(11, "NOVIEMBRE"); 
            dicMeses.Add(12, "DICIEMBRE");
            return dicMeses;
        }
        public static string ObtenerNombreMes(decimal decNumeroMes) {
            if (decNumeroMes > 0 && decNumeroMes <= 12)
            {
                return ListaMeses()[decNumeroMes];
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ObtenerHashMd5(byte[] input)
        {
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(input);
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }
        ///Para validacion de hash
        /// string md5 = ObtenerMd5(auxArchivo.ARCHIVO_BLOB);
        ///        bool comparado = VerificarHashMd5(auxArchivo.ARCHIVO_BLOB, "cc7997d9f80027b82dde4d721d9fe09c");
        ///        Console.WriteLine(md5+"     ----->      "+comparado.ToString());
        public static bool VerificarHashMd5(byte[] input, string hash)
        {
            string hashOfInput = ObtenerHashMd5(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}