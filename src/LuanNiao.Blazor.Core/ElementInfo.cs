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

        #region Mouse Events

        public async void BindClickEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool isStopPropagation = false) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("click", elementID, callBackMethodName, instance, isPreventDefault, isStopPropagation);
        }
         
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "dispose this when the instance disposing..")]
        private async Task BindEvent<T>(string htmlEventName, string elementID, string callBackMethodName, T instance, bool isPreventDefault, bool isStopPropagation) where T : LNBCBase
        {
            var jsInstance = DotNetObjectReference.Create(instance);

            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.BindElementEvent",
                htmlEventName,
                 elementID,
                 callBackMethodName,
                 jsInstance,
                 isPreventDefault,
                 isStopPropagation);
            instance.Disposing += () =>
            {
                jsInstance.Dispose();
            };

        }




    }
}
