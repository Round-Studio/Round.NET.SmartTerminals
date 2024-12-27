using Round.NET.SmartTerminals.Models.Core.Terminals.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Terminals.Command
{
    internal class Command
    {
        public static void RunCode(string Code, string time)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + Code);
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            Console.SetCursorPosition(0, Console.CursorTop - 1);

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
                            ColorPrint.Println($"{e.Data}");
                            temp = e.Data;
                            iserr = false;
                            isruns = true;
                        }
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            ColorPrint.Print($"│ ", ConsoleColor.DarkCyan);
                            ColorPrint.Println($"{e.Data}", ConsoleColor.Red);
                            temp = e.Data;
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

                ColorPrint.Print($"·", ConsoleColor.Red);
                ColorPrint.Println($" [{time}] ", ConsoleColor.Magenta);
            }
        }
    }
}
