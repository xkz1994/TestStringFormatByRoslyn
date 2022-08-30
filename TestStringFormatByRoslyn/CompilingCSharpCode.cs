using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Diagnostics;
using System.Dynamic;


namespace TestStringFormatByRoslyn;

/// <summary>
/// https://gist.github.com/RickStrahl/f65727881668488b0a562df4c21ab560
/// </summary>
public class Program
{
    public enum CompilerModes
    {
        ClassicCodeProvider,
        RoslynCodeProvider,
        RoslynScripting,
        MonoEvaluator
    }

    static void Main(string[] args)
    {
        var mode = CompilerModes.ClassicCodeProvider;

        Console.WriteLine("Starting...");
        var swatch = new Stopwatch();
        swatch.Start();

        RunScript(mode);

        swatch.Stop();
        Console.WriteLine("First Elapsed ms: " + swatch.ElapsedMilliseconds);

        for (int x = 0; x < 2; x++)
        {
            swatch.Reset();
            swatch.Start();

            RunScript(mode);


            swatch.Stop();
            Console.WriteLine("Elapsed ms: " + swatch.ElapsedMilliseconds);
        }


        swatch.Stop();
        Console.WriteLine("Elapsed ms: " + swatch.ElapsedMilliseconds);
        Console.ReadKey();
    }

    public static void RunScript(CompilerModes mode)
    {
        switch (mode)
        {
            case CompilerModes.RoslynScripting:
                RoslynScripting();
                break;
        }
    }


    /// <summary>
    ///  Model parameter to pass to the Mono Code as a static
    ///  ConsoleApp2.Program.Model that's accessible in the script
    /// </summary>
    [ThreadStatic] public static dynamic Model = new ExpandoObject();


    public class ParmModel
    {
        public string Name { get; set; }
    }

    public static void RoslynScripting()
    {
        string methodCode = @"
        string helloString =  $@""Hello {Name}, from Roslyn with CSharpScripting."";
    
";
        var model = new ParmModel { Name = "Rick" };

        var opt = ScriptOptions.Default;
        opt.AddReferences(typeof(string).Assembly, typeof(ParmModel).Assembly);
        opt.AddImports("System");


        var state = CSharpScript.RunAsync(methodCode, opt, model, model.GetType()).Result;

        var result = state.Variables.FirstOrDefault(v => v.Name == "helloString")?.Value as string;

        Console.WriteLine(result);
    }
}