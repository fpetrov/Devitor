using Devitor.Core.Actions.Abstractions;
using Devitor.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devitor.Core.Actions.SharedResults
{
    public class ObjectResult : ActionResult, IStatusCodeActionResult, IActionResult
    {
        public RPCResult StatusCode { get; }
        public object Value { get; set; }

        public ObjectResult(object value)
        {
            Value = value;
        }

        public override Task ExecuteResultAsync()
        {
            return base.ExecuteResultAsync();
        }
    }
}
