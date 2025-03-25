using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using CDL.exceptions;
using CDL.game;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace CDL;

public class VisBlocks(EnvManager em, CDLExceptionHandler exceptionHandler) : CDLBaseVisitor<object>
{
    // All game objects are stored in these lists
    private GameSetup Game { get; set; } = new();
    private GameCharacter Character { get; set; } = new();
    private List<Stage> Stages { get; set; } = [];
    private List<Node> Nodes { get; set; } = [];
    private List<Enemy> Enemies { get; set; } = [];
    private List<Effect> Effects { get; set; } = [];
    private List<Card> Cards { get; set; } = [];

    private CDLExceptionHandler ExceptionHandler { get; set; } = exceptionHandler;

    private readonly ILogger<VisBlocks> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)).CreateLogger<VisBlocks>();

    // 
    private List<object> LocalListContent { get; set; } = [];
    private readonly struct ExpressionHelper(CDLType type, object value)
    {
        readonly CDLType type = type;
        readonly object value = value;
        public override string ToString()
        {
            return $"ExpressionHelper type: {type.Name}\tvalue: {value}";
        }
    }
    private void LogCards()
    {
        foreach (var card in Cards)
        {
            string targets = "";
            string effects = "";
            foreach (TargetTypes target in card.ValidTargets)
                targets += target.ToString() + " ";
            foreach ((Effect effect, int cnt) in card.EffectsApplied)
                effects += $"{cnt}x\t{effect.Name}";

            _logger.LogDebug("Card \"{c}\" properties:\n\tRarity: {r}\n\tValidTargets: {t}\n\tApplies: {a}", card.Name, card.Rarity, targets, effects);
        }
    }
    private void LogEffects()
    {
        foreach (var item in Effects)
        {
            _logger.LogDebug("Effect \"{c}\" properties:\n\tInDmgMod: {t}\n\tOutDmgMod: {a}", item.Name, item.InDmgMod, item.OutDmgMod);
        }
    }
    private void LogNodes()
    {
        foreach (var item in Nodes)
        {
            _logger.LogDebug("Node \"{c}\"", item.Name);
        }
    }
    private void LogStages()
    {
        foreach (var item in Nodes)
        {
            _logger.LogDebug("Stage \"{c}\"", item.Name);
        }
    }
    private void LogEnemies()
    {
        foreach (var item in Enemies)
        {
            _logger.LogDebug("Enemy \"{c}\"", item.Name);
        }
    }
    public override object VisitParamsDef([NotNull] CDLParser.ParamsDefContext context)
    {
        foreach (var item in context.typeNameVarName())
        {
            var type = em.Ts[item.typeName().GetText()];
            var symbol = new Symbol(item.varName().GetText(), type);
            if (type == em.Ts.ERROR)
            {

                (int, int) pos = EnvManager.GetPosLineCol(context);
                ExceptionHandler.AddException(new CDLException(pos.Item1, pos.Item2, "Type error in function parameters definition"));
            }
            else
            {
            }
            em.AddVariableToScope(context, symbol);

        }
        return base.VisitParamsDef(context);
    }
    public override object VisitList([NotNull] CDLParser.ListContext context)
    {
        LocalListContent.Clear();
        return base.VisitList(context);
    }
    public override object VisitSingleListItem([NotNull] CDLParser.SingleListItemContext context)
    {
        string varName = context.varRef().GetText();
        LocalListContent.Add((1, varName, 1));
        return base.VisitSingleListItem(context);
    }
    public override object VisitNumberedListItem([NotNull] CDLParser.NumberedListItemContext context)
    {
        string varName = context.varRef().GetText();
        int num = int.Parse(context.INT().GetText());
        LocalListContent.Add((num, varName, 1));
        return base.VisitNumberedListItem(context);
    }
    public override object VisitChanceListItem([NotNull] CDLParser.ChanceListItemContext context)
    {
        string varName = context.varRef().GetText();
        int num = int.Parse(context.INT(0).GetText());
        int chance = int.Parse(context.INT(1).GetText());
        LocalListContent.Add((num, varName, chance));
        return base.VisitChanceListItem(context);
    }
    public override object VisitAttackListItem([NotNull] CDLParser.AttackListItemContext context)
    {
        string varName = context.varRef().GetText();
        int num = 1;
        if (context.INT() != null)
        {
            num = int.Parse(context.INT().GetText());
        }
        string targetString = context.enemyTarget().GetText();
        switch (targetString)
        {
            case "player":
                LocalListContent.Add(EnemyTarget.PLAYER);
                break;
            default:
                _logger.LogError("Unable to parse target {t}", targetString);
                break;
        }
        return base.VisitAttackListItem(context);
    }
    public override object VisitProgram([NotNull] CDLParser.ProgramContext context)
    {
        var result = base.VisitProgram(context);
        LogStages();
        LogNodes();
        LogEnemies();
        LogEffects();
        LogCards();

        return result;
    }
    public override object VisitGameProperties([NotNull] CDLParser.GamePropertiesContext context)
    {
        em.Env = new Env(em.Env);
        var result = base.VisitGameProperties(context);

        if (em.Env.PrevEnv != null)
            em.Env = em.Env.PrevEnv;

        return result;
    }
    public override object VisitGamePropName([NotNull] CDLParser.GamePropNameContext context)
    {
        if (!em.isVariableOnScope(context.varName().GetText()))
        {
            _logger.LogDebug("Valid Game Name \"{n}\"", context.varName().GetText());
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
    public override object VisitCardDefinition([NotNull] CDLParser.CardDefinitionContext context)
    {
        Card newCard = new()
        {
            Name = context.GetChild(1).GetText()
        };
        Cards.Add(newCard);
        return base.VisitCardDefinition(context);
    }
    public override object VisitCardRarity([NotNull] CDLParser.CardRarityContext context)
    {
        Card lastCard = Cards.Last();
        if (lastCard.Rarity == "")
        {
            lastCard.Rarity = context.rarityName().GetText();
        }
        else
        {
            _logger.LogError("Multiple rarity definitions for card {c} {d}", lastCard.Name, lastCard.Rarity);
        }
        return base.VisitCardRarity(context);
    }
    public override object VisitCardTargets([NotNull] CDLParser.CardTargetsContext context)
    {
        var result = base.VisitCardTargets(context);
        foreach (var item in LocalListContent)
        {
            Cards.Last().ValidTargets.Add((TargetTypes)item);
        }
        return result;
    }
    public override object VisitCardEffects([NotNull] CDLParser.CardEffectsContext context)
    {
        var result = base.VisitCardEffects(context);

        foreach (var item in LocalListContent)
        {
            try
            {
                (int cnt, string varName, int chance) = ((int, string, int))item;

                // TODO
                // How to handle effect parameters?

                //var referredEffect = new Effect(Effects.Where(x => x.Name ==tmp.eff.Name));
                //Cards.Last().EffectsApplied.Add();

                // Placeholder
                var referredEffect = Effects.Where(x => x.Name == varName).FirstOrDefault();
                if (referredEffect != null)
                    Cards.Last().EffectsApplied.Add((referredEffect, 1));
                
                // TODO
                // Probably need to create Effects for example in VisGlobarVars for above to work
            }
            catch (System.Exception)
            {
                _logger.LogError("Unable to parse effect in card effects list");
                throw;
            }

        }
        return result;
    }
    public override object VisitTargetItem([NotNull] CDLParser.TargetItemContext context)
    {

        string targetString = context.GetText();
        switch (targetString)
        {
            case "enemy":
                LocalListContent.Add(TargetTypes.ENEMY);
                break;
            case "enemies":
                LocalListContent.Add(TargetTypes.ENEMIES);
                break;
            case "player":
                LocalListContent.Add(TargetTypes.PLAYER);
                break;
            default:
                _logger.LogError("Unable to parse target {t}", targetString);
                break;
        }
        return base.VisitTargetItem(context);
    }
    public override object VisitLiteralExpression([NotNull] CDLParser.LiteralExpressionContext context)
    {
        var expressionType = em.GetType(context);
        if (expressionType == null)
        {
            return base.VisitLiteralExpression(context);
        }
        return new ExpressionHelper(
            expressionType, context.GetText()
        );
        return base.VisitLiteralExpression(context);
    }
    public override object VisitPrimaryExpression([NotNull] CDLParser.PrimaryExpressionContext context)
    {
        var result = base.VisitPrimaryExpression(context);
        // TODO WIP
        // Expression evaluation cont
        if (result != null)
            System.Console.WriteLine(result.ToString());
        return result;
    }
    public override object VisitEffectDefinition([NotNull] CDLParser.EffectDefinitionContext context)
    {
        Effects.Add(new Effect(context.varName().GetText()));
        var result = base.VisitEffectDefinition(context);
        return result;
    }
    public override object VisitDamageModEffect([NotNull] CDLParser.DamageModEffectContext context)
    {
        if (context.OUTGOING != null)
        {
            //Effects.Last().OutDmgMod = 
        }
        return base.VisitDamageModEffect(context);
    }
}