using LuanNiao.Blazor.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core.Common
{
    public class MouseEvent
    {
        public bool Alt { get; set; }
        public MouseEventButton Button { get; set; }
        public MouseEventButtons Buttons { get; set; }
        public long ClientX { get; set; }
        public long ClientY { get; set; }
        public bool Control { get; set; }
        public bool Meta { get; set; }
        public bool Shift { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
    }
}
