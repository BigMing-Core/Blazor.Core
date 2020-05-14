using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    public class LNElementEventHub
    {
        private readonly IJSRuntime _jSRuntime = null;
        private readonly ConcurrentDictionary<string, LNElementEventItem> _elementPool = new ConcurrentDictionary<string, LNElementEventItem>();

        public LNElementEventHub(IJSRuntime runtime)
        {
            _jSRuntime = runtime;
        }

        public LNElementEventItem GetElementItem(string elementID)
        {
            return _elementPool.GetOrAdd(elementID, (e) => {
                return new LNElementEventItem(e, _jSRuntime);
            });
        }

    }

}
