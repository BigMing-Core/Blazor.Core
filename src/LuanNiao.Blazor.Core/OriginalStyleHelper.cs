using System;
using System.Collections.Generic;
using LuanNiao.Blazor.Core.Common;

namespace LuanNiao.Blazor.Core
{
    public class OriginalStyleHelper
    {


        private string _htmlStyle = string.Empty;
        private readonly Dictionary<string, string> _customStyle = new Dictionary<string, string>();
        private readonly List<string> _customStyleStr = new List<string>();
        private string _styleData = null;

        public OriginalStyleHelper SetStaticStyleData(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return this;
            }
            _htmlStyle = data;
            _styleData = null;
            return this;
        }

        public OriginalStyleHelper AddCustomStyleStr(string styleStr)
        {
            if (string.IsNullOrWhiteSpace(styleStr) || _customStyleStr.Contains(styleStr))
            {
                return this;
            }

            _customStyleStr.Add(styleStr);
            _styleData = null;
            return this;
        }

        public OriginalStyleHelper AddCustomStyle(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
            {
                return this;
            }
            if (_customStyle.ContainsKey(name))
            {
                _customStyle[name] = value;
            }
            else
            {
                _customStyle.Add(name, value);
            }
            _styleData = null;
            return this;
        }


        public OriginalStyleHelper AddDiffCustomStyle(StyleItem whenSuccess, StyleItem whenFailed, Func<bool> condition)
        {
            if (whenSuccess == null || whenFailed == null || whenSuccess.IsNullOrWhiteSpace() || whenFailed.IsNullOrWhiteSpace() || condition == null)
            {
                return this;
            }
            if (condition())
            {
                AddCustomStyle(whenSuccess);
            }
            else
            {
                AddCustomStyle(whenFailed);
            }

            return this;
        }


        public OriginalStyleHelper AddDiffCustomStyle(StyleItem[] whenSuccess, StyleItem[] whenFailed, Func<bool> condition)
        {
            if (whenSuccess == null || whenFailed == null || condition == null)
            {
                return this;
            }
            if (condition())
            {
                for (int i = 0; i < whenSuccess.Length; i++)
                {
                    AddCustomStyle(whenSuccess[i]);
                }
                
            }
            else
            {
                for (int i = 0; i < whenFailed.Length; i++)
                {
                    AddCustomStyle(whenFailed[i]);
                }
            }

            return this;
        }


        public OriginalStyleHelper AddOrUpdateDiffCustomStyle(StyleItem whenSuccess, StyleItem whenFailed, Func<bool> condition)
        {
            if (whenSuccess == null || whenFailed == null || whenSuccess.IsNullOrWhiteSpace() || whenFailed.IsNullOrWhiteSpace() || condition == null)
            {
                return this;
            }
            if (condition())
            {
                AddOrUpdateCustomStyle(whenSuccess);
            }
            else
            {
                AddOrUpdateCustomStyle(whenFailed);
            }

            return this;
        }


        public OriginalStyleHelper AddOrUpdateCustomStyle(StyleItem item)
        {
            if (item == null || item.IsNullOrWhiteSpace())
            {
                return this;
            }
            if (_customStyle.ContainsKey(item.StyleName))
            {
                _customStyle[item.StyleName] = item.Value;
            }
            else
            {
                AddCustomStyle(item);
            }
            return this;
        }



        public OriginalStyleHelper AddCustomStyle(StyleItem item)
        {
            if (item == null || item.IsNullOrWhiteSpace())
            {
                return this;
            }

            return this.AddCustomStyle(item.StyleName, item.Value);
        }

        public OriginalStyleHelper RemoveCustomStyle(StyleItem item)
        {
            if (item == null || item.IsNullOrWhiteSpace())
            {
                return this;
            }

            return this.RemoveCustomStyle(item.StyleName);
        }


        public OriginalStyleHelper AddCustomStyle(string name, string value, Func<bool> when)
        {
            if (when == null || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
            {
                return this;
            }
            if (!when())
            {
                return this;
            }
            return this.AddCustomStyle(name, value);
        }

        public OriginalStyleHelper RemoveCustomStyle(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return this;
            }
            _customStyle.Remove(name);
            _styleData = null;
            return this;
        }

        public OriginalStyleHelper Rest()
        {
            _customStyle.Clear();
            _styleData = null;
            return this;
        }


        public string Build()
        {
            if (string.IsNullOrWhiteSpace(_styleData))
            {
                _styleData = string.Concat(_htmlStyle, string.Join(";", _customStyleStr));
                foreach (var item in this._customStyle)
                {
                    _styleData = string.Concat(_styleData,";", $"{item.Key}:{item.Value};");
                }
            }

            return string.IsNullOrWhiteSpace(_styleData) ? null : _styleData;
        }

    }
}
