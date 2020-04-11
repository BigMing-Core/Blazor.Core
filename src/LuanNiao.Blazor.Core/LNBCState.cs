using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LuanNiao.Blazor.Core
{
    /// <summary>
    /// You can get the component tree, this tree will initial with the first LuanNiaoBlazor Component
    /// </summary>
    public sealed class LNBCState
    {

        private LNBCState()
        { }
        public static LNBCState Instance = new LNBCState();
        private int _currentID = 0;
        public int GetID()
        {
            return System.Threading.Interlocked.Increment(ref _currentID);
        }
    }


}
