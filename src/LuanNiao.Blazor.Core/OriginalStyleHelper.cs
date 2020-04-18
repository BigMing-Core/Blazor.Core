using System;
using System.Collections.Generic;
using System.Linq;
using LuanNiao.Blazor.Core.Common;

namespace LuanNiao.Blazor.Core
{
    public class OriginalStyleHelper
    {


        private string _htmlStyle = string.Empty;
        private readonly Dictionary<string, string> _customStyle = new Dictionary<string, string>();
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
            if (string.IsNullOrWhiteSpace(styleStr))
            {
                return this;
            }

            var styleItems = styleStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < styleItems.Length; i++)
            {
                var styleItem = styleItems[i].Split(":", StringSplitOptions.RemoveEmptyEntries);
                if (styleItem.Length == 2)
                {
                    this.AddCustomStyle(styleItem[0], styleItem[1]);
                }
            }

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


        public bool HasCustomStyle()
        {
            return _customStyle.Count != 0;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(_styleData))
            {
                foreach (var item in this._customStyle)
                {
                    _styleData = string.Concat(_styleData, ";", $"{item.Key}:{item.Value};");
                }
            }

            return string.IsNullOrWhiteSpace(_styleData) ? null : _styleData;
        }

    }
}
