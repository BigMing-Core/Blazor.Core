using LuanNiao.Blazor.Core.ElementEventHub.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LuanNiao.Blazor.Core.ElementEventHub
{
    public sealed class LNElementEvent
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
        /// parameter
        /// </summary>
        private readonly object[] _data = new object[1];
        /// <summary>
        /// current attribute info
        /// </summary>
        public readonly LNElementEventAttribute Attribute;

        public LNElementEvent(MethodInfo methodInfo, LNElementEventAttribute attribute)
        {
            Attribute = attribute;
            Method = methodInfo;
            _parameters = Method.GetParameters();
        }
        public void Fire(LNBCBase instance)
        {
            if (instance.Disposed)
            {
                return;
            }
            Method.Invoke(instance, null);
        }

        public void Fire<T>(T data, LNBCBase instance) where T : class
        {
            if (instance.Disposed)
            {
                return;
            }
            if (_parameters.Length == 1 && _parameters[0].ParameterType == typeof(T))
            {
                _data[0] = data;
                Method.Invoke(instance, _data);
            }
            else
            {
                Method.Invoke(instance, null);
            }
        }

    }
}
