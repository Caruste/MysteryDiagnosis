using BL.Interfaces;
using BL.Services;
using DAL.App.Database;
using DAL.App.EF.Repositories;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace MedicalMystery
{
    /// <summary>
    /// Class which assigns options
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Here we assign the configuration
        /// </summary>
        /// <param name="configuration">Configuration which is used for the webpage</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration of the application
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IDataContext, MedicalMysteryDbContext>();

            services.AddDbContext<MedicalMysteryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, MedicalMysteryDbContext>();


            #region Repositories
            services.AddScoped<IDiseasesRepository, DiseasesRepository>();
            services.AddScoped<ISymptomsRepository, SymptomsRepository>();
            services.AddScoped<ISymptomsInDiseaseRepository, SymptomsInDiseaseRepository>();
            #endregion


            #region Services
            services.AddScoped<IDiseasesService, DiseasesService>();
            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<ISymptomsInDiseaseService, SymptomsInDiseaseService>();
            #endregion

            services.AddMvc();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });


                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "MedicalMystery.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Diagnosis}/{action=Statistics}/{id?}");
            });
        }
    }
}
