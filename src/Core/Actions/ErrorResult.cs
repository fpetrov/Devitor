using Devitor.Core.Actions.SharedResults;
using Devitor.Models.Enums;

namespace Devitor.Core.Actions
{
    public class ErrorResult : StatusCodeResult
    {
        public ErrorResult() : base(RPCResult.Error)
        {

        }
    }
}
