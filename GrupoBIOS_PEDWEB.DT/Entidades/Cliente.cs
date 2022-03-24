namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; }
        public string Nit { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }

        public string NombreNit
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NombreEmpresa))
                {
                    return Nit;
                }
                else
                {
                    return ($"{Nit}" + "-" + $"{NombreEmpresa}");
                }
            }
        }
    }
}
