using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core.Common.Enums
{
    [Flags]
    public enum MouseEventButton
    {
        Left = 0,
        Middle = 1,
        Right = 2,
        Back = 3,
        Forward = 4
    }

    public enum MouseEventButtons
    {
        None = 0,
        Left = 1,
        Right = 2,
        Middle = 4,
        Back = 8,
        Forward = 16
    }
}
