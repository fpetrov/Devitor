using Devitor.Core.Actions;
using Devitor.Core.Application;
using Devitor.Core.Application.Abstractions;

namespace Devitor.Core
{
    public abstract class ControllerBase
    {
        public IApplication Application { get; set; }

        protected ControllerBase(IApplication application)
        {
            Application = application;
        }

        public virtual OkResult Ok()
        {
            return new OkResult();
        }

        public virtual OkObjectResult Ok(object value)
        {
            return new OkObjectResult(value);
        }

        public virtual ErrorResult Error()
        {
            return new ErrorResult();
        }

        public virtual ErrorObjectResult Error(object value)
        {
            return new ErrorObjectResult(value);
        }

        public virtual RedirectResult Redirect(string address)
        {
            return new RedirectResult(address);
        }
    }
}
