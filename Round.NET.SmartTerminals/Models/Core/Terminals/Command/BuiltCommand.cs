using Newtonsoft.Json;
using Round.NET.SmartTerminals.Models.Core.Terminals.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.NET.SmartTerminals.Models.Core.Terminals.Command
{
    internal class BuiltCommand
    {
        public class BuiltCodeStatementConfig
        {
            public string Code { get; set; } = string.Empty;
            public Action<string> Action { get; set; } = new Action<string>((code) => ColorPrint.Println($"未定义操作符：{code}",ConsoleColor.Red));
            public string DescriptiveText { get; set; } = string.Empty;
        }
        public static List<BuiltCodeStatementConfig> BuiltCodeList = new List<BuiltCodeStatementConfig>();
        public static void BuiltCodeStatement(string code, Action<string> action,string DescriptiveText) {
            BuiltCodeList.Add(new BuiltCodeStatementConfig { Code = code, Action = action, DescriptiveText = DescriptiveText });
        }
        public static void InitializerBuiltCode()
        {
            BuiltCodeStatement(";help", CodeRun.Help, "关于此程序");
            BuiltCodeStatement(";about", CodeRun.About, "关于此程序");
            BuiltCodeStatement(";setting", Setting.SettingCore.SettingMenuCore, "设置");
            BuiltCodeStatement(";json", CodeRun.Json, "[Json内容] 自动美化Json");
            BuiltCodeStatement(";term", CodeRun.Terminals, "[文本] 翻译文本=>中文");
        }
        public static bool KeywordProcessing(string Code)
        {
            var Split = Code.Split(' ');
            var Key = Split[0];
            foreach (var item in BuiltCodeList) {
                if (Key == item.Code) { 
                    return true;
                }
            }
            return false;
        }
        public static void RunKeywordCode(string Code)
        {
            var Split = Code.Split(' ');
            var Key = Split[0];
            var Args = Code.Replace($"{Key} ", "");
            foreach (var item in BuiltCodeList)
            {
                if (Key == item.Code)
                {
                    item.Action(Args);
                    return;
                }
            }
        }
        public class CodeRun
        {
            public static void About(string Code)
            {
                ColorPrint.Println(@" ____                                      __      ", ConsoleColor.Blue);
                ColorPrint.Println(@"/\  _`\                                   /\ \__   ", ConsoleColor.Blue);
                ColorPrint.Println(@"\ \,\L\_\     ___ ___       __      _ __  \ \ ,_\  ", ConsoleColor.Blue);
                ColorPrint.Println(@" \/_\__ \   /' __` __`\   /'__`\   /\`'__\ \ \ \/  ", ConsoleColor.Blue);
                ColorPrint.Println(@"   /\ \L\ \ /\ \/\ \/\ \ /\ \L\.\_ \ \ \/   \ \ \_ ", ConsoleColor.Blue);
                ColorPrint.Println(@"   \ `\____\\ \_\ \_\ \_\\ \__/.\_\ \ \_\    \ \__\", ConsoleColor.Blue);
                ColorPrint.Println(@"    \/_____/ \/_/\/_/\/_/ \/__/\/_/  \/_/     \/__/", ConsoleColor.Blue);
                ColorPrint.Println(@"");

                ColorPrint.Println(@" ______                                                          ___              ", ConsoleColor.Green);
                ColorPrint.Println(@"/\__  _\                               __                       /\_ \             ", ConsoleColor.Green);
                ColorPrint.Println(@"\/_/\ \/     __    _ __    ___ ___    /\_\     ___       __     \//\ \      ____  ", ConsoleColor.Green);
                ColorPrint.Println(@"   \ \ \   /'__`\ /\`'__\/' __` __`\  \/\ \  /' _ `\   /'__`\     \ \ \    /',__\ ", ConsoleColor.Green);
                ColorPrint.Println(@"    \ \ \ /\  __/ \ \ \/ /\ \/\ \/\ \  \ \ \ /\ \/\ \ /\ \L\.\_    \_\ \_ /\__, `\", ConsoleColor.Green);
                ColorPrint.Println(@"     \ \_\\ \____\ \ \_\ \ \_\ \_\ \_\  \ \_\\ \_\ \_\\ \__/.\_\   /\____\\/\____/", ConsoleColor.Green);
                ColorPrint.Println(@"      \/_/ \/____/  \/_/  \/_/\/_/\/_/   \/_/ \/_/\/_/ \/__/\/_/   \/____/ \/___/ ", ConsoleColor.Green);

                ColorPrint.Println(@"");
                ColorPrint.Println(@"");
                ColorPrint.Println(@"Smart Terminals", ConsoleColor.Yellow);
                ColorPrint.Println(@"[版权所有 (c) Round Studio 保留所有权利]", ConsoleColor.Blue);
                ColorPrint.Println(@"");

                ColorPrint.Println($"主要信息：", ConsoleColor.Magenta);
                ColorPrint.Println($"开发人员：Minecraft一角钱 (https://space.bilibili.com/1527364468) (https://github.com/MinecraftYJQ)", ConsoleColor.DarkYellow);
                ColorPrint.Println($"开发团队：Round Studio (https://github.com/Round-Studio)", ConsoleColor.DarkYellow);
                ColorPrint.Println($"开源仓库：https://github.com/Round-Studio/Round.NET.SmartTerminals", ConsoleColor.DarkYellow);
                ColorPrint.Println($"");
                ColorPrint.Println($"鸣谢：", ConsoleColor.Magenta);
                ColorPrint.Println($"微软翻译 (翻译引擎) (https://learn.microsoft.com/zh-cn/azure/ai-services/translator/translator-text-apis?tabs=csharp)", ConsoleColor.DarkYellow);
                ColorPrint.Println($"Newtonsoft.Json (Json处理) (https://github.com/JamesNK/Newtonsoft.Json)", ConsoleColor.DarkYellow);
                ColorPrint.Println($"");
            }
            public static void Json(string Code)
            {
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented // 设置缩进和格式化
                    };

                    string formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(Code, typeof(object)), settings);
                    ColorPrint.Println(formattedJson, ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    ColorPrint.Println(ex.ToString(), ConsoleColor.Red);
                }
            }
            public static void Help(string Code)
            {
                foreach(var it in BuiltCodeList)
                {
                    ColorPrint.Print(it.Code, ConsoleColor.Green);
                    ColorPrint.Println($" {it.DescriptiveText}", ConsoleColor.Magenta);
                }
            }
            public static void Terminals(string Code) {
                var text = Translation.Translation.MsTranslationCore(Code);
                if(text == null)
                {
                    ColorPrint.Println("暂时无法翻译，请检查网络！", ConsoleColor.Red);
                }
                else
                {
                    ColorPrint.Println(text, ConsoleColor.Green);
                }
            }
        }
    }
}
