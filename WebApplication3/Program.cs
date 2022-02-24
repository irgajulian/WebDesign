using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using BackgroundTasksSample.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication3
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    #region snippet1
                    services.AddHostedService<TimedHostedService>();
                    #endregion
                })
                .Build();

            await host.StartAsync();

            CreateHostBuilder(args).Build().Run();

            await host.WaitForShutdownAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseUrls("http://192.168.151.53:5000", "https://192.168.151.53:5001");
                    //webBuilder.UseUrls("http://192.168.0.69:5000", "https://192.168.0.69:5001");
                });

    }
}
