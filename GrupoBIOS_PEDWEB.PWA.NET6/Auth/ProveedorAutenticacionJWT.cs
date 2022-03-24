using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

namespace GrupoBIOS_PEDWEB.PWA.Auth
{
    public class ProveedorAutenticacionJWT : AuthenticationStateProvider, IProveedorAutenticacionJWT
    {

        public static readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        public static readonly string TOKENKEY = "TOKENKEY";
        public static readonly string CompaniaId = "CompañiaId";
        public static readonly string TIPOINGRESOID = "TIPOINGRESOID";
        private readonly IConexionRest conexionRest;
        private readonly HttpClient httpClient;
        private readonly IJSRuntime js;
        private readonly ISettings settings;
        public ProveedorAutenticacionJWT(IJSRuntime js, HttpClient httpClient,
               IConexionRest conexionRest,
                                ISettings settings)
        {
            this.settings = settings;
            this.js = js;
            this.httpClient = httpClient;
            this.conexionRest = conexionRest;
        }

        private static AuthenticationState Anonimo =>
            new(new ClaimsPrincipal(new ClaimsIdentity()));

        public AuthenticationState ConstruirAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {

                var token = await js.GetFromLocalStorage(TOKENKEY);

                if (string.IsNullOrEmpty(token))
                {
                    return Anonimo;
                }

                var CompaniaIdString = await js.GetFromLocalStorage(CompaniaId);
                var tipoIngresoIdString = await js.GetFromLocalStorage(TIPOINGRESOID);
                var tiempoExpiracionString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);

                if (DateTime.TryParse(tiempoExpiracionString, out DateTime tiempoExpiracion))
                {
                    if (TokenExpirado(tiempoExpiracion))
                    {
                        await Limpiar();
                        return Anonimo;
                    }

                    if (DebeRenovarToken(tiempoExpiracion))
                    {
                        token = await RenovarToken(token, tipoIngresoIdString, CompaniaIdString);
                    }

                }
                return ConstruirAuthenticationState(token);
            }
            catch
            {
                return Anonimo;
            }
        }

        public async Task Login(UserToken userToken)
        {
            await js.SetInLocalStorage(TOKENKEY, userToken.Token);
            await js.SetInLocalStorage(EXPIRATIONTOKENKEY, userToken.Expiration.ToString());
            await js.SetInLocalStorage(TIPOINGRESOID, userToken.TipoIngresoId.ToString());
            await js.SetInLocalStorage("TIPOUSUARIOID", userToken.TipoUsuarioId.ToString());
            await js.SetInLocalStorage("SUCURSALCLIENTEHIJO", userToken.SucursalClienteHijo);
            await js.SetInLocalStorage("CENTROOPERATIVOID", userToken.CentroOperativoId);
            await js.SetInLocalStorage("NITFIJO", userToken.NitFijo.ToString());

            var authState = ConstruirAuthenticationState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Limpiar();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }


        public async Task ManejarRenovacionToken()
        {
            var CompaniaIdString = await js.GetFromLocalStorage(CompaniaId);
            var tipoIngresoIdString = await js.GetFromLocalStorage(TIPOINGRESOID);
            var tiempoExpiracionString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);

            if (DateTime.TryParse(tiempoExpiracionString, out DateTime tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Logout();
                }
                if (DebeRenovarToken(tiempoExpiracion))
                {
                    var token = await js.GetFromLocalStorage(TOKENKEY);
                    var nuevoToken = await RenovarToken(token, tipoIngresoIdString, CompaniaIdString);
                    var authState = ConstruirAuthenticationState(nuevoToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        private static bool DebeRenovarToken(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion.Subtract(DateTime.Now) < TimeSpan.FromDays(5);
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private static bool TokenExpirado(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion.Date < DateTime.Now.Date;
        }

        private async Task Limpiar()
        {
            await js.RemoveItem(TOKENKEY);
            await js.RemoveItem(EXPIRATIONTOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private async Task<string> RenovarToken(string token, string TIPOINGRESOID, string CompaniaIdString)
        {
            try
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var nuevoTokenResponse = await conexionRest.Get<UserToken>($"{ApiPEDWEBUrl}/Identity/RenovarToken/{TIPOINGRESOID}/{CompaniaIdString}");
                if (!nuevoTokenResponse.Error)
                {
                    if (nuevoTokenResponse.Response != null)
                    {
                        var nuevoToken = nuevoTokenResponse.Response;
                        await js.SetInLocalStorage(TOKENKEY, nuevoToken.Token);
                        await js.SetInLocalStorage(EXPIRATIONTOKENKEY, nuevoToken.Expiration.ToString());
                        return nuevoToken.Token;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return token;
        }
    }
}
