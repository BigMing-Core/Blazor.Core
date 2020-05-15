using LuanNiao.Blazor.Core.ElementEventHub;
using LuanNiao.Blazor.Core.ElementEventHub.Attributes;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    public class LNElementEventHub
    {
        private readonly IJSRuntime _jSRuntime = null;
        private readonly List<int> _registedInstance = new List<int>(10000);
        private readonly Dictionary<Type, LNElementInstance> _clickMethodPool = new Dictionary<Type, LNElementInstance>();


        public LNElementEventHub(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
        }

        public void RegistInstance<T>(T instance) where T : LNBCBase
        {
            lock (_registedInstance)
            {
                if (_registedInstance.Contains(instance.CreateSequence))
                {
                    return;
                }
                _registedInstance.Add(instance.CreateSequence);
            }

            var typeInfo = instance.GetType();
            var methods = typeInfo.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var item in methods)
            {
                var attr = item.GetCustomAttribute<LNElementEventAttribute>();
                if (attr is OnClickEventAttribute clickEventAttribute)
                {
                    lock (_clickMethodPool)
                    {
                        if (!_clickMethodPool.ContainsKey(typeInfo))
                        {
                            _clickMethodPool.Add(typeInfo, new LNElementInstance(item, clickEventAttribute, _jSRuntime));
                        }
                        _clickMethodPool[typeInfo].AddInstance(instance);
                    }
                }
            }

        }
    }

}
