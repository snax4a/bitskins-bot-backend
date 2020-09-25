using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebApi.Helpers;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
                // var host = CreateHostBuilder(args).Build();
                // using (var scope = host.Services.CreateScope())
                // {
                //     scope.ServiceProvider.GetRequiredService<DataContext>().Database.Migrate();
                // }
                // host.Run();
            }
            catch (Exception ex)
            {
                Log.Information(ex, "App crashed with unhandled exception");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostingContext, loggerConfig) =>
                    loggerConfig.ReadFrom
                    .Configuration(hostingContext.Configuration)
                );
    }
}
