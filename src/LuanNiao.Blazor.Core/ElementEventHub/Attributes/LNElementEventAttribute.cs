using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core.ElementEventHub.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public abstract class LNElementEventAttribute : Attribute
    {
        internal readonly string _elementName;
        public LNElementEventAttribute(string elementName)
        {
            _elementName = elementName;
        }
    }
}
