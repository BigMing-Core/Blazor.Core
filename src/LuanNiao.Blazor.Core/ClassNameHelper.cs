using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    public class ClassNameHelper
    {
        private string _htmlClassInfo = string.Empty;
        private readonly List<string> _customClass = new List<string>();

        public ClassNameHelper SetStaticClass(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return this;
            }
            _htmlClassInfo = data.Trim();
            return this;
        }


        public ClassNameHelper AddCustomClass(string data)
        {
            if (string.IsNullOrWhiteSpace(data) || _customClass.Contains(data))
            {
                return this;
            }
            _customClass.Add(data.Trim());
            return this;
        }

        public ClassNameHelper AddCustomClass(string data, Func<bool> when)
        {
            if (when == null || string.IsNullOrWhiteSpace(data) || _customClass.Contains(data))
            {
                return this;
            }
            try
            {
                if (!when())
                {
                    return this;
                }
            }
            catch (Exception)
            {
                //todo use ETW handle this
                return this;
            }

            _customClass.Add(data.Trim());
            return this;
        }

        public ClassNameHelper RemoveCustomClass(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return this;
            }
            _customClass.Remove(data.Trim());
            return this;
        }


        public ClassNameHelper Rest()
        {
            _customClass.Clear(); 
            return this;
        }


        public string Build()
        {
            return string.Concat(_htmlClassInfo, " ", string.Join(" ", _customClass));
        }



    }
}
