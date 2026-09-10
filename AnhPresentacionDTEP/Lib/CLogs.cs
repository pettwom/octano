using AnhClases;
using AnhPresentacionDTEP.Parametros;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
namespace AnhPresentacionDTEP.Lib
{
    public class CLogs
    {
        private string _strPrefijo;
        private string _strFecha;
        private string _logPath;
        private string _strIp;
        public CLogs(string serverMapPath)
        {
            _strPrefijo = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " => ";
            string strAnho = DateTime.Now.Year.ToString();
            string strMes = DateTime.Now.Month.ToString();
            strMes = strMes.Length == 1 ? "0" + strMes : strMes;
            string strDia = DateTime.Now.Day.ToString();
            _strFecha = strAnho + strMes + strDia;
            _logPath = serverMapPath + "/CLogs/";
        }
        public CLogs(string serverMapPath, string strIp)
        {
            _strPrefijo = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " => ";
            string strAnho = DateTime.Now.Year.ToString();
            string strMes = DateTime.Now.Month.ToString();
            strMes = strMes.Length == 1 ? "0" + strMes : strMes;
            string strDia = DateTime.Now.Day.ToString();
            _strFecha = strAnho + strMes + strDia;
            _logPath = serverMapPath + "/CLogs/";
            _strIp = strIp;
        }
        public void Error(Exception exp)
        {
            if (exp.Message != null)
            {
                if (!exp.Message.StartsWith("Subproceso anulado") && !exp.Message.StartsWith("Thread was being aborted"))
                {
                    //StreamWriter sw = new StreamWriter(_logPath + _strFecha, true);
                    string strMensaje = "E " + _strPrefijo + exp.Message + " " + exp.StackTrace;
                    //sw.WriteLine(strMensaje);
                    //sw.Flush();
                    //sw.Close();
                    try
                    {
                        cCorreo.mEnviarEmail(
                            CParametrosHydro.StrServidorDireccion,
                            CParametrosHydro.IntServidorPuerto,
                            CParametrosHydro.StrUsuarioLogin,
                            CParametrosHydro.StrUsuarioPassword,
                            CParametrosHydro.StrUsuarioDe,
                            CParametrosHydro.StrUsuarioCco,
                            "Error Detectado en el OCTANO",
                            strMensaje,
                            false,
                            true
                        );
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void Error(string strMensaje)
        {
            //StreamWriter sw = new StreamWriter(_logPath + _strFecha, true);
            //sw.WriteLine("E " + _strPrefijo + strMensaje);
            //sw.Flush();
            //sw.Close();
            try
            {
                cCorreo.mEnviarEmail(
                    CParametrosHydro.StrServidorDireccion,
                    CParametrosHydro.IntServidorPuerto,
                    CParametrosHydro.StrUsuarioLogin,
                    CParametrosHydro.StrUsuarioPassword,
                    CParametrosHydro.StrUsuarioDe,
                    CParametrosHydro.StrUsuarioCco,
                    "Error Detectado en el OCTANO",
                    strMensaje,
                    false,
                    true
                );
            }
            catch (Exception)
            {
            }
        }

        public void Warning(string exp)
        {
            StreamWriter sw = new StreamWriter(_logPath + _strFecha, true);
            sw.WriteLine("W " + _strPrefijo + exp);
            sw.Flush();
            sw.Close();
        }

        public void Trace(string exp)
        {
            StreamWriter sw = new StreamWriter(_logPath + _strFecha, true);
            sw.WriteLine("T " + _strPrefijo + exp);
            sw.Flush();
            sw.Close();
        }
    }
}