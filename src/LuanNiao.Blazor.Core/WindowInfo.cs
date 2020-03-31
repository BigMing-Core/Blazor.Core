using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using LuanNiao.Blazor.Core.Common;

namespace LuanNiao.Blazor.Core
{
    public class WindowInfo
    {
        private IJSRuntime _jSRuntime = null;
        private WindowSize _initialSize = null;
        public WindowSize InitialSize { get => _initialSize; }

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
        ~WindowInfo()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        public WindowInfo(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
        }

        public async void Init()
        {

            if (_initialSize == null)
            {
                _initialSize = await _jSRuntime.InvokeAsync<WindowSize>("WaveBlazor.GetWindowSize");
            }

        }
    }
}
