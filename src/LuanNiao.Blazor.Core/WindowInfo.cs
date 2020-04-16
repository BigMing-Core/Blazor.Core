using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using LuanNiao.Blazor.Core.Common;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core
{
    public class WindowInfo
    {
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


        
        public async Task<WindowSize> GetWindowSize()
        {
            return await _jSRuntime.InvokeAsync<WindowSize>("LuanNiaoBlazor.GetWindowSize");
        }

    }
}
