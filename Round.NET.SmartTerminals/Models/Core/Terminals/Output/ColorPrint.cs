using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Terminals.Output
{
    internal class ColorPrint
    {
        public static void Println(object Message, ConsoleColor FontColor = ConsoleColor.White, ConsoleColor BackgroundColor = ConsoleColor.Black)
        {
            System.Console.ForegroundColor = FontColor;
            System.Console.BackgroundColor = BackgroundColor;
            System.Console.WriteLine(Message);
            System.Console.ResetColor();
        }
        public static void Print(object Message, ConsoleColor FontColor = ConsoleColor.White, ConsoleColor BackgroundColor = ConsoleColor.Black)
        {
            System.Console.ForegroundColor = FontColor;
            System.Console.BackgroundColor = BackgroundColor;
            System.Console.Write(Message);
            System.Console.ResetColor();
        }
    }
}
