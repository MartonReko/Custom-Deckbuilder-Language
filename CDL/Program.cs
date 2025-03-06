using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.Logging;
namespace CDL;


class Program
{
    static void Main(string[] args)
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("CDL.Program", LogLevel.Debug)
                .AddFilter("CDL.CDLVisitor", LogLevel.Debug)
                .AddConsole();
        });

        var ast = ReadAST("examples/ex_long.cdl");
        CDLVisitor visitor = new(loggerFactory);
        visitor.Visit(ast);
    }

    public static IParseTree ReadAST(string fileName)
    {
        var code = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, fileName));
        var inputStream = new AntlrInputStream(code);
        var lexer = new CDLLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new CDLParser(tokenStream);
        var context = parser.program();
        return context;
    }
}
