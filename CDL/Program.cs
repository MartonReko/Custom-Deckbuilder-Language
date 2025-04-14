using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CDL.exceptions;
using CDL.parsing;
namespace CDL;


class Program
{
    public static CDLExceptionHandler exceptionHandler = new();
    static void Main(string[] args)
    {
        var ast = ReadAST("examples/example.cdl");
        if (!exceptionHandler.IsValid())
        {
            System.Console.WriteLine("Could not parse grammar, exiting...");
            foreach (var item in exceptionHandler.GetExceptions())
            {
                System.Console.WriteLine(item);
            }
            return;
        }
        EnvManager envM = new();
        ObjectsHelper oh = new(envM, exceptionHandler);
        VisGlobalVars visitorVars = new(envM, exceptionHandler, oh);
        visitorVars.Visit(ast);
        VisBlocks visitorBlocks = new(envM, exceptionHandler, oh);
        visitorBlocks.Visit(ast);
        oh.CheckObjectsValidity();
        if (!exceptionHandler.IsValid())
        {
            System.Console.WriteLine("Exceptions found :(");
        }else{
            System.Console.WriteLine("No exception found :)");
        }
    }

    public static IParseTree ReadAST(string fileName)
    {
        var code = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, fileName));
        var inputStream = new AntlrInputStream(code);
        var lexer = new CDLLexer(inputStream);
        lexer.RemoveErrorListeners();
        lexer.AddErrorListener(exceptionHandler);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new CDLParser(tokenStream);
        parser.RemoveErrorListeners();
        parser.AddErrorListener(exceptionHandler);
        var context = parser.program();
        return context;
    }
}
