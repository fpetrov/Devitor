using Devitor.Content;
using Devitor.Core.Actions.Abstractions;
using Devitor.Core.Actions.SharedResults;
using Devitor.Core.Application;
using Devitor.Core.Application.Abstractions;

namespace Devitor.Core.Actions
{
    public class RedirectResult : ObjectResult, IActionExecutor
    {
        public IApplication Application { get; set; }

        public string Address { get; set; }

        public RedirectResult(object value) : base(value)
        {
            Address = value.ToString();
        }

        public override void ExecuteResult()
        {
            Application.Navigate(new UrlContent(Address));
        }
    }
}
