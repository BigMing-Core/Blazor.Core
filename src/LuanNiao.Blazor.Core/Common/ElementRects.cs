using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core.Common
{
    public struct ElementRects
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Top { get; set; }
        public double Bottom { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double OffsetTop { get; set; }
        public double OffsetLeft { get; set; }
        public double OffsetHeight { get; set; }
        public double OffsetWidth { get; set; }
    }
}
