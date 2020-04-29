using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core.Common
{
    public class KeyboardEvent
    {
        public string Key { get; set; }
        public int KeyCode { get; set; }
        public string Code { get; set; }
        public int CharCode { get; set; }
        public int Location { get; set; }
        public bool ShiftKey { get; set; }
        public bool CtrlKey { get; set; }
        public bool AltKey { get; set; }
    }
}
