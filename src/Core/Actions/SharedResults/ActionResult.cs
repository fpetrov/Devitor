using Devitor.Core.Actions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devitor.Core.Actions.SharedResults
{
    public abstract class ActionResult : IActionResult
    {
        public virtual void ExecuteResult()
        {

        }

        public virtual Task ExecuteResultAsync() => default;
    }
}
