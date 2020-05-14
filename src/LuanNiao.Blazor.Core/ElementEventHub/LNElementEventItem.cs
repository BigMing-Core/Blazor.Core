using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace LuanNiao.Blazor.Core
{

    public partial class LNElementEventItem
    {
        private readonly IJSRuntime _jSRuntime = null;
        private readonly string _elementID = null;
        private readonly Dictionary<string, LNBCBase> _instancePool = new Dictionary<string, LNBCBase>();
        private readonly Dictionary<Type, List<MethodInfo>> _clickMethodPool = new Dictionary<Type, List<MethodInfo>>();
        private readonly Dictionary<Type, List<MethodInfo>> _mouseOverMethodPool = new Dictionary<Type, List<MethodInfo>>();

        public LNElementEventItem(string elementID, IJSRuntime runtime)
        {
            _elementID = elementID;
            _jSRuntime = runtime;
            BindEvent();
        }


        private void BindEvent()
        {
            _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.RegistElementEventHub", _elementID, DotNetObjectReference.Create(this));
        }


        public void Bind<T>(T instance) where T : LNBCBase
        {
            lock (_instancePool)
            {
                if (instance is null || _instancePool.ContainsKey(instance.IdentityKey))
                {
                    return;
                }
                _instancePool.Add(instance.IdentityKey, BindToInstance(instance));
            }

        }

        private LNBCBase BindToInstance<T>([NotNull] T instance) where T : LNBCBase
        {
            var methods = instance.GetType().GetMethods(System.Reflection.BindingFlags.InvokeMethod);
            var typeInfo = instance.GetType();
            foreach (var item in methods)
            {
                var attr = item.GetCustomAttribute(typeof(LNElementEventAttribute), false);
                if (attr is LNElementEventAttribute lnAttr)
                {
                    switch (lnAttr._eventType)
                    {
                        case ElementEventType.OnClick:
                            {
                                lock (_clickMethodPool)
                                {
                                    if (_clickMethodPool.ContainsKey(typeInfo))
                                    {
                                        _clickMethodPool[typeInfo].Add(item);
                                    }
                                    else
                                    {
                                        _clickMethodPool.Add(typeInfo, new List<MethodInfo> { item });
                                    }
                                }
                            }
                            break;
                        case ElementEventType.OnMouseOver:
                            {
                                lock (_mouseOverMethodPool)
                                {
                                    if (_mouseOverMethodPool.ContainsKey(typeInfo))
                                    {
                                        _mouseOverMethodPool[typeInfo].Add(item);
                                    }
                                    else
                                    {
                                        _mouseOverMethodPool.Add(typeInfo, new List<MethodInfo> { item });
                                    }
                                }
                            } 
                            break;
                        default:
                            break;
                    }
                }
            }
            return instance;
        }


    }
}
