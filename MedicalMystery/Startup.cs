using BL.Interfaces;
using BL.Services;
using DAL.App.Database;
using DAL.App.Database.Repositories;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalMystery
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
            //services.AddScoped<IDataContext, MedicalMysteryDbContext>();

            services.AddDbContext<MedicalMysteryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, MedicalMysteryDbContext>();
            #region Repositories
            services.AddScoped<IDiseasesRepository, DiseasesRepository>();
            services.AddScoped<ISymptomsRepository, SymptomsRepository>();
            #endregion


            #region Services
            services.AddScoped<IDiseasesService, DiseasesService>();
            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<ISymptomsInDiseaseService, SymptomsInDiseaseService>();
            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
                    template: "{controller=Diagnosis}/{action=Database}/{id?}");
            });
        }
    }
}
