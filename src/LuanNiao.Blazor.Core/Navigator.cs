using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core
{
    public class Navigator
    {
        private readonly IJSRuntime _jSRuntime = null;

        public Navigator(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
        }


        public ValueTask Copy(string text)
        {
            return _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.Copy", text);
        }
    }
}
