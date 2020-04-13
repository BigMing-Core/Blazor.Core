﻿using LuanNiao.Blazor.Core.Common;
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

        public async void BindClickEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("click", elementID, callBackMethodName, instance, isPreventDefault);
        }

        public async void BindMouseOverEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mouseover", elementID, callBackMethodName, instance, isPreventDefault);
        }

        public async void BindMouseEnterEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mouseenter", elementID, callBackMethodName, instance, isPreventDefault);
        }

        public async void BindMouseOutEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mouseout", elementID, callBackMethodName, instance, isPreventDefault);
        }

        public async void BindContextMenuEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("contextmenu", elementID, callBackMethodName, instance, isPreventDefault);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "dispose this when the instance disposing..")]
        private async Task BindEvent<T>(string htmlEvemtName, string elementID, string callBackMethodName, T instance, bool isPreventDefault) where T : LNBCBase
        {
            var jsInstance = DotNetObjectReference.Create(instance);

            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.BindElementEvent",
                htmlEvemtName,
                 elementID,
                 callBackMethodName,
                 jsInstance,
                 isPreventDefault);
            instance.Disposing += () =>
            {
                jsInstance.Dispose();
            };

        }

    }
}