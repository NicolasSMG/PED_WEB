using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.CentrosOperativos.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Componentes.Formularios
{
    public class FormularioUsuarioBase : ComponentBase
    {
        [Parameter] public UsuarioDTO UsuarioDTO { get; set; }
        [Parameter] public FormularioUsuarioDTO FormularioUsuarioDTO { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        [Parameter] public EventCallback ActualizarCentroCostos { get; set; }
        [Inject] private IGestionUsuariosModel GestionUsuariosModel { get; set; }
        [Inject] private IGestionCentrosOperativos_Model CentroOperativoModel { get; set; }
        [Inject] private IMostrarMensajes MostrarMensajes { get; set; }

        protected ElementReference NombreUsuarioInput;
        protected bool MostrarFormulario = false;
        public bool EstaGuardando { get; set; } = false;
        public bool CargandoUsuario { get; set; } = false;
        public int CompaniaId { get; set; }
        public List<CentroOperativo> CentrosOperativos { get; set; }
        protected override void OnInitialized()
        {
            CentrosOperativos = FormularioUsuarioDTO.CentrosOperativos;
            UsuarioDTO.Usuario.TipoUsuarioId = (UsuarioDTO.Usuario.TipoUsuarioId == 0 ? 2 : UsuarioDTO.Usuario.TipoUsuarioId);
        }

        protected async Task OnDataAnnotationsValidate()
        {
            EstaGuardando = true;
            await OnValidSubmit.InvokeAsync(null);
            EstaGuardando = false;
        }

        protected async Task ActualizarConpañia()
        {
            if (UsuarioDTO.Usuario.CompaniaId != CompaniaId)
            {
                CompaniaId = UsuarioDTO.Usuario.CompaniaId;
                CentrosOperativos = await CentroOperativoModel.ObtenerCentroOperativoPorCompañiaAsync(CompaniaId);
                StateHasChanged();
            }

        }
        protected async Task BuscarUsuarioAD(string Usuario)
        {
            if (!string.IsNullOrEmpty(Usuario))
            {

                if (UsuarioDTO.Usuario.TipoUsuarioId == 2)
                {
                    CargandoUsuario = true;
                    var UsuarioLDAP = await GestionUsuariosModel.ObtenerUsuarioLDAPAsync(Usuario);
                    if (UsuarioLDAP != null)
                    {
                        UsuarioDTO.Usuario.PrimerNombre = UsuarioLDAP.FirstName.ToUpper();
                        UsuarioDTO.Usuario.PrimerApellido = UsuarioLDAP.LastName.ToUpper();
                        UsuarioDTO.Usuario.Email = UsuarioLDAP.EmailAddress;
                        UsuarioDTO.Usuario.Celular = UsuarioLDAP.Phone;
                    }
                    else
                    {
                        await MostrarMensajes.MostrarMensajeError("No se ha encontrado el usuario ingresado.");
                        UsuarioDTO.Usuario.NombreUsuario = "";
                        UsuarioDTO.Usuario.PrimerNombre = "";
                        UsuarioDTO.Usuario.PrimerApellido = "";
                        UsuarioDTO.Usuario.Email = "";
                        UsuarioDTO.Usuario.Celular = "";
                        await NombreUsuarioInput.FocusAsync();
                    }
                    CargandoUsuario = false;
                }
            }
        }
    }
}
