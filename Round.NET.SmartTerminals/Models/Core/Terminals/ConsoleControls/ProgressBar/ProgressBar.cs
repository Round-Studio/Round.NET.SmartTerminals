using Round.NET.SmartTerminals.Models.Core.Terminals.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Terminals.ConsoleControls.ProgressBar
{
    internal class ProgressBar
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = 100;
        public int Value { get; set; } = 0;
        private int top = 0;
        private int left = 0;

        public void Show(int value = 0)
        {
            try
            {
                top = Console.CursorTop;
                left = Console.CursorLeft;
                Console.CursorTop = 0;
                Console.CursorLeft = 0;


                // 计算进度条的长度
                int jd = (int)(Console.WindowWidth * (value / (double)Max));
                string percentage = ((value / (double)Max) * 100).ToString("F2").PadLeft(6, '0');
                // 打印进度条
                ColorPrint.Println($"[{new string('#', jd - 2)}{new string(' ', Console.WindowWidth - jd - 7)}]{percentage}%", ConsoleColor.White, ConsoleColor.Blue);

                // 恢复光标位置
                Console.CursorTop = top;
                Console.CursorLeft = left;
            }
            catch { }
        }
        public void SetValue(int value) {
            if (value > Max) return;
            if (value < Min) return;

            Value = value;
            Show(value);
        }
        public void Clear()
        {
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            ColorPrint.Println(new string(' ',Console.WindowWidth));
            Console.CursorTop = top;
            Console.CursorLeft = left;
        }
    }
}
