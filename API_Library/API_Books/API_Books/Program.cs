using API_Books;
using Microsoft.AspNetCore;

namespace API_Books
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            var webHost = WebHost.CreateDefaultBuilder(args)
             .UseConfiguration(config)
             .ConfigureKestrel(options => { options.AddServerHeader = false; })
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseStartup<Startup>();

            return webHost;
        }
    }
}