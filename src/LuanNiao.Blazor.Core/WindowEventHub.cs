using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading;
using LuanNiao.Blazor.Core.Common;

namespace LuanNiao.Blazor.Core
{
    public sealed class WindowEventHub
    {
        private readonly IJSRuntime _jSRuntime = null;


        public WindowEventHub(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
            _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.WindowReSize", DotNetObjectReference.Create(this));
            _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.WindowScroll", DotNetObjectReference.Create(this));
        }


        [JSInvokable]
        public void Scroll(WindowScrollEvent windowScrollEvent)
        {
            Scrolled?.Invoke(windowScrollEvent);
        }

        [JSInvokable]
        public void Resize(WindowSize windowSize)
        {
            Resized?.Invoke(windowSize);
        }

        public event Action<WindowSize> Resized;
        public event Action<WindowScrollEvent> Scrolled;
    }

}
