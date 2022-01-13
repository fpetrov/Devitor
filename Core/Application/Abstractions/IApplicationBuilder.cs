using System;
using Devitor.Core.Application.Window;
using Microsoft.Extensions.DependencyInjection;

namespace Devitor.Core.Application.Abstractions
{
    public interface IApplicationBuilder
    {
        public IServiceProvider ServiceProvider { get; set; }
        public IApplicationBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);
        public IApplication Build();
    }
}