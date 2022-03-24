using GrupoBIOS_PEDWEB.PWA.NET6;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GrupoBIOS_PEDWEB.PWA.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress), Timeout = TimeSpan.FromSeconds(15) });

ConfigureServices(builder.Services);

await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services)
{
    services.AddOptions();
    services.AddMatBlazor();
    services.AddAuthorizationCore();

    services.AddSingleton<IConexionRest, ConexionREST>();
    services.AddSingleton<IMostrarMensajes, MostrarMensajes>();
    services.AddSingleton<ISettings, Settings>();
    services.AddSingleton<RefrescarAppState>();

    services.AddScoped<ProveedorAutenticacionJWT>();
    services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>(
        provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());
    services.AddScoped<IProveedorAutenticacionJWT, ProveedorAutenticacionJWT>(
        provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());
    ConfigureViewModel(services);
    ConfigureModels(services);
}

void ConfigureViewModel(IServiceCollection services)
{
    var assembly = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a
        .FullName.StartsWith("GrupoBIOS_PEDWEB.PWA"))
        .First();
    var classes = assembly.ExportedTypes.Where(a => a
         .FullName.Contains("_ViewModel"));
    foreach (Type t in classes)
    {
        foreach (Type i in t.GetInterfaces())
        {
            services.AddTransient(i, t);
        }
    }
}
void ConfigureModels(IServiceCollection services)
{
    var assembly = AppDomain.CurrentDomain.GetAssemblies()
    .Where(a => a
    .FullName.StartsWith("GrupoBIOS_PEDWEB.PWA"))
    .First();
    var classes = assembly.ExportedTypes.Where(a => a
         .FullName.Contains("_Model"));
    foreach (Type t in classes)
    {
        foreach (Type i in t.GetInterfaces())
        {
            services.AddTransient(i, t);
        }
    }

}