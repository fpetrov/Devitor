using System;
using Devitor.Content;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Devitor.Models.Enums;
using Devitor.Core.Application.Abstractions;
using Devitor.Core.Application.Window;
using Devitor.Models.Network;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Devitor.Models.Enums.Native.Windows;
using Devitor.Core.Native.Abstractions;
using Devitor.Extensions;
using Devitor.Core.Native;

namespace Devitor
{
    /// <summary>
    /// A cross-platform application.
    /// </summary>
    public class Application : IApplication
    {
        // Webview has own handle of the window.
        private readonly IntPtr _nativeWebview;

        // And our app has own.
        private readonly IntPtr _windowHandle;

        // Used to handle OS's specific functions.
        private readonly INativeManager _nativeManager;

        public WindowOptions WindowOptions { get; set; }

        private bool _disposed;
        private bool? _loopbackEnabled;

        private readonly List<CallBackFunction> _callbacks = new List<CallBackFunction>();
        private readonly List<DispatchFunction> _dispatchFunctions = new List<DispatchFunction>();

        /// <summary>
        /// Creates a new application.
        /// </summary>
        /// <param name="debug">
        /// Set to true, to activate a debug view, 
        /// if the current webview implementation supports it.
        /// </param>
        /// <param name="interceptExternalLinks">
        /// Set to true, top open external links in system browser.
        /// </param>
        public Application(bool debug = false)
        {
            _nativeWebview = Bindings.webview_create(debug ? 1 : 0, IntPtr.Zero);

            // We need to search the real handle of the application.
            var process = Process.GetProcessesByName(Assembly
                .GetEntryAssembly()
                .GetName()
                .Name)[0];

            _windowHandle = process.MainWindowHandle;

            _nativeManager = GetNativeManager(this.GetPlatform());
        }

        /// <summary>
        /// Set the title of the application window.
        /// </summary>
        /// <param name="title">The new title.</param>
        /// <returns>The webview object.</returns>
        public IApplication SetTitle(string title)
        {
            Bindings.webview_set_title(_nativeWebview, title);

            return this;
        }

        /// <summary>
        /// Set the size information of the webview application window.
        /// </summary>
        /// <param name="width">The width of the webview application window.</param>
        /// <param name="height">The height of the webview application window.</param>
        /// <param name="hint">The type of the size information.</param>
        /// <returns>The webview object for a fluent api.</returns>
        public IApplication SetSize(int width, int height, ApplicationHint hint)
        {
            Bindings.webview_set_size(_nativeWebview, width, height, hint);

            return this;
        }

        /// <summary>
        /// Injects JavaScript code at the initialization of the new page. Every time
        /// the webview will open a new page. This initialization code will be
        /// executed. It is guaranteed that code is executed before window.onload.
        /// </summary>
        /// <remarks>
        /// Execute this method before <see cref="Navigate(IWebviewContent)"/>
        /// </remarks>
        /// <param name="javascript">The javascript code to execute.</param>
        /// <returns>The webview object for a fluent api.</returns>
        public IApplication InitScript(string javascript)
        {
            Bindings.webview_init(_nativeWebview, javascript);

            return this;
        }

        /// <summary>
        /// Navigates webview to the given content.
        /// </summary>
        /// <param name="webviewContent">The content to navigate to.</param>
        /// <remarks>Content can be a UrlContent, HtmlContent or WebhostContent</remarks>
        /// <returns>The webview object for a fluent api.</returns>
        public IApplication Navigate(IWebviewContent webviewContent)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows && _loopbackEnabled == null) 
                _loopbackEnabled = CheckLoopbackException();

            if (isWindows && !_loopbackEnabled.Value)
            {
                Bindings.webview_navigate(_nativeWebview, webviewContent.ToWebviewUrl());
            }
            else
            {
                Bindings.webview_navigate(_nativeWebview, webviewContent.ToWebviewUrl());
            }

            return this;
        }

        /// <summary>
        /// Refreshes the opened page.
        /// </summary>
        /// <returns></returns>
        public IApplication DisableBorders()
        {
            var style = Bindings.GetWindowLongPtr(_windowHandle, WindowLongFlags.Style);
            var extendedStyle = Bindings.GetWindowLongPtr(_windowHandle, WindowLongFlags.ExtendedStyle);

            var styleNewWindowStandard =
                    style
                    & ~(
                        WindowStylesFlags.Caption // composite of Border and DialogFrame
                                                 //   | WindowStylesFlags.Border
                                                 //   | WindowStylesFlags.DialogFrame                  
                        | WindowStylesFlags.ThickFrame
                        | WindowStylesFlags.SystemMenu
                        | WindowStylesFlags.MaximizeBox // same as TabStop
                        | WindowStylesFlags.MinimizeBox // same as Group
                    );

            var styleNewWindowExtended =
                extendedStyle
                & ~(
                    WindowStylesFlags.ExtendedDlgModalFrame
                    | WindowStylesFlags.ExtendedComposited
                    | WindowStylesFlags.ExtendedWindowEdge
                    | WindowStylesFlags.ExtendedClientEdge
                    | WindowStylesFlags.ExtendedLayered
                    | WindowStylesFlags.ExtendedStaticEdge
                    | WindowStylesFlags.ExtendedToolWindow
                    | WindowStylesFlags.ExtendedAppWindow
                );

            Bindings.SetWindowLongPtr(_windowHandle, WindowLongFlags.Style, styleNewWindowStandard);

            Bindings.SetWindowLongPtr(_windowHandle, WindowLongFlags.ExtendedStyle, styleNewWindowExtended);

            Bindings.SetWindowPos(_windowHandle, 200, 0, 800, 0, 0, 
                (int)(WindowPositionFlags.FrameChanged |
                WindowPositionFlags.NoMove |
                WindowPositionFlags.NoSize |
                WindowPositionFlags.NoZOrder |
                WindowPositionFlags.NoOwnerZOrder));

            Console.WriteLine("ggdfgf");

            return this;
        }

        /// <summary>
        /// Binds a callback so that it will appear under the given name as a global JavaScript function. 
        /// </summary>
        /// <param name="name">Global name of the javascript function.</param>
        /// <param name="callback">Callback with two parameters. id -> The id of the call, req -> The parameters of the call as json</param>
        /// <returns>The webview object for a fluent api.</returns>
        public IApplication Bind(string name, RequestDelegate callback)
        {
            var callbackInstance = new CallBackFunction((id, body, _) => callback(id, body));

            _callbacks.Add(callbackInstance);

            Bindings.webview_bind(_nativeWebview, name, callbackInstance, IntPtr.Zero);

            return this;
        }

        /// <summary>
        /// Runs the main loop of the webview. Should be used as the last statement.
        /// </summary>
        /// <returns>The webview object.</returns>
        public IApplication Run()
        {
            Bindings.webview_run(_nativeWebview);

            return this;
        }

        /// <summary>
        /// Allows to return a value to the caller of a bound callback <see cref="Bind(string, Action{string, string})"/>.
        /// </summary>
        /// <param name="id">The id of the call.</param>
        /// <param name="result">The result of the call.</param>
        /// <param name="body">The result data as json.</param>
        public void Return(string id, RPCResult result, string body)
        {
            Bindings.webview_return(_nativeWebview, id, result, body);
        }

        /// <summary>
        /// Evaluates arbitrary JavaScript code. Evaluation happens asynchronously, also
        /// the result of the expression is ignored. Use bindings if you want to
        /// receive notifications about the results of the evaluation.
        /// </summary>
        /// <param name="javascript">The javascript to execute.</param>
        public void Evaluate(string javascript)
        {
            Bindings.webview_eval(_nativeWebview, javascript);
        }

        public IApplication SetTransparency(byte alpha)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return this;

            Bindings.SetWindowLongPtr(_windowHandle, WindowLongFlags.ExtendedStyle, Bindings.GetWindowLongPtr(_windowHandle, WindowLongFlags.ExtendedStyle) | WindowStylesFlags.ExtendedLayered);
            Bindings.SetLayeredWindowAttributes(_windowHandle, 0, alpha, 0x00000002);

            return this;
        }

        public IApplication MoveOnDrag()
        {
            Bindings.ReleaseCapture();
            Bindings.SendMessage(_windowHandle, 0xA1, 0x2, 0);

            return this;
        }

        /// <summary>
        /// Posts a function to be executed on the main thread of the webview.
        /// </summary>
        /// <param name="dispatchFunction">The function to call on the main thread</param>
        public void Dispatch(Action dispatchFunction)
        {
            void DispatchFuncInstance(IntPtr _, IntPtr __)
            {
                lock (_dispatchFunctions)
                {
                    _dispatchFunctions.Remove(DispatchFuncInstance);
                }

                dispatchFunction();
            }

            lock (_dispatchFunctions)
            {
                _dispatchFunctions.Add(DispatchFuncInstance);
            }

            Bindings.webview_dispatch(_nativeWebview, DispatchFuncInstance, IntPtr.Zero);
        }

        /// <summary>
        /// Disposes the current webview.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            
            Bindings.webview_terminate(_nativeWebview);
            Bindings.webview_destroy(_nativeWebview);
            _callbacks.Clear();

            lock (_dispatchFunctions)
            {
                _dispatchFunctions.Clear();
            }

            _disposed = true;
        }

        private bool? CheckLoopbackException()
        {
            if(Environment.OSVersion.Version.Major < 6 ||
               (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 2))
                return true;

            var loopBack = new Loopback();

            return loopBack.IsWebViewLoopbackEnabled();
        }

        private INativeManager GetNativeManager(Platform platform) =>
            platform switch
            {
                Platform.Windows => new WindowsNativeManager(),
                _ => new WindowsNativeManager()
            };

        ~Application() => Dispose(false);
    }
}
