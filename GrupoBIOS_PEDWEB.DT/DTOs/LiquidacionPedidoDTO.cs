using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class LiquidacionPedidoDTO
    {
        public List<LiquidacionPedido> LiquidacionPedidos { get; set; }
        public bool Preliquidacion { get; set; }
    }
}
