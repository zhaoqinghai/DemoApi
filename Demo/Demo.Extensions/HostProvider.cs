using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Demo.Extensions
{
    public static class HostProvider
    {
        public static IHost GetHost()
        {
            return GetHost(null, null);
        }

        public static IHost GetHost(string[] args)
        {
            return GetHost(args, null);
        }

        public static IHost GetHost(string[] args, Action<IServiceCollection> configureServices)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        if (configureServices != null)
                        {
                            configureServices(services);
                        }
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
}
