using Antlr4.Runtime.Misc;

namespace CDL;

public class CDLVisitor : CDLBaseVisitor<object>
{
    public override object VisitCharSetup([NotNull] CDLParser.CharSetupContext context)
    {
        Console.WriteLine("Character setup!");
        return base.VisitCharSetup(context);
    }
}