using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using CDL.exceptions;
using CDL.game;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace CDL;

public class VisBlocks(EnvManager em, CDLExceptionHandler exceptionHandler, ObjectsHelper oH) : CDLBaseVisitor<object>
{
    private CDLExceptionHandler ExceptionHandler { get; set; } = exceptionHandler;

    private readonly ILogger<VisBlocks> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)).CreateLogger<VisBlocks>();

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
        foreach (var card in oH.Cards)
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
        foreach (var item in oH.Effects)
        {
            _logger.LogDebug("Effect \"{c}\" properties:\n\tInDmgMod: {t}\n\tOutDmgMod: {a}", item.Name, item.InDmgMod, item.OutDmgMod);
        }
    }
    private void LogNodes()
    {
        foreach (var item in oH.Nodes)
        {
            _logger.LogDebug("Node \"{c}\"", item.Name);
        }
    }
    private void LogStages()
    {
        foreach (var item in oH.Nodes)
        {
            _logger.LogDebug("Stage \"{c}\"", item.Name);
        }
    }
    private void LogGame()
    {
        if (oH.Game == null)
            return;
        string stages = "";
        foreach (Stage item in oH.Game.Stages)
        {
            stages += item.Name + " ";
        }
        _logger.LogDebug("Game \"{c}\" properties:\n\tStages: {stages}\n\tPlayer: {player}", oH.Game?.GameName, stages, oH.Game?.Player?.Name);
    }
    private void LogEnemies()
    {
        foreach (var item in oH.Enemies)
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
        string varName = context.varRef().varName().GetText();
        LocalListContent.Add((1, varName, 1));
        return base.VisitSingleListItem(context);
    }
    public override object VisitNumberedListItem([NotNull] CDLParser.NumberedListItemContext context)
    {
        string varName = context.varRef().varName().GetText();
        int num = int.Parse(context.INT().GetText());
        LocalListContent.Add((num, varName, 1));
        return base.VisitNumberedListItem(context);
    }
    public override object VisitChanceListItem([NotNull] CDLParser.ChanceListItemContext context)
    {
        string varName = context.varRef().varName().GetText();
        int num = int.Parse(context.INT(0).GetText());
        int chance = int.Parse(context.INT(1).GetText());
        LocalListContent.Add((num, varName, chance));
        return base.VisitChanceListItem(context);
    }
    public override object VisitAttackListItem([NotNull] CDLParser.AttackListItemContext context)
    {
        string varName = context.varRef().varName().GetText();
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
        LogGame();
        LogStages();
        LogNodes();
        LogEnemies();
        LogEffects();
        LogCards();

        return result;
    }

    // Visitors for gameSetup

    public override object VisitGameSetup([NotNull] CDLParser.GameSetupContext context)
    {
        // New env is totally unecessary right now
        em.Env = new Env(em.Env);
        oH.Game = new GameSetup();
        var result = base.VisitGameSetup(context);

        if (em.Env.PrevEnv != null)
            em.Env = em.Env.PrevEnv;

        return result;
    }
    public override object VisitGameName([NotNull] CDLParser.GameNameContext context)
    {
        if (!em.isVariableOnScope(context.varName().GetText()))
        {
            // Cannot be null because it must be initialized higher in the tree
            if (oH.Game != null)
                oH.Game.GameName = context.varName().GetText();
        }
        else
        {
            ExceptionHandler.AddException($"Invalid game name {context.varName().GetText()}, already taken");
        }
        return base.VisitGameName(context);
    }

    public override object VisitGamePlayerSelect([NotNull] CDLParser.GamePlayerSelectContext context)
    {
        Symbol? playerName = em.getVariableFromScope(context, context.varName().GetText());
        if (playerName == null)
        {
            ExceptionHandler.AddException($"No character found by name {context.varName().GetText()}");
        }
        else
        {
            if (oH.Game != null)
                oH.Game.Player = oH.Character;
        }
        return base.VisitGamePlayerSelect(context);
    }

    public override object VisitGameStages([NotNull] CDLParser.GameStagesContext context)
    {
        // TODO
        var result = base.VisitGameStages(context);
        // Explicit cast generated by VSCode
        foreach ((int num, string name, int chance) item in LocalListContent.Select(v => ((int num, string name, int chance))v))
        {
            Symbol? stage = em.getVariableFromScope(context, item.name);
            if (stage == null)
            {
                ExceptionHandler.AddException(context, $"No stage exists by the name {item.name}");
            }
            else if (stage.Type != em.Ts.STAGE)
            {
                ExceptionHandler.AddException(context, $"Type of {item.name} is not stage, but {stage.Type}");
            }
            else
            {
                oH.Game?.Stages.AddRange(oH.Stages.Where(x => x.Name == stage.Name));
            }
        }
        return result;
    }

    // Visitors for stageDefinition

    // Visitors for nodeDefinition

    // Visitors for charSetup

    // Visitors for enemyDefinition

    // Visitors for effectDefinition

    // Visitors for cardDefinition

    public override object VisitCardRarity([NotNull] CDLParser.CardRarityContext context)
    {
        Card lastCard = oH.Cards.Last();
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
            oH.Cards.Last().ValidTargets.Add((TargetTypes)item);
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
                var referredEffect = oH.Effects.Where(x => x.Name == varName).FirstOrDefault();
                if (referredEffect != null)
                    oH.Cards.Last().EffectsApplied.Add((referredEffect, 1));

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
        oH.Effects.Add(new Effect(context.varName().GetText()));
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

// TODO replace all logerrors with CDLExceptions ;(