using Round.NET.SmartTerminals.Models.Core.Config;
using Round.NET.SmartTerminals.Models.Core.Language;
using Round.NET.SmartTerminals.Models.Core.Plugs;
using Round.NET.SmartTerminals.Models.Core.Terminals.ConsoleControls.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Setting
{
    internal class SettingCore
    {
        private static Config.ConfigCore.RootConfig Con = Config.ConfigCore.MainConfig;
        public static void SettingMenuCore(string code = null)
        {
            Menu menu = new Menu();

            menu.MenuTitle = "Smart Terminals 设置";
            menu.Menus = new List<string>
            {
                "模型设置",
                "语言设置",
                "核心设置",
                "插件设置",
                MenuItemConfig.UnderLine,
                "保存并退出"
            };
            var item = menu.ShowMenu();
            switch (item) {
                case "模型设置":
                    menu.MenuTitle = "模型设置";
                    menu.SelectIndex = 0;
                    menu.Menus = new List<string>
                    {
                        "微软翻译"
                    };
                    item = menu.ShowMenu();
                    SettingMenuCore();
                    break;
                case "语言设置":
                    menu.MenuTitle = "语言设置";
                    menu.SelectIndex = 0;
                    var langs = new List<string>();
                    foreach (var itlang in LanguageSystem.LanguagesList) { 
                        langs.Add($"{itlang.DisplayText} - {itlang.Language}");
                    }
                    menu.Menus = langs;
                    item = menu.ShowMenu();
                    var lang = item.Split(" - ")[1];
                    Con.Language = lang;
                    SettingMenuCore();
                    break;
                case "核心设置":
                    menu.MenuTitle = "核心设置";
                    menu.SelectIndex = 0;
                    menu.Menus = new List<string>
                    {
                        "Cmd.exe",
                        "PowerShell.exe"
                    };
                    item = menu.ShowMenu();
                    Con.RunEngine = item;
                    SettingMenuCore();
                    break;
                case "插件设置":
                    try
                    {
                        menu.MenuTitle = "插件设置";
                        menu.SelectIndex = 0;
                        var lists = new List<string>();
                        foreach (var itlang in PlugCore.PlugsList)
                        {
                            lists.Add($"{itlang.NameSpace} - {itlang.Text}");
                        }
                        menu.Menus = lists;
                        item = menu.ShowMenu();
                    }
                    catch { }
                    SettingMenuCore();
                    break;
                case "保存并退出":
                    Config.ConfigCore.MainConfig = Con;
                    Config.ConfigCore.SaveConfig();
                    ConfigCore.LoadConfig();
                    break;
            }
        }
    }
}
