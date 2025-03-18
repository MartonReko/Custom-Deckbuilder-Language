using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CDL;

public class VisBlocks(EnvManager em) : CDLBaseVisitor<object>
{
    private List<Card> Cards { get; set; } = [];
    bool gameVisited = false;
    bool charVisited = false;
    Dictionary<string, int> defNumbers = [];
    List<Symbol> currentList = new();
    private readonly ILogger<VisBlocks> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)).CreateLogger<VisBlocks>();

    private bool CheckExist(ParserRuleContext[] context, int count)
    {
        if (context.Length == 0)
        {
            _logger.LogError("Missing {x}", context.GetType());
            return false;
        }
        if (context.Length != count)
        {
            _logger.LogError("Missing or multiple {x}", context[0].GetChild(0).GetText());
            return false;
        }
        return true;
    }
    private bool CheckExist(Antlr4.Runtime.Tree.ITerminalNode[] context, int count)
    {
        if (context.Length != count)
        {
            _logger.LogError("Missing or multiple {x}", context[0].GetText());
            return false;
        }
        return true;
    }

    public override object VisitProgram([NotNull] CDLParser.ProgramContext context)
    {
        defNumbers.Add("Stage", 0);
        defNumbers.Add("Node", 0);
        defNumbers.Add("Enemy", 0);
        defNumbers.Add("Effect", 0);
        defNumbers.Add("Card", 0);
        var result = base.VisitProgram(context);

        if (!gameVisited)
            _logger.LogError("Game definition missing");
        if (!charVisited)
            _logger.LogError("Character definition missing");
        if (defNumbers.Any(x => x.Value == 0))
            _logger.LogError("Minimum definitions not satisfied");

        foreach (var item in defNumbers)
        {
            _logger.LogInformation("Number of {name} definitions is {value}", item.Key, item.Value);
        }
        foreach (var card in Cards)
        {
            string targets = "";
            string effects = "";
            foreach (TargetTypes target in card.ValidTargets)
                targets += target.ToString() + " ";
            foreach (Effect applied in card.EffectsApplied)
                effects += applied.Name;

            _logger.LogDebug("Card \"{c}\" properties:\n\tRarity: {r}\n\tValidTargets: {t}\n\tApplies: {a}", card.Name, card.Rarity, targets, effects);
        }

        return result;
    }
    public override object VisitStageProperties([NotNull] CDLParser.StagePropertiesContext context)
    {
        CheckExist(context.lengthDef(), 1);
        return base.VisitStageProperties(context);
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
    public override object VisitGameProperties([NotNull] CDLParser.GamePropertiesContext context)
    {
        CheckExist(context.gamePropName(), 1);
        CheckExist(context.STAGES(), 1);
        CheckExist(context.gamePropPlayerselect(), 1);

        em.Env = new Env(em.Env);
        var result = base.VisitGameProperties(context);

        // Play with currentlist
        if (currentList.Any(x => !x.GetType().Equals(em.Ts.STAGE)))
        { _logger.LogError("Invalid type in stages list"); }

        if (em.Env.PrevEnv != null)
            em.Env = em.Env.PrevEnv;
        currentList.Clear();
        return result;
    }
    public override object VisitListItem([NotNull] CDLParser.ListItemContext context)
    {
        if (context.varRef() != null)
        {
            var listSymbol = em.getVariableFromScope(context, context.varRef().varName().GetText());
            if (listSymbol != null)
            {
                currentList.Add(listSymbol);
            }
        }
        return base.VisitListItem(context);
    }
    public override object VisitGamePropName([NotNull] CDLParser.GamePropNameContext context)
    {
        if (!em.isVariableOnScope(context.varName().GetText()))
        {
            _logger.LogInformation("Valid Game Name \"{n}\"", context.varName().GetText());
        }
        else
        {
            _logger.LogError("Invalid Game Name \"{n}\", already taken", context.varName().GetText());
        }
        return base.VisitGamePropName(context);
    }
    public override object VisitGamePropPlayerselect([NotNull] CDLParser.GamePropPlayerselectContext context)
    {
        em.getVariableFromScope(context, context.varName().GetText());
        return base.VisitGamePropPlayerselect(context);
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
        Card newCard = new()
        {
            Name = context.GetChild(1).GetText()
        };
        Cards.Add(newCard);
        defNumbers.TryGetValue(context.GetChild(0).GetText(), out var currentCount);
        defNumbers[context.GetChild(0).GetText()] = currentCount + 1;
        return base.VisitCardDefinition(context);
    }
    public override object VisitCardProperties([NotNull] CDLParser.CardPropertiesContext context)
    {
        //Card curCard = Cards.Last();
        //curCard.Rarity = context.rarityName().First().GetText();

        return base.VisitCardProperties(context);
    }
    public override object VisitRarityName([NotNull] CDLParser.RarityNameContext context)
    {
        Card lastCard = Cards.Last();
        if (lastCard.Rarity == null)
        {
            lastCard.Rarity = context.GetText();
        }
        else
        {
            _logger.LogError("Multiple rarity definitions for card {c} {d}", lastCard.Name, lastCard.Rarity);
        }
        return base.VisitRarityName(context);
    }
    public override object VisitTargetItem([NotNull] CDLParser.TargetItemContext context)
    {
        Card lastCard = Cards.Last();

        string targetString = context.GetText();
        switch (targetString)
        {
            case "enemy":
                lastCard.ValidTargets.Add(TargetTypes.ENEMY);
                break;
            case "enemies":
                lastCard.ValidTargets.Add(TargetTypes.ENEMIES);
                break;
            case "player":
                lastCard.ValidTargets.Add(TargetTypes.PLAYER);
                break;
            default:
                _logger.LogError("Unable to parse target {t}", targetString);
                break;
        }
        // if (Enum.TryParse(targetString, out TargetTypes target))
        // {
        //     _logger.LogDebug("Parsed target {t}", target);
        //     lastCard.ValidTargets.Add(target);
        // }
        // else
        // {
        //     _logger.LogDebug("Failed to parse target {t}", target);
        // }
        return base.VisitTargetItem(context);
    }
    public override object VisitCardEffectsList([NotNull] CDLParser.CardEffectsListContext context)
    {
        // foreach(var item in context.list().listItem()){
        //     string name = item.varRef().@varName().GetText();    
        //     int num = int.Parse( item.varRef().@params().literalExpression().GetText());
        //     _logger.LogCritical("{a} {num}", name, num);
        // }
        return base.VisitCardEffectsList(context);
    }
}