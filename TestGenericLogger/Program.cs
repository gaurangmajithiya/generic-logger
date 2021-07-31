using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using GenericLogger;

namespace TestGenericLogger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GenericLogger.Logger.Configure();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseGenericLog();
    }
}
