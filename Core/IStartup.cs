using System;
using Devitor.Core.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Devitor.Core
{
    public interface IStartup
    {
        /// <summary>
        /// Register services into the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        public void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">An <see cref="IApplicationBuilder"/> for the app to configure.</param>
        public void Configure(IApplicationBuilder app);
    }
}