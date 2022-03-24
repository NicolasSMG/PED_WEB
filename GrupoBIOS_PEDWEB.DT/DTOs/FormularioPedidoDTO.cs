using GrupoBIOS_PEDWEB.DT.Entidades;
using System.Collections.Generic;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class FormularioPedidoDTO
    {
        public List<Sucursal> Sucursales { get; set; }
        public List<PuntoEnvio> PuntosDeEnvio { get; set; }
        public List<Portafolio> ListaPortafolios { get; set; }
        public List<Cliente> ListaClientes { get; set; }
    }
}
