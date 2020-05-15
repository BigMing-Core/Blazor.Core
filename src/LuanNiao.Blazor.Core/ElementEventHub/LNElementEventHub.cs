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
        private readonly ConcurrentDictionary<string, LNElementInstance> _elementInstancePool = new ConcurrentDictionary<string, LNElementInstance>();



        public LNElementEventHub(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
        }

        public LNElementInstance GetElementInstance(string elementID)
            => _elementInstancePool.GetOrAdd(elementID, (e) => new LNElementInstance(_jSRuntime, elementID, (e) => _elementInstancePool.TryRemove(e, out var _)));



    }
}
