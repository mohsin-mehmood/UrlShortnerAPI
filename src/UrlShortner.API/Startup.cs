using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.WindowsAzure.Storage;
using SnowMaker;
using System;
using System.IO;
using System.Reflection;
using UrlShortner.API.Extensions.DependencyInjection;
using UrlShortner.Infrastructure.Data;

namespace UrlShortner.API
{
    public class Startup
    {
        const string APIName = "URL Shortening API";

        string UniqueKeysStorageContainerName;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            UniqueKeysStorageContainerName = configuration["AppSettings:SnowMakerConfiguration:ContainerName"];
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Enable header based versioning
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            })
            .AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1", new OpenApiInfo { Title = APIName, Version = "v1" });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            })
            .AddServicesRegistrations()
            .AddRepositoriesRegistrations()
            .AddProvidersRegistrations()
            // SQL Server configuration
            .AddDbContext<AppDBContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("UrlShortnerDB")))
            // IOptimisticDataStore for SnowMaker
            .AddSingleton<IOptimisticDataStore>(sp =>
            {
                var cloudStorageAccount = CloudStorageAccount.Parse(Configuration["AppSettings:SnowMakerConfiguration:StorageAccountConnection"]);

                // I know it is bad practice but as per my knowledge there is no support for async resolve option in Microsoft DI
                return BlobOptimisticDataStore.CreateAsync(cloudStorageAccount, UniqueKeysStorageContainerName).Result;
            })
            //SnowMaker UniqueIdGenerator
            .AddSingleton<IUniqueIdGenerator>(sp =>
            {
                var dataStore = sp.GetService<IOptimisticDataStore>();
                if (dataStore != null)
                {
                    return new UniqueIdGenerator(dataStore);
                }

                return null;
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseSwagger();

            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", APIName));


            app.UseHttpsRedirection();

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc();
        }
    }
}
