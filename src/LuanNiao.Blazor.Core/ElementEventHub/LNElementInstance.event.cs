using LuanNiao.Blazor.Core.Common;
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
        public void OnClick(MouseEvent mouseEvent)
        {
            foreach (var item in _clickEventPool)
            {
                item.Value.Fire(mouseEvent, _instancePool[item.Key]);
            }
        }
        [JSInvokable]
        public void OnMouseOver(MouseEvent mouseEvent)
        {
            foreach (var item in _onMouseOverEventPool)
            {
                item.Value.Fire(mouseEvent, _instancePool[item.Key]);
            }
        }


        [JSInvokable]
        public void OnMouseEnter(MouseEvent mouseEvent)
        {
            foreach (var item in _onMouseEnterEventPool)
            {
                item.Value.Fire(mouseEvent, _instancePool[item.Key]);
            }
        }

        [JSInvokable]
        public void OnMouseDown(MouseEvent mouseEvent)
        {
            foreach (var item in _onMouseDownEventPool)
            {
                item.Value.Fire(mouseEvent, _instancePool[item.Key]);
            }
        }


        [JSInvokable]
        public void OnMouseUp(MouseEvent mouseEvent)
        {
            foreach (var item in _onMouseUpEventPool)
            {
                item.Value.Fire(mouseEvent, _instancePool[item.Key]);
            }
        }



        [JSInvokable]
        public void OnMouseMove(MouseEvent mouseEvent)
        {
            foreach (var item in _onMouseMoveEventPool)
            {
                item.Value.Fire(mouseEvent, _instancePool[item.Key]);
            }
        }




        [JSInvokable]
        public void OnMouseOut(MouseEvent mouseEvent)
        {
            foreach (var item in _onMouseOutEventPool)
            {
                item.Value.Fire(mouseEvent, _instancePool[item.Key]);
            }
        }

        [JSInvokable]
        public void OnContextMenu(MouseEvent mouseEvent)
        {
            foreach (var item in _onContextMenuEventPool)
            {
                item.Value.Fire(mouseEvent, _instancePool[item.Key]);
            }
        }

        [JSInvokable]
        public void OnBlur()
        {
            foreach (var item in _onBlurEventPool)
            {
                item.Value.Fire(_instancePool[item.Key]);
            }
        }

        [JSInvokable]
        public void OnFocus()
        {
            foreach (var item in _onFocusEventPool)
            {
                item.Value.Fire(_instancePool[item.Key]);
            }
        }

        [JSInvokable]
        public void OnFocusIn()
        {
            foreach (var item in _onFocusInEventPool)
            {
                item.Value.Fire(_instancePool[item.Key]);
            }
        }

        [JSInvokable]
        public void OnFocusOut()
        {
            foreach (var item in _onFocusOutEventPool)
            {
                item.Value.Fire(_instancePool[item.Key]);
            }
        }

        [JSInvokable]
        public void OnChange()
        {
            foreach (var item in _onChangeEventPool)
            {
                item.Value.Fire(_instancePool[item.Key]);
            }
        }


        [JSInvokable]
        public void OnInput()
        {
            foreach (var item in _onInputEventPool)
            {
                item.Value.Fire(_instancePool[item.Key]);
            }
        }


        [JSInvokable]
        public void OnKeyDown(KeyboardEvent keyboardEvent)
        {
            foreach (var item in _onKeyDownEventPool)
            {
                item.Value.Fire(keyboardEvent, _instancePool[item.Key]);
            }
        }

        [JSInvokable]
        public void OnKeypress(KeyboardEvent keyboardEvent)
        {
            foreach (var item in _onKeypressEventPool)
            {
                item.Value.Fire(keyboardEvent, _instancePool[item.Key]);
            }
        }


        [JSInvokable]
        public void OnKeyup(KeyboardEvent keyboardEvent)
        {
            foreach (var item in _onKeyupEventPool)
            {
                item.Value.Fire(keyboardEvent, _instancePool[item.Key]);
            }
        }


        [JSInvokable]
        public void OnScroll(ElementScrollEvent scrollEvent)
        {
            foreach (var item in _onScrollEventPool)
            {
                item.Value.Fire(scrollEvent, _instancePool[item.Key]);
            }
        }


    }
}
