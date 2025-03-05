using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.Logging;

namespace CDL;

public class CDLVisitor(ILoggerFactory loggerFactory) : CDLBaseVisitor<object>
{
    private readonly ILogger<CDLVisitor> _logger = loggerFactory.CreateLogger<CDLVisitor>();

    public override object Visit(IParseTree tree)
    {
        _logger.LogDebug("Visitor start");
        return base.Visit(tree);
    }
    public override object VisitCharSetup([NotNull] CDLParser.CharSetupContext context)
    {
        var charName = context.varName().GetText();
        var health = context.charProperties().number(0).GetText();
        _logger.LogDebug("Context: {c}\nName: {n}\nHealth: {h}",context.GetType(),charName,health);
        return base.VisitCharSetup(context);
    }
}