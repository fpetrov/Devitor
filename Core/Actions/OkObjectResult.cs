using Devitor.Core.Actions.SharedResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devitor.Core.Actions
{
    public class OkObjectResult : ObjectResult
    {
        public OkObjectResult(object value) : base(value)
        {

        }
    }
}
