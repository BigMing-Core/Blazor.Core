using LuanNiao.Blazor.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core.Common
{


    public class WindowEvent
    {
        public EventType EventType { get; set; }
        public MouseEvent MouseEvent { get; set; }      
    }
}
