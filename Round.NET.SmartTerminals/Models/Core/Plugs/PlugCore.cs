using Round.NET.SmartTerminals.Models.Core.Terminals.ConsoleControls.ProgressBar;
using Round.NET.SmartTerminals.Models.Core.Terminals.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Round.NET.SmartTerminals.Models.Core.Plugs
{
    internal class PlugCore
    {
        public static string NewLoadPlug = "";
        public static List<PlugConfig.PlugRootConfig> PlugsList { get; set; } = new List<PlugConfig.PlugRootConfig>();
        public static void LoadingPlug()
        {
            if (!Directory.Exists("Plugs"))
            {
                Directory.CreateDirectory("Plugs");
                return;
            }
            ProgressBar progressBar = new ProgressBar();
            progressBar.Max = Directory.GetFiles("Plugs").Count();
            foreach (var libfile in Directory.GetFiles("Plugs"))
            {
                progressBar.SetValue(progressBar.Value + 1);
                Console.CursorTop = 1;
                ColorPrint.Print($"正在加载第 {progressBar.Value+1} 个拓展\r");
                if (libfile.Contains(".dll"))
                {
                    try
                    {
                        string dllPath = libfile;
                        var namespaces = Path.GetFileName(dllPath).Replace(".dll", "");
                        Assembly assembly = Assembly.LoadFrom(dllPath);
                        if (assembly != null) {
                            Type mainType = assembly.GetType($"{namespaces}.Main");
                            if (mainType != null)
                            {
                                object mainInstance = Activator.CreateInstance(mainType);
                                MethodInfo mainMethod = mainType.GetMethod("InitLib");
                                if (mainMethod != null)
                                {
                                    NewLoadPlug = namespaces;
                                    PlugsList.Add(new PlugConfig.PlugRootConfig
                                    {
                                        NameSpace = namespaces
                                    });
                                    mainMethod.Invoke(mainInstance, null);
                                    NewLoadPlug = string.Empty;
                                }
                            }
                        }
                    }
                    catch (BadImageFormatException ex) { }
                    catch (FileLoadException ex) { }
                }
            }
            NewLoadPlug = string.Empty;
            progressBar.Clear();
            Console.Clear();
        }
    }
}
