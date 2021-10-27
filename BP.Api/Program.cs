using BP.Api.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api
{
    public class Program
    {
        private static string FileName ="Logs.txt";
        private static IConfiguration Configuration
        {
            get
            {
                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
                return new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false) // opsiyonel değil zorunlu olarak oku
                    .AddJsonFile($"appsettings.{env}.json", optional: true) // ilk buna bak eğer böyle bir dosya yoksa yukarıdakini oku
                    .AddEnvironmentVariables()
                    .Build();
              
            }
        }

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                ////.WriteTo.Debug()
                //.WriteTo.File(FileName)// filePath
                .CreateLogger();


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog() // serilogu kullan diyorum

            //.ConfigureLogging(config =>
            //{
            //    config.ClearProviders();
            //    config.SetMinimumLevel(LogLevel.Debug);
            //   // config.AddConsole();
            //   // config.AddDebug();
            //    config.AddProvider(new MyCustomLoggerFactory()); // kendi custom logger classımı kullanıyorum.
            //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(Configuration); // burda okutturuyorum
                    webBuilder.UseStartup<Startup>();
                });
    }
}
