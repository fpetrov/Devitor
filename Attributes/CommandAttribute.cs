using System;

namespace Devitor.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequestAttribute : Attribute
    {
        public string Name { get; }

        public RequestAttribute()
        {
            Name = null;
        }

        public RequestAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Request name cannot be null, empty, or all-whitespace.");

            Name = name;
        }
    }
}
