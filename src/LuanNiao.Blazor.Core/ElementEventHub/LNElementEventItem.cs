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
        private readonly Dictionary<Type, List<LNElementInstance>> _clickMethodPool = new Dictionary<Type, List<LNElementInstance>>();
        private readonly Dictionary<Type, List<LNElementInstance>> _mouseOverMethodPool = new Dictionary<Type, List<LNElementInstance>>();

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
            var methods = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
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
                                    var eventItem = new LNElementInstance(item);
                                    eventItem.TargetInstance.Add(instance);
                                    if (_clickMethodPool.ContainsKey(typeInfo))
                                    {                                    
                                        _clickMethodPool[typeInfo].Add(eventItem);
                                    }
                                    else
                                    {
                                        _clickMethodPool.Add(typeInfo,new List<LNElementInstance>() { eventItem });
                                    }
                                }
                            }
                            break;
                        case ElementEventType.OnMouseOver:
                            {
                                lock (_clickMethodPool)
                                {
                                    var eventItem = new LNElementInstance(item);
                                    eventItem.TargetInstance.Add(instance);
                                    if (_clickMethodPool.ContainsKey(typeInfo))
                                    {
                                        _clickMethodPool[typeInfo].Add(eventItem);
                                    }
                                    else
                                    {
                                        _clickMethodPool.Add(typeInfo, new List<LNElementInstance>() { eventItem });
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            

        }

      


    }
}
