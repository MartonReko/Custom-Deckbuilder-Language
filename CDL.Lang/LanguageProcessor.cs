using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CDL.Lang.Exceptions;
using CDL.Lang.Parsing;
using CDL.Lang.Parsing.Symboltable;

namespace CDL.Lang;


public class LanguageProcessor
{
    private CDLExceptionHandler exceptionHandler = new();
    public ObjectsHelper? ProcessText(string file)
    {
        var ast = ReadAST(file);
        if (!exceptionHandler.IsValid())
        {
            Console.WriteLine("Could not parse grammar, exiting...");
            foreach (var item in exceptionHandler.GetExceptions())
            {
                Console.WriteLine(item);
            }
            return null;
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
            Console.WriteLine("Exceptions found :(");
            return null;
        }
        else
        {
            Console.WriteLine("No exception found :)");
            return oh;
        }
    }

    private CDLParser.ProgramContext ReadAST(string fileName)
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
