using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.DirectoryServices;
using System.Reflection;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.Soportes.ActiveDirectory
{
    public class ActiveDirectory : IActiveDirectory
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<IActiveDirectory> Logger;
        public ActiveDirectory(IConfiguration Configuration, ILogger<IActiveDirectory> Logger)
        {
            this.Configuration = Configuration;
            this.Logger = Logger;
        }
        public async Task<UsuarioLdap> AutenticarUsuario(string userName, string password)
        {
            try
            {
                return await Task.Run(() =>
                {
                    //Logger.LogWarning($"Credenciales de AD: DireccionDominio {Configuration["LDAP:DireccionDominio"]}, " +
                    //    $"NombreDominio {Configuration["LDAP:NombreDominio"]}, " +
                    //    $"UsuarioAdmin {userName}, " +
                    //    $"ContrasenaAdmin {password}, " +
                    //    $"AuthenticationTypes {Configuration["LDAP:AuthenticationTypes"]}");
                    // Creamos un objeto DirectoryEntry para conectarnos al directorio activo
                    //DirectoryEntry _DirectoryEntry = new DirectoryEntry("LDAP://"+ Configuration["LDAP:NombreDominio"], @"acs\" + userName, password);
                    DirectoryEntry _DirectoryEntry = new(Configuration["LDAP:DireccionDominio"], Configuration["LDAP:NombreDominio"] + @"\" + userName, password);
                    // Creamos un objeto DirectorySearcher para hacer una búsqueda en el directorio activo
                    DirectorySearcher _DirectorySearcher = new(_DirectoryEntry);
                    AgregarPropiedadesFiltro(_DirectorySearcher);
                    // Ponemos como filtro que busque el usuario actual               
                    _DirectorySearcher.Filter = "samAccountName=" + userName;
                    // Extraemos la primera coincidencia
                    SearchResult _SearchResult;
                    _SearchResult = _DirectorySearcher.FindOne();
                    // Obtenemos el objeto de ese usuario
                    DirectoryEntry Usuario = _SearchResult.GetDirectoryEntry();
                    // Obtenemos la lista de SID de los grupos a los que pertenece
                    //Usuario.RefreshCache(new string[] { "tokenGroups" });
                    if (_SearchResult.Path != null)
                    {
                        UsuarioLdap _UsuarioLdap = ObtenerModelo(_SearchResult);
                        return _UsuarioLdap;
                    }
                    return null;
                });
            }
            catch (Exception ex)
            {
                if (ex.Message != "El nombre de usuario o la contraseña no son correctos.")
                {
                    Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                }
            }
            return null;
        }
        public async Task<UsuarioLdap> BuscarUsuario(string userName)
        {
            try
            {
                var usuarioLdap = await Task.Run(() =>
                {
                    //Logger.LogWarning($"Credenciales de AD: DireccionDominio {Configuration["LDAP:DireccionDominio"]}, " +
                    //    $"NombreDominio {Configuration["LDAP:NombreDominio"]}, " +
                    //    $"UsuarioAdmin {Configuration["LDAP:UsuarioAdmin"]}, " +
                    //    $"ContrasenaAdmin {Configuration["LDAP:ContrasenaAdmin"]}, " +
                    //    $"AuthenticationTypes {Configuration["LDAP:AuthenticationTypes"]}");
                    // Creamos un objeto DirectoryEntry para conectarnos al directorio activo
                    //DirectoryEntry _DirectoryEntry = new DirectoryEntry("LDAP://"+ Configuration["LDAP:NombreDominio"], @"acs\" + userName, password);
                    DirectoryEntry _DirectoryEntry = new(Configuration["LDAP:DireccionDominio"], Configuration["LDAP:NombreDominio"] + @"\" +
                        Configuration["LDAP:UsuarioAdmin"], Configuration["LDAP:ContrasenaAdmin"], (AuthenticationTypes)int.Parse(Configuration["LDAP:AuthenticationTypes"]));
                    // Creamos un objeto DirectorySearcher para hacer una búsqueda en el directorio activo
                    DirectorySearcher _DirectorySearcher = new(_DirectoryEntry);
                    AgregarPropiedadesFiltro(_DirectorySearcher);
                    // Ponemos como filtro que busque el usuario actual               
                    _DirectorySearcher.Filter = "samAccountName=" + userName;
                    // Extraemos la primera coincidencia
                    SearchResult _SearchResult;
                    _SearchResult = _DirectorySearcher.FindOne();
                    if (_SearchResult != null)
                    {
                        // Obtenemos el objeto de ese usuario
                        DirectoryEntry Usuario = _SearchResult.GetDirectoryEntry();
                        // Obtenemos la lista de SID de los grupos a los que pertenece
                        //Usuario.RefreshCache(new string[] { "tokenGroups" });
                        if (_SearchResult.Path != null)
                        {
                            UsuarioLdap _UsuarioLdap = ObtenerModelo(_SearchResult);
                            return _UsuarioLdap;
                        }
                    }
                    return null;
                });
                return usuarioLdap;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        /// <summary>
        /// Agrega los objetos de directorio activo que debe traer
        /// </summary>
        /// <param name="_DirectorySearcher"></param>
        private static void AgregarPropiedadesFiltro(DirectorySearcher _DirectorySearcher)
        {
            _DirectorySearcher.PropertiesToLoad.Add("objectSid");
            _DirectorySearcher.PropertiesToLoad.Add("objectGUID");
            _DirectorySearcher.PropertiesToLoad.Add("objectCategory");
            _DirectorySearcher.PropertiesToLoad.Add("objectClass");
            _DirectorySearcher.PropertiesToLoad.Add("memberOf");
            _DirectorySearcher.PropertiesToLoad.Add("cn");
            _DirectorySearcher.PropertiesToLoad.Add("name");
            _DirectorySearcher.PropertiesToLoad.Add("sAMAccountName");
            _DirectorySearcher.PropertiesToLoad.Add("userPrincipalName");
            _DirectorySearcher.PropertiesToLoad.Add("distinguishedName");
            _DirectorySearcher.PropertiesToLoad.Add("displayName");
            _DirectorySearcher.PropertiesToLoad.Add("givenName");
            _DirectorySearcher.PropertiesToLoad.Add("sn");
            _DirectorySearcher.PropertiesToLoad.Add("description");
            _DirectorySearcher.PropertiesToLoad.Add("telephoneNumber");
            _DirectorySearcher.PropertiesToLoad.Add("mail");
            _DirectorySearcher.PropertiesToLoad.Add("streetAddress");
            _DirectorySearcher.PropertiesToLoad.Add("l");
            _DirectorySearcher.PropertiesToLoad.Add("postalCode");
            _DirectorySearcher.PropertiesToLoad.Add("st");
            _DirectorySearcher.PropertiesToLoad.Add("co");
            _DirectorySearcher.PropertiesToLoad.Add("c");
            _DirectorySearcher.PropertiesToLoad.Add("sAMAccountType");
        }
        /// <summary>
        /// Iguala los resultados obtenidos al modelo de datos
        /// </summary>
        /// <param name="_SearchResult"></param>
        /// <returns></returns>
        private static UsuarioLdap ObtenerModelo(SearchResult _SearchResult)
        {
            UsuarioLdap _UsuarioLdap = new()
            {
                ObjectSid = (_SearchResult.Properties["objectSid"].Count > 0) ? _SearchResult.Properties["objectSid"][0].ToString() : null,
                ObjectGuid = (_SearchResult.Properties["objectGUID"].Count > 0) ? _SearchResult.Properties["objectGUID"][0].ToString() : null,
                ObjectCategory = (_SearchResult.Properties["objectCategory"].Count > 0) ? _SearchResult.Properties["objectCategory"][0].ToString() : null,
                ObjectClass = (_SearchResult.Properties["objectClass"].Count > 0) ? _SearchResult.Properties["objectClass"][0].ToString() : null,
                IsDomainAdmin = (_SearchResult.Properties["memberOf"].Count > 0 && _SearchResult.Properties["memberOf"][0].ToString().Contains("CN=Domain Admins,")),
                MemberOf = (_SearchResult.Properties["memberOf"].Count > 0) ? _SearchResult.Properties["memberOf"] : null,
                CommonName = (_SearchResult.Properties["cn"].Count > 0) ? _SearchResult.Properties["cn"][0].ToString() : null,
                UserName = (_SearchResult.Properties["name"].Count > 0) ? _SearchResult.Properties["name"][0].ToString() : null,
                SamAccountName = (_SearchResult.Properties["sAMAccountName"].Count > 0) ? _SearchResult.Properties["sAMAccountName"][0].ToString() : null,
                UserPrincipalName = (_SearchResult.Properties["userPrincipalName"].Count > 0) ? _SearchResult.Properties["userPrincipalName"][0].ToString() : null,
                Name = (_SearchResult.Properties["name"].Count > 0) ? _SearchResult.Properties["name"][0].ToString() : null,
                DistinguishedName = (_SearchResult.Properties["distinguishedName"].Count > 0) ? _SearchResult.Properties["distinguishedName"][0].ToString() : null,
                DisplayName = (_SearchResult.Properties["displayName"].Count > 0) ? _SearchResult.Properties["displayName"][0].ToString() : null,
                FirstName = (_SearchResult.Properties["givenName"].Count > 0) ? _SearchResult.Properties["givenName"][0].ToString() : null,
                LastName = (_SearchResult.Properties["sn"].Count > 0) ? _SearchResult.Properties["sn"][0].ToString() : null,
                Description = (_SearchResult.Properties["description"].Count > 0) ? _SearchResult.Properties["description"][0].ToString() : null,
                Phone = (_SearchResult.Properties["telephoneNumber"].Count > 0) ? _SearchResult.Properties["telephoneNumber"][0].ToString() : null,
                EmailAddress = (_SearchResult.Properties["mail"].Count > 0) ? _SearchResult.Properties["mail"][0].ToString() : null,
                Address = new LdapAddress
                {
                    Street = (_SearchResult.Properties["streetAddress"].Count > 0) ? _SearchResult.Properties["streetAddress"][0].ToString() : null,
                    City = (_SearchResult.Properties["l"].Count > 0) ? _SearchResult.Properties["l"][0].ToString() : null,
                    PostalCode = (_SearchResult.Properties["postalCode"].Count > 0) ? _SearchResult.Properties["postalCode"][0].ToString() : null,
                    StateName = (_SearchResult.Properties["st"].Count > 0) ? _SearchResult.Properties["st"][0].ToString() : null,
                    CountryName = (_SearchResult.Properties["co"].Count > 0) ? _SearchResult.Properties["co"][0].ToString() : null,
                    CountryCode = (_SearchResult.Properties["c"].Count > 0) ? _SearchResult.Properties["c"][0].ToString() : null,
                },
                SamAccountType = (_SearchResult.Properties["sAMAccountType"].Count > 0) ? int.Parse(_SearchResult.Properties["sAMAccountType"][0].ToString()) : 0

            };
            return _UsuarioLdap;
        }
    }
}
