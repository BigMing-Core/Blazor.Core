using Microsoft.JSInterop;
using System;
using System.Collections.Generic;

namespace LuanNiao.Blazor.Core.ElementEventHub
{
    /// <summary>
    /// Use to bind the html element instance
    /// </summary>
    public sealed partial class LNElementInstance : IDisposable
    {
        /// <summary>
        /// JSRT
        /// </summary>
        private readonly IJSRuntime _jSRuntime = null;
        /// <summary>
        /// target element id
        /// </summary>
        internal readonly string _elementID;
        private readonly Action<string> _disposingCB;
        /// <summary>
        /// this instance's js ref
        /// </summary>
        private readonly DotNetObjectReference<LNElementInstance> _dotNetObjectReference;

        private readonly Dictionary<int, LNBCBase> _instancePool = new Dictionary<int, LNBCBase>(); 

        private readonly Dictionary<Type, Dictionary<int, LNElementEvent>> _eventPool = new Dictionary<Type, Dictionary<int, LNElementEvent>>();

        public LNElementInstance(IJSRuntime runtime, string elementID, Action<string> disposingCB)
        {
            _dotNetObjectReference = DotNetObjectReference.Create(this);
            _disposingCB = disposingCB;
            _jSRuntime = runtime;
            _elementID = elementID; 
            this.BindElementNotificationEvent("DOMNodeRemoved", nameof(OnElementRemoved));
        }

        public void Remove(int id)
        {
            lock (_eventPool)
            {
                foreach (var item in _eventPool)
                {
                    item.Value.Remove(id);
                }
            } 
        }

        public void Dispose()
        {
            _disposingCB.Invoke(_elementID);
            lock (this)
            {
                _eventPool.Clear();
            }
            _dotNetObjectReference.Dispose();
        }
    }
}
