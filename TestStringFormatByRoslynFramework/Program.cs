using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace TestStringFormatByRoslynFramework
{
    public class Program
    {
        public static async Task Main()
        {
            const string str = "$\"{ParamDictionary[\"test\"].Substring(2)}\"";

            var test = await CSharpScript.EvaluateAsync<string>(str, globals: new ParamModel
            {
                ParamDictionary = new Dictionary<string, string>
                {
                    { "test", "测试字符串" }
                }
            });
            Console.WriteLine(test);
            Console.ReadKey();
        }
    }
}