using Microsoft.Extensions.DependencyInjection;
using System;

namespace Devitor.Core.Application.Abstractions
{
    public interface IStartupInitializer
    {
        public IServiceProvider Initialize(IServiceCollection serviceCollection);
    }
}
