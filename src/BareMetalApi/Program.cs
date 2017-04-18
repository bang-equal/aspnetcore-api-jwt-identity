using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace BareMetalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var url = $"http://bangequal-server:{Environment.GetEnvironmentVariable("PORT")}/";
            
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                //.UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();

            host.Run();
        }
    }
}
