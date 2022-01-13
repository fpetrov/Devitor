using Devitor.Core.Actions.SharedResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devitor.Core.Actions
{
    public class ErrorObjectResult : ObjectResult
    {
        public ErrorObjectResult(object value) : base(value)
        {

        }
    }
}
