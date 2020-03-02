using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHost(args).Run();
            //ExtensionsCreateHost(args).Run();
        }

        public static IHost ExtensionsCreateHost(string[] args) => HostProvider.GetHost(args, services => {
            services.AddControllers();
            var a = services.Where(item => item.ServiceType.ToString().Contains("ControllerActionEndpointDataSource")).FirstOrDefault();
            var b = services.BuildServiceProvider().GetServices(a.ServiceType);
        });

        public static IHost CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddControllers();
                        var a = services.Where(item => item.ServiceType.ToString().Contains("ControllerActionEndpointDataSource")).FirstOrDefault();
                        var b = services.BuildServiceProvider().GetServices(a.ServiceType);
                    });

                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                })
                .Build();
    }
}
