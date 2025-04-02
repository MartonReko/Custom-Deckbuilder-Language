using System.Data;
using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using CDL.exceptions;
using CDL.game;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace CDL;

public class VisBlocks(EnvManager em, CDLExceptionHandler exceptionHandler, ObjectsHelper oH) : CDLBaseVisitor<object>
{
    private CDLExceptionHandler ExceptionHandler { get; set; } = exceptionHandler;

    private readonly ILogger<VisBlocks> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)).CreateLogger<VisBlocks>();

    private List<object> LocalListContent { get; set; } = [];
    private List<ListHelper> NewLocalListContent { get; set; } = [];
    private readonly struct ExpressionHelper(CDLType type, object value)
    {
        readonly CDLType type = type;
        readonly object value = value;
        public override string ToString()
        {
            return $"ExpressionHelper type: {type.Name}\tvalue: {value}";
        }
    }
    private readonly struct ListHelper(string name, int num = -1, int chance = -1)
    {
        public readonly int num = num;
        public readonly string name = name;
        public readonly int chance = chance;

    }
    //private string currentStageName = "";
    private Stage? currentStage = null;
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
        string enemies = "",rewards = "";
        foreach (var item in oH.Nodes)
        {
            enemies = "";
            rewards = "";
            foreach(var enemy in item.Enemies){
                enemies += $"{enemy.Value}x {enemy.Key.Name} ";
            }
            foreach(var reward in item.RarityNumChance){
                rewards += $"{reward.Value.Item1}x {reward.Key} {reward.Value.Item2}% ";
            }
            _logger.LogDebug(@"Node ""{c}"" properties:
    Enemies: {enemies}
    Rewards: {rewards}", item.Name,enemies,rewards);
        }
    }
    private void LogStages()
    {
        string fill = "", cont = "";
        foreach (var item in oH.Stages)
        {
            fill = "";
            cont = "";
            foreach (var node in item.FillWith)
            {
                fill += $"{node.Name} ";
            }
            foreach (var node in item.MustContain)
            {
                cont += $"{node.Value}x {node.Key.Name} ";
            }
            _logger.LogDebug(@"Stage ""{c}"" properties:
    Length: {length}
    MinWidth: {minW} 
    MaxWidth: {maxW}
    Fill: {fill}
    MustContain: {cont}
    EndsWith: {end}", item.Name, item.StageLength, item.StageWidthMin, item.StageWidthMax, fill, cont, item?.EndsWith?.Name);
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
        NewLocalListContent.Clear();
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
        NewLocalListContent.Add(new ListHelper(varName, num));
        return base.VisitNumberedListItem(context);
    }
    public override object VisitChanceListItem([NotNull] CDLParser.ChanceListItemContext context)
    {
        string varName = context.varRef().varName().GetText();
        int num = int.Parse(context.INT(0).GetText());
        int chance = int.Parse(context.INT(1).GetText());
        LocalListContent.Add((num, varName, chance));
        NewLocalListContent.Add(new ListHelper(varName, num, chance));
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
        if (!em.IsVariableOnScope(context.varName().GetText()))
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
        Symbol? playerName = em.GetVariableFromScope(context, context.varName().GetText());
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
        var result = base.VisitGameStages(context);
        // Explicit cast generated by VSCode
        foreach ((int num, string name, int chance) item in LocalListContent.Select(v => ((int num, string name, int chance))v))
        {
            Symbol? stage = em.GetVariableFromScope(context, item.name);
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

    public override object VisitStageDefinition([NotNull] CDLParser.StageDefinitionContext context)
    {
        string currentStageName = context.varName().GetText();
        currentStage = oH.Stages.Where(x => x.Name == currentStageName).First();
        var result = base.VisitStageDefinition(context);
        currentStageName = "";
        currentStage = null;
        return result;
    }

    public override object VisitStageLength([NotNull] CDLParser.StageLengthContext context)
    {
        if (!int.TryParse(context.INT().GetText(), out int length))
        {
            ExceptionHandler.AddException(context, $"Could not parse stage length for {currentStage?.Name}");
        }
        else if (length < 1)
        {
            ExceptionHandler.AddException(context, $"Invalid value for stage {currentStage?.Name} length: {length}");
        }
        else
        {
            oH.Stages.Where(x => x.Name == currentStage?.Name).First().StageLength = length;
        }
        return base.VisitStageLength(context);
    }
    public override object VisitStageWidthMax([NotNull] CDLParser.StageWidthMaxContext context)
    {
        if (!int.TryParse(context.INT().GetText(), out int length))
        {
            ExceptionHandler.AddException(context, $"Could not parse stage max width for {currentStage?.Name}");
        }
        else if (length < 1)
        {
            ExceptionHandler.AddException(context, $"Invalid value for stage {currentStage?.Name} max width: {length}");
        }
        else
        {
            oH.Stages.Where(x => x.Name == currentStage?.Name).First().StageWidthMax = length;
        }
        return base.VisitStageWidthMax(context);
    }
    public override object VisitStageWidthMin([NotNull] CDLParser.StageWidthMinContext context)
    {
        if (!int.TryParse(context.INT().GetText(), out int length))
        {
            ExceptionHandler.AddException(context, $"Could not parse stage min width for {currentStage?.Name}");
        }
        else if (length < 1)
        {
            ExceptionHandler.AddException(context, $"Invalid value for stage {currentStage?.Name} min width: {length}");
        }
        else
        {
            //oH.Stages.Where(x => x.Name == currentStageName).First().StageWidthMin = length;
            if (currentStage != null) { currentStage.StageWidthMin = length; }
        }
        return base.VisitStageWidthMin(context);
    }
    public override object VisitStageFillWith([NotNull] CDLParser.StageFillWithContext context)
    {
        var result = base.VisitStageFillWith(context);
        foreach ((int num, string name, int chance) in LocalListContent.Select(v => ((int num, string name, int chance))v))
        {
            if (em.CheckVarType(name, em.Ts.NODE))
            {
                Node listNode = oH.Nodes.First(x => x.Name == name);
                currentStage?.FillWith.Add(listNode);
            }
            else
            {
                ExceptionHandler.AddException(context, $"{name} has invalid type, not a node");
            }
        }

        return result;
    }

    public override object VisitStageMustContain([NotNull] CDLParser.StageMustContainContext context)
    {
        var result = base.VisitStageMustContain(context);
        // TODO also use NewLocalListContent for other places instead of casting...
        foreach (var item in NewLocalListContent)
        {
            if (em.CheckVarType(item.name, em.Ts.NODE))
            {
                Node listNode = oH.Nodes.First(x => x.Name == item.name);
                currentStage?.MustContain.Add(listNode, item.num);
            }
            else
            {
                ExceptionHandler.AddException(context, $"{item.name} has invalid type, not a node");
            }
        }

        return result;
    }

    public override object VisitStageEndsWith([NotNull] CDLParser.StageEndsWithContext context)
    {
        if (em.CheckVarType(context.varName().GetText(), em.Ts.NODE))
        {
            Node listNode = oH.Nodes.First(x => x.Name == context.varName().GetText());
            if (currentStage != null) { currentStage.EndsWith = listNode; }
        }
        else
        {
            ExceptionHandler.AddException(context, $"{context.varName().GetText()} has invalid type, must be node");
        }
        return base.VisitStageEndsWith(context);
    }

    // Visitors for nodeDefinition

    private Node currentNode;
    public override object VisitNodeDefinition([NotNull] CDLParser.NodeDefinitionContext context)
    {
        string nodeName = context.varName().GetText();
        currentNode = oH.Nodes.First(x => x.Name == nodeName);
        return base.VisitNodeDefinition(context);
    }

    public override object VisitNodeEnemies([NotNull] CDLParser.NodeEnemiesContext context)
    {
        var result = base.VisitNodeEnemies(context);
        foreach (var item in NewLocalListContent)
        {
            if (!em.CheckVarType(item.name, em.Ts.ENEMY))
            {
                ExceptionHandler.AddException(context, $"{item.name} has invalid type, must be enemy, or does not exist");
            }
            else
            {
                Enemy enemy = oH.Enemies.First(x => x.Name == item.name);
                currentNode.Enemies.Add(enemy,item.num);
            }
        }
        return result;
    }

    public override object VisitNodeRewards([NotNull] CDLParser.NodeRewardsContext context)
    {
        var result = base.VisitNodeRewards(context);
        foreach (var item in NewLocalListContent)
        {
            if (!em.CheckVarType(item.name, em.Ts.STRING))
            {
                ExceptionHandler.AddException(context, $"{item.name} has invalid type, must be string, or does not exist");
            }
            else
            {
                currentNode.RarityNumChance.Add(item.name,(item.num,item.chance));
            }
        }
        return result;
    }

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