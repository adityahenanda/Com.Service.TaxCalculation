using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Com.Service.TaxCalculation.Lib;
using Com.Service.TaxCalculation.Lib.Facade.Product;
using Com.Service.TaxCalculation.Lib.Utilities;
using Com.Service.TaxCalculation.WebApi.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Com.Service.TaxCalculation.WebApi
{
    public class Startup
    {
        private readonly string[] EXPOSED_HEADERS = new string[] { "Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time" };
        private readonly string PRODUCTION_POLICY = "ProductionPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<IValidateService, ValidateService>();
        }

        private void RegisterFacades(IServiceCollection services)
        {
            services
                .AddTransient<IProductFacade, ProductFacade>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString(Constant.DEFAULT_CONNECTION) ?? Configuration[Constant.DEFAULT_CONNECTION];

            #region Register
            services.AddDbContext<Lib.DbContext>(options => options.UseSqlServer(connectionString, c => c.CommandTimeout(60)));

            RegisterServices(services);

            RegisterFacades(services);

            services.AddAutoMapper();
            #endregion

            #region CORS
            services.AddCors(options => options.AddPolicy(PRODUCTION_POLICY, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders(EXPOSED_HEADERS);
            }));
            #endregion

            #region API
            services
                .AddApiVersioning(options => options.DefaultApiVersion = new ApiVersion(1, 0))
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Lib.DbContext>();
                context.Database.Migrate();
            }
            app.UseCors(PRODUCTION_POLICY);
            app.UseMvc();
        }
    }
}
