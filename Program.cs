using dotnet_pairing_exercise_base.service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dotnet_pairing_exercise_base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder().Build();
            var serviceScopes = host.Services.CreateScope();
            var provider = serviceScopes.ServiceProvider;
            var helloWorldService = provider.GetRequiredService<IProcessService>();
            
            helloWorldService.Run();
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                    services.AddTransient<IProcessService, ProcessService>());
        }
    }
}
