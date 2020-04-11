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
    public sealed class WindowEventHub : IDisposable
    {
      
        private bool _inited = false;
        private IJSRuntime _jSRuntime = null;


        #region Disposable pattern
        private bool _disposed = false;
        private void Dispose(bool flage)
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            if (flage)
            {
                _jSRuntime = null;
            }


        }
        ~WindowEventHub()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        public WindowEventHub(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
            Init(); 
        }


        private void Init()
        {
            if (!_inited)
            {
                _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.WindowReSize", DotNetObjectReference.Create(this)); 
            }
            _inited = true;
        }

        [JSInvokable]
        public void Resize(WindowSize windowSize)
        {
            Resized?.Invoke(windowSize);
        }




        public event Action<WindowSize> Resized;


    }

}
