using Devitor.Content;
using Devitor.Core.Application.Abstractions;
using Devitor.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Devitor.Core.Application
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        private IApplication _application;
        public IServiceProvider ServiceProvider { get; set; }
        public IServiceCollection ApplicationServices { get; set; }

        public IApplicationHostBuilder HostBuilder { get; set; }
        private IStartupInitializer StartupInitializer { get; set; }
        public ApplicationBuilder(IApplicationHostBuilder hostBuilder)
        {
            HostBuilder = hostBuilder;
            
            StartupInitializer = new StartupInitializer(HostBuilder);
            ApplicationServices = new ServiceCollection();
        }

        public IApplicationBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
        {
            configureDelegate.Invoke(ApplicationServices);

            return this;
        }
        
        public IApplication Build()
        {
            ServiceProvider = StartupInitializer.Initialize(ApplicationServices);

            _application = new Devitor.Application
            {
                WindowOptions = HostBuilder.WindowOptions
            };

            _application
                .SetTitle(HostBuilder.WindowOptions.Title)
                .SetSize(HostBuilder.WindowOptions.Size.Width, HostBuilder.WindowOptions.Size.Height, ApplicationHint.None)
                .SetTransparency(HostBuilder.WindowOptions.Transparency)
                .Navigate(new UrlContent(HostBuilder.WindowOptions.StartLocation));

            return _application;
        }
    }
}