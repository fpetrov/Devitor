using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devitor.Core.Actions.Abstractions
{
    public interface IActionResult
    {
        Task ExecuteResultAsync();
    }
}
