using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class FiltrosPedido
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosAMostrar { get; set; } = 10;
        public int CompaniaId { get; set; }
        public string? FiltroCliente { get; set; }
        public string? FiltroPedidoSIESA { get; set; }
        public string? FiltroPuntoEnvio { get; set; }
        public string? FiltroNumeroPedido { get; set; }
        public string? FiltroSucursal { get; set; }
    }
}
