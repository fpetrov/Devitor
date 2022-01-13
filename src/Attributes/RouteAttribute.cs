using System;
using System.Collections.Generic;
using System.Text;

namespace Devitor.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RouteAttribute : Attribute
    {
        public string Template { get; }

        public RouteAttribute(string template)
        {
            if (string.IsNullOrWhiteSpace(template))
                throw new ArgumentNullException(nameof(template), "Controller template cannot be null, empty, or all-whitespace.");

            Template = template;
        }
    }
}
