using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Components;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Componentes.Formularios
{
    public class CambiarContrasenaComponentBase : ComponentBase
    {
        [Parameter] public string NombreUsuario { get; set; }
        [Parameter] public CambioContrasena CambioContrasena { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        [Parameter] public string Logo { get; set; }

        protected async override Task OnInitializedAsync()
        {
            StateHasChanged();
        }
        protected async Task OnDataAnnotationsValidate()
        {
            await OnValidSubmit.InvokeAsync(null);
        }
    }
}
