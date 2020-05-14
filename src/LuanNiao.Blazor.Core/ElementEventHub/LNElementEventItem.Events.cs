using LuanNiao.Blazor.Core.Common;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{

    public partial class LNElementEventItem
    {
        public event Action<WindowEvent> Click;
        [JSInvokable]
        public  void OnClick(WindowEvent windowEvent)
        {

            foreach (var item in _clickMethodPool)
            {
                //_instancePool
                //
                //item.Value
            }
            Click?.Invoke(windowEvent);
        }

        public event Action<WindowEvent> MouseOver;
        [JSInvokable]
        public  void OnMouseOver(WindowEvent windowEvent)
        {
            MouseOver?.Invoke(windowEvent);
        }


        public event Action<WindowEvent> MouseEnter;
        public event Action<WindowEvent> MouseDown;
        public event Action<WindowEvent> MouseUp;
        public event Action<WindowEvent> MouseMove;
        public event Action<WindowEvent> MouseOut;
        public event Action<WindowEvent> ContextMenu;


        public event Action<WindowEvent> Focus;
        public event Action<WindowEvent> FocusIn;
        public event Action<WindowEvent> FocusOut;
        public event Action<WindowEvent> Input;
        public event Action<WindowEvent> Reset;
        public event Action<WindowEvent> Search;
        public event Action<WindowEvent> Blur;
        public event Action<WindowEvent> Change;

        public event Action<KeyboardEvent> KeyUp;
        public event Action<KeyboardEvent> KeyPress;
        public event Action<KeyboardEvent> KeyDown;


    }
}
