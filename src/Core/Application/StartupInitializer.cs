using Devitor.Core.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Devitor.Core.Application
{
    public class StartupInitializer : IStartupInitializer
    {
        private readonly IApplicationHostBuilder _hostBuilder;

        public StartupInitializer(IApplicationHostBuilder hostBuilder)
        {
            _hostBuilder = hostBuilder;
        }

        public IServiceProvider Initialize(IServiceCollection serviceCollection)
        {
            var startup = _hostBuilder.StartupClass;

            serviceCollection.AddSingleton(typeof(IStartup), startup);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Initialize the Startup service.
            serviceProvider.GetService<IStartup>();

            return serviceProvider;
        }
    }
}
