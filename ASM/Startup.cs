using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Business;
using Infrastructure.Interface.Business;
using Infrastructure.Interface.DataAccess;
using Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Newtonsoft.Json.Serialization;

namespace ASM
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
            services.AddControllersWithViews();
            // stop default json response conversion to camelCase
            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            // Entity Context
            string connectionString = Configuration.GetConnectionString("AMSConnectionString");
            services.AddDbContext<ASMContext>(options => options.UseSqlServer(connectionString));

            // Services
            services.AddSingleton<IBlobContainerFactory, BlobContainerFactory>();
            services.AddSingleton<IImageClientFactory, ImageClientFactory>();

            // Business
            services.AddScoped<IAssetManager, AssetManager>();
            services.AddScoped<IBlobManager, BlobManager>();
            services.AddScoped<IAnalysisManager, AnalysisManager>();

            // Repository
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IBlobRepository, BlobRepository>();
            services.AddScoped<IAnalysisRepository, AnalysisRepository>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
