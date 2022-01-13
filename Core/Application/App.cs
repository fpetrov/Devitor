using System;
using Devitor.Core.Application.Abstractions;
using Devitor.Extensions;
using Microsoft.Extensions.Configuration;

namespace Devitor.Core.Application
{
    public static class App
    {
        public static IApplicationBuilder CreateDefaultBuilder(
            Action<IApplicationHostBuilder> builderDelegate)
        {
            var hostBuilder = new ApplicationHostBuilder();
            builderDelegate.Invoke(hostBuilder);

            var builder = new ApplicationBuilder(hostBuilder);

            // Register dependencies.
            builder
                .AddConfiguration()
                .AddHttpFactory();

            return builder;
        }
    }
}