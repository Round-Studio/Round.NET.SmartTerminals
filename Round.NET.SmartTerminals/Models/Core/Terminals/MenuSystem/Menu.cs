using Round.NET.SmartTerminals.Models.Core.Terminals.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Terminals.MenuSystem
{
    internal class Menu
    {
        public List<string> Menus { get; set; }
        public string MenuTitle { get; set; } = "Smart Terminals 菜单系统";
        public int SelectIndex { get; set; } = 0;

        public string ShowMenu()
        {
            int width = 0;
            int height = 0; //控制台字符长宽
            Console.Clear();
            void FlushFrame()
            {
                if (width != Console.WindowWidth|| height != Console.WindowHeight) {
                    width = Console.WindowWidth;
                    height = Console.WindowHeight;
                    Console.CursorTop = 0;
                    Console.CursorLeft = 0;
                    ColorPrint.Print("┌", ConsoleColor.Magenta);
                    ColorPrint.Print(new string('─', 3), ConsoleColor.Magenta);
                    ColorPrint.Print($" {MenuTitle} ", ConsoleColor.Green);
                    ColorPrint.Print(new string('─', width - GetPlaceholderLength(MenuTitle) - 7), ConsoleColor.Magenta);
                    ColorPrint.Println("┐", ConsoleColor.Magenta);

                    for (int i = 0; i < height - 2; i++)
                    {
                        ColorPrint.Print("│", ConsoleColor.Magenta);
                        ColorPrint.Print(new string(' ', width - 2), ConsoleColor.Magenta);
                        ColorPrint.Println("│", ConsoleColor.Magenta);
                    }

                    string Text = " ↑ 向上选择 ↓ 向下选择 Enter 确定 ";
                    ColorPrint.Print("└", ConsoleColor.Magenta);
                    ColorPrint.Print(new string('─', 3), ConsoleColor.Magenta);
                    ColorPrint.Print(Text, ConsoleColor.Green);
                    ColorPrint.Print(new string('─', width - 5 - GetPlaceholderLength(Text)), ConsoleColor.Magenta);
                    ColorPrint.Print("┘", ConsoleColor.Magenta);
                }
            }

            int top = 2;
            int left = 3;

            while (true)
            {
                FlushFrame();
                Console.CursorTop = top;
                Console.CursorLeft = left;
                for (int i = 0; i < Menus.Count(); i++) {
                    if (i == SelectIndex)
                    {
                        ColorPrint.Print($" {Menus[i]}{new string(' ', width - GetPlaceholderLength(Menus[i]) - 7)}", ConsoleColor.Black,ConsoleColor.White);
                    }
                    else
                    {
                        ColorPrint.Print($" {Menus[i]}{new string(' ', width - GetPlaceholderLength(Menus[i]) - 7)}");
                    }
                    try
                    {
                        Console.CursorTop = top + i + 1;
                        Console.CursorLeft = left;
                    }
                    catch (Exception e) { 
                        ColorPrint.Println(e.ToString(),ConsoleColor.Red);
                        return string.Empty;
                    }
                }

                var Key = Console.ReadKey().Key;
                switch (Key) { 
                    case ConsoleKey.UpArrow:
                        if (SelectIndex > 0)
                        {
                            SelectIndex--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (SelectIndex < Menus.Count()-1)
                        {
                            SelectIndex++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorTop = 0;
                        Console.CursorLeft = 0;
                        Console.Clear();
                        return Menus[SelectIndex];
                }
            }
        }
        public static int GetPlaceholderLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (c >= '\u4e00' && c <= '\u9fa5') // 检查是否为中文字符
                {
                    length += 2; // 中文字符计为两个单位长度
                }
                else
                {
                    length += 1; // 其他字符计为一个单位长度
                }
            }
            return length;
        }
    }
}