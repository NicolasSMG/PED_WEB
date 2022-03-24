using AccesoBd;
using GrupoBIOS_PEDWEB.BM.Identity;
using GrupoBIOS_PEDWEB.BM.Identity.Interfaces;
using GrupoBIOS_PEDWEB.DM;
using GrupoBIOS_PEDWEB.DM.DataBase;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DM.Interfaces;
using GrupoBIOS_PEDWEB.Soportes;
using GrupoBIOS_PEDWEB.Soportes.ActiveDirectory;
using GrupoBIOS_PEDWEB.Soportes.Correos.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text;

namespace GrupoBIOS_PEDWEB.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["jwt:key"])),
                };
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                var groupName = "v1";

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"PEDWEB {groupName}",
                    Version = groupName,
                    Description = "PEDWEB API",
                    Contact = new OpenApiContact
                    {
                        Name = "Grupo BIOS",
                        Email = string.Empty,
                        Url = new Uri("https://grupobios.co/"),
                    }
                });
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securityScheme, Array.Empty<string>()
                    }
                });
            });


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddTransient<IConexionBD, ConexionInsightDB>();
            services.AddTransient<IDMConexionRestSiesa, DMConexionRestSiesa>();
            services.AddTransient<IDMDTOs, DMDTOs>();
            services.AddTransient<IBMIdentity, BMIdentity>();
            services.AddTransient<IActiveDirectory, ActiveDirectory>();
            services.AddTransient<IGestorCorreo, GestorCorreo>();
            services.AddTransient<GeneradorPassword>();
            ConfigureBMs(services);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API PEDWEB v1"));
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureBMs(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a
            .FullName.StartsWith("GrupoBIOS_PEDWEB.BM"))
            .First();
            var classes = assembly.ExportedTypes.Where(a => a
             .Name.StartsWith("BM"));
            foreach (Type t in classes)
            {
                foreach (Type i in t.GetInterfaces())
                {
                    services.AddTransient(i, t);
                }
            }

        }
    }
}
