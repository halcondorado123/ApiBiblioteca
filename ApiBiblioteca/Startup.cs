using ApiBiblioteca.Models;
using ApiBiblioteca.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.OpenApi.Models;

namespace ApiBiblioteca
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Manejo de interfaces y repositorios
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton(SqlConnection);

            services.AddDbContext<LibrosDbContext>(options =>
                  options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddScoped<ILibrosRepository, LibrosRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RepositoryLibros", Version = "v1" });
            });
        }
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            // Configuracion basica de enrutamiento
            app.UseRouting();
            
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

        }
    }
}
