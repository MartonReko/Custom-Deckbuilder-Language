using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
namespace CDL;


class Program
{
    static void Main(string[] args)
    {
        var ast = ReadAST("examples/ex_long_errors.cdl");
        EnvManager envM = new EnvManager();
        VisGlobalVars visitorVars = new(envM);
        visitorVars.Visit(ast);
        VisBlocks visitorBlocks = new(envM);
        visitorBlocks.Visit(ast);
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
