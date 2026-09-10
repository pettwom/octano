using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace AnhPresentacionDTEP.Lib
{
    public class CMessageBoxManager
    {
        public static int ERROR_MESSAGE = 1;
        public static int WARNING_MESSAGE = 2;
        public static int INFO_MESSAGE = 3;
        public static int OK_MESSAGE = 4;

        private const int _ERROR_MESSAGE = 1;
        private const int _WARNING_MESSAGE = 2;
        private const int _INFO_MESSAGE = 3;
        private const int _OK_MESSAGE = 4;

        /// <summary>
        /// Muestra un mensaje en la página web en un objeto HtmlGenericControl. 
        /// </summary>
        /// <param name="message">Texto del mensaje que se mostrará</param>
        /// <param name="targetControl">El elemento en el que se mostrará el mensaje</param>
        /// <param name="messageType">Indica el tipo de mensaje que se mostrará. 
        /// Puede ser: ERROR_MESSAGE, WARNING_MESSAGE, INFO_MESSAGE, OK_MESSAGE. 
        /// Por defecto es de tipo INFO_MESSAGE
        /// </param>
        /// <example>CMessageBoxManager.ShowMessageDialog("Ocurrio un error", ref targetControl, CMessageBoxManager.ERROR_MESSAGE)</example>
        public static void ShowMessageDialog(string message, ref HtmlGenericControl targetControl, int messageType = _INFO_MESSAGE)
        {
            targetControl.InnerText = message;
            targetControl.Style["display"] = "block";
            switch (messageType)
            {
                case _ERROR_MESSAGE:
                    targetControl.Style["background-color"] = "#FFF0F0";
                    targetControl.Style["border-color"] = "#FF4F4F";
                    targetControl.Style["color"] = "#940000";
                    break;
                case _WARNING_MESSAGE:
                    targetControl.Style["background-color"] = "#FDFFCA";
                    targetControl.Style["border-color"] = "#DBBC15;";
                    targetControl.Style["color"] = "#726000";
                    break;
                case _INFO_MESSAGE:
                    targetControl.Style["background-color"] = "#EBEFF3";
                    targetControl.Style["border-color"] = "#92ADF5";
                    targetControl.Style["color"] = "#000A31";
                    break;
                case _OK_MESSAGE:
                    targetControl.Style["background-color"] = "#EAFFE5";
                    targetControl.Style["border-color"] = "#C5ECB2";
                    targetControl.Style["color"] = "#5A8641";
                    break;
            }
        }

        public static void HideMessageDialog(ref HtmlGenericControl targetControl)
        {
            targetControl.InnerText = "";
            targetControl.Style["display"] = "none";
        }
    }
}