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
                    string[] line = System.IO.File.ReadAllLines(@"C:\Users\Judi\Documents\GitHub\WebApplication3\WebDesign\WebApplication3\bin\Debug\Config.txt");
                    webBuilder.UseUrls(line[1],line[2]);
                    //webBuilder.UseUrls("http://192.168.0.69:5000", "https://192.168.0.69:5001");
                });

    }
}
