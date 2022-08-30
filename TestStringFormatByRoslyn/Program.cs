// See https://aka.ms/new-console-template for more information

using DynamicExpresso;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Net.Utilities.Object.Collection;
using Net.Utilities.Object.String;
using Net.Utilities.Struct.Byte;
using Net.Utilities.Struct.DateTime;
using Net.Utilities.Struct.Enum;
using Net.Utilities.Struct.IntPtr;
using Net.Utilities.Struct.Numeric;
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
var interpreter = new Interpreter()
    .Reference(typeof(Array))
    .Reference(typeof(Enumerable))
    .Reference(typeof(ByteCastHelper))
    .Reference(typeof(CollectionCastHelp))
    .Reference(typeof(DateTimeCastHelper))
    .Reference(typeof(NumberCastHelper))
    .Reference(typeof(EnumCastHelper))
    .Reference(typeof(IntPtrCastHelper))
    .Reference(typeof(StringCastHelper));
object o = 1;
var result = interpreter.Eval("row.ToInt32OrDefault()+1", new Parameter("row", o));
Console.WriteLine(result);
result = interpreter.Eval("row+1", new Parameter("row", o));
Console.WriteLine(result);
o = "202208301112";
result = interpreter.Eval("DateTimeCastHelper.String2DateTime(row,\"yyyyMMddHHmm\")", new Parameter("row", o));
Console.WriteLine(result);

result = interpreter.Eval("row+1", new Parameter("row", o));
Console.WriteLine(result);

o = 4; //0100
result = interpreter.Eval("(row >> 2 & 1) == 1", new Parameter("row", o));
Console.WriteLine(result);

Console.ReadKey();