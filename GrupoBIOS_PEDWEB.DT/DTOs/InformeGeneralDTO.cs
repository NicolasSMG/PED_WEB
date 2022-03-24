using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class InformeGeneralDTO
    {
        public List<Pedido> Pedidos { get; set; }
        public List<DetallePedido> DetallePedidos { get; set; }

    }
}
