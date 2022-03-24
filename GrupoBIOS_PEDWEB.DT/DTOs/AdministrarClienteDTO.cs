using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class AdministrarClienteDTO
    {
        public AdministrarCliente AdministrarCliente { get; set; }
        public List<Compania> CompaniasAcceso { get; set; } = new();
        public List<Sucursal> SucursalesPedidosSeleccionados { get; set; } = new();
        public List<Sucursal> SucursalesPedidosSinSeleccionar { get; set; } = new();
        public List<ClienteHijo> ClientesHijos { get; set; } = new();
        public List<Cliente> ListadoClientes { get; set; } = new();
    }
}
