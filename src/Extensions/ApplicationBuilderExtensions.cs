using Devitor.Core;
using Devitor.Core.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO;

namespace Devitor.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddConfiguration(
            this IApplicationBuilder builder,
            string configurationPath = "appsettings.json")
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configurationPath)
                .Build();

            builder
                .ConfigureServices(services =>
                    services.AddSingleton(
                        typeof(IConfiguration),
                        configuration));

            var bu = builder.ConfigureServices(services => {
                var f = services.Count;

                //return services;
                });

            return builder;
        }

        public static IApplicationBuilder AddHttpFactory(this IApplicationBuilder builder)
        {
            builder
                .ConfigureServices(services => 
                    services.AddHttpClient());

            return builder;
        }
    }
}
