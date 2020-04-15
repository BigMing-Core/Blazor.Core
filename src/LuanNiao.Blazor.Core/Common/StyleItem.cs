using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core.Common
{

    public class StyleItem
    {
        public string StyleName { get; set; }
        public string Value { get; set; }

        public bool IsNullOrWhiteSpace()
        {
            return string.IsNullOrWhiteSpace(StyleName) || string.IsNullOrWhiteSpace(Value);
        }
    }
}
