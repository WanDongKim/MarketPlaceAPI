using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MarketPlaceServiceAPI.Services;
using Swashbuckle.AspNetCore.Swagger;
using MarketPlaceServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceServiceAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<MarketPlaceServiceAPIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MarketPlaceServiceDB")));
            services.AddScoped<IProductsService, ProductsService>();
            services.AddMvc(options =>
            {
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
            }).AddXmlSerializerFormatters();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Info
                {
                    Title = "MarketPlace Service API",
                    Version = "v1",
                    Description = "MarketPlace service for products using Async "
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
                             options.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Async Services"));
        }
    }
}
