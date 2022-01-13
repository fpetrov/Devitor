using Devitor.Core.Application.Abstractions;
using Devitor.Core.Native;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Devitor.Extensions
{
    public static class ApplicationExtensions
    {
        public static Platform GetPlatform(this IApplication application)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var isMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

            if (isWindows)
                return Platform.Windows;

            if (isMacOS)
                return Platform.MacOSX;

            return Platform.Linux;
        }
    }
}
