using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Plugs
{
    public class PlugConfig
    {
        public class PlugRootConfig
        {
            public string Text { get; set; } = "什么都没有...";
            public string NameSpace { get; set; } = string.Empty;
        }
        public static void AddPlugText(string Text)
        {
            if (PlugCore.NewLoadPlug != string.Empty) { 
                foreach(var it in PlugCore.PlugsList)
                {
                    if(it.NameSpace == PlugCore.NewLoadPlug)
                    {
                        it.Text = Text;
                        break;
                    }
                }
            }
        }
    }
}
