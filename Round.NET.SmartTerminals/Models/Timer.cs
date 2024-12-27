using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models
{
    internal class Timer
    {
        public static string GetNewTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
