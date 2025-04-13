using Antlr4.Runtime;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace CDL.exceptions;

public class CDLExceptionHandler : BaseErrorListener, IAntlrErrorListener<int>{
    private readonly List<CDLException> exceptions = [];
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
        exceptions.Add(new CDLException(line,charPositionInLine,msg));
    }
    public List<CDLException> GetExceptions(){
        return exceptions;
    }
    public void AddException(CDLException exc){
        exceptions.Add(exc);
    }
    public void AddException(ParserRuleContext context, string message){
        CDLException exc = new(context.Start.Line, context.Start.Column, message);
        AddException(exc);
    }
    public void AddException(string message){
        CDLException exc = new(message);
        AddException(exc);
    }
    public bool IsValid(){
        return exceptions.Count == 0;
    }

    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        exceptions.Add(new CDLException(line,charPositionInLine,msg));
    }
}