namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public string oEstado
        {
            get
            {
                if (Estado != false)
                {
                    return ("Activo");
                }
                else
                {
                    return ("Inactivo");
                }
            }
        }
    }
}
