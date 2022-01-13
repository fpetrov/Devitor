using Devitor.Core.Actions.Abstractions;
using Devitor.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devitor.Core.Actions.SharedResults
{
    public class StatusCodeResult : ActionResult, IStatusCodeActionResult, IActionResult
    {
        public RPCResult StatusCode { get; }

        public StatusCodeResult(RPCResult rpcResult)
        {
            StatusCode = rpcResult;
        }

        public override void ExecuteResult()
        {
            throw new NotImplementedException();
        }
    }
}
