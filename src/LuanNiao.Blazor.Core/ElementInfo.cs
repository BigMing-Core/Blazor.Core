  using LuanNiao.Blazor.Core.Common;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core
{
    public class ElementInfo
    {
        private readonly IJSRuntime _jSRuntime = null;

        public ElementInfo(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
        }

        public async Task<ElementRects> GetElementRectsByID(string id)
        {
            return await _jSRuntime.InvokeAsync<ElementRects>("LuanNiaoBlazor.ElementOp.GetElementClientRects", id);
        }

        public async Task<string> GetElementValue(string id)
        {
            return await _jSRuntime.InvokeAsync<string>("LuanNiaoBlazor.ElementOp.GetElementValue", id);
        }
        public async void SetElementValue(string id, string value)
        {
            await _jSRuntime.InvokeAsync<string>("LuanNiaoBlazor.ElementOp.SetElementValue", id, value);
        }

        public async Task<string> GetElementInnerText(string id)
        {
            return await _jSRuntime.InvokeAsync<string>("LuanNiaoBlazor.ElementOp.GetElementInnerText", id);
        }

        public async Task<ElementScrollInfo> GetElementScrollInfo(string id)
        {
            return await _jSRuntime.InvokeAsync<ElementScrollInfo>("LuanNiaoBlazor.ElementOp.GetElementScrollInfo", id);
        }

        public async Task ScrollTo(string id, float x, float y)
        {
            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.ElementOp.ScrollTo", id, x, y);
        }

       




    }
}
