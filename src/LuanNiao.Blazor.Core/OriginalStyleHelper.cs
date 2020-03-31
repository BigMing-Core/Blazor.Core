using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    public class OriginalStyleHelper
    {
        private string _htmlStyle = string.Empty;
        private readonly List<string> _customStyle = new List<string>();

        public void SetHtmlStyleData(string data)
        {
            _htmlStyle = data;
        }


        public void AddCustomStyle(string data)
        {
            _customStyle.Add(data);
        }

        public void Rest()
        {
            _customStyle.Clear();
        }


        public string Build()
        {
            return string.Concat(_htmlStyle, string.Join(";", _customStyle));
        }

    }
}
