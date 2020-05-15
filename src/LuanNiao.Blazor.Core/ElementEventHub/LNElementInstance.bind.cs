﻿using LuanNiao.Blazor.Core.ElementEventHub.Attributes;
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
                if (attr is OnClickEventAttribute clickEventAttribute)
                {
                    lock (_clickEventPool)
                    {
                        if (!_clickEventPool.ContainsKey(instance.CreateSequence))
                        {
                            _clickEventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                        }
                    }
                }
                else if (attr is OnMouseOverEventAttribute onMouseOver)
                {
                    lock (_onMouseOverEventPool)
                    {
                        if (!_onMouseOverEventPool.ContainsKey(instance.CreateSequence))
                        {
                            _onMouseOverEventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                        }
                    }
                }
                else if (attr is OnMouseEnterEventAttribute  onMouseEnter)
                {
                    lock (_onMouseEnterEventPool)
                    {
                        if (!_onMouseEnterEventPool.ContainsKey(instance.CreateSequence))
                        {
                            _onMouseEnterEventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                        }
                    }
                }
                else if (attr is OnMouseDownEventAttribute  onMouseDown)
                {
                    lock (_onMouseDownEventPool)
                    {
                        if (!_onMouseDownEventPool.ContainsKey(instance.CreateSequence))
                        {
                            _onMouseDownEventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                        }
                    }
                }
                else if (attr is OnMouseUpEventAttribute onMouseUp)
                {
                    lock (_onMouseUpEventPool)
                    {
                        if (!_onMouseUpEventPool.ContainsKey(instance.CreateSequence))
                        {
                            _onMouseUpEventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                        }
                    }
                }
                else if (attr is OnMouseMoveEventAttribute  onMouseMove)
                {
                    lock (_onMouseMoveEventPool)
                    {
                        if (!_onMouseMoveEventPool.ContainsKey(instance.CreateSequence))
                        {
                            _onMouseMoveEventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                        }
                    }
                }
                else if (attr is OnMouseOutEventAttribute  onMouseOut)
                {
                    lock (_onMouseOutEventPool)
                    {
                        if (!_onMouseOutEventPool.ContainsKey(instance.CreateSequence))
                        {
                            _onMouseOutEventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                        }
                    }
                }
                else if (attr is OnContextMenuEventAttribute  onContextMenu)
                {
                    lock (_onContextMenuEventPool)
                    {
                        if (!_onContextMenuEventPool.ContainsKey(instance.CreateSequence))
                        {
                            _onContextMenuEventPool.Add(instance.CreateSequence, new LNElementEvent(method, attr));
                        }
                    }
                }

            }
            return this;
        }
    }
}
