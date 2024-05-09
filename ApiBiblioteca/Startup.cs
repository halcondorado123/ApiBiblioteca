using ApiBiblioteca.Models;
using ApiBiblioteca.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Text;

namespace ApiBiblioteca
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        private readonly NLog.Logger _logger;


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = NLog.LogManager.GetCurrentClassLogger();
            _logger.Debug("NLog configurado correctamente.");

        }

        // Manejo de interfaces y repositorios
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                //var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
                _logger.Debug("Init main");

                services.AddDbContext<LibrosDbContext>(options =>
                      options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
                services.AddControllersWithViews();
                services.AddScoped<ILibrosRepository, LibrosRepository>();

                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog(); 
                });

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RepositoryLibros ", Version = "v1" });

                    // Configuración del token JWT en Swagger
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                        Name = "Authorization", 
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                    });
                });

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = _configuration["Jwt:Issuer"],
                         ValidAudience = _configuration["Jwt:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                     };
                 });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "El programa se ha detenido porque se genero una excepción en ConfigureServices de la clase Startup");
            }
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                app.UseCors(options =>
                {
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthentication();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "(controller=home)/{action=Index}/{id?}");
                });

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RepositoryLibros v1");
                });

                _logger.Info("Configuración aplicada correctamente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "El programa se ha detenido porque se genero una excepción en Configure de la clase Startup");
                throw;
            }
            finally 
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
