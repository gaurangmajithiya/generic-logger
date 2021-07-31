using Microsoft.Extensions.Hosting;
using Serilog;

namespace GenericLogger
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseGenericLog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog();
            return hostBuilder;
        }
    }
}
