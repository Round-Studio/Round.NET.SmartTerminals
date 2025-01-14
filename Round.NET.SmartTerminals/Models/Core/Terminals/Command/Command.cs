using Round.NET.SmartTerminals.Models.Core.Config;
using Round.NET.SmartTerminals.Models.Core.Terminals.ConsoleControls.Menu;
using Round.NET.SmartTerminals.Models.Core.Terminals.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Terminals.Command
{
    internal class Command
    {
        public static void ClearLine(string Code)
        {
            var top = Console.CursorTop;
            var left = Console.CursorLeft;

            string line = new string(' ', Tools.Tools.GetPlaceholderLength(Code)+4);
            Console.Write(line);
            Console.CursorTop = top;
            Console.CursorLeft = left;
        }
        public static void RunCode(string Code, string time)
        {
            if (Code != "")
            {
                if (BuiltCommand.KeywordProcessing(Code)) { BuiltCommand.RunKeywordCode(Code); }
                else
                {
                    ProcessStartInfo psi = new ProcessStartInfo(ConfigCore.MainConfig.RunEngine, "/c " + Code);
                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;
                    psi.RedirectStandardInput = true;
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    ColorPrint.Print($"┌", ConsoleColor.DarkCyan);
#if DEBUG
                    ColorPrint.Print($" [{time}] ", ConsoleColor.Magenta);
                    ColorPrint.Print("[DEBUG] ", ConsoleColor.Red);
#else
                ColorPrint.Println($" [{time}] ",ConsoleColor.Magenta);
#endif
                    ColorPrint.Println(Code, ConsoleColor.Green);

                    ClearLine(Code);

                    var temp = "";
                    var iserr = false;
                    var isruns = false;
                    using (Process process = Process.Start(psi))
                    {
                        if (process != null)
                        {
                            process.OutputDataReceived += (sender, e) =>
                            {
                                if (!String.IsNullOrEmpty(e.Data))
                                {
                                    ColorPrint.Print($"│ ", ConsoleColor.DarkCyan);
                                    var res = Translation.Translation.MsTranslationCore(e.Data);
                                    if (res != null)
                                    {
                                        ColorPrint.Println($"{res}");
                                        temp = res;
                                    }
                                    else
                                    {
                                        ColorPrint.Println($"{e.Data}");
                                        temp = e.Data;
                                    }
                                    iserr = false;
                                    isruns = true;
                                }
                            };

                            process.ErrorDataReceived += (sender, e) =>
                            {
                                if (!String.IsNullOrEmpty(e.Data))
                                {
                                    ColorPrint.Print($"│ ", ConsoleColor.DarkCyan);
                                    var res = Translation.Translation.MsTranslationCore(e.Data);
                                    if (res != null)
                                    {
                                        ColorPrint.Println($"{res}", ConsoleColor.Red);
                                        temp = res;
                                    }
                                    else
                                    {
                                        ColorPrint.Println($"{e.Data}", ConsoleColor.Red);
                                        temp = e.Data;
                                    }
                                    iserr = true;
                                    isruns = true;
                                }
                            };

                            process.BeginOutputReadLine();
                            process.BeginErrorReadLine();

                            process.WaitForExit();
                        }
                    }

                    if (isruns)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ColorPrint.Print($"└", ConsoleColor.DarkCyan);
                        if (!iserr)
                        {
                            ColorPrint.Println($" {temp}");
                        }
                        else
                        {
                            ColorPrint.Println($" {temp}", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);

                        ColorPrint.Print($"×", ConsoleColor.Red);
                        ColorPrint.Println($" [{time}] ", ConsoleColor.Magenta);
                    }
                }
            }
            else
            {
                Console.SetCursorPosition(0, Console.CursorTop - 2);

                ColorPrint.Print($"×", ConsoleColor.Red);
                ColorPrint.Println($" [{time}] ", ConsoleColor.Magenta);
            }
        }
    }
}
