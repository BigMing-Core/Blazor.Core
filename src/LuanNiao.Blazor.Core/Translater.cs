using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace LuanNiao.Blazor.Core
{
    public static class Translater
    {
        public class SourceItem
        {
            public string CultureName { get; set; }
            public string Data { get; set; }
            public SourceItemType ItemType { get; set; }
        }

        public enum SourceItemType
        {
            LocalFile = 0,
            UrlAddress = 1,
            OrignalString = 2
        }



        private readonly static Dictionary<string, Dictionary<string, string>> _languageSource = new Dictionary<string, Dictionary<string, string>>();
        private static string _currentCulture = null;


        public static event Action<string> CultureChanged;
        public static void AddLanguageFile(SourceItem[] sources)
        {
            foreach (var item in sources)
            {
                switch (item.ItemType)
                {
                    case SourceItemType.LocalFile:
                        break;
                    case SourceItemType.UrlAddress:
                        LoadResourceFile(item.CultureName, item.Data);
                        break;
                    case SourceItemType.OrignalString:
                        AddToCultureData(item.CultureName, item.Data);
                        break;
                    default:
                        break;
                }

            }
        }

        public static void ConvertTo(string culture)
        {
            _currentCulture = culture;
            CultureChanged?.Invoke(_currentCulture);
        }

        public static string Tr(string key)
        {
            if (_currentCulture is null || !_languageSource.ContainsKey(_currentCulture) || !_languageSource[_currentCulture].ContainsKey(key))
            {
                return key;
            }
            return _languageSource[_currentCulture][key];
        }


        private static void LoadResourceFile(string culture, string fileUrl)
        {
            string resultstring = "";
            Encoding encoding = Encoding.UTF8;
            var request = WebRequest.Create(fileUrl);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                resultstring = reader.ReadToEnd();
            }
            AddToCultureData(culture, resultstring);
        }
        private static void AddToCultureData(string culture, string jsonData)
        {
            if (!_languageSource.ContainsKey(culture))
            {
                try
                {
                    _languageSource.Add(culture, System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData));
                }
                catch
                {

                }
            }
        }
    }
}
