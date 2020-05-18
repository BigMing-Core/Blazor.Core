using LuanNiao.Blazor.Core.Common;
using LuanNiao.Blazor.Core.ElementEventHub.Attributes;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core.ElementEventHub
{
    public sealed partial class LNElementInstance
    {


        [JSInvokable]
        public void OnElementRemoved()
        {
            this.Dispose();
        }

        [JSInvokable]
        public void OnClick(MouseEvent mouseEvent) => FireMouseEvent(mouseEvent, typeof(OnClickEventAttribute));
        [JSInvokable]
        public void OnMouseOver(MouseEvent mouseEvent) => FireMouseEvent(mouseEvent, typeof(OnMouseOverEventAttribute));


        [JSInvokable]
        public void OnMouseEnter(MouseEvent mouseEvent) => FireMouseEvent(mouseEvent, typeof(OnMouseEnterEventAttribute));

        [JSInvokable]
        public void OnMouseDown(MouseEvent mouseEvent) => FireMouseEvent(mouseEvent, typeof(OnMouseDownEventAttribute));


        [JSInvokable]
        public void OnMouseUp(MouseEvent mouseEvent) => FireMouseEvent(mouseEvent, typeof(OnMouseUpEventAttribute));


        [JSInvokable]
        public void OnMouseMove(MouseEvent mouseEvent) => FireMouseEvent(mouseEvent, typeof(OnMouseMoveEventAttribute));

        [JSInvokable]
        public void OnMouseOut(MouseEvent mouseEvent) => FireMouseEvent(mouseEvent, typeof(OnMouseOutEventAttribute));

        [JSInvokable]
        public void OnContextMenu(MouseEvent mouseEvent) => FireMouseEvent(mouseEvent, typeof(OnContextMenuEventAttribute));

        [JSInvokable]
        public void OnBlur() => FireNotifactionEvent(typeof(OnBlurEventAttribute));
        [JSInvokable]
        public void OnFocus() => FireNotifactionEvent(typeof(OnFocusOutEventAttribute));

        [JSInvokable]
        public void OnFocusIn() => FireNotifactionEvent(typeof(OnFocusInEventAttribute));

        [JSInvokable]
        public void OnFocusOut() => FireNotifactionEvent(typeof(OnFocusOutEventAttribute));

        [JSInvokable]
        public void OnChange() => FireNotifactionEvent(typeof(OnChangeEventAttribute));


        [JSInvokable]
        public void OnInput() => FireNotifactionEvent(typeof(OnInputEventAttribute));


        [JSInvokable]
        public void OnKeyDown(KeyboardEvent keyboardEvent) => FireKeyboardEvent(keyboardEvent, typeof(OnKeyDownEventAttribute));

        [JSInvokable]
        public void OnKeypress(KeyboardEvent keyboardEvent) => FireKeyboardEvent(keyboardEvent, typeof(OnKeypressEventAttribute));


        [JSInvokable]
        public void OnKeyup(KeyboardEvent keyboardEvent) => FireKeyboardEvent(keyboardEvent, typeof(OnKeyupEventAttribute));


        [JSInvokable]
        public void OnScroll(ElementScrollInfo scrollEvent) => FireScrollEvent(scrollEvent, typeof(OnScrollEventAttribute));



        private void FireMouseEvent(MouseEvent mouseEvent, Type eventType)
        {
            if (_eventPool.ContainsKey(eventType))
            {
                var targetMap = _eventPool[eventType];
                foreach (var eItem in targetMap)
                {
                    eItem.Value.Fire(mouseEvent, _instancePool[eItem.Key]);
                }
            }
        }
        private void FireNotifactionEvent(Type eventType)
        {
            if (_eventPool.ContainsKey(eventType))
            {
                var targetMap = _eventPool[eventType];
                foreach (var eItem in targetMap)
                {
                    eItem.Value.Fire(_instancePool[eItem.Key]);
                }
            }
        }
        private void FireKeyboardEvent(KeyboardEvent keyboardEvent, Type eventType)
        {
            if (_eventPool.ContainsKey(eventType))
            {
                var targetMap = _eventPool[eventType];
                foreach (var eItem in targetMap)
                {
                    eItem.Value.Fire(keyboardEvent, _instancePool[eItem.Key]);
                }
            }
        }

        private void FireScrollEvent(ElementScrollInfo keyboardEvent, Type eventType)
        {
            if (_eventPool.ContainsKey(eventType))
            {
                var targetMap = _eventPool[eventType];
                foreach (var eItem in targetMap)
                {
                    eItem.Value.Fire(keyboardEvent, _instancePool[eItem.Key]);
                }
            }
        }
    }
}
