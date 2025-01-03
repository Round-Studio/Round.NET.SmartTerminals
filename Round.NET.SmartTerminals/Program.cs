using Round.NET.SmartTerminals.Models.Core;
using Round.NET.SmartTerminals.Models.Core.Terminals.MenuSystem;
using Round.NET.SmartTerminals.Models.Explorer;
using Round.NET.SmartTerminals.Models.Translation;

namespace Round.NET.SmartTerminals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Core.InitializerTerminals();
            Explorer.ExplorerCore();
        }
    }
}
