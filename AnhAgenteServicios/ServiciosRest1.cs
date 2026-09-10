using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhAgenteServicios
{
    using System.Configuration;

    public class ServiciosRest
    {
        

        #region Servicios Admninistrador Hydro
        public static readonly string ServicioAdminHydro = Convert.ToString(ConfigurationSettings.AppSettings["ServicioAdministradorHydro"]);
        public static readonly string SvcListaPerfiles = "/ListaPerfiles/";
        public static readonly string SvcListaMenues = "/ListaMenus/";
        public static readonly string SvcListaMenusPorUsuario = "/ListaMenusPorUsuario/";
        public static readonly string SvcListaUsuariosPorModulo = "/ListaUsuariosPorModulo/";
        #endregion
        #region Servicios SIRH
        public static readonly string ServicioJsonSIRH =
            Convert.ToString(ConfigurationSettings.AppSettings["ServicioJsonSirh"]);
        //public static readonly string SvcListaFuncionarioAcf = "/ListaFuncionarioAcf/";        
        public static readonly string SvcDatosFuncionarioANH = "/EDatosFuncionarioANH/";
        public static readonly string SvcOrganigramaVigente = "/OrganigramaVigente/";
        public static readonly string SvcBuscarFuncionarioPorCriterio = "/EBuscarFuncionarioPorCriterio/";
        public static readonly string SvcFotoPerfilFuncionario = "/EFotoPerfilFuncionario/";
        public static readonly string SvcListaPersonalAnh = "/ListaPersonal/";
        
        #endregion

        //#region servicios 

        //public static readonly string ServicioSigde = Convert.ToString(ConfigurationManager.AppSettings["StrServicioSigde"]);
        //#region Consultas

        //public static readonly string SvcListaArchivoBinario = "/ListaArchivoBinario/";
        //public static readonly string SvcListaPlantilla = "/ListaPlantilla/";
        //public static readonly string SvcListaCarpetas = "/ListaArchivoCarpetas/";
        //public static readonly string SvcListaControl = "/ListaControl/";
        //public static readonly string SvcListaTipoDocumento = "/ListaTipoDocumento/";
        //public static readonly string SvcListaCabecera = "/ListaCompPlantilla/";
        //public static readonly string SvcListaServicio = "/ListaServicio/";
        //public static readonly string SvcListaDatosCabecera = "/ListaEtiquetayDatosdeCabecera/";
        //public static readonly string SvcListaDocumento = "/ListaDocumento/";
        //public static readonly string SvcListaCabeceraDocumento = "/ListaCabeceraDocumento/";
        //public static readonly string SvcListaArchivoAdjunto = "/ListaArchivoAdjunto/";
        //public static readonly string SvcListaDirectorio = "/ListaDirectorio/";
        //public static readonly string SvcListaCompartido = "/ListaCompartido/";
        //public static readonly string SvcListaArchivoDigitalSiscondoc = "/ListaArchivoDigitalSiscondoc/";
        //public static readonly string SvcListaNotaInternaSiscondoc = "/ListaNotaInternaSiscondoc/";
        //public static readonly string SvcListaArchivoBlob = "/ListaArchivoBlob/";

        //public static readonly string SvcDatosCodigoBarras = "/DatosCodigoBarras/";
        

        //#endregion

        //#region Listados
        //        
        //#endregion

        //#region Gestion
        //public static readonly string SvcInsertarDirectorio = "/InsertarDirectorio/";
        //public static readonly string SvcModificarDirectorio = "/ModificarDirectorio/";
        //public static readonly string SvcEliminarDirectorio = "/EliminarDirectorio/";

        //public static readonly string SvcInsertarControl = "/InsertarControl/";
        //public static readonly string SvcModificarControl = "/ModificarControl/";
        //public static readonly string SvcEliminarControl = "/EliminarControl/";

        //public static readonly string SvcInsertarTipoDocumento = "/InsertarTipoDocumento/";
        //public static readonly string SvcModificarTipoDocumento = "/ModificarTipoDocumento/";
        //public static readonly string SvcEliminarTipoDocumento = "/EliminarTipoDocumento/";

        //public static readonly string SvcInsertarCompPlantilla = "/InsertarCompPlantilla/";
        //public static readonly string SvcModificarCompPlantilla = "/ModificarCompPlantilla/";
        //public static readonly string SvcEliminarCompPlantilla = "/EliminarCompPlantilla/";

        //public static readonly string SvcInsertarDocumento = "/InsertarDocumento/";
        //public static readonly string SvcModificarDocumento = "/ModificarDocumento/";
        //public static readonly string SvcEliminarDocumento = "/EliminarDocumento/";

        //public static readonly string SvcInsertarArchivo = "/InsertarArchivoDigital/";
        //public static readonly string SvcModificarArchivo = "/ModificarArchivo/";
        //public static readonly string SvcEliminarArchivo = "/EliminarArchivo/";

        //public static readonly string SvcInsertarMetadata = "/InsertarMetadata/";
        //public static readonly string SvcModificarMetadata = "/ModificarMetadata/";
        //public static readonly string SvcEliminarMatadata = "/EliminarMatadata/";

        //public static readonly string SvcInsertarPlantilla = "/InsertarPlantilla/";
        //public static readonly string SvcModificarPlantilla = "/ModificarPlantilla/";
        //public static readonly string SvcEliminarPlantilla = "/EliminarPlantilla/";

        //public static readonly string SvcInsertarArchivoAdjunto = "/InsertarArchivoAdjunto/";
        //public static readonly string SvcModificarArchivoAdjunto = "/ModificarArchivoAdjunto/";

        //public static readonly string SvcInsertarCompartido = "/InsertarCompartido/";
        //public static readonly string SvcEliminarCompartido = "/EliminarCompartido/";

        //public static readonly string SvcInsertarArchivoDigitalB = "/InsertarArchivoDigitalB/";

        //#endregion
        //#endregion
    }
}
