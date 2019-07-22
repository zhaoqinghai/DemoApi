using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using DemoApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoApi
{
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
                })
                .UseServiceProviderFactory<IServiceContainer>(new AspectServiceProviderFactory());
    }

    public class AspectServiceProviderFactory : IServiceProviderFactory<IServiceContainer>
    {
        public IServiceContainer CreateBuilder(IServiceCollection services)
        {
            services.AddControllers();
            var container = services.ToServiceContainer();
            container.AddType<IBirdService, Duck>();
            return container;
        }

        public IServiceProvider CreateServiceProvider(IServiceContainer containerBuilder)
        {
            var provider = containerBuilder.Build();
            return provider;
        }
    }
}
