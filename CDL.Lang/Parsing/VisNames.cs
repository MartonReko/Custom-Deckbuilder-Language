using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using CDL.Lang.Parsing.Symboltable;
using CDL.Lang.Exceptions;
using CDL.Lang.GameModel;

namespace CDL.Lang.Parsing;

public class VisNames(EnvManager em, CDLExceptionHandler exceptionHandler, ObjectsHelper oHelper) : CDLBaseVisitor<object>
{
    private readonly ILogger<VisNames> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(LogLevel.Trace)).CreateLogger<VisNames>();
    private CDLExceptionHandler ExceptionHandler { get; set; } = exceptionHandler;
    private bool AddSymbolToTable(string typeName, ParserRuleContext varNameContext)
    {
        var type = em.Ts[typeName];
        var symbol = new Symbol(varNameContext.GetText(), type);
        bool addedSuccessfully = em.AddVariableToScope(varNameContext, symbol);
        return ((type != em.Ts.ERROR) && addedSuccessfully);
    }
    private readonly List<CDLType> localProps = [];
    private FnSymbol? currentFn;

    public override object VisitProgram([NotNull] CDLParser.ProgramContext context)
    {
        var result = base.VisitProgram(context);
        _logger.LogDebug("Final result of first visitor:\n{env}", em.Env.ToString());
        return result;
    }
    public override object VisitConfigBlock([NotNull] CDLParser.ConfigBlockContext context)
    {
        _logger.LogDebug("Config block \"{name}\" visited, Env:\n{env}", context.GetChild(0).GetType(), em.Env.ToString());
        return base.VisitConfigBlock(context);
    }
    public override object VisitVariableDeclaration([NotNull] CDLParser.VariableDeclarationContext context)
    {
        var type = em.Ts[context.typeName().GetText()];
        if (type == null)
        {
            _logger.LogError("Unknown type \"{type}\" at {pos}", context.typeName().GetText(), EnvManager.GetPos(context));
        }
        else
        {
            if (context.literalExpression() == null)
            {
                _logger.LogError("Undefined variable, this should not happen");
            }
            else
            {
                var expressionType = em.GetType(context.literalExpression());
                if (expressionType != null && !expressionType.InheritsFrom(type))
                {

                    _logger.LogError("Error at {pos}, type {type} of expression is not compatible with the type {typeName} of the variable", EnvManager.GetPos(context.literalExpression()), expressionType.Name, type.Name);

                    (int, int) pos = EnvManager.GetPosLineCol(context);
                    ExceptionHandler.AddException(new CDLException(pos.Item1, pos.Item2, $"Variable {context.varName().GetText()} must be {type.Name}, but {expressionType.Name} was given"));
                }
                else
                {
                    var symbol = new Symbol(context.varName().GetText(), type, context.literalExpression().GetText());
                    em.AddVariableToScope(context.varName(), symbol);
                }
            }
        }

        _logger.LogDebug("Var declaration visited, enviroment:\n{env}", em.Env.ToString());
        return base.VisitVariableDeclaration(context);
    }

    public override object VisitParamsDef([NotNull] CDLParser.ParamsDefContext context)
    {
        foreach (var item in context.typeNameVarName())
        {
            if (item != null)
            {
                var type = em.Ts[item.typeName().GetText()];
                if (type == em.Ts.ERROR)
                {
                    (int, int) pos = EnvManager.GetPosLineCol(context);
                    ExceptionHandler.AddException(new CDLException(pos.Item1, pos.Item2, "Type error"));
                }
                else
                {
                    var symbol = new Symbol(item.varName().GetText(), type);
                    currentFn?.AddParam(symbol.Name, symbol);
                }
            }
        }
        return base.VisitParamsDef(context);
    }

    // Rarity names are parsed as strings, later nodes can refer to cards by it
    public override object VisitRarityName([NotNull] CDLParser.RarityNameContext context)
    {
        var type = em.Ts.RARITY;
        if (type == em.Ts.ERROR)
        {

            (int, int) pos = EnvManager.GetPosLineCol(context);
            ExceptionHandler.AddException(new CDLException(pos.Item1, pos.Item2, "Type error"));
        }
        else
        {
            var symbol = new Symbol(context.GetText(), type);
            em.AddVariableToScope(context, symbol);
        }
        return base.VisitRarityName(context);
    }
    public override object VisitStageDefinition([NotNull] CDLParser.StageDefinitionContext context)
    {
        if (AddSymbolToTable(context.GetChild(0).GetText(), context.varName()))
            oHelper.Stages.Add(new Stage(context.varName().GetText()));
        return base.VisitStageDefinition(context);
    }
    public override object VisitNodeDefinition([NotNull] CDLParser.NodeDefinitionContext context)
    {
        if (AddSymbolToTable(context.GetChild(0).GetText(), context.varName()))
            oHelper.Nodes.Add(new Node(context.varName().GetText()));
        return base.VisitNodeDefinition(context);
    }
    public override object VisitCharSetup([NotNull] CDLParser.CharSetupContext context)
    {
        if (AddSymbolToTable(context.GetChild(0).GetText(), context.varName()))
            oHelper.Character = new ModelCharacter(context.varName().GetText());
        return base.VisitCharSetup(context);
    }
    public override object VisitEnemyDefinition([NotNull] CDLParser.EnemyDefinitionContext context)
    {
        if (AddSymbolToTable(context.GetChild(0).GetText(), context.varName()))
            oHelper.Enemies.Add(new Enemy(context.varName().GetText()));
        return base.VisitEnemyDefinition(context);
    }
    public override object VisitEnemyActionDefinition([NotNull] CDLParser.EnemyActionDefinitionContext context)
    {
        if (AddSymbolToTable(context.GetChild(0).GetText(), context.varName()))
            oHelper.EnemyActions.Add(new EnemyAction(context.varName().GetText()));
        return base.VisitEnemyActionDefinition(context);
    }
    public override object VisitCardDefinition([NotNull] CDLParser.CardDefinitionContext context)
    {
        if (AddSymbolToTable(context.GetChild(0).GetText(), context.varName()))
            oHelper.Cards.Add(new Card(context.varName().GetText()));
        return base.VisitCardDefinition(context);
    }
    public override object VisitEffectDefinition([NotNull] CDLParser.EffectDefinitionContext context)
    {
        var type = em.Ts[context.GetChild(0).GetText()];
        if (type == em.Ts.ERROR)
        {
            (int, int) pos = EnvManager.GetPosLineCol(context);
            ExceptionHandler.AddException(new CDLException(pos.Item1, pos.Item2, "Type error"));
        }
        string symbolText = context.varName().GetText();
        currentFn = new FnSymbol(symbolText, type);
        var result = base.VisitEffectDefinition(context);
        em.AddVariableToScope(context.varName(), currentFn);
        oHelper.Effects.Add(new Effect(context.varName().GetText()));

        localProps.Clear();
        _logger.LogDebug("Effect definition visited, enviroment:\n{env}", em.Env.ToString());
        return result;
    }
}
