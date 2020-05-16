using LuanNiao.Blazor.Core.ElementEventHub.Attributes;
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

            Dictionary<int, LNElementEvent> eventPool = null;
            foreach (var methodName in methodNames)
            {
                var typeInfo = instance.GetType();
                var method = typeInfo.GetMethod(methodName);
                var attr = method.GetCustomAttribute<LNElementEventAttribute>();
                switch (attr)
                {
                    case OnClickEventAttribute _ :
                        eventPool = _clickEventPool;
                        break;
                    case OnMouseOverEventAttribute  _:
                        eventPool = _onMouseOverEventPool;
                        break;
                    case OnMouseEnterEventAttribute  _:
                        eventPool = _onMouseEnterEventPool;
                        break;
                    case OnMouseDownEventAttribute _:
                        eventPool = _onMouseDownEventPool;
                        break;
                    case OnMouseUpEventAttribute _:
                        eventPool = _onMouseUpEventPool;
                        break;
                    case OnMouseMoveEventAttribute _:
                        eventPool = _onMouseMoveEventPool;
                        break;
                    case OnMouseOutEventAttribute _:
                        eventPool = _onMouseOutEventPool;
                        break;
                    case OnContextMenuEventAttribute _:
                        eventPool = _onContextMenuEventPool;
                        break;
                    case OnBlurEventAttribute _:
                        eventPool = _onBlurEventPool;
                        break;
                    case OnChangeEventAttribute _:
                        eventPool = _onChangeEventPool;
                        break;
                    case OnFocusEventAttribute _:
                        eventPool = _onFocusEventPool;
                        break;
                    case OnFocusInEventAttribute _:
                        eventPool = _onFocusInEventPool;
                        break;
                    case OnFocusOutEventAttribute _:
                        eventPool = _onFocusOutEventPool;
                        break;
                    case OnInputEventAttribute _:
                        eventPool = _onInputEventPool;
                        break;
                    case OnKeyDownEventAttribute _:
                        eventPool = _onKeyDownEventPool;
                        break;
                    case OnKeypressEventAttribute _:
                        eventPool = _onKeypressEventPool;
                        break;
                    case OnKeyupEventAttribute _:
                        eventPool = _onKeyupEventPool;
                        break;
                    case OnScrollEventAttribute _:
                        eventPool = _onScrollEventPool;
                        break;
                    default:
                        break;
                }


                lock (eventPool)
                {
                    if (!eventPool.ContainsKey(instance.CreateSequence))
                    {
                        eventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                    }
                }


            }

            
            return this;
        }
    }
}
