using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Tools
{
    internal class Tools
    {
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
