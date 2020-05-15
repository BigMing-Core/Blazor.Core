using LuanNiao.Blazor.Core.Common;
using LuanNiao.Blazor.Core.ElementEventHub.Attributes;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LuanNiao.Blazor.Core.ElementEventHub
{
    public sealed class LNElementInstance : IDisposable
    {
        /// <summary>
        /// Method info use to invoke
        /// </summary>
        public MethodInfo Method { get; private set; }
        /// <summary>
        /// this method's parameter
        /// </summary>
        private readonly ParameterInfo[] _parameters;
        /// <summary>
        /// JSRT
        /// </summary>
        private readonly IJSRuntime _jSRuntime = null;
        /// <summary>
        /// this instance's js ref
        /// </summary>
        private readonly DotNetObjectReference<LNElementInstance> _dotNetObjectReference;
        private readonly object[] _data = new object[1];
        /// <summary>
        /// current attribute info
        /// </summary>
        public readonly LNElementEventAttribute Attribute;
        /// <summary>
        /// all LNBCBase instance, use to invoke
        /// </summary>
        private readonly List<LNBCBase> _targetInstance = new List<LNBCBase>();


        public LNElementInstance(MethodInfo methodInfo, LNElementEventAttribute attribute, IJSRuntime runtime)
        {
            Attribute = attribute;
            Method = methodInfo;
            _parameters = Method.GetParameters();
            _jSRuntime = runtime;
            _dotNetObjectReference = DotNetObjectReference.Create(this);
            _jSRuntime.InvokeVoidAsync("LuanNiaoBlazor.BindElementMouseEvent",
               "click",
                attribute._elementName,
                nameof(MouseEvent),
                _dotNetObjectReference);
            //todo the lementID must be is a parameter
        }

        [JSInvokable]
        public void MouseEvent(MouseEvent mouseEvent)
        {

            if (_parameters.Length == 0)
            {
                _targetInstance.ForEach(item =>
                {
                    Method.Invoke(item, null);
                });
            }
            else if (_parameters.Length == 1 && _parameters[0].ParameterType == EventTypeInfos.MouseEvent)
            {
                _data[0] = mouseEvent;
                _targetInstance.ForEach(item =>
                {
                    Method.Invoke(item, _data);
                });
            }
        }

        public void AddInstance(LNBCBase instance)
        {
            lock (_targetInstance)
            {
                _targetInstance.Add(instance);
            }
            instance.Disposing += () => {
                lock (_targetInstance)
                {
                    _targetInstance.Remove(instance);
                }
            };
        }
        public void RemoveInstance(LNBCBase instance)
        {
            lock (_targetInstance)
            {
                _targetInstance.Remove(instance);
            }
        }

        public void Dispose()
        {
            _dotNetObjectReference.Dispose();
        }
    }
}
