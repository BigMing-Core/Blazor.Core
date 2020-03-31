using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    public class ClassNameHelper
    {
        private string _htmlClassInfo = string.Empty;
        private readonly List<string> _customClass = new List<string>();

        public void SetStaticClass(string data)
        {
            _htmlClassInfo = data;
        }


        public void AddCustomClass(string data)
        {
            _customClass.Add(data);
        }


        public void Rest()
        {
            _customClass.Clear();
        }


        public string Build()
        {
            return string.Concat(_htmlClassInfo, " ", string.Join(" ", _customClass));
        }

         

    }
}
