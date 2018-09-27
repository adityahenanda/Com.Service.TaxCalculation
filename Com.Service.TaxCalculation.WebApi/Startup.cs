using Microsoft.Extensions.Configuration;
using AutoMapper;
using Com.Service.TaxCalculation.Lib.Facade.Product;
using Com.Service.TaxCalculation.Lib.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Com.Service.TaxCalculation.WebApi
{
    public class Startup
    {
        private readonly string[] EXPOSED_HEADERS = new string[] { "Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time" };
        private readonly string POLICY = "Policy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void RegisterEndpoint()
        {
        }

        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<IValidateService, ValidateService>();
        }

        private void RegisterFacades(IServiceCollection services)
        {
            services
                .AddTransient<InterfaceProductFacade, ProductFacade>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];

            #region Register
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString, c => c.CommandTimeout(60)));

            RegisterServices(services);

            RegisterFacades(services);

            services.AddAutoMapper();
            #endregion

            #region CORS
            services.AddCors(options => options.AddPolicy(POLICY, builder =>
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
                var context = serviceScope.ServiceProvider.GetService<DbContext>();
                context.Database.Migrate();
            }

            app.UseCors(POLICY);
            app.UseMvc();
        }
    }
}
