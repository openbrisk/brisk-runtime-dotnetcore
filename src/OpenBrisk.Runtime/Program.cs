namespace OpenBrisk.Runtime
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

	public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://+:8080")
                .Build();
        }
    }
}
