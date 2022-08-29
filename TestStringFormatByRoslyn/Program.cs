// See https://aka.ms/new-console-template for more information

using Microsoft.CodeAnalysis.CSharp.Scripting;
using TestStringFormatByRoslyn;


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