using Microsoft.AspNetCore.Hosting;
using MySqlConnector;
using RetailApplication.Api;
using RetailApplication.Core.IDBHelpers;
using RetailApplication.Infrastructure.DBHelpers;
using System.Configuration;
using System.Reflection;
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}