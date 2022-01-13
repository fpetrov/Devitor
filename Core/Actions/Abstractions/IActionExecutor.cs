using Devitor.Core.Application;
using System;
using System.Collections.Generic;
using System.Text;
using Devitor.Core.Application.Abstractions;

namespace Devitor.Core.Actions.Abstractions
{
    public interface IActionExecutor
    {
        public IApplication Application { get; set; }
    }
}
