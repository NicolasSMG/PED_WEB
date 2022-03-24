using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Model.Administracion.VariablesConexion.Interfaces;
using GrupoBIOS_PEDWEB.PWA.ViewModel.Administracion.VariablesConexion.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Administracion.VariablesConexion
{
    public class GestionCompanias_ViewModel : IGestionCompanias_ViewModel
    {
        private readonly ILogger<GestionCompanias_ViewModel> _logger;
        private readonly IGestionCompanias_Model _GestionCompanias_Model;
        public GestionCompanias_ViewModel(IGestionCompanias_Model GestionCompanias_Model, ILogger<GestionCompanias_ViewModel> logger)
        {
            _GestionCompanias_Model = GestionCompanias_Model;
            _logger = logger;

        }

        public List<Compania> ListaCompanias { get; set; } = new List<Compania>();
        public FormularioCompañiaDTO FormularioCompañiaDTO { get; set; } = new FormularioCompañiaDTO();
        public BodegaDTO BodegaDTO { get; set; } = new BodegaDTO();
        public TerminosCondiciones TerminosYCondiciones { get; set; }

        public async Task ActualizarCompaniaAsync(Compania compania)
        {
            try
            {
                await _GestionCompanias_Model.ActualizarCompania(compania);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }

        }

        public async Task ConstruirFormularioCompañiaDTO(int CompañiaId)
        {
            FormularioCompañiaDTO = await _GestionCompanias_Model.ConstruirFormularioCompañiaDTO(CompañiaId);
        }
        public async Task ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId)
        {
            BodegaDTO = await _GestionCompanias_Model.ConstruirBodegaDTO(CentroOperativoId, CompaniaId);
        }
        public async Task SalvarTerminos(string TC)
        {
            FormularioCompañiaDTO.Compania.TerminosYCondiciones = TC;
        }

        public async Task CargarCompanias()
        {
            try
            {
                ListaCompanias = await _GestionCompanias_Model.CargarCompañias();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }

        public async Task<TerminosCondiciones> ConsultarTerminosYCondiciones(int IdSiesa)
        {
            try
            {
                TerminosYCondiciones = await _GestionCompanias_Model.ObtenerTerminosYCondiciones(IdSiesa);

                if (TerminosYCondiciones != null)
                {
                    return TerminosYCondiciones;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }
        public async Task GuardarCompaniaAsync(Compania compania)
        {
            try
            {
                await _GestionCompanias_Model.GuardarCompañia(compania);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
    }
}
