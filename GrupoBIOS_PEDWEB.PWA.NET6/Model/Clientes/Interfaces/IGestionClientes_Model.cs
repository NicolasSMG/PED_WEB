namespace GrupoBIOS_PEDWEB.PWA.Model.Clientes.Interfaces
{
    public interface IGestionClientes_Model
    {
        Task<List<DT.Entidades.Cliente>> ConsultarClientes();
        Task<List<DT.Entidades.Cliente>> ConsultarClientesPorCompania(int CompaniaId, string nitFijo);
        Task<int> ConsultarRowId(string Nit, int CompaniaId);
        Task<int> ConsultarNitPorRowId(int RowId);
    }
}
