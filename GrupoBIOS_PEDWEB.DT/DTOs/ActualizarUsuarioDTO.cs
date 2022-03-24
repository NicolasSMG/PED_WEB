namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class ActualizarUsuarioDTO
    {
        public FormularioUsuarioDTO FormularioUsuarioDTO { get; set; }
        public UsuarioDTO UsuarioDTO { get; set; }
        public ActualizarUsuarioDTO()
        {
            //Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }
    }
}
