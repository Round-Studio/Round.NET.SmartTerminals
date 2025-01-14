using Round.NET.SmartTerminals;
using Round.NET.SmartTerminals.Models.Core;
using Round.NET.SmartTerminals.Models.Core.Terminals.ConsoleControls.Menu;
using System.Diagnostics;
using System.Reflection;

namespace EnvironmentPackageManager
{
    public class Main
    {
        public static void InitLib()
        {
            Round.NET.SmartTerminals.Models.Core.Plugs.PlugConfig.AddPlugText("环境包管理器");
            Round.NET.SmartTerminals.Models.Core.Terminals.Command.BuiltCommand.BuiltCodeStatement(";pack", (code) =>
            {
                while (true)
                {
                    Menu menu = new Menu();
                    menu.MenuTitle = "环境包管理器 v1.0.0";
                    menu.Menus = new List<string>
                    {
                        "查看包环境",
                        "修改包环境",
                        "添加包环境",
                        MenuItemConfig.UnderLine,
                        "修改系统环境变量",
                        "退出环境包管理器"
                    };

                    switch (menu.ShowMenu())
                    {
                        case "修改系统环境变量":
                            Process.Start("rundll32", "sysdm.cpl,EditEnvironmentVariables");
                            break;
                        case "退出环境包管理器":
                            return;
                    }
                }
            },"包管理器");
        }
    }
}
