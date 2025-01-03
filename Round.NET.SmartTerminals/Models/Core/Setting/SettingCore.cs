using Round.NET.SmartTerminals.Models.Core.Terminals.MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Setting
{
    internal class SettingCore
    {
        public static void SettingMenuCore(string code)
        {
            Menu menu = new Menu();
            menu.MenuTitle = "Smart Terminals 设置";
            menu.Menus = new List<string>
            {
                "模型设置",
                "语言设置",
                "核心设置"
            };
            menu.ShowMenu();
        }
    }
}
