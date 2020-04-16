using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core.Common
{
    public struct WindowSize
    {
        public struct InnerSizeInfo
        {
            public int Height { get; set; }
            public int Width { get; set; }
        }
        public InnerSizeInfo InnerSize { get; set; }
    }

}
