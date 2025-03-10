using Antlr4.Runtime.Misc;
using Microsoft.Extensions.Logging;

namespace CDL;

public class VisBlocks(ILoggerFactory loggerFactory, EnvManager em) : CDLBaseVisitor<object>
{
    bool gameVisited = false;
    bool charVisited = false;
    Dictionary<string, int> defNumbers = [];

    private readonly ILogger<VisBlocks> _logger = loggerFactory.CreateLogger<VisBlocks>();

    public override object VisitProgram([NotNull] CDLParser.ProgramContext context)
    {
        defNumbers.Add("Stage",0);
        defNumbers.Add("Node",0);
        defNumbers.Add("Enemy",0);
        defNumbers.Add("Effect",0);
        defNumbers.Add("Card",0);
        var result = base.VisitProgram(context);

        if (!gameVisited)
            _logger.LogError("Game definition missing");
        if (!charVisited)
            _logger.LogError("Character definition missing");
        if(defNumbers.Any(x => x.Value == 0))
            _logger.LogError("Minimum definitions not satisfied");
        
        foreach(var item in defNumbers){
            _logger.LogInformation("Number of {name} definitions is {value}",item.Key,item.Value);
        }

        return result;
    }
    public override object VisitGameSetup([NotNull] CDLParser.GameSetupContext context)
    {
        if (!gameVisited)
        {
            gameVisited = true;
        }
        else
        {
            _logger.LogError("Multiple game definitions");
        }
        return base.VisitGameSetup(context);
    }
    public override object VisitCharSetup([NotNull] CDLParser.CharSetupContext context)
    {
        if (!charVisited)
        {
            charVisited = true;
        }
        else
        {
            _logger.LogError("Multiple character definitions");
        }
        return base.VisitCharSetup(context);
    }
    public override object VisitStageDefinition([NotNull] CDLParser.StageDefinitionContext context)
    {
        defNumbers.TryGetValue(context.GetChild(0).GetText(), out var currentCount);
        defNumbers[context.GetChild(0).GetText()] = currentCount + 1;
        return base.VisitStageDefinition(context);
    }
    public override object VisitNodeDefinition([NotNull] CDLParser.NodeDefinitionContext context)
    {
        defNumbers.TryGetValue(context.GetChild(0).GetText(), out var currentCount);
        defNumbers[context.GetChild(0).GetText()] = currentCount + 1;
        return base.VisitNodeDefinition(context);
    }
    public override object VisitEnemyDefinition([NotNull] CDLParser.EnemyDefinitionContext context)
    {
        defNumbers.TryGetValue(context.GetChild(0).GetText(), out var currentCount);
        defNumbers[context.GetChild(0).GetText()] = currentCount + 1;
        return base.VisitEnemyDefinition(context);
    }
    public override object VisitEffectDefinition([NotNull] CDLParser.EffectDefinitionContext context)
    {
        defNumbers.TryGetValue(context.GetChild(0).GetText(), out var currentCount);
        defNumbers[context.GetChild(0).GetText()] = currentCount + 1;
        return base.VisitEffectDefinition(context);
    }
    public override object VisitCardDefinition([NotNull] CDLParser.CardDefinitionContext context)
    {
        defNumbers.TryGetValue(context.GetChild(0).GetText(), out var currentCount);
        defNumbers[context.GetChild(0).GetText()] = currentCount + 1;
        return base.VisitCardDefinition(context);
    }
}