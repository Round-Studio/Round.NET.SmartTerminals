using Round.NET.SmartTerminals.Models.Core.Terminals.Command;
using Round.NET.SmartTerminals.Models.Core.Terminals.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core
{
    internal class Core
    {
        public static bool IsRunning { get; set; } = true; //整个程序运行状态
        public static void InitializerTerminals()   //初始化终端
        {
            Translation.Translation.InitTranslationCore();
            BuiltCommand.InitializerBuiltCode();
            ColorPrint.Println("Smart Terminals", ConsoleColor.Yellow);
            ColorPrint.Println("[版权所有 (c) Round Studio 保留所有权利]\n", ConsoleColor.Red);
            ColorPrint.Println("开源仓库 https://github.com/Round-Studio/Round.NET.SmartTerminals.", ConsoleColor.Green);
            ColorPrint.Print("键入", ConsoleColor.Yellow);
            ColorPrint.Print(";help", ConsoleColor.Magenta);
            ColorPrint.Println("以查看内置操作符使用", ConsoleColor.Yellow);
#if DEBUG
            ColorPrint.Println("[DEBUG模式]", ConsoleColor.Red);
#endif
            ColorPrint.Print("\n");
            ConsoleMain();
        }
        public static void ConsoleMain()            //终端主程序
        {
            while (IsRunning)
            {
                var thistime = Timer.GetNewTime();
                ColorPrint.Print  ($"┌",ConsoleColor.DarkCyan);
#if DEBUG
                ColorPrint.Print($" [{thistime}] ", ConsoleColor.Magenta);
                ColorPrint.Println("[DEBUG] ", ConsoleColor.Red);
#else
                ColorPrint.Println($" [{thistime}] ",ConsoleColor.Magenta);
#endif
                ColorPrint.Print  ($"└", ConsoleColor.DarkCyan);
                ColorPrint.Print  ($" $ ", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Yellow;
                var commond = Console.ReadLine();

                Command.RunCode(commond, thistime); //运行命令
            }
        }
    }
}
