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
        private readonly Dictionary<int, LNElementEvent> _clickEventPool = new Dictionary<int, LNElementEvent>();

        private readonly Dictionary<int, LNElementEvent> _onMouseOverEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onMouseEnterEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onMouseDownEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onMouseUpEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onMouseMoveEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onMouseOutEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onContextMenuEventPool = new Dictionary<int, LNElementEvent>();

        private readonly Dictionary<int, LNElementEvent> _onBlurEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onChangeEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onFocusEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onFocusInEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onFocusOutEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onInputEventPool = new Dictionary<int, LNElementEvent>(); 


        private readonly Dictionary<int, LNElementEvent> _onKeyDownEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onKeypressEventPool = new Dictionary<int, LNElementEvent>();
        private readonly Dictionary<int, LNElementEvent> _onKeyupEventPool = new Dictionary<int, LNElementEvent>(); 

        private readonly Dictionary<int, LNElementEvent> _onScrollEventPool = new Dictionary<int, LNElementEvent>(); 
        public LNElementInstance(IJSRuntime runtime, string elementID, Action<string> disposingCB)
        {
            _dotNetObjectReference = DotNetObjectReference.Create(this);
            _disposingCB = disposingCB;
            _jSRuntime = runtime;
            _elementID = elementID;
            _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.RegistElementEventHub", elementID, _dotNetObjectReference);
        }

        public void Remove(int id)
        {
            _clickEventPool.Remove(id);
            _instancePool.Remove(id);
            _onMouseEnterEventPool.Remove(id);
            _onMouseOverEventPool.Remove(id);
            _onMouseDownEventPool.Remove(id);
            _onMouseUpEventPool.Remove(id);
            _onMouseMoveEventPool.Remove(id);
            _onMouseOutEventPool.Remove(id);
            _onContextMenuEventPool.Remove(id);
            _onBlurEventPool.Remove(id);
            _onChangeEventPool.Remove(id);
            _onFocusEventPool.Remove(id);
            _onFocusInEventPool.Remove(id);
            _onFocusOutEventPool.Remove(id);
            _onInputEventPool.Remove(id);
            _onKeyDownEventPool.Remove(id);
            _onKeypressEventPool.Remove(id);
            _onKeyupEventPool.Remove(id);
            _onScrollEventPool.Remove(id);
        }

        public void Dispose()
        {
            _disposingCB.Invoke(_elementID);
            lock (this)
            {
                _clickEventPool.Clear();
                _instancePool.Clear();
                _onMouseDownEventPool.Clear();
                _onMouseEnterEventPool.Clear();
                _onMouseOverEventPool.Clear();
                _onMouseUpEventPool.Clear();
                _onMouseMoveEventPool.Clear();
                _onMouseOutEventPool.Clear();
                _onContextMenuEventPool.Clear();
                _onBlurEventPool.Clear();
                _onChangeEventPool.Clear();
                _onFocusEventPool.Clear();
                _onFocusInEventPool.Clear();
                _onFocusOutEventPool.Clear();
                _onInputEventPool.Clear();
                _onKeyDownEventPool.Clear();
                _onKeypressEventPool.Clear();
                _onKeyupEventPool.Clear();
                _onScrollEventPool.Clear();
            }
            _dotNetObjectReference.Dispose();
        }
    }
}
