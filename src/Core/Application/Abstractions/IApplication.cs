using System;
using Devitor.Content;
using Devitor.Models.Enums;
using Devitor.Models.Network;

namespace Devitor.Core.Application.Abstractions
{
    public interface IApplication : IDisposable
    {
        public IApplication SetTitle(string title);
        public IApplication SetSize(int width, int height, ApplicationHint hint);
        public IApplication InitScript(string javascript);
        public IApplication DisableBorders();
        public IApplication Navigate(IWebviewContent webviewContent);
        public IApplication Bind(string name, RequestDelegate callback);
        public IApplication SetTransparency(byte alpha);

        public IApplication MoveOnDrag();
        public void Return(string id, RPCResult result, string body);
        public void Evaluate(string javaScript);
        public void Dispatch(Action dispatchFunction);
        public IApplication Run();
    }
}
