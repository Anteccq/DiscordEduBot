using System;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiscordEduBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.Configure<Config>(hostContext.Configuration);
            }).RunConsoleAppFrameworkAsync<DiscordEdu>(args);
        }
    }
}
