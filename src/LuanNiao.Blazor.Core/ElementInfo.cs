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
            return await _jSRuntime.InvokeAsync<ElementRects>("LuanNiaoBlazor.GetElementClientRects", id);
        }



        public async void BindClickEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("click", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindMouseOverEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mouseover", elementID, callBackMethodName, instance, isPreventDefault, async);
        }



        public async void BindMouseEnterEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mouseenter", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindMouseDownEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mousedown", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindMouseUpEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mouseup", elementID, callBackMethodName, instance, isPreventDefault, async);
        }
        public async void BindMouseMoveEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mousemove", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindMouseOutEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("mouseout", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindContextMenuEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("contextmenu", elementID, callBackMethodName, instance, isPreventDefault, async);
        }


        public async void BindFocusEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("focus", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindFocusinEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("focusin", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindFocusoutEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("focusout", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindInputEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("input", elementID, callBackMethodName, instance, isPreventDefault, async);
        }

        public async void BindResetEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("reset", elementID, callBackMethodName, instance, isPreventDefault, async);
        }
        public async void BindSearchEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("search", elementID, callBackMethodName, instance, isPreventDefault, async);
        }
        public async void BindBlurEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("blur", elementID, callBackMethodName, instance, isPreventDefault, async);
        }
        public async void BindChangeEvent<T>(string elementID, string callBackMethodName, T instance, bool isPreventDefault = false, bool async = true) where T : LNBCBase
        {
            if (instance == null)
            {
                return;
            }
            await BindEvent("change", elementID, callBackMethodName, instance, isPreventDefault, async);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "dispose this when the instance disposing..")]
        private async Task BindEvent<T>(string htmlEventName, string elementID, string callBackMethodName, T instance, bool isPreventDefault, bool async) where T : LNBCBase
        {
            var jsInstance = DotNetObjectReference.Create(instance);

            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.BindElementEvent",
                htmlEventName,
                 elementID,
                 callBackMethodName,
                 jsInstance,
                 isPreventDefault,
                 async);
            instance.Disposing += () =>
            {
                jsInstance.Dispose();
            };

        }

    }
}
