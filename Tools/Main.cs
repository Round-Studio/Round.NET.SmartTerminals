using Round.NET.SmartTerminals.Models.Core.Terminals.ConsoleControls.Menu;
using System.Diagnostics;
using System.Text;

namespace Tools
{
    public class Main
    {
        public static void InitLib()
        {
            Round.NET.SmartTerminals.Models.Core.Plugs.PlugConfig.AddPlugText("工具包");
            Round.NET.SmartTerminals.Models.Core.Terminals.Command.BuiltCommand.BuiltCodeStatement(";tools", (code) =>
            {
                
            }, "工具包");
        }
    }
}
