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
            if (string.IsNullOrWhiteSpace(data))
            {
                return;
            }
            _htmlClassInfo = data.Trim();
        }


        public void AddCustomClass(string data)
        {
            if (string.IsNullOrWhiteSpace(data) || _customClass.Contains(data))
            {
                return;
            }
            _customClass.Add(data.Trim());
        }

        public void AddCustomClass(string data, Func<bool> when)
        {
            if (when == null || string.IsNullOrWhiteSpace(data) || _customClass.Contains(data))
            {
                return;
            }
            try
            {
                if (!when())
                {
                    return;
                }
            }
            catch (Exception)
            {
                //todo use ETW handle this
                return;
            }

            _customClass.Add(data.Trim());
        }

        public void RemoveCustomClass(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return;
            }
            _customClass.Remove(data.Trim());
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
