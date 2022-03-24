using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class Portafolio : ICloneable
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }
        public bool clone { get; set; }
        public string Referencia { get; set; }
        public int RowId_Item_Ext { get; set; }
        public string Descripcion { get; set; }
        public int Peso { get; set; }
        public string UnidadEmpaque { get; set; }
        public string Linea { get; set; }
        public string Sublinea { get; set; }
        public string SublineaId { get; set; }
        public int CantidadProductoSeleccionado { get; set; }
        public string Medicado { get; set; }
        public double PrecioUnidad { get; set; }
        public string MedicadoSeleccionado { get; set; }
        public int RowId_Item_Ext_MedicadoSeleccionado { get; set; }
        public int CantidadMedicamentoSeleccionado { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public bool DisabledCantidadMedicado { get; set; } = false;

        [System.Text.Json.Serialization.JsonIgnore]
        public bool DisabledEnviarPedido { get; set; } = false;

        [System.Text.Json.Serialization.JsonIgnore]
        public bool ProductoMedicado { get; set; } = false;

        [System.Text.Json.Serialization.JsonIgnore]
        public List<Medicado> Medicados { get; set; } = new List<Medicado>();
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
