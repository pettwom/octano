using AnhAgenteServicios.ServicioHydroSesion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AnhPresentacionDTEP.Parametros
{
    public static class CVariablesSesion
    {
        #region Variables Usuario

        public static readonly string UsuarioId = "UsuarioId";
        public static readonly string UsuarioPerfil = "UsuarioPerfil";
        public static readonly string UsuarioPerfilId = "UsuarioPerfilId";
        public static readonly string UsuarioNombre = "UsuarioNombre";
        public static readonly string UsuarioPassword = "UsuarioPassword";
        public static readonly string UsuarioNombreCompleto = "UsuarioNombreCompleto";
        public static readonly string IdentificadorEntidad = "IdentificadorEntidad";
        public static readonly string NombreEntidad = "NombreEntidad";
        public static readonly string DatosUsuario = "DatosUsuario";
        public static readonly string ValidarCI = "ValidarCI";
        public static readonly string NombreDireccion = "NombreDireccion";
        public static readonly string IdEntidad = "IdEntidad";
        public static readonly string IdEntidadPadre = "IdEntidadPadre";
        public static readonly string IsSuperAdministrador = "IsSuperAdministrador";
        public static readonly string IsSupervisor = "IsSupervisor";
        public static readonly string IsAdminDteyp = "IsAdminDteyp";
        public static readonly string IsFuncionario = "IsFuncionario";
        public static readonly string IdAutenticacion = "HNA_oirausU";
        public static readonly string PerfilUsuario = "Operador";
        public static readonly string UsuarioAdministrador = "UsuarioAdministrador";
        public static readonly string NombreEntidadAgencia = "NombreEntidadAgencia";
        public static readonly string IdEntidadArbol = "IdEntidadArbol";
        public static readonly string IdActividadArbol = "IdActividadArbol";
        public static readonly string ObjFuncionarioANH = "ObjFuncionarioANH";
        public static readonly string srtCorreo = "srtCorreo";
        public static readonly string IdUsuario = "IdUsuario";
        

        /// <summary>
        /// Perfiles de usuario para control de calidad
        /// </summary>
        public static readonly string varPerfilAdministrador = "ADMINISTRADOR HYDRO-OCTANO";
        public static readonly string varPerfilSupervisor = "SUPERVISOR HYDRO-OCTANO";
        public static readonly string varPerfilTecnicoOperador = "TECNICO OPERADOR HYDRO-OCTANO";
        public static readonly string varPerfilFuncionarioANH = "FUNCIONARIO ANH HYDRO-OCTANO";
        public static readonly string varPerfilOperadorRefineria = "OPERADOR REFINERIA HYDRO-OCTANO";
        public static readonly string varPerfilOperadorPlantasAlmacenaje = "OPERADOR PACL HYDRO-OCTANO";
        public static readonly string DocumentoDig = "DOCUMENTO";
        /// <summary>
        /// Listado de usuario de acceso al sistema de control de calidad
        /// </summary>
        //public static readonly List<O_USUARIO_CTY> listaUsuariosAcceso = new List<O_USUARIO_CTY>()
        //                                                  {
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "WALTER ARTEAGA", ESTADO = 1, ID_USUARIO = 592, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "DENIS GONZALES", ESTADO = 1, ID_USUARIO = 573, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "DIEGO VERA POZO", ESTADO = 1, ID_USUARIO = 572, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "JOEL CALLAU", ESTADO = 1, ID_USUARIO = 581, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "OMAR VISCARRA", ESTADO = 1, ID_USUARIO = 593, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "ROXANA TAPIA", ESTADO = 1, ID_USUARIO = 594, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "GUSTAVO CARRASCO ", ESTADO = 1, ID_USUARIO = 595, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "MARCO AREVALO", ESTADO = 1, ID_USUARIO = 596, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "EDWIN MERIDA", ESTADO = 1, ID_USUARIO = 597, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "LUIS BUSTILLOS", ESTADO = 1, ID_USUARIO = 598, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "CARLOS AYLLON", ESTADO = 1, ID_USUARIO = 574, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "JUAN CARLOS LOPEZ", ESTADO = 1, ID_USUARIO = 575, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "HERNAN YUJRA", ESTADO = 1, ID_USUARIO = 579, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "EDDY BANDA", ESTADO = 1, ID_USUARIO = 580, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "ALEXANDER BENITEZ", ESTADO = 1, ID_USUARIO = 345, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "EINARD JOFFRE", ESTADO = 1, ID_USUARIO = 578, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 50,NOMBRE_COMPLETO = "USUARIO ORO NEGRO", ESTADO = 1, ID_USUARIO = 582, PERFIL = varPerfilOperadorEmpresa, PERFIL_ID = 27},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 52,NOMBRE_COMPLETO = "USUARIO GUALBERTO VILLARROEL", ESTADO = 1, ID_USUARIO = 584, PERFIL = varPerfilOperadorEmpresa, PERFIL_ID = 27},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 53,NOMBRE_COMPLETO = "USUARIO ELDER BELL", ESTADO = 1, ID_USUARIO = 585, PERFIL = varPerfilOperadorEmpresa, PERFIL_ID = 27},
        //                                                      new O_USUARIO_CTY(){ID_ENTIDAD = 53,NOMBRE_COMPLETO = "ALEXANDER CUELLAR", ESTADO = 1, ID_USUARIO = 599, PERFIL = varPerfilOperadorEmpresa, PERFIL_ID = 27}
        //                                                  };

        public static readonly List<O_USUARIO_CTY> listaUsuariosAcceso = new List<O_USUARIO_CTY>()
                                                          {
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "WALTER ARTEAGA", ESTADO = 1, ID_USUARIO = 1197, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "DENIS GONZALES", ESTADO = 1, ID_USUARIO = 1196, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "DIEGO VERA POZO", ESTADO = 1, ID_USUARIO = 1270, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "JOEL CALLAU", ESTADO = 1, ID_USUARIO = 1265, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "OMAR VISCARRA", ESTADO = 1, ID_USUARIO = 1143, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "ROXANA TAPIA", ESTADO = 1, ID_USUARIO = 1195, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "GUSTAVO CARRASCO ", ESTADO = 1, ID_USUARIO = 1194, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "MARCO AREVALO", ESTADO = 1, ID_USUARIO = 1193, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "EDWIN MERIDA", ESTADO = 1, ID_USUARIO = 1192, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "LUIS BUSTILLOS", ESTADO = 1, ID_USUARIO = 1191, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "CARLOS AYLLON", ESTADO = 1, ID_USUARIO = 1190, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2069,NOMBRE_COMPLETO = "JUAN CARLOS LOPEZ", ESTADO = 1, ID_USUARIO = 1189, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              //new O_USUARIO_CTY(){ID_ENTIDAD = 50,NOMBRE_COMPLETO = "JUAN CARLOS LOPEZ", ESTADO = 1, ID_USUARIO = 1189, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},

                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2069,NOMBRE_COMPLETO = "HERNAN YUJRA", ESTADO = 1, ID_USUARIO = 645, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              //new O_USUARIO_CTY(){ID_ENTIDAD = 50,NOMBRE_COMPLETO = "HERNAN YUJRA", ESTADO = 1, ID_USUARIO = 645, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},

                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "EDDY BANDA", ESTADO = 1, ID_USUARIO = 1002, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "ALEXANDER BENITEZ", ESTADO = 1, ID_USUARIO = 354, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2169,NOMBRE_COMPLETO = "EINARD JOFFRE", ESTADO = 1, ID_USUARIO = 1187, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},

                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "WALTER ARTEAGA", ESTADO = 1, ID_USUARIO = 1197, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "DENIS GONZALES", ESTADO = 1, ID_USUARIO = 1196, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "DIEGO VERA POZO", ESTADO = 1, ID_USUARIO = 1270, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "JOEL CALLAU", ESTADO = 1, ID_USUARIO = 1265, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "OMAR VISCARRA", ESTADO = 1, ID_USUARIO = 1143, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "ROXANA TAPIA", ESTADO = 1, ID_USUARIO = 1195, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "GUSTAVO CARRASCO ", ESTADO = 1, ID_USUARIO = 1194, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "MARCO AREVALO", ESTADO = 1, ID_USUARIO = 1193, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "EDWIN MERIDA", ESTADO = 1, ID_USUARIO = 1192, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "LUIS BUSTILLOS", ESTADO = 1, ID_USUARIO = 1191, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "CARLOS AYLLON", ESTADO = 1, ID_USUARIO = 1190, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "JUAN CARLOS LOPEZ", ESTADO = 1, ID_USUARIO = 1189, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "HERNAN YUJRA", ESTADO = 1, ID_USUARIO = 645, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "EDDY BANDA", ESTADO = 1, ID_USUARIO = 1002, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "ALEXANDER BENITEZ", ESTADO = 1, ID_USUARIO = 354, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 1,NOMBRE_COMPLETO = "EINARD JOFFRE", ESTADO = 1, ID_USUARIO = 1187, PERFIL = varPerfilAdministrador, PERFIL_ID = 23},

                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2194,NOMBRE_COMPLETO = "USUARIO ORO NEGRO", ESTADO = 1, ID_USUARIO = 1241, PERFIL = varPerfilOperadorRefineria, PERFIL_ID = 27},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2069,NOMBRE_COMPLETO = "USUARIO GUALBERTO VILLARROEL", ESTADO = 1, ID_USUARIO = 1268, PERFIL = varPerfilOperadorRefineria, PERFIL_ID = 27},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2070,NOMBRE_COMPLETO = "USUARIO ELDER BELL", ESTADO = 1, ID_USUARIO = 1269, PERFIL = varPerfilOperadorRefineria, PERFIL_ID = 27},
                                                              new O_USUARIO_CTY(){ID_ENTIDAD = 2308,NOMBRE_COMPLETO = "ALEXANDER CUELLAR", ESTADO = 1, ID_USUARIO = 1188, PERFIL = varPerfilOperadorPlantasAlmacenaje, PERFIL_ID = 28}
                                                          };
        //new O_USUARIO_CTY(){ID_ENTIDAD = 2070,NOMBRE_COMPLETO = "ALEXANDER CUELLAR", ESTADO = 1, ID_USUARIO = 1188, PERFIL = varPerfilOperadorEmpresa, PERFIL_ID = 27}


        #endregion

        #region Variables Documentacion

        public static readonly string DocumentoId = "DocumentoId";

        #endregion

        #region Variables Correspondencia

        public static readonly string CorrespondenciaId = "CorrespondenciaId";
        public static readonly string CorrespondenciaModalidad = "CorrespondenciaModalidad";
        public static readonly string CorrespondenciaOpcion = "CorrespondenciaOpcion";
        public static readonly string CorrespondenciaTipoEnvio = "CorrespondenciaTipoEnvio";
        public static readonly string CorrespodenciaCite = "CorrespondenciaCite";
        public static readonly string CorrespondenciaIdCliente = "id";

        #endregion

        public static readonly string ListaUsuariosModificarPermiso = "ListaUsuariosModificarPermiso";
        public static readonly string ListaUsuariosReplica = "ListaUsuariosReplica";
        public static readonly string ListaUsuariosEliminarPermiso = "ListaUsuariosEliminarPermiso";
        public static readonly string ListaUsuariosNewReplica = "ListaUsuariosNewReplica";

        #region Variables Siscondoc

        public static readonly string SiscondocId = "SiscondocId";
        public static readonly string SiscondoValido = "SiscondocValido";

        #endregion

        #region Variables Correo

        public static readonly string CorreoContenido = "CorreoContenido";
        public static readonly string CorreoEnviado = "CorreoEnviado";

        #endregion

        public static readonly decimal IdFuncionarioAnh = System.Convert.ToDecimal(ConfigurationManager.AppSettings["IdFuncionarioAnh"]);
        public static readonly decimal IdRegulado = System.Convert.ToDecimal(ConfigurationManager.AppSettings["IdRegulado"]);

        public static readonly string LinkCrearCuenta = ConfigurationManager.AppSettings["LinkCrearCuenta"].ToString();
        public static readonly string LinkSireHydro = ConfigurationManager.AppSettings["LinkSireHydro"].ToString();

        #region Octano

        public static readonly string IdEntidadOrigen = "IdEntidadOrigen";
        public static readonly string IdEntidadDestino = "IdEntidadDestino";
        public static readonly string TipoRegistro = "tipoRegistro";
        public static readonly string IdCantidadPadre = "IdCantidadPadre";
        public static readonly string IdUnidadMedidaVolCcal = "IdUnidadMedidaVolCcal";
        public static readonly string IdVolCcalTipoOperacion = "IdVolCcalTipoOperacion";
        public static readonly string IdTipoReporte = "IdTipoReporte";
        public static readonly string IdVolumenDatos = "IdVolumenDatos";
        public static readonly string IdTipoActividad = "IdTipoActividad";
        public static readonly string TipoActividad = "TipoActividad";

        #endregion

        #region Gestion Usuario
        public static readonly string ObjUsuario = "ObjUsuario";
        public static readonly string ListaUsuarios = "ListaUsuarios";
        public static readonly string ListaEntidades = "ListaEntidades";
        public static readonly string ObtenerEntidades = "ObtenerEntidades";

        public static decimal UsuarioSession(HttpContext contexto)
        {
            if (contexto.Session[ObjUsuario] != null)
            {
                return ((O_USUARIO_CTY)contexto.Session[ObjUsuario]).ID_USUARIO;
            }
            else
            {
                contexto.Response.Redirect("~/Sitio/Persona/wfSalir.aspx");
            }
            return 0;
        }

        public static O_USUARIO_CTY Usuario(HttpContext contexto)
        {
            if (contexto.Session[ObjUsuario] != null)
            {
                return (O_USUARIO_CTY)contexto.Session[ObjUsuario];
            }
            else
            {
                contexto.Response.Redirect("~/Sitio/Persona/wfSalir.aspx");
            }
            return null;
        }
        #endregion

        #region seguridad
        public static readonly string PermisoAltas = "PermisoAltas";
        public static readonly string PermisoBajas = "PermisoBajas";
        public static readonly string PermisoModificaciones = "PermisoModificaciones";
        public static readonly string PermisoConsulta = "PermisoConsulta";
        public static readonly string ObjetoArbol = "ObjetoArbol";
        public static readonly string IdConsumidor = "idConsumidor";
        public static readonly string objPerfilesUsauario = "objPerfilesUsauario";
        #endregion

        public static O_DATOS_FUNCIONARIO_CTY FuncionarioAnh(HttpContext contexto)
        {
            if (contexto.Session[ObjFuncionarioANH] != null)
            {
                return (O_DATOS_FUNCIONARIO_CTY)contexto.Session[ObjFuncionarioANH];
            }
            else
            {
                //contexto.Response.Redirect("~/Sitio/Persona/wfSalir.aspx");
                return null;
            }

        }


    }
}