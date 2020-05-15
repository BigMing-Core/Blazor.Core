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
    }
}
