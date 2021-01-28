using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Backend.Models;
using System;


namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddCors( p => p.AddPolicy("CORS", builder => {
                builder.AllowAnyMethod().AllowAnyHeader().WithOrigins(new string[] {
                    "http://127.0.0.1:5500",
                    "https://dankmir.github.io"
                });
            }));

            services.AddDbContext<RestaurantContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("RestaurantCS"));
            });
        }

        private void CertificateAuthentificationDefaults(AuthenticationOptions obj)
        {
            throw new NotImplementedException();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection(); 
            
            app.UseCors("CORS");
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
