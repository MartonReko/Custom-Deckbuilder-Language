using System.Data;
using System.Transactions;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using CDL.Lang.Exceptions;
using CDL.Lang.GameModel;
using CDL.Lang.Parsing.Symboltable;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CDL.Lang.Parsing;

public class VisBlocks(EnvManager envManager, CDLExceptionHandler exceptionHandler, ObjectsHelper objects) : CDLBaseVisitor<object>
{
    private readonly ILogger<VisBlocks> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(LogLevel.Trace)).CreateLogger<VisBlocks>();

    private List<ListHelper> LocalListContent { get; set; } = [];
    private List<(int num, string varName, EnemyTarget target)> LocalEnemyAttackList { get; set; } = [];
    private HashSet<TargetTypes> LocalCardTargetList { get; set; } = [];

    // TODO
    // Possibly unfinished
    private readonly struct ExpressionHelper(CDLType type, string value)
    {
        public readonly CDLType type = type;
        public readonly string value = value;
        public override string ToString()
        {
            return $"ExpressionHelper type: {type.Name}\tvalue: {value}";
        }
    }
    /*
    private readonly struct ListHelper(string name, int num = 1, int chance = 100)
    {

    }
    */
    private Stage currentStage = null!;
    private void LogCharacter()
    {
        string effects = "";
        string deck = "";
        if (objects.Character != null)
        {
            foreach (var effect in objects.Character.EffectEveryTurn)
            {
                effects += $"{effect.Name} ";
            }
            foreach (var card in objects.Character.Deck)
            {
                deck += $"{card.Value}x {card.Key.Name} ";
            }
        }
        _logger.LogDebug(@"Character ""{c}"" properties:
    Health: {health}
    EffectEveryTurn: {effects}
    Deck: {deck}", objects.Character?.Name, objects.Character?.Health, effects, deck);
    }
    private void LogCards()
    {
        foreach (var card in objects.Cards)
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
    private void LogEnemyActions()
    {
        foreach (var ea in objects.EnemyActions)
        {
            string effects = "";
            foreach ((Effect effect, int cnt) in ea.EffectsApplied)
                effects += $"{cnt}x\t{effect.Name} ";

            _logger.LogDebug("EnemyAction \"{c}\" properties:\n\tApplies: {a}", ea.Name, effects);
        }
    }
    private void LogEffects()
    {
        string effects = "";
        foreach (var item in objects.Effects)
        {
            _logger.LogDebug("Effect \"{c}\" properties:\n\tInDmgMod: {t}\n\tOutDmgMod: {a}\n\tDamageDealt: {dmg}", item.Name, item.InDmgMod, item.OutDmgMod, item.DamageDealt);
        }
    }
    private void LogNodes()
    {
        string enemies = "", rewards = "";
        foreach (var item in objects.Nodes)
        {
            enemies = "";
            rewards = "";
            foreach (var enemy in item.Enemies)
            {
                enemies += $"{enemy.Value}x {enemy.Key.Name} ";
            }
            foreach (var reward in item.RarityNumChance)
            {
                rewards += $"{reward.Value.Item1}x {reward.Key} {reward.Value.Item2}% ";
            }
            _logger.LogDebug(@"Node ""{c}"" properties:
    Enemies: {enemies}
    Rewards: {rewards}", item.Name, enemies, rewards);
        }
    }
    private void LogStages()
    {
        string fill = "", cont = "";
        foreach (var item in objects.Stages)
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
        if (objects.Game == null)
            return;
        string stages = "";
        foreach (Stage item in objects.Game.Stages)
        {
            stages += item.Name + " ";
        }
        _logger.LogDebug("Game \"{c}\" properties:\n\tStages: {stages}\n\tPlayer: {player}", objects.Game?.GameName, stages, objects.Game?.Player?.Name);
    }
    private void LogEnemies()
    {
        string actions = "";

        foreach (var item in objects.Enemies)
        {
            actions = "";
            foreach ((EnemyAction ea, EnemyTarget target, int num) in item.Actions)
            {
                actions += $"{num}x {ea.Name} to {target.ToString()} ";
            }
            _logger.LogDebug(@"Enemy ""{c}"" properties:
    Health: {health}
    Actions: {acts}", item.Name, item.Health, actions);
        }
    }
    // TODO
    // Parameters are currently not implemented
    public override object VisitParamsDef([NotNull] CDLParser.ParamsDefContext context)
    {
        foreach (var item in context.typeNameVarName())
        {
            var type = envManager.Ts[item.typeName().GetText()];
            var symbol = new Symbol(item.varName().GetText(), type);
            if (type == envManager.Ts.ERROR)
            {

                (int, int) pos = EnvManager.GetPosLineCol(context);
                exceptionHandler.AddException(new CDLException(pos.Item1, pos.Item2, "Type error in function parameters definition"));
            }
            else
            {
            }
            envManager.AddVariableToScope(context, symbol);

        }
        return base.VisitParamsDef(context);
    }

    // Visitors for lists


    public override object VisitList([NotNull] CDLParser.ListContext context)
    {
        LocalEnemyAttackList.Clear();
        LocalListContent.Clear();
        LocalCardTargetList.Clear();
        return base.VisitList(context);
    }
    public override object VisitSingleListItem([NotNull] CDLParser.SingleListItemContext context)
    {
        string varName = context.varRef().varName().GetText();
        LocalListContent.Add(new ListHelper(varName));
        return base.VisitSingleListItem(context);
    }
    private record ListHelper(string Name, int Num = 1, int Chance = 100);
    public override object VisitNumberedListItem([NotNull] CDLParser.NumberedListItemContext context)
    {
        string varName = context.varRef().varName().GetText();
        int num = int.Parse(context.INT().GetText());
        LocalListContent.Add(new ListHelper(varName, num));
        return base.VisitNumberedListItem(context);
    }

    public override object VisitChanceListItem([NotNull] CDLParser.ChanceListItemContext context)
    {
        string varName = context.varRef().varName().GetText();
        int num = int.Parse(context.INT(0).GetText());
        int chance = int.Parse(context.INT(1).GetText());
        LocalListContent.Add(new ListHelper(varName, num, chance));
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

        if (context.enemyTarget().SELF() != null)
            LocalEnemyAttackList.Add((num, varName, EnemyTarget.SELF));
        else if (context.enemyTarget().PLAYER != null)
            LocalEnemyAttackList.Add((num, varName, EnemyTarget.PLAYER));
        else
            _logger.LogError("Unable to parse target {t}", targetString);


        return base.VisitAttackListItem(context);
    }

    public override object VisitTargetItem([NotNull] CDLParser.TargetItemContext context)
    {

        string targetString = context.GetText();
        switch (targetString)
        {
            case "enemy":
                LocalCardTargetList.Add(TargetTypes.ENEMY);
                break;
            case "enemies":
                LocalCardTargetList.Add(TargetTypes.ENEMIES);
                break;
            case "player":
                LocalCardTargetList.Add(TargetTypes.PLAYER);
                break;
            default:
                _logger.LogError("Unable to parse target {t}", targetString);
                break;
        }
        return base.VisitTargetItem(context);
    }
    public override object VisitProgram([NotNull] CDLParser.ProgramContext context)
    {
        var result = base.VisitProgram(context);
        LogGame();
        LogStages();
        LogNodes();
        LogCharacter();
        LogEnemies();
        LogEffects();
        LogCards();
        LogEnemyActions();

        return result;
    }

    // Visitors for gameSetup

    public override object VisitGameSetup([NotNull] CDLParser.GameSetupContext context)
    {
        // New env is totally unecessary right now
        envManager.Env = new Env(envManager.Env);
        objects.Game = new GameSetup();
        var result = base.VisitGameSetup(context);

        if (envManager.Env.PrevEnv != null)
            envManager.Env = envManager.Env.PrevEnv;

        return result;
    }
    public override object VisitGameName([NotNull] CDLParser.GameNameContext context)
    {
        if (!envManager.IsVariableOnScope(context.varName().GetText()))
        {
            // Cannot be null because it must be initialized higher in the tree
            if (objects.Game != null)
                objects.Game.GameName = context.varName().GetText();
        }
        else
        {
            exceptionHandler.AddException($"Invalid game name {context.varName().GetText()}, already taken");
        }
        return base.VisitGameName(context);
    }

    public override object VisitGamePlayerSelect([NotNull] CDLParser.GamePlayerSelectContext context)
    {
        Symbol? playerName = envManager.GetVariableFromScope(context, context.varName().GetText());
        if (playerName == null)
        {
            exceptionHandler.AddException($"No character found by name {context.varName().GetText()}");
        }
        else
        {
            if (objects.Game != null)
                objects.Game.Player = objects.Character;
        }
        return base.VisitGamePlayerSelect(context);
    }

    public override object VisitGameStages([NotNull] CDLParser.GameStagesContext context)
    {
        var result = base.VisitGameStages(context);
        foreach (var item in LocalListContent)
        {
            Symbol? stage = envManager.GetVariableFromScope(context, item.Name);
            if (stage == null)
            {
                exceptionHandler.AddException(context, $"No stage exists by the name {item.Name}");
            }
            else if (stage.Type != envManager.Ts.STAGE)
            {
                exceptionHandler.AddException(context, $"Type of {item.Name} is not stage, but {stage.Type}");
            }
            else
            {
                objects.Game?.Stages.AddRange(objects.Stages.Where(x => x.Name == stage.Name));
            }
        }
        return result;
    }

    // Visitors for stageDefinition

    public override object VisitStageDefinition([NotNull] CDLParser.StageDefinitionContext context)
    {
        string currentStageName = context.varName().GetText();
        currentStage = objects.Stages.Where(x => x.Name == currentStageName).First();
        var result = base.VisitStageDefinition(context);
        currentStageName = "";
        return result;
    }

    public override object VisitStageLength([NotNull] CDLParser.StageLengthContext context)
    {
        if (!int.TryParse(context.INT().GetText(), out int length))
        {
            exceptionHandler.AddException(context, $"Could not parse stage length for {currentStage?.Name}");
        }
        else if (length < 1)
        {
            exceptionHandler.AddException(context, $"Invalid value for stage {currentStage?.Name} length: {length}");
        }
        else
        {
            objects.Stages.Where(x => x.Name == currentStage?.Name).First().StageLength = length;
        }
        return base.VisitStageLength(context);
    }

    public override object VisitStageWidthMax([NotNull] CDLParser.StageWidthMaxContext context)
    {
        if (!int.TryParse(context.INT().GetText(), out int length))
        {
            exceptionHandler.AddException(context, $"Could not parse stage max width for {currentStage?.Name}");
        }
        else if (length < 1)
        {
            exceptionHandler.AddException(context, $"Invalid value for stage {currentStage?.Name} max width: {length}");
        }
        else
        {
            objects.Stages.Where(x => x.Name == currentStage?.Name).First().StageWidthMax = length;
        }
        return base.VisitStageWidthMax(context);
    }

    public override object VisitStageWidthMin([NotNull] CDLParser.StageWidthMinContext context)
    {
        if (!int.TryParse(context.INT().GetText(), out int length))
        {
            exceptionHandler.AddException(context, $"Could not parse stage min width for {currentStage?.Name}");
        }
        else if (length < 1)
        {
            exceptionHandler.AddException(context, $"Invalid value for stage {currentStage?.Name} min width: {length}");
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

        foreach (var item in LocalListContent)
        {
            if (envManager.CheckType(item.Name, envManager.Ts.NODE))
            {
                Node n = objects.Nodes.First(x => x.Name == item.Name);
                currentStage.FillWith.Add(n);
            }
            else
            {
                BadTypeError(context, item.Name, envManager.Ts.NODE);
            }
        }

        return result;
    }
    private void BadTypeError(ParserRuleContext context, string varName, CDLType goodType)
    {
        var badSymbol = envManager.GetVariableFromScope(context, varName);
        if (badSymbol != null)
        {
            exceptionHandler.AddException(context, $"{varName} must be of type {goodType.Name}, is {badSymbol.Type.Name}");
        }
        else
        {
            exceptionHandler.AddException(context, $"{varName} not found");
        }
    }
    public override object VisitStageMustContain([NotNull] CDLParser.StageMustContainContext context)
    {
        var result = base.VisitStageMustContain(context);
        foreach (var item in LocalListContent)
        {
            if (envManager.CheckType(item.Name, envManager.Ts.NODE))
            {
                Node n = objects.Nodes.First(x => x.Name == item.Name);
                currentStage?.MustContain.Add(n, item.Num);
            }
            else
            {
                exceptionHandler.AddException(context, $"{item.Name} has invalid type, not a node");
            }
        }

        return result;
    }

    public override object VisitStageEndsWith([NotNull] CDLParser.StageEndsWithContext context)
    {
        if (envManager.CheckType(context.varName().GetText(), envManager.Ts.NODE))
        {
            Node listNode = objects.Nodes.First(x => x.Name == context.varName().GetText());
            if (currentStage != null) { currentStage.EndsWith = listNode; }
        }
        else
        {
            exceptionHandler.AddException(context, $"{context.varName().GetText()} has invalid type, must be node");
        }
        return base.VisitStageEndsWith(context);
    }

    // Visitors for nodeDefinition

    private Node currentNode = null!;
    public override object VisitNodeDefinition([NotNull] CDLParser.NodeDefinitionContext context)
    {
        string nodeName = context.varName().GetText();
        currentNode = objects.Nodes.First(x => x.Name == nodeName);
        return base.VisitNodeDefinition(context);
    }

    public override object VisitNodeEnemies([NotNull] CDLParser.NodeEnemiesContext context)
    {
        var result = base.VisitNodeEnemies(context);
        foreach (var item in LocalListContent)
        {
            if (!envManager.CheckType(item.Name, envManager.Ts.ENEMY))
            {
                exceptionHandler.AddException(context, $"{item.Name} does not exist or has invalid type, must be enemy");
            }
            else
            {
                Enemy enemy = objects.Enemies.First(x => x.Name == item.Name);
                currentNode?.Enemies.Add(enemy, item.Num);
            }
        }
        return result;
    }

    public override object VisitNodeRewards([NotNull] CDLParser.NodeRewardsContext context)
    {
        var result = base.VisitNodeRewards(context);
        foreach (var item in LocalListContent)
        {
            if (envManager.CheckType(item.Name, envManager.Ts.RARITY))
            {
                currentNode.RarityNumChance.Add(item.Name, (item.Num, item.Chance));
            }
            else
            {
                BadTypeError(context, item.Name, envManager.Ts.RARITY);
            }
        }
        return result;
    }

    // Visitors for charSetup

    public override object VisitCharHealth([NotNull] CDLParser.CharHealthContext context)
    {
        if (context.number().INT() == null)
        {
            exceptionHandler.AddException(context, $"{context.number().GetText()} must be int");
        }
        else if (int.TryParse(context.number().INT().GetText(), out int value))
        {
            if (objects.Character != null) { objects.Character.Health = value; }
        }
        else
        {
            exceptionHandler.AddException(context, $"Unable to parse {context.number().GetText()} as int");
        }
        return base.VisitCharHealth(context);
    }

    public override object VisitCharEffects([NotNull] CDLParser.CharEffectsContext context)
    {
        var result = base.VisitCharEffects(context);
        foreach (ListHelper item in LocalListContent)
        {
            if (!envManager.CheckType(item.Name, envManager.Ts.EFFECT))
            {
                exceptionHandler.AddException(context, $"{item.Name} does not exist or has invalid type, must be effect");
            }
            else
            {
                Effect curEffect = objects.Effects.First(x => x.Name == item.Name);
                objects.Character?.EffectEveryTurn.Add(curEffect);
            }
        }
        return result;
    }

    public override object VisitCharDeck([NotNull] CDLParser.CharDeckContext context)
    {
        var result = base.VisitCharDeck(context);

        foreach (ListHelper item in LocalListContent)
        {
            if (!envManager.CheckType(item.Name, envManager.Ts.CARD))
            {
                exceptionHandler.AddException(context, $"{item.Name} does not exist or has invalid type, must be card");
            }
            else
            {
                Card curCard = objects.Cards.First(x => x.Name == item.Name);
                objects.Character?.Deck.Add(curCard, item.Num);
            }
        }

        return result;
    }

    // Visitors for enemyDefinition

    // Disables compiler warning for possible null values.
    private Enemy currentEnemy = null!;
    public override object VisitEnemyDefinition([NotNull] CDLParser.EnemyDefinitionContext context)
    {
        string enemyName = context.varName().GetText();
        currentEnemy = objects.Enemies.First(x => x.Name == enemyName);
        return base.VisitEnemyDefinition(context);
    }

    public override object VisitEnemyHealth([NotNull] CDLParser.EnemyHealthContext context)
    {
        if (int.TryParse(context.number().GetText(), out int health))
        {
            if (health <= 0)
            {
                exceptionHandler.AddException($"Invalid value: {health}, enemy health must be at least 1");
            }
            else
            {
                currentEnemy.Health = health;
            }
        }
        else
        {
            exceptionHandler.AddException(context, $"Unable to parse {context.number().GetText()} as int");
        }
        return base.VisitEnemyHealth(context);
    }

    public override object VisitEnemyActions([NotNull] CDLParser.EnemyActionsContext context)
    {
        var result = base.VisitEnemyActions(context);
        foreach (var (num, varName, target) in LocalEnemyAttackList)
        {
            if (!envManager.CheckType(varName, envManager.Ts.ENEMYACTION))
            {
                exceptionHandler.AddException(context, $"{varName} does not exist or has invalid type, must be of enemy action type");
            }
            else
            {
                EnemyAction curAction = objects.EnemyActions.First(x => x.Name == varName);
                currentEnemy?.Actions.Add((curAction, target, num));
            }
        }
        return result;
    }

    // Visitors for enemyAction

    private EnemyAction? currentEAction;
    public override object VisitEnemyActionDefinition([NotNull] CDLParser.EnemyActionDefinitionContext context)
    {
        currentEAction = objects.EnemyActions.First(x => x.Name == context.varName().GetText());
        return base.VisitEnemyActionDefinition(context);
    }

    public override object VisitEnemyActionEffects([NotNull] CDLParser.EnemyActionEffectsContext context)
    {
        var result = base.VisitEnemyActionEffects(context);
        foreach (var item in LocalListContent)
        {
            if (!envManager.CheckType(item.Name, envManager.Ts.EFFECT))
            {
                exceptionHandler.AddException(context, $"{item.Name} has invalid type, must be effect, or does not exist");
            }
            else
            {
                Effect effect = objects.Effects.First(x => x.Name == item.Name);
                currentEAction?.EffectsApplied.Add((effect, item.Num));
            }
        }
        return result;
    }

    // Visitors for effectDefinition

    private Effect? currentEffect;
    public override object VisitEffectDefinition([NotNull] CDLParser.EffectDefinitionContext context)
    {
        //oH.Effects.Add(new Effect(context.varName().GetText()));
        currentEffect = objects.Effects.First(x => x.Name == context.varName().GetText());
        var result = base.VisitEffectDefinition(context);
        return result;
    }

    public override object VisitDamageModEffect([NotNull] CDLParser.DamageModEffectContext context)
    {
        var result = base.VisitDamageModEffect(context);
        if (currentEffect != null)
        {
            if (context.INCOMING != null)
            {
                double.TryParse(localExpressions.Pop().value, out double value);
                currentEffect.InDmgMod = value;
            }
            else
            {
                double.TryParse(localExpressions.Pop().value, out double value);
                currentEffect.OutDmgMod = value;
            }
        }
        return result;
    }

    public override object VisitDamageDealEffect([NotNull] CDLParser.DamageDealEffectContext context)
    {
        // TODO
        // make this value transformation universal
        var result = base.VisitDamageDealEffect(context);
        var popped = localExpressions.Pop().value;
        if (!double.TryParse(popped, out double val))
        {
            var symbol = envManager.GetVariableFromScope(context, popped);
            val = double.Parse(symbol!.Value!);
        }
        //double value = double.Parse(localExpressions.Pop().value);
        if (currentEffect != null) currentEffect.DamageDealt = val;
        if (context.effectActivationOpt().INSTANTLY != null)
        {
            currentEffect!.EffectType = EffectType.INSTANT;
        }
        else if (context.effectActivationOpt().ENDOFTURN != null)
        {
            currentEffect!.EffectType = EffectType.TURNEND;
        }
        return result;
    }



    // Visitors for cardDefinition

    private Card? currentCard;
    public override object VisitCardDefinition([NotNull] CDLParser.CardDefinitionContext context)
    {
        currentCard = objects.Cards.First(x => x.Name == context.varName().GetText());
        var result = base.VisitCardDefinition(context);

        return result;
    }
    public override object VisitCardRarity([NotNull] CDLParser.CardRarityContext context)
    {
        if (currentCard?.Rarity == "")
        {
            currentCard.Rarity = context.rarityName().GetText();
        }
        else
        {
            _logger.LogError("Multiple rarity definitions for card {c} {d}", currentCard?.Name, currentCard?.Rarity);
        }
        return base.VisitCardRarity(context);
    }
    public override object VisitCardTargets([NotNull] CDLParser.CardTargetsContext context)
    {
        var result = base.VisitCardTargets(context);
        foreach (var item in LocalCardTargetList)
        {
            currentCard?.ValidTargets.Add(item);
        }
        return result;
    }
    public override object VisitCardEffects([NotNull] CDLParser.CardEffectsContext context)
    {
        var result = base.VisitCardEffects(context);
        foreach (var item in LocalListContent)
        {
            if (!envManager.CheckType(item.Name, envManager.Ts.EFFECT))
            {
                exceptionHandler.AddException(context, $"{item.Name} has invalid type, must be effect, or does not exist");
            }
            else
            {
                Effect effect = objects.Effects.First(x => x.Name == item.Name);
                currentCard?.EffectsApplied.Add((effect, item.Num));
            }
        }
        return result;
    }

    // Visitors for expressions

    private Stack<ExpressionHelper> localExpressions = [];
    public override object VisitExpressionContainer([NotNull] CDLParser.ExpressionContainerContext context)
    {
        localExpressions.Clear();
        var result = base.VisitExpressionContainer(context);
        return result;
    }
    public override object VisitLiteralExpression([NotNull] CDLParser.LiteralExpressionContext context)
    {

        var expressionType = envManager.GetType(context);
        if (expressionType == null)
        {
            exceptionHandler.AddException(context, $"Literal expression {context.GetText()} has unrecognized type");
            return base.VisitLiteralExpression(context);
        }
        var symbol = envManager.GetVariableFromScope(context, context.GetText().ToString());
        if (symbol?.Value != null)
        {
            var num = double.Parse(symbol.Value);
            localExpressions.Push(new ExpressionHelper(expressionType, num.ToString()));
        }
        else
        {
            localExpressions.Push(new ExpressionHelper(expressionType, context.GetText()));
        }
        return base.VisitLiteralExpression(context);
    }
    public override object VisitPrimaryExpression([NotNull] CDLParser.PrimaryExpressionContext context)
    {
        var result = base.VisitPrimaryExpression(context);
        return result;
    }
    public override object VisitOpExpression([NotNull] CDLParser.OpExpressionContext context)
    {
        var result = base.VisitOpExpression(context);
        foreach (ExpressionHelper item in localExpressions)
        {
            if (!(item.type == envManager.Ts.DOUBLE || item.type == envManager.Ts.INT))
            {
                localExpressions.Clear();
                exceptionHandler.AddException(context, $"Type error in evaluating expression: int or double expected, got {item.type.Name}");
                return result;
            }
        }
        double opB = double.Parse(localExpressions.Pop().value);
        double opA = double.Parse(localExpressions.Pop().value);
        switch (context.expressionOp().GetText())
        {
            case "+":
                localExpressions.Push(new ExpressionHelper(envManager.Ts.DOUBLE, (opA + opB).ToString()));
                break;
            case "-":
                localExpressions.Push(new ExpressionHelper(envManager.Ts.DOUBLE, (opA - opB).ToString()));
                break;
            case "*":
                localExpressions.Push(new ExpressionHelper(envManager.Ts.DOUBLE, (opA * opB).ToString()));
                break;
            case "/":
                localExpressions.Push(new ExpressionHelper(envManager.Ts.DOUBLE, (opA / opB).ToString()));
                break;
            default:
                localExpressions.Clear();
                localExpressions.Push(new ExpressionHelper(envManager.Ts.DOUBLE, 1.ToString()));
                break;
        }
        return result;
    }
}

// TODO replace all logerrors with CDLExceptions
