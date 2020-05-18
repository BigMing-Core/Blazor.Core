using LuanNiao.Blazor.Core.ElementEventHub.Attributes;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core.ElementEventHub
{
    public sealed partial class LNElementInstance
    {


        public LNElementInstance Bind(LNBCBase instance, params string[] methodNames)
        {
            if (!_instancePool.ContainsKey(instance.CreateSequence))
            {
                lock (_instancePool)
                {
                    if (!_instancePool.ContainsKey(instance.CreateSequence))
                    {
                        _instancePool.Add(instance.CreateSequence, instance);
                        instance.Disposing += () =>
                        {
                            Remove(instance.CreateSequence);
                        };
                    }

                }
            }

            foreach (var methodName in methodNames)
            {
                var typeInfo = instance.GetType();
                var method = typeInfo.GetMethod(methodName);
                var attr = method.GetCustomAttribute<LNElementEventAttribute>();
                var attrType = attr.GetType();
                if (!_eventPool.ContainsKey(attrType))
                {
                    lock (_eventPool)
                    {
                        if (!_eventPool.ContainsKey(attrType))
                        {
                            _eventPool.Add(attrType, new Dictionary<int, LNElementEvent>());
                        }
                        switch (attr)
                        {
                            case OnClickEventAttribute _:
                                this.BindElementMouseEvent("click", nameof(OnClick));
                                break;
                            case OnMouseOverEventAttribute _:
                                this.BindElementMouseEvent("mouseover", nameof(OnMouseOver));
                                break;
                            case OnMouseEnterEventAttribute _:
                                this.BindElementMouseEvent("mouseenter", nameof(OnMouseEnter));
                                break;
                            case OnMouseDownEventAttribute _:
                                this.BindElementMouseEvent("mousedown", nameof(OnMouseDown));
                                break;
                            case OnMouseUpEventAttribute _:
                                this.BindElementMouseEvent("mouseup", nameof(OnMouseUp));
                                break;
                            case OnMouseMoveEventAttribute _:
                                this.BindElementMouseEvent("mousemove", nameof(OnMouseMove));
                                break;
                            case OnMouseOutEventAttribute _:
                                this.BindElementMouseEvent("mouseout", nameof(OnMouseOut));
                                break;
                            case OnContextMenuEventAttribute _:
                                this.BindElementMouseEvent("contextmenu", nameof(OnContextMenu));
                                break;
                            case OnBlurEventAttribute _:
                                this.BindElementNotificationEvent("blur", nameof(OnBlur));
                                break;
                            case OnChangeEventAttribute _:
                                this.BindElementNotificationEvent("change", nameof(OnChange));
                                break;
                            case OnFocusEventAttribute _:
                                this.BindElementNotificationEvent("focus", nameof(OnFocus));
                                break;
                            case OnFocusInEventAttribute _:
                                this.BindElementNotificationEvent("focusin", nameof(OnFocusIn));
                                break;
                            case OnFocusOutEventAttribute _:
                                this.BindElementNotificationEvent("focusout", nameof(OnFocusOut));
                                break;
                            case OnInputEventAttribute _:
                                this.BindElementNotificationEvent("input", nameof(OnInput));
                                break;

                            case OnKeyDownEventAttribute _:
                                this.BindElementKeyboardEvent("keydown", nameof(OnKeyDown));
                                break;
                            case OnKeypressEventAttribute _:
                                this.BindElementKeyboardEvent("keypress", nameof(OnKeypress));
                                break;
                            case OnKeyupEventAttribute _:
                                this.BindElementKeyboardEvent("keyup", nameof(OnKeyup));
                                break;
                            case OnScrollEventAttribute _:
                                this.BindElementScroll(nameof(OnScroll));
                                break;
                            default:
                                break;
                        }
                    }

                }
                
                lock (_eventPool)
                {                    
                    if (!_eventPool[attrType].ContainsKey(instance.CreateSequence))
                    {
                        _eventPool[attrType].Add(instance.CreateSequence, new LNElementEvent(method, attr));
                    }
                }
            }


            return this;
        }

        private async void BindElementMouseEvent(string eventName, string methodName)
        {
            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.ElementEventHub.BindElementMouseEvent", _elementID, eventName, _dotNetObjectReference, methodName);
        }

        private async void BindElementNotificationEvent(string eventName, string methodName)
        {
            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.ElementEventHub.BindElementNotificationEvent", _elementID, eventName, _dotNetObjectReference, methodName);
        }

        private async void BindElementKeyboardEvent(string eventName, string methodName)
        {
            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.ElementEventHub.BindElementKeyboardEvent", _elementID, eventName, _dotNetObjectReference, methodName);
        }


        private async void BindElementScroll(string methodName)
        {
            await _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.ElementEventHub.BindElementScroll", _elementID, _dotNetObjectReference, methodName);
        }
    }
}
