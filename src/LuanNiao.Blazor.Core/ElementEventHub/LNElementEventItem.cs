using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LuanNiao.Blazor.Core
{

    public partial class LNElementEventItem
    {
        private readonly IJSRuntime _jSRuntime = null;
        private readonly string _elementID = null;
        private readonly ConcurrentDictionary<string, LNBCBase> _instancePool = new ConcurrentDictionary<string, LNBCBase>();
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
            if (instance is null || _instancePool.ContainsKey(instance.IdentityKey))
            {
                return;
            }
            _instancePool.GetOrAdd(instance.IdentityKey, BindToInstance(instance));
        }

        private LNBCBase BindToInstance<T>([NotNull] T instance) where T : LNBCBase
        {
            var methods = instance.GetType().GetMethods(System.Reflection.BindingFlags.InvokeMethod);
            foreach (var item in methods)
            {
                //if (item.GetCustomAttributes(typeof(LNElementEventAttribute), false))
                //{

                //}
            }
            return instance;
        }


    }
}
