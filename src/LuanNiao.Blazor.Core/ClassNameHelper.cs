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
        public ClassNameHelper SetStaticClass(string data,Func<bool> when)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return this;
            }
            if (!when())
            {
                return this;
            }
            return SetStaticClass(data);
        }


        public ClassNameHelper AddCustomClass(string data)
        {
            if (string.IsNullOrWhiteSpace(data) || _customClass.Contains(data))
            {
                return this;
            }
            lock (_customClass)
            {
                _customClass.Add(data.Trim());
            }
            return this;
        }
        public ClassNameHelper TakeInverse(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return this;
            }
            if (Contains(data))
            {
                this.RemoveCustomClass(data);
            }
            else
            {
                this.AddCustomClass(data);
            }
            return this;
        }

        public bool Contains(string data)
        {
            return this._customClass.Contains(data) || this._htmlClassInfo == data;
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

            AddCustomClass(data);
            return this;
        }
        public ClassNameHelper RemoveCustomClass(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return this;
            }
            lock (_customClass)
            {
                _customClass.Remove(data.Trim());
            }
            return this;
        }
        public ClassNameHelper RemoveCustomClass(string data, Func<bool> when)
        {
            if (when == null || string.IsNullOrWhiteSpace(data))
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
            RemoveCustomClass(data);
            return this;
        }


        public ClassNameHelper Rest()
        {
            lock (_customClass)
            {
                _customClass.Clear();
            }
            return this;
        }


        public string Build()
        {
            string res = "";
            lock (_customClass)
            {
                res = string.Concat(_htmlClassInfo, " ", string.Join(" ", _customClass));
            }
            return res;
        }



    }
}
