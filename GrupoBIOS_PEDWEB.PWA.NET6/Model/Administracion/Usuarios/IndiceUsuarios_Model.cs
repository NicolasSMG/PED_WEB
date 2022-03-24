using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.PWA.Model
{
    public class IndiceUsuarios_Model : IIndiceUsuariosModel
    {
        private readonly ISettings settings;
        private readonly IConexionRest conexionRest;
        public IndiceUsuarios_Model(ISettings settings
                                        , IConexionRest conexionRest)
        {
            this.settings = settings;
            this.conexionRest = conexionRest;
        }


        public async Task<List<Usuario>> CargarUsuariosOnline(int CompaniaId)
        {
            var ApiUrl = await settings.GetApiUrl();
            var responseHttp = await conexionRest.Get<List<Usuario>>($"{ApiUrl}/Usuarios/FiltrarUsuarios/{CompaniaId}");
            if (!responseHttp.Error)
            {
                return responseHttp.Response;
            }
            return new List<Usuario>();
        }
    }
}
