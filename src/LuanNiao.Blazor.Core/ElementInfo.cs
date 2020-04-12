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
        ~ElementInfo()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion



        public ElementInfo(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
        }

        public async Task<ElementRects> GetElementRectsByID(string id)
        {
            return await _jSRuntime.InvokeAsync<ElementRects>("LuanNiaoBlazor.GetElementClientRects", id);
        }

        public async void BindClickEvent<T>(string elementID, string callBackMethodName, T instance) where T : class
        {
            await BindEvent("click", elementID, callBackMethodName, instance);
        }

        public async void BindMouseOverEvent<T>(string elementID, string callBackMethodName, T instance) where T : class
        {
            await BindEvent("mouseover", elementID, callBackMethodName, instance);
        }

        public async void BindMouseEnterEvent<T>(string elementID, string callBackMethodName, T instance) where T : class
        {
            await BindEvent("mouseenter", elementID, callBackMethodName, instance);
        }

        public async void BindMouseOutEvent<T>(string elementID, string callBackMethodName, T instance) where T : class
        {
            await BindEvent("mouseout", elementID, callBackMethodName, instance);
        }









        private async Task BindEvent<T>(string htmlEvemtName, string elementID, string callBackMethodName, T instance) where T : class
        {
            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.BindElementEvent",
                htmlEvemtName,
                 elementID,
                 callBackMethodName,
                 DotNetObjectReference.Create(instance));
        }

    }
}
