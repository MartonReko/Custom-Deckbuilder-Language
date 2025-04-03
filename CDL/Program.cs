using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CDL.exceptions;
namespace CDL;


class Program
{
    public static CDLExceptionHandler exceptionHandler = new();
    static void Main(string[] args)
    {
        var ast = ReadAST("examples/ex_long_errors.cdl");
        if (!exceptionHandler.isValid())
        {
            System.Console.WriteLine("Could not parse grammar, exiting...");
            foreach (var item in exceptionHandler.getExceptions())
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
        oh.isValid();
        if (!exceptionHandler.isValid())
        {
            System.Console.WriteLine("Exceptions found :(");
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
