using System.Data;
using Antlr4.Runtime.Misc;
using CDL.Lang.Exceptions;
using CDL.Lang.GameModel;
using CDL.Lang.Parsing.Symboltable;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CDL.Lang.Parsing;

public class VisBlocks(EnvManager em, CDLExceptionHandler exceptionHandler, ObjectsHelper oH) : CDLBaseVisitor<object>
{
    private CDLExceptionHandler ExceptionHandler { get; set; } = exceptionHandler;

    private readonly ILogger<VisBlocks> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(LogLevel.Trace)).CreateLogger<VisBlocks>();

    private List<ListHelper> LocalListContent { get; set; } = [];
    private List<(int num, string varName, EnemyTarget target)> LocalEnemyAttackList { get; set; } = [];
    private HashSet<TargetTypes> LocalCardTargetList { get; set; } = [];
    private readonly struct ExpressionHelper(CDLType type, string value)
    {
        public readonly CDLType type = type;
        public readonly string value = value;
        public override string ToString()
        {
            return $"ExpressionHelper type: {type.Name}\tvalue: {value}";
        }
    }
    private readonly struct ListHelper(string name, int num = 1, int chance = 100)
    {
        public readonly int num = num;
        public readonly string name = name;
        public readonly int chance = chance;

    }
    private Stage? currentStage = null;
    private void LogCharacter()
    {
        string effects = "";
        string deck = "";
        if (oH.Character != null)
        {
            foreach (var effect in oH.Character.EffectEveryTurn)
            {
                effects += $"{effect.Name} ";
            }
            foreach (var card in oH.Character.Deck)
            {
                deck += $"{card.Value}x {card.Key.Name} ";
            }
        }
        _logger.LogDebug(@"Character ""{c}"" properties:
    Health: {health}
    EffectEveryTurn: {effects}
    Deck: {deck}", oH.Character?.Name, oH.Character?.Health, effects, deck);
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
    private void LogEnemyActions()
    {
        foreach (var ea in oH.EnemyActions)
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
        foreach (var item in oH.Effects)
        {
            effects = "";
            foreach (var effect in item.EffectsApplied)
            {
                effects += $"{effect.num}x {effect.effect.Name} to {effect.target}";
            }
            _logger.LogDebug("Effect \"{c}\" properties:\n\tInDmgMod: {t}\n\tOutDmgMod: {a}\n\tDamageDealt: {dmg}\n\tEffects applied: {effects}", item.Name, item.InDmgMod, item.OutDmgMod, item.DamageDealt, effects);
        }
    }
    private void LogNodes()
    {
        string enemies = "", rewards = "";
        foreach (var item in oH.Nodes)
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
        string actions = "";

        foreach (var item in oH.Enemies)
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

        if (context.enemyTarget().SELF != null)
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
        foreach (var item in LocalListContent)
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

        foreach (var item in LocalListContent)
        {
            if (em.CheckVarType(item.name, em.Ts.NODE))
            {
                Node listNode = oH.Nodes.First(x => x.Name == item.name);
                currentStage?.FillWith.Add(listNode);
            }
            else
            {
                ExceptionHandler.AddException(context, $"{item.name} has invalid type, not a node");
            }
        }

        return result;
    }

    public override object VisitStageMustContain([NotNull] CDLParser.StageMustContainContext context)
    {
        var result = base.VisitStageMustContain(context);
        // TODO also use LocalListContent for other places instead of casting...
        foreach (var item in LocalListContent)
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

    private Node? currentNode;
    public override object VisitNodeDefinition([NotNull] CDLParser.NodeDefinitionContext context)
    {
        string nodeName = context.varName().GetText();
        currentNode = oH.Nodes.First(x => x.Name == nodeName);
        return base.VisitNodeDefinition(context);
    }

    public override object VisitNodeEnemies([NotNull] CDLParser.NodeEnemiesContext context)
    {
        var result = base.VisitNodeEnemies(context);
        foreach (var item in LocalListContent)
        {
            if (!em.CheckVarType(item.name, em.Ts.ENEMY))
            {
                ExceptionHandler.AddException(context, $"{item.name} does not exist or has invalid type, must be enemy");
            }
            else
            {
                Enemy enemy = oH.Enemies.First(x => x.Name == item.name);
                currentNode?.Enemies.Add(enemy, item.num);
            }
        }
        return result;
    }

    public override object VisitNodeRewards([NotNull] CDLParser.NodeRewardsContext context)
    {
        var result = base.VisitNodeRewards(context);
        foreach (var item in LocalListContent)
        {
            if (!em.CheckVarType(item.name, em.Ts.STRING))
            {
                ExceptionHandler.AddException(context, $"{item.name} has invalid type, must be string, or does not exist");
            }
            else
            {
                currentNode?.RarityNumChance.Add(item.name, (item.num, item.chance));
            }
        }
        return result;
    }

    // Visitors for charSetup

    public override object VisitCharHealth([NotNull] CDLParser.CharHealthContext context)
    {
        if (context.number().INT() == null)
        {
            ExceptionHandler.AddException(context, $"{context.number().GetText()} must be int");
        }
        else if (int.TryParse(context.number().INT().GetText(), out int value))
        {
            if (oH.Character != null) { oH.Character.Health = value; }
        }
        else
        {
            ExceptionHandler.AddException(context, $"Unable to parse {context.number().GetText()} as int");
        }
        return base.VisitCharHealth(context);
    }

    public override object VisitCharEffects([NotNull] CDLParser.CharEffectsContext context)
    {
        var result = base.VisitCharEffects(context);
        foreach (ListHelper item in LocalListContent)
        {
            if (!em.CheckVarType(item.name, em.Ts.EFFECT))
            {
                ExceptionHandler.AddException(context, $"{item.name} does not exist or has invalid type, must be effect");
            }
            else
            {
                Effect curEffect = oH.Effects.First(x => x.Name == item.name);
                oH.Character?.EffectEveryTurn.Add(curEffect);
            }
        }
        return result;
    }

    public override object VisitCharDeck([NotNull] CDLParser.CharDeckContext context)
    {
        var result = base.VisitCharDeck(context);

        foreach (ListHelper item in LocalListContent)
        {
            if (!em.CheckVarType(item.name, em.Ts.CARD))
            {
                ExceptionHandler.AddException(context, $"{item.name} does not exist or has invalid type, must be card");
            }
            else
            {
                Card curCard = oH.Cards.First(x => x.Name == item.name);
                oH.Character?.Deck.Add(curCard, item.num);
            }
        }

        return result;
    }

    // Visitors for enemyDefinition

    private Enemy? currentEnemy;
    public override object VisitEnemyDefinition([NotNull] CDLParser.EnemyDefinitionContext context)
    {
        string enemyName = context.varName().GetText();
        currentEnemy = oH.Enemies.First(x => x.Name == enemyName);
        return base.VisitEnemyDefinition(context);
    }
    public override object VisitEnemyHealth([NotNull] CDLParser.EnemyHealthContext context)
    {
        if (int.TryParse(context.number().GetText(), out int value))
        {
            if (currentEnemy != null) { currentEnemy.Health = value; }
        }
        else
        {
            ExceptionHandler.AddException(context, $"Unable to parse {context.number().GetText()} as int");
        }
        return base.VisitEnemyHealth(context);
    }

    public override object VisitEnemyActions([NotNull] CDLParser.EnemyActionsContext context)
    {
        var result = base.VisitEnemyActions(context);
        foreach (var (num, varName, target) in LocalEnemyAttackList)
        {
            if (!em.CheckVarType(varName, em.Ts.ENEMYACTION))
            {
                ExceptionHandler.AddException(context, $"{varName} does not exist or has invalid type, must be of enemy action type");
            }
            else
            {
                EnemyAction curAction = oH.EnemyActions.First(x => x.Name == varName);
                currentEnemy?.Actions.Add((curAction, target, num));
            }
        }
        return result;
    }

    // Visitors for enemyAction

    private EnemyAction? currentEAction;
    public override object VisitEnemyActionDefinition([NotNull] CDLParser.EnemyActionDefinitionContext context)
    {
        currentEAction = oH.EnemyActions.First(x => x.Name == context.varName().GetText());
        return base.VisitEnemyActionDefinition(context);
    }

    public override object VisitEnemyActionEffects([NotNull] CDLParser.EnemyActionEffectsContext context)
    {
        var result = base.VisitEnemyActionEffects(context);
        foreach (var item in LocalListContent)
        {
            if (!em.CheckVarType(item.name, em.Ts.EFFECT))
            {
                ExceptionHandler.AddException(context, $"{item.name} has invalid type, must be effect, or does not exist");
            }
            else
            {
                Effect effect = oH.Effects.First(x => x.Name == item.name);
                currentEAction?.EffectsApplied.Add((effect, item.num));
            }
        }
        return result;
    }

    // Visitors for effectDefinition

    private Effect? currentEffect;
    public override object VisitEffectDefinition([NotNull] CDLParser.EffectDefinitionContext context)
    {
        //oH.Effects.Add(new Effect(context.varName().GetText()));
        currentEffect = oH.Effects.First(x => x.Name == context.varName().GetText());
        var result = base.VisitEffectDefinition(context);
        return result;
    }

    public override object VisitDamageModEffect([NotNull] CDLParser.DamageModEffectContext context)
    {
        var result = base.VisitDamageModEffect(context);
        if (currentEffect != null)
        {

            // TODO direction
            double.TryParse(localExpressions.Pop().value, out double value);
            currentEffect.InDmgMod = value;
        }
        return result;
    }

    public override object VisitDamageDealEffect([NotNull] CDLParser.DamageDealEffectContext context)
    {
        var result = base.VisitDamageDealEffect(context);
        double value = double.Parse(localExpressions.Pop().value);
        // TODO
        //currentEffect!.DamageDealt = 1;
        if (currentEffect != null) currentEffect.DamageDealt = value;
        return result;
    }

    public override object VisitApplierEffect([NotNull] CDLParser.ApplierEffectContext context)
    {
        var result = base.VisitApplierEffect(context);
        foreach (var item in LocalListContent)
        {
            if (!em.CheckVarType(item.name, em.Ts.EFFECT))
            {
                ExceptionHandler.AddException(context, $"{item.name} has invalid type, must be effect, or does not exist");
            }
            else
            {
                if (Enum.TryParse(typeof(EffectTarget), context.effectTarget().GetText().ToUpper(), out object? target))
                {
                    EffectTarget effectTarget = (EffectTarget)target;
                    Effect effect = oH.Effects.First(x => x.Name == item.name);
                    currentEffect?.EffectsApplied.Add((effect, item.num, effectTarget));
                }
                else
                {
                    ExceptionHandler.AddException(context, $"Unable to parse effect target {context.effectTarget().GetText()}");
                }
            }
        }
        return result;
    }

    // Visitors for cardDefinition

    private Card? currentCard;
    public override object VisitCardDefinition([NotNull] CDLParser.CardDefinitionContext context)
    {
        currentCard = oH.Cards.First(x => x.Name == context.varName().GetText());
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
            if (!em.CheckVarType(item.name, em.Ts.EFFECT))
            {
                ExceptionHandler.AddException(context, $"{item.name} has invalid type, must be effect, or does not exist");
            }
            else
            {
                Effect effect = oH.Effects.First(x => x.Name == item.name);
                currentCard?.EffectsApplied.Add((effect, item.num));
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
        var expressionType = em.GetType(context);
        if (expressionType == null)
        {
            ExceptionHandler.AddException(context, $"Literal expression {context.GetText()} has unrecognized type");
            return base.VisitLiteralExpression(context);
        }
        localExpressions.Push(new ExpressionHelper(expressionType, context.GetText()));
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
            if (!(item.type == em.Ts.DOUBLE || item.type == em.Ts.INT))
            {
                localExpressions.Clear();
                ExceptionHandler.AddException(context, $"Type error in evaluating expression: int or double expected, got {item.type.Name}");
                return result;
            }
        }
        double opB = double.Parse(localExpressions.Pop().value);
        double opA = double.Parse(localExpressions.Pop().value);
        switch (context.expressionOp().GetText())
        {
            case "+":
                localExpressions.Push(new ExpressionHelper(em.Ts.DOUBLE, (opA + opB).ToString()));
                break;
            case "-":
                localExpressions.Push(new ExpressionHelper(em.Ts.DOUBLE, (opA - opB).ToString()));
                break;
            case "*":
                localExpressions.Push(new ExpressionHelper(em.Ts.DOUBLE, (opA * opB).ToString()));
                break;
            case "/":
                localExpressions.Push(new ExpressionHelper(em.Ts.DOUBLE, (opA / opB).ToString()));
                break;
            default:
                localExpressions.Clear();
                localExpressions.Push(new ExpressionHelper(em.Ts.DOUBLE, 1.ToString()));
                break;
        }
        return result;
    }
}

// TODO replace all logerrors with CDLExceptions