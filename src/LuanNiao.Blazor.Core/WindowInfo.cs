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
        private readonly IJSRuntime _jSRuntime = null;
 
        public WindowInfo(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
        }


        
        public async Task<WindowSize> GetWindowSize()
        {
            return await _jSRuntime.InvokeAsync<WindowSize>("LuanNiaoBlazor.WindowOP.GetWindowSize");
        }

        public async Task<WindowScrollInfo> GetWindowScrollInfo()
        {
            return await _jSRuntime.InvokeAsync<WindowScrollInfo>("LuanNiaoBlazor.WindowOP.GetScrollInfo");
        }

    }
}
