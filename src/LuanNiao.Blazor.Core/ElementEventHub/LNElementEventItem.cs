using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{

    public partial class LNElementEventItem
    {
        private readonly IJSRuntime _jSRuntime = null;
        private readonly string _elementID = null;
        public LNElementEventItem(string elementID, IJSRuntime runtime)
        {
            _elementID = elementID;
            _jSRuntime = runtime;
            BindEvent();
        }


        private void BindEvent()
        {

        }
    }
}
