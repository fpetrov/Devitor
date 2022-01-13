using Devitor.Core.Actions.SharedResults;
using Devitor.Models.Enums;

namespace Devitor.Core.Actions
{
    public class OkResult : StatusCodeResult
    {
        public OkResult() : base(RPCResult.Success)
        {

        }
    }
}
