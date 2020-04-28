using LuanNiao.Blazor.Core.Common;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core
{
    public sealed class BodyEventHub
    {
        private readonly IJSRuntime _jSRuntime = null;


        public BodyEventHub(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
            BindEvent("click", nameof(ClickCB), false, true);
            BindEvent("mouseover", nameof(MouseOverCB), false, true);
            BindEvent("mouseenter", nameof(MousEnterCB), false, true);
            BindEvent("mouseup", nameof(MouseUpCB), false, true);
            BindEvent("mousedown", nameof(MousDownCB), false, true);
            BindEvent("mousemove", nameof(MouseMoveCB), false, true);
            BindEvent("mouseout", nameof(MouseOutCB), false, true);
            BindEvent("contextmenu", nameof(ContextMenuCB), false, true);
        }

        public event Action<WindowEvent> MouseMove;
        public event Action<WindowEvent> MouseOut;
        public event Action<WindowEvent> ContextMenu;
        public event Action<WindowEvent> MousDown;
        public event Action<WindowEvent> MouseUp;
        public event Action<WindowEvent> MouseEnter;
        public event Action<WindowEvent> MouseOver;
        public event Action<WindowEvent> Click;

        [JSInvokable]
        public void MouseMoveCB(WindowEvent e)
        {
            MouseMove?.Invoke(e);
        }

        [JSInvokable]
        public void MouseOutCB(WindowEvent e)
        {
            MouseOut?.Invoke(e);
        }
        [JSInvokable]
        public void ContextMenuCB(WindowEvent e)
        {
            ContextMenu?.Invoke(e);
        }


        private void BindEvent(string htmlEventName, string callBackMethodName, bool isPreventDefault, bool async)
        {
            var jsInstance = DotNetObjectReference.Create(this);

            _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.BindBodyEvent",
                    htmlEventName,
                 callBackMethodName,
                 jsInstance,
                 isPreventDefault,
                 async);

        }
        [JSInvokable]
        public void MousDownCB(WindowEvent e)
        {
            MousDown?.Invoke(e);
        }

        [JSInvokable]
        public void MouseUpCB(WindowEvent e)
        {
            MouseUp?.Invoke(e); 
        }
        [JSInvokable]
        public void MousEnterCB(WindowEvent e)
        {
            MouseEnter?.Invoke(e);
        }

        [JSInvokable]
        public void MouseOverCB(WindowEvent e)
        {
            MouseOver?.Invoke(e);
        }

        [JSInvokable]
        public void ClickCB(WindowEvent e)
        {
            Click?.Invoke(e);
        }




    }
}
