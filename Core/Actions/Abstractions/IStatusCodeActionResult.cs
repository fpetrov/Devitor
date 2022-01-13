using Devitor.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devitor.Core.Actions.Abstractions
{
    public interface IStatusCodeActionResult
    {
        RPCResult StatusCode { get; }
    }
}
