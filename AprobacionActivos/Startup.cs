using AprobacionActivos.Infraestructure;
using AprobacionActivos.Interfaces;
using AprobacionActivos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos
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
            #region Database
            string DevTooljetStringConnector = Configuration.GetConnectionString("Tooljet");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseOracle(DevTooljetStringConnector, opt => opt.UseOracleSQLCompatibility("11")).EnableSensitiveDataLogging());
            #endregion

            #region Services
            services.AddScoped<SolicitudInterface, SolicitudService>();
            services.AddScoped<AprobacionInterface, AprobacionService>();
            services.AddScoped<ActivoInterface, ActivoService>();
            #endregion

            #region Mapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AprobacionActivos", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AprobacionActivos v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
