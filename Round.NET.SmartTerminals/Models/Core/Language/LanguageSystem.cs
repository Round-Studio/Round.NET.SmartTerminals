using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Language
{
    public class LanguageSystem
    {
        public class LanguageConfig
        {
            public string DisplayText { get; set; }
            public string Language { get; set; }
        }
        public static List<LanguageConfig> LanguagesList = new List<LanguageConfig>();
        public static void InitializerLanguage()
        {
            AddNewLanguage(new LanguageConfig
            {
                DisplayText = "简体中文",
                Language = "zh-cn"
            });
            AddNewLanguage(new LanguageConfig
            {
                DisplayText = "俄语",
                Language = "ru"
            });
            AddNewLanguage(new LanguageConfig
            {
                DisplayText = "英语",
                Language = "en-us"
            });
        }
        public static void AddNewLanguage(LanguageConfig config)
        {
            LanguagesList.Add(config);
        }
    }
}
