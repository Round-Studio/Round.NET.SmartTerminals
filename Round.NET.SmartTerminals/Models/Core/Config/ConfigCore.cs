using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Config
{
    internal class ConfigCore
    {
        public class RootConfig
        {
            public string Language { get; set; } = "zh-cn";
            public string TranslationEngine { get; set; } = "MS";
            public string RunEngine { get; set; } = "Cmd.exe";
        }
        public static string ConfigFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\SmartTerminalsConfig.json";
        public static RootConfig MainConfig { get; set; } = new RootConfig();

        public static void LoadConfig()
        {
            if (File.Exists(ConfigFile))
            {
                var json = File.ReadAllText(ConfigFile);
                MainConfig = JsonConvert.DeserializeObject<RootConfig>(json);
            }
            else
            {
                SaveConfig();
            }
        }
        public static void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(MainConfig, Formatting.Indented);
            File.WriteAllText(ConfigFile, json);
        }
    }
}
