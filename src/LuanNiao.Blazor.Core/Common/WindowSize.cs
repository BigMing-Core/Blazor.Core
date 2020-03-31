using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core.Common
{
    public class WindowSize
    {
        public class InnerSizeInfo
        {
            public int Height { get; set; }
            public int Width { get; set; }
        }
        public InnerSizeInfo InnerSize { get; set; }
    }

}
