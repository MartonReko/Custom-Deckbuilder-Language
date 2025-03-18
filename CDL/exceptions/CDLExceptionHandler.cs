using Antlr4.Runtime;
using NLog.Targets;

namespace CDL.exceptions;

public class CDLExceptionHandler : BaseErrorListener, IAntlrErrorListener<int>{
    private readonly List<CDLException> exceptions = [];
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
        exceptions.Add(new CDLException(line,charPositionInLine,msg));

    }
    public List<CDLException> getExceptions(){
        return exceptions;
    }
    public void AddException(CDLException exc){
        exceptions.Add(exc);
    }
    public void AddException(ParserRuleContext context, string message){
        CDLException exc = new CDLException(context.Start.Line, context.Start.Column, message);
        AddException(exc);
    }
    public bool isValid(){
        return exceptions.Count == 0;
    }

    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        exceptions.Add(new CDLException(line,charPositionInLine,msg));
    }
}