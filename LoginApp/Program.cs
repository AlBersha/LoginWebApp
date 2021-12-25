using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LoginApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            CreateHostBuilder(args).Run();
        }

        private static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options => options.ConfigureEndpoints())
                .Build();
            
            // WebHost.CreateDefaultBuilder(args)
            //     .UseStartup<Startup>().Build();
    }
}