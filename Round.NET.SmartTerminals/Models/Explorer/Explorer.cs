using Round.NET.SmartTerminals.Models.Core.Terminals.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Explorer
{
    internal class Explorer
    {
        public class ItemConfig
        {
            public string Text { get; set; }
            public string Path { get; set; }
        }
        public static void ExplorerCore(string path = "")
        {
            if (path == "")
            {
                var it = GetDiskList();
                Menu(it);
            }
            else
            {
                var it = GetDirList(path);
                Menu(it);
            }
        }
        public static void Menu(List<ItemConfig> it)
        {
            int SelItem = 0;
            bool updateScreen = true;

            while (Core.Core.IsRunning)
            {
                if (updateScreen)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        if (i > it.Count()-1) break;
                        try
                        {

                            Console.CursorTop = i; // 移动光标到当前项的行
                        }
                        catch { }
                        if (i == SelItem)
                        {
                            ColorPrint.Println(it[i].Text, ConsoleColor.DarkCyan);
                        }
                        else
                        {
                            ColorPrint.Println(it[i].Text);
                        }
                    }
                    updateScreen = false;
                }

                var key = Console.ReadKey(true).Key; // true参数允许我们不按回车键即可读取按键

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (SelItem > 0)
                        {
                            SelItem--;
                            updateScreen = true;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (SelItem < it.Count - 1)
                        {
                            SelItem++;
                            updateScreen = true;
                        }
                        break;
                    case ConsoleKey.Enter:
                        it = GetDirList(it[SelItem].Path);
                        updateScreen = true;
                        SelItem = 0;
                        Console.Clear();
                        break;
                }
            }
        }
        public static List<ItemConfig> GetDiskList()
        {
            var list = new List<ItemConfig>();
            foreach (var disk in DriveInfo.GetDrives())
            {
                list.Add(new ItemConfig
                {
                    Text = disk.Name,
                    Path = disk.RootDirectory.ToString()
                });
            }
            return list;
        }
        public static List<ItemConfig> GetDirList(string Path)
        {
            try
            {
                var list = new List<ItemConfig>();
                list.Add(new ItemConfig
                {
                    Text = "/..",
                    Path = System.IO.Path.GetDirectoryName(Path)
                });
                foreach (var dir in Directory.GetDirectories(Path))
                {
                    list.Add(new ItemConfig
                    {
                        Text = dir.Split('\\')[dir.Split('\\').Count() - 1],
                        Path = dir
                    });
                }
                foreach (var file in Directory.GetFiles(Path))
                {
                    list.Add(new ItemConfig
                    {
                        Text = file.Split('\\')[file.Split('\\').Count() - 1],
                        Path = file
                    });
                }
                return list;
            }
            catch
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {Path}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // 使用Task.Run启动进程，避免阻塞主线程
                Task.Run(() =>
                {
                    using (Process process = new Process { StartInfo = startInfo })
                    {
                        process.Start();
                    }
                });
                if (Path == null)
                {
                    return GetDiskList();
                }
                else
                {
                    var ret = GetDirList(Path.Replace(System.IO.Path.GetFileName(Path), ""));
                    return ret;
                }
            }
        }
    }
}
