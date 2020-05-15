using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    public class LNElementInstance
    {
        public  MethodInfo Method { get; private set; }
        public List<LNBCBase> TargetInstance { get; private set; } = new List<LNBCBase>();


        public LNElementInstance(MethodInfo methodInfo)
        {
            Method = methodInfo;
        }
    }
}
