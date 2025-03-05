using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace CDL;


class Program
{
    static void Main(string[] args)
    {
        Test();
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
    public static void Test()
    {
        var ast = ReadAST("examples/ex_long.cdl");
        CDLVisitor visitor = new CDLVisitor();
        Console.WriteLine(visitor.Visit(ast));
    }
}
