using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class LNElementEventAttribute : Attribute
    {
        public readonly ElementEventType _eventType;
        public LNElementEventAttribute(ElementEventType elementEventType)
        {
            _eventType = elementEventType;
        }
    }

    public enum ElementEventType
    { 
        OnClick,
        OnMouseOver
    }
}
