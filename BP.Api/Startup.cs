using BP.Api.BackgroundServices;
using BP.Api.Extensions;
using BP.Api.Models;
using BP.Api.Services;
using BP.Api.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api
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

            services.AddControllers();
            //services.AddControllers()
            //    .AddFluentValidation(i => i.RunDefaultMvcValidationAfterFluentValidationExecutes = false);



            services.AddHealthChecks();

            services.AddHostedService<DateTimeLogWriter>(); // loglama background service

            services.AddLogging(); // 
          
            services.ConfigureMapping(); // contact, contactDTO mapleme fonksiyonumuz

            services.AddScoped<IContactService, ContactService>();

            services.AddTransient<IValidator<ContactDTO>, ContactValidator>(); // validasyon işlemi


            services.AddHttpClient("garantiapi", config =>
             {
                 config.BaseAddress = new Uri("http://wwww.garanti.com");
                 config.DefaultRequestHeaders.Add("Authorization", "Bearer 12312341234");

             }); // test için bir apiyi çağrıyor istek atıyor yani

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BP.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BP.Api v1"));
            }

            app.UseCustomHealthCheck(); // healtcheck ekledik extensionsda ki classımızdan çağırdık

            app.UseResponseCaching(); // cacheleme için controllerda attribute ekliyoruz

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
