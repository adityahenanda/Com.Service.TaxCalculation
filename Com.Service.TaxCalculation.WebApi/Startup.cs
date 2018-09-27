using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
using Com.Service.TaxCalculation.Lib.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;


namespace Com.Service.TaxCalculation.WebApi
{
    public class Startup
    {
        /* Hard Code */
        private string[] EXPOSED_HEADERS = new string[] { "Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time" };
        private string POLICY = "Policy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Register

        private void RegisterFacades(IServiceCollection services)
        {
            services
                .AddTransient<IWeavingSalesContract, WeavingSalesContractFacade>()
        }


        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<IValidateService,ValidateService>();
        }



        #endregion Register
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];

            /* Register */
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));
            RegisterFacades(services);
            RegisterServices(services);
            services.AddAutoMapper();

            /* Versioning */
            services.AddApiVersioning(options => { options.DefaultApiVersion = new ApiVersion(1, 0); });

           
            /* CORS */
            services.AddCors(options => options.AddPolicy(POLICY, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders(EXPOSED_HEADERS);
            }));

            /* API */
            services
               .AddMvcCore()
               .AddAuthorization()
               .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
               .AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* Update Database */
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                DbContext context = serviceScope.ServiceProvider.GetService<DbContext>();
                context.Database.Migrate();
            }

            app.UseAuthentication();
            app.UseCors(POLICY);
            app.UseMvc();
        }
    }
}
