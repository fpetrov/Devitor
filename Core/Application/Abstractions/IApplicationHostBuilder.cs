using System;
using Devitor.Core.Application.Window;

namespace Devitor.Core.Application.Abstractions
{
    public interface IApplicationHostBuilder
    {
        public Type StartupClass { get; set; }

        public WindowOptions WindowOptions { get; set; }
        public IApplicationHostBuilder ConfigureWindow(Action<WindowOptions> options);
        public IApplicationHostBuilder UseStartup<TType>() where TType : IStartup;
    }

    public class ApplicationHostBuilder : IApplicationHostBuilder
    {
        public Type StartupClass { get; set; }
        
        public WindowOptions WindowOptions { get; set; }

        public ApplicationHostBuilder()
        {
            WindowOptions = new WindowOptions();
        }

        public IApplicationHostBuilder ConfigureWindow(Action<WindowOptions> window)
        {
            window.Invoke(WindowOptions);

            return this;
        }

        public IApplicationHostBuilder UseStartup<TType>()
            where TType : IStartup
        {
            StartupClass = typeof(TType);

            return this;
        }
    }
}